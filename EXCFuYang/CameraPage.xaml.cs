﻿using BaseDLL;
using BaseUtil;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace XinHua
{
    /// <summary>
    /// CameraPage.xaml 的交互逻辑
    /// </summary>
    public partial class CameraPage : Page
    {
        private static bool ShootSucess = false;
        private static double b, c;
       
        public CameraPage()
        {           
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 900, 675);
            AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
            AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 3000);

            DataContext = Time;
            Countdown_timer();
        }


        AmLivingBodyApi.AmCaptureImageProc capture_image_callback = new AmLivingBodyApi.AmCaptureImageProc(OnCaptureImage);
        private unsafe static void OnCaptureImage(int code, string image, IntPtr data)
        {
            ShootSucess = code == 0;
        }

        //对比逻辑
        private int tryCount = 0;
        private void Comparison()
        {
            string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";
            string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";
            if (Global.Related.IDCardData.PhotoFileName == null)
                return;
            b = AmLivingBodyApi.AmVerify(Global.Related.IDCardData.PhotoFileName, paths);
            c = AmLivingBodyApi.AmVerify(Global.Related.IDCardData.PhotoFileName, paths_black);

            ShootSucess = false;

            Log.Write("人脸比对相似度:" + Environment.NewLine + b.ToString() + Environment.NewLine + c.ToString());

            if (b > 0.7 && c > 0.7)
            {
                SwitchPage();
            }
            else
            {
                AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 30000);
                tryCount += 1;
            }
            if (tryCount > 2)
            {
                Content = new HomePage("人脸对比失败，请重试");
                Pages();
            }
        }

        private void SwitchPage()
        {
            switch (Global.Related.PageType)
            {
                case "XinHuaPrint":
                    Content = new Report();
                    Pages();
                    break;
                default:
                    Content = new HomePage("没有进入页面,人脸对比成功");
                    Pages();

                    break;
            }

        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        //倒计时
        DispatcherTimer pageTimer = null;
        TimeCount Time = new TimeCount();
        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled=true,
                Interval=TimeSpan.FromSeconds(1)
            };
            pageTimer.Tick += new EventHandler((sender,e)=>
            {
                if (--Time.Countdown >= 0)
                {
                    if (ShootSucess)
                    {
                        Media.Player(0);
                        Comparison();
                    }

                    if (Time.Countdown % 10 == 0)
                        Media.Player(4);
                }
                else
                {
                    Content = new HomePage("超时自动返回");
                    Pages();
                }
            });
        }

        private string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";
        private string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";

        private void Pages()
        {
            //删除人脸识别缓存
            File.Delete(paths);
            File.Delete(paths_black);

            IDcard.DeleteIDcardImages(Global.Related.IDCardData);

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));

            AmLivingBodyApi.AmStopCapture();
            AmLivingBodyApi.AmCloseDevice();

        }
    }
}
