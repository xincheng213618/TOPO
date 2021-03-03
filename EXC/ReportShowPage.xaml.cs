using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using EXCResources;

namespace EXC
{
    /// <summary>
    /// ReportShowPage.xaml 的交互逻辑
    /// </summary>
    public partial class ReportShowPage : Page
    {
        private string CompanyName;
        private string IDcardNo;
        private string response;


        public ReportShowPage()
        {
            response = null;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            //获取列表
            Thread worker = new Thread(() => Requests());
            worker.Start();
        }

        private void HomeMsg(string msg)
        {
            Content = new HomePage(msg);
            Pages();
        }

        private ObservableCollection<ReportTempItem> ReportTempItem = new ObservableCollection<ReportTempItem>();
        private void TempPhrase(string response)
        {
            try
            {
                JObject JsonData = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = JsonData["resultCode"].ToString();
                if (resultCode != "1")
                {
                    Dispatcher.BeginInvoke(new Action(() => HomeMsg("莱斯数据返回错误")));
                    return;
                }
                int ReportNum = 0;
                JArray resultArray = (JArray)JsonConvert.DeserializeObject(JsonData["data"].ToString());
                foreach (JObject result in resultArray)
                {
                    ReportTempItem item = new ReportTempItem();
                    item.xh = ++ReportNum;
                    item.summary = (string)result.GetValue("summary");
                    item.id = (string)result.GetValue("id");
                    item.name = (string)result.GetValue("name");
                    ReportTempItem.Add(item);
                }
            }
            catch
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("莱斯获取验证接口解析错误")));
            }

        }

        private void Requests()
        {

            string response = Http.Provincial.LSreporttemp(ReportData.Name, ReportData.IDCardNo);
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("莱斯获取验证接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => TempPhrase(response)));
            response = Http.Provincial.DAList(ReportData.LoginName);
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("大汉获取列表接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => ListPhrase(response)));
            Dispatcher.BeginInvoke(new Action(() => Media.Player(null, 6)));
            ReportData.Success = false;
        }

        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();
        private void ListPhrase(string response)
        {
            try
            {
                ReportListView.ItemsSource = CompanyReportItem;
                JObject JsonData = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = JsonData["resultCode"].ToString();
                if (resultCode != "1")
                {
                    Dispatcher.BeginInvoke(new Action(() => HomeMsg("查询不到信息")));
                    return;
                }

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
            catch 
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("大汉获取列表接口解析错误")));
            }
        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }


        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void sss()
        {
            EnhanceGrid.Visibility = Visibility.Hidden;
            PopBorder.Visibility = Visibility.Visible;
            PopLabel.Content = "正在生成报告";
        }
        private void PDFShow(string response)
        {
            if (response ==null)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("莱斯获取验证接口连接错误")));
                return;
            }
            
            try
            {
                bool PDFcover = false;
                JObject Jsonresponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = Jsonresponse["resultCode"].ToString();
                string filePath = null;
                if (resultCode.Equals("1"))
                {
                    JObject data = (JObject)JsonConvert.DeserializeObject(Jsonresponse["data"].ToString());
                    filePath = "Temp\\" + (string)data.GetValue("bgbh") + ".pdf";
                    Log.Write(filePath);
                    string bs64 = (string)data.GetValue("bgwj");

                    PDFcover =XCovert.Base64ToFile(bs64, filePath);
                }
                else
                {
                    Content = new HomePage((string)Jsonresponse.GetValue("resultMsg"));
                    Pages();
                    return;
                }

                PopLabel.Content = "正在打印报告";
                if(!PDFcover)
                {
                    Dispatcher.BeginInvoke(new Action(() => HomeMsg("PDF  生成失败")));
                    return;
                }
                PrintStart(filePath);
            }
            catch(Exception ex)
            {
                Log.WriteException(ex);
            }


        }

        private async void alert(string msg, int time)
        {
            if (Printstar)
            {
                PopLabel.Content = "正在初始化打印机";
                PopBorder.Visibility = Visibility.Visible;
                await Task.Delay(TimeSpan.FromSeconds(5));
                Printstar = false;
            }


            //PopLabel.Content = "正在打印第"+ (LocalPrintNum+1).ToString()+"份报告";
            PopLabel.Content = "正在打印报告中";

            await Task.Delay(TimeSpan.FromSeconds(time));

            if (Printover)
            {
                PopLabel.Content = "打印完毕";
                await Task.Delay(TimeSpan.FromSeconds(3));
                PopBorder.Visibility = Visibility.Hidden;
                PrintAllNumLabel.Content = PrintAll;
                PrintOver.Visibility = Visibility.Visible;
                await Dispatcher.BeginInvoke(new Action(() => Media.Player(null, 8)));
            }

        }

        private void Leve_0_Click(object sender, RoutedEventArgs e)
        {
            EnhanceGrid.Visibility = Visibility.Hidden;
        }
        private AxAcroPDFLib.AxAcroPDF pdfControl;
        //加载PDF文件
        private void LoadPDFFile(string pdfPath)
        {
            pdfControl = formsHost.Child as AxAcroPDFLib.AxAcroPDF;
            pdfControl.LoadFile(pdfPath);
        }
        //打印PDF文件
        private void PrintPDFFile(string pdfPath)
        {

            pdfControl = formsHost.Child as AxAcroPDFLib.AxAcroPDF;
            pdfControl.printAll();
            PrintAlready = true;
        }

        public void PrintStart(string filePath)
        {
            Dispatcher.Invoke(new Action(() => alert(null, 11)));

            pdfControl = new AxAcroPDFLib.AxAcroPDF();
            pdfControl.BeginInit();
            formsHost.Child = pdfControl;
            pdfControl.EndInit();

            Dispatcher.Invoke(new Action(() => LoadPDFFile(filePath)));
            Dispatcher.Invoke(new Action(() => PrintPDFFile(filePath)));
        }
        bool PrintAlready = false;


        private void Return_Click(object sender, RoutedEventArgs e)
        {
            PrintOver.Visibility = Visibility.Hidden;
            EnhanceGrid.Visibility = Visibility.Hidden;
        }
        //全选
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //foreach (ReportItem che in ReportListView.Items)
            //{
            //    che.IsCheck = true;
            //}
        }

        //下载
        private int LocalPrintNum =0;
        private int PrintAll = 0;
        private bool Printover = false;
        private bool Printstar = true;
        private void DownLoadePDF(string Level)
        {
            LocalPrintNum = 0;
            //ReportItem.ElementAt(ReportListView.SelectedIndex).ischeck.ToString();
            for (; LocalPrintNum < CompanyReportItem.Count; LocalPrintNum++)
            {
                if (CompanyReportItem.ElementAt(LocalPrintNum).Ischeck)
                {
                    PrintAll += 1;
                    CompanyName = CompanyReportItem.ElementAt(LocalPrintNum).CompanyName.ToString();
                    string response = Http.Provincial.LSReport(CompanyName, "1", ReportData.Name, ReportData.IDCardNo, ReportData.PhoneNum, Level);

                    Dispatcher.Invoke(new Action(() => PDFShow(response)));
                }
                Printover = true;

            }
            LocalPrintNum = 0;
        }

        private void PrintAll_Click(object sender, RoutedEventArgs e)
        {
            EnhanceGrid.Visibility = Visibility.Visible;
        }

        private void Level_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string  Level = ReportTempItem.ElementAt((int)button.Tag).id.ToString();
            EnhanceGrid.Visibility = Visibility.Hidden;
            Dispatcher.BeginInvoke(new Action(() => sss()));
            Thread worker3 = new Thread(() => DownLoadePDF(Level));
            worker3.Start();
        }
    }


    public class ReportTempItem
    {
        public int xh { get; set; }
        public string summary { get; set; }//公司名称
        public string name { get; set; }//社会统一信用代码
        public string id { get; set; }//申请人

    }



}
