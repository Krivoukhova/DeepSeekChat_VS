namespace DeepSeekChat.Theming;

/// <summary>
/// The list of available themes.
/// </summary>
internal static class ThemeCatalog
{
    /// <summary>
    /// Gets the default theme id.
    /// </summary>
    public const string DefaultThemeId = "violet";

    /// <summary>
    /// Gets all.
    /// </summary>
    public static IReadOnlyList<ThemeDefinition> All { get; } = new[]
    {
        new ThemeDefinition("violet",   "ThemeViolet",   "#0D0420", "#1A0F3D", "#3A2270", "#2A1A5C", "#9B87E0", "#8772C6", "#3C218D", "#FFFFFF", "#4CAF50", "#3C218D"),
        new ThemeDefinition("light",    "ThemeLight",    "#FFFFFF", "#F3F4F6", "#E5E7EB", "#D1D5DB", "#111827", "#6B7280", "#4F46E5", "#FFFFFF", "#16A34A", "#FFFFFF"),
        new ThemeDefinition("graphite", "ThemeGraphite", "#1E1F22", "#2B2D30", "#3C3F41", "#393B40", "#DFE1E5", "#9DA0A8", "#3E7BC4", "#FFFFFF", "#5FAD65", "#3E7BC4"),
        new ThemeDefinition("ocean",    "ThemeOcean",    "#0B1D2A", "#12293A", "#1D4057", "#1E3A4F", "#CFE8F5", "#7FA6BD", "#1F7A9E", "#FFFFFF", "#4CC38A", "#1F7A9E"),
        new ThemeDefinition("plum",     "ThemePlum",     "#1F1226", "#2D1A38", "#4A2A5C", "#3C2349", "#EBD5F2", "#B58FC4", "#8E3FA8", "#FFFFFF", "#6CC08B", "#8E3FA8"),
        new ThemeDefinition("forest",   "ThemeForest",   "#101A14", "#18271E", "#264033", "#25382C", "#D5EBDD", "#86AB95", "#2F7D52", "#FFFFFF", "#6CCF8E", "#2F7D52"),
    };

    /// <summary>
    /// Finds a theme by id.
    /// </summary>
    /// <param name="id">
    /// Theme id.
    /// </param>
    /// <returns>
    /// The matching theme, or the default theme if the id is unknown.
    /// </returns>
    public static ThemeDefinition Get(string? id) =>
        All.FirstOrDefault(theme => theme.Id == id) ?? All[0];
}
