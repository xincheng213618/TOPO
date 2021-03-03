using BaseUtil;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EXC
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
            this.DataContext = Time;
            Countdown_timer();
            FocusManager.SetFocusedElement(this, passwordbox);
            PassErrorLabel.Visibility = Visibility.Hidden;
        }

        private bool PasswordEye = true;
        private void Eye_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordEye)
            {
                PasswordText.Visibility = Visibility.Visible;
                PasswordText.Text = passwordbox.Password;
                PasswordEye = false;
            }
            else
            {
                PasswordEye = true;
                passwordbox.Password = PasswordText.Text;
                PasswordText.Visibility = Visibility.Hidden;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            if (!PasswordEye)
            {
                passwordbox.Password = PasswordText.Text;
            }

            if (passwordbox.Password == "")
            {
                PassErrorLabel.Content = "请输入密码";
                PassErrorLabel.Visibility = Visibility.Visible;
            }

            else if (passwordbox.Password == Global.configData.Adminpassword)
            {
                Content = new SettingPage();
                Pages();
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
    }
}
