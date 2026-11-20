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
            id: "DeepSeekChatVS.086a659d-7148-4a0f-8edb-498687ffd499",
            version: this.ExtensionAssemblyVersion,
            publisherName: "Krivoukhova",
            displayName: "DeepSeek AI Assistant",
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
