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
    /// SearchHeiFei2.xaml 的交互逻辑
    /// </summary>
    public partial class SearchHeiFei2 : Page
    {
        string recordId;
        string tableId;
        string typeConfigId;
        public SearchHeiFei2(string recordId,string tableId,string typeConfigId)
        {
            this.recordId = recordId;
            this.tableId = tableId;
            this.typeConfigId = typeConfigId;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Countdown_timer();
            WaitShow.Visibility = Visibility.Visible;


            Thread thread = new Thread(() => Requests())
            {
                IsBackground = true
            };
            thread.Start();
        }

    //    private int PageNo = 1;
        private void Requests()
        {

            string response = Http.HeFei.GetGroupsFileDetail(tableId,recordId, typeConfigId);
            Dispatcher.BeginInvoke(new Action(() => HeiFeiListPhrase(response)));
        }


        private readonly ObservableCollection<HeiFeiList> HeiFeiBaseItem = new ObservableCollection<HeiFeiList>();
        private readonly ObservableCollection<HeiFeiList> HeiFeiXKItem = new ObservableCollection<HeiFeiList>();
        private readonly ObservableCollection<HeiFeiList> HeiFeiCFItem = new ObservableCollection<HeiFeiList>();
        private readonly ObservableCollection<HeiFeiList> HeiFeiRedItem = new ObservableCollection<HeiFeiList>();
        private readonly ObservableCollection<HeiFeiList> HeiFeiBlackItem = new ObservableCollection<HeiFeiList>();
        private readonly ObservableCollection<HeiFeiList> HeiFeiCommitItem = new ObservableCollection<HeiFeiList>();



        private void HeiFeiListPhrase(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            HeiFeiBasicListView.ItemsSource = HeiFeiBaseItem;
            HeiFeiXKListView.ItemsSource = HeiFeiXKItem;
            HeiFeiCFListView.ItemsSource = HeiFeiCFItem;
            HeiFeiRedListView.ItemsSource = HeiFeiRedItem;
            HeiFeiBlackListView.ItemsSource = HeiFeiBlackItem;
            HeiFeiCommitListView.ItemsSource = HeiFeiCommitItem;
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                bool falg = (bool)@object.GetValue("flag");
                if (falg)
                {
                    JObject rows = (JObject)@object["rows"];


                    JArray baseInfo = (JArray)rows["baseInfo"];

                    foreach (JObject result in baseInfo)
                    {
                        HeiFeiList item = new HeiFeiList();
                        item.Field = (string)result.GetValue("field");
                        item.Value = (string)result.GetValue("value");
                        HeiFeiBaseItem.Add(item);
                    }
                    if (HeiFeiBaseItem.Count == 0)
                    {
                        BasicInfoMsg.Visibility = Visibility.Visible;
                    }

                    JArray xkInfo = (JArray)rows["xzxk"];

                    foreach (var xkInfoDetail in xkInfo)
                    {
                        foreach (JObject result1 in xkInfoDetail)
                        {
                            HeiFeiList item1 = new HeiFeiList();
                            item1.Field = (string)result1.GetValue("field");
                            item1.Value = (string)result1.GetValue("value");
                            HeiFeiXKItem.Add(item1);
                        }
                    }
                    if (HeiFeiXKItem.Count == 0)
                    {
                        HeiFeiXKMsg.Visibility = Visibility.Visible;
                    }
                    
                    JArray cfInfo = (JArray)rows["xzcf"];

                    foreach (var cfInfoDetail in cfInfo)
                    {
                        foreach (JObject result2 in cfInfoDetail)
                        {
                            HeiFeiList item2 = new HeiFeiList();
                            item2.Field = (string)result2.GetValue("field");
                            item2.Value = (string)result2.GetValue("value");
                            HeiFeiCFItem.Add(item2);
                        }
                    }
                    if (HeiFeiCFItem.Count == 0)
                    {
                        HeiFeiCFMsg.Visibility = Visibility.Visible;
                    }

                    JArray RedInfo = (JArray)rows["hongmd"];

                    foreach (var RedInfoDetail in RedInfo)
                    {
                        foreach (JObject result3 in RedInfoDetail)
                        {
                            HeiFeiList item3 = new HeiFeiList();
                            item3.Field = (string)result3.GetValue("field");
                            item3.Value = (string)result3.GetValue("value");
                            HeiFeiRedItem.Add(item3);
                        }
                    }
                    if (HeiFeiRedItem.Count == 0)
                    {
                        HeiFeiRedMsg.Visibility = Visibility.Visible;
                    }

                    JArray BlackInfo = (JArray)rows["heimd"];

                    foreach (var BlackInfoDetail in BlackInfo)
                    {
                        foreach (JObject result4 in BlackInfoDetail)
                        {
                            HeiFeiList item4 = new HeiFeiList();
                            item4.Field = (string)result4.GetValue("field");
                            item4.Value = (string)result4.GetValue("value");
                            HeiFeiBlackItem.Add(item4);
                        }
                    }
                    if (HeiFeiBlackItem.Count == 0)
                    {
                        HeiFeiBlackMsg.Visibility = Visibility.Visible;
                    }


                    JArray xycnInfo = (JArray)rows["xycn"];

                    foreach (var xycnInfoDetail in xycnInfo)
                    {
                        foreach (JObject result5 in xycnInfoDetail)
                        {
                            HeiFeiList item5 = new HeiFeiList();
                            item5.Field = (string)result5.GetValue("field");
                            item5.Value = (string)result5.GetValue("value");
                            HeiFeiCommitItem.Add(item5);
                        }
                    }
                    if (HeiFeiCommitItem.Count == 0)
                    {
                        HeiFeiCommitMsg.Visibility = Visibility.Visible;
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
            catch(Exception)
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
                    InFoTitle.Content =  "基本信息";

                    break;
                case "Red":
                    Panel.SetZIndex(RedInfoShow, ChangeNo);
                    InFoTitle.Content =  "红名单";

                    break;
                case "Black":
                    Panel.SetZIndex(BlackInfoShow, ChangeNo);
                    InFoTitle.Content =   "黑名单";

                    break;
                case "XK":
                    Panel.SetZIndex(XKInfoShow, ChangeNo);
                    InFoTitle.Content =   "行政许可";

                    break;
                case "CF":
                    Panel.SetZIndex(CFInfoShow, ChangeNo);
                    InFoTitle.Content =   "行政处罚";

                    break;
                case "CN":
                    Panel.SetZIndex(CommitInfoShow, ChangeNo);
                    InFoTitle.Content =   "信用承诺";

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

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
    }

    public class HeiFeiList: ListItem
    {
        public string Field { get; set; }

        public string Value { get; set; }
    }
}
