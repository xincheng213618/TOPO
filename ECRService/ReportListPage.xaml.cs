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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }



    /// <summary>
    /// ReportListPage.xaml 的交互逻辑
    /// </summary>
    public partial class ReportListPage : Page
    {
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
                    Content = new HomePage();
                    Pages();
                }

                if (mainWidownData.Countdown % 15 == 0)
                {
                    Media.Player(6);
                }
            });
        }

        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }


        private ObservableCollection<ReportItem> reportItems = new ObservableCollection<ReportItem>() { };

        private void GetReportListCompleted(string response)
        {
            listView.ItemsSource = reportItems;
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
                        reportItems.Add(item);                 
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
                Log.Write(ex.Message);
                Content = new HomePage("接口解析错误");
                Pages();
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
            Dispatcher.BeginInvoke(new Action(() => GetReportListCompleted(response)));
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();

        }

        private void PrintPDFFile(ReportItem item)
        {
            hintLabel.Content = $"正在打印{item.Identifier}……";

            AcrobatHelper.pdfControl.LoadFile($"Temp\\{item.Identifier}.pdf");
            AcrobatHelper.pdfControl.printAll();

            Webservice.NanJing.updatereportstatus(item.Identifier, "1");

            printCount++;

            if (downloadCount == printCount)
            {
                Content = new HomePage("打印完成，请取走您的报告");
                Pages();
            }
        }

        private void DownloadAndPrintReport(ReportItem item)
        {      
            Dispatcher.BeginInvoke(new Action(() => hintLabel.Content = $"正在下载企业信用报告{item.Identifier}……"));

            if (!File.Exists($"{item.Identifier}.pdf"))
            {
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
                            ZipFileExtensions.ExtractToDirectory(archive, "Temp");
                        }
                    }

                    Thread.Sleep(4000);
                    Dispatcher.BeginInvoke(new Action(() => PrintPDFFile(item)));
                }
                else if (returncode.CompareTo("e") == 0)
                {
                    Content = new HomePage(returnmsg);
                    Pages();
                }
            }

        }

        private void PrintClick_ProcessProc()
        {
            try
            {
                pageTimer.IsEnabled = false;

                for (int i = 0; i < reportItems.Count; i++)
                {
                    if (reportItems[i].IsSelected)
                    {
                        downloadCount++;
                        reportItems[i].isPrinted = true;
                        DownloadAndPrintReport(reportItems[i]);
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
            Content = new PrintIndexPage();
            Pages();
        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
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
    }
}
