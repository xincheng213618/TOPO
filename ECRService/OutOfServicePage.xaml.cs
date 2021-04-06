using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// OutOfServicePage.xaml 的交互逻辑
    /// </summary>
    public partial class OutOfServicePage : Page
    {
        private int cornerEvent;
        private DispatcherTimer cornerEventTimer;

        public OutOfServicePage(string message)
        {
            InitializeComponent();

            InitializeCornerEvent();

            ServiceData.Initialize();

            textBlock.Text = message;
        }

        private void InitializeCornerEvent()
        {
            cornerEvent = 0;
            cornerEventTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(10),
            };

            cornerEventTimer.Tick += new EventHandler((sender, e) =>
            {
                cornerEventTimer.IsEnabled = false;
                cornerEvent = 0;
            });
        }

        private void LeftTopCorner_Click(object sender, RoutedEventArgs e)
        {
            cornerEvent = 1;
            if (!cornerEventTimer.IsEnabled)
            {
                cornerEventTimer.IsEnabled = true;
            }
        }

        private void RightTopCorner_Click(object sender, RoutedEventArgs e)
        {
            if (cornerEvent == 1)
                cornerEvent = 3;
            else
                cornerEvent = 0;
        }

        private void LeftBottom_Click(object sender, RoutedEventArgs e)
        {
            if (cornerEvent == 7 && !MaintainData.maintainLock)
            {
                cornerEventTimer.IsEnabled = false;
                cornerEvent = 0;

                MaintainData.returnPage = this;

                // 启动管理流程
                Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new MaintainPasswordPage())));
            }
            else
                cornerEvent = 0;
        }

        private void RightBottom_Click(object sender, RoutedEventArgs e)
        {
            if (cornerEvent == 3)
                cornerEvent = 7;
            else
                cornerEvent = 0;
        }
    }
}
