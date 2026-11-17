using System.Runtime.Serialization;
using DeepSeekChat.Constants;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// A chat message: sender, text, photo thumbnails and a copy button.
/// </summary>
[DataContract]
internal sealed class MessageViewModel : ThemedItem
{
    private string _copyLabel = ChatConstants.CopyIcon;

    /// <summary>
    /// Creates a message model.
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
    /// <param name="ui">
    /// Shared appearance state.
    /// </param>
    /// <param name="imagePaths">
    /// Paths to photos for thumbnails; missing files are skipped.
    /// </param>
    public MessageViewModel(string sender, string text, bool isAi, UiContext ui, IEnumerable<string> imagePaths)
        : base(ui)
    {
        Header = (isAi ? ChatConstants.BotEmoji : ChatConstants.UserEmoji) + " " + sender;
        Text = text;
        IsAi = isAi;

        List<ThumbnailViewModel> thumbnails = imagePaths
            .Where(File.Exists)
            .Select(path => new ThumbnailViewModel(path))
            .ToList();
        Thumbnails.AddRange(thumbnails);
        HasThumbnails = thumbnails.Count > 0;

        CopyCommand = new AsyncCommand(CopyTextAsync);
    }

    /// <summary>
    /// Gets header.
    /// </summary>
    [DataMember]
    public string Header { get; }

    /// <summary>
    /// Gets text.
    /// </summary>
    [DataMember]
    public string Text { get; }

    /// <summary>
    /// Gets is ai.
    /// </summary>
    [DataMember]
    public bool IsAi { get; }

    /// <summary>
    /// Gets has thumbnails.
    /// </summary>
    [DataMember]
    public bool HasThumbnails { get; }

    /// <summary>
    /// Gets thumbnails.
    /// </summary>
    [DataMember]
    public ObservableList<ThumbnailViewModel> Thumbnails { get; } = new();

    /// <summary>
    /// Copy button icon; briefly switches to a checkmark.
    /// </summary>
    /// <summary>
    /// Gets or sets copy label.
    /// </summary>
    [DataMember]
    public string CopyLabel
    {
        get => _copyLabel;
        private set => SetProperty(ref _copyLabel, value);
    }

    /// <summary>
    /// Gets copy command.
    /// </summary>
    [DataMember]
    public AsyncCommand CopyCommand { get; }

    /// <summary>
    /// Copies the message text and briefly shows a checkmark.
    /// </summary>
    /// <param name="parameter">
    /// Command parameter (unused).
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task CopyTextAsync(object? parameter, CancellationToken cancellationToken)
    {
        if (!ClipboardHelper.TrySetText(Text))
        {
            return;
        }

        CopyLabel = ChatConstants.CopiedIcon;
        await Task.Delay(ChatConstants.CopyFeedbackMs, cancellationToken);
        CopyLabel = ChatConstants.CopyIcon;
    }
}
