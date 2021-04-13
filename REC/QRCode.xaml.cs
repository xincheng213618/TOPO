using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        public QRCode()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数1
        /// </summary>
       

        private Thread DecodeThread = null;
        private bool bIsLoop = true;

        private string Msg = null; 
        private bool Sucess=false;

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
                    Content = new FunctionPage();
                    Pages();
                }
                if (timeCount.Countdown % 8 == 3 && !Sucess)
                    Media.Play("Base\\Media\\"+ "请将收件单上的二维码放在二维码扫描处扫描.mp3");
            });
        }


        /// <summary>
        ///  方法：扫码线程方法
        /// </summary>
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

        private BrushConverter Use1 = new BrushConverter();

        private void ShowMsg()
        {
            timeCount.Countdown = 20;
            ShowText.Text = Msg.TrimEnd('\0');//有趣的地方
            FunctionButton.Tag = "Search";
            FunctionBorder.Background = (Brush)Use1.ConvertFrom("#60d0ff");
            FunctionBorder.BorderBrush = Brushes.HotPink;

            ButtonLabel.Content = "查    询";
            Sucess = true;
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
                    Content = new FunctionPage();
                    Pages();
                    break;
                case "Search":
                    string Text;
                    if (DetectMsg(out Text))
                    {
                        Global.Related.transtionId = Text;
                        Content = new FunctionPage1();
                        Pages();
                    }
                    else
                    {
                        PopAlert("请放置正确的二维码，并重试", 3);
                        TitleLabel.Content = "您可重新扫描二维码";
                        //FunctionBorder.Background = bru
                        FunctionButton.Tag = "Return";
                        ButtonLabel.Content = "返    回";
                    }
                    break;
            }
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
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
