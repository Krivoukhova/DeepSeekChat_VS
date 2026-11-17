namespace DeepSeekChat.Services;

/// <summary>
/// Paths to the extension's data files in the user profile.
/// </summary>
internal static class AppPaths
{
    /// <summary>
    /// Gets data directory.
    /// </summary>
    public static string DataDirectory { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "DeepSeekChat");

    /// <summary>
    /// Gets the encrypted api key file.
    /// </summary>
    public static string ApiKeyFile => Path.Combine(DataDirectory, "apikey.dat");

    /// <summary>
    /// Gets the plain-text key file used by earlier versions (migrated and deleted on first load).
    /// </summary>
    public static string LegacyApiKeyFile => Path.Combine(DataDirectory, "settings.txt");

    /// <summary>
    /// Gets the chats file.
    /// </summary>
    public static string ChatsFile => Path.Combine(DataDirectory, "chats.json");

    /// <summary>
    /// Gets the preferences file.
    /// </summary>
    public static string PreferencesFile => Path.Combine(DataDirectory, "ui.json");

    /// <summary>
    /// Gets the images directory.
    /// </summary>
    public static string ImagesDirectory => Path.Combine(DataDirectory, "images");

    /// <summary>
    /// Gets the avatar file.
    /// </summary>
    public static string AvatarFile => Path.Combine(DataDirectory, "robot_avatar.png");


    /// <summary>
    /// Creates the data directory if it does not exist yet.
    /// </summary>
    public static void EnsureDataDirectory() => Directory.CreateDirectory(DataDirectory);
}
