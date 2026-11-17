using System.Text;
using DeepSeekChat.Constants;
using DeepSeekChat.Models;

namespace DeepSeekChat.Services;

/// <summary>
/// Turns a message and its attachments into a model request: 
/// text files are embedded in the text, photos are encoded.
/// </summary>
internal sealed class AttachmentReader
{
    private readonly ImageCache _imageCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentReader"/> class.
    /// </summary>
    public AttachmentReader(ImageCache imageCache)
    {
        _imageCache = imageCache;
    }

    /// <summary>
    /// Reads the attachments and builds the final message.
    /// </summary>
    /// <param name="message">
    /// Text typed by the user.
    /// </param>
    /// <param name="attachments">
    /// Attached files.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The prepared message.
    /// </returns>
    /// <exception cref="AttachmentException">
    /// The file is too large or could not be read.
    /// </exception>
    public async Task<PreparedMessage> ReadAsync(
        string message,
        IReadOnlyList<Attachment> attachments,
        CancellationToken cancellationToken)
    {
        var prompt = new StringBuilder(message);
        var displayLines = new List<string>();
        var images = new List<ImagePart>();
        var cachedImagePaths = new List<string>();

        if (message.Length > 0)
        {
            displayLines.Add(message);
        }

        foreach (Attachment attachment in attachments)
        {
            try
            {
                if (attachment.IsImage)
                {
                    images.Add(await ReadImageAsync(attachment, cancellationToken));
                    cachedImagePaths.Add(_imageCache.Store(attachment.FilePath));
                }
                else
                {
                    string content = await ReadTextAsync(attachment, cancellationToken);
                    prompt.AppendFormat(ChatConstants.AttachmentBlockFormat, attachment.Name, content);
                }
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                throw new AttachmentException(AttachmentFailure.Unreadable, attachment.Name, ex);
            }

            displayLines.Add(string.Format(ChatConstants.AttachmentLineFormat, attachment.Icon, attachment.Name));
        }

        return new PreparedMessage(prompt.ToString(), string.Join('\n', displayLines), images, cachedImagePaths);
    }

    /// <summary>
    /// Reads an image and encodes it as Base64.
    /// </summary>
    /// <param name="attachment">
    /// The image attachment.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The image, ready to send to the API.
    /// </returns>
    private static async Task<ImagePart> ReadImageAsync(Attachment attachment, CancellationToken cancellationToken)
    {
        EnsureSize(attachment, ChatConstants.MaxImageBytes);

        byte[] bytes = await File.ReadAllBytesAsync(attachment.FilePath, cancellationToken);
        return new ImagePart(GetMimeType(attachment.FilePath), Convert.ToBase64String(bytes));
    }

    /// <summary>
    /// Reads a text file in full.
    /// </summary>
    /// <param name="attachment">
    /// The text attachment.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The file content.
    /// </returns>
    private static Task<string> ReadTextAsync(Attachment attachment, CancellationToken cancellationToken)
    {
        EnsureSize(attachment, ChatConstants.MaxTextFileBytes);
        return File.ReadAllTextAsync(attachment.FilePath, cancellationToken);
    }

    /// <summary>
    /// Checks that the file size does not exceed the limit.
    /// </summary>
    /// <param name="attachment">
    /// The attachment to check.
    /// </param>
    /// <param name="maxBytes">
    /// Maximum allowed size in bytes.
    /// </param>
    private static void EnsureSize(Attachment attachment, long maxBytes)
    {
        if (new FileInfo(attachment.FilePath).Length > maxBytes)
        {
            throw new AttachmentException(AttachmentFailure.TooLarge, attachment.Name);
        }
    }

    private static string GetMimeType(string filePath) => Path.GetExtension(filePath).ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".gif" => "image/gif",
        ".webp" => "image/webp",
        _ => "image/png",
    };
}
