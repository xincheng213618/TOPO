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
    /// LegalEntityQueryIndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class LegalEntityQueryIndexPage : Page
    {
        private const int TIMEOUT = 30;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        public LegalEntityQueryIndexPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
        }
        private MainWidownData MainWidownData = new MainWidownData() { Countdown = 30 };

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--countdown <= 0)
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

        private void EnterpriseUnitQuery_Click(object sender, RoutedEventArgs e)
        {
            Content = new LegalEntityQueryPage(1);
            Pages();
        }

        private void IndividualBusinessQuery_Click(object sender, RoutedEventArgs e)
        {
            Content = new LegalEntityQueryPage(2);
            Pages();
        }

        private void NonEnterpriseQuery_Click(object sender, RoutedEventArgs e)
        {
            Content = new LegalEntityQueryPage(3);
            Pages();
        }

        private void SocialGroupQuery_Click(object sender, RoutedEventArgs e)
        {
            Content = new LegalEntityQueryPage(4);
            Pages();
        }

        private void PublicInstitutionQuery_Click(object sender, RoutedEventArgs e)
        {
            Content = new LegalEntityQueryPage(5);
            Pages();
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
            }
        }
    }
}
