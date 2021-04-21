using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BaseUtil
{
    public static partial class KeyHelper
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto)]
        public static extern void Keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        /// <summary> 模拟按下指定建 Keys.ControlKey </summary>
        public static void OnKeyPress(byte keyCode)
        {
            Keybd_event(keyCode, 0, 0, 0);
            Keybd_event(keyCode, 0, 2, 0);
        }

        public static void OnKeyDown(byte keyCode)
        {
            Keybd_event(keyCode, 0, 0, 0);
        }

        public static void OnKeyUp(byte keyCode)
        {
            Keybd_event(keyCode, 0, 2, 0);
        }

        public static void HotKey(List<byte> keyCodes)
        {
            for (int i = 0; i < keyCodes.Count; i++)
            {
                Keybd_event(keyCodes[i], 0, 0, 0);
            }
            for (int i = 0; i < keyCodes.Count; i++)
            {
                Keybd_event(keyCodes[i], 0, 2, 0);
            }

        }
        public class KeyCode
        {
            public static byte SELECT = 0x29;
            public static byte DEL = 0x2E;
            public static byte HELP = 0x2F;
            public static byte BACK = 0x08;
            public static byte CLEAR = 0x0C;
            public static byte ENTER = 0x0D;

            public static byte SHIFT = 0x10;
            public static byte CTRL = 0x11;
            public static byte ALT = 0x12;
            public static byte PAUSE = 0x13;
            public static byte CAPS_LOCK = 0x14;
            public static byte SPACE = 0x20;
            public static byte END = 0x23;
            public static byte HOME = 0x24;


            public static byte A = 0x41;
            public static byte B = 0x42;
            public static byte C = 0x43;
            public static byte D = 0x44;
            public static byte E = 0x45;
            public static byte F = 0x46;
            public static byte G = 0x47;
            public static byte H = 0x48;
            public static byte I = 0x49;
            public static byte J = 0x4A;
            public static byte K = 0x4B;
            public static byte L = 0x4C;
            public static byte M = 0x4D;
            public static byte N = 0x4E;
            public static byte O = 0x4F;
            public static byte P = 0x50;
            public static byte Q = 0x51;
            public static byte R = 0x52;
            public static byte S = 0x53;
            public static byte T = 0x54;
            public static byte U = 0x55;
            public static byte V = 0x56;
            public static byte W = 0x57;
            public static byte X = 0x58;
            public static byte Y = 0x59;
            public static byte Z = 0x5A;

            public static byte WinL = 0x5B;
            public static byte WinR = 0x5C;

            public static byte a = 0x61;
            public static byte b = 0x62;
            public static byte c = 0x63;
            public static byte d = 0x64;
            public static byte e = 0x65;
            public static byte f = 0x66;
            public static byte g = 0x67;
            public static byte h = 0x68;
            public static byte i = 0x69;
            public static byte j = 0x6A;
            public static byte k = 0x6B;
            public static byte l = 0x6C;
            public static byte m = 0x6D;
            public static byte n = 0x6E;
            public static byte o = 0x6F;
            public static byte p = 0x70;
            public static byte q = 0x71;
            public static byte r = 0x72;
            public static byte s = 0x73;
            public static byte t = 0x74;
            public static byte u = 0x75;
            public static byte v = 0x76;
            public static byte w = 0x77;
            public static byte x = 0x78;
            public static byte y = 0x79;
            public static byte z = 0x7A;
        }
    }
}
