using BaseDLL;
using BaseUtil;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RECSuzhou
{
    /// <summary>
    /// IDcardInputPage.xaml 的交互逻辑
    /// </summary>
    public partial class IDcardInputPage : Page
    {
        public IDcardInputPage()
        {
            InitializeComponent();

        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Account.Text = Global.Related.IDCardData.Name;
            IDCardNoText.Text = Global.Related.IDCardData.IDCardNo;
            FocusManager.SetFocusedElement(this, Account);
            CoutLabel.DataContext = Time;
            Countdown_timer();
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



        private void Search_Click(object sender, RoutedEventArgs e)
        {

            Global.Related.IDCardData.Name = Account.Text;
            Global.Related.IDCardData.IDCardNo = IDCardNoText.Text;
            if (Account.Text.Length == 0 || Account.Text.Length == 0)
            {
                ErrorLabel.Visibility = Visibility.Visible;
                ErrorLabel.Content = "请输入姓名和身份证号";
                return;
            }
            else
            {
                // 输入外籍证件号也可查询
                SwitchPage();
                //if (Check.CheckIDCardNo(idcardData.IDCardNo))
                //{
                //    SwitchPage();
                //}
                //else
                //{
                //    ErrorLabel.Visibility = Visibility.Visible;
                //    ErrorLabel.Content = "请输入正确的身份证号码";
                //}
            }
        }
        private void SwitchPage()
        {
            switch (Global.Related.PageType)
            {
                case "HomeCountPages":
                    Global.Related.PageType = null;
                    Content = new HomeCountPages();
                    break;
                case "NoHome":
                case "NoHomeChild":
                    Content = new NoHomePages();
                    break;
                case "OwnerShipPages":
                    Content = new OwnerShipPages();
                    break;
                case "SZWZArchivePages":
                case "SZHQArchivePages":
                    Content = new SZArchivePage();
                    break;
            }
            Pages();
        }




        private void Account_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Visibility = Visibility.Hidden; 
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

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Time.Countdown = 59;
        }

        private void ClearText_Click(object sender, RoutedEventArgs e)
        {
            Account.Text = "";
            IDCardNoText.Text = "";
        }
    }
}
