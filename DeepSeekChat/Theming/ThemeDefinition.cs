namespace DeepSeekChat.Theming;

/// <summary>
/// A theme's colors in "#RRGGBB" format.
/// </summary>
/// <param name="Id">
/// Theme id stored in the preferences.
/// </param>
/// <param name="LabelKey">
/// Translation key of the theme name.
/// </param>
/// <param name="Swatch">
/// Color of the sample circle in Settings.
/// </param>
internal sealed record ThemeDefinition(
    string Id,
    string LabelKey,
    string BgPrimary,
    string BgSecondary,
    string BgHover,
    string Border,
    string TextPrimary,
    string TextSecondary,
    string Accent,
    string AccentText,
    string Success,
    string Swatch);
