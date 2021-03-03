using Resources;
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
using System.Xml;

namespace EXC
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    public partial class ReportWeiFang : Page
    {
        private IDCardData idcardData;

        public ReportWeiFang(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Thread worker = new Thread(() => RequestPerosonUrl());
            worker.IsBackground = true;
            worker.Start();
        }
        private void RequestPerosonUrl()
        {
            string response = Webservice.WeiFang.GetPersonInfo(idcardData.IDCardNo);
            Dispatcher.BeginInvoke(new Action(() => PhrasePerson(response)));
        }
        private void PhrasePerson(string response)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);

            string returncode = document.SelectSingleNode("//data/returncode").InnerText;
            string report = document.SelectSingleNode("//data/result/report").InnerText;
            if (report != "")
            {
                string ReportID = document.SelectSingleNode("//data/result/report/baseinfo/base//id").InnerText;

                Thread thread = new Thread(() => RequestPDFUrl(ReportID));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                Content = new HomePage("查不到该用户的报告");
                Pages();
            }
        }

        private void RequestPDFUrl(string ReportID, string Type ="1")
        {
            string response = Webservice.WeiFang.GetReportGR(ReportID, Type);
            Dispatcher.BeginInvoke(new Action(() => PhrasePersonGetPDF(response)));
        }

        private void PhrasePersonGetPDF(string response)
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
                Log.Write(FileName);
                XCovert.Base64ToFile(report, FileName);
                Content = new Pdfshow(FileName);
                Pages();
            }
        }




        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }






    }
}
