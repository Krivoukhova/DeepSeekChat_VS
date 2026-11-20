using DeepSeekChat.UI;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Shell;

namespace DeepSeekChat.Commands;

/// <summary>
/// The "View → DeepSeek AI Assistant" command: opens the chat window.
/// </summary>
[VisualStudioContribution]
internal sealed class OpenChatCommand : Command
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenChatCommand"/> class.
    /// </summary>
    public OpenChatCommand(VisualStudioExtensibility extensibility)
        : base(extensibility)
    {
    }

    public override CommandConfiguration CommandConfiguration => new("%DeepSeekChat.OpenChat.DisplayName%")
    {
        Placements =
        [
            CommandPlacement.VsctParent(new Guid("{d309f791-903f-11d0-9efc-00a0c911004f}"), id: 0x0135, priority: 0x0900),
        ],
        Icon = new(ImageMoniker.Custom("DeepSeekChat"), IconSettings.IconAndText),
    };

    /// <summary>
    /// Opens the DeepSeek chat window and gives it focus.
    /// </summary>
    /// <param name="context">
    /// Command invocation context.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A task that completes once the window is open.
    /// </returns>
    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken) =>
        Extensibility.Shell().ShowToolWindowAsync<DeepSeekChatToolWindow>(activate: true, cancellationToken);
}
