using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BaseDLL;
using System.Net;
using BaseUtil;

using System.Windows.Threading;
using TimeCount = BaseUtil.TimeCount;

namespace EXC
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    public partial class Report : Page
    {
        private IDCardData idcardData;
        private string CompanyID;

        public Report()
        {
            InitializeComponent();
        }



        CompayQueryListItem compayQueryListItem;

        public Report(CompayQueryListItem compayQueryListItem)
        {
            this.compayQueryListItem = compayQueryListItem;
            InitializeComponent();
        }

        public Report(IDCardData idcardData, string TemplateID, string CompanyID)
        {
            this.CompanyID = CompanyID;
            this.idcardData = idcardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.idcardData = Global.iDCard;
            Countdown_timer();
            PopLabel.Content = "正在查询报告中";
            PopBorder.Visibility = Visibility.Visible;
            Thread worker = new Thread(() => RequestUrl());
            worker.IsBackground = true;
            worker.Start();
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
        private void RequestUrl()
        {
            string response;
            switch (Global.PageType)
            {
                case "ReportGRWeiHai":
                    response = Http.HeFei.GetGRReport(idcardData.Name, idcardData.IDCardNo, Covert.FileToBase64(idcardData.PhotoFileName));
                    Dispatcher.BeginInvoke(new Action(() => ReportGRWeiHai(response)));
                    break;
                case "ReportGRWeiHaiHBF":
                    response = Http.HeFei.GetGRReportHBF(idcardData.Name, idcardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportGRWeiHai(response)));
                    break;
                case "ReportWeiHai":
                    response = Http.HeFei.GetReport(compayQueryListItem.CompanyName ?? "", compayQueryListItem.CompanyID ?? "", compayQueryListItem.USCI ?? "", "1");
                    Dispatcher.BeginInvoke(new Action(() => ReportWeiHai(response)));
                    break;

                case "sss":
                    break;
            }
        }


        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();

        private void ReportGRWeiHai(string response)
        {
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(response);

                if ((string)jObject.GetValue("code") == "1")
                {
                    string FileName = "Temp\\" + idcardData.Name + ".pdf";
                    if (Covert.Base64ToFile((string)jObject.GetValue("data"), FileName))
                    {
                        Content = new Pdfshow(FileName);
                        Pages();
                    }
                    else
                    {
                        Content = new Pdfshow("pdf 转换失败");
                        Pages();
                    }

                }
                else
                {
                    Global.HomeErrorText = (string)jObject.GetValue("msg");
                    Content = new HomePage();
                    Pages();
                }
            }
            catch
            {
                Global.HomeErrorText = "接口解析错误";
                Content = new HomePage();
                Pages();
            }
        }
        private void ReportWeiHai(string response)
        {
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(response);

                if ((string)jObject.GetValue("code") == "1")
                {
                    string FileName = "Temp\\" + compayQueryListItem.CompanyName + ".pdf";
                    if (Covert.Base64ToFile((string)jObject.GetValue("data"), FileName))
                    {
                        Content = new Pdfshow(FileName);
                        Pages();
                    }
                    else
                    {
                        Content = new Pdfshow("pdf 转换失败");
                        Pages();
                    }
                }
                else
                {
                    Global.HomeErrorText = (string)jObject.GetValue("msg");
                    Content = new HomePage();
                    Pages();
                }
            }
            catch
            {
                Global.HomeErrorText = "接口解析错误";
                Content = new HomePage();
                Pages();
            }
        }

        private void HomeClcik(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void ReportListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }



}
