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
        private string TemplateID = "";

        public Report()
        {
            InitializeComponent();
        }

 

        //WeihaiGR
      
      

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.CompanyID = Global.WHDatas.  CompanyID;
            this.TemplateID = Global.WHDatas. TemplateID;
            this.idcardData = Global.WHDatas.CameraIdcard;
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
            
            switch (Global.PageType)
            {
                case "ReportWeiHai":
                    JObject jObject = WebService.ExportLegalPersonalPdf(idcardData, CompanyID, TemplateID);
                    Dispatcher.BeginInvoke(new Action(() => ReportGRWeiHai(jObject)));
                    break;
                case "ReportGRWeiHai":
                    jObject = WebService.Exportpersonalpdf(idcardData, TemplateID);
                    Dispatcher.BeginInvoke(new Action(() => ReportGRWeiHai(jObject)));
                    break;
            }

            
        
        }

        #region 数据一段解析

        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();
       
        private void ReportGRWeiHai(JObject response)
        {
            if ((string)response.GetValue("code") == "0")
            {
                string FilePath = "Temp//" + idcardData.Name + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                PopLabel.Content = "正在下载报告";

                Thread worker1 = new Thread(() => GetGRWeiHai((string)response.GetValue("data"), FilePath));
                worker1.IsBackground = true;
                worker1.Start();
            }
            else
            {
                Global.WHDatas.HomeError = (string)response.GetValue("msg");
                Content = new HomePage();
                Pages();
            }
        }
        private void GetGRWeiHai(string response, string FilePath)
        {
            bool Sucess = Downloade(response, FilePath);
           
            Dispatcher.BeginInvoke(new Action(() => ReportToPDFShow(Sucess, FilePath)));
        }


        private void ReportToPDFShow(bool Sucess, string FilePath)
        {
            if (Sucess)
            {
                PDF.PDFWeiHaiMark(FilePath, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                if (Global.PageType == "ReportWeiHai")
                {
                    Content = new Pdfshow(FilePath, AllowPrint: false);
                }
                else if (Global.PageType == "ReportGRWeiHai")
                {
                    Content = new Pdfshow(FilePath);
                }
            }
            else
            {
                
                Global.WHDatas.HomeError = "PDF下载失败";
                Content = new HomePage();
            }
            Pages();
        }

        public static bool Downloade(string url, string filePath)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                Stream stream = new FileStream(filePath, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return true;
            }
            catch
            {
                return false;
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
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

       


     
        private void ReportListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }



}
