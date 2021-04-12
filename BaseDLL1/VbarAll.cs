using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDLL
{
    // 扫码的部分，
    public static class VbarAll
    {
        /// <summary>
        ///  扫码器
        /// </summary>
        private static string Kind = null;

       
        /// <summary>
        /// 打开设备;
        /// </summary>
        /// <returns>ture 成功</returns>
        public static bool Open()
        {
            if (VbarapiR.OpenDevice())
            {
                Kind = "VbarapiR";

                return true;
            }
            else if (Vbarapi.OpenDevice())
            {
                Kind = "Vbarapi";
                return true;
            }
            return false;
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        public static void Close()
        {
            if (Kind == "Vbarapi")
            {
                Vbarapi.CloseDevice();
            }
            else if (Kind == "VbarapiR")
            {
                VbarapiR.CloseDevice();
            }
        }
        /// <summary>
        /// 是否连接
        /// </summary>
        /// <returns></returns>
        public static bool Isconnected()
        {
            if (Kind == "Vbarapi")
            {
                return Vbarapi.Isconnected();
            }
            else if (Kind == "VbarapiR")
            {
                return VbarapiR.Isconnected();
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 背光控制
        /// </summary>
        /// <param name="bswitch"></param>
        public static void Backlight(bool bswitch)
        {
            if (Kind == "Vbarapi")
            {
                Vbarapi.Backlight(bswitch);
            }
            else if (Kind == "VbarapiR")
            {
                VbarapiR.Backlight(bswitch);
            }
        }
        public static void BeepControl()
        {
            if (Kind == "VbarapiR")
            {
                VbarapiR.BeepControl();
            }
        }

        public static void AddSymbolType(int codeFormat, bool enable)
        {
            if (Kind == "VbarapiR")
            {
                VbarapiR.AddSymbolType(codeFormat, enable);
            }
        }

        /// <summary>
        /// 获取扫码内容
        /// </summary>
        /// <param name="result_buffer">内容</param>
        /// <param name="result_size">大小</param>
        /// <returns></returns>
        public static bool GetResultStr(out string result_buffer, out int result_size)
        {
            string Msg = null;
            int Size = 0;
            bool Su = false;
            if (Kind == "Vbarapi")
            {
                Su = Vbarapi.GetResultStr(out Msg, out Size);
                result_buffer = Msg;
                result_size = Size;
                return Su;
            }
            else if (Kind == "VbarapiR")
            {
                Su = VbarapiR.GetResultStr(out Msg, out Size);
                result_buffer = Msg;
                result_size = Size;
                return Su;
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
