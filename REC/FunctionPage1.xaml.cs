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

        public FunctionPage1()
        {
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
            string response = Http.GetInfo(Global.Related.IDCardData.IDCardNo, Global.Related.IDCardData.Name, Global.Related.transtionId);
            Dispatcher.BeginInvoke(new Action(() => GetPhrase(response)));
        }

        /// <summary>
        /// 房屋信息
        /// </summary>
        private ObservableCollection<RECData> RECListViewItem = new ObservableCollection<RECData>();
        int RECListNo = 0;
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
                                RECData Item = new RECData();
                                RECListNo += 1;
                                Item.ListNo = RECListNo ;
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
                                Item.QLQTZK=(string)result.GetValue("QLQTZK");
                                Item.QT = (string)result.GetValue("QT");
                                Item.FJ = (string)result.GetValue("FJ");
                                Item.HandlingStatus = (string)result.GetValue("SFZT");
                                //Item.HST = (string)result.GetValue("HST");
                                //Item.HSTSucess= Covert.Base64ToJpeg(Item.HST, "Temp\\HST.jpg");
                                Item.SLBH = (string)result.GetValue("SLBH");
                                Item.PROID = (string)result.GetValue("PROID");
                                Item.ZSID = (string)result.GetValue("ZSID");
                                bool show = false;

                                //重读字段不显示
                                foreach (RECData items in RECListViewItem)
                                {
                                    show = Item.BDCQZH == items.BDCQZH;
                                }

                                //证明字段不显示
                                if ((string)result.GetValue("ZSTYPE") == "证明")
                                {
                                    show = true;
                                }

                                if (!show)
                                {
                                    Item.FileName = "Temp\\" + Item.QLR + RECListNo.ToString() + "show.pdf";
                                    Item.PrintName = "Temp\\Print" + RECListNo.ToString() + ".pdf"; 
                                    RECListViewItem.Add(Item);
                                }
                            }
                            if (RECListViewItem.Count == 0)
                            {
                                Content = new HomePage("查询到已经被过滤到的信息，请联系C13号窗口工作人员");
                                Pages();
                            }
                            Media.Play("Base\\Media\\请选择您要打印的证书.mp3");
                        }
                        else
                        {
                            Content = new HomePage("查询接口异常，请联系C13号窗口工作人员");
                            Pages();
                        }
                    }
                    else
                    {
                        Content = new HomePage("查询接口异常，请联系C13号窗口工作人员");
                        Pages();
                    }
                }
                catch
                {
                    Media.Play("Base\\Media\\查询不到身份证和流水号对应的信息.mp3");
                    Content = new HomePage("查询不到身份证和流水号对应的信息");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("查询接口异常，请联系C13号窗口工作人员");
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
                    Pages();
                    break;
                case "Return":
                    Content = new FunctionPage();
                    Pages();
                    break;
                default:
                    break;
            }
        }

        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedIndex>=0)
            {
                PDF.DrawPDF(RECListViewItem[ListView.SelectedIndex].FileName, RECListViewItem[ListView.SelectedIndex]);
                PDF.DrawPrintPDF(RECListViewItem[ListView.SelectedIndex].PrintName, RECListViewItem[ListView.SelectedIndex]);
                Content = new FunctionPage2( RECListViewItem[ListView.SelectedIndex]);
                Pages();
            }
        }
    }





}
