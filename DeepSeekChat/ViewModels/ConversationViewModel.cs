using System.Runtime.Serialization;
using DeepSeekChat.Constants;
using DeepSeekChat.Models;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Chat page: messages, input box, attachments, sending and current chat management.
/// </summary>
[DataContract]
internal sealed class ConversationViewModel : NotifyPropertyChangedObject
{
    private readonly ChatSession _session;
    private readonly DeepSeekApi _api;
    private readonly AttachmentReader _attachmentReader;
    private readonly UiContext _ui;
    private readonly UserPrompts _prompts;
    private readonly PageNavigator _navigator;

    private string _inputText = string.Empty;
    private bool _showWelcome = true;
    private bool _isTyping;
    private bool _hasAttachments;
    private bool _isAttachMenuOpen;
    private bool _isSending;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConversationViewModel"/> class.
    /// </summary>
    public ConversationViewModel(
        ChatSession session,
        DeepSeekApi api,
        AttachmentReader attachmentReader,
        UiContext ui,
        UserPrompts prompts,
        PageNavigator navigator)
    {
        _session = session;
        _api = api;
        _attachmentReader = attachmentReader;
        _ui = ui;
        _prompts = prompts;
        _navigator = navigator;

        SendCommand = new AsyncCommand((parameter, token) => SendAsync(parameter as string, token));
        QuickActionCommand = new AsyncCommand((parameter, token) => FillInputAsync(parameter as string));
        NewChatCommand = new AsyncCommand((parameter, token) => StartNewChatAsync());
        DeleteChatCommand = new AsyncCommand(DeleteCurrentChatAsync);
        OpenAttachMenuCommand = new AsyncCommand((parameter, token) => OpenAttachMenuAsync());
        AddPhotoCommand = new AsyncCommand((parameter, token) => AddAttachmentAsync(pickPhoto: true));
        AddFileCommand = new AsyncCommand((parameter, token) => AddAttachmentAsync(pickPhoto: false));
    }

    /// <summary>
    /// Gets messages.
    /// </summary>
    [DataMember]
    public ObservableList<MessageViewModel> Messages { get; } = new();

    /// <summary>
    /// Gets attachments.
    /// </summary>
    [DataMember]
    public ObservableList<AttachmentViewModel> Attachments { get; } = new();

    /// <summary>
    /// Gets or sets input text.
    /// </summary>
    [DataMember]
    public string InputText
    {
        get => _inputText;
        set => SetProperty(ref _inputText, value ?? string.Empty);
    }

    /// <summary>
    /// The welcome block is visible while the chat has no messages.
    /// </summary>
    [DataMember]
    public bool ShowWelcome { get => _showWelcome; private set => SetProperty(ref _showWelcome, value); }

    /// <summary>
    /// Gets or sets whether the assistant is currently generating a reply.
    /// </summary>
    [DataMember]
    public bool IsTyping { get => _isTyping; private set => SetProperty(ref _isTyping, value); }

    /// <summary>
    /// Gets or sets whether the composer contains attachments.
    /// </summary>
    [DataMember]
    public bool HasAttachments { get => _hasAttachments; private set => SetProperty(ref _hasAttachments, value); }

    /// <summary>
    /// Two-way bound to the drop-up list of the "+" button.
    /// </summary>
    /// <summary>
    /// Gets or sets whether the attachment menu is open.
    /// </summary>
    [DataMember]
    public bool IsAttachMenuOpen
    {
        get => _isAttachMenuOpen;
        set => SetProperty(ref _isAttachMenuOpen, value);
    }

    /// <summary>
    /// Gets send command.
    /// </summary>
    [DataMember]
    public AsyncCommand SendCommand { get; }

    /// <summary>
    /// Gets quick action command.
    /// </summary>
    [DataMember]
    public AsyncCommand QuickActionCommand { get; }

    /// <summary>
    /// Gets new chat command.
    /// </summary>
    [DataMember]
    public AsyncCommand NewChatCommand { get; }

    /// <summary>
    /// Gets delete chat command.
    /// </summary>
    [DataMember]
    public AsyncCommand DeleteChatCommand { get; }

    /// <summary>
    /// Gets open attach menu command.
    /// </summary>
    [DataMember]
    public AsyncCommand OpenAttachMenuCommand { get; }

    /// <summary>
    /// Gets add photo command.
    /// </summary>
    [DataMember]
    public AsyncCommand AddPhotoCommand { get; }

