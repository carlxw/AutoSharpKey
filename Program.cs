using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using static Script;
using static System.Net.Mime.MediaTypeNames;

// https://stackoverflow.com/questions/29467348/how-to-set-mouse-position-without-importing-user32-dll-in-wpf
class KeyboardHook
{
    // Global variables for keyboard hook
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    [STAThread]
    public static void Main(string[] args)
    {
        setup();
        _hookID = SetHook(_proc);
        System.Windows.Forms.Application.Run();
        UnhookWindowsHookEx(_hookID);
    }

    // To allow low-level keyboad hook
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    // ===========================================================

    // https://learn.microsoft.com/en-us/archive/blogs/toub/low-level-keyboard-hook-in-c
    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        IntPtr keyDefault = CallNextHookEx(_hookID, nCode, wParam, lParam);
        IntPtr keyBlock = (IntPtr)1;
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            string key = ((Keys)Marshal.ReadInt32(lParam)).ToString();

            // START HANDLING KEYBOARD HOOK HERE ==========
            // Console.WriteLine(key);
            IntPtr output = loop(key, keyDefault, keyBlock);
            // Console.WriteLine(output);
            return output;
        }
        // if (wParam == (IntPtr)WM_KEYUP) Console.WriteLine("{0} - Keyup event", keyDefault);
        return keyDefault;
    }
}