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
using BaseUtil;

namespace EXC
{
    //Designed By Mr.Xin 2020.8.12

    /// <summary>
    /// SearchHeiFei.xaml 的交互逻辑
    /// </summary>
    public partial class SearchHeiFei : Page
    {
        int PageNo = 1;
        string SearchText = "";
        string Type;

        public SearchHeiFei()
        {
            InitializeComponent();
        }

        public SearchHeiFei( string Type)
        {
            this.Type = Type;
            InitializeComponent();
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
        private async void PopAlert(string Msg, int time)
        {
            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Countdown_timer();
            
            Search();
            switch (Global.PageType)
            {
                case "HeiFeiRed":
                    SearchTitleLabel.Content = "红名单";
                    break;
                case "HeiFeiBlack":
                    SearchTitleLabel.Content = "黑名单";
                    break;
                case "HeiFeiXK":
                    SearchTitleLabel.Content = "行政许可";
                    break;
                case "HeiFeiCF":
                    SearchTitleLabel.Content = "行政处罚";
                    break;
                case "HeiFeiImportantPerson":
                    SearchTitleLabel.Content = "重点人群";
                    break;
                case "HeiFeiEnterprise":
                    SearchTitleLabel.Content = "企业信用信息查询";
                    break;
                default:
                    break;
            }
        }

        //企业查询页面
        private void Query_Click(object sender, RoutedEventArgs e)
        {
            SearchText = SearchContent.Text;
            PageNo = 1;
            HeiFeiListItem.Clear();
            Search();
        }
        //回车搜索
        private void KeyEnter(object sender, KeyEventArgs e)
        {
            SearchText = SearchContent.Text;
            PageNo = 1;
            HeiFeiListItem.Clear();
            if (e.Key == Key.Enter)
                Search();
        }

        private void Search()
        {
            WaitShow.Visibility = Visibility.Visible;
            Thread worker = new Thread(() => SearchRequests())
            {
                IsBackground = true
            };
            worker.Start();
        }

        private void SearchRequests()
        {
            string response;
            switch (Global.PageType)
            {
                case "HeiFeiRed":
                    response = Http.HeFei.GetRedPageInfo(PageNo.ToString(), SearchText);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response)));