    /// <summary>
    /// Gets add file command.
    /// </summary>
    [DataMember]
    public AsyncCommand AddFileCommand { get; }

    /// <summary>
    /// Puts a request containing the code selected in the editor into the input box.
    /// </summary>
    /// <param name="code">
    /// The selected code.
    /// </param>
    /// <param name="requestType">
    /// Request kind: explanation or refactoring.
    /// </param>
    public void SetSelectedCode(string code, RequestType requestType)
    {
        string templateKey = requestType == RequestType.RefactorCode ? "RefactorTemplate" : "ExplainTemplate";
        InputText = string.Format(_ui.Translate(templateKey), code);
    }

    /// <summary>
    /// Shows a saved chat instead of the current one; the current chat is saved first.
    /// </summary>
    /// <param name="chat">
    /// The chat to open.
    /// </param>
    public void OpenChat(ChatHistoryItem chat)
    {
        _session.Open(chat);

        Messages.Clear();
        Messages.AddRange(chat.Messages.Select(CreateMessage).ToList());
        ClearComposer();
        ShowWelcome = Messages.Count == 0;
    }

    /// <summary>
    /// Removes a chat from the history; if it is open, clears the chat window.
    /// </summary>
    /// <param name="chatId">
    /// Id of the chat to delete.
    /// </param>
    public void DeleteChat(string chatId)
    {
        if (_session.Delete(chatId))
        {
            ClearConversation();
        }
    }

    private Task FillInputAsync(string? text)
    {
        InputText = text ?? string.Empty;
        return Task.CompletedTask;
    }

    private Task OpenAttachMenuAsync()
    {
        IsAttachMenuOpen = true;
        return Task.CompletedTask;
    }

    private Task StartNewChatAsync()
    {
        _session.StartNew();
        ClearConversation();
        _navigator.Show(AppPage.Chat);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes the current chat after user confirmation.
    /// </summary>
    /// <param name="parameter">
    /// Command parameter (unused).
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task DeleteCurrentChatAsync(object? parameter, CancellationToken cancellationToken)
    {
        if (!await _prompts.ConfirmAsync(_ui.Translate("ConfirmDeleteCurrent"), cancellationToken))
        {
            return;
        }

        DeleteChat(_session.Current.Id);
        _navigator.Show(AppPage.Chat);
    }

    /// <summary>
    /// Opens the file picker and adds the selected file to the attachments.
    /// </summary>
    /// <param name="pickPhoto">
    /// true to pick a photo, false to pick any file.
    /// </param>
    private async Task AddAttachmentAsync(bool pickPhoto)
    {
        IsAttachMenuOpen = false;

        string title = _ui.Translate(pickPhoto ? "AddPhoto" : "AddFile");
        string filter = pickPhoto ? FileDialogHelper.ImagesFilter : FileDialogHelper.AllFilesFilter;
        string? path = await FileDialogHelper.PickFileAsync(title, filter);

        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        Attachments.Add(new AttachmentViewModel(Attachment.Create(path, pickPhoto), _ui, RemoveAttachment));
        HasAttachments = true;
    }

    private void RemoveAttachment(AttachmentViewModel attachment)
    {
        Attachments.Remove(attachment);
        HasAttachments = Attachments.Count > 0;
    }

    /// <summary>
    /// Sends the message only if no send is in progress and there is something to send.
    /// </summary>
    /// <param name="text">
    /// Text from the input box.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task SendAsync(string? text, CancellationToken cancellationToken)
    {
        string message = text?.Trim() ?? string.Empty;
        bool isEmpty = message.Length == 0 && Attachments.Count == 0;

        if (_isSending || isEmpty)
        {
            return;
        }

        _isSending = true;
        try
        {
            await SendPreparedAsync(message, cancellationToken);
        }
        finally
        {
            IsTyping = false;
            _isSending = false;
        }
    }

    /// <summary>
    /// Processes attachments, shows the user's message and requests the model's reply.
    /// </summary>
    /// <param name="message">
    /// Message text.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task SendPreparedAsync(string message, CancellationToken cancellationToken)
    {
        PreparedMessage? prepared = await PrepareAsync(message, cancellationToken);
        if (prepared is null)
        {
            return;
        }

        ChatHistoryItem chat = _session.Current;
        IReadOnlyList<ChatMessageItem> history = chat.Messages.Where(item => !item.IsError).ToList();

        AddMessage(chat, _ui.Translate("SenderYou"), prepared.DisplayText, isAi: false, imagePaths: prepared.CachedImagePaths);
        ClearComposer();
        IsTyping = true;

        await RequestReplyAsync(chat, history, prepared, cancellationToken);
    }

    /// <summary>
    /// Prepares the message for sending; shows an attachment error in the chat.
    /// </summary>
    /// <param name="message">
    /// Message text.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The prepared message; null if an attachment could not be read.
    /// </returns>
    private async Task<PreparedMessage?> PrepareAsync(string message, CancellationToken cancellationToken)
    {
        List<Attachment> attachments = Attachments.Select(item => item.Source).ToList();

        try
        {
            return await _attachmentReader.ReadAsync(message, attachments, cancellationToken);
        }
        catch (AttachmentException ex)
        {
            string key = ex.Failure == AttachmentFailure.TooLarge ? "FileTooLarge" : "FileReadError";
            AddError(string.Format(_ui.Translate(key), ex.FileName));
            return null;
        }
    }

    /// <summary>
    /// Requests the model's reply and adds it to the chat; a request error is shown as a message.
    /// </summary>
    /// <param name="prepared">
    /// The prepared message.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task RequestReplyAsync(
        ChatHistoryItem chat,
        IReadOnlyList<ChatMessageItem> history,
        PreparedMessage prepared,
        CancellationToken cancellationToken)
    {
        try
        {
            ChatModel model = prepared.Images.Count > 0 ? ChatModels.ForImages(_ui.Model) : _ui.Model;
            string reply = await _api.SendAsync(
                prepared.Prompt,
                prepared.Images,
                history,
                _ui.UserName,
                model,
                cancellationToken);
            AddMessage(chat, ChatConstants.AppName, reply, isAi: true);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            AddError(chat, string.Format(_ui.Translate("ErrorApiResponse"), DescribeError(ex)));
        }
    }

