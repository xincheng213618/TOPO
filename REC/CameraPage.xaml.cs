using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace REC
{
    public class CameraData
    {
        public int TryCount = 3;
        public string ImageName = Directory.GetCurrentDirectory() + $"\\capture.jpg";
        public string ImageName1 = Directory.GetCurrentDirectory() + $"\\capture_1.jpg";

        public bool ShootSucess = false;

        public double Similarity = 0;
        public double Similarity1 = 0;
    }


    /// <summary>
    /// CameraPage.xaml 的交互逻辑
    /// </summary>
    public partial class CameraPage : Page
    {
        public CameraPage() 
        {
            InitializeComponent();
        }

        private DispatcherTimer pageTimer = null;
        public RECDate timeCount = new RECDate() { Countdown = 20};

        private void Page_Initialized(object sender, EventArgs e)
        {
            AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 900, 675);
            AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
            AmLivingBodyApi.AmCaptureImage(Global.Related.CameraData.ImageName , 20000);
            TopGrid.DataContext = timeCount;
            Countdown();
        }


        AmLivingBodyApi.AmCaptureImageProc capture_image_callback = new AmLivingBodyApi.AmCaptureImageProc(OnCaptureImage);
        private unsafe static void OnCaptureImage(int code, string image, IntPtr data)
        {
            Global.Related.CameraData.ShootSucess = code == 0;
        }

        private void Comparison()
        {
            if (Global.Related.IDCardData.PhotoFileName != null)
            {
                Global.Related.CameraData.Similarity = AmLivingBodyApi.AmVerify(Global.Related.IDCardData.PhotoFileName, Global.Related.CameraData.ImageName);
                Global.Related.CameraData.Similarity1 = AmLivingBodyApi.AmVerify(Global.Related.IDCardData.PhotoFileName, Global.Related.CameraData.ImageName1);

                if (Global.Related.CameraData.Similarity > 0.7)
                {
                    Requests.Camer_Upload(Global.Related.IDCardData.IDCardNo, Global.Related.IDCardData.Name, Global.Related.CameraData.Similarity.ToString(), Global.Related.CameraData.ImageName, Global.Related.IDCardData.PhotoFileName);
                    SwitchPage();
                }
                else if (Global.Related.CameraData.Similarity < 0)
                {
                    Content = new HomePage("摄像头报错："+ Environment.NewLine + Global.Related.CameraData.Similarity.ToString());
                    Pages();
                }
                else
                {
                    Global.Related.CameraData.ShootSucess = false;
                    AmLivingBodyApi.AmCaptureImage(Global.Related.CameraData.ImageName, 20000);
                    Global.Related.CameraData.TryCount += 1;
                }

                if (Global.Related.CameraData.TryCount > 2)//在这里设置错误验证次数
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
                    if (Global.Related.CameraData.ShootSucess)
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
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));

            AmLivingBodyApi.AmStopCapture();
            AmLivingBodyApi.AmCloseDevice(); 
        }

        private void SwitchPage()
        {
            switch (Global.Related.PageType)
            {
                default:
                    Content = new FunctionPage();
                    Pages();
                    break;
            }
        }

    }
}
