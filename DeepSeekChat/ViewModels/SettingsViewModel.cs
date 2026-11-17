using System.Runtime.Serialization;
using DeepSeekChat.Constants;
using DeepSeekChat.Localization;
using DeepSeekChat.Models;
using DeepSeekChat.Services;
using DeepSeekChat.Theming;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Settings page: API key, language and theme.
/// </summary>
[DataContract]
internal sealed class SettingsViewModel : NotifyPropertyChangedObject
{
    private readonly ApiKeyStore _keyStore;
    private readonly DeepSeekApi _api;
    private readonly UiContext _ui;
    private readonly PageNavigator _navigator;

    private string _apiKeyText = string.Empty;
    private string _status = string.Empty;
    private string _statusColor = string.Empty;
    private bool _isKeyMasked;
    private bool _isKeyEditable = true;
    private string _maskedKey = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    public SettingsViewModel(ApiKeyStore keyStore, DeepSeekApi api, UiContext ui, PageNavigator navigator)
    {
        _keyStore = keyStore;
        _api = api;
        _ui = ui;
        _navigator = navigator;
        _statusColor = ui.Palette.Success;

        foreach (Language language in Translator.Languages)
        {
            Languages.Add(CreateOption(language.Code, language.NativeName, string.Empty, SelectLanguageAsync));
        }

        foreach (ThemeDefinition theme in ThemeCatalog.All)
        {
            Themes.Add(CreateOption(theme.Id, _ui.Translate(theme.LabelKey), theme.Swatch, SelectThemeAsync));
        }

        OptionViewModel.MarkSelected(Languages, _ui.Language);
        OptionViewModel.MarkSelected(Themes, _ui.ThemeId);

        SaveCommand = new AsyncCommand((parameter, token) => SaveAsync(parameter as string, token));
        EditKeyCommand = new AsyncCommand((parameter, token) => EditKeyAsync());
    }

    /// <summary>
    /// Gets languages.
    /// </summary>
    [DataMember]
    public ObservableList<OptionViewModel> Languages { get; } = new();

    /// <summary>
    /// Gets themes.
    /// </summary>
    [DataMember]
    public ObservableList<OptionViewModel> Themes { get; } = new();

    /// <summary>
    /// Gets or sets api key text.
    /// </summary>
    [DataMember]
    public string ApiKeyText
    {
        get => _apiKeyText;
        set => SetProperty(ref _apiKeyText, value ?? string.Empty);
    }

    /// <summary>
    /// true while a saved key is shown as asterisks instead of the input box.
    /// </summary>
    /// <summary>
    /// Gets or sets is key masked.
    /// </summary>
    [DataMember]
    public bool IsKeyMasked
    {
        get => _isKeyMasked;
        private set
        {
            SetProperty(ref _isKeyMasked, value);
            IsKeyEditable = !value;
        }
    }

    /// <summary>
    /// true while the key input box is shown.
    /// </summary>
    [DataMember]
    public bool IsKeyEditable { get => _isKeyEditable; private set => SetProperty(ref _isKeyEditable, value); }

    /// <summary>
    /// The saved key as asterisks.
    /// </summary>
    [DataMember]
    public string MaskedKey { get => _maskedKey; private set => SetProperty(ref _maskedKey, value); }

    /// <summary>
    /// Gets or sets status.
    /// </summary>
    [DataMember]
    public string Status { get => _status; private set => SetProperty(ref _status, value); }

    /// <summary>
    /// Color of the save result message: green for success, red for an error.
    /// </summary>
    [DataMember]
    public string StatusColor { get => _statusColor; private set => SetProperty(ref _statusColor, value); }

    /// <summary>
    /// Gets save command.
    /// </summary>
    [DataMember]
    public AsyncCommand SaveCommand { get; }

    /// <summary>
    /// Gets copy key command.
    /// </summary>
    /// <summary>
    /// Gets edit key command.
    /// </summary>
    [DataMember]
    public AsyncCommand EditKeyCommand { get; }

