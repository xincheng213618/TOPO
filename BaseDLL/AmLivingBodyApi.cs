using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace BaseDLL
{
    //BaseDLL 的设计原则是所有的调用的DLL  如果是用代码的形式的存在的话，就单独存放，如果是调用了其他的DLL ，并且是通过复用的形式进行调用的话，就分成若干个DLL 进行调用，这样不会让某一个项目，增加若干不存在项目结构

    public class AmLivingBodyApi
    {

        private const string DLLPath = @"C:\Program Files (x86)\AuthenMetric\LivingBody\bin\LibLivingBody.dll";
        /// <summary>
        /// 设置显示视频的窗口句柄及显示区域
        /// </summary>
        /// <param name="handle">显示视频的窗口句柄</param>
        /// <param name="left">显示视频相对于父窗口的左边的距离，单位为像素</param>
        /// <param name="top">显示视频相对于父窗口的上边的距离，单位为像素</param>
        /// <param name="width">显示视频的宽度，单位为像素</param>
        /// <param name="height">显示视频的高度，单位为像素</param>
        /// <returns>=AM_E_SUCCESS 表示成功， 小于0表示错误码</returns>
        [DllImport(DLLPath, EntryPoint = "AmSetVideoWindowHandle", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmSetVideoWindowHandle(IntPtr handle, int left, int top, int width, int height);

    
        /// <summary>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        ///     打开设备
        /// </summary>
        /// <returns>=AM_E_SUCCESS 表示成功，小于0表示错误码</returns>
        [DllImport(DLLPath, EntryPoint = "AmOpenDevice", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmOpenDevices();


        public static int AmOpenDevice(bool OnlyDLL = false)
        {
            if (File.Exists(DLLPath))
            {
                return OnlyDLL ? 0 : AmOpenDevices();
            }
            else
            {
                return -999;
            }
        }


        /// <summary>
        ///关闭设备
        /// </summary>
        /// <returns>=AM_E_SUCCESS 表示成功，小于0表示错误码</returns>
        [DllImport(DLLPath, EntryPoint = "AmCloseDevice", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCloseDevice();


        /**
          * 打开可见光摄像头
          */
        [DllImport(DLLPath, EntryPoint = "AmOpenVISCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmOpenVISCamera();

        /**
          * 关闭可见光摄像头
          */

        [DllImport(DLLPath, EntryPoint = "AmCloseVISCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCloseVISCamera();


        /**
          * 打开近红外摄像头
          */
        [DllImport(DLLPath, EntryPoint = "AmOpenNIRCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmOpenNIRCamera();

        /**
          * 关闭近红外摄像头
          */
        [DllImport(DLLPath, EntryPoint = "AmCloseNIRCamera", CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCloseNIRCamera();


        /// <summary>
        ///     人脸比对
        /// </summary>
        /// <param name="path1">第一张图片全路径，以.jpg或.bmp后缀</param>
        /// <param name="path2">第二张图片全路径，以.jpg或.bmp后缀</param>
        /// <returns>返回值：>0表示两张图片比对的相似度，小于0表示错误码</returns>
        [DllImport(DLLPath, EntryPoint = "AmVerify", CallingConvention = CallingConvention.StdCall)]
        public static extern float AmVerify(string path1, string path2);


        /// <summary>
        ///     发送活体拍照指令，请求活体检测
        /// </summary>
        /// <param name="image">活体通过的图片保存全路径，以.jpg或.bmp后缀</param>
        /// <param name="timeout">指定活体检测的时间，单位为毫秒</param>
        /// <returns></returns>
        [DllImport(DLLPath, CallingConvention = CallingConvention.StdCall)]
        public static extern int AmCaptureImage(string image, int timeout);


        /// <summary>
        ///     发送停止活体拍照指令，请求停止活体检测
        /// </summary>
        /// <returns>=AM_E_SUCCESS 表示成功，小于0表示错误码</returns>
        [DllImport(DLLPath, CallingConvention = CallingConvention.StdCall)]
        public static extern int AmStopCapture();


        /// <summary>
        ///     设置活体抓拍回调函数，请求活体检测的结果由该回调函数返回
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        [DllImport(DLLPath, EntryPoint = "AmSetCaptureImageCallback", CallingConvention = CallingConvention.StdCall)]
        public static extern void AmSetCaptureImageCallback(Delegate callback, IntPtr data);


        /// <summary>
        ///     活体抓拍回调函数
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="image">活体图片全路径</param>
        /// <param name="data">用户数据</param>
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        public delegate void AmCaptureImageProc(int code, string image, IntPtr data);

        public static Dictionary<int, object> Ecode = new Dictionary<int, object>()
        {
            { -1,"内部错误" },
            { -2,"非法指令集" },
            { -3,"无效的模型" },
            { -4,"加载DLL失败" },
            { -257,"未找到授权" },
            { -258,"创建授权错误" },
            { -259,"打开授权错误" },
            { -260,"读取授权错误" },
            { -261,"写入授权错误" },
            { -262,"授权损坏" },
            { -263,"授权过期" },
            { -264,"授权时间异常" },
            { -265,"非法机器" },
            { -266,"检测不到与授权绑定的摄像头" },
            { -513,"无效的句柄" }, 
            { -514,"无效参数" },
            { -515,"无效的图像格式，暂支持jpg、bmp;" },
            { -516,"没有检测到人脸" },
            { -769,"传入无效的窗口句柄" },
            { -770,"活体检测窗体显示失败" },
            { -771,"两个摄像头都已经打开" },
            { -772,"近红外摄像头打开失败" },
            { -773,"可见光摄像头打开失败" },
            { -774,"近红外摄像头未打开" },
            { -775,"可见光摄像头未打开" },
            { -776,"正在活体检测中" },
            { -777,"非活体" },
            { -778,"未知设备" },
            { -779,"没有找到人脸比对引擎" },
            { -780,"人脸比对服务未启动" },
            { -781,"创建引擎失败" },
            { -782,"无效的json串" },
            { -783,"申请内存失败" },
            { -784,"无效的图像路径" },
            { -999,"不存在C:\\Program Files (x86)\\AuthenMetric\\LivingBody\\bin\\LibLivingBody.dll" }
        };
    }

}
