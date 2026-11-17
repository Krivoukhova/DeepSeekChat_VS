using System.Runtime.Serialization;
using DeepSeekChat.Localization;
using DeepSeekChat.Services;
using DeepSeekChat.Theming;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Root data context of the DeepSeek window: 
/// theme, texts, navigation and pages.
/// </summary>
[DataContract]
internal sealed class ChatViewModel : NotifyPropertyChangedObject
{
    private readonly PageNavigator _navigator;
    private readonly HistoryViewModel _history;
    private readonly SettingsViewModel _settings;
    private readonly AccountViewModel _account;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatViewModel"/> class.
    /// </summary>
    public ChatViewModel(
        UiContext ui,
        PageNavigator navigator,
        ConversationViewModel conversation,
        ModelPickerViewModel modelPicker,
        HistoryViewModel history,
        SettingsViewModel settings,
        AccountViewModel account)
    {
        _navigator = navigator;
        _history = history;
        _settings = settings;
        _account = account;

        Theme = ui.Palette;
        T = ui.Text;
        AvatarBrush = new XamlFragment(BotAvatar.GetAvatarBrushXaml());
        Navigation = navigator;
        Conversation = conversation;
        ModelPicker = modelPicker;
        History = history;
        Settings = settings;
        Account = account;

        ShowChatCommand = new AsyncCommand((parameter, token) => ShowAsync(AppPage.Chat));
        ShowHistoryCommand = new AsyncCommand((parameter, token) => ShowHistoryAsync());
        ShowSettingsCommand = new AsyncCommand((parameter, token) => ShowSettingsAsync());
        ShowAccountCommand = new AsyncCommand(ShowAccountAsync);
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

    /// <summary>
    /// Gets avatar brush.
    /// </summary>
    [DataMember]
    public XamlFragment AvatarBrush { get; }

    /// <summary>
    /// Gets navigation.
    /// </summary>
    [DataMember]
    public PageNavigator Navigation { get; }

    /// <summary>
    /// Gets conversation.
    /// </summary>
    [DataMember]
    public ConversationViewModel Conversation { get; }

    /// <summary>
    /// Gets model picker.
    /// </summary>
    [DataMember]
    public ModelPickerViewModel ModelPicker { get; }

    /// <summary>
    /// Gets history.
    /// </summary>
    [DataMember]
    public HistoryViewModel History { get; }

    /// <summary>
    /// Gets settings.
    /// </summary>
    [DataMember]
    public SettingsViewModel Settings { get; }

    /// <summary>
    /// Gets account.
    /// </summary>
    [DataMember]
    public AccountViewModel Account { get; }

    /// <summary>
    /// Gets show chat command.
    /// </summary>
    [DataMember]
    public AsyncCommand ShowChatCommand { get; }

    /// <summary>
    /// Gets show history command.
    /// </summary>
    [DataMember]
    public AsyncCommand ShowHistoryCommand { get; }

    /// <summary>
    /// Gets show settings command.
    /// </summary>
    [DataMember]
    public AsyncCommand ShowSettingsCommand { get; }

    /// <summary>
    /// Gets show account command.
    /// </summary>
    [DataMember]
    public AsyncCommand ShowAccountCommand { get; }

    /// <summary>
    /// Navigates to a page.
    /// </summary>
    private Task ShowAsync(AppPage page)
    {
        _navigator.Show(page);
        return Task.CompletedTask;
    }

    private Task ShowHistoryAsync()
    {
        _history.Refresh();
        return ShowAsync(AppPage.History);
    }

    private Task ShowSettingsAsync()
    {
        _settings.Prepare();
        return ShowAsync(AppPage.Settings);
    }

    /// <summary>
    /// Opens the account page and loads current information.
    /// </summary>
    /// <param name="parameter">
    /// Command parameter (unused).
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task ShowAccountAsync(object? parameter, CancellationToken cancellationToken)
    {
        await ShowAsync(AppPage.Account);
        await _account.LoadAsync(cancellationToken);
    }
}
