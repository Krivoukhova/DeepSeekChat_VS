using System.Runtime.Serialization;
using DeepSeekChat.Models;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Page with the list of saved chats.
/// </summary>
[DataContract]
internal sealed class HistoryViewModel : NotifyPropertyChangedObject
{
    private readonly ChatHistoryStore _store;
    private readonly ConversationViewModel _conversation;
    private readonly UiContext _ui;
    private readonly UserPrompts _prompts;
    private readonly PageNavigator _navigator;

    private bool _isEmpty;

    /// <summary>
    /// Initializes a new instance of the <see cref="HistoryViewModel"/> class.
    /// </summary>
    public HistoryViewModel(
        ChatHistoryStore store,
        ConversationViewModel conversation,
        UiContext ui,
        UserPrompts prompts,
        PageNavigator navigator)
    {
        _store = store;
        _conversation = conversation;
        _ui = ui;
        _prompts = prompts;
        _navigator = navigator;
    }

    /// <summary>
    /// Gets items.
    /// </summary>
    [DataMember]
    public ObservableList<HistoryItemViewModel> Items { get; } = new();

    /// <summary>
    /// Gets or sets is empty.
    /// </summary>
    [DataMember]
    public bool IsEmpty { get => _isEmpty; private set => SetProperty(ref _isEmpty, value); }

    /// <summary>
    /// Reloads the history from storage; newest chats come first.
    /// </summary>
    public void Refresh()
    {
        List<HistoryItemViewModel> items = _store.LoadAll()
            .OrderByDescending(chat => chat.UpdatedAt)
            .Select(chat => new HistoryItemViewModel(chat, _ui, OpenAsync, DeleteAsync))
            .ToList();

        Items.Clear();
        Items.AddRange(items);
        IsEmpty = items.Count == 0;
    }

    /// <summary>
    /// Opens the selected chat and switches the window to the chat page.
    /// </summary>
    /// <param name="chat">
    /// The selected chat.
    /// </param>
    /// <returns>
    /// A completed task.
    /// </returns>
    private Task OpenAsync(ChatHistoryItem chat)
    {
        _conversation.OpenChat(chat);
        _navigator.Show(AppPage.Chat);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes a chat after confirmation and refreshes the list.
    /// </summary>
    /// <param name="chat">
    /// The chat to delete.
    /// </param>
    private async Task DeleteAsync(ChatHistoryItem chat)
    {
        string question = string.Format(_ui.Translate("ConfirmDeleteChat"), chat.Title);
        if (!await _prompts.ConfirmAsync(question, CancellationToken.None))
        {
            return;
        }

        _conversation.DeleteChat(chat.Id);
        Refresh();
    }
}
