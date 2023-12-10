using System;
using static Keyboard;
using static Mouse;
using static Windows32;
using static Clipboard;
using System.Linq;

class Script
{
    public static void setup()
    {
        Console.WriteLine("Program is active!");
    }

    static bool enable = true;
    public static IntPtr loop(string key, IntPtr keyDefault, IntPtr keyBlock)
    {
        if (!enable) return keyDefault;
        switch (key)
        {
            case "F12":
                exitApp();
                break;

            case "J":
                Console.WriteLine("J key is pressed!");
                return keyBlock;
        }
        return keyDefault;
    }
}