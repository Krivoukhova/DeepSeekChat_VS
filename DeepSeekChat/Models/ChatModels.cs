namespace DeepSeekChat.Models;

/// <summary>
/// The list of models offered in the chat.
/// </summary>
internal static class ChatModels
{
    /// <summary>
    /// Gets flash.
    /// </summary>
    public static ChatModel Flash { get; } = new("flash", "deepseek-flash", "DeepSeek-V4.1-Flash", true);

    /// <summary>
    /// Gets pro.
    /// </summary>
    public static ChatModel Pro { get; } = new("pro", "deepseek-v4-pro", "DeepSeek-V4-Pro", false);

    /// <summary>
    /// Gets all.
    /// </summary>
    public static IReadOnlyList<ChatModel> All { get; } = new[] { Flash, Pro };

    /// <summary>
    /// Finds a model by id.
    /// </summary>
    /// <param name="id">
    /// Model id.
    /// </param>
    /// <returns>
    /// The matching model, or Flash if the id is unknown.
    /// </returns>
    public static ChatModel Get(string? id) => All.FirstOrDefault(model => model.Id == id) ?? Flash;

    /// <summary>
    /// Picks the model that will handle a request with images.
    /// </summary>
    /// <param name="selected">
    /// The model chosen by the user.
    /// </param>
    /// <returns>
    /// The chosen model if it accepts images; otherwise Flash.
    /// </returns>
    public static ChatModel ForImages(ChatModel selected) => selected.SupportsImages ? selected : Flash;
}
