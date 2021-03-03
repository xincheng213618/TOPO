
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using BaseUtil;

namespace EXC
{
    //Designed By Mr.Xin 2019.12
    //ReBuilding By.Mr.Xin 2020.5
    /// CompayQueryDetailPage.xaml 的交互逻辑
    /// </summary>

    public partial class CompayQueryDetailPage : Page, IDisposable
    {
        private string CompanyName = null;
        private string CompanyID = null;
        private CompayQueryListItem compayQueryListItem;

        public CompayQueryDetailPage(CompayQueryListItem compayQueryListItem)
        {
            this.compayQueryListItem = compayQueryListItem;
            this.CompanyName = compayQueryListItem.CompanyName;
            this.CompanyID = compayQueryListItem.CompanyID;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Countdown_timer();
            labelscpoe.Text = compayQueryListItem.BusinessScope;
            LabelRegisteredAddress.Content += compayQueryListItem.RegisteredAddress;
            LabelLegalRepresentative.Content += compayQueryListItem.LegalRepresentative;
            LabelUSCI.Content += compayQueryListItem.USCI;
            InFoTitle.Content = CompanyName + "基本信息";
            LabelGongShang.Content += compayQueryListItem.BRN;
            BasicInfoListView.DataContext = compayQueryListItem;

            //加载企业标题及基本信息
            FocusManager.GetIsFocusScope(this);


            Thread thread1 = new Thread(() => Requests("XZXK"))
            {
                IsBackground = true
            };
            thread1.Start();

            Thread thread2 = new Thread(() => Requests("XZCF"))
            {
                IsBackground = true
            };
            thread2.Start();

        }

        private int PageNo = 1;
        private void Requests(string Type)
        {

            switch (Type)
            {
                case "XZXK":
                    string response = Http.HeFei.XZXK(CompanyID, PageNo.ToString());
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "XZCF":
                    response = Http.HeFei.XZCF(CompanyID, PageNo.ToString());
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "Red":
                    response = Http.HeFei.Red(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                case "Black":
                    response = Http.HeFei.Black(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response, Type)));
                    break;
                default:
                    break;
            }

        }

        private readonly ObservableCollection<HeiFeiXK> HeiFeiXKItem = new ObservableCollection<HeiFeiXK>();
        private readonly ObservableCollection<HeiFeiCF> HeiFeiCFItem = new ObservableCollection<HeiFeiCF>();
        private readonly ObservableCollection<HeiFeiRed> HeiFeiRedItem = new ObservableCollection<HeiFeiRed>();
        private readonly ObservableCollection<HeiFeiBlack> HeiFeiBlackItem = new ObservableCollection<HeiFeiBlack>();

        int HeiFeiXKNo = 0;
        int HeiFeiCFNo = 0;
        int HeiFeiRedNo = 0;
        int HeiFeiBlackNo = 0;

        private void HeiFeiListPhrase(string response, string Type)
        {
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                string code = (string)@object.GetValue("code");
                if (code=="200")
                {
                    switch (Type)
                    {
                        case "XZXK":
                            JObject result = (JObject)@object["result"];

                            JArray rows1 = (JArray)result["rows"];
                            HeiFeiXKListView.ItemsSource = HeiFeiXKItem;
                            foreach (JObject result1 in rows1)
                            {
                                HeiFeiXK item1 = new HeiFeiXK();
                                HeiFeiXKNo += 1;
                                item1.ListNo = HeiFeiXKNo;
                                item1.xmmc = (string)result1.GetValue("XK_WSH");
                                item1.jdswh = (string)result1.GetValue("XK_XKWS");
                                item1.xknr = (string)result1.GetValue("XK_NR");
                                HeiFeiXKItem.Add(item1);
                            }
                            if (HeiFeiXKItem.Count == 0)
                            {
                                HeiFeiXKMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "XZCF":
                            JObject result3 = (JObject)@object["result"];

                            JArray rows2 = (JArray)result3["rows"];
                            HeiFeiCFListView.ItemsSource = HeiFeiCFItem;
                            foreach (JObject result1 in rows2)
                            {
                                HeiFeiCF item1 = new HeiFeiCF();
                                HeiFeiCFNo += 1;
                                item1.ListNo = HeiFeiCFNo;
                                item1.jdswh = (string)result1.GetValue("CF_WSH");
                                item1.cfmc = (string)result1.GetValue("CF_WFXW");
                                item1.cfsy = (string)result1.GetValue("CF_NR");
                                HeiFeiCFItem.Add(item1);
                            }
                            if (HeiFeiCFItem.Count == 0)
                            {
                                HeiFeiCFMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "Red":
                            HeiFeiRedMsg.Visibility = Visibility.Visible;

                            //JArray result4 = (JArray)@object["result"];

                            //JArray rows3 = (JArray)result4["2"];
                            //HeiFeiRedListView.ItemsSource = HeiFeiRedItem;
                            //foreach (JObject result1 in rows3)
                            //{
                            //    HeiFeiRed item1 = new HeiFeiRed();
                            //    HeiFeiRedNo += 1;
                            //    item1.ListNo = HeiFeiRedNo;
                            //    item1.cfjljg = (string)result1.GetValue("cfjljg");
                            //    item1.slyj = (string)result1.GetValue("slyj");
                            //    item1.fbsj = (string)result1.GetValue("fbsj");
                            //    HeiFeiRedItem.Add(item1);
                            //}
                            //if (HeiFeiRedItem.Count == 0)
                            //{
                            //    HeiFeiRedMsg.Visibility = Visibility.Visible;
                            //}
                            break;
                        case "Black":
                            HeiFeiBlackMsg.Visibility = Visibility.Visible;

                            //JObject result5 = (JObject)@object["result"];

                            //JArray rows4 = (JArray)result5["rows"];
                            //HeiFeiBlackListView.ItemsSource = HeiFeiBlackItem;
                            //foreach (JObject result1 in rows4)
                            //{
                            //    HeiFeiBlack item1 = new HeiFeiBlack();
                            //    HeiFeiBlackNo += 1;
                            //    item1.ListNo = HeiFeiBlackNo;
                            //    item1.cfjljg = (string)result1.GetValue("cfjljg");
                            //    item1.slyj = (string)result1.GetValue("slyj");
                            //    item1.jzrq = (string)result1.GetValue("jzrq");
                            //    HeiFeiBlackItem.Add(item1);
                            //}
                            //if (HeiFeiBlackItem.Count == 0)
                            //{
                            //    HeiFeiBlackMsg.Visibility = Visibility.Visible;
                            //}
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string Msg = (string)@object.GetValue("msg");
                    Content = new HomePage(Msg);
                    Pages();
                }

            }
            catch
            {
                //Content = new HomePage("接口解析错误，请联系开发人员");
                //Pages();
            }

        }

        #region 倒计时模块
        private DispatcherTimer pageTimer = null;
        private TimeCount Time = new TimeCount();

        private void Countdown_timer()
        {
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
        #endregion

        private void Home_Click(object sender, RoutedEventArgs e)
        {

            Content = new HomePage();
            Pages();
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();

        }
        //页面转换
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }



        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Content = new Report(compayQueryListItem);
            //Content = new IDCardPage();
            Pages();
        }





        private void ProgressBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pageTimer.IsEnabled = ! pageTimer.IsEnabled;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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
                default:
                    Log.Write("程序出现了BUG和程序被破解了");
                    break;
            }

        }


        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
    }

    public class HeiFeiXK : ListItem
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
}
