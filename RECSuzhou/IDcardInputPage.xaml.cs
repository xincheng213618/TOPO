using BaseDLL;
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



        private IDCardData idcardData = new IDCardData();
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            idcardData.Name = Account.Text;
            idcardData.IDCardNo = IDCardNoText.Text;
            
            if (idcardData.Name.Length == 0 || idcardData.IDCardNo.Length == 0)
            {
                ErrorLabel.Visibility = Visibility.Visible;
                ErrorLabel.Content = "请输入姓名和身份证号";
                return;
            }
            else
            {
                if (Check.CheckIDCardNo(idcardData.IDCardNo))
                {
                    try
                    {
                        TimeSpan sp = DateTime.Now.Subtract(DateTime.ParseExact(idcardData.IDCardNo.Substring(6, 8), "yyyyMMdd", null));
                        if (sp.Days / 365 > 18)
                        {
                            Content = new HomePage("请选择成年人无房证明打印！");
                            Pages();
                            return;
                        }
                        Content = new NoHomePages(idcardData);
                        Pages();
                    }
                    catch
                    {
                        ErrorLabel.Visibility = Visibility.Visible;
                        ErrorLabel.Content = "请输入正确的身份证号码";
                    }
                }
                else
                {
                    ErrorLabel.Visibility = Visibility.Visible;
                    ErrorLabel.Content = "请输入正确的身份证号码";
                }
            }
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
    
    }
}
