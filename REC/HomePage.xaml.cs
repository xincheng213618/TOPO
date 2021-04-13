using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BaseDLL;
using BaseUtil;

namespace REC
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        public HomePage(string Msg)
        {
            InitializeComponent();
            PopAlert(Msg, 6);
        }
        private Timer timer;

        private void Page_Initialized(object sender, EventArgs e)
        {
            //要增加一个全局参数，作为整体流程的参数，参数变动标动流程变动
            Global.Related.Initialized();
            Global.PageType = null;
            //读取内容
            string text = System.IO.File.ReadAllText("Base\\打证须知.txt");
            DisclaimerContent.Text = text;
            VbarAll.Backlight(false);

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);

        }
        private void TimeRun()
        {
            MainWindow.WindowsData.Status1 = "翻页：" + ESerialPort.CheckDeviceCode[ESerialPort.CheckDevice1()];
            MainWindow.WindowsData.Status2 = "盖章：" + ESerialPort.CheckDeviceCode[ESerialPort.CheckDevice2()];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //这里是测试开放的部分
            //Content = new FunctionPage1(new IDCardData() { Name = "", IDCardNo = "" }, "2");
            //Pages();
            //return;

            if (MainWindow.WindowsData.Status1 == "翻页：正常" && MainWindow.WindowsData.Status2 == "盖章：正常")
            {
                Content = new IDCardPage();
                Pages();
            }
            else if (MainWindow.WindowsData.Status1 == "翻页：缺证")
            {
                PopAlert("请联系工作人员放置证书", 3); 
            }
            else
            {
                PopAlert("机器故障，请联系工作人员", 3);
            }

        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }

        private async void PopAlert(string Msg, int time)
        {
            Log.Write("HomePagePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;

        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
            timer.Dispose();
        }
    }
}
