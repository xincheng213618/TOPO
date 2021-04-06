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
using BaseUtil;

namespace ECRService
{
    /// <summary>
    /// QueryIndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class QueryIndexPage : Page
    {
        public QueryIndexPage()
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

        private void CompanyQueryIndex_Click(object sender, RoutedEventArgs e)
        {
            Content =  new LegalEntityQueryIndexPage();
            Pages();
        }


        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            switch (button.Tag)
            {
                case "Return":
                    Content = new HomePage();
                    Pages();
                    break;
                case "PersonalQuery":
                    Content = new NaturalPersonQueryIndexPage();
                    Pages();
                    break;
                case "CompanyQuery":
                    Content = new LegalEntityQueryIndexPage();
                    Pages();
                    break;
                default:
                    break;  
            }
        }
    }
}
