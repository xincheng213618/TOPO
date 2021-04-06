using BaseUtil;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ECRService
{
    /// <summary>
    /// PrintIndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class PrintIndexPage : Page
    {
        public PrintIndexPage()
        {
            InitializeComponent();
        }

        private DispatcherTimer pageTimer = null;
        private MainWidownData mainWidownData = new MainWidownData() { Countdown = 30 };

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
        }

        private void Countdown_timer()
        {

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (mainWidownData.Countdown % 15 == 0)
                    Media.Player(1);

                if (--mainWidownData.Countdown <= 0)
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


        //信用报告查询
        private void CompanyCreditReport_Click(object sender, RoutedEventArgs e)
        {
            Global.PageType = "CompanyCreditReporrt";
            Content = new IDCardPage();
            Pages();
            //pageTimer.IsEnabled = false;
            //ServiceData.type = 1;
            //Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new IDCardPage())));
           // Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new ReportListPage())));
        }
        //检疫检验报告查询
        private void QuarantineCreditReport_Click(object sender, RoutedEventArgs e)
        {
            Global.PageType = "QuarantineCreditReport";
            Content = new IDCardPage();
            Pages();

            //pageTimer.IsEnabled = false;
            //ServiceData.type = 2;
            //Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new IDCardPage())));
            // Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new QuarantineCreditQueryPage())));
        }


        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }


    }
}
