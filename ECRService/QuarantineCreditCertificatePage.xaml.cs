using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml;
using ECRService.ReportServiceReference;
using System.ComponentModel;

namespace ECRService
{
    /// <summary>
    /// QuarantineCreditCertificatePage.xaml 的交互逻辑
    /// </summary>
    public partial class QuarantineCreditCertificatePage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        private QuarantineCreditQueryPage returnPage = null;
        private QuarantineCreditQueryCondition condition = null;
        private string identifier = null;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        private AxAcroPDFLib.AxAcroPDF pdfControl = null;

        public int Countdown
        {
            get { return countdown; }
            set { countdown = value; OnPropertyChanged("Countdown"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int Kinds = 2;
        public QuarantineCreditCertificatePage(QuarantineCreditQueryPage returnPage)
        {
            this.returnPage = returnPage;
            InitializeComponent();
            this.DataContext = this;

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    returnPage.ResetTimer();
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }
            });
        }

        private void DownloadReportCompleted()
        {
            hintBorder.Visibility = Visibility.Hidden;
            printButton.Visibility = Visibility.Visible;

            pdfControl.LoadFile($"{identifier}.pdf");
            pdfControl.setShowScrollbars(false);
            pdfControl.setShowToolbar(false);
            pdfControl.setView("FitH");
            formsHost.Visibility = Visibility.Visible;
            formsHost2.Visibility = Visibility.Visible;
            //pdfControl.CausesValidation = false;
            //pdfControl.IsAccessible = false;

            hintBorder2.Visibility = Visibility.Visible;
            hintLabel2.Content = "上下滑动翻页";
        }

        private void DownloadReportThreadProc()
        {
            if (!File.Exists($"{identifier}.pdf"))
            {
                try
                {
                    System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                    binding.MaxReceivedMessageSize = 16777216;
                    binding.SendTimeout = TimeSpan.FromSeconds(30);
                    binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                    System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                    ReportService reportService = new ReportServiceClient(binding, endpointAddress);

                    string response = reportService.getreport(Global.Config.LoginName, Global.Config.LoginPassword, condition.Record, condition.Institution, Kinds.ToString());

                    XmlDocument document = new XmlDocument();
                    document.LoadXml(response);

                    string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                    string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;
                    if (returncode.CompareTo("1") == 0)
                    {
                        identifier = document.SelectSingleNode("//data/result/report/id").InnerText;
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
                catch (Exception ex)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        pageTimer.IsEnabled = false;

                    }));
                    return;
                }
            }

            Dispatcher.BeginInvoke(new Action(() => DownloadReportCompleted()));
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            pdfControl = new AxAcroPDFLib.AxAcroPDF();
            pdfControl.BeginInit();
            formsHost.Child = pdfControl;
            pdfControl.EndInit();

            hintLabel.Content = "正在下载出入境检验检疫企业信用等级证明……";
            Thread worker = new Thread(() => DownloadReportThreadProc());
            worker.Start();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.Dispose();
            pageTimer.IsEnabled = false;
            returnPage.ResetTimer();
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage)));
        }

        private Timer timer;
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.printAll();

            printButton.Visibility = Visibility.Hidden;
            

            System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
            binding.MaxReceivedMessageSize = 16777216;
            binding.SendTimeout = TimeSpan.FromSeconds(30);
            binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
            System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
            ReportService reportService = new ReportServiceClient(binding, endpointAddress);
            reportService.updatereportstatus(Global.Config.LoginName, Global.Config.LoginPassword, identifier, "", "1");
            
            pageTimer.IsEnabled = false;
            hintLabel2.Content = "正在打印出入境检验检疫企业信用等级证明……";
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompletedInfo())), null, TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);
        }

        private void PrintCompletedInfo()
        {
            timer.Dispose();

            hintLabel2.Content = "打印完成，请取走出入境检验检疫企业信用等级证明！";

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompleted())), null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }

        private void PrintCompleted()
        {
            pdfControl.Dispose();
            timer.Dispose();

            returnPage.ResetTimer();
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage)));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.Dispose();
            IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.gotoNextPage();
        }

        private void pre_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.gotoPreviousPage();
        }
    }
}
