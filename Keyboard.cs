using System.Windows.Forms;

class Keyboard
{
    /// <summary>
    /// Send input to system
    /// </summary>
    /// <param name="str">Character or string of characters to send</param>
    public static void send(string str, uint repeat = 0)
    {
        if (repeat == 0) SendKeys.SendWait(str);
        else for (uint i = 0; i < repeat; i++) SendKeys.SendWait(str);
    }

    /// <summary>
    /// Send input string to system
    /// </summary>
    /// <param name="str">Character or string of characters to send</param>
    public static void sendStr(string str) { for (int i = 0; i < str.Length; i++) SendKeys.SendWait(str[i].ToString()); }
}