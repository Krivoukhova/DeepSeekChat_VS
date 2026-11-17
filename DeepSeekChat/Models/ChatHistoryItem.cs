using DeepSeekChat.Constants;

namespace DeepSeekChat.Models;

/// <summary>
/// A saved chat: 
/// title, modification time and message list.
/// </summary>
public sealed class ChatHistoryItem
{
    /// <summary>
    /// Gets or sets id.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    public string Title { get; set; } = ChatConstants.DefaultChatTitle;

    /// <summary>
    /// Gets or sets created at.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets updated at.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets messages.
    /// </summary>
    public List<ChatMessageItem> Messages { get; set; } = new();
}