    /// <summary>
    /// Shows an error in the current chat.
    /// </summary>
    private void AddError(string text) => AddError(_session.Current, text);

    /// <summary>
    /// Adds an error notice to a chat; error notices are not sent back to the model.
    /// </summary>
    private void AddError(ChatHistoryItem chat, string text) =>
        AddMessage(chat, _ui.Translate("Error"), text, isAi: true, isError: true);

    /// <summary>
    /// Short error text for the user: the server's own message when the response is JSON, otherwise the exception message.
    /// </summary>
    /// <param name="ex">
    /// The exception raised by the request.
    /// </param>
    /// <returns>
    /// The text to show.
    /// </returns>
    private static string DescribeError(Exception ex)
    {
        if (ex is not DeepSeekApiException api)
        {
            return ex.Message;
        }

        try
        {
            using System.Text.Json.JsonDocument document = System.Text.Json.JsonDocument.Parse(api.ResponseBody);
            string? message = document.RootElement.GetProperty("error").GetProperty("message").GetString();
            return $"{(int)api.StatusCode}: {message}";
        }
        catch (Exception parse) when (parse is System.Text.Json.JsonException or KeyNotFoundException or InvalidOperationException)
        {
            return $"{(int)api.StatusCode}";
        }
    }

    /// <summary>
    /// Adds a message to its chat (persisting it) and, if that chat is still open, to the on-screen list.
    /// The chat may differ from the current one when the user switched chats while a reply was pending.
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
    /// Paths to the message photos.
    /// </param>
    /// <param name="isError">
    /// The message is an error notice.
    /// </param>
    private void AddMessage(
        ChatHistoryItem chat,
        string sender,
        string text,
        bool isAi,
        IReadOnlyList<string>? imagePaths = null,
        bool isError = false)
    {
        IReadOnlyList<string> images = imagePaths ?? Array.Empty<string>();

        _session.AddMessage(chat, sender, text, isAi, images, isError);

        if (ReferenceEquals(chat, _session.Current))
        {
            Messages.Add(new MessageViewModel(sender, text, isAi, _ui, images));
            ShowWelcome = false;
        }
    }

    private MessageViewModel CreateMessage(ChatMessageItem item) =>
        new(item.Sender, item.Message, item.IsAi, _ui, item.ImagePaths);

    /// <summary>
    /// Clears the input box and attachments.
    /// </summary>
    private void ClearComposer()
    {
        InputText = string.Empty;
        Attachments.Clear();
        HasAttachments = false;
    }

    /// <summary>
    /// Clears the chat window and brings back the welcome block.
    /// </summary>
    private void ClearConversation()
    {
        Messages.Clear();
        ClearComposer();
        ShowWelcome = true;
    }
}
