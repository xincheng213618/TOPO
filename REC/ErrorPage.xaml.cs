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
                    Login();

                    break;
                default:
                    MessageBox.Show("未自定义功能的按钮，请重启软件");
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
                MessageBox.Show("密码错误" + Environment.NewLine + "如果忘记密码，联系维修人员");
            }

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
