using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECRService
{
    /// <summary>
    /// NaturalPersonQueryIndexPage2.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPersonQueryIndexPage2 : Page
    {
        private const int TIMEOUT = 30;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        public NaturalPersonQueryIndexPage2()
        {
            InitializeComponent();

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }

                if (countdown % 15 == 0)
                {
                    media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/2、查询信息类型.mp3", UriKind.Absolute);
                    media.Position = TimeSpan.Zero;
                    //media.Play();
                }

                //timerLabel.Content = countdown.ToString();
            });
            pageTimer.IsEnabled = true;
        }

        private void FDCJJ_Click(object sender, RoutedEventArgs e)
        {
            /* 注册房地产经纪人 */
            ServiceData.type = 9;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonQueryPage())));
        }

        private void JLGC_Click(object sender, RoutedEventArgs e)
        {
            /* 监理工程师 */
            ServiceData.type = 10;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonQueryPage())));
        }

        private void XXXTGCJL_Click(object sender, RoutedEventArgs e)
        {
            /* 信息系统工程监理资质工程师 */
            ServiceData.type = 11;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonQueryPage())));
        }

        private void XTJCGJXMJL_Click(object sender, RoutedEventArgs e)
        {
            /* 计算机信息系统集成高级项目经理 */
            ServiceData.type = 12;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonQueryPage())));
        }

        private void XTJCXMJL_Click(object sender, RoutedEventArgs e)
        {
            /* 计算机信息系统集成项目经理 */
            ServiceData.type = 13;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonQueryPage())));
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonQueryIndexPage())));
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new QueryIndexPage())));
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/2、查询信息类型.mp3", UriKind.Absolute);
            media.Position = TimeSpan.Zero;
            //media.Play();
        }
    }
}
