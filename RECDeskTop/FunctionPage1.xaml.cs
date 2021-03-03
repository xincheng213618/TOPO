using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace REC
{
    /// <summary>
    /// FunctionPage1.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPage1 : Page
    {

        IDCardData idCardData ;
        string TranstionId;//流水号
        public FunctionPage1(IDCardData idCardData,string TranstionId)
        {
            this.idCardData = idCardData;
            this.TranstionId = TranstionId;
            InitializeComponent();
        }

        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount();
        private void Page_Initialized(object sender, EventArgs e)
        {
            TopGrid.DataContext = timeCount;
            Countdown();
            WaitShow.Visibility = Visibility.Visible;

            Thread worker = new Thread(() => Requests())
            {
                IsBackground = true
            };
            worker.Start();
        }
        private void Requests()
        {
            string response = Http.GetInfo(idCardData, TranstionId);
            Thread.Sleep(500);
            Dispatcher.BeginInvoke(new Action(() => GetPhrase(response)));

        }
        private ObservableCollection<RECListView> RECListViewItem = new ObservableCollection<RECListView>();
        int RECListNo = 1;
        private void GetPhrase(string response)
        {
            WaitShow.Visibility = Visibility.Hidden; 
            ListView.ItemsSource = RECListViewItem;
            if (response != null)
            {
                try
                {
                    JObject RECResponse = (JObject)JsonConvert.DeserializeObject(response);
                    string code = (string)RECResponse.GetValue("code");
                    if (code.Equals("0"))
                    {
                        JArray resultArray = (JArray)RECResponse.GetValue("data");
                        if (resultArray.Count != 0)
                        {
                            foreach (JObject result in resultArray)
                            {
                                RECListView Item = new RECListView();
                                Item.ListNo += RECListNo ;
                                Item.QLR = (string)result.GetValue("QLR");
                                Item.QLRZJH = (string)result.GetValue("QLRZJH");
                                Item.GYQK = (string)result.GetValue("GYQK");
                                Item.ZL = (string)result.GetValue("ZL");
                                Item.BDCDYH = (string)result.GetValue("BDCDYH");
                                Item.BDCQZH = (string)result.GetValue("BDCQZH");
                                Item.QLLX = (string)result.GetValue("QLLX");
                                Item.QLXZ = (string)result.GetValue("QLXZ");
                                Item.SYQX = (string)result.GetValue("SYQX");
                                Item.YT = (string)result.GetValue("YT");
                                Item.MJ = (string)result.GetValue("MJ");
                                Item.QT = (string)result.GetValue("QT");
                                Item.HandlingStatus = (string)result.GetValue("SFZT");
                                Item.Cost = "200";
                                Covert.Base64ToJpeg((string)result.GetValue("HST"), "1.jpg");
                                Item.FileName = Item.QLR +".pdf";
                                PDF.DrawPDF(Item.FileName, Item);

                                RECListViewItem.Add(Item);
                            }
                        }
                        else
                        {
                            Content = new HomePage("查无此数据");
                            Pages();
                        }
                    }
                    else
                    {
                        Content = new HomePage((string)RECResponse.GetValue("Message"));
                        Pages();
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



        private void Countdown()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1), };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown <= 0)
                {
                    Content = new HomePage(); //这里超时返回主界面
                    Pages();
                }
            });
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    break;
                case "Return":
                    Content = new FunctionPage(idCardData);
                    break;
                default:
                    break;
            }
            Pages();
        }

        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedIndex>=0)
            {
                string FilePath = RECListViewItem.ElementAt(ListView.SelectedIndex).FileName;
                Content = new FunctionPage2(idCardData, FilePath);
                Pages();
            }
        }
    }





}
