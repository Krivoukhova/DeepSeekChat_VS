using DeepSeekChat.Constants;

namespace DeepSeekChat.Models;

/// <summary>
/// A file or photo attached to an outgoing message.
/// </summary>
/// <param name="FilePath">
/// Full path to the file.
/// </param>
/// <param name="IsImage">
/// Indicates that the file must be sent as an image.
/// </param>
internal sealed record Attachment(string FilePath, bool IsImage)
{
    public string Name => Path.GetFileName(FilePath);

    public string Icon => IsImage ? ChatConstants.PhotoIcon : ChatConstants.FileIcon;

    /// <summary>
    /// Creates an attachment and determines its kind from the file extension.
    /// </summary>
    /// <param name="filePath">
    /// Full path to the file.
    /// </param>
    /// <param name="pickedAsPhoto">
    /// The file was picked via the "Add photo" item.
    /// </param>
    /// <returns>
    /// The new attachment.
    /// </returns>
    public static Attachment Create(string filePath, bool pickedAsPhoto)
    {
        string extension = Path.GetExtension(filePath);
        bool hasImageExtension = ChatConstants.ImageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        return new Attachment(filePath, pickedAsPhoto || hasImageExtension);
    }
}
