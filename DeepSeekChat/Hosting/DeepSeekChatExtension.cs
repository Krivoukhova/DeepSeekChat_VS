using DeepSeekChat.Commands;
using DeepSeekChat.Services;
using DeepSeekChat.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;

namespace DeepSeekChat.Hosting;

/// <summary>
/// Entry point of the out-of-process extension: 
/// Visual Studio metadata and service registration.
/// </summary>
[VisualStudioContribution]
internal sealed class DeepSeekChatExtension : Microsoft.VisualStudio.Extensibility.Extension
{
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
            id: "DeepSeekChat.5b0f2c7e-8a41-4f6d-9d3a-6c1e2b7a90f4",
            version: this.ExtensionAssemblyVersion,
            publisherName: "Krivoukhova",
            displayName: "DeepSeek Chat",
            description: "DeepSeek AI assistant integrated into Visual Studio for working with code."),
    };

    /// <summary>
    /// Registers services as singletons: 
    /// the chat state is shared by the window and the editor commands.
    /// </summary>
    /// <param name="serviceCollection">
    /// The extension service collection.
    /// </param>
    protected override void InitializeServices(IServiceCollection serviceCollection)
    {
        base.InitializeServices(serviceCollection);

        serviceCollection.AddSingleton<ApiKeyStore>();
        serviceCollection.AddSingleton<ChatHistoryStore>();
        serviceCollection.AddSingleton<UiPreferencesStore>();
        serviceCollection.AddSingleton<ImageCache>();
        serviceCollection.AddSingleton<AttachmentReader>();
        serviceCollection.AddSingleton<DeepSeekApi>();
        serviceCollection.AddSingleton<ChatSession>();
        serviceCollection.AddSingleton<UiContext>();
        serviceCollection.AddSingleton<UserPrompts>();

        serviceCollection.AddSingleton<PageNavigator>();
        serviceCollection.AddSingleton<ConversationViewModel>();
        serviceCollection.AddSingleton<ModelPickerViewModel>();
        serviceCollection.AddSingleton<HistoryViewModel>();
        serviceCollection.AddSingleton<SettingsViewModel>();
        serviceCollection.AddSingleton<AccountViewModel>();
        serviceCollection.AddSingleton<ChatViewModel>();

        serviceCollection.AddSingleton<CodeRequestRunner>();
    }
}
