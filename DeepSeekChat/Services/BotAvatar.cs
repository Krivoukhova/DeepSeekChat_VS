using System.Diagnostics;
using System.Reflection;

namespace DeepSeekChat.Services;

/// <summary>
/// Bot images on the welcome screen: the head.
/// </summary>
internal static class BotAvatar
{
    private const string AvatarResource = "DeepSeekChat.Resources.robot_avatar.png";

    /// <summary>
    /// Prepares the XAML brush with the bot's head.
    /// </summary>
    /// <returns>
    /// The brush XAML; a transparent brush if the image could not be prepared.
    /// </returns>
    public static string GetAvatarBrushXaml() => GetBrushXaml(AvatarResource, AppPaths.AvatarFile);

    private static string GetBrushXaml(string resourceName, string targetPath)
    {
        try
        {
            ExtractToDataDirectory(resourceName, targetPath);
            return ImageBrushXaml.ForFile(targetPath);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Trace.TraceWarning($"Failed to prepare image '{resourceName}': {ex.Message}");
            return ImageBrushXaml.Transparent;
        }
    }

    /// <summary>
    /// Copies an embedded resource to the data directory if no copy exists or its size differs.
    /// </summary>
    /// <param name="resourceName">
    /// Embedded resource name.
    /// </param>
    /// <param name="targetPath">
    /// Path of the copy file.
    /// </param>
    private static void ExtractToDataDirectory(string resourceName, string targetPath)
    {
        AppPaths.EnsureDataDirectory();

        using Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
            ?? throw new FileNotFoundException($"Embedded resource '{resourceName}' was not found.");

        bool isUpToDate = File.Exists(targetPath) && new FileInfo(targetPath).Length == resource.Length;
        if (isUpToDate)
        {
            return;
        }

        using FileStream file = File.Create(targetPath);
        resource.CopyTo(file);
    }
}
