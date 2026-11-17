namespace DeepSeekChat.Localization;

/// <summary>
/// Looks up translations in <see cref="TranslationTable"/>.
/// </summary>
internal static class Translator
{
    /// <summary>
    /// Gets the default language.
    /// </summary>
    public const string DefaultLanguage = "en";

    /// <summary>
    /// Gets the languages.
    /// </summary>
    public static IReadOnlyList<Language> Languages => TranslationTable.Languages;

    /// <summary>
    /// Checks whether a language is supported.
    /// </summary>
    /// <param name="code">
    /// Language code.
    /// </param>
    /// <returns>
    /// true if translations for this language exist; otherwise false.
    /// </returns>
    public static bool IsSupported(string? code) =>
        TranslationTable.Languages.Any(language => language.Code == code);

    /// <summary>
    /// Picks a supported language.
    /// </summary>
    /// <param name="code">
    /// Requested language code.
    /// </param>
    /// <returns>
    /// The given code if it is supported; otherwise the default language.
    /// </returns>
    public static string ResolveLanguage(string? code) =>
        IsSupported(code) ? code! : DefaultLanguage;

    /// <summary>
    /// Returns the translation of a string.
    /// </summary>
    /// <param name="language">
    /// Language code.
    /// </param>
    /// <param name="key">
    /// String key.
    /// </param>
    /// <returns>
    /// The translation; the key itself if the string is not found.
    /// </returns>
    public static string Translate(string language, string key)
    {
        if (!TranslationTable.Rows.TryGetValue(key, out string[]? row))
        {
            return key;
        }

        int index = IndexOf(language);
        return row[index];
    }

    /// <summary>
    /// Finds the position of a language in the translation table.
    /// </summary>
    /// <param name="language">
    /// Language code.
    /// </param>
    /// <returns>
    /// The column index; 0 (the default language) if the language is not found.
    /// </returns>
    private static int IndexOf(string language)
    {
        for (int i = 0; i < TranslationTable.Languages.Count; i++)
        {
            if (TranslationTable.Languages[i].Code == language)
            {
                return i;
            }
        }

        return 0;
    }
}
