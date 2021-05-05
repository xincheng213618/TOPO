using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleWindow
{
    /// <summary>
    /// SimpleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleWindows : Window
    {
        public SimpleWindows()
        {
            InitializeComponent();
        }

        Timer TimerUpdate;
        Timer timer;
        /// <summary>
        /// 刷新时间 /毫秒
        /// </summary>
        public int RefreshTimer = 1000;

        public SimpleWindows(int Screens)
        {
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[Screens];
            InitializeComponent();
            Left = screen.WorkingArea.Left;
            Top = screen.WorkingArea.Top;
            Height = screen.WorkingArea.Height;
            Width = screen.WorkingArea.Width;
        }
        public SimpleWindows(int Screens, int RefreshTimer)
        {
            this.RefreshTimer = RefreshTimer;
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[Screens];
            InitializeComponent();
            Left = screen.WorkingArea.Left;
            Top = screen.WorkingArea.Top;
            Height = screen.WorkingArea.Height;
            Width = screen.WorkingArea.Width;
        }



        private void Window_Initialized(object sender, EventArgs e)
        {
            if (!Directory.Exists("BackGround"))
                Directory.CreateDirectory("BackGround");
            TimerUpdate = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => Updated())), null, 0, RefreshTimer);//本来是60，不过没必要刷新这么快，就1s1次就好
        }

        public string VideoFileName;
        public string PictureFileName;

        private BitmapImage bmImg;
        public void Updated()
        {
            if (timer != null)
                timer.Dispose();
            if (!BackgroundItem.Video.IsPlayer)
                MediaPlayer.Close();

            if (BackgroundItem.Kind)
            {
                if (BackgroundItem.Video.Files.Length != 0)
                {
                    if (!BackgroundItem.Video.IsPlayer)
                    {
                        VideoFileName = BackgroundItem.Video.Files[BackgroundItem.Video.VideoNum];
                        MediaPlayer.Source = new Uri(VideoFileName);
                        MediaPlayer.Play();
                        BackgroundItem.Video.IsPlayer = !BackgroundItem.Video.IsPlayer;
                    }
                }
            }
            else
            {
                if (BackgroundItem.Picture.Files.Length != 0)
                {
                    if (!BackgroundItem.Picture.Auto)
                    {
                        PictureFileName = BackgroundItem.Picture.Files[BackgroundItem.Picture.PictureNum];
                        bmImg = new BitmapImage(new Uri(PictureFileName))
                        {
                            CacheOption = BitmapCacheOption.None
                        };
                        ImageShow.Source = bmImg;
                    }
                    else
                    {
                        timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 60 - DateTime.Now.Second, BackgroundItem.Picture.Intervaltime); //切换
                    }

                }
            }
        }


        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundItem.Video.VideoNum++;
            VideoFileName = BackgroundItem.Video.Files[Math.Abs(BackgroundItem.Video.VideoNum % BackgroundItem.Video.Files.Length)];

            MediaPlayer.Source = new Uri(VideoFileName);
            MediaPlayer.Play();
        }

        private void TimeRun()
        {
            BackgroundItem.Picture.PictureNum++;
            PictureFileName = BackgroundItem.Picture.Files[Math.Abs(BackgroundItem.Picture.PictureNum % BackgroundItem.Picture.Files.Length)];
            bmImg = new BitmapImage(new Uri(PictureFileName))
            {
                CacheOption = BitmapCacheOption.None
            };
            ImageShow.Source = bmImg;


        }





    }

    public static class BackgroundItem
    {
        public static int Screens = 1;
        /// <summary>
        /// true 为视频，false 为图片
        /// </summary>
        public static bool Kind = true;

        public static class Video
        {
            public static string[] Files = { };
            public static int VideoNum = 0;

            public static bool IsPlayer = false;
        }
        public static class Picture
        {
            public static string[] Files = { };
            public static int PictureNum = 0;

            public static bool Auto = false;
            public static int Intervaltime = 1000;
        }
    }

}
