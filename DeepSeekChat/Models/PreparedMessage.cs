namespace DeepSeekChat.Models;

/// <summary>
/// A user message after attachments are processed.
/// </summary>
/// <param name="Prompt">
/// The prompt for the model, including the content of text files.
/// </param>
/// <param name="DisplayText">
/// The text shown in the chat: the message and the attachment list.
/// </param>
/// <param name="Images">
/// Images to send to the API.
/// </param>
/// <param name="CachedImagePaths">
/// Paths to local image copies used for thumbnails.
/// </param>
internal sealed record PreparedMessage(
    string Prompt,
    string DisplayText,
    IReadOnlyList<ImagePart> Images,
    IReadOnlyList<string> CachedImagePaths);
