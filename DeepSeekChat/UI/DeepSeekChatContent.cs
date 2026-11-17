using DeepSeekChat.ViewModels;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.UI;

/// <summary>
/// The DeepSeek window content. 
/// The markup is the embedded resource DeepSeekChat.UI.DeepSeekChatContent.xaml.
/// </summary>
internal sealed class DeepSeekChatContent : RemoteUserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeepSeekChatContent"/> class.
    /// </summary>
    public DeepSeekChatContent(ChatViewModel viewModel)
        : base(dataContext: viewModel)
    {
    }
}
