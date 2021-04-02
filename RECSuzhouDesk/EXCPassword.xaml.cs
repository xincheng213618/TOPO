using BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace RECSuzhou
{
    /// <summary>
    /// EXCPassword.xaml 的交互逻辑
    /// </summary>
    public partial class EXCPassword : Page
    {
        public EXCPassword()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            CoutLabel.DataContext = Time;
            Countdown_timer();
            FocusManager.SetFocusedElement(this, passwordbox);
            PassErrorLabel.Visibility = Visibility.Hidden;
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (passwordbox.Password == "")
            {
                PassErrorLabel.Content = "请输入密码";
                PassErrorLabel.Visibility = Visibility.Visible;
            }

            else if (passwordbox.Password == Global.Config.AdminPassword)
            {
                Content = new SettingPage();
                Pages();
                //Environment.Exit(0);
            }
            else
            {
                PassErrorLabel.Content = "密码错误";
                PassErrorLabel.Visibility = Visibility.Visible;
            }

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        //倒计时模块
        private DispatcherTimer pageTimer = null;
        private TimeCount Time = new TimeCount();
        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
