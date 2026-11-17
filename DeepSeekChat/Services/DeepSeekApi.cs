using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DeepSeekChat.Constants;
using DeepSeekChat.Models;

namespace DeepSeekChat.Services;

/// <summary>
/// DeepSeek HTTP API client: sends messages, 
/// validates the key and fetches the balance.
/// </summary>
internal sealed class DeepSeekApi
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromMinutes(5) };
    private readonly ApiKeyStore _keyStore;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeepSeekApi"/> class.
    /// </summary>
    public DeepSeekApi(ApiKeyStore keyStore)
    {
        _keyStore = keyStore;
    }

    /// <summary>
    /// Sends a request to the model and returns its reply.
    /// </summary>
    /// <param name="prompt">
    /// Prompt text.
    /// </param>
    /// <param name="images">
    /// Images attached to the request.
    /// </param>
    /// <param name="history">
    /// Earlier messages of the chat (without the new one), sent as context.
    /// </param>
    /// <param name="userName">
    /// The name to address the user by; an empty string if no name is set.
    /// </param>
    /// <param name="model">
    /// The model that handles the request.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The model's reply text.
    /// </returns>
    /// <exception cref="DeepSeekApiException">
    /// The server returned a failure status.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// The response contains no message text.
    /// </exception>
    public async Task<string> SendAsync(
        string prompt,
        IReadOnlyList<ImagePart> images,
        IReadOnlyList<ChatMessageItem> history,
        string userName,
        ChatModel model,
        CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = CreateRequest(HttpMethod.Post, ApiConstants.ChatCompletionsPath, _keyStore.Load());
        string payload = BuildChatPayload(prompt, images, history, userName, model);
        request.Content = new StringContent(payload, Encoding.UTF8, ApiConstants.JsonMediaType);

        using HttpResponseMessage response = await _http.SendAsync(request, cancellationToken).ConfigureAwait(false);
        string body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new DeepSeekApiException(response.StatusCode, body);
        }

        return ParseReply(body);
    }

    /// <summary>
    /// Validates the key by requesting the balance.
    /// </summary>
    /// <param name="apiKey">
    /// The key to validate.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The check result; for a rejected key it holds the server error text.
    /// </returns>
    public async Task<KeyValidation> ValidateKeyAsync(string apiKey, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return KeyValidation.Empty;
        }

        using HttpRequestMessage request = CreateRequest(HttpMethod.Get, ApiConstants.BalancePath, apiKey);
        using HttpResponseMessage response = await _http.SendAsync(request, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            return KeyValidation.Accepted;
        }

        string details = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        return KeyValidation.Rejected(string.IsNullOrWhiteSpace(details) ? response.StatusCode.ToString() : details);
    }

    /// <summary>
    /// Gets the key state and the account balance.
    /// </summary>
    /// <param name="apiKey">
    /// API key.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The account information; network errors are reflected in the state instead of being thrown.
    /// </returns>
    public async Task<AccountInfo> GetAccountAsync(string apiKey, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return new AccountInfo(KeyState.Missing, null, AccountState.NoKey);
        }

        try
        {
            using HttpRequestMessage request = CreateRequest(HttpMethod.Get, ApiConstants.BalancePath, apiKey);
            using HttpResponseMessage response = await _http.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new AccountInfo(KeyState.Invalid, null, AccountState.Error);
            }

            string body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return ParseAccount(body);
        }
        catch (Exception ex) when (ex is HttpRequestException or JsonException)
        {
            return new AccountInfo(KeyState.CheckFailed, null, AccountState.NoConnection);
        }
    }

    #region Request construction

    /// <summary>
    /// Creates a request with an authorization header.
    /// </summary>
    /// <param name="method">
    /// HTTP method.
    /// </param>
    /// <param name="path">
    /// Path relative to the API base address.
    /// </param>
    /// <param name="apiKey">
    /// API key.
    /// </param>
    /// <returns>
    /// The new HTTP request.
    /// </returns>
    private static HttpRequestMessage CreateRequest(HttpMethod method, string path, string apiKey)
    {
        var request = new HttpRequestMessage(method, ApiConstants.BaseUrl + path);
        request.Headers.Authorization = new AuthenticationHeaderValue(ApiConstants.BearerScheme, apiKey);
        return request;
    }

    /// <summary>
    /// Builds the JSON request for the model; adds a system message when a name is set.
    /// </summary>
    /// <param name="prompt">
    /// Prompt text.
    /// </param>
    /// <param name="images">
    /// Request images.
    /// </param>
    /// <param name="userName">
    /// User name or an empty string.
    /// </param>
    /// <param name="model">
    /// The model that handles the request.
    /// </param>
    /// <returns>
    /// The request body as JSON.
    /// </returns>
    private static string BuildChatPayload(
        string prompt,
        IReadOnlyList<ImagePart> images,
        IReadOnlyList<ChatMessageItem> history,
        string userName,
        ChatModel model)
    {
        var messages = new List<object>();

        if (!string.IsNullOrWhiteSpace(userName))
        {
            messages.Add(new
            {
                role = "system",
                content = string.Format(ApiConstants.UserNameInstructionFormat, userName.Trim()),
            });
        }

        foreach (ChatMessageItem item in TrimHistory(history))
        {
            messages.Add(new { role = item.IsAi ? "assistant" : "user", content = item.Message });
        }

        messages.Add(new { role = "user", content = BuildUserContent(prompt, images) });

        var payload = new Dictionary<string, object>
        {
            ["model"] = model.ApiName,
            ["messages"] = messages,
            ["thinking"] = new { type = ApiConstants.ThinkingDisabled },
            ["temperature"] = ApiConstants.Temperature,
        };

        return JsonSerializer.Serialize(payload);
    }

    /// <summary>
    /// Keeps the most recent messages that fit the context budget.
    /// </summary>
    private static IEnumerable<ChatMessageItem> TrimHistory(IReadOnlyList<ChatMessageItem> history)
    {
        var kept = new List<ChatMessageItem>();
        int total = 0;

        for (int i = history.Count - 1; i >= 0 && kept.Count < ApiConstants.MaxHistoryMessages; i--)
        {
            total += history[i].Message.Length;
            if (total > ApiConstants.MaxHistoryChars)
            {
                break;
            }

            kept.Add(history[i]);
        }

        kept.Reverse();
        return kept;
    }

    /// <summary>
    /// Builds the user message content: a string, or a list of parts when images are present.
    /// </summary>
    /// <param name="prompt">
    /// Prompt text.
    /// </param>
    /// <param name="images">
    /// Request images.
    /// </param>
    /// <returns>
    /// The value for the content field.
    /// </returns>
    private static object BuildUserContent(string prompt, IReadOnlyList<ImagePart> images)
    {
        if (images.Count == 0)
        {
            return prompt;
        }

        var parts = new List<object> { new { type = "text", text = prompt } };
        parts.AddRange(images.Select(image => new
        {
            type = "image_url",
            image_url = new { url = $"data:{image.MimeType};base64,{image.Base64}" },
        }));

        return parts;
    }

    #endregion

    #region Response parsing

    /// <summary>
    /// Extracts the reply text from JSON.
    /// </summary>
    /// <param name="body">
    /// Server response body.
    /// </param>
    /// <returns>
    /// The model's reply text.
    /// </returns>
    private static string ParseReply(string body)
    {
        using JsonDocument document = JsonDocument.Parse(body);

        return document.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()
            ?? throw new InvalidDataException("The response contains no message content.");
    }

    /// <summary>
    /// Parses the balance response.
    /// </summary>
    /// <param name="body">
    /// Server response body.
    /// </param>
    /// <returns>
    /// The account information.
    /// </returns>
    private static AccountInfo ParseAccount(string body)
    {
        using JsonDocument document = JsonDocument.Parse(body);
        JsonElement root = document.RootElement;

        bool isAvailable = root.TryGetProperty("is_available", out JsonElement available)
            && available.ValueKind == JsonValueKind.True;

        return new AccountInfo(
            KeyState.Valid,
            FindUsdBalance(root),
            isAvailable ? AccountState.Active : AccountState.InsufficientFunds);
    }

    /// <summary>
    /// Looks for the US dollar balance among the account balances.
    /// </summary>
    /// <param name="root">
    /// The response root element.
    /// </param>
    /// <returns>
    /// The balance prefixed with "$"; null if there is no dollar balance.
    /// </returns>
    private static string? FindUsdBalance(JsonElement root)
    {
        if (!root.TryGetProperty("balance_infos", out JsonElement infos) || infos.ValueKind != JsonValueKind.Array)
        {
            return null;
        }

        foreach (JsonElement info in infos.EnumerateArray())
        {
            bool isUsd = info.TryGetProperty("currency", out JsonElement currency)
                && currency.GetString() == ApiConstants.UsdCurrency;

            if (isUsd && info.TryGetProperty("total_balance", out JsonElement total))
            {
                return "$" + total.GetString();
            }
        }

        return null;
    }
    #endregion
}
