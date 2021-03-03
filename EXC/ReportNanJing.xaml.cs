using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using EXCResources;

namespace EXC
{
    /// <summary>
    /// ReportNanJing.xaml 的交互逻辑
    /// </summary>
    public partial class ReportNanJing : Page
    {
        private IDCardData idcardData;
        public ReportNanJing()
        {
            if (IDCardInfo.IDCardNo != null)
            {
                idcardData.IDCardNo = IDCardInfo.IDCardNo;
                idcardData.Name = IDCardInfo.Name;
            }
            InitializeComponent();
        }
        public ReportNanJing(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }

        private ObservableCollection<ReportItem> ReportItem = new ObservableCollection<ReportItem>();
        private void Page_Initialized(object sender, EventArgs e)
        {
            IDCardInfo.IDCardNo = idcardData.IDCardNo;
            IDCardInfo.Name = idcardData.Name;
            PopLabel.Content = "正在查询中";
            PopBorder.Visibility = Visibility.Visible;

            Thread worker = new Thread(() => Requests());
            worker.IsBackground = true;
            worker.Start();

        }

        private void NanJingGR()
        {
            string response = Webservice.NanJing.GetReport(idcardData.IDCardNo);

            PopBorder.Visibility = Visibility.Hidden;

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
        private void Msg(string mes)
        {
            Content = new HomePage(mes);
            Pages();
        }
        private void Requests()
        {
            if (GlobalData.PageType == "ReportGRNingYang")
            {
                GlobalData.PageType = null;
                Dispatcher.BeginInvoke(new Action(() => NanJingGR()));
                return;
            }

            Dispatcher.BeginInvoke(new Action(() => NanJingComapnyReport()));

        }
        private void NanJingComapnyReport()
        {
            string response = Webservice.NanJing.GetReportList(idcardData.Name, idcardData.IDCardNo);
            //string response = "<?xml version=\"1.0\" encoding=\"utf-8\"?><data><returncode>1</returncode><returnmsg>调用成功</returnmsg><result><report><id>XYBG0100200312023</id><qymc>南京杰翔冷暖设备有限公司</qymc><tyshxydm>913201043023030311</tyshxydm><zzjgdm>30230303-1</zzjgdm><fddbr>王绪祥</fddbr><jbr>王绪祥</jbr><sqrq>2020-03-12</sqrq></report></result></data>";

            PopBorder.Visibility = Visibility.Hidden;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);//加载xml

            string code = xml.SelectSingleNode("//data/returncode").InnerText;
            string returnmsg = xml.SelectSingleNode("//data/returnmsg").InnerText;

            if (code == "0")
            {
                ReportMsgLabel.Content = returnmsg;
                ReportMsg.Visibility = Visibility.Visible;
                return;
            }
            if (code == "1")
            {
                ReportListView.ItemsSource = ReportItem;
                int xh = 0;
                XmlNodeList xnList = xml.SelectNodes("//data/result/report");
                foreach (XmlNode xn in xnList)
                {
                    ReportItem item = new ReportItem();
                    xh = xh + 1;
                    item.xh = xh;
                    item.ComPanyName = xn["qymc"].InnerText;
                    item.FileName = xn["id"].InnerText;
                    item.Applicant = xn["tyshxydm"].InnerText; ;
                    item.ApplicationData = xn["sqrq"].InnerText;
                    ReportItem.Add(item);
                }
            }
            else
            {
                Content = new HomePage(returnmsg);
                Pages();
            }
           
        }

        private void ReportListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string CompanyName = ReportItem.ElementAt(ReportListView.SelectedIndex).ComPanyName.ToString();
            string FileName = ReportItem.ElementAt(ReportListView.SelectedIndex).FileName.ToString();
            string filePath = "Temp\\" + FileName + ".pdf";
            if (File.Exists(filePath))
            {
                GlobalData.PageType = "ReportNanJing";
                Content = new Pdfshow(filePath);
                Pages();
                return;
            }
            string response = Webservice.NanJing.GetReport(FileName);

            XmlDocument document = new XmlDocument();
            document.LoadXml(response);
            string Code = document.SelectSingleNode("//data/returncode").InnerText;
            string returnmsg =document.SelectSingleNode("//data/returnmsg").InnerText;

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
                GlobalData.PageType = "ReportNanJing";
                Content = new Pdfshow(Directory.GetCurrentDirectory() + "\\Temp\\" + name + ".pdf");
                Pages();
            }
            else
            {
                Content = new HomePage(returnmsg);
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
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
    }
}
