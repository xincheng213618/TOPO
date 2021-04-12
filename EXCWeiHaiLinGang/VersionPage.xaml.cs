using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BaseDLL;
using BaseUtil;
    
using System.Windows.Threading;
using System.Linq;

namespace EXC
{
    /// <summary>
    /// VersionPage.xaml 的交互逻辑
    /// </summary>
    public partial class VersionPage : Page
    {
        IDCardData iDCardData;
        string CompanyID;
        public VersionPage()
        {
            InitializeComponent();
        }

        public VersionPage(IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
           
            InitializeComponent();
        }
        public VersionPage(IDCardData iDCardData, string CompanyID)
        {
            this.iDCardData = iDCardData;
            this.CompanyID = CompanyID;
            InitializeComponent();
        }



        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            PopBorder.Visibility = Visibility.Visible;
            InfoLabel.Content = "获取模板中";

            Thread worker = new Thread(() => RequestsUrl())
            {
                IsBackground = true
            };
            worker.Start();
        }

        private ObservableCollection<VersionItem> VersionItem = new ObservableCollection<VersionItem>();
        //private int VersionNo = 0;
        private void RequestsUrl()
        {
            //WebService.GetPersonInfo(iDCardData);//只是上传不做处理

            //JObject response = WebService.GetReportTemplate();
            //Dispatcher.BeginInvoke(new Action(() => Phrase(response)));
        }

        private void Phrase(JObject response)
        {
            PopBorder.Visibility = Visibility.Hidden;
            Media.Player(1);
            switch (Global.PageType)
            {
                case "ReportWeiHai":
                    InfoLabel.Content = "请选择企业信用报告模板";
                    break;
                case "ReportGRWeiHai":
                    InfoLabel.Content = "请选择个人信用报告模板";
                    ContentBorder1.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
            Global.CameraPass = true;
            if (!Global.CameraPass)
            {
                InfoLabel.Content = "人脸对比失败，仅提供简易模板";
            }

            if ((string)response.GetValue("code") == "0")
            { 
                JArray jArray = (JArray)JsonConvert.DeserializeObject((string)response.GetValue("data"));
                foreach (JObject result in jArray)
                {
                    VersionItem item = new VersionItem();
                    if ((string)result.GetValue("isenable") == "1")
                    {
                       
                        item.TemplateID = (string)result.GetValue("templateid");
                        item.TemplateName = (string)result.GetValue("templatename");
                    }
                }
                if (cGrid.Children.Count<1)
                {
                    Content = new HomePage("暂无可显示的报告");
                    Pages();
                }
             
            }
            else
            {
                Content = new HomePage((string)response.GetValue("msg"));
                Pages();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string TemplateID = VersionItem[int.Parse(button.Tag.ToString())].TemplateID; //这里正常应该做报错检测，但是由于数据时自动生成，因此不会出错

            switch (Global.PageType)
            {
                case "ReportWeiHai":
                    Content = new Report(iDCardData, TemplateID, CompanyID);
                    Pages();
                    break;
                case "ReportGRWeiHai":
                    //Content = new Report(iDCardData, TemplateID);
                    //Pages();
                    break;
            }

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

    }

}
