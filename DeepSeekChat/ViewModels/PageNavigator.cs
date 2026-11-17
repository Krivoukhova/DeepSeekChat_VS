using System.Runtime.Serialization;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Tracks which window page is currently shown.
/// </summary>
[DataContract]
internal sealed class PageNavigator : NotifyPropertyChangedObject
{
    private bool _isChatView = true;
    private bool _isHistoryView;
    private bool _isSettingsView;
    private bool _isAccountView;
    private bool _showBack;

    /// <summary>
    /// Gets or sets is chat view.
    /// </summary>
    [DataMember]
    public bool IsChatView { get => _isChatView; private set => SetProperty(ref _isChatView, value); }

    /// <summary>
    /// Gets or sets is history view.
    /// </summary>
    [DataMember]
    public bool IsHistoryView { get => _isHistoryView; private set => SetProperty(ref _isHistoryView, value); }

    /// <summary>
    /// Gets or sets is settings view.
    /// </summary>
    [DataMember]
    public bool IsSettingsView { get => _isSettingsView; private set => SetProperty(ref _isSettingsView, value); }

    /// <summary>
    /// Gets or sets is account view.
    /// </summary>
    [DataMember]
    public bool IsAccountView { get => _isAccountView; private set => SetProperty(ref _isAccountView, value); }

    /// <summary>
    /// Show the back button (any page except the chat).
    /// </summary>
    [DataMember]
    public bool ShowBack { get => _showBack; private set => SetProperty(ref _showBack, value); }

    /// <summary>
    /// Switches the window to a page.
    /// </summary>
    /// <param name="page">
    /// The page to show.
    /// </param>
    public void Show(AppPage page)
    {
        IsChatView = page == AppPage.Chat;
        IsHistoryView = page == AppPage.History;
        IsSettingsView = page == AppPage.Settings;
        IsAccountView = page == AppPage.Account;
        ShowBack = page != AppPage.Chat;
    }
}
