using DeepSeekChat.Constants;
using DeepSeekChat.Models;

namespace DeepSeekChat.Services;

/// <summary>
/// The current chat and its persistence to the history. 
/// Empty chats are never saved.
/// </summary>
internal sealed class ChatSession
{
    private readonly ChatHistoryStore _store;
    private readonly HashSet<string> _deletedIds = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatSession"/> class.
    /// </summary>
    public ChatSession(ChatHistoryStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Gets or sets current.
    /// </summary>
    public ChatHistoryItem Current { get; private set; } = new();

    /// <summary>
    /// Adds a message to the current chat and saves the chat. 
    /// The title comes from the first user message.
    /// </summary>
    /// <param name="sender">
    /// Sender name.
    /// </param>
    /// <param name="text">
    /// Message text.
    /// </param>
    /// <param name="isAi">
    /// The message was written by the model.
    /// </param>
    /// <param name="imagePaths">
    /// Paths to copies of the message photos.
    /// </param>
    public void AddMessage(string sender, string text, bool isAi, IReadOnlyList<string> imagePaths) =>
        AddMessage(Current, sender, text, isAi, imagePaths, isError: false);

    /// <summary>
    /// Adds a message to a specific chat, which may no longer be the current one
    /// (the user can switch chats while a reply is pending).
    /// A chat deleted in the meantime is not saved again.
    /// </summary>
    /// <param name="chat">
    /// The chat the message belongs to.
    /// </param>
    /// <param name="sender">
    /// Sender name.
    /// </param>
    /// <param name="text">
    /// Message text.
    /// </param>
    /// <param name="isAi">
    /// The message was written by the model.
    /// </param>
    /// <param name="imagePaths">
    /// Paths to copies of the message photos.
    /// </param>
    /// <param name="isError">
    /// The message is an error notice.
    /// </param>
    public void AddMessage(
        ChatHistoryItem chat,
        string sender,
        string text,
        bool isAi,
        IReadOnlyList<string> imagePaths,
        bool isError)
    {
        chat.Messages.Add(new ChatMessageItem
        {
            Sender = sender,
            Message = text,
            IsAi = isAi,
            IsError = isError,
            CreatedAt = DateTime.Now,
            ImagePaths = imagePaths.ToList(),
        });

        if (!isAi && chat.Title == ChatConstants.DefaultChatTitle)
        {
            chat.Title = CreateTitle(text);
        }

        chat.UpdatedAt = DateTime.Now;

        if (!_deletedIds.Contains(chat.Id))
        {
            _store.Save(chat);
        }
    }

    /// <summary>
    /// Saves the current chat and starts a new one.
    /// </summary>
    public void StartNew()
    {
        Persist();
        Current = new ChatHistoryItem();
    }

    /// <summary>
    /// Saves the current chat and switches to the one picked from the history.
    /// </summary>
    /// <param name="chat">
    /// The chat to open.
    /// </param>
    public void Open(ChatHistoryItem chat)
    {
        Persist();
        _deletedIds.Remove(chat.Id);
        Current = chat;
    }

    /// <summary>
    /// Removes a chat from the history; 
    /// if it was the current chat, starts a new empty one.
    /// </summary>
    /// <param name="chatId">
    /// Id of the chat to delete.
    /// </param>
    /// <returns>
    /// true if the current chat was deleted; 
    /// otherwise false.
    /// </returns>
    public bool Delete(string chatId)
    {
        _deletedIds.Add(chatId);
        _store.Delete(chatId);

        if (Current.Id != chatId)
        {
            return false;
        }

        Current = new ChatHistoryItem();
        return true;
    }

    /// <summary>
    /// Saves the current chat to the history; 
    /// empty chats are skipped.
    /// </summary>
    private void Persist()
    {
        if (Current.Messages.Count > 0)
        {
            _store.Save(Current);
        }
    }

    /// <summary>
    /// Builds a chat title from the first message: 
    /// a single line, no longer than the allowed length.
    /// </summary>
    /// <param name="message">
    /// Text of the first user message.
    /// </param>
    /// <returns>
    /// The chat title.
    /// </returns>
    private static string CreateTitle(string message)
    {
        string title = message.Replace("\r", " ").Replace("\n", " ").Trim();

        if (title.Length == 0)
        {
            return ChatConstants.DefaultChatTitle;
        }

        return title.Length > ChatConstants.TitleMaxLength
            ? title[..ChatConstants.TitleMaxLength].Trim() + ChatConstants.TitleEllipsis
            : title;
    }
}
