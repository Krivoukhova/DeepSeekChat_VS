using System.Diagnostics;
using System.Globalization;
using DeepSeekChat.Localization;
using DeepSeekChat.Models;
using DeepSeekChat.Theming;

namespace DeepSeekChat.Services;

/// <summary>
/// Storage for the language, theme and user name (ui.json).
/// </summary>
internal sealed class UiPreferencesStore
{
    /// <summary>
    /// Loads the preferences and substitutes valid values for missing or outdated ones.
    /// </summary>
    /// <returns>
    /// Valid preferences; the default language is taken from the system language.
    /// </returns>
    public UiPreferences Load()
    {
        UiPreferences preferences = JsonFile.TryRead<UiPreferences>(AppPaths.PreferencesFile) ?? new UiPreferences();

        if (!Translator.IsSupported(preferences.Language))
        {
            preferences.Language = Translator.ResolveLanguage(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
        }

        preferences.Theme = ThemeCatalog.Get(preferences.Theme).Id;
        preferences.UserName ??= string.Empty;
        preferences.Model = ChatModels.Get(preferences.Model).Id;

        return preferences;
    }

    /// <summary>
    /// Saves the preferences. A write failure is not critical and is only logged.
    /// </summary>
    /// <param name="preferences">
    /// The preferences to save.
    /// </param>
    public void Save(UiPreferences preferences)
    {
        try
        {
            JsonFile.Write(AppPaths.PreferencesFile, preferences);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Trace.TraceWarning($"Failed to save UI preferences: {ex.Message}");
        }
    }
}
