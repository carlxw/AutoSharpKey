using System;
using System.Runtime.InteropServices;

static class Clipboard
{
    /// <summary>
    /// Gets the text contents of the clipboards
    /// </summary>
    /// <returns>Clipboard string</returns>
    public static string pasteClipboard() { return ModifiedClipboard.GetText(); }

    /// <summary>
    /// Sets a string to the clipboard
    /// </summary>
    /// <param name="str">String to pass to clipboard</param>
    public static void copyClipboard(string str) { ModifiedClipboard.SetText(str); }
}

// Rids the need to rely on .NET class Clipboard
// https://stackoverflow.com/questions/5944605/c-sharp-clipboard-gettext
class ModifiedClipboard
{
    [DllImport("user32.dll")]
    static extern IntPtr GetClipboardData(uint uFormat);
    [DllImport("user32.dll")]
    static extern bool IsClipboardFormatAvailable(uint format);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool OpenClipboard(IntPtr hWndNewOwner);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool CloseClipboard();
    [DllImport("kernel32.dll")]
    static extern IntPtr GlobalLock(IntPtr hMem);
    [DllImport("kernel32.dll")]
    static extern bool GlobalUnlock(IntPtr hMem);

    const uint CF_UNICODETEXT = 13;
    public static string GetText()
    {
        if (!IsClipboardFormatAvailable(CF_UNICODETEXT)) return null;
        if (!OpenClipboard(IntPtr.Zero)) return null;

        string data = null;
        var hGlobal = GetClipboardData(CF_UNICODETEXT);
        if (hGlobal != IntPtr.Zero)
        {
            var lpwcstr = GlobalLock(hGlobal);
            if (lpwcstr != IntPtr.Zero)
            {
                data = Marshal.PtrToStringUni(lpwcstr);
                GlobalUnlock(lpwcstr);
            }
        }
        CloseClipboard();

        return data;
    }

    // Overload .NET Clipboard method
    public static void SetText(string x)
    {
        System.Windows.Forms.Clipboard.SetText(x);
    }
}