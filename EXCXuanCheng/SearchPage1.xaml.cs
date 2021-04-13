using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EXCXuanCheng
{
    /// <summary>
    /// SearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchPage1 : Page
    {
        private Timer timer = null;
        //查询
        private bool querying = false;
        private int pageNo = 1;
        private string SearchContent = null;

        public SearchPage1()
        {
            InitializeComponent();
        }


        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            switch (Global.Related.PageType)
            {
                case "XuanChengQiYi":
                    SearchTitleLabel.Content = "安徽省企业信息列表搜索";
                    break;
                case "XuanChengShiYi":
                    SearchTitleLabel.Content = "安徽省事业单位企信息列表搜索";
                    break;
                case "XuanChengSheHui":
                    SearchTitleLabel.Content = "安徽省社会团体信息列表搜索";
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
            Thread worker = new Thread(() => SearchRequests(SearchContent))
            {
                IsBackground = true
            };
            worker.Start();
        }

        private void SearchRequests(string SearchContent)
        {
            string response = null;
            switch (Global.Related.PageType)
            {
                case "XuanChengQiYi":
                    response = Http.XuanCheng.Search.queryList(SearchContent, "企业");
                    break;
                case "XuanChengShiYi":
                    response = Http.XuanCheng.Search.queryList(SearchContent, "事业单位");
                    break;
                case "XuanChengSheHui":
                    response = Http.XuanCheng.Search.queryList(SearchContent, "社会团体");
                    break;
                default:
                    MessageBox.Show("找不到对应的类型，请充实");
                    break;
            }
            Dispatcher.BeginInvoke(new Action(() => Phrase(response)));
        }



        //数据展示
        private int CompanyListNum = 0;
        private readonly ObservableCollection<CompayQueryListItem> CompaQueryListItem = new ObservableCollection<CompayQueryListItem>();

        private void Phrase(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            querying = false;

            SearchGrid.Visibility = Visibility.Visible;
            listView.Visibility = Visibility.Visible;
            listView.ItemsSource = CompaQueryListItem;
            if (response != null)
            {
                try
                {
                    JObject jObject = (JObject)JsonConvert.DeserializeObject(response);
                    string code = (string)jObject.GetValue("code");

                    if (code.Equals("0"))
                    {
                        JObject data = (JObject)jObject["data"];
                        totalLabel.Content = $"共{data["total"] ?? ""}条数据";

                        JArray jArray = (JArray)data["rows"];

                        foreach(JObject jObject1 in jArray)
                        {
                            CompayQueryListItem Item = new CompayQueryListItem()
                            {
                                ListNo = CompanyListNum++,
                                CompanyName = (string)jObject1.GetValue("qymc"),
                                CompanyID = (string)jObject1.GetValue("id"),
                                USCI = "统一社会信用代码："+(string)jObject1.GetValue("tyshxydm"),
                                Type = "机构类型："+(string)jObject1.GetValue("jglx"),

                            };
                            CompaQueryListItem.Add(Item);
                        }

                        if (CompaQueryListItem.Count > 0)
                        {
                            SearchGrid.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            PopAlert("暂无数据", 3);
                        }
                    }
                    else
                    {
                        PopAlert("暂无数据",3);
                    }





                }
                catch
                {
                    Content = new HomePage("接口解析错误");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("接口连接错误");
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
            CompanyListNum = 0;
            CompaQueryListItem.Clear();
            SearchGrid.Visibility = Visibility.Hidden;
            queryInput.Visibility = Visibility.Visible;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }


        //提示方法


        private async void PopAlert(string Msg, int time)
        {
            SearchGrid.Visibility = Visibility.Hidden;
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

            if (listView.SelectedIndex > -1)
            {
                SearchContent = CompaQueryListItem.ElementAt(listView.SelectedIndex).CompanyName.ToString();

                switch (Global.Related.PageType)
                {
                    case "TopoSearch":
                        Content = new CompayQueryDetailPage(SearchContent);
                        Pages();
                        break;
                    default:
                        Content = new CompayQueryDetailPage(SearchContent);
                        Pages();
                        break;
                }
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;
            pageNo += 1;
            Thread worker = new Thread(() => SearchRequests(SearchContent))
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
            Time.Countdown = 30;
        }

        private void DragDown_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            CompanySearchBox.Text = button.Tag.ToString();
            border1.Visibility = Visibility.Hidden;
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Visible;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
