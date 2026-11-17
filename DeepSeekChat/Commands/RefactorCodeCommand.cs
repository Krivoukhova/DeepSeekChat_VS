using DeepSeekChat.Models;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace DeepSeekChat.Commands;

/// <summary>
/// Code editor context menu item "Refactor with DeepSeek".
/// </summary>
[VisualStudioContribution]
internal sealed class RefactorCodeCommand : Command
{
    private readonly CodeRequestRunner _runner;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefactorCodeCommand"/> class.
    /// </summary>
    public RefactorCodeCommand(VisualStudioExtensibility extensibility, CodeRequestRunner runner)
        : base(extensibility)
    {
        _runner = runner;
    }

    public override CommandConfiguration CommandConfiguration => new("%DeepSeekChat.RefactorCode.DisplayName%")
    {
        Placements =
        [
            CommandPlacement.VsctParent(new Guid("{d309f791-903f-11d0-9efc-00a0c911004f}"), id: 0x01C0, priority: 0x0200),
        ],
        Icon = new(ImageMoniker.Custom("DeepSeekChat"), IconSettings.IconAndText),
    };

    /// <summary>
    /// Sends the code selected in the editor to the chat with a request to refactor it.
    /// </summary>
    /// <param name="context">
    /// Command invocation context.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A task that completes once the chat window is open.
    /// </returns>
    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken) =>
        _runner.RunAsync(context, RequestType.RefactorCode, cancellationToken);
}
