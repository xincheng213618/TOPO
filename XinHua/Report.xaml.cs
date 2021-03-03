using BaseDLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
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

namespace XinHua
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    public partial class Report : Page
    {
        private string CompanyName = null;
        private string CompanyID = null;
        private string USCI = null;

        public Report(string CompanyName,string CompanyID,string USCI)
        {
            this.CompanyName = CompanyName;
            this.CompanyID = CompanyID;
            this.USCI = USCI;
            InitializeComponent();
        }
        public Report (IDCardData iDCardData)
        {
            InitializeComponent();
        }



        private void Page_Initialized(object sender, EventArgs e)
        {
            WaitShow.Visibility = Visibility.Visible;
            switch (Global.PageType)
            {
                case "XinHuaPrint":
                    hintLabel.Content = "正在生成报告";        
                    break;
                case "CreditChina":
                    hintLabel.Content = "下载信用报告中";
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
                case "CreditChina":
                    bool Success = Http.GetCreditchinaReport(CompanyName, USCI, "Temp//" + CompanyName + ".pdf");
                    Dispatcher.BeginInvoke(new Action(() => ReportCreditChina(Success)));
                    break;
                default:
                    break;
            }
        }
        private void ReportCreditChina(bool Success)
        {
            if (Success)
            {
                Content = new Pdfshow("Temp//" + CompanyName + ".pdf");
                Pages();
            }
            else
            {
                Content = new HomePage("无法获取该企业的信用报告");
                Pages();
            }
        }



        private void ReportXinHua(string response)
        {

            if (response != null)
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
            else
            {
                Content = new HomePage("接口连接错误，返回值为null");
                Pages();
            }

        }
        private void RequestUrl1(string FileName, string filePath = null)
        {
            
            switch (Global.PageType)
            {
                case "XinHuaPrint":
                    bool Sucess = Http.GetXinHuaReport(FileName, filePath);
                    Dispatcher.BeginInvoke(new Action(() => ReportToPDFShow(Sucess, filePath)));
                    break;
                default:
                    break;
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
}
