using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace DeepSeekChat.Services;

/// <summary>
/// Local storage for the DeepSeek API key. The key is encrypted with Windows DPAPI
/// (current user scope), so the file is useless outside this Windows account.
/// </summary>
internal sealed class ApiKeyStore
{
    private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("DeepSeekChat.ApiKey.v1");

    /// <summary>
    /// Loads the saved key.
    /// </summary>
    /// <returns>
    /// The key, or an empty string if no key is saved or it cannot be read.
    /// </returns>
    public string Load()
    {
        try
        {
            if (File.Exists(AppPaths.ApiKeyFile))
            {
                byte[] encrypted = Convert.FromBase64String(File.ReadAllText(AppPaths.ApiKeyFile).Trim());
                byte[] plain = ProtectedData.Unprotect(encrypted, Entropy, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(plain).Trim();
            }

            return MigrateLegacyKey();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or CryptographicException or FormatException)
        {
            Trace.TraceWarning($"Failed to read the API key: {ex.GetType().Name}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Encrypts and saves the key to a file in the user profile.
    /// </summary>
    /// <param name="apiKey">
    /// DeepSeek API key.
    /// </param>
    public void Save(string apiKey)
    {
        AppPaths.EnsureDataDirectory();
        byte[] encrypted = ProtectedData.Protect(
            Encoding.UTF8.GetBytes(apiKey.Trim()),
            Entropy,
            DataProtectionScope.CurrentUser);
        JsonFile.WriteTextAtomic(AppPaths.ApiKeyFile, Convert.ToBase64String(encrypted));
    }

    /// <summary>
    /// Moves a plain-text key left by pre-release builds into encrypted storage and deletes the old file.
    /// </summary>
    private string MigrateLegacyKey()
    {
        if (!File.Exists(AppPaths.LegacyApiKeyFile))
        {
            return string.Empty;
        }

        string key = File.ReadAllText(AppPaths.LegacyApiKeyFile).Trim();
        if (key.Length > 0)
        {
            Save(key);
        }

        File.Delete(AppPaths.LegacyApiKeyFile);
        return key;
    }
}
