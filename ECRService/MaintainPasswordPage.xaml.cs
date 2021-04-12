using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ECRService
{
    /// <summary>
    /// MaintainPasswordPage.xaml 的交互逻辑
    /// </summary>
    public partial class MaintainPasswordPage : Page
    {
        private int tryCount = 0;
        private DispatcherTimer pageTimer = null;

        public MaintainPasswordPage()
        {
            InitializeComponent();

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(60),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                pageTimer.IsEnabled = false;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    (Application.Current.MainWindow as MainWindow).frame.Navigate(MaintainData.returnPage);
                    MaintainData.returnPage = null;
                }));
            });
            pageTimer.IsEnabled = true;
        }

        private void Authenticate()
        {
            tryCount++;

            // 检查密码是否相符
           if (passwordBox.Password == /*ConfigData.Decrypt*/(Global.Config.AdminPassword))
            {
                tryCount = 0;
                MaintainData.returnPage = null;
                // 最小化窗口
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
                pageTimer.IsEnabled = false;
                // 维护完成
                Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new MaintainCompletedPage())));
            }
            else
            {
                if (tryCount >= 3)
                {
                    tryCount = 0;

                    MaintainData.maintainLock = true;
                    MaintainData.maintainLockTimer = new Timer( _ =>
                    {
                        MaintainData.maintainLock = false;
                        MaintainData.maintainLockTimer.Dispose();
                        MaintainData.maintainLockTimer = null;
                    }, null, 180000, Timeout.Infinite);

                    pageTimer.IsEnabled = false;

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        (Application.Current.MainWindow as MainWindow).frame.Navigate(MaintainData.returnPage);
                        MaintainData.returnPage = null;
                    }));
                }
            }
        }

        private void Password_Click(object sender, RoutedEventArgs e)
        {
            Authenticate();
            passwordBox.Password = "";
        }
    }
}