    /// <summary>
    /// Prepares the page for display: shows the saved key as asterisks and clears the previous status.
    /// </summary>
    public void Prepare()
    {
        ApiKeyText = string.Empty;
        Status = string.Empty;
        ShowMaskedKey(_keyStore.Load());
    }

    /// <summary>
    /// Switches to the masked view if a key is saved, otherwise to the input box.
    /// </summary>
    /// <param name="key">
    /// The saved key or an empty string.
    /// </param>
    private void ShowMaskedKey(string key)
    {
        MaskedKey = new string('*', Math.Min(key.Length, ChatConstants.MaskedKeyMaxLength));
        IsKeyMasked = key.Length > 0;
    }

    /// <summary>
    /// Switches to the empty input box so a new key can be entered.
    /// </summary>
    /// <returns>
    /// A completed task.
    /// </returns>
    private Task EditKeyAsync()
    {
        ApiKeyText = string.Empty;
        Status = string.Empty;
        IsKeyMasked = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Creates a choice button for a Settings list.
    /// </summary>
    /// <param name="id">
    /// Option id.
    /// </param>
    /// <param name="label">
    /// Label.
    /// </param>
    /// <param name="swatch">
    /// Sample circle color or an empty string.
    /// </param>
    /// <param name="select">
    /// Action invoked on selection.
    /// </param>
    /// <returns>
    /// The new choice button.
    /// </returns>
    private OptionViewModel CreateOption(string id, string label, string swatch, Func<string, Task> select) =>
        new(id, label, swatch, _ui, select);

    /// <summary>
    /// Switches the language and refreshes theme labels.
    /// </summary>
    /// <param name="code">
    /// Language code.
    /// </param>
    /// <returns>
    /// A completed task.
    /// </returns>
    private Task SelectLanguageAsync(string code)
    {
        _ui.SetLanguage(code);
        OptionViewModel.MarkSelected(Languages, _ui.Language);

        for (int i = 0; i < ThemeCatalog.All.Count; i++)
        {
            Themes[i].Label = _ui.Translate(ThemeCatalog.All[i].LabelKey);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Applies a theme.
    /// </summary>
    /// <param name="themeId">
    /// Theme id.
    /// </param>
    /// <returns>
    /// A completed task.
    /// </returns>
    private Task SelectThemeAsync(string themeId)
    {
        _ui.SetTheme(themeId);
        OptionViewModel.MarkSelected(Themes, _ui.ThemeId);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Validates the key on the server and saves it; on success returns the window to the chat.
    /// </summary>
    /// <param name="apiKey">
    /// The entered key.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task SaveAsync(string? apiKey, CancellationToken cancellationToken)
    {
        string key = apiKey?.Trim() ?? string.Empty;
        Status = string.Empty;

        try
        {
            KeyValidation validation = await _api.ValidateKeyAsync(key, cancellationToken);

            if (validation.Status != KeyValidationStatus.Accepted)
            {
                ShowFailure(DescribeRejection(validation));
                return;
            }

            _keyStore.Save(key);
            ApiKeyText = string.Empty;
            ShowMaskedKey(key);
            StatusColor = _ui.Palette.Success;
            Status = _ui.Translate("ApiKeySaved");

            await Task.Delay(ChatConstants.SaveFeedbackMs, cancellationToken);
            _navigator.Show(AppPage.Chat);
        }
        catch (Exception ex) when (ex is HttpRequestException or IOException or UnauthorizedAccessException)
        {
            ShowFailure(ex.Message);
        }
    }

    /// <summary>
    /// Builds the error text for a rejected key.
    /// </summary>
    /// <param name="validation">
    /// The check result.
    /// </param>
    /// <returns>
    /// The translated error text.
    /// </returns>
    private string DescribeRejection(KeyValidation validation) =>
        validation.Status == KeyValidationStatus.Empty
            ? _ui.Translate("ApiKeyEmpty")
            : string.Format(_ui.Translate("ApiKeyInvalid"), validation.Details);

    /// <summary>
    /// Shows an error in red.
    /// </summary>
    /// <param name="message">
    /// Error text.
    /// </param>
    private void ShowFailure(string message)
    {
        StatusColor = ChatConstants.ErrorColor;
        Status = message;
    }
}
