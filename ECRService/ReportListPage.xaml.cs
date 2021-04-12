using System;
using System.IO;
using System.IO.Compression;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Xml;
using BaseDLL;
using BaseUtil;

namespace ECRService
{
    public class ReportItem : INotifyPropertyChanged
    {
        public bool isPrinted = false;
        public string Identifier { get; set; }
        public string CompanyName { get; set; }
        public string FillingDate { get; set; }

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged("IsSelected"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }



    /// <summary>
    /// ReportListPage.xaml 的交互逻辑
    /// </summary>
    public partial class ReportListPage : Page
    {
        private ObservableCollection<ReportItem> items;
        private bool printing = false;
        public bool isPageTimer = false;
        private int downloadCount;
        private int printCount;
        private Timer timer;

        IDCardData iDCardData = new IDCardData();

        public ReportListPage(IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
            listView.ItemsSource = items;
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            DataContext = mainWidownData;
            AcrobatHelper.pdfControl = new AxAcroPDFLib.AxAcroPDF();
            AcrobatHelper.pdfControl.BeginInit();
            formsHost.Child = AcrobatHelper.pdfControl;
            AcrobatHelper.pdfControl.EndInit();

            hintLabel.Content = "正在获取企业信用报告列表……";
            Thread worker = new Thread(() => GetReportListProc());
            worker.Start();
        }
        private DispatcherTimer pageTimer = null;
        private MainWidownData mainWidownData = new MainWidownData() { Countdown = 90 };
        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--mainWidownData.Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }

                if (mainWidownData.Countdown % 15 == 0)
                {
                    Media.Player(6);
                }
            });
        }


        private void GetReportListCompleted(string response)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(response);

                string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;

                if (returncode.CompareTo("1") == 0)
                {
                    XmlNodeList xnList = document.SelectNodes("//data/result/report");
                    foreach (XmlNode xn in xnList)
                    {
                        ReportItem item = new ReportItem();

                        item.Identifier = xn["id"].InnerText;
                        item.CompanyName = xn["qymc"].InnerText;
                        item.FillingDate = xn["sqrq"].InnerText;
                        items.Add(item);                 
                    }
                }
                else if (returncode.CompareTo("0") == 0)
                {
                    pageTimer.IsEnabled = false;
                }
                else if (returncode.CompareTo("e") == 0)
                {
                    throw (new Exception(returnmsg));
                }
            }
            catch (Exception ex)
            {

            }

            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;
            listView.Visibility = Visibility.Visible;
            exitButton.Visibility = Visibility.Visible;

            Media.Player(6);
        }

        private void GetReportListProc()
        {
            string response = Webservice.NanJing.GetReportList(iDCardData.IDCardNo, "1");
            //string response =  reportService.getreportlist(Global.Config.LoginName, Global.Config.LoginPassword, iDCardData.IDCardNo, "1");
            // string response = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><data><returncode>1</returncode><returnmsg>调用成功</returnmsg><result><report><id>ABC0000000001</id><qymc>江苏同袍信息科技有限公司</qymc><sqrq>2017-09-09</sqrq></report></result></data>";
            Dispatcher.BeginInvoke(new Action(() => GetReportListCompleted(response)));
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
        }

        private void LoadPDFFile(ReportItem item)
        {
            AcrobatHelper.pdfControl.LoadFile($"{item.Identifier}.pdf");
        }

        private void PrintPDFFile(ReportItem item)
        {
            hintLabel.Content = $"正在打印{item.Identifier}……";
            AcrobatHelper.pdfControl.printAll();

            Webservice.NanJing.updatereportstatus(item.Identifier, "1");

            printCount++;

            if (downloadCount == printCount)
            {
                Dispatcher.BeginInvoke(new Action(() => PrintingJobsCompleted()));
            }
        }

        private void DownloadAndPrintReport(ReportItem item)
        {
            
            Dispatcher.BeginInvoke(new Action(() => hintLabel.Content = $"正在下载企业信用报告{item.Identifier}……"));

            if (!File.Exists($"{item.Identifier}.pdf"))
            {
                //System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                //binding.MaxReceivedMessageSize = 16777216;
                //binding.SendTimeout = TimeSpan.FromSeconds(30);
                //binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                //System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                //CreditreportDelegate reportService = new CreditreportDelegateClient(binding, endpointAddress);
                //string response = reportService.getreport(Global.Config.LoginName, Global.Config.LoginPassword, item.Identifier, "", "1");

                string response = Webservice.NanJing.GetReport(item.Identifier, "", "1");
                XmlDocument document = new XmlDocument();
                document.LoadXml(response);

                string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;
                if (returncode.CompareTo("1") == 0)
                {
                    string report = document.SelectSingleNode("//data/result/report/content").InnerText;
                    byte[] zip = Convert.FromBase64String(report);

                    using (Stream stream = new MemoryStream(zip))
                    {
                        using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                        {
                            ZipFileExtensions.ExtractToDirectory(archive, ".");
                        }
                    }
                }
                else if (returncode.CompareTo("0") == 0)
                {
                }
                else if (returncode.CompareTo("e") == 0)
                {
                    throw new Exception(returnmsg);
                }
            }

            Thread.Sleep(4000);
            Dispatcher.BeginInvoke(new Action(() => LoadPDFFile(item)));

            Thread.Sleep(4000);
            Dispatcher.BeginInvoke(new Action(() => PrintPDFFile(item)));

        }

        private void PrintingJobsCompleted()
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].isPrinted)
                {
                    items.RemoveAt(i);
                }
            }

            printing = false;

            exitButton.Visibility = Visibility.Visible;

            Media.Player(7);

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompletedInfo())), null, TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);
        }

        private void PrintCompletedInfo()
        {
            timer.Dispose();

            hintLabel.Content = "打印完成，请取走企业信用报告！";

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => ClearPrintCompletedInfo())), null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }

        private void ClearPrintCompletedInfo()
        {
            timer.Dispose();
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            if (items.Count == 0)
            {
                pageTimer.IsEnabled = false;
                Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
            }

            if (isPageTimer)
            {
                pageTimer.IsEnabled = true;
            }
            
        }

        private void PrintClick_ProcessProc()
        {
            try
            {
                isPageTimer = false;
                pageTimer.IsEnabled = false;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].IsSelected)
                    {
                        downloadCount++;
                        items[i].isPrinted = true;
                        DownloadAndPrintReport(items[i]);
                    }
                }

                if (downloadCount == 0)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        printing = false;
                        isPageTimer = true;
                        pageTimer.IsEnabled = true;

                        exitButton.Visibility = Visibility.Visible;

                        hintLabel.Content = "请先选择报告，再点击“打印”按钮！";
                        hintBorder.Visibility = Visibility.Visible;
                        border.Visibility = Visibility.Hidden;
                    }));
                }

                isPageTimer = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (printing)
                return;


            printing = true;
            exitButton.Visibility = Visibility.Hidden;
            hintBorder.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;

            downloadCount = 0;
            printCount = 0;

            
            Thread worker = new Thread(() => PrintClick_ProcessProc());
            worker.Start();
        }

        private void ListView_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            ReportItem item = (ReportItem)((ListViewItem)sender).Content;
            if (item.IsSelected)
                item.IsSelected = false;
            else
                item.IsSelected = true;
        }

        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new PrintIndexPage())));
        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
