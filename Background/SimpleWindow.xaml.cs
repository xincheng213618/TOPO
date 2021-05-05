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
        Timer TimerUpdate;
        Timer timer;
        /// <summary>
        /// 刷新时间 /毫秒
        /// </summary>
        public int RefreshTimer = 1000;
        BackGround backGround;

        public SimpleWindows(int Screens, string FileName,string Kinds)
        {
            backGround = new BackGround()
            {
                FileName = FileName,
                Kinds =Kinds,
            };

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[Screens];
            InitializeComponent();
            Left = screen.WorkingArea.Left;
            Top = screen.WorkingArea.Top;
            Height = screen.WorkingArea.Height;
            Width = screen.WorkingArea.Width;

            if (backGround.Kinds == "Video")
            {
                MediaPlayer.Source = new Uri(backGround.FileName);
                MediaPlayer.Play();
            }
            else if (backGround.Kinds == "picture")
            {
                ImageShow.Source= new BitmapImage(new Uri(backGround.FileName))
                {
                    CacheOption = BitmapCacheOption.None
                };
                timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PictureAuto())), null, 0, backGround.Intervaltime); //切换
            }
        }

        public SimpleWindows(int Screens, string Folder, string Kinds, int Intervaltime)
        {
            backGround = new BackGround()
            {
                Files = Directory.GetDirectories(Folder),
                Kinds = Kinds,
            };

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[Screens];
            InitializeComponent();
            Left = screen.WorkingArea.Left;
            Top = screen.WorkingArea.Top;
            Height = screen.WorkingArea.Height;
            Width = screen.WorkingArea.Width;


            if (backGround.Kinds == "Video")
            {
                MediaPlayer.Source = new Uri(backGround.FileName);
                MediaPlayer.Play();
            }
            else if (backGround.Kinds == "picture")
            {
                ImageShow.Source = new BitmapImage(new Uri(backGround.FileName))
                {
                    CacheOption = BitmapCacheOption.None
                };
            }
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
            TimerUpdate = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => Updated())), null, 0, RefreshTimer);//本来是60，不过没必要刷新这么快，就1s1次就好
        }

        public string VideoFileName;
        public string PictureFileName;

        public void Updated()
        {

        }


        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (backGround.Kinds == "Video")
            {
                MediaPlayer.Play();
            }
            else if (backGround.Kinds =="videos")
            {
                MediaPlayer.Source = new Uri(backGround.Files[backGround.VideoNum++]);
            }
        }

        private void PictureAuto()
        {
            ImageShow.Source = new BitmapImage(new Uri(backGround.Files[Math.Abs(backGround.PictureNum++ % backGround.Files.Length)]))
            {
                CacheOption = BitmapCacheOption.None
            }; 
        }
    }

    public class BackGround
    {
        public string FileName;
        public string[] Files = { };


        public string Kinds;

        public bool VideoAuto = false;

        public int VideoNum = 0;

        public int PictureNum = 0;
        public bool PictureAuto = true;
        public int Intervaltime = 1000;


    }

}
