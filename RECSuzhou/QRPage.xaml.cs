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

namespace RECSuzhou
{
    /// <summary>
    /// QRPage.xaml 的交互逻辑
    /// </summary>
    public partial class QRPage : Page
    {
        public QRPage()
        {
            InitializeComponent();
        }
        private Thread DecodeThread = null;
        private bool bIsLoop = true;

        private string Msg = null; //存储扫描到的数据
        private void Page_Initialized(object sender, EventArgs e)
        {
            CoutLabel.DataContext = Time;
            Countdown_timer();
            if (VbarAll.Isconnected())
            {
                VbarAll.Backlight(true);
                DecodeThread = new Thread(new ThreadStart(DecodeThreadMethod))
                {
                    IsBackground = true
                };
                DecodeThread.Start();
            }
            else
            {
                TitleLabel.Content = "扫码器未连接";
            }
        }
        /// 方法：扫码线程方法
        private void DecodeThreadMethod()
        {
            do
            {
                bool decoderesult = VbarAll.GetResultStr(out Msg, out int size);
                if (decoderesult && Msg != "")
                {
                    VbarAll.BeepControl();
                    Dispatcher.BeginInvoke(new Action(() => ShowMsg()));
                    Thread.Sleep(1000);
                }
            }
            while (bIsLoop);
        }


        private void ShowMsg()
        {
            Time.Countdown = 30;
            ShowText.Text = Msg;
            Search.Tag = "Search";
            Search.BorderBrush = Brushes.HotPink;
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {                
                case "Tip":
                    PopAlert("请先扫描二维码",3);
                    break;
                case "Search":
                    string Text;
                    if (DetectMsg(out Text))
                    {
                        pageTimer.IsEnabled = false;  
                        Thread thread = new Thread(() => Requests(Text))
                        {
                            IsBackground = true
                        };
                        thread.Start();
                    }
                    else
                    {
                        PopAlert("请放置正确的二维码，并重试", 3);
                        TitleLabel.Content = "您可重新扫描二维码";
                        Search.Tag = "Tip";
                        Search.BorderBrush = Brushes.Gray;
                    }

                    break;

            }
        }
        private bool DetectMsg(out string Text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");//读卡判断规则

            Text = ShowText.Text;
            if (reg.IsMatch(Text))
            {
                return true;
            }
            else
            {
                Text = "";
                return false;
            }
        }
        private void Requests(string Text)
        {
           
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();


        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage("页面超时自动返回");
                    Pages();
                }
            });
        }
        private void Pages()
        {
            bIsLoop = false;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private async void PopAlert(string Msg, int time)
        {
            Log.Write("QRCodePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
    }
}
