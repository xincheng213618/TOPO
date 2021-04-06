using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ECRService
{
    /// <summary>
    /// PrinterStatusErrorPage.xaml 的交互逻辑
    /// </summary>
    public partial class PrinterStatusErrorPage : Page
    {
        private int cornerEvent = 0;
        private DispatcherTimer cornerEventTimer = null;
        private CancellationTokenSource workerTokenSource = null;

        public PrinterStatusErrorPage(string message)
        {
            workerTokenSource = new CancellationTokenSource();

            InitializeComponent();

            textBlock.Text = message;
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
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
                cornerEvent = 0;
                cornerEventTimer.IsEnabled = false;
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
                workerTokenSource.Cancel();

                MaintainData.returnPage = new HomePage() ;

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
