namespace DeepSeekChat.Services;

/// <summary>
/// Keeps copies of sent photos so history thumbnails do not depend on the original files.
/// </summary>
internal sealed class ImageCache
{
    /// <summary>
    /// Copies a file into the cache directory under a unique name.
    /// </summary>
    /// <param name="sourcePath">
    /// Path to the source image.
    /// </param>
    /// <returns>
    /// Path to the cached copy.
    /// </returns>
    public string Store(string sourcePath)
    {
        Directory.CreateDirectory(AppPaths.ImagesDirectory);

        string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(sourcePath);
        string targetPath = Path.Combine(AppPaths.ImagesDirectory, fileName);
        File.Copy(sourcePath, targetPath);

        return targetPath;
    }
}
