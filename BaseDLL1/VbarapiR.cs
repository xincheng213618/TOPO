using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BaseDLL
{
    public class VbarapiR //R200
    {
        /// <summary>
        /// DLL位置
        /// </summary>
        private const string DLLPath = @"BaseDLL\Vbar\libvbar.dll";
        /// <summary>
        /// 指针
        /// </summary>
        public static IntPtr dev;
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        [DllImport(DLLPath, EntryPoint = "vbar_open", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Vbar_open(string addr, long parm);
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <param name="dev"></param>
        [DllImport(DLLPath, EntryPoint = "vbar_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Vbar_close(IntPtr dev);
        /// <summary>
        /// 背光控制
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="bswitch"></param>
        /// <returns></returns>
        [DllImport(DLLPath, EntryPoint = "vbar_backlight", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Vbar_backlight(IntPtr dev, bool bswitch);
        /// <summary>
        /// 判断硬件设备是否连接
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        [DllImport(DLLPath, EntryPoint = "vbar_is_connected", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Vbar_is_connected(IntPtr dev);
        /// <summary>
        /// 蜂鸣器控制
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        [DllImport(DLLPath, EntryPoint = "vbar_beep", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Vbar_beep(IntPtr dev, int times);
        /// <summary>
        /// 获取扫码结果
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="result_type"></param>
        /// <param name="result_buffer"></param>
        /// <param name="result_size"></param>
        /// <returns></returns>
        [DllImport(DLLPath, EntryPoint = "vbar_scan", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Vbar_scan(IntPtr dev, ref int result_type, byte[] result_buffer, ref int result_size);
        /// <summary>
        /// 码制添加
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="codeFormat"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        [DllImport(DLLPath, EntryPoint = "vbar_add_symbol_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Vbar_add_symbol_type(IntPtr dev, int codeFormat, bool enable);


        /// <summary>
        /// 连接设备
        /// </summary>
        /// <returns></returns>
        public static bool OpenDevice()
        {
            dev = Vbar_open("1", 0);//这里要赋值
            return !(dev == IntPtr.Zero);
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        public static void CloseDevice()
        {
            Vbar_close(dev);
        }
        /// <summary>
        /// 背光控制
        /// </summary>
        /// <param name="bswitch"></param>
        public static void Backlight(bool bswitch)
        {
            if (Isconnected())
            {
                Vbar_backlight(dev, bswitch);
            }
        }
        /// <summary>
        /// 判断设备是否连接
        /// </summary>
        /// <returns></returns>
        public static bool Isconnected()
        {
            return (dev != IntPtr.Zero) && Vbar_is_connected(dev);
        }

        /// <summary>
        /// 设置码制
        /// </summary>
        /// <param name="codeFormat"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public static bool AddSymbolType(int codeFormat, bool enable)
        {
            if (dev != null)
            {
                if (Vbar_add_symbol_type(dev, codeFormat, enable))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// 蜂鸣器控制
        /// </summary>
        public static void BeepControl()
        {
            Vbar_beep(dev, 30);
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        public static void Closedev()
        {
            Vbar_close(dev);
            dev = IntPtr.Zero;
        }
        /// <summary>
        ///  解码设置
        /// </summary>
        /// <param name="result_buffer"></param>
        /// <param name="result_size"></param>
        /// <returns></returns>
        public static bool GetResultStr(out string result_buffer, out int result_size)
        {
            byte[] c_result = new byte[1024];
            int c_size = 0;
            int c_type = 0;

            c_result[0] = 0x01;

            if (Vbar_is_connected(dev))
            {
                if (Vbar_scan(dev, ref c_type, c_result, ref c_size))
                {
                    result_buffer = Encoding.UTF8.GetString(c_result);
                    result_size = c_size;
                    return true;
                }
                else
                {
                    result_buffer = null;
                    result_size = 0;
                    return false;
                }
            }
            else
            {
                result_buffer = null;
                result_size = 0;
                return false;
            }
        }

    }
}
