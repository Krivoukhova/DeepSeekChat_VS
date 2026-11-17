namespace DeepSeekChat.Models;

/// <summary>
/// An image prepared for sending to the API.
/// </summary>
/// <param name="MimeType">
/// Image MIME type.
/// </param>
/// <param name="Base64">
/// File content encoded as Base64.
/// </param>
internal sealed record ImagePart(string MimeType, string Base64);
