namespace DeepSeekChat.Models;

/// <summary>
/// API key state.
/// </summary>
internal enum KeyState
{
    Missing,
    Invalid,
    Valid,
    CheckFailed,
}

/// <summary>
/// DeepSeek account state.
/// </summary>
internal enum AccountState
{
    Active,
    InsufficientFunds,
    NoKey,
    Error,
    NoConnection,
}

/// <summary>
/// Account information obtained with an API key.
/// </summary>
/// <param name="Key">
/// Key state.
/// </param>
/// <param name="UsdBalance">
/// Balance in US dollars; null if unavailable.
/// </param>
/// <param name="State">
/// Account state.
/// </param>
internal sealed record AccountInfo(KeyState Key, string? UsdBalance, AccountState State);
