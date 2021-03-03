using EXCResources;
using iTextSharp.text;
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
    /// ShenZhenDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class ShenZhenDetailPage : Page
    {
        string CompanyID = null;
        string Types = null;
        
        public ShenZhenDetailPage(string CompanyID,string Types = "1")
        {

            this.CompanyID = CompanyID;
            this.Types = Types;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Thread worker = new Thread(() => GetDetail())
            {
                IsBackground = true
            };
            worker.Start();
        }
        private void GetDetail()
        {
            string response = Webservice.ShenZhen.CompanyDetail(CompanyID,Types);

            Dispatcher.BeginInvoke(new Action(() => ShenZhenPhrase(response)));
        }
        private void ShenZhenPhrase(string response)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);

            string Code = document.SelectSingleNode("//data/returncode").InnerText;
            string Msg = document.SelectSingleNode("//data/returnmsg").InnerText;
            if (Code.Equals("1"))
            {
                CompanyItem item = new CompanyItem();
                string baseinfo = "//data/result/record/baseinfo";

                //基本信息
                string bases = baseinfo + "/registerinfo/base";

                //企业名称
                item.CompanyName = document.SelectSingleNode(bases + "//name") == null ? "--" : document.SelectSingleNode(bases + "//name").InnerText;


                //统一社会信用代码
                item.USCI = document.SelectSingleNode(bases + "/tyshxydm") == null ? "--" : document.SelectSingleNode(bases + "/tyshxydm").InnerText;

                //注册号
                item.BRN = document.SelectSingleNode(bases + "/gszch") == null ? "--" : document.SelectSingleNode(bases + "/gszch").InnerText;

                //法定代表人
                item.LegalRepresentative = document.SelectSingleNode(bases + "/fddbr") == null ? "--" : document.SelectSingleNode(bases + "/fddbr").InnerText;

                //法定代身份证号
                item.LegalRepresentativeIDCardNo = document.SelectSingleNode(bases + "/sfzhm") == null ? "" : document.SelectSingleNode(bases + "/sfzhm").InnerText;
                item.LegalRepresentativeIDCardNo = item.LegalRepresentativeIDCardNo.Substring(0, 3) + "***********" + item.LegalRepresentativeIDCardNo.Substring(14, 4);


                //住所
                //items.zcdz = document.SelectSingleNode(bases + "/zcdz") == null ? "" : document.SelectSingleNode(bases + "/zcdz").InnerText;
                //labeladdress.Text = items.zcdz;
                //basicRegAddressTextBlock.Text = items.zcdz;

                //成立日期
                item.Establishment = document.SelectSingleNode(bases + "/clrq") == null ? "" : document.SelectSingleNode(bases + "/clrq").InnerText;

                //营业期限
                item.LicenseDate = document.SelectSingleNode(bases + "/yyqx") == null ? "" : document.SelectSingleNode(bases + "/yyqx").InnerText;


                ////核准日期
                //items.hzrq = document.SelectSingleNode(bases + "/hzrq") == null ? "" : document.SelectSingleNode(bases + "/hzrq").InnerText;

                //认缴注册资本总额
                item.RegisteredCapital = document.SelectSingleNode(bases + "/zczb") == null ? "" : document.SelectSingleNode(bases + "/zczb").InnerText;

                //企业类型
                item.Type = document.SelectSingleNode(bases + "/jglx") == null ? "" : document.SelectSingleNode(bases + "/jglx").InnerText;

                //企业登记状态
                item.BusinessStatus = document.SelectSingleNode(bases + "/qyztm") == null ? "" : document.SelectSingleNode(bases + "/qyztm").InnerText;

                ////年报情况
                //items.nbqk = document.SelectSingleNode(bases + "/nbqk") == null ? "" : document.SelectSingleNode(bases + "/nbqk").InnerText;

                //经营范围
                item.BusinessScope = document.SelectSingleNode(bases + "/jyfw") == null ? "" : document.SelectSingleNode(bases + "/jyfw").InnerText;

                //提示
                //items.ts = document.SelectSingleNode(bases + "/ts") == null ? "" : document.SelectSingleNode(bases + "/ts").InnerText;
                //basicEconKindTextBlock.Text = items.ts;

                //企业股东信息
                XmlNodeList sockinfoList = document.SelectNodes(baseinfo + "/sockinfoList/sockinfo") == null ? null : document.SelectNodes(baseinfo + "/sockinfoList/sockinfo");
                if (sockinfoList != null && sockinfoList.Count > 0)
                {
                    sockinfoItems = new ObservableCollection<SockinfoItem>();
                    sockinfoListView.ItemsSource = sockinfoItems;
                    sockinfoBool = true;
                    int i = 0;
                    foreach (XmlNode node in sockinfoList)
                    {
                        SockinfoItem item = new SockinfoItem();

                        item.xh = ++i;
                        //股东名称
                        item.gdmc = node["gdmc"] == null ? "" : node["gdmc"].InnerText;
                        //身份证号码
                        string sfzhm = node["sfzhm"] == null ? "" : node["sfzhm"].InnerText;
                        if (!"".Equals(sfzhm) && sfzhm.Length == 18)
                        {
                            string start = sfzhm.Substring(0, 3);
                            string end = sfzhm.Substring(14, 4);
                            item.sfzhm = start + "***********" + end;
                        }
                        else
                        {
                            item.sfzhm = sfzhm;
                        }
                        //出资额(万)
                        item.cze = node["cze"] == null ? "" : node["cze"].InnerText;
                        //出资比例(%)
                        item.czbl = node["czbl"] == null ? "" : node["czbl"].InnerText;

                        sockinfoItems.Add(item);
                    }

                }


                //管理人员信息
                XmlNodeList mainmemberList = document.SelectNodes(baseinfo + "/mainmemberList/mainmember") == null ? null : document.SelectNodes(baseinfo + "/mainmemberList/mainmember");
                if (mainmemberList != null && mainmemberList.Count > 0)
                {
                    mainmemberItems = new ObservableCollection<mainmemberItem>();
                    mainmemberListView.ItemsSource = mainmemberItems;
                    mainmemberBool = true;
                    int i = 0;
                    foreach (XmlNode node in mainmemberList)
                    {
                        mainmemberItem item = new mainmemberItem();
                        item.xh = ++i;
                        //姓名
                        item.xm = node["xm"] == null ? "" : node["xm"].InnerText;
                        //职务
                        item.zw = node["zw"] == null ? "" : node["zw"].InnerText;
                        //身份证号码
                        string sfzhm = node["sfzhm"] == null ? "" : node["sfzhm"].InnerText;
                        if (!"".Equals(sfzhm) && sfzhm.Length == 18)
                        {
                            string start = sfzhm.Substring(0, 3);
                            string end = sfzhm.Substring(14, 4);
                            item.sfzhm = start + "***********" + end;
                        }
                        else
                        {
                            item.sfzhm = sfzhm;
                        }
                        mainmemberItems.Add(item);
                    }
                }

                //变更信息
                XmlNodeList changeinfoList = document.SelectNodes(baseinfo + "/changeinfoList/changeinfo") == null ? null : document.SelectNodes(baseinfo + "/changeinfoList/changeinfo");

                if (changeinfoList != null && changeinfoList.Count > 0)
                {
                    changeinfoItems = new ObservableCollection<changeinfoItem>();
                    changeinfoListView.ItemsSource = changeinfoItems;
                    changeinfoBool = true;
                    int i = 0;
                    foreach (XmlNode node in changeinfoList)
                    {
                        changeinfoItem item = new changeinfoItem();
                        item.xh = ++i;
                        //变更内容
                        item.bgnr = node["bgnr"] == null ? "" : node["bgnr"].InnerText;
                        //变更前
                        item.bgq = node["bgq"] == null ? "" : node["bgq"].InnerText;
                        //变更后
                        item.bgh = node["bgh"] == null ? "" : node["bgh"].InnerText;
                        //变更时间
                        item.bgsj = node["bgsj"] == null ? "" : node["bgsj"].InnerText;

                        changeinfoItems.Add(item);
                    }

                }

                string blacklist = "//data/result/record/blacklist";

                //海关失信企业信息
                XmlNodeList customsDiscreditEnterpriseList = document.SelectNodes(blacklist + "/customsDiscreditEnterpriseList/customsDiscreditEnterprise") == null ? null : document.SelectNodes(blacklist + "/customsDiscreditEnterpriseList/customsDiscreditEnterprise");
                if (customsDiscreditEnterpriseList != null && customsDiscreditEnterpriseList.Count > 0)
                {
                    customsDiscreditEnterpriseItem = new ObservableCollection<CustomsDiscreditEnterpriseItem>();
                    customsDiscreditEnterpriseListView.ItemsSource = customsDiscreditEnterpriseItem;
                    customsNumBool = true;
                    int i = 0;
                    foreach (XmlNode node in customsDiscreditEnterpriseList)
                    {
                        CustomsDiscreditEnterpriseItem item = new CustomsDiscreditEnterpriseItem();
                        item.xh = ++i;
                        //企业中文名称
                        item.qymc = node["qymc"] == null ? "" : node["qymc"].InnerText;
                        //海关编码
                        item.hgbm = node["hgbm"] == null ? "" : node["hgbm"].InnerText;
                        //组织机构代码
                        item.zzjgdm = node["zzjgdm"] == null ? "" : node["zzjgdm"].InnerText;
                        //工商注册日期
                        item.zcrq = node["zcrq"] == null ? "" : node["zcrq"].InnerText;
                        //海关每次注册日期
                        item.hgmczcrq = node["hgmczcrq"] == null ? "" : node["hgmczcrq"].InnerText;
                        //海关信用等级
                        item.sxdj = node["sxdj"] == null ? "" : node["sxdj"].InnerText;
                        //海关信用等级认定时间
                        item.xydjrdsj = node["xydjrdsj"] == null ? "" : node["xydjrdsj"].InnerText;

                        customsDiscreditEnterpriseItem.Add(item);
                    }
                }

                //证监会市场禁入信息
                XmlNodeList CSRCnoEntryList = document.SelectNodes(blacklist + "/CSRCnoEntryList/CSRCnoEntry") == null ? null : document.SelectNodes(blacklist + " /CSRCnoEntryList/CSRCnoEntry");
                if (CSRCnoEntryList != null && CSRCnoEntryList.Count > 0)
                {
                    CSRCnoEntryInfoItem = new ObservableCollection<CSRCnoEntryInfoItem>();
                    CSRCnoEntryListView.ItemsSource = CSRCnoEntryInfoItem;
                    CSRCnoEntryNumBool = true;
                    int i = 0;
                    foreach (XmlNode node in CSRCnoEntryList)
                    {
                        CSRCnoEntryInfoItem item = new CSRCnoEntryInfoItem();
                        item.xh = ++i;
                        //处罚对象
                        item.cfdx = node["cfdx"] == null ? "" : node["cfdx"].InnerText;
                        //处罚名称
                        item.cfmc = node["cfmc"] == null ? "" : node["cfmc"].InnerText;
                        //处罚类型
                        item.cflx = node["cflx"] == null ? "" : node["cflx"].InnerText;
                        //处罚日期
                        item.cfrq = node["cfrq"] == null ? "" : node["cfrq"].InnerText;

                        CSRCnoEntryInfoItem.Add(item);
                    }
                }

                //重大税收
                XmlNodeList majorTaxList = document.SelectNodes(blacklist + "/majorTaxList/majorTax") == null ? null : document.SelectNodes(blacklist + "/majorTaxList/majorTax");

                if (majorTaxList != null && majorTaxList.Count > 0)
                {
                    majorTaxItems = new ObservableCollection<MajorTaxItem>();
                    majorTaxListView.ItemsSource = majorTaxItems;
                    majorTaxNumBool = true;
                    int i = 0;
                    foreach (XmlNode node in majorTaxList)
                    {
                        MajorTaxItem item = new MajorTaxItem();
                        item.xh = ++i;
                        //处罚决定时间
                        item.cfrq = node["cfrq"] == null ? "" : node["cfrq"].InnerText;
                        //案件性质
                        item.ajxz = node["ajxz"] == null ? "" : node["ajxz"].InnerText;
                        //处罚名称
                        item.cfmc = node["cfmc"] == null ? "" : node["cfmc"].InnerText;
                        //纳税人识别号
                        item.nsrsbh = node["nsrsbh"] == null ? "" : node["nsrsbh"].InnerText;
                        //注册地址
                        item.zcdz = node["zcdz"] == null ? "" : node["zcdz"].InnerText;
                        //主要违法事实
                        item.wfss = node["wfss"] == null ? "" : node["wfss"].InnerText;
                        //处罚情况
                        item.cfqk = node["cfqk"] == null ? "" : node["cfqk"].InnerText;
                        //信息来源单位
                        item.xxly = node["xxly"] == null ? "" : node["xxly"].InnerText;

                        majorTaxItems.Add(item);
                    }
                }

                //最高法院失信被执行人信息
                XmlNodeList SGCBreakFaithList = document.SelectNodes(blacklist + "/SGCBreakFaithList/SGCBreakFaith") == null ? null : document.SelectNodes(blacklist + "/SGCBreakFaithList/SGCBreakFaith");

                if (SGCBreakFaithList != null && SGCBreakFaithList.Count > 0)
                {
                    SGCBreakFaithInfoItems = new ObservableCollection<SGCBreakFaithInfoItem>();
                    SGCBreakFaithListView.ItemsSource = SGCBreakFaithInfoItems;
                    SGCBreakFaithInfoNumBool = true;
                    int i = 0;
                    foreach (XmlNode node in SGCBreakFaithList)
                    {
                        SGCBreakFaithInfoItem item = new SGCBreakFaithInfoItem();
                        item.xh = ++i;
                        //被执行对象
                        item.bzxdx = node["bzxdx"] == null ? "" : node["bzxdx"].InnerText;
                        //执行案号
                        item.zxwah = node["zxwah"] == null ? "" : node["zxwah"].InnerText;
                        //案件类型
                        item.ajlx = node["ajlx"] == null ? "" : node["ajlx"].InnerText;
                        //当前状态
                        item.zt = node["zt"] == null ? "" : node["zt"].InnerText;
                        //执行法院
                        item.zxfy = node["zxfy"] == null ? "" : node["zxfy"].InnerText;
                        //发布时间
                        item.fbsj = node["fbsj"] == null ? "" : node["fbsj"].InnerText;

                        SGCBreakFaithInfoItems.Add(item);
                    }
                }

                //拖欠农民工工资信息
                XmlNodeList defaultWagesOfMigrantWorkersList = document.SelectNodes(blacklist + "/DefaultWagesOfMigrantWorkersList/DefaultWagesOfMigrantWorkers") == null ? null : document.SelectNodes(blacklist + "/DefaultWagesOfMigrantWorkersList/DefaultWagesOfMigrantWorkers");

                if (defaultWagesOfMigrantWorkersList != null && defaultWagesOfMigrantWorkersList.Count > 0)
                {
                    defaultWagesOfMigrantWorkersInfoItems = new ObservableCollection<DefaultWagesOfMigrantWorkersInfoItem>();
                    defaultWagesOfMigrantWorkersListView.ItemsSource = defaultWagesOfMigrantWorkersInfoItems;
                    defaultWagesOfMigrantWorkersNumBool = true;
                    int i = 0;
                    foreach (XmlNode node in defaultWagesOfMigrantWorkersList)
                    {
                        DefaultWagesOfMigrantWorkersInfoItem item = new DefaultWagesOfMigrantWorkersInfoItem();
                        item.xh = ++i;
                        //企业名称
                        item.qymc = node["qymc"] == null ? "" : node["qymc"].InnerText;
                        //列入名单事由
                        item.lrsy = node["lrsy"] == null ? "" : node["lrsy"].InnerText;
                        //列入时间
                        item.lrsj = node["lrsj"] == null ? "" : node["lrsj"].InnerText;

                        defaultWagesOfMigrantWorkersInfoItems.Add(item);
                    }
                }

                //安全生产领域联合惩戒对象信息
                XmlNodeList WorkSafetyAdministrationBlacklist = document.SelectNodes(blacklist + "/WorkSafetyAdministrationBlacklist/WorkSafetyAdministration") == null ? null : document.SelectNodes(blacklist + "/WorkSafetyAdministrationBlacklist/WorkSafetyAdministration");

                if (WorkSafetyAdministrationBlacklist != null && WorkSafetyAdministrationBlacklist.Count > 0)
                {
                    workSafetyAdministrationBlacklistInfoItems = new ObservableCollection<WorkSafetyAdministrationBlacklistInfoItem>();
                    workSafetyAdministrationBlacklistView.ItemsSource = workSafetyAdministrationBlacklistInfoItems;
                    workSafetyAdministrationBlacklistNumBool = true;
                    int i = 0;
                    foreach (XmlNode node in WorkSafetyAdministrationBlacklist)
                    {
                        WorkSafetyAdministrationBlacklistInfoItem item = new WorkSafetyAdministrationBlacklistInfoItem();
                        item.xh = ++i;
                        //单位名称
                        item.dwmc = node["dwmc"] == null ? "" : node["dwmc"].InnerText;
                        //统一社会信用代码
                        item.tyshxydm = node["tyshxydm"] == null ? "" : node["tyshxydm"].InnerText;
                        //主要负责人
                        item.zyfzr = node["zyfzr"] == null ? "" : node["zyfzr"].InnerText;
                        //失信行为简况
                        item.sxxwjk = node["sxxwjk"] == null ? "" : node["sxxwjk"].InnerText;
                        //信息报送机关
                        item.bsjg = node["bsjg"] == null ? "" : node["bsjg"].InnerText;
                        //纳入理由
                        item.nrly = node["nrly"] == null ? "" : node["nrly"].InnerText;

                        workSafetyAdministrationBlacklistInfoItems.Add(item);
                    }
                }

                ////税务登记信息
                ///**企业参保信息*/
                //string ginsengInfo = baseinfo + "/ginsengInfo";
                //XmlNode ginsengInfoElement = document.SelectSingleNode(ginsengInfo) == null ? null : document.SelectSingleNode(ginsengInfo);
                //if (ginsengInfoElement != null && !"".Equals(ginsengInfoElement))
                //{
                //    ginsengInfoItems = new ObservableCollection<ginsengInfoItem>();
                //    ginsengInfoListView.ItemsSource = ginsengInfoItems;
                //    ginsengInfoBool = true;

                //    string ginsengInfoText;
                //    //社保单位编号
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/sbbh") == null ? null : document.SelectSingleNode(ginsengInfo + "/sbbh").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "社保单位编号",
                //            text = ginsengInfoText
                //        });

                //    }
                ////投保起始年
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/tbqsn") == null ? "" : document.SelectSingleNode(ginsengInfo + "/tbqsn").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "投保起始年",
                //            text = ginsengInfoText
                //        });

                //    }
                //    //投保起始月
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/tbqsy") == null ? "" : document.SelectSingleNode(ginsengInfo + "/tbqsy").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "投保起始月",
                //            text = ginsengInfoText
                //        });

                //    }
                //    //当前状态
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/zt") == null ? "" : document.SelectSingleNode(ginsengInfo + "/zt").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "当前状态",
                //            text = ginsengInfoText
                //        });

                //    }
                //    //参保总人数
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/cbrs") == null ? "" : document.SelectSingleNode(ginsengInfo + "/cbrs").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "参保总人数",
                //            text = ginsengInfoText
                //        });

                //    }
                //    //养老参保人数
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/ylrs") == null ? "" : document.SelectSingleNode(ginsengInfo + "/ylrs").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "养老参保人数",
                //            text = ginsengInfoText
                //        });

                //    }
                //    //医疗参保人数
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/ylcbrs") == null ? "" : document.SelectSingleNode(ginsengInfo + "/ylcbrs").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "医疗参保人数",
                //            text = ginsengInfoText
                //        });

                //    }
                //    //工伤参保人数
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/gsrs") == null ? "" : document.SelectSingleNode(ginsengInfo + "/gsrs").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "工伤参保人数",
                //            text = ginsengInfoText
                //        });

                //    }
                //    //失业参保人数
                //    ginsengInfoText = document.SelectSingleNode(ginsengInfo + "/syrs") == null ? "" : document.SelectSingleNode(ginsengInfo + "/syrs").InnerText;
                //    if (ginsengInfoText != null)
                //    {
                //        ginsengInfoItems.Add(new ginsengInfoItem()
                //        {
                //            label = "失业参保人数",
                //            text = ginsengInfoText
                //        });

                //    }
                //}


                ////住房公积金缴存数据表
                //XmlNodeList housingFundList = document.SelectNodes(baseinfo + "/housingFundList/housingFund") == null ? null : document.SelectNodes(baseinfo + "/housingFundList/housingFund");

                //if (housingFundList != null && housingFundList.Count > 0)
                //{
                //    housingFundItems = new ObservableCollection<housingFundItem>();
                //    housingFundListView.ItemsSource = housingFundItems;
                //    housingFundBool = true;

                //    foreach (XmlNode node in housingFundList)
                //    {
                //        housingFundItem item = new housingFundItem();
                //        //单位公积金号
                //        item.gjjh = node["gjjh"] == null ? "" : node["gjjh"].InnerText;
                //        //单位开户时间
                //        item.khrq = node["khrq"] == null ? "" : node["khrq"].InnerText;
                //        //公积金缴存截至时间
                //        item.jzrq = node["jzrq"] == null ? "" : node["jzrq"].InnerText;
                //        //近12个月单位账户状态
                //        item.zhzt = node["zhzt"] == null ? "" : node["zhzt"].InnerText;

                //        housingFundItems.Add(item);
                //    }
                //}


                ////市场监督
                //XmlNodeList marketSupervisionList = document.SelectNodes(blacklist + "/marketSupervisionList/marketSupervision");
                //if (marketSupervisionList != null && marketSupervisionList.Count > 0)
                //{
                //    marketSupervisionItems = new ObservableCollection<marketSupervisionItem>();
                //    marketSupervisionListView.ItemsSource = marketSupervisionItems;
                //    marketNumBool = true;

                //    foreach (XmlNode node in marketSupervisionList)
                //    {

                //        marketSupervisionItem item = new marketSupervisionItem();
                //        //列入时间
                //        item.lrsj = node["lrsj"] == null ? "" : node["lrsj"].InnerText;
                //        //列入原因
                //        item.lrsy = node["lrsy"] == null ? "" : node["lrsy"].InnerText;
                //        //状态
                //        item.zt = node["zt"] == null ? "" : node["zt"].InnerText;
                //        //登记机关
                //        item.jdjg = node["jdjg"] == null ? "" : node["jdjg"].InnerText;

                //        marketSupervisionItems.Add(item);
                //    }
                //}

                ////食品安全黑名单
                //XmlNodeList blacklistOfFoodSafetyList = document.SelectNodes(blacklist + "/blacklistOfFoodSafetyList/blacklistOfFoodSafety") == null ? null : document.SelectNodes(blacklist + "/blacklistOfFoodSafetyList/blacklistOfFoodSafety");

                //if (blacklistOfFoodSafetyList != null && blacklistOfFoodSafetyList.Count > 0)
                //{
                //    blacklistOfFoodSafetyItems = new ObservableCollection<blacklistOfFoodSafetyItem>();
                //    blacklistOfFoodSafetyListView.ItemsSource = blacklistOfFoodSafetyItems;
                //    foodSafetyNumBool = true;

                //    foreach (XmlNode node in blacklistOfFoodSafetyList)
                //    {

                //        blacklistOfFoodSafetyItem item = new blacklistOfFoodSafetyItem();
                //        //公布时间
                //        item.gbsj = node["gbsj"] == null ? "" : node["gbsj"].InnerText;
                //        //违法行为
                //        item.wfxw = node["wfxw"] == null ? "" : node["wfxw"].InnerText;
                //        //处罚情况
                //        item.cfxx = node["cfxx"] == null ? "" : node["cfxx"].InnerText;

                //        blacklistOfFoodSafetyItems.Add(item);
                //    }
                //}

                ////政府挂牌督办重大火灾隐患
                //XmlNodeList MajorFireHazardList = document.SelectNodes(blacklist + "/MajorFireHazardList/MajorFireHazard") == null ? null : document.SelectNodes(blacklist + "/MajorFireHazardList/MajorFireHazard");

                //if (MajorFireHazardList != null && MajorFireHazardList.Count > 0)
                //{
                //    majorFireHazardItems = new ObservableCollection<majorFireHazardItem>();
                //    MajorFireHazardListView.ItemsSource = majorFireHazardItems;
                //    majorFireHazardNumBool = true;

                //    foreach (XmlNode node in MajorFireHazardList)
                //    {
                //        majorFireHazardItem item = new majorFireHazardItem();
                //        //挂牌日期
                //        item.gprq = node["gprq"] == null ? "" : node["gprq"].InnerText;
                //        //整改日期
                //        item.zgrq = node["zgrq"] == null ? "" : node["zgrq"].InnerText;
                //        //挂牌级别
                //        item.gpjb = node["gpjb"] == null ? "" : node["gpjb"].InnerText;
                //        //摘牌日期
                //        item.zprq = node["zprq"] == null ? "" : node["zprq"].InnerText;
                //        //决定机关
                //        item.jdjg = node["jdjg"] == null ? "" : node["jdjg"].InnerText;

                //        majorFireHazardItems.Add(item);
                //    }

                //}

                ////安全生产不良记录黑名单
                //XmlNodeList badProductionRecordList = document.SelectNodes(blacklist + "/badProductionRecordList/badProductionRecord") == null ? null :document.SelectNodes(blacklist + "/badProductionRecordList/badProductionRecord");

                //if (badProductionRecordList != null && badProductionRecordList.Count > 0)
                //{
                //    badProductionRecordItems = new ObservableCollection<badProductionRecordItem>();
                //    badProductionRecordListView.ItemsSource = badProductionRecordItems;
                //    badProductionNumBool = true;

                //    foreach (XmlNode node in badProductionRecordList)
                //    {
                //        badProductionRecordItem item = new badProductionRecordItem();
                //        //处罚决定时间
                //        item.cfjdrq = node["cfjdrq"] == null ? "" : node["cfjdrq"].InnerText;
                //        //违法事实
                //        item.wfss = node["wfss"] == null ? "" : node["wfss"].InnerText;
                //        //纳入理由
                //        item.nrly = node["nrly"] == null ? "" : node["nrly"].InnerText;
                //        //执法部门
                //        item.zfbm = node["zfbm"] == null ? "" : node["zfbm"].InnerText;

                //        badProductionRecordItems.Add(item);
                //    }
                //}

                ////签发空头支票黑名单
                //XmlNodeList emptyEhequeList = document.SelectNodes(blacklist + "/emptyEhequeList/emptyEheque") == null ? null : document.SelectNodes(blacklist + "/emptyEhequeList/emptyEheque");

                //if (emptyEhequeList != null && emptyEhequeList.Count > 0)
                //{
                //    emptyEhequeItems = new ObservableCollection<emptyEhequeItem>();
                //    emptyEhequeListView.ItemsSource = emptyEhequeItems;
                //    emptyEhequeNumBool = true;

                //    foreach (XmlNode node in emptyEhequeList)
                //    {
                //        emptyEhequeItem item = new emptyEhequeItem();
                //        //发布时间
                //        item.fbsj = node["fbsj"] == null ? "" : node["fbsj"].InnerText;
                //        //空头支票章数
                //        item.ktzp = node["ktzp"] == null ? "" : node["ktzp"].InnerText;
                //        //逾期未缴纳次数
                //        item.yqwjn = node["yqwjn"] == null ? "" : node["yqwjn"].InnerText;

                //        emptyEhequeItems.Add(item);
                //    }
                //}

                ////拖欠金融机构贷款制裁记录
                //XmlNodeList arrearsOfSanctionsList = document.SelectNodes(blacklist + "/arrearsOfSanctionsList/arrearsOfSanctions") == null ? null : document.SelectNodes(blacklist + "/arrearsOfSanctionsList/arrearsOfSanctions");

                //if (arrearsOfSanctionsList != null && arrearsOfSanctionsList.Count > 0)
                //{
                //    arrearsOfSanctionsItems = new ObservableCollection<arrearsOfSanctionsItem>();
                //    arrearsOfSanctionsListView.ItemsSource = arrearsOfSanctionsItems;
                //    arrearsNumBool = true;

                //    foreach (XmlNode node in arrearsOfSanctionsList)
                //    {
                //        arrearsOfSanctionsItem item = new arrearsOfSanctionsItem();
                //        //时间
                //        item.sj = node["sj"] == null ? "" : node["sj"].InnerText;
                //        //贷款证号
                //        item.dkhh = node["dkhh"] == null ? "" : node["dkhh"].InnerText;
                //        //担保人
                //        item.dbr = node["dbr"] == null ? "" : node["dbr"].InnerText;

                //        arrearsOfSanctionsItems.Add(item);
                //    }
                //}

                ////环保失信企业信息
                //XmlNodeList environmentalFaithlessList = document.SelectNodes(blacklist + "/environmentalFaithlessList/environmentalFaithless") == null ? null : document.SelectNodes(blacklist + "/environmentalFaithlessList/environmentalFaithless");

                //if (environmentalFaithlessList != null && environmentalFaithlessList.Count > 0)
                //{
                //    environmentalFaithlessItems = new ObservableCollection<environmentalFaithlessItem>();
                //    environmentalFaithlessListView.ItemsSource = environmentalFaithlessItems;
                //    environmenNumBool = true;

                //    foreach (XmlNode node in environmentalFaithlessList)
                //    {
                //        environmentalFaithlessItem item = new environmentalFaithlessItem();
                //        //企业名称
                //        item.qymc = node["qymc"] == null ? "" : node["qymc"].InnerText;
                //        //环境违法情形
                //        item.hjwfqx = node["hjwfqx"] == null ? "" : node["hjwfqx"].InnerText;

                //        environmentalFaithlessItems.Add(item);
                //    }
                //}

            }
            else
            {
                Content = new HomePage(Msg);
                Pages();
            }
        }
        private void Pages()
        {
            //pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

    }
}

