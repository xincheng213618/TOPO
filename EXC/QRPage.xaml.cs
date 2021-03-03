using BaseDLL;
using BaseUtil;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace EXC
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
        private string Msg;


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
            string[] temp = Msg.Split('$');
            if (temp.Length > 1)
            {
                ShowText.Text = Msg.Split('$')[0];

            }
            else
            {
                ShowText.Text = Msg;
            }

            inputbutton.Content = "确  定";
            inputbutton.Tag = "Search";
            TitleLabel.Content = "扫码成功";
        }

        private void DrawPDF(string CQZH)
        {
            string FileName = "Temp\\" + CQZH + ".pdf";
            bool sucess = PDF.DrawYiXing1_Bank(FileName, CQZH);
            Dispatcher.BeginInvoke(new Action(() => DrawSucess(sucess, FileName)));
        }

        private void DrawSucess(bool sucess,string FileName)
        {
            if (sucess)
            {
                WaitShow.Visibility = Visibility.Hidden;
                Content = new Pdfshow(FileName);
                Pages();
            }
            else
            {
                Content = new HomePage("暂无信息");
                Pages();
            }
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

        private void inputbutton_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;
            Button button = sender as Button;

            switch (button.Tag)
            {
                case "fangchan":
                    if (Textfangchan.Text.Length > 6)
                    {
                        string CQHD = Textfangchan.Text;
                        WaitShow.Visibility = Visibility.Visible;
                        Thread thread = new Thread(() => DrawPDF(CQHD))
                        {
                            IsBackground = true
                        };
                        thread.Start();
                    }
                    else
                    {
                        TitleLabel.Content = " 房产证号错误，请重新输入";
                        Textfangchan.Text = "";
                    }

                    break;
                case "Search":
                case "QRcode":
                    if (!(ShowText.Text[0]==('等')))
                    {
                        if (TitleLabel.Content.Equals("扫码器未连接"))
                        {
                            MessageBox.Show("扫码器未连接");
                        }
                        else
                        {
                            string CQHD = ShowText.Text;
                            WaitShow.Visibility = Visibility.Visible;
                            Log.Write(CQHD);
                            Thread thread1 = new Thread(() => DrawPDF(CQHD))
                            {
                                IsBackground = true
                            };
                            thread1.Start();
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


    }
}



