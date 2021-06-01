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
        Timer timer;
        /// <summary>
        /// 刷新时间 /毫秒
        /// </summary>
        public int RefreshTimer = 1000;
        BackGround backGround;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Screens">指定屏幕</param>
        public SimpleWindows()
        {
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[System.Windows.Forms.Screen.AllScreens.Count()-1];
            InitializeComponent();
            //工作区域
            //Left = screen.WorkingArea.Left;
            //Top = screen.WorkingArea.Top;
            //Height = screen.WorkingArea.Height;
            //Width = screen.WorkingArea.Width;
            Left = screen.Bounds.Left;
            Top = screen.Bounds.Top;
            Height = screen.Bounds.Height;
            Width = screen.Bounds.Width;
        }


        public void Update(string FileName,string Kinds)
        {
            backGround = new BackGround()
            {
                FileName = FileName,
                Kinds =Kinds,
            };
            if (backGround.Kinds == "video")
            {
                ImageShow.Source = null;
                MediaPlayer.Source = new Uri(backGround.FileName);
                MediaPlayer.Play();
            }
            else if (backGround.Kinds == "picture")
            {
                ImageShow.Source= new BitmapImage(new Uri(backGround.FileName))
                {
                    CacheOption = BitmapCacheOption.None
                };
            }
        }
        /// <summary>
        /// back
        /// </summary>
        /// <param name="Folder">路径</param>
        /// <param name="Kinds">类型</param>
        /// <param name="Intervaltime">幻灯片间隔时间</param>
        public void Update(string Folder, string Kinds, int Intervaltime)
        {
            backGround = new BackGround()
            {
                Files = Directory.GetFiles(Folder),
                Kinds = Kinds,
                Intervaltime= Intervaltime
            };

            if (backGround.Files.Length > 0)
            {
                if (timer!=null)
                    timer.Dispose();
                ImageShow.Source = null;

                if (backGround.Kinds == "videos")
                {
                    MediaPlayer.Source = new Uri(backGround.Files[0]);
                    MediaPlayer.Play();
                }
                else if (backGround.Kinds == "pictures")
                {
                    timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PictureAuto())), null, 0, backGround.Intervaltime); //切换
                }
            }

        }

        public void Update ( string[] Files, string Kinds, int Intervaltime)
        {
            backGround = new BackGround()
            {
                Files = Files,
                Kinds = Kinds,
                Intervaltime = Intervaltime
            };


            if (backGround.Files.Length > 0)
            {
                if (timer != null)
                    timer.Dispose();
                if (backGround.Kinds == "videos")
                {
                    ImageShow.Source = null;
                    MediaPlayer.Source = new Uri(backGround.Files[0]);
                    MediaPlayer.Play();
                }
                else if (backGround.Kinds == "pictures")
                {
                    timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PictureAuto())), null, 0, backGround.Intervaltime); //切换

                }
            }
        }





        private void Window_Initialized(object sender, EventArgs e)
        {
        }




        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (backGround.Kinds == "video")
            {
                MediaPlayer.Source = new Uri(backGround.FileName);
                MediaPlayer.Play();
            }
            else if (backGround.Kinds =="videos")
            {
                backGround.VideoNum++;
                MediaPlayer.Source = new Uri(backGround.Files[Math.Abs(backGround.VideoNum % backGround.Files.Length)]);
                MediaPlayer.Play();
            }
        }

        private void PictureAuto()
        {
            if (backGround.Files.Length > 0)
            {
                backGround.PictureNum++;
                ImageShow.Source = new BitmapImage(new Uri(backGround.Files[Math.Abs(backGround.PictureNum % backGround.Files.Length)]))
                {
                    CacheOption = BitmapCacheOption.None
                };
            }
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
