using System.Runtime.Serialization;
using DeepSeekChat.Models;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// A row of the saved chats list.
/// </summary>
[DataContract]
internal sealed class HistoryItemViewModel : ThemedItem
{
    /// <summary>
    /// Creates a history row.
    /// </summary>
    /// <param name="chat">
    /// The saved chat.
    /// </param>
    /// <param name="ui">
    /// Shared appearance state.
    /// </param>
    /// <param name="open">
    /// Action invoked when the row is picked.
    /// </param>
    /// <param name="delete">
    /// Action invoked when the trash button is clicked.
    /// </param>
    public HistoryItemViewModel(
        ChatHistoryItem chat,
        UiContext ui,
        Func<ChatHistoryItem, Task> open,
        Func<ChatHistoryItem, Task> delete)
        : base(ui)
    {
        Title = chat.Title;
        UpdatedAtText = chat.UpdatedAt.ToString("g");
        OpenCommand = new AsyncCommand((parameter, token) => open(chat));
        DeleteCommand = new AsyncCommand((parameter, token) => delete(chat));
    }

    /// <summary>
    /// Gets title.
    /// </summary>
    [DataMember]
    public string Title { get; }

    /// <summary>
    /// Gets updated at text.
    /// </summary>
    [DataMember]
    public string UpdatedAtText { get; }

    /// <summary>
    /// Gets open command.
    /// </summary>
    [DataMember]
    public AsyncCommand OpenCommand { get; }

    /// <summary>
    /// Gets delete command.
    /// </summary>
    [DataMember]
    public AsyncCommand DeleteCommand { get; }
}
