using System.Runtime.Serialization;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.Theming;

/// <summary>
/// The current UI colors. 
/// XAML binds them to brushes, 
/// so a theme change applies immediately.
/// </summary>
[DataContract]
internal sealed class ThemePalette : NotifyPropertyChangedObject
{
    private string _bgPrimary = string.Empty;
    private string _bgSecondary = string.Empty;
    private string _bgHover = string.Empty;
    private string _border = string.Empty;
    private string _textPrimary = string.Empty;
    private string _textSecondary = string.Empty;
    private string _accent = string.Empty;
    private string _accentText = string.Empty;
    private string _success = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemePalette"/> class.
    /// </summary>
    public ThemePalette()
    {
        Apply(ThemeCatalog.Get(ThemeCatalog.DefaultThemeId));
    }

    /// <summary>
    /// Gets or sets bg primary.
    /// </summary>
    [DataMember]
    public string BgPrimary { get => _bgPrimary; private set => SetProperty(ref _bgPrimary, value); }

    /// <summary>
    /// Gets or sets bg secondary.
    /// </summary>
    [DataMember]
    public string BgSecondary { get => _bgSecondary; private set => SetProperty(ref _bgSecondary, value); }

    /// <summary>
    /// Gets or sets bg hover.
    /// </summary>
    [DataMember]
    public string BgHover { get => _bgHover; private set => SetProperty(ref _bgHover, value); }

    /// <summary>
    /// Gets or sets border.
    /// </summary>
    [DataMember]
    public string Border { get => _border; private set => SetProperty(ref _border, value); }

    /// <summary>
    /// Gets or sets text primary.
    /// </summary>
    [DataMember]
    public string TextPrimary { get => _textPrimary; private set => SetProperty(ref _textPrimary, value); }

    /// <summary>
    /// Gets or sets text secondary.
    /// </summary>
    [DataMember]
    public string TextSecondary { get => _textSecondary; private set => SetProperty(ref _textSecondary, value); }

    /// <summary>
    /// Gets or sets accent.
    /// </summary>
    [DataMember]
    public string Accent { get => _accent; private set => SetProperty(ref _accent, value); }

    /// <summary>
    /// Gets or sets accent text.
    /// </summary>
    [DataMember]
    public string AccentText { get => _accentText; private set => SetProperty(ref _accentText, value); }

    /// <summary>
    /// Gets or sets success.
    /// </summary>
    [DataMember]
    public string Success { get => _success; private set => SetProperty(ref _success, value); }

    /// <summary>
    /// Applies a theme's colors to every UI element.
    /// </summary>
    /// <param name="theme">
    /// The theme to apply.
    /// </param>
    public void Apply(ThemeDefinition theme)
    {
        BgPrimary = theme.BgPrimary;
        BgSecondary = theme.BgSecondary;
        BgHover = theme.BgHover;
        Border = theme.Border;
        TextPrimary = theme.TextPrimary;
        TextSecondary = theme.TextSecondary;
        Accent = theme.Accent;
        AccentText = theme.AccentText;
        Success = theme.Success;
    }
}
