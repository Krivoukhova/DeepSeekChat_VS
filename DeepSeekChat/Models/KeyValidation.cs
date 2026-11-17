namespace DeepSeekChat.Models;

/// <summary>
/// Outcome of an API key check.
/// </summary>
internal enum KeyValidationStatus
{
    Accepted,
    Empty,
    Rejected,
}

/// <summary>
/// Result of an API key check performed by the DeepSeek server.
/// </summary>
/// <param name="Status">
/// Check outcome.
/// </param>
/// <param name="Details">
/// Server error text for a rejected key.
/// </param>
internal sealed record KeyValidation(KeyValidationStatus Status, string Details = "")
{
    /// <summary>
    /// Gets accepted.
    /// </summary>
    public static KeyValidation Accepted { get; } = new(KeyValidationStatus.Accepted);

    /// <summary>
    /// Gets empty.
    /// </summary>
    public static KeyValidation Empty { get; } = new(KeyValidationStatus.Empty);

    public static KeyValidation Rejected(string details) => new(KeyValidationStatus.Rejected, details);
}
