using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;

namespace ECRService
{
    class AmLivingBodyApi
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        /**
         * 设置窗口句柄
         */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmSetVideoWindowHandle", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmSetVideoWindowHandle(IntPtr handle, int left, int top, int width, int height);

        /**
          * 活体抓拍回调函数
          */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmSetCaptureImageCallback", CallingConvention = CallingConvention.StdCall)]
        public static extern void AmSetCaptureImageCallback(AmCaptureImageProc callback, IntPtr data);

        /**
          * 打开摄像头
          */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmOpenDevice();

        /**
         * 关闭摄像头
         */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCloseDevice();

        /**
          * 打开可见光摄像头
          */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmOpenVISCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmOpenVISCamera();

        /**
          * 关闭可见光摄像头
          */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmCloseVISCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCloseVISCamera();


        /**
          * 打开近红外摄像头
          */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmOpenNIRCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmOpenNIRCamera();

        /**
          * 关闭近红外摄像头
          */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmCloseNIRCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCloseNIRCamera();

        /**
         * 开始抓拍活体
         */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmCaptureImage", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCaptureImage(string image, long timeout);

        /**
         * 停止活体检测
         */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmStopCapture", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmStopCapture();

        /**
         * 活体抓拍图像与证件照进行比对
         */
        [DllImport("C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll", EntryPoint = "AmVerify", CallingConvention = CallingConvention.StdCall)]
        public static extern float AmVerify(string image1, string image2);

        /// <summary>
        ///     活体抓拍回调函数
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="image">活体图片全路径</param>
        /// <param name="data">用户数据</param>
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        public delegate void AmCaptureImageProc(int code, string image, IntPtr data);
    }
}
