using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace EXCYiXing
{
    /// <summary>
    /// QRPage.xaml 的交互逻辑
    /// </summary>
    public partial class YiXingNew : Page
    {
        public YiXingNew()
        {
            InitializeComponent();
        }

        public TimeCount Time = new TimeCount();

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Countdown_timer();
        }
        private DispatcherTimer pageTimer = null;
        private bool bIsLoop = true;
        private Thread DecodeThread = null;
        string Msg;


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
                    cqzh(Msg);
                    Thread.Sleep(1000);
                }
            }
            while (bIsLoop);
        }
        private void ShowMsg()
        {
            ShowText.Text = Msg;
            inputbutton.Content = "确  定";
            TitleLabel.Content = "扫码成功";

        }

        //倒计时模块
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
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }




        private string CQHD = "";
        private void cqzh()
        {
            try
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    WaitShow.Visibility = Visibility.Visible;
                }));
                string FileName = "1.pdf";
                if (PDF.DrawYiXing1_Bank(FileName, CQHD))
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
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
        private void inputbutton_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "fangchan":
                    //  if (Textfangchan.Text.Length > 6)
                    if (Textfangchan.Text.Length > 6)
                    {
                        CQHD = Textfangchan.Text;
                      

                        Thread worker22 = new Thread(() => cqzh())
                        {
                            IsBackground = true
                        };
                        worker22.Start();
                    }
                    else
                    {
                        TitleLabel.Content = " 房产证号错误，请重新输入";
                        Textfangchan.Text = "";
                    }

                    break;

                case "QRcode":

                    if (TitleLabel.Content.Equals("扫码器未连接"))
                    {
                        //扫码器未连接  不处理点击
                    }
                    else
                    {
                        if (Msg.Length < 2)
                        {
                            TitleLabel.Content = "未扫描到信息，请重试";
                        }
                        else
                        {

                            CQHD = Textfangchan.Text;
                            Thread worker22 = new Thread(() => cqzh(Msg))
                            {
                                IsBackground = true
                            };
                            worker22.Start();
                        }
                    }
                    break;
            }
        }


        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;

            Button button = sender as Button;
            switch (button.Tag)
            {
                case "input":

                    input.Visibility = Visibility.Hidden;
                    QRcode.Visibility = Visibility.Visible;
                    button.Tag = "QRcode";
                    ButtonChange.Content = "输  入";
                    inputbutton.Tag = "QRcode";
                    inputbutton.IsEnabled = false;
                    TitleLabel.Content = "请等待";
                    inputbutton.Content = "确  定";
                    bIsLoop = true;
                    ShowText.Text = "等待自助机右下方扫码器提示灯点亮后，请将预约的二维码放在扫码器上方";
                    if (VbarAll.Isconnected())
                    {
                        TitleLabel.Content = "请扫描二维码";
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
                    break;
                case "QRcode":
                    ButtonChange.Content = "扫  码";
                    TitleLabel.Content = "请输入房产证号";
                    QRcode.Visibility = Visibility.Hidden;
                    input.Visibility = Visibility.Visible;
                    Dockfangchan.Visibility = Visibility.Visible;
                    button.Tag = "input";
                    inputbutton.Tag = "fangchan";
                    bIsLoop = false;

                    break;
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;
            }
        }

        private void Textfangchan_TextChanged(object sender, TextChangedEventArgs e)
        {
            Time.Countdown = 59;
        }
    }
}



