using BaseUtil;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

/**
 主页面
 **/
namespace ECRService
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
            PopAlert(Msg, 3);//吉林 原本为3
        }

        private void Page_Initialized(object sender, EventArgs e)
        {

        }

        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Print":
                    Content = new PrintIndexPage();
                    Pages();
                    break;
                case "Search":
                    Content = new QueryIndexPage();
                    Pages();
                    break;

                default:
                    break;
            }
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
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
    }
}
