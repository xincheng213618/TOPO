using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BaseDLL
{
    /// <summary>
    /// 微光互联 TR200调用
    /// </summary>
    public class Vbarapi
    {
        private const string DLLPath = @"BaseDLL\Vbar\vbar.dll"; //指定全局的DLL调用地址
        private static IntPtr dev = IntPtr.Zero;  //指定一个全局指针
        //打开信道
        [DllImport(DLLPath, EntryPoint = "vbar_channel_open", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Vbar_channel_open(int type, long parm);
        //发送数据
        [DllImport(DLLPath, EntryPoint = "vbar_channel_send", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Vbar_channel_send(IntPtr dev, byte[] data, int length);
        //接收数据
        [DllImport(DLLPath, EntryPoint = "vbar_channel_recv", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Vbar_channel_recv(IntPtr dev, byte[] buffer, int size, int milliseconds);
        //关闭信道
        [DllImport(DLLPath, EntryPoint = "vbar_channel_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Vbar_channel_close(IntPtr dev);


        /// <summary>
        ///连接设备
        /// </summary>
        /// <returns></returns>
        public static bool OpenDevice()
        {
            dev = Vbar_channel_open(1, 1);
            if (dev == IntPtr.Zero)
                return false;
            else
                return true;
        }
        public static bool Isconnected()
        {
            return dev != IntPtr.Zero;
        }


        /// <summary>
        ///  关闭设备
        /// </summary>
        public static void CloseDevice()
        {
            if (dev != null)
            {
                Vbar_channel_close(dev);
                dev = IntPtr.Zero;
            }

        }
        private static byte[] iSetByte_ctl = new byte[64];
        /// <summary>
        ///  扫码开关
        /// </summary>
        /// <param name="cswitch"></param>
        public static void ControlScan(bool cswitch)
        {
            if (dev != null)
            {
                if (cswitch)
                {
                    iSetByte_ctl[0] = 0x55;
                    iSetByte_ctl[1] = 0xAA;
                    iSetByte_ctl[2] = 0x05;
                    iSetByte_ctl[3] = 0x01;
                    iSetByte_ctl[4] = 0x00;
                    iSetByte_ctl[5] = 0x00;
                    iSetByte_ctl[6] = 0xfb;
                }
                else
                {
                    iSetByte_ctl[0] = 0x55;
                    iSetByte_ctl[1] = 0xAA;
                    iSetByte_ctl[2] = 0x05;
                    iSetByte_ctl[3] = 0x01;
                    iSetByte_ctl[4] = 0x00;
                    iSetByte_ctl[5] = 0x01;
                    iSetByte_ctl[6] = 0xfa;
                }
                Vbar_channel_send(dev, iSetByte_ctl, 64);
            }

        }
 
        private static byte[] iSetByte = new byte[64] ;
        /// <summary>
        ///   背光控制
        /// </summary>
        /// <param name="bswitch"></param>
        public static void Backlight(bool bswitch)
        {
            if (dev != null)
            {
                if (bswitch)
                {
                    iSetByte[0] = 0x55;
                    iSetByte[1] = 0xAA;
                    iSetByte[2] = 0x24;
                    iSetByte[3] = 0x01;
                    iSetByte[4] = 0x00;  
                    iSetByte[5] = 0x01;
                    iSetByte[6] = 0xDB;
                }
                else
                {
                    iSetByte[0] = 0x55;
                    iSetByte[1] = 0xAA;
                    iSetByte[2] = 0x24;
                    iSetByte[3] = 0x01;
                    iSetByte[4] = 0x00;
                    iSetByte[5] = 0x00;
                    iSetByte[6] = 0xDA;
                }
                Vbar_channel_send(dev, iSetByte, 64);
            }
        }
        /// <summary>
        ///  解码设置
        /// </summary>
        /// <param name="result_buffer"></param>
        /// <param name="result_size"></param>
        /// <returns></returns>
        public static bool GetResultStr(out string result_buffer, out int result_size)
        {
            byte[] c_result = new byte[256];
            if (dev != null)
            {
                byte[] bufferrecv = new byte[1024];
                Vbar_channel_recv(dev, bufferrecv, 1024, 200);
                if (bufferrecv[0] == 85 && bufferrecv[1] == 170 && bufferrecv[3] == 0)
                {
                    //联系厂家修改过
                    //int datalen = (bufferrecv[4] & 0xff) + ((bufferrecv[5] << 8) & 0xff);
                    int datalen = bufferrecv[4] + (bufferrecv[5] << 8);
                    byte[] readBuffers = new byte[datalen];
                    for (int s1 = 0; s1 < datalen; s1++)
                    {
                        readBuffers[s1] = bufferrecv[6 + s1];
                    }

                    result_buffer = Encoding.UTF8.GetString(readBuffers);
                    result_size = datalen;
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
