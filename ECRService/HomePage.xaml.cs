using System;
using System.IO;
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
        private int cornerEvent = 0;
        private DispatcherTimer cornerEventTimer = null;

        public HomePage()
        {
            InitializeComponent();
        }

        public HomePage(string Msg)
        {
            InitializeComponent();
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
        
    }
}
