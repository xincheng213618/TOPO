
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using Resources;

namespace EXC
{
    //Designed By Mr.Xin 2019.12
    //ReBuilding By.Mr.Xin 2020.5
    /// CompayQueryDetailPage.xaml 的交互逻辑
    /// </summary>

    public partial class XinHuaCompanyDetailPage : Page, IDisposable
    {
        private string companyName = null;
        private string CompanyID = null;
        private CompayQueryDetailItem item;

        #region 转跳的起始
        public XinHuaCompanyDetailPage(string CompanyID =null)
        {
            this.CompanyID = CompanyID;
            this.DataContext = this;
            listItems = new ObservableCollection<CompayQueryDetailItem>();
            InitializeComponent();
        }

        #endregion

        #region 倒计时模块
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
        #endregion

        private void Home_Click(object sender, RoutedEventArgs e)
        {

            Content = new HomePage();
            Pages();
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if (TOPOItem.CompanyID.Count > 0)
            {
                string ComapnyID = (string)TOPOItem.CompanyID.Pop();
                Content = new CompayQueryDetailPage(null,ComapnyID);
                Pages();

            }
            else
            {
                Content = new HomePage();
                Pages();
            }

        }
        //页面转换
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            List<Button> Buttonlist = new List<Button>() { HomeButton };

            //List<Border> List = new List<Border>() { };
            for (int i = 0; i < Buttonlist.Count; i++)
                Buttonlist[i].Visibility = Visibility.Hidden;

            Countdown_timer();
            BasicInfo();

            //加载企业标题及基本信息
            FocusManager.GetIsFocusScope(this);

            Thread worker = new Thread(() => GetLegalEntityDetailProc(1, 1))
            {
                IsBackground = true
            };
            worker.Start();

            Thread worker2 = new Thread(() => GetLegalEntityDetailProc(2, InvestmentPageNum))
            {
                IsBackground = true
            };
            worker2.Start();

            Thread worker3 = new Thread(() => GetLegalEntityDetailProc(3, InvestmentPageNum))
            {
                IsBackground = true
            };
            worker3.Start();

            Thread worker6 = new Thread(() => GetLegalEntityDetailProc(6, RefereePageNum))
            {
                IsBackground = true
            };
            worker6.Start();

            Thread worker7 = new Thread(() => GetLegalEntityDetailProc(7, AnnouncementPageNum))
            {
                IsBackground = true
            };
            worker7.Start();

            Thread worker8 = new Thread(() => GetLegalEntityDetailProc(8, PartnersPageNum))
            {
                IsBackground = true
            };
            worker8.Start();

            Thread worker9 = new Thread(() => GetLegalEntityDetailProc(9, BranchPageNum))
            {
                IsBackground = true
            };
            worker9.Start();

            Thread worker10 = new Thread(() => GetLegalEntityDetailProc(10, PatentPageNum))
            {
                IsBackground = true
            };
            worker10.Start();

            Thread worker11 = new Thread(() => GetLegalEntityDetailProc(11, SoftPageNum))
            {
                IsBackground = true
            };
            worker11.Start();

            Thread worker12 = new Thread(() => GetLegalEntityDetailProc(12, TradePageNum))
            {
                IsBackground = true
            };
            worker12.Start();
        }

        #region 发起网络请求
        private void GetLegalEntityDetailProc(int type, int pageNo)
        {
            /*获取企业基本信息*/
            if (type == 1)
            {
                string response = Http.XinHua.CompanyBasicInfo(CompanyID, companyName);
                //string response = Http.TOPO.CompanyBasicInfo(companyName);
                Dispatcher.BeginInvoke(new Action(() => GetCompayQueryDetailCompleted(response)));
            }

            /*对外投资信息*/
            if (type == 2)
            {
                string response = Http.XinHua.Investment(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => InvestmentCompleted(response, pageNo)));
            }

            ///*查询工商异常名录*/
            //if (type == 3)
            //{
            //    string response = Http.TOPO.BusinessAbnormalion(companyName, pageNo);
            //    Dispatcher.BeginInvoke(new Action(() => BusinessAbnormalionCompleted(response, pageNo)));
            //}

            ///*获取被执行人信息*/
            //if (type == 4)
            //{
            //    string BgUrl = GlobalData.configData.GetAssetZhiXingByPage;

            //    Dictionary<string, object> Bgdic = new Dictionary<string, object>();
            //    Bgdic.Add("requestUser", requestUser);
            //    Bgdic.Add("requestPwd", requestPwd);
            //    Bgdic.Add("companyName", companyName);
            //    Bgdic.Add("pageNo", pageNo);
            //    string bgResponse = HttpRequestClient.HttpRequestGet(BgUrl, Bgdic);
            //    Dispatcher.BeginInvoke(new Action(() => GetBePerformedDetailCompleted(bgResponse, pageNo)));
            //}

            ///*获取失信被执行人信息*/
            //if (type == 5)
            //{
            //    string dishonesUrl = GlobalData.configData.GetAssetShiXinByPage;

