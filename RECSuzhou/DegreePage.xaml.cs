using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace RECSuzhou
{
    /// <summary>
    /// DegreePage.xaml 的交互逻辑
    /// </summary>
    public partial class DegreePage : Page
    {
        public DegreePage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            TotalLabel.Content = Global.Related.IDCardData.Name + TotalLabel.Content;
            Countdown_timer();
            WaitShow.Visibility = Visibility.Visible;
            Thread thread = new Thread(() => DegreeList())
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void DegreeList()
        {
            string response = Http.DegreeList(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo);
            Dispatcher.BeginInvoke(new Action(() => DegreeListParse(response)));
        }

        private int DegreeListNum = 0;
        private ObservableCollection<HouseItem> HouseItem = new ObservableCollection<HouseItem>();
        private void DegreeListParse(string response)
        {
            if (response != null)
            {
                WaitShow.Visibility = Visibility.Hidden;
                try
                {
                    DegreeListView.ItemsSource = HouseItem;
                    JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                    if (jsons["code"].ToString() == "0")
                    {
                        JArray houselist = (JArray)jsons["houselist"];
                        if (houselist.Count > 0)
                        {
                            foreach (JObject house in houselist)
                            {
                                HouseItem item = new HouseItem();
                                item.ListNo = ++DegreeListNum;
                                item.Location = (string)house.GetValue("zl");
                                item.HouseID = (string)house.GetValue("fwbm");                              
                                HouseItem.Add(item);
                            }
                        }
                        else
                        {
                            DegreeMsg.Visibility = Visibility.Visible;
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
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void DegreeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WaitShow.Visibility = Visibility.Visible;
            if (DegreeListView.SelectedIndex >= 0)
            {
                Time.Countdown = 60;
                string houseId = HouseItem.ElementAt(DegreeListView.SelectedIndex).HouseID.ToString();
                string zuoluo = HouseItem.ElementAt(DegreeListView.SelectedIndex).Location.ToString();
                WaitShow.Visibility = Visibility.Visible;
                Thread tread2 = new Thread(() => Degree(houseId, zuoluo))
                 {
                     IsBackground = true
                  };
                 tread2.Start();
          
            }
        }
        private void Degree(string HouseID,string ZuoLuo)
        {
            string response = Http.Degree(Global.Related.IDCardData.Name,Global.Related.IDCardData.IDCardNo, "207", ZuoLuo);
            Dispatcher.BeginInvoke(new Action(() => DegreeParse(response)));
        }

        private void DegreeParse(string response)
        {
            WaitShow.Visibility = Visibility.Visible;
            if (response != null) 
            {
                try
                {                  
                    JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                    if (jsons["code"].ToString() == "0")
                    {
                        string filePath = "Temp\\" + jsons["filename"].ToString();
                        string reportcontent = jsons["report"].ToString();
                        if (reportcontent != null)
                        {
                            Covert.Base64ToFile(reportcontent, filePath);
                        }
                        Http.AddAction(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo, "dayinxuewei");
                        Content = new Pdfshow(filePath);
                        Pages();
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
}
