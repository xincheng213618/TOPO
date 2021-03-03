using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Background
{
    //Designed By Mr.Xin 2020.7.10
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BackgroundWindow : Window
    {

        public IntPtr winHandle;// 当前窗体指针

        Timer timer;
        public BackgroundWindow()
        {
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[BackgroundItem.Screens];
            InitializeComponent();
            Left = screen.WorkingArea.Left;
            Top = screen.WorkingArea.Top;
            Height = screen.WorkingArea.Height;
            Width = screen.WorkingArea.Width;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            string FileName = "BackGround";
            if (!Directory.Exists(FileName))
                Directory.CreateDirectory(FileName);
        }

        public string VideoFileName;
        public string PictureFileName;

        private BitmapImage bmImg;
        public void Updated()
        {
            VideoGrid.Visibility = BackgroundItem.Kind ? Visibility.Visible : Visibility.Hidden;
            PictureGrid.Visibility = BackgroundItem.Kind ? Visibility.Hidden : Visibility.Visible;

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
        public static bool Kind = true; //true 为视频，false 为图片

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
