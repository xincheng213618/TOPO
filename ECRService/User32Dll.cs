using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECRService
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        public Int32 dx;
        public Int32 dy;
        public Int32 Mousedata;
        public Int32 dwFlag;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeybdInput
    {
        public Int16 wVk;
        public Int16 wScan;
        public Int32 dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        public Int32 uMsg;
        public Int16 wParamL;
        public Int16 wParamH;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Input
    {
        [FieldOffset(0)]
        public Int32 type;
        [FieldOffset(4)]
        public MouseInput mi;
        [FieldOffset(4)]
        public KeybdInput ki;
        [FieldOffset(4)]
        public HardwareInput hi;
    }

    class User32dll
    {
        public const Int32 INPUT_KEYBOARD = 1;
        public const Int32 KEYEVENTF_KEYUP = 0x0002;
        public const Int16 VK_CAPITAL = 0x14;
        public const Int16 VK_BACK = 0x08;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern UInt32 SendInput(UInt32 nInputs, Input[] pInputs, int cbSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short VkKeyScan(char ch);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetKeyboardState(byte[] keyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetKeyboardState(byte[] keyState);
    }
}
