using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RECSuzhou
{
    /// <summary>
    /// SearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchPage : Page
    {
        //查询
        private bool querying = false;
        private int pageNo = 1;
        private string SearchContent = null;
        public SearchPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {         
            Countdown_timer();
            switch (Global.PageType)
            {
             
                case "SZHQMoney":
                    SearchTitleLabel.Content = "请输入要查询资金的编号";
                    break;
                case "SZHQProgress":
                    SearchTitleLabel.Content = "请输入要查询进程的编号";
                    break;
              
                default:
                    break;
            }
           
        }

        //企业查询页面
        private void Query_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        //回车搜索
        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search();
        }

        private void Search()
        {

            SearchContent = CompanySearchBox.Text;
            if (SearchContent.Length == 0)
            {
                PopAlert("请输入需要查询的内容", 1);
                return;
            }
            if (querying) return;
            querying = true;
            WaitShow.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询请稍候";
            Time.Countdown = 59;

            pageNo = 1;
            Thread worker = new Thread(() => SearchRequests())
            {
                IsBackground = true
            };
            worker.Start();
        }

        private void SearchRequests()
        {
            string response = null;
            switch (Global.PageType)
            {
              
                case "SZHQMoney":
                    response = Http.MoneyProgressQuery(SearchContent);
                    Dispatcher.BeginInvoke(new Action(() => SZHQParse(response)));
                    break;
                case "SZHQProgress":
                    response = Http.ProcessProgressQuery(SearchContent);
                    Dispatcher.BeginInvoke(new Action(() => SZHQParse(response)));
                    break;

                default:
                    break;
            }

        }
        private void SZHQParse(string response)
        {
            try
            {
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json != null && !json.Equals(""))
                {
                    string code = json["code"].ToString();
                    string msg = json["Message"].ToString();
                    if (code.Equals("0"))
                    {
                        string result = json["result"].ToString();
                        InfoLabel.Text = result;
                        if (result.Length == 0)
                        {
                            InfoLabel.Text = "未查询到此数据";
                        }
                        queryInput.Visibility = Visibility.Hidden;
                        InfoBorder.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PopAlert(msg, 2);
                    }
                }
                else
                {
                    PopAlert("未搜索到相关信息", 2);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                Content = new HomePage("该接口解析错误");
                Pages();
            }
        }
        //返回
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            some();
        }

        private void some()
        {
            Time.Countdown = 59;
           
            
            queryInput.Visibility = Visibility.Visible;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        //下拉框
      

        //清除手写板
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //Clear_ink();
            CompanySearchBox.Text = "";
        }

        ///手写板清除
      

        //修正不直接对页面负责对窗口负责
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //User32dll.KeyHelper.OnKeyPress(User32dll.KeyHelper.KeyCode.BACK);
            if (CompanySearchBox.Text.Length > 0) 
            {
                CompanySearchBox.Text = CompanySearchBox.Text.Substring(0, CompanySearchBox.Text.Length - 1);
            }
            
        }

       


        private async void PopAlert(string Msg, int time)
        {
            
            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
            some();

        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();


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
                FocusManager.SetFocusedElement(this, CompanySearchBox);

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
        //跳转到详情页面（新）
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            //try
            //{
            //    SearchContent = CompayQueryListItem.ElementAt(listView.SelectedIndex).CompanyName.ToString();


            //    switch (Global.PageType)
            //    {
            //        case "TopoSearch":
            //            Content = new CompayQueryDetailPage(SearchContent);
            //            Pages();
            //            break;
            //        case "QingDaoReport":
            //            SearchTitleLabel.Content = "请输入企业名称或者统一社会信用代码";
            //            break;

            //        default:
            //            break;
            //    }
            //}
            //catch
            //{
            //    listView.SelectedIndex = 0;
            //}
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;
            pageNo += 1;
            Thread worker = new Thread(() => SearchRequests())
            {
                IsBackground = true
            };
            worker.Start();
        }
        //有用的东西
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Time.Countdown = 59;           
        }

        

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Visible;
        }

        private void Keypad_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; 
            CompanySearchBox.Text = CompanySearchBox.Text + button.Tag.ToString();
         }

        private void KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.O });
        }
    }

}
