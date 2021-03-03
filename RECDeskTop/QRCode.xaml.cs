using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace REC
{
    /// <summary>
    /// QRCode.xaml 的交互逻辑
    /// </summary>
    public partial class QRCode : Page
    {
        IDCardData iDCardData;
        public QRCode(IDCardData iDCardData)
        {
            iDCardData = this.iDCardData;
            InitializeComponent();
        }

        private Thread DecodeThread = null;
        private bool bIsLoop = true;

        private string Msg = null; //存储扫描到的数据

        private void Page_Initialized(object sender, EventArgs e)
        {
            TopGrid.DataContext = timeCount;
            Countdown();
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

        private TimeCount timeCount = new TimeCount() { Countdown = 20 };

        private DispatcherTimer pageTimer = null;

        private void Countdown()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown <= 0)
                {
                    Content = new FunctionPage(iDCardData);
                    Pages();
                }
            });
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
            timeCount.Countdown = 20;
            ShowText.Text = Msg.TrimEnd('\0');//有趣的地方
            FunctionButton.Tag = "Search";
            ButtonLabel.Content = "查    询";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {     
                case "HomePage":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Return":
                    Content = new FunctionPage(iDCardData);
                    Pages();
                    break;
                case "Search":
                    string Text;
                    if (DetectMsg(out Text))
                    {
                        Content = new FunctionPage1(iDCardData ,Text);
                        Pages();
                    }
                    else
                    {
                        PopAlert("暂未支持此二维码，请检测二维码并重试", 3);
                        TitleLabel.Content = "您可重新扫描二维码";
                        FunctionButton.Tag = "Return";
                        ButtonLabel.Content = "返    回";
                    }
                    break;
            }
        }

        private bool DetectMsg(out string Text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");//读卡判断规则

            Text = ShowText.Text;//这里面有一个需要注意的地方
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
