using System.Runtime.InteropServices;

namespace DeepSeekChat.Services;

/// <summary>
/// System file picker dialog.
/// </summary>
internal static class FileDialogHelper
{
    /// <summary>
    /// Gets the images filter.
    /// </summary>
    public const string ImagesFilter = "Images\0*.png;*.jpg;*.jpeg;*.gif;*.webp\0All files\0*.*\0\0";

    /// <summary>
    /// Gets the all files filter.
    /// </summary>
    public const string AllFilesFilter = "All files\0*.*\0\0";

    private const int NoChangeDirectory = 0x00000008;
    private const int PathMustExist = 0x00000800;
    private const int FileMustExist = 0x00001000;
    private const int ExplorerStyle = 0x00080000;
    private const int PathBufferChars = 32768;

    /// <summary>
    /// Gets or sets open file name.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct OpenFileName
    {
        public int StructSize;
        public IntPtr Owner;
        public IntPtr Instance;
        public string? Filter;
        public string? CustomFilter;
        public int MaxCustomFilter;
        public int FilterIndex;
        public IntPtr File;
        public int MaxFile;
        public string? FileTitle;
        public int MaxFileTitle;
        public string? InitialDirectory;
        public string? Title;
        public int Flags;
        public short FileOffset;
        public short FileExtension;
        public string? DefaultExtension;
        public IntPtr CustomData;
        public IntPtr Hook;
        public string? TemplateName;
        public IntPtr Reserved;
        public int ReservedInt;
        public int FlagsEx;
    }

    [DllImport("comdlg32.dll", EntryPoint = "GetOpenFileNameW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool GetOpenFileName(ref OpenFileName dialog);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    /// <summary>
    /// Shows the file picker dialog on a dedicated STA thread.
    /// </summary>
    /// <param name="title">
    /// Window title.
    /// </param>
    /// <param name="filter">
    /// Win32 filter ('description\0mask\0' pairs, terminated by a double null).
    /// </param>
    /// <returns>
    /// The selected file path, or null if the selection was cancelled.
    /// </returns>
    public static Task<string?> PickFileAsync(string title, string filter)
    {
        var completion = new TaskCompletionSource<string?>();

        var thread = new Thread(() =>
        {
            try
            {
                completion.SetResult(ShowDialog(title, filter));
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
        })
        {
            IsBackground = true,
        };

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();

        return completion.Task;
    }

    /// <summary>
    /// Shows the system file picker dialog on the current thread.
    /// </summary>
    /// <param name="title">
    /// Window title.
    /// </param>
    /// <param name="filter">
    /// Win32 file filter.
    /// </param>
    /// <returns>
    /// The selected file path, or null if cancelled.
    /// </returns>
    private static string? ShowDialog(string title, string filter)
    {
        IntPtr buffer = Marshal.AllocHGlobal(PathBufferChars * sizeof(char));

        try
        {
            Marshal.WriteInt16(buffer, 0, 0);

            var dialog = new OpenFileName
            {
                Owner = GetForegroundWindow(),
                Filter = filter,
                FilterIndex = 1,
                File = buffer,
                MaxFile = PathBufferChars,
                Title = title,
                Flags = ExplorerStyle | FileMustExist | PathMustExist | NoChangeDirectory,
            };
            dialog.StructSize = Marshal.SizeOf(dialog);

            return GetOpenFileName(ref dialog) ? Marshal.PtrToStringUni(buffer) : null;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}
