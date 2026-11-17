namespace DeepSeekChat.Services;

/// <summary>
/// Reason an attachment cannot be sent.
/// </summary>
internal enum AttachmentFailure
{
    TooLarge,
    Unreadable,
}

/// <summary>
/// Error while preparing an attachment for sending.
/// </summary>
internal sealed class AttachmentException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentException"/> class.
    /// </summary>
    public AttachmentException(AttachmentFailure failure, string fileName, Exception? inner = null)
        : base($"{failure}: {fileName}", inner)
    {
        Failure = failure;
        FileName = fileName;
    }

    /// <summary>
    /// Gets failure.
    /// </summary>
    public AttachmentFailure Failure { get; }

    /// <summary>
    /// Gets file name.
    /// </summary>
    public string FileName { get; }
}
