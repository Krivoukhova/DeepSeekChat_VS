using System.Runtime.Serialization;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// A photo thumbnail inside a message.
/// </summary>
/// <summary>
/// Gets or sets thumbnail view model.
/// </summary>
[DataContract]
internal sealed class ThumbnailViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThumbnailViewModel"/> class.
    /// </summary>
    public ThumbnailViewModel(string imagePath)
    {
        Brush = new XamlFragment(ImageBrushXaml.ForFile(imagePath));
    }

    /// <summary>
    /// Gets brush.
    /// </summary>
    [DataMember]
    public XamlFragment Brush { get; }
}
