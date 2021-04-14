using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseUtil;
namespace EXC
{
    /// <summary>
    /// SearchHeiFei1.xaml 的交互逻辑
    /// </summary>
    public partial class SearchHeiFei1 : Page
    {
        public SearchHeiFei1()
        {
            InitializeComponent();
        }
        private string ID;
        private string USCI;
        private string CompanyName;
        public SearchHeiFei1(string CompanyID,string CompanyName,string USCI)
        {
            this.ID = CompanyID;
            this.USCI = USCI;
            this.CompanyName = "";
            InitializeComponent();
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Countdown_timer();
            WaitShow.Visibility = Visibility.Visible;

            InFoTitle.Content = CompanyName +"基本信息";

            Thread thread = new Thread(() => Requests("HeiFeiEnterprise"))
            {
                IsBackground = true
            };
            thread.Start();


            Thread thread1 = new Thread(() => Requests("HeiFeiXZ"))
            {
                IsBackground = true
            };
            thread1.Start();

            Thread thread2 = new Thread(() => Requests("HeiFeiCF"))
            {
                IsBackground = true
            };
            thread2.Start();

            Thread thread3 = new Thread(() => Requests("HeiFeiRed"))
            {
                IsBackground = true
            };
            thread3.Start();
            Thread thread4 = new Thread(() => Requests("HeiFeiBlack"))
            {
                IsBackground = true
            };
            thread4.Start();
            Thread thread5 = new Thread(() => Requests("HeiFeiCommit"))
            {
                IsBackground = true
            };
            thread5.Start();


        }
        private int PageNo=1;
        private void Requests(string Type)
        {

            switch (Type)
            {
                case "HeiFeiEnterprise":
                    string response = Http.HeFei.getCompanyRelaInfo(ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "HeiFeiXZ":
                    response = Http.HeFei.GetCompanyXZXKInfo(ID, PageNo.ToString());
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "HeiFeiCF":
                    response = Http.HeFei.GetCompanyXZCFInfo(ID, PageNo.ToString());
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "HeiFeiRed":
                    response = Http.HeFei.GetCompanyRedInfo(ID, PageNo.ToString());
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "HeiFeiBlack":
                    response = Http.HeFei.GetCompanyBlackInfo(ID, PageNo.ToString());
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "HeiFeiCommit":
                    response = Http.HeFei.GetXycnCnsDetailsBySubjectCode(USCI, PageNo.ToString());
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                default:
                    break;
            }

        }

        private readonly ObservableCollection<ListBoxItem> CompanyItem = new ObservableCollection<ListBoxItem>();
        private readonly ObservableCollection<HeiFeiXK> HeiFeiXKItem = new ObservableCollection<HeiFeiXK>();
        private readonly ObservableCollection<HeiFeiCF> HeiFeiCFItem = new ObservableCollection<HeiFeiCF>();
        private readonly ObservableCollection<HeiFeiRed> HeiFeiRedItem = new ObservableCollection<HeiFeiRed>();
        private readonly ObservableCollection<HeiFeiBlack> HeiFeiBlackItem = new ObservableCollection<HeiFeiBlack>();
        private readonly ObservableCollection<HeiFeiCommit> HeiFeiCommitItem = new ObservableCollection<HeiFeiCommit>();

        int HeiFeiXKNo =0;
        int HeiFeiCFNo = 0;
        int HeiFeiRedNo = 0;
        int HeiFeiBlackNo = 0;
        int HeiFeiCommitNo = 0;

        private void HeiFeiListPhrase(string response,string Type)
        {
            //HeiFeiList.ItemsSource = HeiFeiListItem;
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                bool falg = (bool)@object.GetValue("flag");
                if (falg)
                {
                    switch (Type)
                    {
                        case "HeiFeiEnterprise":
                            JObject rows = (JObject)@object["rows"];

                            JObject result = (JObject)rows["company"];
                            CompanyItem item = new CompanyItem
                            {
                                USCI = "社会统一信用代码：" + (string)result.GetValue("uniscid"),
                                CompanyName = (string)result.GetValue("baseDwmc"),
                                BRN = (string)result.GetValue("baseZch"),
                                LegalRepresentative = "法定代表人：" + (string)result.GetValue("baseFddbr"),//企业法定达标人
                                Industry = "行业：" + (string)result.GetValue("baseHy"),
                                RegisteredCapital = (string)result.GetValue("zjze"),
                                RegisteredAddress = "注册地址：" + (string)result.GetValue("baseXxdz"),
                                BusinessScope = (string)result.GetValue("baseJyfw")
                            };
                            BasicInfoListView.DataContext = item;
                            WaitShow.Visibility = Visibility.Hidden;

                            break;
                        case "HeiFeiXZ":
                            JArray rows1 = (JArray)@object["rows"];
                            HeiFeiXKListView.ItemsSource = HeiFeiXKItem;
                            foreach (JObject result1 in rows1)
                            {
                                HeiFeiXK item1 = new HeiFeiXK();
                                HeiFeiCFNo += 1;
                                item1.ListNo = HeiFeiCFNo;
                                item1.xmmc = (string)result1.GetValue("xmmc");
                                item1.jdswh = (string)result1.GetValue("jdswh");
                                item1.xknr = (string)result1.GetValue("xknr");
                                HeiFeiXKItem.Add(item1);
                            }
                            if (HeiFeiXKItem.Count == 0)
                            {
                                HeiFeiXKMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "HeiFeiCF":
                            JArray rows2 = (JArray)@object["rows"];
                            HeiFeiCFListView.ItemsSource = HeiFeiCFItem;
                            foreach (JObject result1 in rows2)
                            {
                                HeiFeiCF item1 = new HeiFeiCF();
                                HeiFeiXKNo += 1;
                                item1.ListNo = HeiFeiXKNo;
                                item1.jdswh = (string)result1.GetValue("jdswh");
                                item1.cfmc = (string)result1.GetValue("cfmc");
                                item1.cfsy = (string)result1.GetValue("cfsy");
                                HeiFeiCFItem.Add(item1);
                            }
                            if (HeiFeiCFItem.Count == 0)
                            {
                                HeiFeiCFMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "HeiFeiRed":
                            JArray rows3 = (JArray)@object["rows"];
                            HeiFeiRedListView.ItemsSource = HeiFeiRedItem;
                            foreach (JObject result1 in rows3)
                            {
                                HeiFeiRed item1 = new HeiFeiRed();
                                HeiFeiRedNo += 1;
                                item1.ListNo = HeiFeiRedNo;
                                item1.cfjljg = (string)result1.GetValue("cfjljg");
                                item1.slyj = (string)result1.GetValue("slyj");
                                item1.fbsj = (string)result1.GetValue("fbsj");
                                HeiFeiRedItem.Add(item1);
                            }
                            if (HeiFeiRedItem.Count == 0)
                            {
                                HeiFeiRedMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "HeiFeiBlack":
                            JArray rows4 = (JArray)@object["rows"];
                            HeiFeiBlackListView.ItemsSource = HeiFeiBlackItem;
                            foreach (JObject result1 in rows4)
                            {
                                HeiFeiBlack item1 = new HeiFeiBlack();
                                HeiFeiBlackNo += 1;
                                item1.ListNo = HeiFeiBlackNo;
                                item1.cfjljg = (string)result1.GetValue("cfjljg");
                                item1.slyj = (string)result1.GetValue("slyj");
                                item1.jzrq = (string)result1.GetValue("jzrq");
                                HeiFeiBlackItem.Add(item1);
                            }
                            if (HeiFeiBlackItem.Count == 0)
                            {
                                HeiFeiBlackMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "HeiFeiCommit":

                            JArray rows5 = (JArray)@object["rows"];
                            HeiFeiCommitListView.ItemsSource = HeiFeiCommitItem;
                            foreach (JObject result1 in rows5)
                            {
                                HeiFeiCommit item1 = new HeiFeiCommit();
                                HeiFeiCommitNo += 1;
                                item1.ListNo = HeiFeiCommitNo;
                                item1.title = (string)result1.GetValue("title");
                                item1.pubTime = (string)result1.GetValue("pubTime");
                                HeiFeiCommitItem.Add(item1);
                            }
                            if (HeiFeiCommitItem.Count == 0)
                            {
                                HeiFeiCommitMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string Msg = (string)@object.GetValue("msg");
                    Global.LoadDatas.HomePageError = Msg;

                    Content = new HomePage();
                    Pages();
                }

            }
            catch
            {
                Global.LoadDatas.HomePageError = "接口解析错误，请联系开发人员";
                Content = new HomePage();
                Pages();
            }

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

        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }


        int ChangeNo = 1;
        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            ChangeNo += 1;
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Basic":
                    Panel.SetZIndex(BasicInfoShow, ChangeNo);
                    InFoTitle.Content = CompanyName + "基本信息";

                    break;
                case "Red":
                    Panel.SetZIndex(RedInfoShow, ChangeNo);
                    InFoTitle.Content = CompanyName + "红名单";

                    break;
                case "Black":
                    Panel.SetZIndex(BlackInfoShow, ChangeNo);
                    InFoTitle.Content = CompanyName + "黑名单";

                    break;
                case "XK":
                    Panel.SetZIndex(XKInfoShow, ChangeNo);
                    InFoTitle.Content = CompanyName + "行政许可";

                    break;
                case "CF":
                    Panel.SetZIndex(CFInfoShow, ChangeNo);
                    InFoTitle.Content = CompanyName + "行政处罚";

                    break;
                case "CN":
                    Panel.SetZIndex(CommitInfoShow, ChangeNo);
                    InFoTitle.Content = CompanyName + "信用承诺";

                    break;
                default:
                    Log.Write("程序出现了BUG和程序被破解了");
                    break;
            }

        }

        private void SCManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
        {

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class HeiFeiXK:ListItem
    {
        public string xmmc { get; set; }
        public string jdswh { get; set; }

        public string xknr { get; set; }

    }
    public class HeiFeiCF : ListItem
    {
        public string cfsy { get; set; }
        public string jdswh { get; set; }

        public string cfmc { get; set; }
    }

    public class HeiFeiRed : ListItem
    {
        public string cfjljg { get; set; }
        public string slyj { get; set; }

        public string fbsj { get; set; }
    }

    public class HeiFeiBlack : ListItem
    {
        public string cfjljg { get; set; }
        public string slyj { get; set; }

        public string jzrq { get; set; }
    }

    public class HeiFeiCommit : ListItem
    {
        public string title { get; set; }

        public string pubTime { get; set; }
    }

}
