
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace XinHua
{
    /// <summary>
    /// SearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchPage : Page
    {
        private string companyName = null;
        //查询
        private bool querying = false;
        public SearchPage()
        {
            InitializeComponent();
        }
        public SearchPage(string Msg)
        {
            InitializeComponent();
            PopAlert(Msg, 3);
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            FocusManager.SetFocusedElement(this, CompanySearchBox);
            Countdown_timer();
            switch(Global.Related.PageType)
            {
                case ("QiYeXinXi"):
                    searchTitle.Content = "企业信息查询";
                    break;
                case ("NaShuiXinYongA"):
                    searchTitle.Content = "纳税信用A级企业列表搜索";
                    break;
                case ("ShuiShouWeiFa"):
                    searchTitle.Content = "重大税收违法案件人查询";
                    break;
                case ("ShiXinRen"):
                    searchTitle.Content = "失信被执行人查询";
                    break;
                default:
                    searchTitle.Content = "企业名称查询页面";
                    break;
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        //计时器模块
        private DispatcherTimer pageTimer = null;
        TimeCount Time = new TimeCount();

        private void Countdown_timer()
        {
            this.DataContext = Time;
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {

                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        //页面转换
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            companyName = CompanySearchBox.Text;
            if (companyName.Length ==0)
            {
                PopAlert("请输入需要查询的内容", 2);
                return;
            }
            if (querying) return;
            querying = true;
            WaitShow.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询请稍候";
            Time.Countdown = 59;


            Content = new SearchListPage(companyName);
            Pages();             
        }

        private async void PopAlert(string Msg, int time)
        {           
            PopTips.Text = Msg;
            Pop.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(time));
            Pop.Visibility = Visibility.Hidden;
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Time.Countdown = 59;
        }
        //回车搜索
        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
            }
        }




        private void POP_Button(object sender, RoutedEventArgs e)
        {
            Pop.Visibility = Visibility.Hidden;
        }

        private void KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("SystemKeyBoard.exe");
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.O });
        }
    }
}
