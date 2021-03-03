using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseUtil;
using BaseDLL;


namespace PEC
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
            PopAlert(Msg, 3);
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            Global.PageType = null;
        }
        private DispatcherTimer pageTimer = null;
        int Countdown = 0;
        int Imagepath = 1;
        private async void PopAlert(string Msg, int time)
        {
            Log.Write("HomePagePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Provincial":
                    Global.PageType = "Provincial";
                    Content = new LoginPage();
                    Pages();
                    break;
                case "ProvincialLYG"://连云港要求替换省信用部分代码
                    Global.PageType = "ProvincialLYG";
                    Content = new HomePage((string)button.Tag);
                    Pages();
                    break;
                case "QRCode":
                    Global.PageType = "QRCode";
                    Content = new QRCode();
                    Pages();
                    break;
                case "ProvincialPeople"://省信用
                    Global.PageType = "ProvincialPeople";
                    IDCardData iDCardData = new IDCardData() { Name = "陈信成", IDCardNo = "222222" };
                    Content = new ReportPage(iDCardData, @"E:\仓库\PEC\bin\Debug\1.pdf");
                    Pages();
                    break;
            }
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private void Countdown_timer()
        {
            this.DataContext = this;
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                Countdown = Countdown + 1;
                if (Countdown > 3600)
                {
                    Countdown = 1;
                }
                if (Countdown % 10 == 0)
                {
                    if (Imagepath == 1)
                    {
                        imageLogout1.Visibility = Visibility.Visible;
                        imageLogout2.Visibility = Visibility.Hidden;
                        imageLogout3.Visibility = Visibility.Hidden;
                        Imagepath = 2;
                        return;
                    }
                    if (Imagepath == 2)
                    {
                        imageLogout2.Visibility = Visibility.Visible;
                        imageLogout3.Visibility = Visibility.Hidden;
                        Imagepath = 3;
                        return;
                    }
                    if (Imagepath == 3)
                    {
                        imageLogout3.Visibility = Visibility.Visible;
                        Imagepath = 1;
                        return;
                    }
                }
            });
        }
    }
}
