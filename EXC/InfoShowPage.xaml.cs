using Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using BaseDLL;

namespace EXC
{
    /// <summary>
    /// InfoShowPage.xaml 的交互逻辑
    /// </summary>
    public partial class InfoShowPage : Page
    {
        private string CompanyName;
        private string Uniscid;
        private string CompanyID;
        public InfoShowPage(string CompanyName ,string Uniscid =null,string CompanyID = null)
        {
            this.CompanyName = CompanyName;
            this.Uniscid = Uniscid;
            this.CompanyID = CompanyID;
            InitializeComponent();
        }
        private IDCardData iDCardData;
        public InfoShowPage (IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.DataContext = Time;
            Countdown_timer();
            switch (Global.PageType)
            {
                case "XinHuaTaxA":
                    InfoTitleLabel.Content = "纳税信用A级企业详情";
                    break;
                case "XinHuaTaxV":
                    InfoTitleLabel.Content = "重大税收违法案件人详情";
                    break;
                case "LostCredit":
                    InfoTitleLabel.Content = "失信被执行人详情";
                    break;
                case "MsgGRWeiFang":
                    InfoTitleLabel.Content = "个人信息查询";
                    break;
                default:
                    break;
            }

            Thread work1 = new Thread(() => Request());
            work1.IsBackground = true;
            work1.Start();
        }


