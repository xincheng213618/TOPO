using BaseUtil;
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
using System.Windows.Threading;
using System.Xml;

namespace PEC
{
    /// <summary>
    /// ReportGRPage.xaml 的交互逻辑
    /// </summary>
    public partial class ReportGRPage : Page
    {
        public ReportGRPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Acrobat.pdfControl.BeginInit();
            formsHost.Child = Acrobat.pdfControl;
            Acrobat.pdfControl.EndInit();

            switch (Global.Related.PageType)
            {
                case "ProvincialLYG":
                case "ProvincialPeople":
                case "ReportSuZhou":
                    Thread thread = new Thread(() => Requests1());
                    thread.Start();
                    break;

            }

        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private void Requests1()
        {
            string response = Http.Provincial.GetGRReport(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo);
            Dispatcher.BeginInvoke(new Action(() => ProvincialPeoplePhrase(response)));
        }

        private void ProvincialPeoplePhrase(string response)
        {
            if (response != null)
            {
                try
                {
                    JObject JsonData = (JObject)JsonConvert.DeserializeObject(response);
                    string resultCode = JsonData["resultCode"].ToString();
                    //连云港查不到数据时；
                    if (resultCode != "1")
                    {
                        switch (Global.Related.PageType)
                        {
                            case "ProvincialLYG":
                                PDF.DrawPDF("1.pdf", Global.Related.IDCardData);
                                PDF.SetPdfBackground("1.pdf", "sample.pdf");
                                Acrobat.pdfControl.LoadFile("sample.pdf");
                                PrintUtilWindow printUtil = new PrintUtilWindow(3);
                                printUtil.Closed += PrintOver;
                                printUtil.Show();
                                Acrobat.pdfControl.printAll();

                                break;
                            case "ProvincialPeople":
                                Content = "查询不到个人报告数据";
                                Pages();
                                break;
                        }

                    }
                    else
                    {
                        JObject data = (JObject)JsonConvert.DeserializeObject(JsonData["data"].ToString());
                        string filePath = "Cache\\" + (string)data.GetValue("bgbh") + ".pdf";
                        string bs64 = (string)data.GetValue("bgwj");
                        Covert.Base64ToFile(bs64, filePath);
                        
                        switch (Global.Related.PageType)
                        {
                            case "GRReportSuZhou":
                                string response1 = Http.SuZhou.Upload(filePath);
                                XmlDocument document = new XmlDocument();
                                document.LoadXml(response1);
                                string Code = document.SelectSingleNode("//SEAL_DOC_RESPONSE/RET_CODE").InnerText;
                                if (Code == "1")
                                {
                                    string returnmsg = document.SelectSingleNode("//SEAL_DOC_RESPONSE/FILE_LIST/FILE/FILE_URL").InnerText;
                                    Requests.Downloade(returnmsg, filePath);
                                }
                                break;
                        }

                        Acrobat.pdfControl.LoadFile(filePath);
                        PrintUtilWindow printUtil = new PrintUtilWindow(10);
                        printUtil.Closed += PrintOver;
                        WaitShow.Visibility = Visibility.Hidden;
                        printUtil.Show();
                        Acrobat.pdfControl.printAll();
                    }
                }
                catch(Exception ex)
                {
                    Log.WriteException(ex);
                    Content = new HomePage("接口解析错误");
                    Pages();
                }

            }
            else
            {
                Content = new HomePage("接口连接错误");
                Pages();
            }
        }
        private void PrintOver(object sender, EventArgs e)
        {
            Content = new PrintTips();
            Pages();
        }




    }
}
