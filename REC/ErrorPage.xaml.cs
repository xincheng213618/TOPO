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

namespace REC
{
    /// <summary>
    /// ErrorPage.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorPage : Page
    {
        string ErrorMsg;
        public ErrorPage( string ErrorMsg)
        {
            this.ErrorMsg = ErrorMsg;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            TitleLabel.Content = ErrorMsg;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Login":
                    if (PassWord.Password == Global.Config.AdminPassword)
                    {
                        Content = new HomePage();
                        Pages();
                    }
                    else
                    {
                        PopAlert("密码错误" + Environment.NewLine + "如果忘记密码，联系维修人员",1);
                    }
                    break;

                case "OCR":
                    if (PassWord.Password == Global.Config.AdminPassword)
                    {
                        Content = new FunctionPage4();
                        Pages();
                    }
                    else
                    {
                        PopAlert("密码错误" + Environment.NewLine + "如果忘记密码，联系维修人员", 1);
                    }
                    break;
                default:
                    MessageBox.Show("未自定义功能的按钮，请重启软件");
                    PopAlert("未自定义功能的按钮，请重启软件", 1);

                    break;
            }
        }

        private void Login()
        {
            if (PassWord.Password == Global.Config.AdminPassword)
            {
                Content = new HomePage();
                Pages();
            }
            else
            {
                PopAlert("密码错误" + Environment.NewLine + "如果忘记密码，联系维修人员", 1);
            }

        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }

        private async void PopAlert(string Msg, int time)
        {
            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;

        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }
    }
}
