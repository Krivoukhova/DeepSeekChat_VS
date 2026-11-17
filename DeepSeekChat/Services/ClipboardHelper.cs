using System.Runtime.InteropServices;

namespace DeepSeekChat.Services;

/// <summary>
/// Writes text to the clipboard through Win32: 
/// the extension runs outside the VS process and has no WPF dispatcher.
/// </summary>
internal static class ClipboardHelper
{
    private const uint UnicodeTextFormat = 13;
    private const uint MovableMemory = 0x0002;
    private const int OpenAttempts = 10;
    private const int RetryDelayMs = 20;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool OpenClipboard(IntPtr owner);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool EmptyClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetClipboardData(uint format, IntPtr memory);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GlobalAlloc(uint flags, UIntPtr bytes);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GlobalLock(IntPtr memory);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalUnlock(IntPtr memory);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GlobalFree(IntPtr memory);

    /// <summary>
    /// Puts text on the clipboard.
    /// </summary>
    /// <param name="text">
    /// The text to copy.
    /// </param>
    /// <returns>
    /// true if the text was placed on the clipboard; otherwise false.
    /// </returns>
    public static bool TrySetText(string text)
    {
        IntPtr memory = AllocateUnicodeString(text);
        if (memory == IntPtr.Zero)
        {
            return false;
        }

        if (TryPublish(memory))
        {
            return true;
        }

        GlobalFree(memory);
        return false;
    }

    /// <summary>
    /// Copies a string to global memory as null-terminated UTF-16.
    /// </summary>
    /// <param name="text">
    /// The text to copy.
    /// </param>
    /// <returns>
    /// The memory handle; IntPtr.Zero if allocation failed.
    /// </returns>
    private static IntPtr AllocateUnicodeString(string text)
    {
        int byteCount = (text.Length + 1) * sizeof(char);
        IntPtr memory = GlobalAlloc(MovableMemory, (UIntPtr)byteCount);
        if (memory == IntPtr.Zero)
        {
            return IntPtr.Zero;
        }

        IntPtr target = GlobalLock(memory);
        if (target == IntPtr.Zero)
        {
            GlobalFree(memory);
            return IntPtr.Zero;
        }

        Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
        Marshal.WriteInt16(target, text.Length * sizeof(char), 0);
        GlobalUnlock(memory);

        return memory;
    }

    /// <summary>
    /// Opens the clipboard (retrying while it is busy) and hands it the memory. 
    /// On success the system owns the memory.
    /// </summary>
    /// <param name="memory">
    /// Memory handle holding the text.
    /// </param>
    /// <returns>
    /// true if the data was placed on the clipboard; otherwise false.
    /// </returns>
    private static bool TryPublish(IntPtr memory)
    {
        for (int attempt = 0; attempt < OpenAttempts; attempt++)
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                Thread.Sleep(RetryDelayMs);
                continue;
            }

            try
            {
                EmptyClipboard();
                return SetClipboardData(UnicodeTextFormat, memory) != IntPtr.Zero;
            }
            finally
            {
                CloseClipboard();
            }
        }

        return false;
    }
}
