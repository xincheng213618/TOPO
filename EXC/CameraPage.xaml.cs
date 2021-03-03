using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

//Designed By Mr.Xin 2020.4.27 重新优化代码结构,让整体代码的复合度更高
//Update By Mr.xin 2021.1.12  逻辑优化
namespace EXC
{
    /// <summary>
    /// CameraPage.xaml 的交互逻辑
    /// </summary>
    public partial class CameraPage : Page
    {
        int ret;
        private static bool ShootSucess = false;
        private static double b, c;

        private IDCardData idcardData;
        public CameraPage(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            formhost.Width = 900;
            formhost.Height = 675;
            AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 900, 675);
            AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
            ret = AmLivingBodyApi.AmOpenDevice();

            DataContext = Time;
            Countdown_timer();
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
        private void Comparison()
        {
            string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";
            string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";
            if (idcardData.PhotoFileName == null)
                return;
            b = AmLivingBodyApi.AmVerify(idcardData.PhotoFileName, paths);
            c = AmLivingBodyApi.AmVerify(idcardData.PhotoFileName, paths_black);

            ShootSucess = false;

            Log.Write("人脸比对相似度:"+ Environment.NewLine + b.ToString() + Environment.NewLine + c.ToString());
           
            if (b > 0.7 && c > 0.7)
            {
              
                SwitchPage();
            }
            else
            {
                tryCount += 1;
            }
            if (tryCount > Global.configData.CameraTryCount)
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
            switch (Global.PageType)
            {
                case "ReportNanJing":
                case "ReportGRNanJing":
                case "ReportGRNingYang":
                case "ReportNingYang":
                case "ReportNingYangAll":
                case "ReportXinTai":
                case "ReportGRXinTai":
                case "ReportGRWeiFang":
                case "ReportHuangShi":
                case "ReportHeFei":
                case "ReportHeFei1":
                case "ReportGRHeFei":
                case "YiXingPerson":
                case "YiXingBanch":
                    IDCardInfo.Name = idcardData.Name;
                    Content = new Report(idcardData);
                    break;

                default:    
                    Content = new HomePage("没有配置进入页面,人脸对比成功");
                    break;
            }
            Pages();
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
                if (ret == 0)
                {
                    if (--Time.Countdown >= 0)
                    {

                        if (ShootSucess)
                            Comparison();
                        else
                            CapturePhoto();

                        if (Time.Countdown % 10 == 0)
                            Media.Player(4);
                    }
                    else
                    {
                        Content = new HomePage("超时自动返回");
                        Pages();
                    }
                }
                else
                {
                    Content = new HomePage("摄像头打开错误: " + AmLivingBodyApi.Ecode[ret]);
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

            IDcard.DeleteIDcardImages(idcardData);

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));

            AmLivingBodyApi.AmStopCapture();
            AmLivingBodyApi.AmCloseDevice();
        }
    }

}
