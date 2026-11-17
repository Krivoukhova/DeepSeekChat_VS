using System.Security;

namespace DeepSeekChat.Services;

/// <summary>
/// Builds XAML brushes for showing pictures in Remote UI, which cannot reference extension resources.
/// </summary>
internal static class ImageBrushXaml
{
    private const string PresentationNamespace = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";

    /// <summary>
    /// XAML of a transparent brush — the fallback when a picture cannot be shown.
    /// </summary>
    public static string Transparent { get; } =
        $"<SolidColorBrush xmlns=\"{PresentationNamespace}\" Color=\"Transparent\"/>";

    /// <summary>
    /// Creates a brush with an image from a file.
    /// </summary>
    /// <param name="filePath">
    /// Path to the image file.
    /// </param>
    /// <returns>
    /// The brush XAML; a transparent brush if the path is invalid.
    /// </returns>
    public static string ForFile(string filePath)
    {
        if (!Uri.TryCreate(filePath, UriKind.Absolute, out Uri? uri))
        {
            return Transparent;
        }

        string source = SecurityElement.Escape(uri.AbsoluteUri) ?? string.Empty;
        return $"<ImageBrush xmlns=\"{PresentationNamespace}\" ImageSource=\"{source}\" Stretch=\"UniformToFill\"/>";
    }
}
