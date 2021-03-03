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
using Resources;
using BaseDLL;

namespace EXC
{
    /// <summary>
    /// OwnerShipPage.xaml 的交互逻辑
    /// </summary>
    public partial class OwnerShipPages : Page
    {
        private IDCardData idcardData;
        public OwnerShipPages(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }
        public OwnerShipPages()
        {
            idcardData.Name=IDCardInfo.Name;
            idcardData.IDCardNo =IDCardInfo.IDCardNo;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            OwnerShipLabel.Content = idcardData.Name.Replace(" ", "") + OwnerShipLabel.Content;
            Countdown_timer();
            PopLabel.Content = "正在查询中";
            PopBorder.Visibility = Visibility.Visible;
            Thread worker2 = new Thread(() => OwnerShip());
            worker2.IsBackground = true;
            worker2.Start();

        }
        private void Msg(string meg)
        {

        }
        private void OwnerShip()
        {
            string response = Http.RealEstate.OwnerShip(idcardData.Name.Trim(), idcardData.IDCardNo.Trim());

            Dispatcher.BeginInvoke(new Action(() => Parse(response)));

        }
        private int OwnerShipNum = 0;
        private ObservableCollection<HouseItem> HouseItem = new ObservableCollection<HouseItem>();
        private void Parse(string response)
        {
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => Msg("该接口连接错误")));
                return;
            }
            PopBorder.Visibility = Visibility.Hidden;
            try
            {
                OwnerShipListView.ItemsSource = HouseItem;
                JObject jsons = (JObject)JsonConvert.DeserializeObject(response);


                switch (jsons["code"].ToString())
                {
                    case "0":
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
                                string reportcontent= (string)house.GetValue("report");
                                item.Visible = reportcontent != null ? "Visible" : "Hidden";
                                if (reportcontent != null)
                                {
                                    XCovert.Base64ToFile(reportcontent, item.FilePath);
                                }
                                item.FilePage = (string)house.GetValue("pageNumber");
                                HouseItem.Add(item);
                            }
                        }
                        else    
                        {
                            OwnerShipMsg.Visibility = Visibility.Visible;
                        }
                        break;

                    default:
                        Content = new HomePage(jsons["Message"].ToString());
                        Pages();
                        break;
                }
            }
            catch 
            {
                Content = new HomePage("该接口解析错误");
                Pages();

            }

        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        //页面转跳
        private void Pages()
        {
            //Function.FilesDelete(filenames);
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        ////定时器
        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public Times Time = new Times();


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

        private void OwnerShipListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (HouseItem.ElementAt(OwnerShipListView.SelectedIndex).Visible== "Visible")
            {
                //上传打印行动
                Http.RealEstate.AddAction(idcardData.Name, idcardData.IDCardNo, "dayinquanshu");

                string FileName = HouseItem.ElementAt(OwnerShipListView.SelectedIndex).FilePath.ToString();
                IDCardInfo.Name = idcardData.Name;
                IDCardInfo.IDCardNo = idcardData.IDCardNo;
                Content = new Pdfshow(FileName);
                Pages();
            }
           else
            {   
                MessageBox.Show("暂无此报告的PDF,请至房屋所属区域不动产档案中心窗口打印");
            }

        }
    }
}
