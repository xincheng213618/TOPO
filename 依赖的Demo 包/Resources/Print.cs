using System;
using System.Collections.Generic;
using System.Management;

namespace Resources
{
    public class Prints
    {
        //Desgined By Mr.Xin 2020.6.17
        public static int PrinterStatue(string PrinterDevice)
        {
            int ret = 0;
            string path = @"win32_printer.DeviceId='" + PrinterDevice + "'";
            ManagementObject printer = new ManagementObject(path);
            printer.Get();
            ret = Convert.ToInt32(printer.Properties["PrinterState"].Value);
            string a = printer.Properties["PortName"].Value.ToString();

            //MessageBox.Show(printer.Properties["WorkOffline"].Value.ToString());
            //string a = PrintCode[ret];

            return ret;
        }

        public static Dictionary<int, string> PrinterStatusCodes = new Dictionary<int, string>()
        {
            { 0,"打印机准备就绪" },
            { 1,"打印机已暂停" },
            { 2,"打印机错误" },
            { 4,"打印机待删除" },
            { 8,"卡纸" },
            { 16,"没纸了" },
            { 32,"手动进纸" },
            { 64,"纸张问题" },
            { 128,"打印机离线" },
            { 256,"IO active" },
            { 512,"打印机忙" },
            { 1024,"正在打印" },
            { 1025 ,"正在暂停打印"},
            { 2048,"打印机出纸槽已满" },
            { 4096,"无法使用。" },
            { 8192,"等候中" },
            { 16384,"处理中" },
            { 17408 ,"正在进行后台打印"},
            { 32768,"初始化中" },
            { 65536,"热身" },
            { 131072,"碳粉不足" },
            { 262144,"无碳粉" },
            { 524288,"Page punt" },
            { 1048576,"用户干预" },
            { 2097152,"内存不足" },
            { 4194304,"	通过开放" },
            { 8388608,"服务器未知" },
            { 6777216,"节能" },
        };

    }
}
