using System.Runtime.Serialization;
using DeepSeekChat.Constants;
using DeepSeekChat.Models;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Account page: 
/// key state, balance and the name the model uses to address the user.
/// </summary>
[DataContract]
internal sealed class AccountViewModel : NotifyPropertyChangedObject
{
    private readonly DeepSeekApi _api;
    private readonly ApiKeyStore _keyStore;
    private readonly UiContext _ui;

    private string _keyStatus = ChatConstants.Loading;
    private string _balance = ChatConstants.Loading;
    private string _status = ChatConstants.Loading;
    private string _userName;
    private string _nameStatus = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountViewModel"/> class.
    /// </summary>
    public AccountViewModel(DeepSeekApi api, ApiKeyStore keyStore, UiContext ui)
    {
        _api = api;
        _keyStore = keyStore;
        _ui = ui;
        _userName = ui.UserName;

        SaveNameCommand = new AsyncCommand((parameter, token) => SaveNameAsync(parameter as string, token));
    }

    /// <summary>
    /// Gets or sets key status.
    /// </summary>
    [DataMember]
    public string KeyStatus { get => _keyStatus; private set => SetProperty(ref _keyStatus, value); }

    /// <summary>
    /// Gets or sets balance.
    /// </summary>
    [DataMember]
    public string Balance { get => _balance; private set => SetProperty(ref _balance, value); }

    /// <summary>
    /// Gets or sets status.
    /// </summary>
    [DataMember]
    public string Status { get => _status; private set => SetProperty(ref _status, value); }

    /// <summary>
    /// Gets or sets user name.
    /// </summary>
    [DataMember]
    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value ?? string.Empty);
    }

    /// <summary>
    /// Brief "name saved" confirmation.
    /// </summary>
    [DataMember]
    public string NameStatus { get => _nameStatus; private set => SetProperty(ref _nameStatus, value); }

    /// <summary>
    /// Gets save name command.
    /// </summary>
    [DataMember]
    public AsyncCommand SaveNameCommand { get; }

    /// <summary>
    /// Requests current account information from the server and shows it on the page.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        KeyStatus = Balance = Status = ChatConstants.Loading;

        AccountInfo info = await _api.GetAccountAsync(_keyStore.Load(), cancellationToken);

        KeyStatus = _ui.Translate(GetKeyStateKey(info.Key));
        Status = _ui.Translate(GetAccountStateKey(info.State));
        Balance = info.Key == KeyState.Valid
            ? info.UsdBalance ?? _ui.Translate("BalanceUnknown")
            : ChatConstants.NotAvailable;
    }

    /// <summary>
    /// Saves the name and briefly shows a confirmation.
    /// </summary>
    /// <param name="name">
    /// The entered name.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    private async Task SaveNameAsync(string? name, CancellationToken cancellationToken)
    {
        _ui.SetUserName(name ?? string.Empty);
        UserName = _ui.UserName;

        NameStatus = _ui.Translate("NameSaved");
        await Task.Delay(ChatConstants.NameSavedVisibleMs, cancellationToken);
        NameStatus = string.Empty;
    }

    /// <summary>
    /// Picks the translation key for an API key state.
    /// </summary>
    /// <param name="state">
    /// Key state.
    /// </param>
    /// <returns>
    /// String key in the translation table.
    /// </returns>
    private static string GetKeyStateKey(KeyState state) => state switch
    {
        KeyState.Missing => "KeyMissing",
        KeyState.Invalid => "KeyInvalid",
        KeyState.Valid => "KeyValid",
        _ => "KeyCheckFailed",
    };

    /// <summary>
    /// Picks the translation key for an account state.
    /// </summary>
    /// <param name="state">
    /// Account state.
    /// </param>
    /// <returns>
    /// String key in the translation table.
    /// </returns>
    private static string GetAccountStateKey(AccountState state) => state switch
    {
        AccountState.Active => "StatusActive",
        AccountState.InsufficientFunds => "StatusInsufficient",
        AccountState.NoKey => "StatusNoKey",
        AccountState.NoConnection => "StatusNoConnection",
        _ => "Error",
    };
}
