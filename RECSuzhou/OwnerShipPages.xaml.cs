using BaseDLL;
using BaseUtil;
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

namespace RECSuzhou
{
    /// <summary>
    /// OwnerShipPages.xaml 的交互逻辑
    /// </summary>
    public partial class OwnerShipPages : Page
    {
        public OwnerShipPages()
        {
            InitializeComponent();
        }


        private void Page_Initialized(object sender, EventArgs e)
        {
            TotalLabel.Content = Global.Related.IDCardData.Name + TotalLabel.Content;
            Countdown_timer();
            WaitShow.Visibility = Visibility.Visible;
            Thread thread = new Thread(() => OwnerShip())
            {
                IsBackground = true
            };
            thread.Start();

        }
        private void OwnerShip()
        {
            string response = Http.OwnerShip(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo);
            Dispatcher.BeginInvoke(new Action(() => OwnerShipParse(response)));
        }

        private int OwnerShipNum = 0;
        private ObservableCollection<HouseItem> HouseItem = new ObservableCollection<HouseItem>();
        private void OwnerShipParse(string response)
        {
            if (response != null)
            {
                WaitShow.Visibility = Visibility.Hidden;
                try
                {
                    OwnerShipListView.ItemsSource = HouseItem;
                    JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                    if (jsons["code"].ToString() == "0")
                    {
                        JArray houselist = (JArray)jsons["houselist"];
                        if (houselist.Count > 0)
                        {
                            foreach (JObject house in houselist)
                            {
                                HouseItem item = new HouseItem();
                                item.ListNo = ++OwnerShipNum;
                                item.Location = (string)house.GetValue("zuoluo");
                                item.HouseID = (string)house.GetValue("bianhao");
                                item.Name = (string)house.GetValue("name");
                                item.FilePath = Environment.CurrentDirectory + "\\Temp\\" + (string)house.GetValue("filename");
                                string reportcontent = (string)house.GetValue("report");
                                item.Visible = reportcontent != null ? "Visible" : "Hidden";
                                if (reportcontent != null)
                                {
                                    Covert.Base64ToFile(reportcontent, item.FilePath);
                                }
                                item.FilePage = (string)house.GetValue("pageNumber");
                                HouseItem.Add(item);
                            }
                        }
                        else
                        {
                            OwnerShipMsg.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        Content = new HomePage(jsons["Message"].ToString());
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("该接口解析错误");
                    Pages();

                }
            }
            else
            {
                Content = new HomePage("该接口连接错误");
                Pages();
            }


        }

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
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        //页面转跳
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private void OwnerShipListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OwnerShipListView.SelectedIndex > -1) 
            {
                if (HouseItem.ElementAt(OwnerShipListView.SelectedIndex).Visible == "Visible")
                {
                    Http.AddAction(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo, "dayinquanshu", "");

                    string FileName = HouseItem.ElementAt(OwnerShipListView.SelectedIndex).FilePath.ToString();
                    Content = new Pdfshow(FileName);
                    Pages();
                }
                else
                {
                    MessageBox.Show("暂无此报告的PDF,请至房屋所属区域不动产档案中心窗口打印");
                }
            }

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
    }
}
