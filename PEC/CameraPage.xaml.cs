using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

//Designed By Mr.Xin 2020.4.27 重新优化代码结构,让整体代码的复合度更高
//Update By Mr.xin 2021.1.12  逻辑优化
namespace PEC
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
            formhost.Width = 900;
            formhost.Height = 675;
            AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 900, 675);
            AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
            AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 30000);

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

            Log.Write("人脸比对相似度:"+ Environment.NewLine + b.ToString() + Environment.NewLine + c.ToString());
           
            if (b > 0.7 && c > 0.7)
            {
                SwitchPage();
            }
            else
            {
                AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 30000);
                tryCount += 1;
            }
            if (tryCount >  3)
            {
                Content = new HomePage("人脸对比失败，请重试");
                Pages();
            }
        }

        //跳转
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void SwitchPage()
        { 
            switch (Global.Related.PageType)
            {
                default:
                    Content = new ReportPage(Global.Related.IDCardData);
                    Pages();
                    break;
            }
        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;

        public TimeCount Time = new TimeCount();
        private void Countdown_timer()
        {
            Time.Countdown = 30;
            pageTimer = new DispatcherTimer(){
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
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

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));

            AmLivingBodyApi.AmStopCapture();
        }
    }

}
