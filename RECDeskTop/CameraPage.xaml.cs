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
        IDCardData iDCardData = new IDCardData();

        public CameraPage(IDCardData iDCardData) 
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
        }


        private DispatcherTimer pageTimer = null;
        public RECDate timeCount = new RECDate() { Countdown =20};

        private void Page_Initialized(object sender, EventArgs e)
        {
            int ret;
            formhost.Width = 900;
            formhost.Height = 675;
            AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 900, 675);
            AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
            ret = AmLivingBodyApi.AmOpenDevice();
            if (ret != 0)
            {
                Dispatcher.BeginInvoke(new Action(() => Msg("摄像头打开错误:" + AmLivingBodyApi.Ecode[ret])));
            }
            TopGrid.DataContext = timeCount;
            Countdown();

        }

        // 人脸识别调用核心代码
        private void CapturePhoto()
        {
            int ret;
            string Paths = Directory.GetCurrentDirectory() + $"\\capture.jpg";
            ret = AmLivingBodyApi.AmCaptureImage(Paths, 3000);
        }

        AmLivingBodyApi.AmCaptureImageProc capture_image_callback = new AmLivingBodyApi.AmCaptureImageProc(OnCaptureImage);
        private unsafe static void OnCaptureImage(int code, string image, IntPtr data)
        {
            ShootSucess = code == 0 ? true : false;
            if (ShootSucess)
                Media.Player(0);
        }



        //对比逻辑
        private int tryCount = 0;

        string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";
        string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";

        private void Comparison()
        {
            if (iDCardData.PhotoFileName != null)
            {
                b = AmLivingBodyApi.AmVerify(iDCardData.PhotoFileName, paths);
                c = AmLivingBodyApi.AmVerify(iDCardData.PhotoFileName, paths_black);

                ShootSucess = false;

                if (b > 0.7 && c > 0.7)
                {
                    SwitchPage();
                }
                else if (b < 0)
                {
                    string msg = "";
                    try
                    {
                        msg = (string)AmLivingBodyApi.Ecode[(int)b];
                    }
                    catch { }
                    Content = new HomePage("摄像头报错："+ Environment.NewLine + b+Environment.NewLine + msg);
                    Pages();
                }
                else
                {
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
                if (--timeCount.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
                if (ShootSucess)
                    Comparison();
                else
                    CapturePhoto();

                if (timeCount.Countdown % 10 == 0)
                    Media.Player(0);

            });
        }
        private void Msg(string msg)
        {
            Content = new HomePage(msg);
            Pages();
        }

        private void Pages()
        {
            //CSQLite.Insert.WriteIDCardData(idcardData, paths, paths_black, b); //持久化保存比对信息，不做处理
            File.Delete(paths);
            File.Delete(paths_black);
            IDcard.DeleteIDcardImages(iDCardData);

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));

            AmLivingBodyApi.AmStopCapture();
            AmLivingBodyApi.AmCloseDevice();
        }
        private void SwitchPage()
        {
            switch (Global.PageType)
            {
                case "Self":
                    //CSQLite.Insert.WriteIDCardData(idcardData);
                    Content = new FunctionPage(iDCardData);
                    break;
                default:

                    break;
            }
            Pages();
        }

    }
}