        private void Request()
        {
            string response = null; ;
            switch (Global.PageType)
            {
                case "XinHuaTaxA":
                    response = Http.XinHua.XinHuaTaxADetail(CompanyName,Uniscid);
                    break;
                case "XinHuaTaxV":
                    response = Http.XinHua.XinHuaTaxVDetail(CompanyName, CompanyID);
                    break;
                case "LostCredit":
                    response = Http.XinHua.XinHuaLostCreditDetail(CompanyName, CompanyID);
                    break;
                case "MsgGRWeiFang":
                    response = Webservice.WeiFang.GetPersonInfo(iDCardData.IDCardNo);
                    break;
;                default:
                    break;
            }

            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => Content = new HomePage("数据服务器连接出错")));
                Dispatcher.BeginInvoke(new Action(() => Pages()));
                return;
            }

            switch (Global.PageType)
            {
                case "XinHuaTaxA":
                    Dispatcher.BeginInvoke(new Action(() => XinHuaTaxADetailPharse(response)));
                    break;
                case "XinHuaTaxV":
                    Dispatcher.BeginInvoke(new Action(() => XinHuaTaxVDetailPharse(response)));
                    break;
                case "LostCredit":
                    Dispatcher.BeginInvoke(new Action(() => XinHuaLostCreditDetailPharse(response)));
                    break;
                case "MsgGRWeiFang":
                    Dispatcher.BeginInvoke(new Action(() => MsgGRWeiFangDetailPharse(response)));
                    break;
                default:
                    break;
            }
        }

        
        public int XinHuaTaxANum = 0;
        private ObservableCollection<XinHuaTaxA> XinHuaTaxAItem = new ObservableCollection<XinHuaTaxA>();
        private void XinHuaTaxADetailPharse(string response)
        {
            Panel.SetZIndex(XinHuaTaxABorder, 9);
            try
            {
                XinHuaTaxAListView.ItemsSource = XinHuaTaxAItem;
                JObject res = (JObject)JsonConvert.DeserializeObject(response);
                JArray data = (JArray)res["data"];

                if (data.Count != 0)
                {
                    foreach (JObject result in data)
                    {
                        XinHuaTaxANum += 1;

                        XinHuaTaxA item = new XinHuaTaxA
                        {
                            ListNo = XinHuaTaxANum,
                            CompanyName = (string)result.GetValue("name"),
                            USCI = (string)result.GetValue("discernNumber"),
                            annualEvaluation = (string)result.GetValue("annualEvaluation"),
                            taxBracket = (string)result.GetValue("taxBracket"),
                            CompanyID = (string)result.GetValue("companyId")
                        };
                        long unixTimeStamp = (long)result.GetValue("updateDate")/1000;
                        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 8)); // 当地时区
                        DateTime dt = startTime.AddSeconds(unixTimeStamp);                        
                        item.updateDate = dt.ToString("yyyy年MM月dd日");
                        XinHuaTaxAItem.Add(item);
                        XinHuaTaxANowTotalLabel.Content = $"已经加载{XinHuaTaxAItem.Count}条数据";
                        XinHuaTaxAtotalLabel.Content = $"共{XinHuaTaxAItem.Count }条数据";
                    }
                }
                else
                {
                    XinHuaTaxAMsg.Visibility = Visibility.Visible;
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                Content = new HomePage("数据解析错误");
                Pages();

            }

        }


        public int XinHuaTaxVNum = 0;
        private ObservableCollection<XinHuaTaxV> XinHuaTaxVItem = new ObservableCollection<XinHuaTaxV>();
        private void XinHuaTaxVDetailPharse(string response)
        {
            Panel.SetZIndex(XinHuaTaxVBorder, 9);
            XinHuaTaxVListView.ItemsSource = XinHuaTaxVItem;
            try
            {
                XinHuaTaxAListView.ItemsSource = XinHuaTaxAItem;
                JObject res = (JObject)JsonConvert.DeserializeObject(response);
                JArray data = (JArray)res["data"];

                if (data.Count != 0)
                {
                    foreach (JObject result in data)
                    {
                        XinHuaTaxANum += 1;

                        XinHuaTaxV item = new XinHuaTaxV
                        {
                            xh = XinHuaTaxVNum,
                            CompanyName = (string)result.GetValue("companyName"),
                            taxNo = (string)result.GetValue("taxNo"),
                            Uniscid = (string)result.GetValue("organizationCode"),
                            registeredLand = (string)result.GetValue("registeredLand"),
                            legalPerson = (string)result.GetValue("legalPerson"),
                            legalpersonSex = (string)result.GetValue("legalpersonSex"),
                            documentType = (string)result.GetValue("documentType"),
                            legalPersonsec = (string)result.GetValue("legalPersonsec"),
                            caseNature = (string)result.GetValue("caseNature"),
                            caseDes = (string)result.GetValue("caseDes"),
                            dealResult = (string)result.GetValue("dealResult")
                        };
                        long unixTimeStamp = (long)result.GetValue("createDate") / 1000;
                        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 8)); // 当地时区
                        DateTime dt = startTime.AddSeconds(unixTimeStamp);
                        item.createDate = dt.ToString("yyyy年MM月dd日");
                        XinHuaTaxVItem.Add(item);
                        XinHuaTaxVNowTotalLabel.Content = $"已经加载{XinHuaTaxVItem.Count}条数据";
                        XinHuaTaxVtotalLabel.Content = $"共{XinHuaTaxVItem.Count }条数据";
                    }
                }
                else
                {
                    XinHuaTaxAMsg.Visibility = Visibility.Visible;
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                Content = new HomePage("数据解析错误");
                Pages();

            }

        }



        public int XinHuaLostCreditNum = 0;
        private ObservableCollection<LostCredit> XinHuaLostCreditItem = new ObservableCollection<LostCredit>();
        private void XinHuaLostCreditDetailPharse(string response)
        {
            Panel.SetZIndex(XinHuaLostCreditBorder, 9);
            XinHuaTaxVListView.ItemsSource = XinHuaLostCreditItem;
            try
            {
                XinHuaLostCreditListView.ItemsSource = XinHuaLostCreditItem;
                JObject res = (JObject)JsonConvert.DeserializeObject(response);
                JArray data = (JArray)res["data"];

                if (data.Count != 0)
                {
                    foreach (JObject result in data)
                    {
                        XinHuaLostCreditNum += 1;

                        LostCredit item = new LostCredit
                        {
                            xh = XinHuaLostCreditNum,
                            CompanyName = (string)result.GetValue("name"),
                            companyId = (string)result.GetValue("companyId"),
                            Uniscid = (string)result.GetValue("organizationCode"),
                            courtName =(string)result.GetValue("courtName"),
                            performance = (string)result.GetValue("performance"),
                            disreputTypeName = (string)result.GetValue("disreputTypeName"),

                            duty = (string)result.GetValue("duty"),
                            updateDate = (string)result.GetValue("publishDateDesc"),
                        };
                        XinHuaLostCreditItem.Add(item);
                        XinHuaLostCreditNowTotalLabel.Content = $"已经加载{XinHuaLostCreditItem.Count}条数据";
                        XinHuaLostCredittotalLabel.Content = $"共{XinHuaLostCreditItem.Count }条数据";
                    }
                }
                else
                {
                    XinHuaTaxAMsg.Visibility = Visibility.Visible;
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                Content = new HomePage("数据解析错误");
                Pages();
                  
            }

        }

        private void MsgGRWeiFangDetailPharse(string response)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);

            string returncode = document.SelectSingleNode("//data/returncode").InnerText;
            string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;
            string bases = "//data/result/report/baseinfo/base";
            if (returncode.Equals("1"))
            {
                string innerText = document.SelectSingleNode(bases + "//xm") == null ? "" : document.SelectSingleNode(bases + "//xm").InnerText;
                innerText = document.SelectSingleNode(bases + "//xb") == null ? "" : document.SelectSingleNode(bases + "//xb").InnerText;
                innerText = document.SelectSingleNode(bases + "//mz") == null ? "" : document.SelectSingleNode(bases + "//mz").InnerText;
                innerText = document.SelectSingleNode(bases + "//sfzhm") == null ? "" : document.SelectSingleNode(bases + "//sfzhm").InnerText;
                innerText = document.SelectSingleNode(bases + "//jtzz") == null ? "" : document.SelectSingleNode(bases + "//jtzz").InnerText;
            }
        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;

        private Times Time = new Times();

        private void Countdown_timer()
        {
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

        //页面转换
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;  
        }


        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void XinHuaTaxAListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CompanyName = XinHuaTaxAItem.ElementAt(XinHuaTaxAListView.SelectedIndex).CompanyName.ToString();
                CompanyID = XinHuaTaxAItem.ElementAt(XinHuaTaxAListView.SelectedIndex).CompanyID.ToString();
                Content = new XinHuaCompanyDetailPage( CompanyID);
                Pages();
            }
            catch
            {
                XinHuaTaxAListView.SelectedIndex = 0;
            }
        }

        private void XinHuaTaxVListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Content = new XinHuaCompanyDetailPage(CompanyID);
            Pages();
        }
    }

    public class XinHuaTaxV 
    {
        public int xh { get; set; }
        public string CompanyName { get; set; }
        public string Uniscid { get; set; }
        public string companyId { get; set; }
        public string id { get; set; }

        public string city { get; set; }
        public string inspectionOrg { get; set; }
        public string taxNo { get; set; }
        public string organizationCode { get; set; }
        public string registeredLand { get; set; }
        public string legalPerson { get; set; }
        public string legalpersonSex { get; set; }
        public string documentType { get; set; }
        public string legalPersonsec { get; set; }
        public string legalpersonsecSex { get; set; }
        public string documentsecType { get; set; }
        public string caseNature { get; set; }
        public string caseDes { get; set; }
        public string dealResult { get; set; }
        public string dealOrg { get; set; }
        public string dealTime { get; set; }
        public string transDmTongInserDate { get; set; }
        public string odsTxDt { get; set; }
        public string createBy { get; set; }
        public string createDate { get; set; }
        public string updateBy { get; set; }
        public string updateDate { get; set; }
    }

    public class LostCredit
    {
        public int xh { get; set; }
        public string CompanyName { get; set; }
        public string Uniscid { get; set; }
        public string companyId { get; set; }
        public string id { get; set; }
        public string organizationCode { get; set; }
        public string caseCode { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string buesinessentity { get; set; }
        public string courtName { get; set; }
        public string areaName { get; set; }
        public string areaId { get; set; }
        public string gistCid { get; set; }
        public string gistUnit { get; set; }
        public string performance { get; set; }
        public string disreputTypeName { get; set; }
        public string publishDate { get; set; }
        public string regDate { get; set; }
        public string performedPart { get; set; }
        public string unperformPart { get; set; }
        public string duty { get; set; }
        public string createBy { get; set; }
        public string createDate { get; set; }
        public string updateBy { get; set; }
        public string updateDate { get; set; }

    }


}
