using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace EXCYiXing
{
    /// <summary>
    /// SMPage.xaml 的交互逻辑
    /// </summary>
    public partial class SMPage : Page
    {
        public SMPage()
        {
            InitializeComponent();
            DataContext = Time;
            Countdown_timer();
            DecodeThread = new Thread(new ThreadStart(DecodeThreadMethod))
            {
                IsBackground = true
            };
            DecodeThread.Start();
        }
        string Msg;
        private void ShowMsg()
        {
            tb.Text = Msg;

        }
        private bool bIsLoop = true;
        private void DecodeThreadMethod()
        {

            do
            {
                bool decoderesult = VbarAll.GetResultStr(out Msg, out int size);
                if (decoderesult && Msg != "")
                {
                    VbarAll.BeepControl();
                    Dispatcher.BeginInvoke(new Action(() => ShowMsg()));
                 
                   
                }
            }
            while (bIsLoop);
        }
        private DispatcherTimer pageTimer = null;
        private Thread DecodeThread = null;
        public TimeCount Time = new TimeCount();
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private void Countdown_timer()
        {
            Time.Countdown = 60;
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tb.Text.Length<5)
            {
                return;
            }
            else
            {
                cqzh(tb.Text);
            }
        }
        private void cqzh(string m)
        {
            try
            {
                string FileName = "2.pdf";

                Dispatcher.BeginInvoke(new Action(() => {

                    WaitShow.Visibility = Visibility.Visible;
                }));
                if (PDF.DrawYiXing1_Bank(FileName, m))
                {
                    Dispatcher.BeginInvoke(new Action(() => {

                        WaitShow.Visibility = Visibility.Hidden;
                    }));

                    Dispatcher.BeginInvoke(new Action(() => PDFShow(FileName)));
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Content = new HomePage("暂无信息");
                        Pages();
                    }));

                }
            }
            catch
            {

                Dispatcher.BeginInvoke(new Action(() =>
                {

                    Content = new HomePage("暂无信息");
                    Pages();
                }));
            }
        }
        private void PDFShow(string FileName)
        {
            Content = new Pdfshow(FileName);
            Pages();
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Time.Countdown = 60;
            if (tb.Text.Length>0)
            {
                bIsLoop = false;
            }
            else
            {
                bIsLoop = true;
            }
                       
        }
    }
}
