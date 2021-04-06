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
    /// NaturalPersonQueryIndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPersonQueryIndexPage : Page
    {
        private const int TIMEOUT = 30;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        public NaturalPersonQueryIndexPage()
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



        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));

        }

        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Return":
                    Content = new QueryIndexPage();
                    Pages();
                    break;
                case "Fun":
                    Content = new NaturalPersonQueryPage(button.TabIndex);
                    Pages();
                    break;




            }
        }

        private void Page_Initialized(object sender, EventArgs e)
        {

        }
    }
}