public class LegalEntityDetailItem
{
    public string tyshxydm { get; set; }
    public string name { get; set; }
    public string fddbr { get; set; }
    public string gszch { get; set; }
    public string sfzhm { get; set; }
    public string zczb { get; set; }
    public string zcdz { get; set; }
    public string nbqk { get; set; }
    public string clrq { get; set; }
    public string yyqx { get; set; }
    public string hzrq { get; set; }
    public string jglx { get; set; }
    public string jyfw { get; set; }
    public string qyztm { get; set; }
    public string ts { get; set; }
}

//企业股东信息
public class SockinfoItem
{
    public int xh { get; set; }
    public string gdmc { get; set; }
    public string sfzhm { get; set; }
    public string cze { get; set; }
    public string czbl { get; set; }
}
//管理人员信息
public class mainmemberItem
{
    public int xh { get; set; }
    public string xm { get; set; }
    public string zw { get; set; }
    public string sfzhm { get; set; }
}
//变更信息
public class changeinfoItem
{
    public int xh { get; set; }
    public string bgnr { get; set; }
    public string bgq { get; set; }
    public string bgh { get; set; }
    public string bgsj { get; set; }
}

////年报信息
//public class NbxxInfoItem
//{

//}

//海关失信企业信息
public class CustomsDiscreditEnterpriseItem
{
    public int xh { get; set; }
    public string qymc { get; set; }
    public string hgbm { get; set; }
    public string zzjgdm { get; set; }
    public string zcrq { get; set; }
    public string hgmczcrq { get; set; }
    public string sxdj { get; set; }
    public string xydjrdsj { get; set; }
}

