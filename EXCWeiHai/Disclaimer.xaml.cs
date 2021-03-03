using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace EXC
{
    //Designed By Mr.Xin  2020.8.7
    /// <summary>
    /// Disclaimer.xaml 的交互逻辑
    /// </summary>
    public partial class Disclaimer : Page
    {
        public Disclaimer()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            DisclaimerContent.Text = System.IO.File.ReadAllText("Base\\温馨提示.txt");
            DataContext = Time;
            Time.Countdown = 20;
            Countdown_timer();
        }

        private void Home_Clcik(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public  TimeCount  Time = new TimeCount();

        private void Countdown_timer()
        {
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
        //同意之后   进行转跳
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (Global.PageType)
            {
                case "ReportGRWeiHai":
                    IDCardData idcarddata = new IDCardData()
                    {
                        Name = "毕明宇",
                        IDCardNo = "371081198706050050"
                    };

                    Content = new VersionPage(idcarddata);
                    //  Content = new IDCardPage();
                    Pages();

                    break;
                case "ReportWeiHai":
                    IDCardData iDCardData = new IDCardData()
                    {
                        Name = "胡洪珂",
                        IDCardNo = "411327200103063136"
                    };
                    Content = new SearchPage(iDCardData);

                    //Content = new IDCardPage();
                    Pages();
                    break;

            };

        }
    }
}
 