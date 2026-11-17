namespace DeepSeekChat.Localization;

/// <summary>
/// UI language.
/// </summary>
/// <param name="Code">
/// Two-letter language code (ISO 639-1).
/// </param>
/// <param name="NativeName">
/// Language name written in that language.
/// </param>
internal sealed record Language(string Code, string NativeName);
