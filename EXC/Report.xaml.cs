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
using BaseDLL;
using BaseUtil;


namespace EXC
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

        private void Page_Initialized(object sender, EventArgs e)
        {
            WaitShow.Visibility = Visibility.Visible;
            switch (Global.Related.PageType)
            {
                case "YiXingBanch":
                case "YiXingPerson":
                    InfoLabel.Content = "正在查询不动产证明信息";
                    break;
                case "XinHuaPrint":
                    hintLabel.Content = "正在生成报告";
                    InfoLabel.Content = "正在获取报告";
                    break;
                default:
                    InfoLabel.Content = "正在下载报告";
                    break;
            }
            Thread worker = new Thread(() => RequestUrl());
            worker.IsBackground = true;
            worker.Start();
        }
        private void RequestUrl()
        {
            string response;
           
            switch (Global.Related.PageType)
            {
                case "ReportNingYang":
                    response = Webservice.NingYang.GetReportList(Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportNingYang(response)));
                    break;
                case "ReportGRNanJing":
                    Global.Related.PageType = "";
                    response = Webservice.NanJing.GetReport(Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportGRNanJing(response)));
                    break;
                case "ReportNanJing":
                    response = Webservice.NanJing.GetReportList(Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportNanJing(response)));
                    break;
                case "ReportGRNingYang":
                    Global.Related.PageType = "";
                    response = Webservice.NingYang.GetReportGR(Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportGRNingYang(response)));
                    break;
                case "ReportNingYangAll":
                    response = Webservice.NingYang.GetReportList("");
                    Dispatcher.BeginInvoke(new Action(() => ReportNingYang(response)));
                    break;
                case "ReportXinTai":
                    response = Webservice.XinTai.GetReportList(Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportXinTai(response)));
                    break;
                case "ReportGRXinTai":
                    Global.Related.PageType = "";
                    response = Webservice.XinTai.GetReportGR(Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportGRXinTai(response)));
                    break;
                case "ReportGRWeiFang":
                    try
                    {
                        response = Webservice.WeiFang.GetReportList(Global.Related.IDCardData.IDCardNo);
                    }
                    catch
                    {
                        response = null;
                    }
                    Dispatcher.BeginInvoke(new Action(() => ReportGRXinTai(response)));
                    break;
                case "ReportHuangShi":
                    response = Webservice.HuangShi.GetReportList(Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportHuangShi(response)));
                    break;
                case "YiXingNew":
                case "YiXingPerson":
                    response = Http.YinXingNew.DJZL(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo);
                    Dispatcher.BeginInvoke(new Action(() => ReportPersonYinXing(response)));
                      
                    break;

            }
        }

        #region 数据一段解析

        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();
        private void ReportNingYang(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            ReportGrid.Visibility = Visibility.Visible;//要把表格显示出来
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
                    xh += 1;
                    item.ListNo = xh;
                    item.CompanyName = xn["qymc"].InnerText;
                    item.FileName = xn["id"].InnerText;
                    item.Applicant = xn["fddbrxm"].InnerText; ;
                    item.ApplicationData = xn["applytime"].InnerText;
                    CompanyReportItem.Add(item);
                }
            }
            else
            {
                ReportMsgLabel.Content = returnmsg;
                ReportMsg.Visibility = Visibility.Visible;
            }
        }
        private void ReportNanJing(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            ReportGrid.Visibility = Visibility.Visible;//要把表格显示出来
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
        /// <summary>
        /// 宜兴 表格数据更改
        /// </summary>
        /// <param name="response">请求的List数据</param>
        private void ReportPersonYinXing(string response)
        {
            InfoLabel.Content = "不动产信息";

            WaitShow.Visibility = Visibility.Hidden;
            InfoLabelTile2.Content = "产权证号";
            InfoLabelTile3.Content = "坐落";
            InfoLabelTile4.Content = "不动产类型";

            ReportGrid.Visibility = Visibility.Visible;//要把表格显示出来

            if (response == null)
            {
                Content = new HomePage("接口连接错误，返回值为null");
                Pages();
                return;
            }
            try
            {
                JObject resObjs = (JObject)JsonConvert.DeserializeObject(response);
                ReportListView.ItemsSource = CompanyReportItem;
                string code = (string)resObjs.GetValue("code");
                if (code == "0")
                {
                    JArray resultArray = (JArray)resObjs["data"];
                    int xh = 0;
                    foreach (JObject result in resultArray)
                    {
                        CompanyReportItem item = new CompanyReportItem();
                        item.ListNo = ++xh;
                        item.Applicant = (string)result.GetValue("CQZH");//不动单元号
                                                                         //item.BRN = (string)result.GetValue("FWDM");
                        item.CompanyName = (string)result.GetValue("ZL");//坐落
                        item.ApplicationData = (string)result.GetValue("BDCLX");
                        item.BRN = (string)result.GetValue("FWDM");

                        CompanyReportItem.Add(item);
                    }
                    if (CompanyReportItem.Count == 0)
                    {
                        Content = new HomePage("暂无数据");
                        Pages();
                    }
                }
                else
                {
                    Content = new HomePage((string)resObjs.GetValue("msg"));
                    Pages();
                }
            }
            catch
            {
                Content = new HomePage("暂无数据");
                Pages();
            }
           
        }

        private void ReportHuangShi(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            ReportGrid.Visibility = Visibility.Visible;//要把表格显示出来
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
                    item.FileName = xn["reportnumber"].InnerText;
                    //item.Applicant = xn["tyshxydm"].InnerText; ;
                    item.ApplicationData = xn["applytime"].InnerText;
                    CompanyReportItem.Add(item);
                }
            }
            else
            {
                ReportMsgLabel.Content = returnmsg;
                ReportMsg.Visibility = Visibility.Visible;
            }
        }

        private void ReportXinTai(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            ReportGrid.Visibility = Visibility.Visible;//要把表格显示出来
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
                    item.Applicant = xn["fddbrxm"].InnerText; ;
                    item.ApplicationData = xn["applytime"].InnerText;
                    CompanyReportItem.Add(item);
                }
            }
            else
            {
                ReportMsgLabel.Content = returnmsg;
                ReportMsg.Visibility = Visibility.Visible;
            }
        }
        private void ReportGRNingYang(string response)
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
        private void ReportGRXinTai(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            if (response == null)
            {
                Content = new HomePage("接口连接错误，返回值为null");
                Pages();
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(response);
                string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;
                if (returncode.CompareTo("1") != 0 || returnmsg == "未找到符合条件的报告")
                {
                    Content = new HomePage(returnmsg);
                    Pages();
                }
                else
                {
                    string name = document.SelectSingleNode("//data/result/name").InnerText;
                    string report = document.SelectSingleNode("//data/result/report").InnerText;
                    string FileName = "Temp\\" + name + ".pdf";
                    if (report != "")
                    {
                        Covert.Base64ToFile(report, FileName);
                        Content = new Pdfshow(FileName);
                        Pages();
                    }
                    else
                    {
                        Content = new HomePage("报告生成失败");
                        Pages();
                    }
                    Log.Write(FileName);
                }
            }
            catch
            {
                Content = new HomePage("查不到数据");
                Pages();
            }
        }
        #endregion
        private void HomeClcik(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        /// <summary>
        /// 列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hintLabel.Content = "正在下载报告请稍等";
            WaitShow.Visibility = Visibility.Visible;
            try
            {
                switch (Global.Related.PageType)
                {
                    case "YiXingPerson":
                    case "YiXingBanch":
                        string CQZH = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).Applicant.ToString();//不动产权证书

                        WaitShow.Visibility = Visibility.Visible;

                        Thread therad = new Thread(() => RequestsYiXing(CQZH))
                        {
                            IsBackground = true
                        };
                        therad.Start();

                        break;
                    default:
                        string CompanyName = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).CompanyName.ToString();

                        string FileName = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).FileName.ToString();
                        string filePath = "FileName.pdf";
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
                        break;
                }
            }
            catch(Exception ex)
            {
                Log.WriteException(ex);
            }
         
        }

        private void RequestsYiXing(string CQZH)
        {
            string FileName = "1.pdf";
            if (PDF.DrawYiXing1(FileName, Global.Related.IDCardData, CQZH))
            {
                Dispatcher.BeginInvoke(new Action(() => PDFShow(FileName)));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() => { WaitShow.Visibility = Visibility.Hidden; })); 
            }

        }

        private void PDFShow(string FileName)
        {
            WaitShow.Visibility = Visibility.Hidden;
            hintLabel.Content = " 正在绘制PDF";

            Content = new Pdfshow(FileName);
            Pages();
        }


        private void RequestUrl1(string FileName, string filePath = null)
        {
            string response;
            switch (Global.Related.PageType)
            {
                case "ReportNingYang":
                    response = Webservice.NingYang.GetReport(FileName);
                    Dispatcher.BeginInvoke(new Action(() => GetReportNingYang(response)));
                    break;
                case "ReportNanJing":
                    response = Webservice.NanJing.GetReport(FileName);
                    Dispatcher.BeginInvoke(new Action(() => GetReportNanJing(response)));
                    break;
                case "YiXingPerson":
                    response = Http.YiXing.GetPersonPDF(Global.Related.IDCardData,FileName);
                    Dispatcher.BeginInvoke(new Action(() => GetReportYiXing(response)));
                    break;
                case "YiXingBanch":
                    response = Http.YiXing.GetBankPDF(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo, FileName);
                    Dispatcher.BeginInvoke(new Action(() => GetReportYiXing(response)));
                    break;
                case "ReportXinTai":
                    response = Webservice.XinTai.GetReport(FileName);
                    Dispatcher.BeginInvoke(new Action(() => GetReportXinTai(response)));
                    break;
                case "ReportHuangShi":
                    response = Webservice.HuangShi.GetReport(FileName);
                    Dispatcher.BeginInvoke(new Action(() => GetReportXinTai(response)));
                    break;

            }
        }

        #region 数据解析2段
        private void GetReportNingYang(string response)
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

        private void GetReportXinTai(string response)
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

        private void GetReportYiXing(string response)
        {
            if (response == null)
            {
                Content = new HomePage("该接口连接错误");
                Pages();
            }
            try
            {
                JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                string code = (string)jsons.GetValue("code");

                if (code == "0")
                {
                    string pdfbs4 = (string)jsons.GetValue("report");
                    string filePath = "Temp//" + (string)jsons.GetValue("filename");
                    Covert.Base64ToFile(pdfbs4, filePath);
                    Content = new Pdfshow(filePath);
                    Pages();
                }
                else
                {
                    Content = new HomePage((string)jsons.GetValue("Message"));
                    Pages();
                }
            }
            catch
            {
                Content = new HomePage("该接口解析错误");
                Pages();
            }
        }


        #endregion

        private void ReportListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }



}
