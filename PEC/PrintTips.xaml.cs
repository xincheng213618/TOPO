using BaseUtil;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PEC
{
    /// <summary>
    /// PrintTips.xaml 的交互逻辑
    /// </summary>
    public partial class PrintTips : Page
    {
        public PrintTips()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            CountLabel.DataContext = timeCount;
            timeCount.Countdown = 11;
            Countdown();
        }

        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount();

        private void Countdown()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown <= 0)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    Pages();
                    break;
            }
        }
    }
}
