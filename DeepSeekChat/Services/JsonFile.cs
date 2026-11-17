using System.Diagnostics;
using System.Text.Json;

namespace DeepSeekChat.Services;

/// <summary>
/// Reads and writes JSON files with shared serialization settings.
/// </summary>
internal static class JsonFile
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Reads an object from a JSON file.
    /// </summary>
    /// <param name="path">
    /// File path.
    /// </param>
    /// <returns>
    /// The object, or null if the file does not exist or is corrupted or unavailable.
    /// </returns>
    public static T? TryRead<T>(string path) where T : class
    {
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(File.ReadAllText(path), Options);
        }
        catch (JsonException ex)
        {
            Trace.TraceWarning($"'{path}' is corrupted: {ex.Message}");
            BackUpCorrupted(path);
            return null;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Trace.TraceWarning($"Failed to read '{path}': {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Writes an object to a JSON file, creating the data directory if needed.
    /// </summary>
    /// <param name="path">
    /// File path.
    /// </param>
    /// <param name="value">
    /// The object to save.
    /// </param>
    public static void Write<T>(string path, T value)
    {
        AppPaths.EnsureDataDirectory();
        WriteTextAtomic(path, JsonSerializer.Serialize(value, Options));
    }

    /// <summary>
    /// Writes a text file through a temporary file so a crash mid-write cannot destroy the previous content.
    /// </summary>
    /// <param name="path">
    /// Target file path.
    /// </param>
    /// <param name="content">
    /// Text to write.
    /// </param>
    public static void WriteTextAtomic(string path, string content)
    {
        AppPaths.EnsureDataDirectory();
        string temp = path + ".tmp";
        File.WriteAllText(temp, content);

        if (File.Exists(path))
        {
            File.Replace(temp, path, null);
        }
        else
        {
            File.Move(temp, path);
        }
    }

    /// <summary>
    /// Keeps a copy of an unreadable file so the next save does not silently destroy it.
    /// </summary>
    private static void BackUpCorrupted(string path)
    {
        try
        {
            File.Copy(path, path + ".corrupt", overwrite: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Trace.TraceWarning($"Failed to back up '{path}': {ex.Message}");
        }
    }
}
