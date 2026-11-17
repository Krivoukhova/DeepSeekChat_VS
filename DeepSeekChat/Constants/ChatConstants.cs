namespace DeepSeekChat.Constants;

/// <summary>
/// Chat UI constants and attachment limits.
/// </summary>
internal static class ChatConstants
{
    /// <summary>
    /// Gets the app name.
    /// </summary>
    public const string AppName = "DeepSeek";
    /// <summary>
    /// Gets the default chat title.
    /// </summary>
    public const string DefaultChatTitle = "New Chat";
    /// <summary>
    /// Gets the title ellipsis.
    /// </summary>
    public const string TitleEllipsis = "...";
    /// <summary>
    /// Gets the title max length.
    /// </summary>
    public const int TitleMaxLength = 45;

    /// <summary>
    /// Gets the bot emoji.
    /// </summary>
    public const string BotEmoji = "🤖";
    /// <summary>
    /// Gets the user emoji.
    /// </summary>
    public const string UserEmoji = "👤";
    /// <summary>
    /// Gets the photo icon.
    /// </summary>
    public const string PhotoIcon = "🖼";
    /// <summary>
    /// Gets the file icon.
    /// </summary>
    public const string FileIcon = "📎";
    /// <summary>
    /// Gets the copy icon.
    /// </summary>
    public const string CopyIcon = "📋";
    /// <summary>
    /// Gets the copied icon.
    /// </summary>
    public const string CopiedIcon = "✓";
    /// <summary>
    /// Gets the not available.
    /// </summary>
    public const string NotAvailable = "—";
    /// <summary>
    /// Gets the loading.
    /// </summary>
    public const string Loading = "…";
    /// <summary>
    /// Gets the error color.
    /// </summary>
    public const string ErrorColor = "#E5484D";

    /// <summary>
    /// Gets the copy feedback ms.
    /// </summary>
    public const int CopyFeedbackMs = 1200;
    /// <summary>
    /// Gets the masked key max length.
    /// </summary>
    public const int MaskedKeyMaxLength = 40;
    /// <summary>
    /// Gets the save feedback ms.
    /// </summary>
    public const int SaveFeedbackMs = 500;
    /// <summary>
    /// Gets the name saved visible ms.
    /// </summary>
    public const int NameSavedVisibleMs = 1500;

    /// <summary>
    /// Gets the max text file bytes.
    /// </summary>
    public const long MaxTextFileBytes = 200 * 1024;
    /// <summary>
    /// Gets the max image bytes.
    /// </summary>
    public const long MaxImageBytes = 5 * 1024 * 1024;

    /// <summary>
    /// Format of the block holding a text attachment; {0} is the file name, {1} is the text.
    /// </summary>
    public const string AttachmentBlockFormat = "\n\n--- {0} ---\n{1}\n--- end ---";

    /// <summary>
    /// Format of an attachment line in the message text; {0} is the icon, {1} is the file name.
    /// </summary>
    public const string AttachmentLineFormat = "{0} {1}";

    /// <summary>
    /// Gets the image extensions.
    /// </summary>
    public static readonly string[] ImageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".webp" };
}
