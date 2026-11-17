namespace DeepSeekChat.Models;

/// <summary>
/// A model that can be picked in the chat.
/// </summary>
/// <param name="Id">
/// Id stored in the preferences.
/// </param>
/// <param name="ApiName">
/// Model name sent to the DeepSeek API.
/// </param>
/// <param name="DisplayName">
/// Name shown in the picker.
/// </param>
/// <param name="SupportsImages">
/// Whether the model accepts images.
/// </param>
internal sealed record ChatModel(string Id, string ApiName, string DisplayName, bool SupportsImages);