            //    Dictionary<string, object> dishonesDic = new Dictionary<string, object>();
            //    dishonesDic.Add("requestUser", requestUser);
            //    dishonesDic.Add("requestPwd", requestPwd);
            //    dishonesDic.Add("companyName", companyName);
            //    dishonesDic.Add("pageNo", pageNo);
            //    string dishonesResponse = HttpRequestClient.HttpRequestGet(dishonesUrl, dishonesDic);
            //    Dispatcher.BeginInvoke(new Action(() => GetDishonesDetailCompleted(dishonesResponse, pageNo)));
            //}

            ///*获取裁判文书信息*/
            if (type == 6)
            {
                string response = Http.XinHua.Referee(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => RefereeCompleted(response, pageNo)));
            }

            ///*获取法院公告信息*/
            if (type == 7)
            {
                string response = Http.XinHua.Announcement(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => AnnouncementCompleted(response, pageNo)));
            }

            ///*获取股东出资*/
            if (type == 8)
            {
                string response = Http.XinHua.Partners(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => PartnersCompleted(response, pageNo)));
            }

            /*分支机构*/
            if (type == 9)
            {
                string response = Http.XinHua.Branch(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => BranchCompleted(response, pageNo)));
            }

            /*专利信息*/
            if (type == 10)
            {
                string response = Http.XinHua.PatentInfo(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => PatentCompleted(response, pageNo)));
            }
            /*软件著作权信息*/
            if (type == 11)
            {
                string response = Http.XinHua.SoftwareCopyRightUrl(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => CopyrightCompleted(response, pageNo)));
            }
            /*商标信息*/
            if (type == 12)
            {
                string brandRresponse = Http.XinHua.Trademark(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => TrademarkCompleted(brandRresponse, pageNo)));
            }
        }
        #endregion


        #region  各个部分的数据结构解析
        /*企业基本信息*/
        private ObservableCollection<CompayQueryDetailItem> listItems = null;
        private void GetCompayQueryDetailCompleted(string response)
        {
            item = new CompayQueryDetailItem();

            try
            {
                JObject compayInfo = (JObject)JsonConvert.DeserializeObject(response);
                string stateCode = compayInfo["stateCode"].ToString();

                if (stateCode.Equals("60111"))
                {
                    JObject data = (JObject)compayInfo["data"];

                    JArray resultArray = (JArray)data["returnData"];

                    JObject result = (JObject)resultArray[0];
                    item.CompanyName = (string)result.GetValue("name");
                    companyName = item.CompanyName;
                    InFoTitle.Content = companyName + "基本信息";

                    item.BRN = (string)result.GetValue("regno");
                    item.regOrgan = (string)result.GetValue("orgNo");
                    item.LegalRepresentative = (string)result.GetValue("operName");
                    try
                    {
                        item.Establishment = Convert.ToDateTime(result.GetValue("termStart").ToString()).ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        item.Establishment = (string)result.GetValue("startDate");
                    }
                    //string endDate = result["EndDate"] == null ? "--" : Convert.ToDateTime(result["EndDate"].ToString()).ToString("yyyy-MM-dd");
                    item.BusinessStatus = (string)result.GetValue("status");
                    //string province = result["Province"] == null ? "-" : result["Province"].ToString();
                    //item.updatedDate = result["UpdatedDate"] == null ? "--" : Convert.ToDateTime(result["UpdatedDate"].ToString()).ToString("yyyy-MM-dd");
                    item.USCI = (string)result.GetValue("creditCode");
                    item.regMoney = (string)result.GetValue("registCapi");
                    item.entType = (string)result.GetValue("econKind");
                    item.RegisteredAddress = (string)result.GetValue("address");
                    item.BusinessScope = (string)result.GetValue("scope");
                    item.WebSite = (string)result.GetValue("www");
                    item.email = (string)result.GetValue("email");
                    item.Phone = (string)result.GetValue("phoneNumber");
                    item.Industry = (string)result.GetValue("industry");

           
                    comanpyStatusLabel.Content =  item.BusinessStatus;

                    labelstartDate.Content += item.Establishment;
                    labelfrName.Content += item.LegalRepresentative;
                    labelCreditCode.Content += item.USCI;
                    entType.Content += item.entType;
                    labeladdress.Content += item.RegisteredAddress;
                    labelscpoe.Text += item.BusinessScope;
                    labelindustry.Content += item.Industry;
                    labelphoneNumber.Content += item.Phone;
                }
                else if (stateCode.Equals("60112"))
                {
                    Content = new HomePage("查询失败");
                    Pages();
                }
                else if (stateCode.Equals("60100"))
                {
                    BasicInfoMsg.Visibility = Visibility.Visible;
                    BasicInfoMsgLabel.Content = "数据库中暂时查不到此数据";
                }
            }
            catch (Exception ex)
            {
                BasicInfoMsg.Visibility = Visibility.Visible;
                Log.WriteException(ex);
                Log.Write(response);
            }
        }

        //多次请求时的页数问题
        private int PatentNum = 0;
        private int PatentPageNum = 1;
        private ObservableCollection<PatentDetailItem> patentDetailItem = new ObservableCollection<PatentDetailItem>();
        /*专利信息*/
        private void PatentCompleted(string response, int pageNo)
        {
            try
            {
                patentListView.ItemsSource = patentDetailItem;
                JObject patentResponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = patentResponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)patentResponse["data"];
                    JArray resultArray = (JArray)data["returnData"];
                    if (resultArray.Count != 0)
                    {
                        PatenttotalLabel.Content = $"共{(string)data.GetValue("totalCount")}条数据";
                        //绑定
                        foreach (JObject result in resultArray)
                        {
                            PatentDetailItem item = new PatentDetailItem();
                            PatentNum += 1;
                            item.xh = PatentNum;
                            item.title = (string)result.GetValue("title"); 
                            item.publicNumber = (string)result.GetValue("publicationNumber"); 
                            item.kindCodeCesc = (string)result.GetValue("kindCodeDesc"); 
                            item.publicDate = (string)result.GetValue("pubDate") == null ? "--" : Convert.ToDateTime(result["pubDate"].ToString()).ToString("yyyy-MM-dd");
                            item.patentDescription = (string)result.GetValue("abstracter");
                            //添加到list
                            patentDetailItem.Add(item);
                        }
                        PatentNowTotalLabel.Content = "已经加载" + patentDetailItem.Count + "条数据";
                        if (pageNo * 10 > patentDetailItem.Count)
                            PatentNextPageBorder.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        if (PatentPageNum == 1)
                            patentMsg.Visibility = Visibility.Visible;
                        PatentNextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    patentMsg.Visibility = Visibility.Visible;
                    PatentNextPageBorder.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                patentMsg.Visibility = Visibility.Visible;
                PatentNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }
        }

        /*软件著作权信息*/
        private int SoftNum = 0;
        private int SoftPageNum = 1;
        private ObservableCollection<CopyrightDetailItem> CopyrightDetailItem = new ObservableCollection<CopyrightDetailItem>();
        private void CopyrightCompleted(string response, int pageNo)
        {
            try
            {
                CopyrightListView.ItemsSource = CopyrightDetailItem;
                JObject CopyrightResponse = (JObject)JsonConvert.DeserializeObject(response);
                string datas = CopyrightResponse["data"].ToString() ?? "";
                string resultCode = CopyrightResponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)CopyrightResponse["data"];
                    JArray resultArray = (JArray)data["returnData"];
                    if (resultArray != null && !resultArray.Equals("") && resultArray.Count != 0)
                    {
                        CopyrighttotalLabel.Content = $"共{(string)data.GetValue("totalCount")}条数据";
                        foreach (JObject result in resultArray)
                        {
                            CopyrightDetailItem item = new CopyrightDetailItem();
                            SoftNum += 1;
                            item.xh = SoftNum;
                            item.name = (string)result.GetValue("name");
                            item.regNum = (string)result.GetValue("registerNo");
                            item.owner = (string)result.GetValue("owner") ;
                            item.regDate = (string)result.GetValue("registerAperDate");
                            item.category = (string)result.GetValue("category") ;
                            item.publishDate = (string)result.GetValue("publishDate");

                            CopyrightDetailItem.Add(item);
                        }
                        CopyrightNowTotalLabel.Content = "已经加载" + CopyrightDetailItem.Count + "条数据";
                        if (pageNo * 10 > CopyrightDetailItem.Count)
                            CopyrightNextPageBorder.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        if (SoftPageNum == 1)
                            copyrightMsg.Visibility = Visibility.Visible;
                        CopyrightNextPageBorder.Visibility = Visibility.Hidden;

                    }
                }
                else
                {
                    copyrightMsg.Visibility = Visibility.Visible;
                    CopyrightNextPageBorder.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                //数据解析失败时，也做无数据产生
                copyrightMsg.Visibility = Visibility.Visible;
                CopyrightNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }
        }
        
        /*商标信息*/
        private int TrademarkNum = 0;
        private int TradePageNum = 1;
        private readonly ObservableCollection<TrademarkItem> TrademarkItem =new ObservableCollection<TrademarkItem>();
        private void TrademarkCompleted(string response, int pageNo)
        {
            try
            {
                TradeListView.ItemsSource = TrademarkItem;
                JObject Traderesponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = Traderesponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)Traderesponse["data"];
                    JArray resultArray = (JArray)data["returnData"];
                    if (resultArray.Count != 0)
                    {
                        TradeMarktotalLabel.Content = $"共{(string)data.GetValue("totalCount")}条数据";
                        foreach (JObject result in resultArray)
                        {
                            TrademarkItem item = new TrademarkItem();
                            item.xh = ++TrademarkNum;
                            item.trademarkName = (string)result.GetValue("name");
                            item.imageUrl = (string)result.GetValue("imageUrl");
                            item.appCn = (string)result.GetValue("applicantCn");
                            item.regNo = (string)result.GetValue("regno");
                            item.appDate = Convert.ToDateTime((string)result.GetValue("appDate")).ToString("yyyy-MM-dd");
                            //类型
                            string intCls = (string)result.GetValue("intCls");
                            item.intCls = BrandType(intCls);
                            //商标状态
                            string status = (string)result.GetValue("status");
                            item.flowStatus = status != "1" ? status != "2" ? status != "3" ? "不定" : "有效" : "不定" : "待审";
                            item.agent = (string)result.GetValue("agency");

                            TrademarkItem.Add(item);
                        }
                        TradeMarkNowTotalLabel.Content = "已经加载" + TrademarkItem.Count + "条数据";
                        if (pageNo * 10 > TrademarkItem.Count)
                            TradeMarkNextPageBorder.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        if (TradePageNum == 1)
                            TrademarkMsg.Visibility = Visibility.Visible;
                        TradeMarkNextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    TrademarkMsg.Visibility = Visibility.Visible;
                    TradeMarkNextPageBorder.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                TrademarkMsg.Visibility = Visibility.Visible;
                TradeMarkNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }
        }

        public  string BrandType(string type)
        {
            string res = "";
            switch (type)
            {
                case "1": res = "化学原料"; break;
                case "2": res = "颜料油漆"; break;
                case "3": res = "日化用品"; break;
                case "4": res = "燃料油脂"; break;
                case "5": res = "医药"; break;
                case "6": res = "金属材料"; break;
                case "7": res = "机械设备"; break;
                case "8": res = "手工器械"; break;
                case "9": res = "科学仪器"; break;
                case "10": res = "医疗器械"; break;
                case "11": res = "灯具空调"; break;
                case "12": res = "运输工具"; break;
                case "13": res = "军火烟火"; break;
                case "14": res = "珠宝钟表"; break;
                case "15": res = "乐器"; break;
                case "16": res = "办公用品"; break;
                case "17": res = "橡胶制品"; break;
                case "18": res = "皮革皮具"; break;
                case "19": res = "建筑材料"; break;
                case "20": res = "家具"; break;
                case "21": res = "厨房洁具"; break;
                case "22": res = "绳网袋篷"; break;
                case "23": res = "纱线丝"; break;
                case "24": res = "布料床单"; break;
                case "25": res = "服装鞋帽"; break;
                case "26": res = "钮扣拉链"; break;
                case "27": res = "地毯席垫"; break;
                case "28": res = "健身器材"; break;
                case "29": res = "食品"; break;
                case "30": res = "方便食品"; break;
                case "31": res = "饲料种籽"; break;
                case "32": res = "啤酒饮料"; break;
                case "33": res = "酒"; break;
                case "34": res = "烟草烟具"; break;
                case "35": res = "广告营销"; break;
                case "36": res = "金融物管"; break;
                case "37": res = "建筑修理"; break;
                case "38": res = "通讯服务"; break;
                case "39": res = "运输贮藏"; break;
                case "40": res = "材料加工"; break;
                case "41": res = "教育娱乐"; break;
                case "42": res = "网站服务"; break;
                case "43": res = "餐饮住宿"; break;
                case "44": res = "医疗服务；兽医服务；农业、园艺和林业服务"; break;
                case "45": res = "法律服务、社会服务"; break;
                default: res = "查不到对应的数据"; break;
            }
            return res;
        }

        /*裁判文书信息*/
        private int RefereeNum = 0;
        private int RefereePageNum = 1;
        private ObservableCollection<RefereeItem> RefereeItem = new ObservableCollection<RefereeItem>();
        private void RefereeCompleted(string response, int pageNo)
        {
            try
            {
                RefereeListView.ItemsSource = RefereeItem;
                JObject judicativeResponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = judicativeResponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)judicativeResponse["data"];
                    JArray resultArray = (JArray)data["returnData"];
                    if (resultArray.Count != 0)
                    {
                        RefereetotalLabel.Content = $"共{data["totalCount"] ?? ""}条数据";
                        foreach (JObject result in resultArray)
                        {
                            RefereeItem item = new RefereeItem
                            {
                                xh = ++RefereeNum,
                                //文书编号
                                caseNo = (string)result.GetValue("caseNo"),
                                //标题
                                caseName = (string)result.GetValue("caseName"),
                                //类型
                                caseType = (string)result.GetValue("caseType"),
                                judgeDate = (string)result.GetValue("judgeDate"),

                                //ms: 民事案件   zx：执行案件 xz：行政案件，  zscq  知识产权 
                                //if (caseTypes.Equals("ms"))
                                //{
                                //    item.caseType = "民事案件";
                                //}
                                //else if (caseTypes.Equals("zx"))
                                //{
                                //    item.caseType = "执行案件";
                                //}
                                //else if (caseTypes.Equals("xz"))
                                //{
                                //    item.caseType = "行政案件";
                                //}
                                //else if (caseTypes.Equals("zscq"))
                                //{
                                //    item.caseType = "知识产权";
                                //}
                                //else
                                //{
                                //    item.caseType = "其他";
                                //}
                                //执行法院
                                court = (string)result.GetValue("court")
                            };
                            RefereeItem.Add(item);
                        }
                        RefereeNowTotalLabel.Content = "已经加载" + RefereeItem.Count + "条数据";
                        if (pageNo * 10 > RefereeItem.Count)
                            RefereeNextPageBorder.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        if (RefereePageNum == 1)
                            RefereeMsg.Visibility = Visibility.Visible;
                        RefereeNextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    RefereeMsg.Visibility = Visibility.Visible;
                    RefereeNextPageBorder.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                RefereeMsg.Visibility = Visibility.Visible;
                RefereeNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }
        }

        /*法院公告*/
        private int AnnouncementNum = 0;
        private int AnnouncementPageNum = 1;
        private ObservableCollection<AnnouncementItem> AnnouncementItem =new ObservableCollection<AnnouncementItem>();

        private void AnnouncementCompleted(string response ,int pageNo)
        {
            try
            {
                AnnouncementListView.ItemsSource = AnnouncementItem;
                JObject AnnouncementResponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = AnnouncementResponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)AnnouncementResponse["data"];
                    JArray resultArray = (JArray)data["returnData"];
                    if (resultArray.Count != 0)
                    {
                        AnnouncementtotalLabel.Content = $"共{data["totalCount"] ?? ""}条数据";

                        foreach (JObject result in resultArray)
                        {
                            AnnouncementItem item = new AnnouncementItem();
                            item.xh = ++AnnouncementNum;
                            //公布时间
                            item.publishedDate =Convert.ToDateTime((string)result.GetValue("publicDate")).ToString("yyyy-MM-dd");
                            //种类
                            item.category = (string)result.GetValue("catagory");
                            //当事人
                            item.party = (string)result.GetValue("litigant");
                            //内容
                            item.content = (string)result.GetValue("content");
                            AnnouncementItem.Add(item);
                        }
                        AnnouncementNowTotalLabel.Content = "已经加载" + AnnouncementItem.Count + "条数据";
                        if (pageNo * 10 > AnnouncementItem.Count)
                            AnnouncementNextPageBorder.Visibility = Visibility.Hidden;

                        AnnouncementMsg.Visibility = Visibility.Visible;//这是一个BUG ，暂时没有时间修复了。

                    }
                    else
                    {
                        if (AnnouncementPageNum==1)
                            AnnouncementMsg.Visibility = Visibility.Visible;
                        AnnouncementNextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    if (AnnouncementPageNum == 1)
                        AnnouncementMsg.Visibility = Visibility.Visible;
                    AnnouncementNextPageBorder.Visibility = Visibility.Hidden;
                }

            }
            catch(Exception ex)
            {
                AnnouncementMsg.Visibility = Visibility.Visible;
                AnnouncementNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }

        }

        /*股权分配*/
        private int PartnersNum = 0;
        private int PartnersPageNum = 1;
        private ObservableCollection<PartnersItem> PartnersItem = new ObservableCollection<PartnersItem>();

        private void PartnersCompleted(string response, int pageNo)
        {
            try
            {
                JObject PartnersResponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = PartnersResponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)PartnersResponse["data"];
                    JArray resultArray = (JArray)data["returnData"];
                    if (resultArray.Count != 0)
                    {
                        PartnersListView.ItemsSource = PartnersItem;

                        foreach (JObject result in resultArray)
                        {
                            PartnersItem item = new PartnersItem();
                            item.xh = ++PartnersNum;
                            item.stockName = (string)result.GetValue("stockName");
                            item.stockPercent= (string)result.GetValue("stockPercent");
                            item.realCapi = (string)result.GetValue("realCapi");
                            item.investType = (string)result.GetValue("investType");
                            item.identifyCode = (string)result.GetValue("identifyCode");
                            item.identifyType = (string)result.GetValue("identifyType");
                            item.shouldDate = (string)result.GetValue("shoudDate");
                            item.shouldMoey = (string)result.GetValue("shouldCapi");

                            PartnersItem.Add(item);
                        }
                        //PartnerstotalLabel.Content = "共" + PartnersItem.Count + "条数据" ;
                        if (PartnersItem.Count< pageNo * 10)
                        {
                            PartnersNextPageBorder.Visibility = Visibility.Hidden;
                        }
                        PartnersNowTotalLabel.Content = "已经加载" + PartnersItem.Count + "条数据";


                    }
                    else
                    {
                        if (PartnersPageNum == 1)
                            PartnersMsg.Visibility = Visibility.Visible;
                        PartnersNextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    if (PartnersPageNum == 1)
                        PartnersMsg.Visibility = Visibility.Visible;
                    PartnersNextPageBorder.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                PartnersMsg.Visibility = Visibility.Visible;
                PartnersNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }

        }

        /*对外投资*/
        private int InvestmentNum = 0;
        private int InvestmentPageNum = 1;
        private ObservableCollection<InvestmentItem> InvestmentItem = new ObservableCollection<InvestmentItem>();

        private void InvestmentCompleted(string response, int pageNo)
        {
            try
            {
                JObject InvestmentResponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = InvestmentResponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)InvestmentResponse["data"];
                    JArray resultArray = (JArray)data["returnData"];
                    if (resultArray.Count != 0)
                    {
                        InvestmentListView.ItemsSource = InvestmentItem;
                        InvestmenttotalLabel.Content = "共" + (string)data.GetValue("totalCount") + "条数据";

                        foreach (JObject result in resultArray)
                        {
                            InvestmentItem item = new InvestmentItem();
                            item.xh = ++InvestmentNum;
                            item.CompanyID = (string)result.GetValue("id");
                            item.investedCompany = (string)result.GetValue("name");
                            item.creditCode = (string)result.GetValue("no");
                            item.econKind = (string)result.GetValue("econKind");
                            item.no = (string)result.GetValue("no");
                            item.regMoney = (string)result.GetValue("registCapi");
                            item.startDate = (string)result.GetValue("termStart");
                            item.status = (string)result.GetValue("status");
                            item.fundedRatio = (string)result.GetValue("fundedRatio");
                            InvestmentItem.Add(item);
                        }
                        //InvestmenttotalLabel.Content = "共" + InvestmentItem.Count + "条数据" ;
                        if (InvestmentItem.Count < pageNo * 10)
                        {
                            InvestmentNextPageBorder.Visibility = Visibility.Hidden;
                        }
                        InvestmentNowTotalLabel.Content = "已经加载" + InvestmentItem.Count + "条数据";
                    }
                    else
                    {
                        if (InvestmentPageNum == 1)
                            InvestmentMsg.Visibility = Visibility.Visible;
                        InvestmentNextPageBorder.Visibility = Visibility.Hidden;  
                    }
                }
                else
                {
                    if (InvestmentPageNum == 1)
                        InvestmentMsg.Visibility = Visibility.Visible;
                    InvestmentNextPageBorder.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                InvestmentMsg.Visibility = Visibility.Visible;
                InvestmentNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }

        }


        /*分支机构*/
        private int BranchNum = 0;
        private int BranchPageNum = 1;
        private ObservableCollection<BranchItem> BranchItem = new ObservableCollection<BranchItem>();

        private void BranchCompleted(string response, int pageNo)
        {
            try
            {
                BranchListView.ItemsSource = BranchItem;

                JObject BranchResponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = BranchResponse["stateCode"].ToString();
                if ("60111".Equals(resultCode))
                {
                    JObject data = (JObject)BranchResponse["data"];
                    JArray resultArray = (JArray)data["returnData"];

                    BranchtotalLabel.Content = "共" + (string)data.GetValue("totalCount") + "条数据";

                    if (resultArray.Count != 0)
                    {
                        foreach (JObject result in resultArray)
                        {
                            BranchItem item = new BranchItem();
                            item.xh = ++BranchNum;
                            item.CompanyID = (string)result.GetValue("id");
                            item.branchesAme = (string)result.GetValue("name");

                            BranchItem.Add(item);
                        }
                        BranchNowTotalLabel.Content = "已经加载" + BranchItem.Count + "条数据";
                        if (BranchItem.Count< pageNo * 10)
                        {
                            BranchNextPageBorder.Visibility = Visibility.Hidden;
                        }

                    }
                    else
                    {
                        if (BranchPageNum == 1)
                            BranchMsg.Visibility = Visibility.Visible;
                        BranchNextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    BranchMsg.Visibility = Visibility.Visible;
                    BranchNextPageBorder.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                PartnersMsg.Visibility = Visibility.Visible;
                PartnersNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }

        }


        /*经营异常*/
        private int BusinessAbnormalionNum = 0;
        //private int BusinessAbnormalionPageNum = 1;
        private ObservableCollection<BusinessAbnormalionItem> BusinessAbnormalionItem = new ObservableCollection<BusinessAbnormalionItem>();

        private void BusinessAbnormalionCompleted(string response, int pageNo)
        {
            try
            {
                JObject BranchResponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = BranchResponse["resultCode"].ToString();
                if ("0".Equals(resultCode))
                {
                    JObject data = (JObject)BranchResponse["data"];
                    BranchtotalLabel.Content = "共" + (string)data.GetValue("totoal") + "条数据";
                    JArray resultArray = (JArray)data["list"];
                    if (resultArray.Count != 0)
                    {
                        BranchListView.ItemsSource = BusinessAbnormalionItem;

                        foreach (JObject result in resultArray)
                        {
                            BusinessAbnormalionItem item = new BusinessAbnormalionItem
                            {
                                xh = ++BusinessAbnormalionNum,
                                branchesAme = (string)result.GetValue("branchesAme")
                            };

                            BusinessAbnormalionItem.Add(item);
                        }

                        if (BranchItem.Count < BranchPageNum * 10)
                        {
                            BranchNextPageBorder.Visibility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        if (BranchPageNum == 1)
                            BranchMsg.Visibility = Visibility.Visible;
                        BranchNextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    BranchMsg.Visibility = Visibility.Visible;
                    BranchNextPageBorder.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                PartnersMsg.Visibility = Visibility.Visible;
                PartnersNextPageBorder.Visibility = Visibility.Hidden;
                Log.WriteException(ex);
            }



        }

        #endregion


        #region 前台数据解析
        private void FontWeight_Set()
        {
            Time.Countdown = 59;

            BasicLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            TrademarkLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            PropertyLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            EquityLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            JudicialLabel.FontWeight = FontWeightNormalLabel.FontWeight;
        }
        //基本信息
        private void BasicInfo_Click(object sender, RoutedEventArgs e)
        {
            BasicInfo();
        }
        private void BasicInfo()
        {
            FontWeight_Set();
            BasicLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            InFoTitle.Content = companyName+ "基本信息";
            BasicInfoShow.Visibility = Visibility.Visible;

            PropertyInfoShow.Visibility = Visibility.Hidden;
            TrademarkShow.Visibility = Visibility.Hidden;
            JudicialInfoShow.Visibility = Visibility.Hidden;
            EquityInfoShow.Visibility = Visibility.Hidden;

        }
        //股权信息
        private void Equity_Info_Click(object sender, RoutedEventArgs e)
        {
            EquityInfo();
        }
        private void EquityInfo()
        {
            FontWeight_Set();
            EquityLabel.FontWeight = FontWeightBoldLabel.FontWeight;

            InFoTitle.Content = companyName + "股权信息";
            EquityInfoShow.Visibility = Visibility.Visible;

            BasicInfoShow.Visibility = Visibility.Hidden;
            PropertyInfoShow.Visibility = Visibility.Hidden;
            TrademarkShow.Visibility = Visibility.Hidden;
            JudicialInfoShow.Visibility = Visibility.Hidden;

        }
        private void Partners_Click(object sender, RoutedEventArgs e)
        { 
            PartnersLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            InvestmentLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            BranchLabel.FontWeight = FontWeightNormalLabel.FontWeight;

            PartnersGrid.Visibility = Visibility.Visible;
            InvestmentGrid.Visibility = Visibility.Hidden;
            BranchGrid.Visibility = Visibility.Hidden;
        }

        private void Investment_Click(object sender, RoutedEventArgs e)
        {
            InvestmentLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            PartnersLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            BranchLabel.FontWeight = FontWeightNormalLabel.FontWeight;

            InvestmentGrid.Visibility = Visibility.Visible;
            BranchGrid.Visibility = Visibility.Hidden;
            PartnersGrid.Visibility = Visibility.Hidden;
        }

        private void Branch_Click(object sender, RoutedEventArgs e)
        {
            BranchLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            InvestmentLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            PartnersLabel.FontWeight = FontWeightNormalLabel.FontWeight;

            BranchGrid.Visibility = Visibility.Visible;
            PartnersGrid.Visibility = Visibility.Hidden;
            InvestmentGrid.Visibility = Visibility.Hidden;
        }
        //知识产权
        private void Property_Info_Click(object sender, RoutedEventArgs e)
        {
            PropertyInfo();
        }
        private void PropertyInfo()
        {
            FontWeight_Set();
            PropertyLabel .FontWeight = FontWeightBoldLabel.FontWeight;

            InFoTitle.Content = companyName + "知识产权信息";
            PropertyInfoShow.Visibility = Visibility.Visible;

            BasicInfoShow.Visibility = Visibility.Hidden;
            TrademarkShow.Visibility = Visibility.Hidden;
            JudicialInfoShow.Visibility = Visibility.Hidden;
            EquityInfoShow.Visibility = Visibility.Hidden;
        }
        private void Copyright_Click(object sender, RoutedEventArgs e)
        {
            copyrightLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            patentLabel.FontWeight = FontWeightNormalLabel.FontWeight;

            patentGrid.Visibility = Visibility.Hidden;
            copyrightGrid.Visibility = Visibility.Visible;
        }

        private void Patent_Click(object sender, RoutedEventArgs e)
        {
            patentLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            copyrightLabel.FontWeight = FontWeightNormalLabel.FontWeight;

            patentGrid.Visibility = Visibility.Visible;
            copyrightGrid.Visibility = Visibility.Hidden;
        }
        //商标
        private void Trademark_Click(object sender, RoutedEventArgs e)
        {
            Trademark();
        }
        private void Trademark()
        {
            FontWeight_Set();
            TrademarkLabel.FontWeight = FontWeightBoldLabel.FontWeight;

            InFoTitle.Content = companyName + "商标信息";
            TrademarkShow.Visibility = Visibility.Visible;

            BasicInfoShow.Visibility = Visibility.Hidden;
            PropertyInfoShow.Visibility = Visibility.Hidden;
            JudicialInfoShow.Visibility = Visibility.Hidden;
            EquityInfoShow.Visibility = Visibility.Hidden;

        }
        //司法

        private void Judicial_Click(object sender, RoutedEventArgs e)
        {
            Judicial();
        }
        private void Judicial()
        {
            FontWeight_Set();
            JudicialLabel.FontWeight = FontWeightBoldLabel.FontWeight;

            InFoTitle.Content = companyName + "司法信息";
            JudicialInfoShow.Visibility = Visibility.Visible;

            BasicInfoShow.Visibility = Visibility.Hidden;
            PropertyInfoShow.Visibility = Visibility.Hidden;
            TrademarkShow.Visibility = Visibility.Hidden;
            EquityInfoShow.Visibility = Visibility.Hidden;

        }
        private void Referee_Click(object sender, RoutedEventArgs e)
        {
            RefereeLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            AnnouncementLabel.FontWeight = FontWeightNormalLabel.FontWeight;

            RefereeGrid.Visibility = Visibility.Visible;
            AnnouncementGrid.Visibility = Visibility.Hidden;
        }

        private void Announcement_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementLabel.FontWeight = FontWeightBoldLabel.FontWeight;
            RefereeLabel.FontWeight = FontWeightNormalLabel.FontWeight;

            AnnouncementGrid.Visibility = Visibility.Visible;
            RefereeGrid.Visibility = Visibility.Hidden;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Global.PageType = "XinHuaPrint";
            CompanyInfo.CompanyName = companyName;
            CompanyInfo.CompanyID = CompanyID;


            Content = new Report();
            //Content = new IDCardPage();
            Pages();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;

            if (JudicialLabel.FontWeight == FontWeightBoldLabel.FontWeight)
            {
                if (RefereeLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                {
                    RefereePageNum += 1;
                    Thread worker6 = new Thread(() => GetLegalEntityDetailProc(6, RefereePageNum));
                    worker6.IsBackground = true;
                    worker6.Start();
                }
                if (AnnouncementLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                {
                    AnnouncementPageNum += 1;
                    Thread worker7 = new Thread(() => GetLegalEntityDetailProc(7, AnnouncementPageNum));
                    worker7.IsBackground = true;
                    worker7.Start();
                }
            }

            if (PropertyLabel.FontWeight == FontWeightBoldLabel.FontWeight)
            {
                if (patentLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                {
                    PatentPageNum += 1;
                    Thread worker10 = new Thread(() => GetLegalEntityDetailProc(10, PatentPageNum));
                    worker10.IsBackground = true;
                    worker10.Start();
                }
                if (copyrightLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                {
                    SoftPageNum += 1;
                    Thread worker11 = new Thread(() => GetLegalEntityDetailProc(11, SoftPageNum));
                    worker11.IsBackground = true;
                    worker11.Start();
                }
            }
            if (TrademarkLabel.FontWeight == FontWeightBoldLabel.FontWeight)
            {
                TradePageNum += 1;
                Thread worker12 = new Thread(() => GetLegalEntityDetailProc(12, TradePageNum));
                worker12.IsBackground = true;
                worker12.Start();
            }

            if (EquityLabel.FontWeight == FontWeightBoldLabel.FontWeight)
            {
                if (PartnersLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                {
                    PartnersPageNum += 1;
                    Thread worker8 = new Thread(() => GetLegalEntityDetailProc(8, PartnersPageNum));
                    worker8.IsBackground = true;
                    worker8.Start();
                }
                if (InvestmentLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                {
                    InvestmentPageNum += 1;
                    Thread worker2 = new Thread(() => GetLegalEntityDetailProc(2, InvestmentPageNum));
                    worker2.IsBackground = true;
                    worker2.Start();
                }
                if (BranchLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                {
                    BranchPageNum += 1;
                    Thread worker9 = new Thread(() => GetLegalEntityDetailProc(9, BranchPageNum));
                    worker9.IsBackground = true;
                    worker9.Start();
                }

            }


        }

        #endregion 


        private void PopClose_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyLabel.FontWeight == FontWeightBoldLabel.FontWeight)
            {
                if (patentLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                    PopPatentBorder.Visibility = Visibility.Hidden;
            }

            if (JudicialLabel.FontWeight == FontWeightBoldLabel.FontWeight)
            {
                if (RefereeLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                    PopRefereeBorder.Visibility = Visibility.Hidden;
                if (AnnouncementLabel.FontWeight == FontWeightBoldLabel.FontWeight)
                { }
            }
        }

        private void patentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PatentDescriptionText.Text =  patentDetailItem.ElementAt(patentListView.SelectedIndex).patentDescription.ToString();
            PopPatentBorder.Visibility = Visibility.Visible;
        }


        private void copyrightListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RefereeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TradeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void ProgressBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pageTimer.IsEnabled = ! pageTimer.IsEnabled;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void InvestmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TOPOItem.CompanyID.Push(CompanyID);
            //Content = new XinHuaCompanyDetailPage(InvestmentItem.ElementAt(InvestmentListView.SelectedIndex).CompanyID.ToString());
            //Pages();
        }

        private void BranchListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TOPOItem.CompanyID.Push(CompanyID);
            //Content = new XinHuaCompanyDetailPage(BranchItem.ElementAt(BranchListView.SelectedIndex).CompanyID.ToString());
            //Pages();
        }
    }


}
