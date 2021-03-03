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

namespace EXC
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }


        private TimeCount timeCount = new TimeCount() { Countdown = 29 };

        private void Page_Initialized(object sender, EventArgs e)
        {
            PassWordBorder.DataContext = timeCount;
            Countdown();
            FocusManager.SetFocusedElement(this, PassWord);
        }
        private DispatcherTimer pageTimer = null;

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
        private async void PopAlert(string Msg, int time)
        {
            Log.Write("QRCodePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
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
                case "Login":
                    Login();
                    break;
                case "ModifyPassWord":
                    SettingBorder.Visibility = Visibility.Hidden;
                    TitleLabel.Content = "修改密码";
                    ButtonLabel.Content = "确定";
                    FunctionButton.Tag = "Modify";
                    break;
                case "Modify":
                    SettingBorder.Visibility = Visibility.Visible;
                    Global.Config.AdminPassword = PassWord.Password;
                    PassWord.Password = null;
                    Global.WriteConfigFile("Config");
                    PopAlert("密码修改成功" + Environment.NewLine + "如果忘记密码，联系维修人员", 2);

                    break;
                case "Close":
                    Environment.Exit(0);
                    break;
            }
        }

        private void PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }

        private void Login()
        {
            if (PassWord.Password == Global.Config.AdminPassword)
            {
                PassWord.Password = null;
                SettingBorder.Visibility = Visibility.Visible;
            }
            else
            {
                PopAlert("密码错误" + Environment.NewLine + "如果忘记密码，联系维修人员", 2);
            }
        }
    }
}

