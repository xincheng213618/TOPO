﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EXCResources
{
    /// <summary>
    /// 微光互联调用
    /// </summary>
    public class Vbarapi
    {
        private static IntPtr dev = IntPtr.Zero;

        //打开信道
        [DllImport("Resources\\vbar.dll", EntryPoint = "vbar_channel_open", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr vbar_channel_open(int type, long parm);
        //发送数据
        [DllImport("Resources\\vbar.dll", EntryPoint = "vbar_channel_send", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vbar_channel_send(IntPtr dev, byte[] data, int length);
        //接收数据
        [DllImport("Resources\\vbar.dll", EntryPoint = "vbar_channel_recv", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vbar_channel_recv(IntPtr dev, byte[] buffer, int size, int milliseconds);
        //关闭信道
        [DllImport("Resources\\vbar.dll", EntryPoint = "vbar_channel_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void vbar_channel_close(IntPtr dev);


        //连接设备
        public static bool OpenDevice()
        {
            dev = vbar_channel_open(1, 1);
            if (dev == IntPtr.Zero)
                return false;
            else
                return true;
        }
        //关闭设备
        public static void CloseDevice()
        {
            if (dev != null)
            {
                vbar_channel_close(dev);
                dev = IntPtr.Zero;
            }

        }
        private static byte[] iSetByte_ctl = new byte[64];
        //扫码开关
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
                vbar_channel_send(dev, iSetByte_ctl, 64);
            }

        }
        //背光控制
        private static byte[] iSetByte = new byte[64];
        public static void backlight(bool bswitch)
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
                vbar_channel_send(dev, iSetByte, 64);
            }
        }
        //解码设置
        public static bool getResultStr(out byte[] result_buffer, out int result_size)
        {
            byte[] c_result = new byte[256];
            if (dev != null)
            {
                byte[] bufferrecv = new byte[1024];
                vbar_channel_recv(dev, bufferrecv, 1024, 200);
                if (bufferrecv[0] == 85 && bufferrecv[1] == 170 && bufferrecv[3] == 0)
                {
                    //MessageBox.Show(bufferrecv[4].ToString());
                    //MessageBox.Show(bufferrecv[5].ToString());
                    //联系厂家修改过
                    //int datalen = (bufferrecv[4] & 0xff) + ((bufferrecv[5] << 8) & 0xff);
                    int datalen = bufferrecv[4] + (bufferrecv[5] << 8);
                    byte[] readBuffers = new byte[datalen];
                    for (int s1 = 0; s1 < datalen; s1++)
                    {
                        readBuffers[s1] = bufferrecv[6 + s1];
                    }
                    result_buffer = readBuffers;
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
