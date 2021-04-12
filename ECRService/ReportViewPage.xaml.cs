using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Drawing.Printing;
using System.Xml;
using ECRService.ReportServiceReference;
using System.ComponentModel;
using System.Runtime.InteropServices;
using O2S.Components.PDFRender4NET;
using O2S.Components.PDFRender4NET.Printing;
using ECRService.CreditreportDelegate1;
using System.Security.Cryptography;

namespace ECRService
{
    /// <summary>
    /// ReportViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class ReportViewPage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        private ReportListPage returnPage = null;
        private SpecificMattersdetailsPage returnPage2 = null;
        private string identifier = null;
        private string companyName = null; 
        private ReportItem item = null;
        private int itemIndex = -1;
        private ObservableCollection<ReportItem> items = ServiceData.data as ObservableCollection<ReportItem>;

        private AxAcroPDFLib.AxAcroPDF pdfControl = null;
        //申请书截取
        string book = "";

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
        //页面跳转过来访问的构造方法
        public ReportViewPage(ReportListPage returnPage, string companyName, string identifier)
        {

            this.returnPage = returnPage;
            this.identifier = identifier;
            this.companyName = companyName;

            InitializeComponent();
            this.DataContext = this;

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage)));
                    (returnPage as ReportListPage).ResetTimer();
                }

                if (Countdown % 15 == 0 && book.Length == 0)
                {
                    media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/7、取走身份证和报告.mp3", UriKind.Absolute);
                    media.Position = TimeSpan.Zero;
                    media.Play();
                }
            });
            pageTimer.IsEnabled = true;
        }

        //页面跳转过来访问的构造方法
        public ReportViewPage(SpecificMattersdetailsPage returnPage2, string identifier)
        {
            book = Application.Current.Properties["apply"].ToString().Substring(1, Application.Current.Properties["apply"].ToString().Length - 1);
            this.returnPage2 = returnPage2;
            this.identifier = book;

            InitializeComponent();
            this.DataContext = this;

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                    book = null;
                    //Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage)));
                    //(returnPage as ReportListPage).ResetTimer();
                }

                if (Countdown % 15 == 0)
                {
                    media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/7、取走身份证和报告.mp3", UriKind.Absolute);
                    media.Position = TimeSpan.Zero;
                    media.Play();
                }
            });
            pageTimer.IsEnabled = true;
        }
        //页面初始化事件
        private void Page_Initialized(object sender, EventArgs e)
        {
            if (book != "")
            {
                pdfControl = new AxAcroPDFLib.AxAcroPDF();
                pdfControl.BeginInit();
                formsHost.Child = pdfControl;
                pdfControl.EndInit();

                hintLabel.Content = "正在下载" + book + "……";
                Thread worker2 = new Thread(() => DownloadBook(book));
                worker2.Start();
            }
            else
            {

                pdfControl = new AxAcroPDFLib.AxAcroPDF();
                pdfControl.BeginInit();
                formsHost.Child = pdfControl;
                pdfControl.EndInit();

                for (itemIndex = 0; itemIndex < items.Count; itemIndex++)
                {
                    if (items[itemIndex].Identifier.Equals(identifier))
                    {
                        item = items[itemIndex];
                        break;
                    }
                }

                item.IsSelected = !item.IsSelected;

                hintLabel.Content = "正在下载企业信用报告……";
                Thread worker = new Thread(() => DownloadReportThreadProc());
                worker.Start();
            }

        }
        //下载申请书
        private void DownloadBook(string book)
        {
            //多线程异步执行
            Dispatcher.BeginInvoke(new Action(delegate
            {

                hintBorder.Visibility = Visibility.Hidden;
                border.Visibility = Visibility.Hidden;
                //identifier = book;
                pdfControl.LoadFile(@"/ECRServiceLocal/ECRService/申请书/" + book+".pdf");
                pdfControl.setShowScrollbars(false);
                pdfControl.setShowToolbar(false);
                pdfControl.setView("FitH");
                formsHost.Visibility = Visibility.Visible;
               // formsHost2.Visibility = Visibility.Visible;

                hintBorder2.Visibility = Visibility.Visible;
                hintLabel2.Content = "上下滑动翻页";
            }));

        }
        private void DownloadReportCompleted()
        {
            //多线程异步执行
            Dispatcher.BeginInvoke(new Action(delegate
            {
                hintBorder.Visibility = Visibility.Hidden;
                border.Visibility = Visibility.Hidden;
                //identifier = book;
                pdfControl.LoadFile($"{ServiceData.reportID}.pdf");
                pdfControl.setShowScrollbars(false);
                pdfControl.setShowToolbar(false);
                pdfControl.setView("FitH");
                formsHost.Visibility = Visibility.Visible;
                //formsHost2.Visibility = Visibility.Visible;

                hintBorder2.Visibility = Visibility.Visible;
                hintLabel2.Content = "上下滑动翻页";
            }));

        }
        //下载文件
        private void DownloadReportThreadProc()
        {
            //文件不存在进入
            if (!File.Exists($"{identifier}.pdf"))
            {
                try
                {
                    System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                    binding.MaxReceivedMessageSize = 16777216;
                    binding.SendTimeout = TimeSpan.FromSeconds(30);
                    binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                    System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                    //ReportService reportService = new ReportServiceClient(binding, endpointAddress);
                    //string response = reportService.getreport(GlobalData.configData.LoginName, GlobalData.configData.LoginPassword, identifier, "", "1");
                    CreditreportDelegate reportService = new CreditreportDelegateClient(binding, endpointAddress);
                    string response = reportService.getreport(Global.Config.LoginName, Global.Config.LoginPassword, identifier, "", "1");

                    XmlDocument document = new XmlDocument();
                    document.LoadXml(response);

                    string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                    string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;
                    if (returncode.CompareTo("1") == 0)
                    {
                        string report = document.SelectSingleNode("//data/result/report/content").InnerText;
                        ServiceData.reportID = document.SelectSingleNode("//data/result/report/id").InnerText;
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

                        return;
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
            //文件存在就调用这个方法
            Dispatcher.BeginInvoke(new Action(() => DownloadReportCompleted()));
        }
        //返回上一页面
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.Dispose();
            if (book != "" )
            {
                pageTimer.IsEnabled = false;
                //Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new OfficePage())));
                (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage2);
                //returnPage.ResetTimer();
            }
            else
            {
                pageTimer.IsEnabled = false;
                //Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new OfficePage())));
                (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage);
                returnPage.ResetTimer();
            }
            
        }

        Timer timer = null;
        private SpecificMattersdetailsPage specificMattersdetailsPage;
        //打印按钮事件
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            //判断
            if (book!="")
            {
                border.Visibility = Visibility.Visible;
                //pdfControl.printAll();
                //开始打印
                //pdfControl.printAll();
                printShow(@"\ECRServiceLocal\ECRService\申请书\"+book+".pdf");

                printButton.Visibility = Visibility.Hidden;
                //items.RemoveAt(itemIndex);

                //string photo;
                //using (var stream = File.Open(ServiceData.idCardInfo.no + ".jpeg", FileMode.Open))
                //{
                //    int len = (int)stream.Length;
                //    Byte[] buffer = new Byte[len];
                //    stream.Read(buffer, 0, len);
                 //   photo = Convert.ToBase64String(buffer);
               // }

                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = 16777216;
                binding.SendTimeout = TimeSpan.FromSeconds(30);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                //ReportService reportService = new ReportServiceClient(binding, endpointAddress);
                //reportService.updatereportstatus(GlobalData.configData.LoginName, GlobalData.configData.LoginPassword, item.Identifier, photo, ServiceData.type.ToString());

                pageTimer.IsEnabled = false;

                hintLabel2.Content = $"正在打印{book}"+"……";
                timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompletedInfo())), null, TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);
            }
            else
            {
                border.Visibility = Visibility.Visible;
                pdfControl.printAll();

                printButton.Visibility = Visibility.Hidden;
                items.RemoveAt(itemIndex);

                string photo;
                using (var stream = File.Open(ServiceData.idCardInfo.no + ".jpeg", FileMode.Open))
                {
                    int len = (int)stream.Length;
                    Byte[] buffer = new Byte[len];
                    stream.Read(buffer, 0, len);
                    photo = Convert.ToBase64String(buffer);
                }

                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = 16777216;
                binding.SendTimeout = TimeSpan.FromSeconds(30);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                // ReportService reportService = new ReportServiceClient(binding, endpointAddress);
                ReportService reportService = new ReportServiceClient(binding, endpointAddress);
                reportService.updatereportstatus(Global.Config.LoginName, Global.Config.LoginPassword, item.Identifier, photo, ServiceData.type.ToString());

                pageTimer.IsEnabled = false;
                hintLabel2.Content = "正在打印企业信用报告……";
                timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompletedInfo())), null, TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);
            }
           
        }
        //打印完成
        private void PrintCompletedInfo()
        {
            timer.Dispose();
            hintLabel2.Content = "打印完成，请取走企业信用报告！";

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompleted())), null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }

        private void PrintCompleted()
        {
            pdfControl.Dispose();
            if ( book != "")
            {
                timer.Dispose();
                IsEnabled = false;
                book = null;
            }
            else
            {
                timer.Dispose();
                if (items.Count > 0)
                {
                    IsEnabled = false;
                    returnPage.ResetTimer();
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage)));
                }
                else
                {
                    
                    IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }
            }
            
        }
        //返回首页
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.Dispose();
            IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
            book = null;
        }
        //下一页
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.gotoNextPage();
        }
        //上一页
        private void pre_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.gotoPreviousPage();
        }

        private int printShow(string url)
        {
            int isOK = 0;
            PDFFile file = PDFFile.Open(url);
            PrinterSettings settings = new PrinterSettings();
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            PageSettings pageSetting = new PageSettings();
            pageSetting.Landscape = true;
            pd.DefaultPageSettings = pageSetting;
            pd.PrinterSettings = settings;
            settings.DefaultPageSettings.Landscape = true;
            settings.PrinterName = DefaultPrinter();
            settings.PrintToFile = false;
            settings.Duplex = Duplex.Simplex;
            pd.PrinterSettings = settings;

            PaperSize ps = new PaperSize(url, 4, 9);
            ps.RawKind = 9; 
            
            PDFPrintSettings pdfPrintSettings = new PDFPrintSettings(settings);
            pdfPrintSettings.PaperSize = ps;
            pdfPrintSettings.PageScaling = PageScaling.None;
            pdfPrintSettings.PrinterSettings.Copies = 1;

            try
            {
                file.Print(pdfPrintSettings);
                isOK = 1;
            }
            catch (Exception)
            {
                isOK = -1;
                throw;
            }
            finally
            {

                file.Dispose();
            }
            return isOK;
        }

        //C#如何获取本地的打印机列表并且指定默认打印机
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机  

        private static PrintDocument fPrintDocument = new PrintDocument();
        //获取本机默认打印机名称  
        public static String DefaultPrinter()
        {
            return fPrintDocument.PrinterSettings.PrinterName;
        }
        public static List<String> GetLocalPrinters()
        {
            List<String> fPrinters = new List<String>();
            fPrinters.Add(DefaultPrinter()); //默认打印机始终出现在列表的第一项  
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                {
                    fPrinters.Add(fPrinterName);
                }
            }
            return fPrinters;
        }

        //MD5加密
        public static string GetMD5Str(string toCryString)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(toCryString)));
        }
    }


}
