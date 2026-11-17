using DeepSeekChat.Models;

namespace DeepSeekChat.Services;

/// <summary>
/// Chat history storage (chats.json).
/// </summary>
internal sealed class ChatHistoryStore
{
    private readonly object _sync = new();

    /// <summary>
    /// Loads all saved chats.
    /// </summary>
    /// <returns>
    /// The chat list; empty if the history is missing or corrupted.
    /// </returns>
    public IReadOnlyList<ChatHistoryItem> LoadAll()
    {
        lock (_sync)
        {
            return ReadAll();
        }
    }

    /// <summary>
    /// Saves a chat: updates the existing one by id or adds a new one.
    /// </summary>
    /// <param name="chat">
    /// The chat to save.
    /// </param>
    public void Save(ChatHistoryItem chat)
    {
        lock (_sync)
        {
            List<ChatHistoryItem> chats = ReadAll();
            int index = chats.FindIndex(item => item.Id == chat.Id);

            if (index >= 0)
            {
                chats[index] = chat;
            }
            else
            {
                chats.Add(chat);
            }

            JsonFile.Write(AppPaths.ChatsFile, chats);
        }
    }

    /// <summary>
    /// Removes a chat from the history.
    /// </summary>
    /// <param name="chatId">
    /// Chat id.
    /// </param>
    public void Delete(string chatId)
    {
        lock (_sync)
        {
            List<ChatHistoryItem> chats = ReadAll();
            ChatHistoryItem? chat = chats.Find(item => item.Id == chatId);
            if (chat is null)
            {
                return;
            }

            chats.Remove(chat);
            JsonFile.Write(AppPaths.ChatsFile, chats);
            DeleteImages(chat);
        }
    }

    /// <summary>
    /// Deletes cached photo copies of a removed chat; only files inside the images directory are touched.
    /// </summary>
    private static void DeleteImages(ChatHistoryItem chat)
    {
        string root = Path.GetFullPath(AppPaths.ImagesDirectory) + Path.DirectorySeparatorChar;

        foreach (string path in chat.Messages.SelectMany(message => message.ImagePaths ?? new List<string>()))
        {
            try
            {
                string full = Path.GetFullPath(path);
                if (full.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(full);
                }
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or NotSupportedException)
            {
                System.Diagnostics.Trace.TraceWarning($"Failed to delete a cached image: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Reads the history from the file and guarantees every chat has a message list. Must be called under the lock.
    /// </summary>
    /// <returns>
    /// A mutable chat list.
    /// </returns>
    private static List<ChatHistoryItem> ReadAll()
    {
        var chats = JsonFile.TryRead<List<ChatHistoryItem>>(AppPaths.ChatsFile) ?? new List<ChatHistoryItem>();

        foreach (ChatHistoryItem chat in chats)
        {
            chat.Messages ??= new List<ChatMessageItem>();
        }

        return chats;
    }
}
