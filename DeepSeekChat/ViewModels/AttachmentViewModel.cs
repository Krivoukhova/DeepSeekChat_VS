using System.Runtime.Serialization;
using DeepSeekChat.Models;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// A chip for an attached file or photo above the input box.
/// </summary>
[DataContract]
internal sealed class AttachmentViewModel : ThemedItem
{
    /// <summary>
    /// Creates an attachment chip.
    /// </summary>
    /// <param name="attachment">
    /// The attached file.
    /// </param>
    /// <param name="ui">
    /// Shared appearance state.
    /// </param>
    /// <param name="remove">
    /// Action invoked when the cross is clicked.
    /// </param>
    public AttachmentViewModel(Attachment attachment, UiContext ui, Action<AttachmentViewModel> remove)
        : base(ui)
    {
        Source = attachment;
        Name = attachment.Name;
        Icon = attachment.Icon;
        RemoveCommand = new AsyncCommand((parameter, token) =>
        {
            remove(this);
            return Task.CompletedTask;
        });
    }

    /// <summary>
    /// Gets source.
    /// </summary>
    public Attachment Source { get; }

    /// <summary>
    /// Gets name.
    /// </summary>
    [DataMember]
    public string Name { get; }

    /// <summary>
    /// Gets icon.
    /// </summary>
    [DataMember]
    public string Icon { get; }

    /// <summary>
    /// Gets remove command.
    /// </summary>
    [DataMember]
    public AsyncCommand RemoveCommand { get; }
}
