using System.Runtime.Serialization;
using DeepSeekChat.Localization;
using DeepSeekChat.Services;
using DeepSeekChat.Theming;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Base class of list items: gives templates access to theme colors and translated texts.
/// </summary>
[DataContract]
internal abstract class ThemedItem : NotifyPropertyChangedObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemedItem"/> class.
    /// </summary>
    protected ThemedItem(UiContext ui)
    {
        Theme = ui.Palette;
        T = ui.Text;
    }

    /// <summary>
    /// Gets theme.
    /// </summary>
    [DataMember]
    public ThemePalette Theme { get; }

    /// <summary>
    /// Gets t.
    /// </summary>
    [DataMember]
    public UiText T { get; }
}
