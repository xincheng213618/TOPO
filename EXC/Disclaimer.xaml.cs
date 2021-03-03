using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            string text = System.IO.File.ReadAllText("温馨提示.txt");
            DisclaimerContent.Text = text;
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
        public Times Time = new Times();

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
        //同意之后进行转跳
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (Global.PageType)
            {
                case "ReportGRWeiHai":
                    Content = new IDCardPage();
                    break;
                case "ReportWeiHai":
                    Content = new IDCardPage();
                    break;

            };
            Pages();

        }
    }
}
 