//证监会市场禁入信息
public class CSRCnoEntryInfoItem
{
    public int xh { get; set; }
    public string cfdx { get; set; }
    public string cfmc { get; set; }
    public string cflx { get; set; }
    public string cfrq { get; set; }

}

//重大税收违法案件信息
public class MajorTaxItem
{
    public int xh { get; set; }
    public string cfrq { get; set; }
    public string ajxz { get; set; }
    public string cfmc { get; set; }
    public string nsrsbh { get; set; }
    public string zcdz { get; set; }
    public string wfss { get; set; }
    public string cfqk { get; set; }
    public string xxly { get; set; }
}
//最高法院失信被执行人信息
public class SGCBreakFaithInfoItem
{
    public int xh { get; set; }
    public string bzxdx { get; set; }
    public string zxwah { get; set; }
    public string ajlx { get; set; }
    public string zt { get; set; }
    public string zxfy { get; set; }
    public string fbsj { get; set; }
}
//拖欠农民工工资信息
public class DefaultWagesOfMigrantWorkersInfoItem
{
    public int xh { get; set; }
    public string qymc { get; set; }
    public string lrsy { get; set; }
    public string lrsj { get; set; }

}
//安监总局安全生产黑名单
public class WorkSafetyAdministrationBlacklistInfoItem
{
    public int xh { get; set; }
    public string dwmc { get; set; }
    public string tyshxydm { get; set; }
    public string zyfzr { get; set; }
    public string sxxwjk { get; set; }
    public string bsjg { get; set; }
    public string nrly { get; set; }
}
