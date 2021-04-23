using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseUtil;
using BaseDLL;
using System.Printing;
using System.Collections.Generic;

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

        private DispatcherTimer pageTimer = null;
        private void Page_Initialized(object sender, EventArgs e)
        {
            Global.Related.Initialized();
            //图片轮换的另一种简单的写法
            int Countdown = 0;
            List<Image> images = new List<Image> { imageLogout1, imageLogout2, imageLogout3 };
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(10)};
            pageTimer.Tick += new EventHandler((sender1, e1) => { Panel.SetZIndex(images[Countdown % 3], Countdown += 1); }); 
        }
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
                    Global.Related.PageType = "Provincial";
                    Content = new LoginPage();
                    Pages();
                    break;
                case "ProvincialLYG"://连云港要求替换省信用部分代码
                    Global.Related.PageType = "ProvincialLYG";
                    Content = new HomePage((string)button.Tag);
                    Pages();
                    break;
                case "QRCode":
                    Global.Related.PageType = "QRCode";
                    Content = new QRCode();
                    Pages();
                    break;
                case "ProvincialPeople"://省信用
                    Global.Related.PageType = "ProvincialPeople";
                    Content = new IDCardPage();
                    Pages();
                    break;
            }
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

    }
}
