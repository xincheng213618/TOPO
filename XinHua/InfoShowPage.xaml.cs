using BaseUtil;
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
using System.Windows.Threading;

namespace XinHua
{
    /// <summary>
    /// InfoShowPage.xaml 的交互逻辑
    /// </summary>
    public partial class InfoShowPage : Page
    {
        private string CompanyName;
        private string Uniscid;
        private string CompanyID;
        public InfoShowPage(string CompanyName, string Uniscid = null, string CompanyID = null)
        {
            this.CompanyName = CompanyName;
            this.Uniscid = Uniscid;
            this.CompanyID = CompanyID;
            InitializeComponent();
        }


        Thread thread;

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.DataContext = Time;
            Countdown_timer();
            switch (Global.Related.PageType) 
            {
                case "ShuiShouWeiFa":
                    InfoTitleLabel.Content = "重大税收违法案件人详情";
                    break;
                case "NaShuiXinYongA":
                    InfoTitleLabel.Content = "纳税信用A级企业详情";
                    break;
                case "ShiXinRen":
                    InfoTitleLabel.Content = "失信被执行人详情";
                    break;
                default:
                    break;
            }
            thread = new Thread(() => Request())
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void Request()
        {
            string response = null; 
            switch(Global.Related.PageType)
            {
                case "ShuiShouWeiFa":
                    response = Http.XinHuaTaxVDetail(CompanyName, CompanyID);
                    break;
                case "NaShuiXinYongA":
                    response = Http.XinHuaTaxADetail(CompanyName, Uniscid);
                    break;
                case "ShiXinRen":
                    response = Http.XinHuaLostCreditDetail(CompanyName, CompanyID);
                    break;
                default:
                    break;
            }
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => Content = new HomePage("数据服务器连接出错")));
                Dispatcher.BeginInvoke(new Action(() => Pages()));
                return;
            }
            switch (Global.Related.PageType)
            {
                case "ShuiShouWeiFa":
                    Dispatcher.BeginInvoke(new Action(() => XinHuaTaxVDetailPharse(response)));
                    break;
                case "NaShuiXinYongA":
                    Dispatcher.BeginInvoke(new Action(() => XinHuaTaxADetailPharse(response)));
                    break;
                case "ShiXinRen":
                    Dispatcher.BeginInvoke(new Action(() => XinHuaLostCreditDetailPharse(response)));
                    break;
                default:
                    break;
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
                        NowTotalLabel.Content = $"已经加载{XinHuaTaxVItem.Count}条数据";
                        CredittotalLabel.Content = $"共{XinHuaTaxVItem.Count }条数据";

                    }
                }
                else
                {
                    Content = new SearchPage("查不到数据");
                    Pages();
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                Content = new HomePage("数据解析错误");
                Pages();

            }
        }
        public int XinHuaTaxANum = 0;
        private ObservableCollection<XinHuaTaxA> XinHuaTaxAItem = new ObservableCollection<XinHuaTaxA>();
        private void XinHuaTaxADetailPharse(string response)
        {
            Panel.SetZIndex(XinHuaTaxABorder, 9);
            XinHuaTaxAListView.ItemsSource = XinHuaTaxAItem;
            try
            {
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
                        long unixTimeStamp = (long)result.GetValue("updateDate") / 1000;
                        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 8)); // 当地时区
                        DateTime dt = startTime.AddSeconds(unixTimeStamp);
                        item.updateDate = dt.ToString("yyyy年MM月dd日");
                        XinHuaTaxAItem.Add(item);
                    }
                    NowTotalLabel.Content = $"已经加载{XinHuaTaxAItem.Count}条数据";
                    CredittotalLabel.Content = $"共{XinHuaTaxAItem.Count }条数据";
                }
                else
                {
                    Content = new SearchPage("查不到数据");
                    Pages();
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
                            courtName = (string)result.GetValue("courtName"),
                            performance = (string)result.GetValue("performance"),
                            disreputTypeName = (string)result.GetValue("disreputTypeName"),

                            duty = (string)result.GetValue("duty"),
                            updateDate = (string)result.GetValue("publishDateDesc"),
                        };
                        XinHuaLostCreditItem.Add(item);
                    }
                    NowTotalLabel.Content = $"已经加载{XinHuaLostCreditItem.Count}条数据";
                    CredittotalLabel.Content = $"共{XinHuaLostCreditItem.Count }条数据";
                }
                else
                {
                    Content = new SearchPage("查不到数据");
                    Pages();
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                Content = new HomePage("数据解析错误");
                Pages();

            }
        }
        //倒计时模块
        DispatcherTimer pageTimer = null;
        TimeCount Time = new TimeCount();
        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1)

            };
            pageTimer.Tick += new EventHandler((sender,e)=>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }


        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void XinHuaTaxAListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CompanyName = XinHuaTaxAItem.ElementAt(XinHuaTaxAListView.SelectedIndex).CompanyName.ToString();
                CompanyID = XinHuaTaxAItem.ElementAt(XinHuaTaxAListView.SelectedIndex).CompanyID.ToString();
                Content = new XinHuaCompanyDetailPage(CompanyID);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Return":
                    Content = new SearchPage();
                    Pages();
                    break;
                default:
                    break;
            }
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
