using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;

class Windows32
{
    // Async version is used for faster compile time
    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    private const int SW_SHOWNORMAL = 1;
    private const int SW_SHOWMINIMIZED = 2;
    private const int SW_MAXIMIZE = 3;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    private static IntPtr findWindow(string windowName)
    {
        Process[] arr = Process.GetProcesses();
        foreach (var x in arr)
        {
            if (x.MainWindowTitle.ToLower().Contains(windowName.ToLower())) return x.MainWindowHandle;
        }
        return (IntPtr)0;
    }

    /// <summary>
    /// Wait for user-defined amount in milliseconds. Program to temporarily deactivate for said time.
    /// </summary>
    /// <param name="ms">Number of milliseconds to do nothing, as an integer value</param>
    public static void sleep(int ms) { Thread.Sleep(ms); }

    /// <summary>
    /// https://stackoverflow.com/questions/2817707/find-and-activate-an-applications-window
    /// Activates a window given a search string
    /// </summary>
    /// <param name="window">Title of window to activate</param>
    /// <returns>True if the operation is a success</returns>
    public static bool winActivate(string windowName)
    {
        IntPtr window = findWindow(windowName);
        if (window != (IntPtr)0)
        {
            // Maximize window, then activate it in that order
            winMaximize(window);
            SetForegroundWindow(window);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Displays a message box
    /// </summary>
    /// <param name="message">Message to be displayed by message box</param>
    public static void msgBox(string message) { MessageBox.Show(message, "C# Hotkeys"); }

    /// <summary>
    /// Terminates the program
    /// </summary>
    public static void exitApp() { Environment.Exit(1); }

    /// <summary>
    /// Gets the active window title
    /// </summary>
    public static string winGetActiveTitle()
    {
        StringBuilder Buff = new StringBuilder(256);
        IntPtr handle = GetForegroundWindow();
        GetWindowText(handle, Buff, 256);
        return Buff.ToString();

        /*
        if (GetWindowText(handle, Buff, 256) > 0) return Buff.ToString();
        return null;
        */
    }

    /// <summary>
    /// Makeshift implementation of winActive. Will not work 100% of the time.
    /// </summary>
    /// <param name="windowName">Window name to verify if active or not</param>
    /// <returns>Returns true if the specified window is active</returns>
    public static bool winActive(string windowName)
    {
        if (!winExist(windowName)) return false;
        string currentActive = winGetActiveTitle();
        if (currentActive == null) Console.WriteLine("Something went wrong");
        if (currentActive.ToUpper().Contains(windowName.ToUpper())) return true;
        else return false;
    }

    /// <summary>
    /// Checks if a particular window name exists in the background
    /// </summary>
    /// <param name="windowName">Window name to search for</param>
    /// <returns>True if the window is found</returns>
    public static bool winExist(string windowName)
    {
        IntPtr window = findWindow(windowName);
        if (window != (IntPtr)0) return true;
        return false;
    }

    /// <summary>
    /// Maximizes the given window
    /// </summary>
    /// <param name="hwnd">Window handle to target</param>
    public static void winMaximize(IntPtr hwnd) { ShowWindowAsync(hwnd, SW_MAXIMIZE); }

    /// <summary>
    /// Minimizes the given window
    /// </summary>
    /// <param name="hwnd">Window handle to target</param>
    public static void winMinimize(IntPtr hwnd) { ShowWindowAsync(hwnd, SW_SHOWMINIMIZED); }
}