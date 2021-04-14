using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace REC
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


        private DispatcherTimer pageTimer = null;
        public RECDate timeCount = new RECDate() { Countdown = 20};

        private void Page_Initialized(object sender, EventArgs e)
        {
            formhost.Width = 900;
            formhost.Height = 675;
            Log.Write("正在尝试打开摄像头："+ AmLivingBodyApi.AmOpenDevice().ToString());
            AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 900, 675);
            AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
            AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 20000);

            TopGrid.DataContext = timeCount;
            Countdown();
        }


        AmLivingBodyApi.AmCaptureImageProc capture_image_callback = new AmLivingBodyApi.AmCaptureImageProc(OnCaptureImage);
        private unsafe static void OnCaptureImage(int code, string image, IntPtr data)
        {
            ShootSucess = code == 0;
        }


        private int tryCount = 0;

        string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";

        string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";

        private void Comparison()
        {
            if (Global.Related.IDCardData.PhotoFileName != null)
            {
                b = AmLivingBodyApi.AmVerify(Global.Related.IDCardData.PhotoFileName, paths);
                c = AmLivingBodyApi.AmVerify(Global.Related.IDCardData.PhotoFileName, paths_black);

                ShootSucess = false;

                if (b > 0.7 && c > 0.7)
                {
                    Requests.Camer_Upload(Global.Related.IDCardData.IDCardNo, Global.Related.IDCardData.Name, b.ToString(), paths, Global.Related.IDCardData.PhotoFileName);
                    SwitchPage();
                }
                else if (b < 0)
                {
                    Content = new HomePage("摄像头报错："+ Environment.NewLine + b+Environment.NewLine + b.ToString());
                    Pages();
                }
                else
                {
                    AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 20000);
                    tryCount += 1;
                }

                if (tryCount > 2)//在这里设置错误验证次数
                {
                    Content = new HomePage("人脸对比失败，请重试");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("读卡器未读取到身份证照片信息，请重试，如还有问题请联系工作人员");
                Pages();
            }

        }



        private void Countdown()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1), };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown >= 0)
                {

                    if (ShootSucess)
                    {
                        Media.Player(0);
                        Comparison();
                    }

                    if (timeCount.Countdown % 8 == 3)
                        Media.Play("Base\\Media\\请抬头直视摄像头.mp3");
                }
                else
                {
                    Content = new HomePage("超时自动返回");
                    Pages();
                }

            });
        }


        private void Pages()
        {
            File.Delete(paths);
            File.Delete(paths_black);

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));

            AmLivingBodyApi.AmStopCapture();
            AmLivingBodyApi.AmCloseDevice(); 
        }
        private void SwitchPage()
        {
            switch (Global.PageType)
            {
                default:
                    Content = new FunctionPage();
                    Pages();
                    break;
            }
        }

    }
}
