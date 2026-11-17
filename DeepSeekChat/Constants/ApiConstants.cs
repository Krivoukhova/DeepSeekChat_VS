namespace DeepSeekChat.Constants;

/// <summary>
/// Constants for working with the DeepSeek HTTP API.
/// </summary>
internal static class ApiConstants
{
    /// <summary>
    /// Gets the base url.
    /// </summary>
    public const string BaseUrl = "https://api.deepseek.com";
    /// <summary>
    /// Gets the chat completions path.
    /// </summary>
    public const string ChatCompletionsPath = "/chat/completions";
    /// <summary>
    /// Gets the balance path.
    /// </summary>
    public const string BalancePath = "/user/balance";
    /// <summary>
    /// Gets the thinking disabled.
    /// </summary>
    public const string ThinkingDisabled = "disabled";
    /// <summary>
    /// Gets the temperature.
    /// </summary>
    public const double Temperature = 0.7;
    /// <summary>
    /// Gets the bearer scheme.
    /// </summary>
    public const string BearerScheme = "Bearer";
    /// <summary>
    /// Gets the json media type.
    /// </summary>
    public const string JsonMediaType = "application/json";
    /// <summary>
    /// Gets the usd currency.
    /// </summary>
    public const string UsdCurrency = "USD";

    /// <summary>
    /// Maximum number of earlier messages sent as context.
    /// </summary>
    public const int MaxHistoryMessages = 20;

    /// <summary>
    /// Maximum total length of the earlier messages sent as context.
    /// </summary>
    public const int MaxHistoryChars = 60000;

    /// <summary>
    /// System instruction carrying the user's name; {0} is the name.
    /// </summary>
    public const string UserNameInstructionFormat =
        "The user's name is {0}. Address the user by this name when it feels natural, " +
        "and always reply in the language the user writes in.";
}
