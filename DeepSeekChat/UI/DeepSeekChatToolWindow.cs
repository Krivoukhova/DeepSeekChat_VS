using DeepSeekChat.Constants;
using DeepSeekChat.ViewModels;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.ToolWindows;
using Microsoft.VisualStudio.RpcContracts.RemoteUI;

namespace DeepSeekChat.UI;

/// <summary>
/// The DeepSeek chat window.
/// </summary>
[VisualStudioContribution]
internal sealed class DeepSeekChatToolWindow : ToolWindow
{
    private readonly DeepSeekChatContent _content;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeepSeekChatToolWindow"/> class.
    /// </summary>
    public DeepSeekChatToolWindow(VisualStudioExtensibility extensibility, ChatViewModel viewModel)
        : base(extensibility)
    {
        Title = ChatConstants.AppName;
        _content = new DeepSeekChatContent(viewModel);
    }

    public override ToolWindowConfiguration ToolWindowConfiguration => new()
    {
        Placement = ToolWindowPlacement.Floating,
        DockDirection = Dock.Right,
    };

    /// <summary>
    /// No initialization is needed: 
    /// all state lives in the data model shared by the window and the commands.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A completed task.
    /// </returns>
    public override Task InitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// Returns the window content.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The Remote UI control with the chat markup.
    /// </returns>
    public override Task<IRemoteUserControl> GetContentAsync(CancellationToken cancellationToken) =>
        Task.FromResult<IRemoteUserControl>(_content);

    /// <summary>
    /// Disposes the window content. 
    /// The data model belongs to the service container and is not disposed.
    /// </summary>
    /// <param name="disposing">
    /// true when disposing explicitly.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _content.Dispose();
        }

        base.Dispose(disposing);
    }
}
