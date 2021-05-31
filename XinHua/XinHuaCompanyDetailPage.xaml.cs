using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections;
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
    /// XinHuaCompanyDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class XinHuaCompanyDetailPage : Page
    {
        private string companyName = null;
        private string CompanyID = null;
        private string USCI = null;
        private CompayQueryDetailItem item;
        public XinHuaCompanyDetailPage(string CompanyID = null)
        {
            this.CompanyID = CompanyID;
            listItems = new ObservableCollection<CompayQueryDetailItem>();
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            TOP.DataContext = Time;

            Countdown_timer();
            Panel.SetZIndex(BasicInfoShow, Zindex);
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
                string response = Http.CompanyBasicInfo(CompanyID);
                //string response = Http.TOPO.CompanyBasicInfo(companyName);
                Dispatcher.BeginInvoke(new Action(() => GetCompayQueryDetailCompleted(response)));
            }

            /*对外投资信息*/
            if (type == 2)
            {
                string response = Http.Investment(CompanyID, pageNo.ToString());
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
                string response = Http.Referee(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => RefereeCompleted(response, pageNo)));
            }

            ///*获取法院公告信息*/
            if (type == 7)
            {
                string response = Http.Announcement(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => AnnouncementCompleted(response, pageNo)));
            }

            ///*获取股东出资*/
            if (type == 8)
            {
                string response = Http.Partners(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => PartnersCompleted(response, pageNo)));
            }

            /*分支机构*/
            if (type == 9)
            {
                string response = Http.Branch(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => BranchCompleted(response, pageNo)));
            }

            /*专利信息*/
            if (type == 10)
            {
                string response = Http.PatentInfo(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => PatentCompleted(response, pageNo)));
            }
            /*软件著作权信息*/
            if (type == 11)
            {
                string response = Http.SoftwareCopyRightUrl(CompanyID, pageNo.ToString());
                Dispatcher.BeginInvoke(new Action(() => CopyrightCompleted(response, pageNo)));
            }
            /*商标信息*/
            if (type == 12)
            {
                string brandRresponse = Http.Trademark(CompanyID, pageNo.ToString());
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
                    InFoTitle.Content = companyName;
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

                    USCI = item.USCI;
                    comanpyStatusLabel.Content = item.BusinessStatus;

                    labelstartDate.Content += item.Establishment;
                    labelfrName.Content += item.LegalRepresentative;
                    labelCreditCode.Content += item.USCI;
                    entType.Content += item.entType;
                    labeladdress.Content += item.RegisteredAddress;
                    labelscpoe.Text += item.BusinessScope;
                    labelindustry.Content += item.Industry;
                    labelphoneNumber.Content += item.Phone;

                    listItems.Add(item);
                }
                else if (stateCode.Equals("60112"))
                {
                    Content = new HomePage("查询失败");
                    Pages();
                }
                else if (stateCode.Equals("60100"))
                {
                    Content = new HomePage("查不到企业数据");
                    Pages();
                }
            }
            catch
            {
                Log.WriteUrl(null, response);
                Content = new HomePage("查不到企业数据");
                Pages();
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
                if (patentDetailItem.Count > 0)
                    patentListView.ScrollIntoView(patentListView.Items[patentDetailItem.Count - 1]);

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
                if (CopyrightDetailItem.Count > 0)
                    CopyrightListView.ScrollIntoView(CopyrightListView.Items[CopyrightDetailItem.Count - 1]);
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
                            item.owner = (string)result.GetValue("owner");
                            item.regDate = (string)result.GetValue("registerAperDate");
                            item.category = (string)result.GetValue("category");
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
        private readonly ObservableCollection<TrademarkItem> TrademarkItem = new ObservableCollection<TrademarkItem>();
        private void TrademarkCompleted(string response, int pageNo)
        {
            TradeListView.ItemsSource = TrademarkItem;
            if (TrademarkItem.Count > 0)
                TradeListView.ScrollIntoView(TradeListView.Items[TrademarkItem.Count - 1]);
            try
            {

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
        //商标类型
        public static string BrandType(string type)
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
                if (RefereeItem.Count > 0)
                    RefereeListView.ScrollIntoView(RefereeListView.Items[RefereeItem.Count - 1]);
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
        private ObservableCollection<AnnouncementItem> AnnouncementItem = new ObservableCollection<AnnouncementItem>();

        private void AnnouncementCompleted(string response, int pageNo)
        {
            try
            {
                AnnouncementListView.ItemsSource = AnnouncementItem;
                if (AnnouncementItem.Count > 0)
                    AnnouncementListView.ScrollIntoView(AnnouncementListView.Items[AnnouncementItem.Count - 1]);
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
                            item.publishedDate = Convert.ToDateTime((string)result.GetValue("publicDate")).ToString("yyyy-MM-dd");
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

                        AnnouncementMsg.Visibility = Visibility.Hidden;//这是一个BUG ，暂时没有时间修复了。

                    }
                    else
                    {
                        if (AnnouncementPageNum == 1)
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
            catch (Exception ex)
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
            PartnersListView.ItemsSource = PartnersItem;
            if (PartnersItem.Count > 0)
                PartnersListView.ScrollIntoView(PartnersListView.Items[PartnersItem.Count - 1]);
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

                        foreach (JObject result in resultArray)
                        {
                            PartnersItem item = new PartnersItem();
                            item.xh = ++PartnersNum;
                            item.stockName = (string)result.GetValue("stockName");
                            item.stockPercent = (string)result.GetValue("stockPercent");
                            item.realCapi = (string)result.GetValue("realCapi");
                            item.investType = (string)result.GetValue("investType");
                            item.identifyCode = (string)result.GetValue("identifyCode");
                            item.identifyType = (string)result.GetValue("identifyType");
                            item.shouldDate = (string)result.GetValue("shoudDate");
                            item.shouldMoey = (string)result.GetValue("shouldCapi");

                            PartnersItem.Add(item);
                        }
                        PartnerstotalLabel.Content = "共" + PartnersItem.Count + "条数据" ;
                        if (PartnersItem.Count < pageNo * 10)
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
            InvestmentListView.ItemsSource = InvestmentItem;
            if (InvestmentItem.Count > 0)
                InvestmentListView.ScrollIntoView(InvestmentListView.Items[InvestmentItem.Count - 1]);
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
            BranchListView.ItemsSource = BranchItem;
            if (BranchItem.Count > 0)
                BranchListView.ScrollIntoView(BranchListView.Items[BranchItem.Count - 1]);
            try
            {
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
                        if (BranchItem.Count < pageNo * 10)
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
            BranchListView.ItemsSource = BusinessAbnormalionItem;
            if (BusinessAbnormalionItem.Count > 0)
                BranchListView.ScrollIntoView(BranchListView.Items[BusinessAbnormalionItem.Count - 1]);
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
        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pageTimer.IsEnabled = !pageTimer.IsEnabled;
        }


        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        Thread thread;
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;

            Button button = (Button)sender;
            switch (button.Tag)
            {
                case "Partners":
                    PartnersPageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(8, PartnersPageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;
                case "Investment":
                    InvestmentPageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(2, InvestmentPageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;
                case "Branch":
                    BranchPageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(9, BranchPageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;
                case "Patent":
                    PatentPageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(10, PatentPageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;

                case "Copyright":
                    SoftPageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(11, SoftPageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;
                case "TradeMark":
                    TradePageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(12, TradePageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;
                case "Announcement":
                    AnnouncementPageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(7, AnnouncementPageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;   
                case "Referee":
                    RefereePageNum += 1;
                    thread = new Thread(() => GetLegalEntityDetailProc(6, RefereePageNum))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;
                default:
                    break;
            }
        }

        private void PopClose_Click(object sender, RoutedEventArgs e)
        {
            PopPatentBorder.Visibility = Visibility.Hidden;
            PopRefereeBorder.Visibility = Visibility.Hidden;
        }


        private void RefereeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void patentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PatentDescriptionText.Text = patentDetailItem.ElementAt(patentListView.SelectedIndex).patentDescription.ToString();
            PopPatentBorder.Visibility = Visibility.Visible;
        }

        private void copyrightListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TradeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InvestmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BranchListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
  
        #region 倒计时模块
        private DispatcherTimer pageTimer = null;
        private TimeCount Time = new TimeCount();

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
        //页面转换
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
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
                    Content = new SearchListPage(companyName);
                    Pages();
                    break;
                case "Print":              
                    //Global.Related.PageType =button.Tag.ToString();
                    Global.Related.CompanyData.CompanyID = CompanyID;
                    Global.Related.CompanyData.CompanyName = companyName;
                    Global.Related.CompanyData.USCI = listItems[0].USCI;
                    //Content = new Report();
                    //Pages();
                    Content = new IDCardPage();
                    Pages();
                    break;
                default:
                    break;
            }
        }
        private BrushConverter Use1 = new BrushConverter();

        int Zindex = 1;
        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Zindex += 1;
            Time.Countdown = 59;
            List<Button> ButtonList = new List<Button>() { basicButton, EquityButton,PropertyButton, JudicialButton, TrademarkButton };
            List<Border> BorderList = new List<Border>() { basicBorder, EquityBorder, PropertyBorder, JudicialBorder, TrademarkBorder };

            foreach (Border border in BorderList)
            {
                border.Background = (Brush)Use1.ConvertFromInvariantString("#eeeeef");
            }

            foreach (Button button1 in ButtonList)
            {
                button1.FontSize = 25;
                button1.FontWeight = FontWeights.Normal;
                button1.Foreground = Brushes.Black;
            }

            
            Button button = sender as Button;
            button.FontSize = 26;
            button.Foreground = (Brush)Use1.ConvertFromInvariantString("#ff8160");
            //button.Foreground = Brushes.Red;
            button.FontWeight = FontWeights.Bold;
            switch (button.Tag)
            {
                case "Basic":
                    Panel.SetZIndex(BasicInfoShow,Zindex);
                    basicBorder.Background = (Brush)Use1.ConvertFromInvariantString("#bedffc");
                    break;
                case "Equity":
                    Panel.SetZIndex(EquityInfoShow, Zindex);
                    EquityBorder.Background = (Brush)Use1.ConvertFromInvariantString("#bedffc");

                    break;
                case "Property":
                    Panel.SetZIndex(PropertyInfoShow, Zindex);
                    PropertyBorder.Background = (Brush)Use1.ConvertFromInvariantString("#bedffc");
                    break;
                case "Judicial":
                    Panel.SetZIndex(JudicialInfoShow, Zindex);
                    JudicialBorder.Background = (Brush)Use1.ConvertFromInvariantString("#bedffc");

                    break;
                case "Trademark":
                    Panel.SetZIndex(TrademarkShow, Zindex);
                    TrademarkBorder.Background = (Brush)Use1.ConvertFromInvariantString("#bedffc");

                    break;
                default:
                    break;
            }
        }

        private void Button_Equity_Click(object sender, RoutedEventArgs e)
        {
            Zindex += 1;
            List<Button> ButtonList = new List<Button>() { PartnersButton, InvestmentButton, BranchButton };
            foreach (Button temp in ButtonList)
            {
                temp.FontSize = 20;
                temp.FontWeight = FontWeights.Normal;
                temp.Foreground = Brushes.Black;
                temp.Background = Brushes.White;
            }
            Button button = sender as Button;
            button.FontSize = 21;
            button.Foreground = (Brush)Use1.ConvertFromInvariantString("#ff8160");
            button.Background = (Brush)Use1.ConvertFromInvariantString("#e6f1fb");
            button.FontWeight = FontWeights.Bold;
            switch (button.Tag)
            {
                case "Partners":
                    Panel.SetZIndex(PartnersGrid, Zindex);
                    break;
                case "Investment":
                    Panel.SetZIndex(InvestmentGrid, Zindex);
                    break;
                case "Branch":
                    Panel.SetZIndex(BranchGrid, Zindex);
                    break;

                default:
                    break;
            }
        }



        private void Button_PropertyInfo_Click(object sender, RoutedEventArgs e)
        {
            Zindex += 1;
            List<Button> ButtonList = new List<Button>() { PatentButton, CopyrightButton };
            foreach (Button temp in ButtonList)
            {
                temp.FontSize = 20;
                temp.FontWeight = FontWeights.Normal;
                temp.Foreground = Brushes.Black;
                temp.Background = Brushes.White;
            }
            Button button = sender as Button;
            button.FontSize = 21;
            button.Foreground = (Brush)Use1.ConvertFromInvariantString("#ff8160");
            button.Background = (Brush)Use1.ConvertFromInvariantString("#e6f1fb");
            button.FontWeight = FontWeights.Bold;
            switch (button.Tag)
            {
                case "Patent":
                    Panel.SetZIndex(PatentGrid, Zindex);
                    break;
                case "Copyright":
                    Panel.SetZIndex(copyrightGrid, Zindex);
                    break;
                default:
                    break;
            }

        }

        private void Button_Judicial_Click(object sender, RoutedEventArgs e)
        {
            Zindex += 1;
            List<Button> ButtonList = new List<Button>() { RefereeButton, AnnouncementButton, BusinessAbnormalionButton };
            foreach (Button temp in ButtonList)
            {
                temp.FontSize = 20;
                temp.FontWeight = FontWeights.Normal;
                temp.Foreground = Brushes.Black;
                temp.Background = Brushes.White;
            }
            Button button = sender as Button;
            button.FontSize = 21;
            button.Foreground = (Brush)Use1.ConvertFromInvariantString("#ff8160");
            button.Background = (Brush)Use1.ConvertFromInvariantString("#e6f1fb");
            button.FontWeight = FontWeights.Bold;
            switch (button.Tag)
            {
                case "Referee":
                    Panel.SetZIndex(RefereeGrid, Zindex);
                    break;
                case "Announcement":
                    Panel.SetZIndex(AnnouncementGrid, Zindex);
                    break;
                case "BusinessAbnormalion":
                    //Panel.SetZIndex(BranchGrid, Zindex);
                    break;

                default:
                    break;
            }
        }

    }
    #region 配置类型

    //专利信息
    public class PatentDetailItem
    {
        public int xh { get; set; }//展示序号
        public string title { get; set; }//标题
        public string publicNumber { get; set; }//公示号
        public string kindCodeCesc { get; set; }//专利类型
        public string publicDate { get; set; }//公开时间
        public string agency { get; set; }//代理公司
        public string agent { get; set; }//代理人
        public string patentDescription { get; set; }//专利描述
        public string patentImageUrl { get; set; }//专利图片
        public string inventorList { get; set; }//发明者
        public string applicationNum { get; set; }
        public string companyName { get; set; }
    }
    //软件著作信息
    public class CopyrightDetailItem
    {
        public int xh { get; set; }
        public string name { get; set; }
        public string regNum { get; set; }//登记号
        public string owner { get; set; }//著作权人
        public string regDate { get; set; }//注册登报日期
        public string category { get; set; }//种类目录
        public string publishDate { get; set; }//公开日期
    }
    //商标信息
    public class TrademarkItem
    {
        public int xh { get; set; }
        public string trademarkName { get; set; }//商标名称
        public string imageUrl { get; set; }//商标图片URL
        public string regNo { get; set; }//工商注册号
        public string appDate { get; set; }//商标注册时间
        public string appCn { get; set; }//申请人名称
        public string agent { get; set; }//代理商
        public string intCls { get; set; }//商标类型
        public string flowStatus { get; set; }//流动状态
    }

    //裁判文书
    public class RefereeItem
    {
        public int xh { get; set; }
        public string total { get; set; } //总数
        public string caseNo { get; set; }//文书编号
        public string court { get; set; }//法院
        public string caseName { get; set; }//裁判文书名称
        public string caseType { get; set; }//裁判文书类型
        public string judgeDate { get; set; }// 判决日期

        public bool ischech { get; set; }

    }
    //法院公告
    public class AnnouncementItem
    {
        public int xh { get; set; }
        public string publishedDate { get; set; }//公开日期
        public string category { get; set; }//公告类型
        public string content { get; set; }//内容
        public string party { get; set; }//当事人
    }
    //股权分配
    public class PartnersItem
    {
        public int xh { get; set; }

        public string stockName { get; set; }//股东姓名

        public string stockPercent { get; set; }//占股比例

        public string stockType { get; set; }//股东类型
        public string shouldMoey { get; set; }//投资（应缴）资金
        public string realCapi { get; set; }//实缴出资额
        public string investType { get; set; }//投资类型
        public string identifyCode { get; set; }//身份证号
        public string identifyType { get; set; }//非公示项
        public string shouldDate { get; set; }//身份证号
    }
    //投资信息
    public class InvestmentItem
    {
        public int xh { get; set; }
        public string CompanyID { get; set; }
        public string investedCompany { get; set; }//被投资的企业名称
        public string econKind { get; set; }//被投资的企业类型
        public string status { get; set; }//被投资的企业现状
        public string creditCode { get; set; }//被投资的企业的统一社会信用代码
        public string no { get; set; }//被投资的企业的工商注册号
        public string startDate { get; set; }//被投资的企业的成立日期
        public string fundedRatio { get; set; }//投资比例
        public string regMoney { get; set; }//投资数额

    }
    //分支
    public class BranchItem
    {
        public int xh { get; set; }
        public string CompanyID { get; set; }
        public string branchesAme { get; set; }//分支机构名称
    }
    //经营异常
    public class BusinessAbnormalionItem
    {
        public int xh { get; set; }
        public string branchesAme { get; set; }//分支机构名称
    }

    public class TOPOItem
    {
        public static Stack ComapnyName = new Stack();
        public static Stack CompanyID = new Stack();

    }


    #endregion
   
}
