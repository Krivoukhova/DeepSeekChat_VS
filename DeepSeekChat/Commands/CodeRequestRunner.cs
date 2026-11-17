using DeepSeekChat.Models;
using DeepSeekChat.Services;
using DeepSeekChat.UI;
using DeepSeekChat.ViewModels;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Editor;
using Microsoft.VisualStudio.Extensibility.Shell;

namespace DeepSeekChat.Commands;

/// <summary>
/// Shared logic of the "Explain" and "Refactor" commands: 
/// takes the selected code, opens the window and fills the input box.
/// </summary>
internal sealed class CodeRequestRunner
{
    private readonly VisualStudioExtensibility _extensibility;
    private readonly ConversationViewModel _conversation;
    private readonly PageNavigator _navigator;
    private readonly UserPrompts _prompts;
    private readonly UiContext _ui;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeRequestRunner"/> class.
    /// </summary>
    public CodeRequestRunner(
        VisualStudioExtensibility extensibility,
        ConversationViewModel conversation,
        PageNavigator navigator,
        UserPrompts prompts,
        UiContext ui)
    {
        _extensibility = extensibility;
        _conversation = conversation;
        _navigator = navigator;
        _prompts = prompts;
        _ui = ui;
    }

    /// <summary>
    /// Sends the code selected in the active editor to the chat.
    /// </summary>
    /// <param name="context">
    /// Command invocation context.
    /// </param>
    /// <param name="requestType">
    /// Request kind: explanation or refactoring.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    public async Task RunAsync(IClientContext context, RequestType requestType, CancellationToken cancellationToken)
    {
        string code = await ReadSelectionAsync(context, cancellationToken);

        if (string.IsNullOrWhiteSpace(code))
        {
            await _prompts.NotifyAsync(_ui.Translate("SelectCodeFirst"), cancellationToken);
            return;
        }

        _conversation.SetSelectedCode(code, requestType);
        _navigator.Show(AppPage.Chat);
        await _extensibility.Shell().ShowToolWindowAsync<DeepSeekChatToolWindow>(activate: true, cancellationToken);
    }

    /// <summary>
    /// Reads the text selected in the active editor.
    /// </summary>
    /// <param name="context">
    /// Command invocation context.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The selected text; an empty string if there is no editor or selection.
    /// </returns>
    private static async Task<string> ReadSelectionAsync(IClientContext context, CancellationToken cancellationToken)
    {
        using ITextViewSnapshot? textView = await context.GetActiveTextViewAsync(cancellationToken);
        return textView?.Selection.Extent.CopyToString() ?? string.Empty;
    }
}
