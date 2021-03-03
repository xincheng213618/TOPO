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
using System.Xml;
using EXCResources;
namespace EXC
{
    /// <summary>
    /// ReportNingYang.xaml 的交互逻辑
    /// </summary>
    public partial class ReportNingYang : Page
    {

        private IDCardData idcardData;
        public ReportNingYang()
        {
            if (IDCardInfo.IDCardNo != null)
                idcardData.IDCardNo = IDCardInfo.IDCardNo;
            InitializeComponent();
        }
        public ReportNingYang(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }
        private ObservableCollection<ReportItem> ReportItem = new ObservableCollection<ReportItem>();
        private void Page_Initialized(object sender, EventArgs e)
        {
            IDCardInfo.IDCardNo = idcardData.IDCardNo;

            PopLabel.Content = "正在查询中";
            PopBorder.Visibility = Visibility.Visible;

            Thread worker = new Thread(() => Requests());
            worker.IsBackground = true;
            worker.Start();
        }

        private void Requests()
        {
            if (GlobalData.PageType == "ReportGRNingYang")
            {
                GlobalData.PageType = null;
                Dispatcher.BeginInvoke(new Action(() => NingYangGR()));
                return;
            }
            string response = Webservice.NingYang.GetReportList(idcardData.IDCardNo);
            Dispatcher.BeginInvoke(new Action(() => Pharse(response)));
        }
        private void NingYangGR()
        {
            string response = Webservice.NingYang.GetReportGR(idcardData.IDCardNo);

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

        private void Pharse(string response)
        {
            PopBorder.Visibility = Visibility.Hidden;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);//加载xml

            string code = xml.SelectSingleNode("//data/returncode").InnerText;
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
                    item.Applicant = xn["fddbrxm"].InnerText; ;
                    item.ApplicationData = xn["applytime"].InnerText;
                    ReportItem.Add(item);
                }
            }
            else
            {
                ReportMsg.Visibility = Visibility.Visible;
            }
        }

        private void ReportListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(ReportItem.ElementAt(ReportListView.SelectedIndex).xh.ToString());
            string CompanyName = ReportItem.ElementAt(ReportListView.SelectedIndex).ComPanyName.ToString();
            string FileName = ReportItem.ElementAt(ReportListView.SelectedIndex).FileName.ToString();

            string filePath = "Temp\\" + FileName + ".pdf";
            if (File.Exists(filePath))
            {
                GlobalData.PageType = "ReportNingYang";
                Content = new Pdfshow(filePath);
                Pages();
                return ;
            }

            string response = Webservice.NingYang.GetReport(FileName);

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
                GlobalData.PageType = "ReportNingYang";
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
