using BaseDLL;
using BaseUtil;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace XinHua
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Top.DataContext = Time;
            Countdown_timer();
        }

        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();
        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }

        //页面转跳
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }



     
       

        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Foreground = Brushes.White;



            switch ((string)btn.Tag)
            {
                case "Function":
                    FunctionScrollViewer.Visibility = Visibility.Visible;
                    PageScrollViewer.Visibility = Visibility.Hidden;
                    TestScrollViewer.Visibility = Visibility.Hidden;
                    btn.FontWeight = FontWeights.Bold;
                    btn.Foreground = Brushes.HotPink;
                    break;
                case "Page":
                    FunctionScrollViewer.Visibility = Visibility.Hidden;
                    PageScrollViewer.Visibility = Visibility.Visible;
                    TestScrollViewer.Visibility = Visibility.Hidden;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                case "Test":
                    FunctionScrollViewer.Visibility = Visibility.Hidden;
                    PageScrollViewer.Visibility = Visibility.Hidden;
                    TestScrollViewer.Visibility = Visibility.Visible;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {

                case "Close":
                    Environment.Exit(0);
                    break;
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;

                default:
                    break;
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
               
                   
            }
        }

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {            
                case "PDF":
                    Content = new Pdfshow();
                    break;
                default:
                    break;
            }
            Pages();
        }
    }
}
