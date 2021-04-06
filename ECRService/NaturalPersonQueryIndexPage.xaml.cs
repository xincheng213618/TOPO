using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ECRService
{
    /// <summary>
    /// NaturalPersonQueryIndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPersonQueryIndexPage : Page
    {
        public NaturalPersonQueryIndexPage()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = MainWidownData;
            Countdown_timer();
        }
        private MainWidownData MainWidownData = new MainWidownData() { Countdown = 30 };
        private DispatcherTimer pageTimer = null;


        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--MainWidownData.Countdown <= 0)
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


    }
}
