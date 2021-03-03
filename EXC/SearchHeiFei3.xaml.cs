using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace EXC
{
    /// <summary>
    /// SearchHeiFei3.xaml 的交互逻辑
    /// </summary>
    public partial class SearchHeiFei3 : Page
    {
        string XdrMc;
        string ID;
        public SearchHeiFei3(string XdrMc,string ID)
        {
            this.XdrMc = XdrMc;
            this.ID = ID;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.DataContext = Time;
            Countdown_timer();
            WaitShow.Visibility = Visibility.Visible;

            switch (Global.PageType)
            {
                case "HeiFeiRed":
                    InfoTitleLabel.Content = XdrMc + "红名单详情";
                    break;

                case "HeiFeiBlack":
                    InfoTitleLabel.Content = XdrMc + "黑名单详情";
                    break;

                case "HeiFeiXK":
                    InfoTitleLabel.Content = XdrMc + "信用许可详情";
                    break;

                case "HeiFeiCF":
                    InfoTitleLabel.Content = XdrMc + "信用处罚详情";
                    break;

                default:
                    break;
            }

            Thread thread = new Thread(() => Requests())
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void Requests()
        {
            string response;
            switch (Global.PageType)
            {
                case "HeiFeiRed":
                    response = Http.HeFei.GetRedListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiDetail(response)));
                    break;

                case "HeiFeiBlack":
                    response = Http.HeFei.GetBlackListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiDetail(response)));
                    break;

                case "HeiFeiXK":
                    response = Http.HeFei.GetLicensingListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiDetail(response)));
                    break;

                case "HeiFeiCF":
                    response = Http.HeFei.GetSanctionListByNameAndCode(XdrMc, ID);
                    Dispatcher.BeginInvoke(new Action(() => HeiFeiDetail(response)));
                    break;

                default:
                    break;
            }
        }

        private ObservableCollection<HeiFeiDetail> HeiFeiDetailItem = new ObservableCollection<HeiFeiDetail>();
        private void HeiFeiDetail(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            HeiFeiListView.ItemsSource = HeiFeiDetailItem;
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                bool falg = (bool)@object.GetValue("flag");
                if (falg)
                {
                    JArray jArray = (JArray)@object["rows"];
                    if (jArray != null && !jArray.Equals("") && jArray.Count != 0)
                    {
                        foreach (JObject result in jArray)
                        {
                            switch (Global.PageType)
                            {
                                case "HeiFeiRed":
                                    HeiFeiDetail item = new HeiFeiDetail();
                                    item.Title = (string)result.GetValue("slyj");
                                    item.Content = (string)result.GetValue("cfjljg");
                                    item.Department = (string)result.GetValue("fbbm");
                                    item.Date = (string)result.GetValue("jzrq");

                                    HeiFeiDetailItem.Add(item);
                                    break;
                                case "HeiFeiBlack":
                                    HeiFeiDetail item1 = new HeiFeiDetail();
                                    item1.Title = (string)result.GetValue("slyj");
                                    item1.Content = (string)result.GetValue("cfjljg") ;
                                    item1.Department = (string)result.GetValue("fbbm");
                                    item1.Date = (string)result.GetValue("jzrq");
                                    HeiFeiDetailItem.Add(item1);
                                    break;
                                case "HeiFeiXK":
                                    HeiFeiDetail item2 = new HeiFeiDetail();
                                    item2.Title = (string)result.GetValue("jdswh");
                                    item2.Content = (string)result.GetValue("xmmc");
                                    item2.Department = (string)result.GetValue("bmmc");
                                    item2.Date = (string)result.GetValue("jdrq");
                                    HeiFeiDetailItem.Add(item2);
                                    break;
                                case "HeiFeiCF":

                                    HeiFeiDetail item3 = new HeiFeiDetail();
                                    item3.Title = (string)result.GetValue("jdswh");
                                    item3.Content = (string)result.GetValue("cfmc");
                                    item3.Department = (string)result.GetValue("bmmc");
                                    item3.Date = (string)result.GetValue("jdrq");
                                    HeiFeiDetailItem.Add(item3);
                                    break;
                            }
                        }
                        if (HeiFeiDetailItem.Count == 0)
                        {
                            HeiFeiListViewMsg.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
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
                Content = new HomePage("接口解析错误，请联系开发人员");
                Pages();
            }
        }





        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public Times Time = new Times();

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
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }

    public class HeiFeiDetail
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Department { get; set; }
        public string Date { get; set; }
    }
}
