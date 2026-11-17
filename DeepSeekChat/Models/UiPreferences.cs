using DeepSeekChat.Theming;

namespace DeepSeekChat.Models;

/// <summary>
/// User interface preferences.
/// </summary>
internal sealed class UiPreferences
{
    /// <summary>
    /// Gets or sets language.
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets theme.
    /// </summary>
    public string Theme { get; set; } = ThemeCatalog.DefaultThemeId;

    /// <summary>
    /// Gets or sets user name.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets model.
    /// </summary>
    public string Model { get; set; } = ChatModels.Flash.Id;
}
