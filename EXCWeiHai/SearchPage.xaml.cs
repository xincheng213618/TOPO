using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;
using System.Text;

namespace EXC
{

    //Designed By Mr.Xin 2020.5.25 5:38  修正
    public partial class SearchPage : Page
    {
        //查询
        private bool querying = false;
        private int pageNo = 1;
        private string SearchContent = null;
        IDCardData iDCardData = new IDCardData();

        public SearchPage()
        {
           
            InitializeComponent();
        }

        public SearchPage(IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            switch (Global.PageType)
            {
                case "ReportWeiHai":
                    SearchTitleLabel.Content = "请输入本市企业名称或者统一社会信用代码";
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
            switch (Global.PageType)
            {
                case "ReportWeiHai":
                    JObject jObject = WebService.GetLegalPersonPage(SearchContent, pageNo, 10);
                    Dispatcher.BeginInvoke(new Action(() => WeiHaiParse(jObject)));
                    break;
                default:
                    break;
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
            CompayQueryListItem.Clear();
            SearchGrid.Visibility = Visibility.Hidden;
            queryInput.Visibility = Visibility.Visible;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }


        //下拉框
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }





        #region 数据解析
        //数据展示
        private int CompanyListNum = 0;
        private readonly ObservableCollection<CompayQueryListItem> CompayQueryListItem = new ObservableCollection<CompayQueryListItem>();

        private void WeiHaiParse(JObject response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            querying = false;

            SearchGrid.Visibility = Visibility.Visible;
            WeiHaiListView.Visibility = Visibility.Visible;
            WeiHaiListView.ItemsSource = CompayQueryListItem;

            if ((string)response.GetValue("code") == "0")
            {
                JObject json = (JObject)JsonConvert.DeserializeObject((string)response.GetValue("data"));

                JArray jArray = (JArray)json["result"];
                totalLabel.Content = $"共{json["lastPageNumber"] ?? ""}条数据";

                if (jArray != null && !jArray.Equals("") && jArray.Count != 0)
                {
                    foreach (JObject returnData in jArray)
                    {
                        CompayQueryListItem item = new CompayQueryListItem();
                        CompanyListNum += 1;
                        item.ListNo = CompanyListNum;

                        item.CompanyID = (string)returnData.GetValue("xybsm");
                        item.CompanyName = (string)returnData.GetValue("qymc");
                        item.USCI = (string)returnData.GetValue("tyshxydm");
                        item.Industry = (string)returnData.GetValue("qylb");
                        CompayQueryListItem.Add(item);
                    }
                    NowTotalLabel.Content = "已经加载" + CompayQueryListItem.Count + "条数据";
                }
                else
                {
                    if (pageNo == 1)
                        PopAlert("未搜索到相应信息", 4);

                    NextPageBorder.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                Content = new HomePage((string)response.GetValue("msg"));
                Pages();
            }
        }


        #endregion

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
                Timers.Content = Time.Countdown;
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
                switch (Global.PageType)
                {
                    case "ReportWeiHai":
                        string CompanyID = CompayQueryListItem.ElementAt(listView.SelectedIndex).CompanyID.ToString();
                        Content = new VersionPage(iDCardData, CompanyID);
                        Pages();
                        break;
                    default:
                        break;
                }
            }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string strs = File.ReadAllText(System.Environment.CurrentDirectory + "/Base/政策解读.txt", Encoding.UTF8);
            DisclaimerContent.Text = strs;
            zcjdGrid.Visibility = Visibility.Visible;
          

      
            zcjdGrid.Visibility = Visibility.Visible;
            queryInput.Visibility = Visibility.Hidden;
            Topname.Visibility = Visibility.Hidden;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            zcjdGrid.Visibility = Visibility.Hidden;
            queryInput.Visibility = Visibility.Visible;
            Topname.Visibility = Visibility.Visible;
        }
    }
}
