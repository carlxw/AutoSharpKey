using System;
using System.Runtime.InteropServices;

class Mouse
{
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePointer mouse);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

    /// <summary>
    /// Necessary struct to call .dll functions
    /// </summary>
    private struct MousePointer
    {
        public int x;
        public int y;

        public MousePointer(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// Move mouse to specified location
    /// </summary>
    /// <param name="x">x-position to move mouse to</param>
    /// <param name="y">y-position to move mouse to</param>
    public static void mouseMove(int x, int y) { SetCursorPos(x, y); }

    /// <summary>
    /// Prints the current mouse coordinates to the console
    /// </summary>
    public static int[] mouseGetPos(bool debug = false)
    {
        MousePointer currentMousePos;
        var pos = GetCursorPos(out currentMousePos);
        if (debug) Console.WriteLine("{0}, {1}", currentMousePos.x, currentMousePos.y);
        int[] output = { currentMousePos.x, currentMousePos.y };
        return output;
    }

    /// <summary>
    /// Click the mouse at its current position
    /// </summary>
    /// <param name="button">Enter string "left" or "right" to perform a left/right click</param>
    /// <param name="times">Number of times to perform the click</param>
    public static void click(string button = "left", int times = 1)
    {
        int[] currentpos = mouseGetPos();
        uint X = (uint)currentpos[0];
        uint Y = (uint)currentpos[1];

        // https://stackoverflow.com/questions/2416748/how-do-you-simulate-mouse-click-in-c
        for (int i = 0; i < times; i++)
        {
            if (button.ToLower() == "left")
            {
                mouse_event(0x02 | 0x04, X, Y, 0, 0);
            }

            else if (button.ToLower() == "right")
            {
                mouse_event(0x08 | 0x10, X, Y, 0, 0);
            }
        }
    }
}