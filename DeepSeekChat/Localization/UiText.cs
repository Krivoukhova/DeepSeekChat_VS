using System.Runtime.Serialization;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.Localization;

/// <summary>
/// Translated UI texts bound from XAML as {Binding T.Name}. Updated when the language changes.
/// </summary>
[DataContract]
internal sealed class UiText : NotifyPropertyChangedObject
{
    private string _back = string.Empty;
    private string _newChat = string.Empty;
    private string _history = string.Empty;
    private string _deleteChat = string.Empty;
    private string _settings = string.Empty;
    private string _account = string.Empty;
    private string _copy = string.Empty;
    private string _attach = string.Empty;
    private string _addPhoto = string.Empty;
    private string _addFile = string.Empty;
    private string _welcomeTitle = string.Empty;
    private string _welcomeSub = string.Empty;
    private string _welcomeDesc = string.Empty;
    private string _quickExplain = string.Empty;
    private string _quickFix = string.Empty;
    private string _quickTests = string.Empty;
    private string _quickOptimize = string.Empty;
    private string _typing = string.Empty;
    private string _placeholder = string.Empty;
    private string _historyTitle = string.Empty;
    private string _historyEmpty = string.Empty;
    private string _settingsTitle = string.Empty;
    private string _apiKeyLabel = string.Empty;
    private string _settingsInfo = string.Empty;
    private string _save = string.Empty;
    private string _languageLabel = string.Empty;
    private string _themeLabel = string.Empty;
    private string _accountTitle = string.Empty;
    private string _apiKeyField = string.Empty;
    private string _balanceField = string.Empty;
    private string _statusField = string.Empty;
    private string _nameLabel = string.Empty;
    private string _changeKey = string.Empty;
    private string _nameHint = string.Empty;
    private string _modelLabel = string.Empty;

    /// <summary>
    /// Gets or sets back.
    /// </summary>
    [DataMember]
    public string Back { get => _back; private set => SetProperty(ref _back, value); }

    /// <summary>
    /// Gets or sets new chat.
    /// </summary>
    [DataMember]
    public string NewChat { get => _newChat; private set => SetProperty(ref _newChat, value); }

    /// <summary>
    /// Gets or sets history.
    /// </summary>
    [DataMember]
    public string History { get => _history; private set => SetProperty(ref _history, value); }

    /// <summary>
    /// Gets or sets delete chat.
    /// </summary>
    [DataMember]
    public string DeleteChat { get => _deleteChat; private set => SetProperty(ref _deleteChat, value); }

    /// <summary>
    /// Gets or sets settings.
    /// </summary>
    [DataMember]
    public string Settings { get => _settings; private set => SetProperty(ref _settings, value); }

    /// <summary>
    /// Gets or sets account.
    /// </summary>
    [DataMember]
    public string Account { get => _account; private set => SetProperty(ref _account, value); }

    /// <summary>
    /// Gets or sets change key.
    /// </summary>
    [DataMember]
    public string ChangeKey { get => _changeKey; private set => SetProperty(ref _changeKey, value); }

    /// <summary>
    /// Gets or sets copy.
    /// </summary>
    [DataMember]
    public string Copy { get => _copy; private set => SetProperty(ref _copy, value); }

    /// <summary>
    /// Gets or sets attach.
    /// </summary>
    [DataMember]
    public string Attach { get => _attach; private set => SetProperty(ref _attach, value); }

    /// <summary>
    /// Gets or sets add photo.
    /// </summary>
    [DataMember]
    public string AddPhoto { get => _addPhoto; private set => SetProperty(ref _addPhoto, value); }

    /// <summary>
    /// Gets or sets add file.
    /// </summary>
    [DataMember]
    public string AddFile { get => _addFile; private set => SetProperty(ref _addFile, value); }

    /// <summary>
    /// Gets or sets welcome title.
    /// </summary>
    [DataMember]
    public string WelcomeTitle { get => _welcomeTitle; private set => SetProperty(ref _welcomeTitle, value); }

    /// <summary>
    /// Gets or sets welcome sub.
    /// </summary>
    [DataMember]
    public string WelcomeSub { get => _welcomeSub; private set => SetProperty(ref _welcomeSub, value); }

    /// <summary>
    /// Gets or sets welcome desc.
    /// </summary>
    [DataMember]
    public string WelcomeDesc { get => _welcomeDesc; private set => SetProperty(ref _welcomeDesc, value); }

    /// <summary>
    /// Gets or sets quick explain.
    /// </summary>
    [DataMember]
    public string QuickExplain { get => _quickExplain; private set => SetProperty(ref _quickExplain, value); }

    /// <summary>
    /// Gets or sets quick fix.
    /// </summary>
    [DataMember]
    public string QuickFix { get => _quickFix; private set => SetProperty(ref _quickFix, value); }

    /// <summary>
    /// Gets or sets quick tests.
    /// </summary>
    [DataMember]
    public string QuickTests { get => _quickTests; private set => SetProperty(ref _quickTests, value); }

    /// <summary>
    /// Gets or sets quick optimize.
    /// </summary>
    [DataMember]
    public string QuickOptimize { get => _quickOptimize; private set => SetProperty(ref _quickOptimize, value); }

    /// <summary>
    /// Gets or sets typing.
    /// </summary>
    [DataMember]
    public string Typing { get => _typing; private set => SetProperty(ref _typing, value); }

    /// <summary>
    /// Gets or sets placeholder.
    /// </summary>
    [DataMember]
    public string Placeholder { get => _placeholder; private set => SetProperty(ref _placeholder, value); }

