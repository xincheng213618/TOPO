using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BaseDLL;
using BaseUtil;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PEC
{
    /// <summary>
    /// ReportPage.xaml 的交互逻辑
    /// </summary>
    public partial class ReportPage : Page
    {
        IDCardData iDCardData;
        string LoginName;
        string mobanId;
        public ReportPage(IDCardData iDCardData, string LoginName)
        {
            this.iDCardData = iDCardData;
            this.LoginName = LoginName;
            InitializeComponent();
        }
        public ReportPage(IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
        }
        public ReportPage()
        {
            LoginName = ReportData.LoginName;
            iDCardData = new IDCardData() { 
            Name=ReportData.Name,
            IDCardNo=ReportData.IDCardNo,  };
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            QiYeList.Visibility = Visibility.Visible;
            WaitShow.Visibility = Visibility.Visible;
            Thread thread = new Thread(() => Requests1());
            thread.Start();
        }

        bool Printover = false;
        public void PrintStart(string filePath)
        {
            Acrobat.pdfControl.BeginInit();
            formsHost.Child = Acrobat.pdfControl;
            Acrobat.pdfControl.EndInit();
            Acrobat.pdfControl.LoadFile(filePath);
            Acrobat.pdfControl.printAllFit(true);
            PrintAlready = true;
        }
      

        private int PrintAll = 0;
        bool PrintAlready = false;
        private bool Printstar = true;
        private async void alert(string msg, int time)
        {
            if (Printstar)
            {
                PopLabel.Content = "正在初始化打印机";
                PopBorder.Visibility = Visibility.Visible;
                await Task.Delay(TimeSpan.FromSeconds(5));
                Printstar = false;
            }
            PopLabel.Content = "正在打印报告中";
            await Task.Delay(TimeSpan.FromSeconds(time));
            PopLabel.Content = "打印完毕";
            await Task.Delay(TimeSpan.FromSeconds(3));
            PopBorder.Visibility = Visibility.Hidden;
            if (PrintAll == 0)
            {
                printnum.Content = 1;
            }
            else
            {
                printnum.Content = PrintAll;
            }
            Content = new PrintTips();
            Pages();

        }

        private void Requests1()
        {
            string response = Http.Provincial.DAList(LoginName);
            Dispatcher.BeginInvoke(new Action(() => Phrase(response)));
            string response1 = Http.Provincial.LSreporttemp(iDCardData.Name, iDCardData.IDCardNo);
            Dispatcher.BeginInvoke(new Action(() => Phrase1(response1)));
        }
        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();
        private void Phrase(string response)
        {
            if (response != null)
            {
                WaitShow.Visibility = Visibility.Hidden;
                ReportListView.ItemsSource = CompanyReportItem;
                try
                {
                    JObject JsonData = (JObject)JsonConvert.DeserializeObject(response);
                    string resultCode = JsonData["resultCode"].ToString();
                    if (resultCode == "1")
                    {
                        int ReportNum = 0;
                        JArray resultArray = (JArray)JsonConvert.DeserializeObject(JsonData["data"].ToString());
                        foreach (JObject result in resultArray)
                        {
                            CompanyReportItem item = new CompanyReportItem();
                            item.ListNo = ++ReportNum;
                            item.CompanyName = (string)result.GetValue("pgName");
                            item.USCI = (string)result.GetValue("creditCode");
                            item.Applicant = (string)result.GetValue("realName");
                            item.ApplicantIDcardNo = (string)result.GetValue("cardNumber");
                            item.Ischeck = true;
                            CompanyReportItem.Add(item);
                        }
                    }
                    else
                    {
                        Content = new HomePage("查询不到信用报告信息");
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("大汉列表接口解析错误");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("大汉列表接口连接错误");
                Pages();
            }
        }
        private ObservableCollection<TemplateItem> TemplateItem = new ObservableCollection<TemplateItem>();
        /// <summary>
        ///  莱斯模板ID
        /// </summary>
        /// <param name="response">请求数据</param>
        private void Phrase1(string response)
        {
            if (response != null)
            {
                try
                {
                    JObject JsonData = (JObject)JsonConvert.DeserializeObject(response);
                    string resultCode = JsonData["resultCode"].ToString();
                    if (resultCode == "1")
                    {
                        int Num = 0;
                        JArray resultArray = (JArray)JsonConvert.DeserializeObject(JsonData["data"].ToString());
                        foreach (JObject result in resultArray)
                        {
                            TemplateItem item = new TemplateItem();
                            item.ListNo = ++Num;
                            item.TemplateNakeName = (string)result.GetValue("summary");
                            item.TemplateID = (string)result.GetValue("id");
                            item.TemplateName = (string)result.GetValue("name");
                            TemplateItem.Add(item);
                        }
                    }
                    else
                    {
                        Content = new HomePage("获取不到莱斯模板ID");
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("莱斯模板ID接口解析错误");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("莱斯模板ID接口连接错误");
                Pages();
            }


            int idwindth = 10;
            foreach (var item in TemplateItem)
            {
                Border bdWrite = new Border()
                {
                    Width = idwindth,
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness(0),
                };
                Button b = new Button()
                {
                    Content = item.TemplateName,
                    Background = Brushes.Transparent,
                    Tag = item.TemplateID,
                    BorderThickness = new Thickness(0),
                };
                b.Click += BanbenNo;
                Border bd = new Border()
                {
                    Width = 130,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 170,
                    BorderThickness = new Thickness(0),
                    Background = Brushes.LightGoldenrodYellow,
                    CornerRadius = new CornerRadius(20),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Child = b
                };
                mobanlist.Children.Add(bdWrite);
                mobanlist.Children.Add(bd);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Print":
                    MoBanList.Visibility = Visibility.Visible;
                    break;
                case "MouBanReturn":
                    MoBanList.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        static int printcount = 0;

        private void ProvincialPrintPdf1()
        {
            foreach (var item in CompanyReportItem)
            {
                if (item.Ischeck)
                {
                    printcount += 1;
                    string respose = Http.Provincial.LSReport(item.CompanyName, "1", ReportData.Name, ReportData.IDCardNo, ReportData.PhoneNum, mobanId);
                    JObject JsonData = (JObject)JsonConvert.DeserializeObject(respose);
                    string resultCode = JsonData["resultCode"].ToString();
                    if (resultCode != "1")
                    {
                        Dispatcher.BeginInvoke(new Action(() => new HomePage(JsonData["resultMsg"].ToString())));
                        return;
                    }
                    JObject data = (JObject)JsonConvert.DeserializeObject(JsonData["data"].ToString());
                    string filePath = "Cache\\" + (string)data.GetValue("bgbh") + ".pdf";
                    Log.Write(filePath);
                    string bs64 = (string)data.GetValue("bgwj");
                    Covert.Base64ToFile(bs64, filePath);
                    PopLabel.Content = "正在打印报告";
                    if (printcount > 1)
                    {
                        Printover = true;
                        printcount = 0;
                    }
                    PrintStart(filePath);
                }
                else
                {

                }
            }



        }

        private void BanbenNo(object sender, RoutedEventArgs e)
        {
            WaitShow.Visibility = Visibility.Visible;
            MoBanList.Visibility = Visibility.Hidden;
            alert(null, 11);

            mobanId = ((Button)sender).Tag.ToString();

            PopBorder.Visibility = Visibility.Visible;
            Dispatcher.BeginInvoke(new Action(ProvincialPrintPdf1));
            PopLabel.Content = "打印完毕";
        }
    }
}
