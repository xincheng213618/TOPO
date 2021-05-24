using EXCResources;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SingleVerification
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        private bool AutoRun = false;//自动模式
        public bool CameraRun = false;//是否处于摄像头的模式
        private static bool ShootSucess = false;//是否拍摄成功
        public static int read_success = -1;//身份证是否 读取成功

        IDCardData idcardData = new IDCardData();
        readonly Times Time = new Times();

        public static int m_iPort;

        public HomePage()
        {
            InitializeComponent();
        }


        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;

            Countdown_timer();
            pageTimer.IsEnabled = false;

            m_iPort = IDcard.IDcardSet();

            formhost.Width = 640;
            formhost.Height = 480;
            AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 640, 480);
            AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromTicks(500),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (++Time.Countdown % 10 == 0)
                    Media.Player(null, 4);

                if (!CameraRun)
                {
                    if (read_success == 1 || read_success == 0)
                    {
                        read_success = -1;
                        CameraRun = true;
                        AmLivingBodyApi.AmOpenDevice();
                    }
                    else
                    {
                        IDCardRun();
                    }
                }
                else
                {
                    if (ShootSucess)
                    {
                        pageTimer.IsEnabled = false;
                        CameraRun = false;
                        Comparison();
                    }
                    else
                        CapturePhoto();
                }
            });
        }
        private string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";
        private string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";
        private double b, c;

        private void Comparison()
        {
            ShootSucess = false;

            b = AmLivingBodyApi.AmVerify(idcardData.PhotoFileName, paths);
            c = AmLivingBodyApi.AmVerify(idcardData.PhotoFileName, paths_black);

            CSQLite.Insert.WriteIDCardData(idcardData, paths, paths_black, b);

            File.Delete(paths);
            File.Delete(paths_black);
            IDcard.DeleteIDcardImages(idcardData);

            TextIDCardNo.Text = "";
            TextName.Text = "";
            label.Content = b<0? $" {AmLivingBodyApi.Ecode[(int)b]}" : "人脸相似度" + b;

            if (b > 0.9 && c > 0.9)
            {
                Dispatcher.BeginInvoke(new Action(() => Alert(0, 4)));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() => Alert(1, 4)));
            }
        }

        private void CapturePhoto()
        {
            AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 3000);
        }
        AmLivingBodyApi.AmCaptureImageProc capture_image_callback = new AmLivingBodyApi.AmCaptureImageProc(OnCaptureImage);
        private unsafe static void OnCaptureImage(int code, string image, IntPtr data)
        {
            ShootSucess = code == 0;
        }




        private async void Alert(int code, double time)
        {
            Pass.Visibility = code == 0 ? Visibility.Visible : Visibility.Hidden;
            NotPass.Visibility = code == 1 ? Visibility.Visible : Visibility.Hidden;
            NotIDcard.Visibility = code == 0 || code == 1 ? Visibility.Hidden : Visibility.Visible;

            CameraBorder.Visibility = Visibility.Hidden;
            PopBorder.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(time));
            PopBorder.Visibility = Visibility.Hidden;
            CameraBorder.Visibility = Visibility.Visible;

            pageTimer.IsEnabled = AutoRun;
        }

        private void IDCardRun()
        {
            read_success = IDcard.IDcardRead(m_iPort, ref idcardData);

            if (read_success == 0 || read_success == 1)
            {
                TextName.Text = idcardData.Name.Substring(0);
                TextIDCardNo.Text = idcardData.IDCardNo.Substring(0);
                pageTimer.IsEnabled = true;
            }    
            else
            {
                if (!AutoRun)
                    Dispatcher.BeginInvoke(new Action(() => Alert(2, 3)));
            }
        }

        private void IDCard_Auto_click(object sender, RoutedEventArgs e)
        {
            AutoRun = !AutoRun;
            CameraRun = false;
            ShootSucess = false;
            pageTimer.IsEnabled = AutoRun;
            AutoRead.Content = AutoRun ? "停止" : "自动读卡";
        }

        private void IDCard_click(object sender, RoutedEventArgs e)
        {
            IDCardRun();
        }




        private void MySqlcDemo(object sender, RoutedEventArgs e)
        {
            SQLWindow sQLWindow = new SQLWindow();
            sQLWindow.Show();
        }
    }
}