    /// <summary>
    /// Gets or sets history title.
    /// </summary>
    [DataMember]
    public string HistoryTitle { get => _historyTitle; private set => SetProperty(ref _historyTitle, value); }

    /// <summary>
    /// Gets or sets history empty.
    /// </summary>
    [DataMember]
    public string HistoryEmpty { get => _historyEmpty; private set => SetProperty(ref _historyEmpty, value); }

    /// <summary>
    /// Gets or sets settings title.
    /// </summary>
    [DataMember]
    public string SettingsTitle { get => _settingsTitle; private set => SetProperty(ref _settingsTitle, value); }

    /// <summary>
    /// Gets or sets api key label.
    /// </summary>
    [DataMember]
    public string ApiKeyLabel { get => _apiKeyLabel; private set => SetProperty(ref _apiKeyLabel, value); }

    /// <summary>
    /// Gets or sets settings info.
    /// </summary>
    [DataMember]
    public string SettingsInfo { get => _settingsInfo; private set => SetProperty(ref _settingsInfo, value); }

    /// <summary>
    /// Gets or sets save.
    /// </summary>
    [DataMember]
    public string Save { get => _save; private set => SetProperty(ref _save, value); }

    /// <summary>
    /// Gets or sets language label.
    /// </summary>
    [DataMember]
    public string LanguageLabel { get => _languageLabel; private set => SetProperty(ref _languageLabel, value); }

    /// <summary>
    /// Gets or sets theme label.
    /// </summary>
    [DataMember]
    public string ThemeLabel { get => _themeLabel; private set => SetProperty(ref _themeLabel, value); }

    /// <summary>
    /// Gets or sets account title.
    /// </summary>
    [DataMember]
    public string AccountTitle { get => _accountTitle; private set => SetProperty(ref _accountTitle, value); }

    /// <summary>
    /// Gets or sets api key field.
    /// </summary>
    [DataMember]
    public string ApiKeyField { get => _apiKeyField; private set => SetProperty(ref _apiKeyField, value); }

    /// <summary>
    /// Gets or sets balance field.
    /// </summary>
    [DataMember]
    public string BalanceField { get => _balanceField; private set => SetProperty(ref _balanceField, value); }

    /// <summary>
    /// Gets or sets status field.
    /// </summary>
    [DataMember]
    public string StatusField { get => _statusField; private set => SetProperty(ref _statusField, value); }

    /// <summary>
    /// Gets or sets name label.
    /// </summary>
    [DataMember]
    public string NameLabel { get => _nameLabel; private set => SetProperty(ref _nameLabel, value); }

    /// <summary>
    /// Gets or sets name hint.
    /// </summary>
    [DataMember]
    public string NameHint { get => _nameHint; private set => SetProperty(ref _nameHint, value); }

    /// <summary>
    /// Gets or sets model label.
    /// </summary>
    [DataMember]

    public string ModelLabel { get => _modelLabel; private set => SetProperty(ref _modelLabel, value); }

    /// <summary>
    /// Loads translations for a language into every property.
    /// </summary>
    /// <param name="language">
    /// Language code.
    /// </param>
    public void Apply(string language)
    {
        Back = Translator.Translate(language, nameof(Back));
        NewChat = Translator.Translate(language, nameof(NewChat));
        History = Translator.Translate(language, nameof(History));
        DeleteChat = Translator.Translate(language, nameof(DeleteChat));
        Settings = Translator.Translate(language, nameof(Settings));
        Account = Translator.Translate(language, nameof(Account));
        Copy = Translator.Translate(language, nameof(Copy));
        ChangeKey = Translator.Translate(language, nameof(ChangeKey));
        Attach = Translator.Translate(language, nameof(Attach));
        AddPhoto = Translator.Translate(language, nameof(AddPhoto));
        AddFile = Translator.Translate(language, nameof(AddFile));
        WelcomeTitle = Translator.Translate(language, nameof(WelcomeTitle));
        WelcomeSub = Translator.Translate(language, nameof(WelcomeSub));
        WelcomeDesc = Translator.Translate(language, nameof(WelcomeDesc));
        QuickExplain = Translator.Translate(language, nameof(QuickExplain));
        QuickFix = Translator.Translate(language, nameof(QuickFix));
        QuickTests = Translator.Translate(language, nameof(QuickTests));
        QuickOptimize = Translator.Translate(language, nameof(QuickOptimize));
        Typing = Translator.Translate(language, nameof(Typing));
        Placeholder = Translator.Translate(language, nameof(Placeholder));
        HistoryTitle = Translator.Translate(language, nameof(HistoryTitle));
        HistoryEmpty = Translator.Translate(language, nameof(HistoryEmpty));
        SettingsTitle = Translator.Translate(language, nameof(SettingsTitle));
        ApiKeyLabel = Translator.Translate(language, nameof(ApiKeyLabel));
        SettingsInfo = Translator.Translate(language, nameof(SettingsInfo));
        Save = Translator.Translate(language, nameof(Save));
        LanguageLabel = Translator.Translate(language, nameof(LanguageLabel));
        ThemeLabel = Translator.Translate(language, nameof(ThemeLabel));
        AccountTitle = Translator.Translate(language, nameof(AccountTitle));
        ApiKeyField = Translator.Translate(language, nameof(ApiKeyField));
        BalanceField = Translator.Translate(language, nameof(BalanceField));
        StatusField = Translator.Translate(language, nameof(StatusField));
        NameLabel = Translator.Translate(language, nameof(NameLabel));
        NameHint = Translator.Translate(language, nameof(NameHint));
        ModelLabel = Translator.Translate(language, nameof(ModelLabel));
    }
}