                    break;
                case "HeiFeiBlack":
                    response = Http.HeFei.GetBlackPageInfo(PageNo.ToString(), SearchText);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response)));
                    break;
                case "HeiFeiXK":
                    response = Http.HeFei.GetXZXKPageInfo(PageNo.ToString(), SearchText);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response)));
                    break;
                case "HeiFeiCF":
                    response = Http.HeFei.GetXZCFPageInfo(PageNo.ToString(), SearchText);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response)));
                    break;
                case "HeiFeiImportantPerson":
                    response = Http.HeFei.GetGroupsFileListByPager(PageNo.ToString(), SearchText , Type);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response)));
                    break;
                case "HeiFeiEnterprise":
                    response = Http.HeFei.GetXYXXPageInfo(PageNo.ToString(), SearchText);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response)));
                    break;
                default: 
                    break;
            }

        }

        private readonly ObservableCollection<HeiFeiListItem> HeiFeiListItem = new ObservableCollection<HeiFeiListItem>();

        private void HeiFeiListPhrase(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            HeiFeiList.ItemsSource = HeiFeiListItem;
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                bool falg = (bool)@object.GetValue("flag");
                if (falg)
                {
                    totalLabel.Content = $"共{(string)@object.GetValue("total") ?? ""}条数据";
                    JArray jArray = (JArray)@object["rows"];
                    if (jArray != null && !jArray.Equals("") && jArray.Count != 0)
                    {
                        foreach (JObject result in jArray)
                        {
                            HeiFeiListItem item = new HeiFeiListItem();

                            switch (Global.PageType)
                            {
                                case "HeiFeiImportantPerson":
                                    item.ID = (string)result.GetValue("recordId");
                                    item.XdrMc = (string)result.GetValue("xm");
                                    item.Xzjg = (string)result.GetValue("chineseName");
                                    item.XdrFr = (string)result.GetValue("sfzh");
                                    item.Jdrq = (string)result.GetValue("zgzsh");//证书编号

                                    item.recordId= (string)result.GetValue("recordId");
                                    item.tableId = (string)result.GetValue("tableId");
                                    item.typeConfigId = (string)result.GetValue("typeConfigId");
                                    break;
                                case "HeiFeiRed":
                                case "HeiFeiBlack":
                                case "HeiFeiXK":
                                case "HeiFeiCF":
                                    item.ID = (string)result.GetValue("id");
                                    item.XdrMc = (string)result.GetValue("xdrMc");
                                    item.XdrShxym = (string)result.GetValue("xdrShxym");
                                    item.XdrShxymSrc = (string)result.GetValue("xdrShxymSrc");
                                    item.Xzjg = (string)result.GetValue("xzjg");
                                    item.XdrLx = (string)result.GetValue("xdrLx");
                                    item.XdrFr = (string)result.GetValue("xdrFr");
                                    item.Jdrq = (string)result.GetValue("jdrq");
                                    break;
                                case "HeiFeiEnterprise":
                                    item.ID = (string)result.GetValue("id");
                                    item.XdrMc = (string)result.GetValue("baseDwmc");
                                    item.XdrShxym = (string)result.GetValue("uniscid");
                                    item.XdrShxymSrc = (string)result.GetValue("xdrShxymSrc");
                                    item.Xzjg = (string)result.GetValue("baseHy");
                                    item.XdrLx = (string)result.GetValue("baseHy");
                                    item.XdrFr = (string)result.GetValue("baseFddbr");
                                    item.Jdrq = (string)result.GetValue("gxrq");
                                    break;
                                default:
                                    break;

                            }

                            HeiFeiListItem.Add(item);
                        }
                        NowTotalLabel.Content = "已经加载" + HeiFeiListItem.Count + "条数据";
                        NextPageBorder.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PopAlert("暂无数据，请更换搜索词", 3);
                        NextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    string Msg =(string)@object.GetValue("msg");
                    PopAlert(Msg,3);
                    NextPageBorder.Visibility = Visibility.Hidden;
                }

            }
            catch 
            {
                Global.LoadDatas.HomePageError = "接口解析错误，请联系开发人员";
                Content = new HomePage();
                Pages();
            }

        }





        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            PageNo += 1;
            Search();
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }


        private void HeiFeiList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (Global.PageType)
                {
                    case "HeiFeiRed":
                    case "HeiFeiBlack":
                    case "HeiFeiXK":
                    case "HeiFeiCF":
                        string id = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).ID.ToString();
                        string XdrMc = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).XdrMc.ToString();
                        WaitShow.Visibility = Visibility.Visible;
                        Content = new SearchHeiFei3 (XdrMc,id);
                        Pages();

                        //Thread worker = new Thread(() => SearchContentRequests(id, XdrMc))
                        //{
                        //    IsBackground = true
                        //};
                        //worker.Start();
                        break;
                    case "HeiFeiImportantPerson":
                        string recordId = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).recordId.ToString();
                        string tableId = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).tableId.ToString();
                        string typeConfigId = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).typeConfigId.ToString();
                        Content = new SearchHeiFei2(recordId, tableId, typeConfigId);
                        Pages();

                        break;
                    case "HeiFeiEnterprise":
                        id = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).ID.ToString();
                        XdrMc = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).XdrMc.ToString();
                        string XdrShxym = HeiFeiListItem.ElementAt(HeiFeiList.SelectedIndex).XdrShxym.ToString();

                        //Content = new CompayQueryDetailPage(XdrMc);
                        //Pages();
                        Content = new SearchHeiFei1(id, XdrShxym,XdrMc);
                        Pages();
                        break;
                }
            }
            catch
            {

            };


        }

        private void SearchContentRequests(string ID,string XdrMc)
        {
            string response;
            switch (Global.PageType)
            {
                case "HeiFeiRed":
                    response = Http.HeFei.GetRedListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiRedBlackPhrase(response)));
                    break;
                case "HeiFeiBlack":
                    response = Http.HeFei.GetBlackListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiRedBlackPhrase(response)));
                    break;
                case "HeiFeiXK":
                    response = Http.HeFei.GetLicensingListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiRedBlackPhrase(response)));
                    break;
                case "HeiFeiCF":
                    response = Http.HeFei.GetSanctionListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiRedBlackPhrase(response)));
                    break;
                default:
                    break;
            }
        }



        private void HeiFeiRedBlackPhrase(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                bool falg = (bool)@object.GetValue("flag");
                if (falg)
                {
                    JArray jArray = (JArray)@object["rows"];
                    if (jArray != null && !jArray.Equals("") && jArray.Count != 0)
                    {
                        int i = 0;
                        foreach (JObject result in jArray)
                        {
                            if (i == 0)
                            {
                                switch (Global.PageType)
                                {
                                    case "HeiFeiRed":
                                        InfoShowTitile.Text = "奖励信息";
                                        InfoShowReason.Text = "依据：" + (string)result.GetValue("slyj");
                                        InfoShowContent.Text = (string)result.GetValue("cfjljg") != null ? (string)result.GetValue("cfjljg") : "暂无奖励信息";
                                        InfoShowDate.Text = "有效期：" + (string)result.GetValue("jzrq");

                                        break;
                                    case "HeiFeiBlack":
                                        InfoShowTitile.Text = "处罚信息";
                                        InfoShowReason.Text = "依据：" + (string)result.GetValue("slyj");
                                        InfoShowContent.Text = (string)result.GetValue("cfjljg") != null ? (string)result.GetValue("cfjljg") : "暂无奖励信息"; 
                                        InfoShowDate.Text = "有效期：" + (string)result.GetValue("jzrq");

                                        break;
                                    case "HeiFeiXK":
                                        InfoShowTitile.Text = (string)result.GetValue("xmmc");
                                        InfoShowReason.Text = "依据：" + (string)result.GetValue("jdswh");
                                        InfoShowContent.Text = (string)result.GetValue("splb") ?? "暂无信息";
                                        InfoShowDate.Text = "有效期：" + (string)result.GetValue("jzrq");
                                        break;
                                    case "HeiFeiCF":
                                        InfoBorder.Width = 800;
                                        InfoShowTitile.Text = (string)result.GetValue("cfmc");
                                        InfoShowReason.Text = "依据：" + (string)result.GetValue("cfyj");
                                        InfoShowContent.Text = (string)result.GetValue("cflb1") ?? (string)result.GetValue("cflb2");
                                        InfoShowDate.Text = (string)result.GetValue("jdrq");
                                        break;
                                }                        
                                i++;
                            }
                            InfoShow.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        PopAlert("暂无数据", 3);
                    }
                }
                else
                {
                    string Msg = (string)@object.GetValue("msg");
                    PopAlert(Msg, 3);
                }

            }
            catch
            {
                Global.LoadDatas.HomePageError = "接口解析错误，请联系开发人员";

                Content = new HomePage();
                Pages();
            }

        }

        private void InfoShow_Click(object sender, RoutedEventArgs e)
        {
            InfoShow.Visibility = Visibility.Hidden;
        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();

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


