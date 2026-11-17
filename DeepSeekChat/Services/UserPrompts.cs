using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Shell;

namespace DeepSeekChat.Services;

/// <summary>
/// Visual Studio modal messages: 
/// confirmations and notifications.
/// </summary>
internal sealed class UserPrompts
{
    private readonly VisualStudioExtensibility _extensibility;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserPrompts"/> class.
    /// </summary>
    public UserPrompts(VisualStudioExtensibility extensibility)
    {
        _extensibility = extensibility;
    }

    /// <summary>
    /// Asks the user to confirm an action.
    /// </summary>
    /// <param name="message">
    /// Question text.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// true if the user confirmed; otherwise false.
    /// </returns>
    public Task<bool> ConfirmAsync(string message, CancellationToken cancellationToken) =>
        _extensibility.Shell().ShowPromptAsync(message, PromptOptions.OKCancel, cancellationToken);

    /// <summary>
    /// Shows an informational message with an OK button.
    /// </summary>
    /// <param name="message">
    /// Message text.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    public async Task NotifyAsync(string message, CancellationToken cancellationToken)
    {
        await _extensibility.Shell().ShowPromptAsync(message, PromptOptions.OK, cancellationToken);
    }
}
