using DeepSeekChat.Localization;
using DeepSeekChat.Models;
using DeepSeekChat.Theming;

namespace DeepSeekChat.Services;

/// <summary>
/// Shared UI state: language, theme, user name, the selected model. 
/// Changes are saved immediately.
/// </summary>
internal sealed class UiContext
{
    private readonly UiPreferencesStore _store;
    private readonly UiPreferences _preferences;

    /// <summary>
    /// Initializes a new instance of the <see cref="UiContext"/> class.
    /// </summary>
    public UiContext(UiPreferencesStore store)
    {
        _store = store;
        _preferences = store.Load();

        Palette = new ThemePalette();
        Palette.Apply(ThemeCatalog.Get(_preferences.Theme));

        Text = new UiText();
        Text.Apply(_preferences.Language);
    }

    /// <summary>
    /// Gets palette.
    /// </summary>
    public ThemePalette Palette { get; }

    /// <summary>
    /// Gets text.
    /// </summary>
    public UiText Text { get; }

    public string Language => _preferences.Language;

    public string ThemeId => _preferences.Theme;

    public string UserName => _preferences.UserName;

    public ChatModel Model => ChatModels.Get(_preferences.Model);


    /// <summary>
    /// Returns the translation of a string in the current language.
    /// </summary>
    /// <param name="key">
    /// String key in the translation table.
    /// </param>
    /// <returns>
    /// The translated text.
    /// </returns>
    public string Translate(string key) => Translator.Translate(Language, key);

    /// <summary>
    /// Switches the UI language. An unsupported language is ignored.
    /// </summary>
    /// <param name="code">
    /// Language code.
    /// </param>
    public void SetLanguage(string code)
    {
        if (!Translator.IsSupported(code))
        {
            return;
        }

        _preferences.Language = code;
        Text.Apply(code);
        _store.Save(_preferences);
    }

    /// <summary>
    /// Applies a theme. An unknown id falls back to the default theme.
    /// </summary>
    /// <param name="themeId">
    /// Theme id.
    /// </param>
    public void SetTheme(string themeId)
    {
        ThemeDefinition theme = ThemeCatalog.Get(themeId);

        _preferences.Theme = theme.Id;
        Palette.Apply(theme);
        _store.Save(_preferences);
    }

    /// <summary>
    /// Saves the name the model uses to address the user.
    /// </summary>
    /// <param name="userName">
    /// The user name; surrounding whitespace is trimmed.
    /// </param>
    public void SetUserName(string userName)
    {
        _preferences.UserName = userName.Trim();
        _store.Save(_preferences);
    }

    /// <summary>
    /// Selects the model used for new requests.
    /// </summary>
    /// <param name="modelId">
    /// Model id; an unknown id falls back to Flash.
    /// </param>
    public void SetModel(string modelId)
    {
        _preferences.Model = ChatModels.Get(modelId).Id;
        _store.Save(_preferences);
    }
}
