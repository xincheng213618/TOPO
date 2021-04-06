using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing.Printing;
using System.Xml;
using System.ComponentModel;
using System.Runtime.InteropServices;
using ECRService.ServiceReferenceRecord;
using System.Drawing;
using System.Security.Cryptography;

namespace ECRService
{
    public class RecordQueryListItem
    {
        public string image { get; set; }
       
    }
    /// <summary>
    /// RecordViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class RecordViewPage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;
        private CancellationTokenSource tokenSource = null;
        private BitmapImage bitmap;
        private FileStream m_ImageStream;
        private int no = 0;
        private int pageNo = 0;
        private int pageSize = 0;
        private string[] strRecords;
        private List<string> fileNameList = null;
        private Timer timer;
        private Thread worker_PrinterStatus = null;

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

        public RecordViewPage()
        {
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
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new PrintIndexPage())));
                    
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

        private void Page_Initialized(object sender, EventArgs e)
        {
            strRecords = new string[] { };
           
            tokenSource = new CancellationTokenSource();
            hintLabel.Content = "正在下载查档报告……";
            Thread worker2 = new Thread(() => DownloadRecord(tokenSource.Token));
            worker2.Start();
        }

        private void DownloadRecord(CancellationToken token)
        {
            try
            {
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = 16777216;
                binding.SendTimeout = TimeSpan.FromSeconds(30);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);

                CreditreportDelegate reportService = new CreditreportDelegateClient(binding, endpointAddress);
                string response = reportService.receiveResult(Global.Config.LoginName, Global.Config.LoginPassword,"", Id);

                Dispatcher.BeginInvoke(new Action(() => GetRecordCompleted(response)));
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                {

                }
            }
        }
        private void GetRecordCompleted(string response)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);

            XmlElement result = (XmlElement)document.SelectSingleNode("//result");

            string code = null;
            if (result != null)
            {
                code = result.GetAttribute("code");

                if (code != null && code.Equals("0"))
                {
                    XmlNodeList recordList = document.SelectNodes("//result/content");

                    if (recordList != null)
                    {
                        if (recordList.Count>0)
                        {
                            List<string> records = new List<string>();
                            fileNameList = new List<string>();
                            foreach (XmlNode record in recordList)
                            {
                                ++no;
                                string filename = record["filename"].InnerText;
                                string filedata = record["filedata"].InnerText;

                                var contents = Convert.FromBase64String(filedata);
                                string imagePath = Directory.GetCurrentDirectory() + "\\" + filename;


                                Base64ToImg(filedata).Save(imagePath);
                             
                                records.Add(imagePath);
                                fileNameList.Add(filename);
                            }
                            strRecords = records.ToArray();

                            string imageFilePath = strRecords[0];
                            m_ImageStream = new FileStream(imageFilePath, FileMode.Open);

                            bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.StreamSource = m_ImageStream;
                            bitmap.EndInit();
                            imagetxt.Source = bitmap;
                            scrollViewer.Visibility = Visibility.Visible;
                            hintBorder.Visibility = Visibility.Hidden;
                            border.Visibility = Visibility.Hidden;

                            pageSize = recordList.Count;
                            hintBorder2.Visibility = Visibility.Visible;
                            hintLabel2.Content = "第 " + (pageNo+1) + " 共 " + pageSize + " 页";
                        }
                        else
                        {
                            IsEnabled = false;
                            hintLabel.Content = "暂无档案报告可以查看，请申请后再试！";
                            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new PrintIndexPage())));
                        }
                    }
                }
            }
        }
        //解析base64编码获取图片
        private Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if (m_ImageStream != null)
            {
                m_ImageStream.Close();
                m_ImageStream.Dispose();
            }
            IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new PrintIndexPage())));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            if (m_ImageStream != null)
            {
                m_ImageStream.Close();
                m_ImageStream.Dispose();
            }
            IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
            
        }
       
       
        //打印按钮
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            hintBorder.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            printButton.Visibility = Visibility.Hidden;
            hintLabel.Content = "正在打印档案报告";

            if (worker_PrinterStatus != null)
            {
                worker_PrinterStatus.Join();
                worker_PrinterStatus = null;
            }

            int isOK = 0;
            for (int i=0;i<pageSize;i++)
            {
                if (m_ImageStream != null)
                {
                    m_ImageStream.Close();
                    m_ImageStream.Dispose();
                }

               isOK =  printShow(strRecords[i]);
            }
            printButton.Visibility = Visibility.Hidden;

            if (isOK == 1)
            {
                timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompletedInfo())), null, TimeSpan.FromSeconds(3), Timeout.InfiniteTimeSpan);
            }
           
        }
        //打印完成
        private void PrintCompletedInfo()
        {
            timer.Dispose();

            hintLabel.Content = "打印完成，请取走档案报告！";
            
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompleted())), null, TimeSpan.FromSeconds(3), Timeout.InfiniteTimeSpan);
        }
        private void PrintCompleted()
        {            
            timer.Dispose();     
            IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));

        }
        //下一页
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (pageNo<(pageSize-1))
            {
                if (m_ImageStream != null)
                {
                    m_ImageStream.Close();
                    m_ImageStream.Dispose();
                }
                pageNo = ++pageNo;
                hintLabel2.Content = "第 " + (pageNo+1) + " 共 " + pageSize + " 页";
                string imageFilePath = strRecords[pageNo];
                m_ImageStream = new FileStream(imageFilePath, FileMode.Open);

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = m_ImageStream;
                bitmap.EndInit();
                imagetxt.Source = bitmap;
               
            }
            else
            {
                alert("已经是最后一页了");
            }
             
        }
        //上一页
        private void pre_Click(object sender, RoutedEventArgs e)
        {
            if (pageNo>0)
            {
                if (m_ImageStream != null)
                {
                    m_ImageStream.Close();
                    m_ImageStream.Dispose();
                }
                pageNo = --pageNo;
                hintLabel2.Content = "第 " + (pageNo+1) + " 共 " + pageSize + " 页";
                string imageFilePath = strRecords[pageNo];
                m_ImageStream = new FileStream(imageFilePath, FileMode.Open);

                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = m_ImageStream;
                bitmap.EndInit();
                imagetxt.Source = bitmap;
               
            }
            else
            {
                alert("已经是第一页了");
            }
           
        }

     
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private async void alert(string msg)
        {
            hintBorder.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            hintLabel.Content = msg;
            await Task.Delay(TimeSpan.FromSeconds(1));
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;
        }

        private int printShow(string url)
        {
            int isOK = 0;
            PrinterSettings settings = new PrinterSettings();
            PrintDocument pd = new PrintDocument();
            PrintController printController = new StandardPrintController();
            pd.PrintController = printController;
            pd.DefaultPageSettings.Landscape = true;
            PageSettings pageSetting = new PageSettings();
            pageSetting.Landscape = true;
            pd.DefaultPageSettings = pageSetting;
            pd.PrinterSettings = settings;
            //SetDefaultPrinter(@"\\192.200.200.115\HP LaserJet Professional M1219nf MFP");
            settings.DefaultPageSettings.Landscape = true;
            settings.PrinterName = DefaultPrinter();
            settings.PrintToFile = false;
            settings.Duplex = Duplex.Simplex;
            pd.PrinterSettings = settings;
            PaperSize ps = new PaperSize(url, 4, 9);
            ps.RawKind = 9;
            System.Drawing.Image image = null;
            pd.PrintPage += (s, args) =>
            {
                image = System.Drawing.Image.FromFile(url);
                System.Drawing.Rectangle m = args.PageBounds;
                
                if (image.Width < image.Height)
                    image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                if (image.Width >= image.Height)
                {
                    if ((double)image.Width / (double)image.Height <= (double)m.Width / (double)m.Height)
                    {
                        int w = (int)((double)image.Width / (double)image.Height * (double)m.Height);
                        int dx = (m.Width - w) / 2;
                        m.X = dx;
                        m.Y = 0;
                        m.Width = w;
                    }
                    else
                    {
                        int h = (int)((double)image.Height / (double)image.Width * (double)m.Width);
                        int dy = (m.Height - h) / 2;
                        m.X = 0;
                        m.Y = dy;
                        m.Height = h;
                    }
                }
                args.Graphics.DrawImage(image, m);
            };

            try
            {
                pd.Print();
                isOK = 1;
            }
            catch (Exception)
            {
                isOK = -1;
                throw;
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                }
            }
            return isOK;
        }

        private string CheckPrinterStatus()
        {
            string errorMessage = null;

            return errorMessage;
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
