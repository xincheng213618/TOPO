using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
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
using System.Xml;

namespace XinHua
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    public partial class Report : Page
    {
        public Report()
        {
            InitializeComponent();
        }
        private IDCardData idcardData;
        public Report(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            WaitShow.Visibility = Visibility.Visible;
            switch (Global.PageType)
            {
                case "XinHuaPrint":
                    hintLabel.Content = "正在生成报告";        
                    break;
                case "NanJingReport":
                    break;
                case "NanJingGRReport":
                    hintLabel.Content = "正在生成报告";
                    break;
                default:
                    break;
            }
            Thread worker = new Thread(() => RequestUrl());
            worker.IsBackground = true;
            worker.Start();
        }
        private void RequestUrl()
        {
            string response;
            switch(Global.PageType)
            {
                case "XinHuaPrint":
                    response = Http.XinHuaReport(CompanyInfo.CompanyName, CompanyInfo.CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => ReportXinHua(response)));
                    break;
                case "NanJingReport":
                    
                    response = Webservice.NanJing.GetReportList(idcardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportNanJing(response)));
                    break;
                case "NanJingGRReport":
                    Global.PageType = null;
                    response = Webservice.NanJing.GetReport(idcardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportGRNanJing(response)));
                    break;
                default:
                    break;
            }
           

        }
        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();
        private void ReportNanJing(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            reportList.Visibility = Visibility.Visible;
            if (response == null)
            {
                Content = new HomePage("接口连接错误，返回值为null");
                Pages();
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);//加载xml

            string code = xml.SelectSingleNode("//data/returncode").InnerText;
            string returnmsg = xml.SelectSingleNode("//data/returnmsg").InnerText;

            if (code == "1")
            {
                ReportListView.ItemsSource = CompanyReportItem;
                int xh = 0;

                XmlNodeList xnList = xml.SelectNodes("//data/result/report");
                foreach (XmlNode xn in xnList)
                {
                    CompanyReportItem item = new CompanyReportItem();
                    xh = xh + 1;
                    item.ListNo = xh;
                    item.CompanyName = xn["qymc"].InnerText;
                    item.FileName = xn["id"].InnerText;
                    item.Applicant = xn["tyshxydm"].InnerText; ;
                    item.ApplicationData = xn["sqrq"].InnerText;
                    CompanyReportItem.Add(item);
                }
            }
            else
            {
                ReportMsgLabel.Content = returnmsg;
                ReportMsg.Visibility = Visibility.Visible;
            }
        }
        private void ReportGRNanJing(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            if (response == null)
            {
                Content = new HomePage("接口连接错误，返回值为null");
                Pages();
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(response);
            string returncode = document.SelectSingleNode("//data/returncode").InnerText;
            string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;
            if (returncode.CompareTo("1") == 0)
            {
                string name = document.SelectSingleNode("//data/result/report/name").InnerText;
                string report = document.SelectSingleNode("//data/result/report/content").InnerText;
                byte[] zip = Convert.FromBase64String(report);

                using (Stream stream = new MemoryStream(zip))
                {
                    using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                    {
                        ZipFileExtensions.ExtractToDirectory(archive, "Temp");
                    }
                    stream.Close();
                }
                Content = new Pdfshow(Directory.GetCurrentDirectory() + "\\Temp\\" + name + ".pdf");
                Pages();
            }
            else
            {
                Content = new HomePage(returnmsg);
                Pages();
            }
        }
        private void ReportXinHua(string response)
        {
            if (response == null)
            {
                Content = new HomePage("接口连接错误，返回值为null");
                Pages();
            }
            else
            {
                try
                {
                    JObject resObj = (JObject)JsonConvert.DeserializeObject(response);

                    if ((resObj["stateCode"].ToString()).Equals("1"))
                    {
                        string FileName = resObj["data"].ToString();
                        string Filepath = "Temp\\" + CompanyInfo.CompanyName + ".pdf";

                        hintLabel.Content = "正在下载报告请稍等";
                        Thread worker1 = new Thread(() => RequestUrl1(FileName, Filepath));
                        worker1.IsBackground = true;
                        worker1.Start();
                    }
                    else
                    {
                        Content = new HomePage("新华报告路径获取错误");
                        Pages();
                    }
                }
                catch
                {
                    //Content = new HomePage("查询不到该企业的数据");
                    Content = new HomePage("企业报告生成中，请稍后重试");
                    Pages();
                }
            }

        }
        private void RequestUrl1(string FileName, string filePath = null)
        {

            string response;
            switch (Global.PageType)
            {
                case "XinHuaPrint":
                    bool Sucess = Http.GetXinHuaReport(FileName, filePath);
                    Dispatcher.BeginInvoke(new Action(() => ReportToPDFShow(Sucess, filePath)));
                    break;
                case "NanJingReport":
                    response = Webservice.NanJing.GetReport(FileName);
                    Dispatcher.BeginInvoke(new Action(() => GetReportNanJing(response)));
                    break;
                default:
                    break;
            }
        }
        private void GetReportNanJing(string response)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);
            string Code = document.SelectSingleNode("//data/returncode").InnerText;
            string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;

            if (Code == "1")
            {
                string name = document.SelectSingleNode("//data/result/report/id").InnerText;
                string report = document.SelectSingleNode("//data/result/report/content").InnerText;
                byte[] zip = Convert.FromBase64String(report);

                using (Stream stream = new MemoryStream(zip))
                {
                    using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                    {
                        ZipFileExtensions.ExtractToDirectory(archive, "Temp");
                    }
                    stream.Close();
                }
                Content = new Pdfshow(Directory.GetCurrentDirectory() + "\\Temp\\" + name + ".pdf");
                Pages();
            }
            else
            {
                Content = new HomePage(returnmsg);
                Pages();
            }
        }
        private void ReportToPDFShow(bool Sucess, string filepath)
        {
            if (Sucess) 
            {
                Global.PageType = null;
                Content = new Pdfshow(filepath);
            }
            else
            {
                Content = new HomePage("PDF下载失败");
            }

            Pages();
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void HomeClcik(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

       

        private void ReportListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
           

        }

        private void ReportListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hintLabel.Content = "正在下载报告请稍等";
            WaitShow.Visibility = Visibility.Visible;
            try
            {
                string CompanyName = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).CompanyName.ToString();

                string FileName = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).FileName.ToString();
                string filePath = "Temp\\" + FileName + ".pdf";
                if (File.Exists(filePath))
                {
                    Content = new Pdfshow(filePath);
                    Pages();
                    return;
                }
                Thread worker3 = new Thread(() => RequestUrl1(FileName, filePath))
                {
                    IsBackground = true
                };
                worker3.Start();

            }
            catch(Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //计时器模块
        private DispatcherTimer pageTimer = null;
        TimeCount Time = new TimeCount();

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

    public static class CompanyInfo
    {
        public static string CompanyID = null;
        public static string CompanyName = null;
        public static void Initialized()
        {
            CompanyID = null;
            CompanyName = null;
        }

    }
    public class CompanyReportItem : CompanyItem
    {
        public string Applicant { get; set; }//申请人
        public string ApplicantIDcardNo { get; set; }//申请人身份证号
        public string ApplicationData { get; set; }//申请日期
        public string FilePath { get; set; } //文件地址
    }
}
