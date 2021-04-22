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
            public byte this[string index]
            {
                // get 访问器
                get
                {
                    // 返回 index 指定的值
                    switch (index)
                    {
                        case "a":
                        case "A":
                            return A;
                        case "b":
                        case "B":
                            return B;
                        case "C":
                        case "c":
                            return C;

                        case "d":
                        case "D":
                            return D;

                        case "E":
                        case "e":
                            return E;
                        case "f":
                        case "F":
                            return F;
                        case "G":
                        case "g":
                            return G;
                        case "H":
                        case "h":
                            return H;
                        case "I":
                        case "i":
                            return I;
                        case "J":
                        case "j":
                            return J;
                        case "K":
                        case "k":
                            return K;
                        case "L":
                        case "l":
                            return L;
                        case "M":
                        case "m":
                            return M;
                        case "N":
                        case "n":
                            return N;
                        case "O":
                        case "o":
                            return O;
                        case "P":
                        case "p":
                            return P;
                        case "Q":
                        case "q":
                            return Q;
                        case "R":
                        case "r":
                            return R;
                        case "S":
                        case "s":
                            return S;
                        case "T":
                        case "t":
                            return T;
                        case "U":
                        case "u":
                            return U;
                        case "V":
                        case "v":
                            return V;
                        case "W":
                        case "w":
                            return W;
                        case "X":
                        case "x":
                            return X;
                        case "Y":
                        case "y":
                            return Y;
                        case "Z":
                        case "z":
                            return Z;
                        case "1":
                            return one;
                        case "2":
                            return two;
                        case "3":
                            return three;
                        case "4":
                            return four;
                        case "5":
                            return five;
                        case "6":
                            return six;
                        case "7":
                            return seven;
                        case "8":
                            return eight;
                        case "9":
                            return nine;
                        case "0":
                            return zero;
                        case "shift":
                            return SHIFT;
                        default:
                            return 1;

                    }
                }

                // set 访问器
                set
                {
                    // 设置 index 指定的值
                }
            }
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

            public static byte zero = 0x30;
            public static byte one = 0x31;
            public static byte two = 0x32;
            public static byte three = 0x33;
            public static byte four = 0x34;
            public static byte five = 0x35;
            public static byte six = 0x36;
            public static byte seven = 0x37;
            public static byte eight = 0x38;
            public static byte nine = 0x39;


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
