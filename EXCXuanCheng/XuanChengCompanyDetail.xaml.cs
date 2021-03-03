using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace EXCXuanCheng
{
    /// <summary>
    /// XuanChengCompanyDetail.xaml 的交互逻辑
    /// </summary>
    public partial class XuanChengCompanyDetail : Page
    {
        string response_basic;
        string response_XKXX;
        string response_ZZXX;
        string response_CBXX;
        string response_LHXX;
        string response_BLXX;
        private string CompanyID = null;
        public XuanChengCompanyDetail(string CompanyID = null)
        {
            this.CompanyID = CompanyID;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Countdown_timer();

            Thread thread1 = new Thread(() => Requests("ZCXX"))
            {
                IsBackground = true
            };
            thread1.Start();

            Thread thread2 = new Thread(() => Requests("XKXX"))
            {
                IsBackground = true
            };
            thread2.Start();
            Thread thread3 = new Thread(() => Requests("ZZXX"))
            {
                IsBackground = true
            };
            thread3.Start();
            Thread thread4 = new Thread(() => Requests("CBXX"))
            {
                IsBackground = true
            };
            thread4.Start();
            Thread thread5 = new Thread(() => Requests("LHXX"))
            {
                IsBackground = true
            };
            thread5.Start();
            Thread thread6 = new Thread(() => Requests("BLXX"))
            {
                IsBackground = true
            };
            thread6.Start();
        }

        private void Requests(string Type)
        {

            switch (Type)
            {
                case "ZCXX":
                    response_basic = Http.XuanCheng.ZCXX(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => XuanChengListPhrase(response_basic, Type)));
                    break;
                case "XKXX":
                    response_XKXX = Http.XuanCheng.XKXX(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => XuanChengListPhrase(response_XKXX, Type)));
                    break;
                case "ZZXX":
                    response_ZZXX = Http.XuanCheng.ZZXX(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => XuanChengListPhrase(response_ZZXX, Type)));
                    break;
                case "CBXX":
                    response_CBXX = Http.XuanCheng.CBXX(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => XuanChengListPhrase(response_CBXX, Type)));
                    break;
                case "LHXX":
                    response_LHXX = Http.XuanCheng.LHXX(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => XuanChengListPhrase(response_LHXX, Type)));
                    break;
                case "BLXX":
                    response_BLXX = Http.XuanCheng.BLXX(CompanyID);
                    Dispatcher.BeginInvoke(new Action(() => XuanChengListPhrase(response_BLXX, Type)));
                    break;

                default:
                    break;
            }
        }


        Basic item1;
        private static ObservableCollection<XKXX> XKXXItems;
        private static ObservableCollection<ZZXX> ZZXXItems;
        private static ObservableCollection<CBXX> CBXXItems;
        private static ObservableCollection<NsxyjbInfoItem> NsxyjbInfoItems;
        private static ObservableCollection<JlInfoItem> JlInfoItems;
        private static ObservableCollection<RyInfoItem> RyInfoItems;
        private static ObservableCollection<SxbzxrInfoItem> SxbzxrInfoItems;
        private static ObservableCollection<XzcfInfoItem> XzcfInfoItems;
        private static ObservableCollection<SxqyInfoItem> SxqyInfoItems;
        int XuanChengXKNo = 0;
        int XuanChengZZNo = 0;
        int XuanChengCBNo = 0;
        int XuanChengLH_nsxyNo = 0;
        int XuanChengLH_jlNo = 0;
        int XuanChengLH_ryNo = 0;
        int XuanChengLH_sxbzxrNo = 0;
        int XuanChengLH_xzcfNo = 0;
        int XuanChengLH_sxqyNo = 0;
        private void XuanChengListPhrase(string response, string Type)
        {
            
      
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                string code = (string)@object.GetValue("flag");
                if (code == "True")
                {
                    switch (Type)
                    {
                        case "ZCXX":
                            item1 = new Basic();

                            JArray rows1 = (JArray)@object["rows"];

                            foreach (JObject result1 in rows1)
                            {
                                
                               
                                LabelUSCI.Content += item1.uniscid = (string)result1.GetValue("uniscid");
                                InFoTitle.Content = item1.baseDwmc = (string)result1.GetValue("baseDwmc");
                                LabelLegalRepresentative.Content += item1.baseFddbr = (string)result1.GetValue("baseFddbr");
                                item1.baseZch = (string)result1.GetValue("baseZch");
                                item1.baseJgdm = (string)result1.GetValue("baseJgdm");
                                item1.baseZczj = (string)result1.GetValue("baseZczj");
                                item1.baseHy = (string)result1.GetValue("baseHy");
                                item1.baseSzqh = (string)result1.GetValue("baseSzqh");
                                LabelChengLiData.Content += item1.baseClrq = (string)result1.GetValue("baseClrq");
                                LabelRegisteredAddress.Content += item1.baseXxdz = (string)result1.GetValue("baseXxdz");
                                item1.baseQyzl = (string)result1.GetValue("baseQyzl");
                                item1.baseZycp = (string)result1.GetValue("baseZycp");
                                item1.baseDwjj = (string)result1.GetValue("baseDwjj");
                                item1.zjze = (string)result1.GetValue("zjze");
                                item1.xydm = (string)result1.GetValue("xydm");
                                item1.baseSwdjh = (string)result1.GetValue("baseSwdjh");
                                item1.baseZcrq = (string)result1.GetValue("baseZcrq");
                                item1.baseNsrzt = (string)result1.GetValue("baseNsrzt");
                                item1.baseGdsx = (string)result1.GetValue("baseGdsx");
                                item1.baseZzjgxz = (string)result1.GetValue("baseZzjgxz");
                                labelscpoe.Text = item1.baseJyfw = (string)result1.GetValue("baseJyfw");
                                item1.gxrq = (string)result1.GetValue("gxrq");
                                item1.baseLxdh = (string)result1.GetValue("baseLxdh");
                                item1.baseYzbm = (string)result1.GetValue("baseYzbm");
                                
                            }

                            break;
                        case "XKXX":
                            XKXXItems = new ObservableCollection<XKXX>();
                            JArray rows2 = (JArray)@object["rows"];
                            XuanChengXKListView.ItemsSource = XKXXItems;
                            foreach (JObject result1 in rows2)
                            {
                                XuanChengXKNo += 1;
                                XKXX item11 = new XKXX {
                                ListNo = XuanChengXKNo,
                                jdswh = (string)result1.GetValue("jdswh"),
                                xmmc = (string)result1.GetValue("xmmc"),
                                splb = (string)result1.GetValue("splb"),
                                jdrq = (string)result1.GetValue("jdrq"),
                                jzrq = (string)result1.GetValue("jzrq"),
                                xknr = (string)result1.GetValue("xknr"),
                                xzjg = (string)result1.GetValue("xzjg"),
                                ztmc = (string)result1.GetValue("ztmc"),
                                    };

                            XKXXItems.Add(item11);
                            }
                            if (XKXXItems.Count == 0)
                            {
                                XuanChengXKMsg.Visibility = Visibility.Visible;
                            }


                            break;
                        case "ZZXX":
                            ZZXXItems = new ObservableCollection<ZZXX>();
                         
                            JArray rows3 = (JArray)@object["rows"];
                            XuanChengZZListView.ItemsSource = ZZXXItems;
                            foreach (JObject result1 in rows3)
                            {
                              
                                XuanChengZZNo += 1;
                                ZZXX zzxxItem = new ZZXX
                                {
                                    ListNo = XuanChengZZNo,
                                    zsmc = (string)result1.GetValue("zsmc"),
                                    zsbh = (string)result1.GetValue("zsbh"),
                                    zzlb = (string)result1.GetValue("zzlb"),
                                    zzdj = (string)result1.GetValue("zzdj"),
                                    zznr = (string)result1.GetValue("zznr"),
                                    fbrq = (string)result1.GetValue("fbrq"),
                                };

                                ZZXXItems.Add(zzxxItem);
                            }
                            if (ZZXXItems.Count == 0)
                            {
                                XuanChengZZMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "CBXX":
                            CBXXItems = new ObservableCollection<CBXX>();
                      
                            JArray rows4 = (JArray)@object["rows"];
                            XuanChengCBListView.ItemsSource = CBXXItems;
                            foreach (JObject result1 in rows4)
                            {
                                XuanChengCBNo += 1;
                                
                                CBXX cbxxItem = new CBXX
                                {
                                    ListNo = XuanChengCBNo,
                                    cbrq = (string)result1.GetValue("cbrq"),
                                    cbzt_mc = (string)result1.GetValue("cbzt_mc"),
                                    bllb = (string)result1.GetValue("bllb"),
                                    xzlx_mc = (string)result1.GetValue("xzlx_mc"),
                                    jgdm = (string)result1.GetValue("jgdm"),
                                };

                                CBXXItems.Add(cbxxItem);
                            }
                            if (CBXXItems.Count == 0)
                            {
                                XuanChengCBMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "LHXX":
                            NsxyjbInfoItems = new ObservableCollection<NsxyjbInfoItem>();
                            JlInfoItems = new ObservableCollection<JlInfoItem>();
                            RyInfoItems = new ObservableCollection<RyInfoItem>();
                    
                            JObject data = (JObject)@object["data"];
                            JArray nsxy = (JArray)data["nsxyjbInfo"];
                            nsxyListView.ItemsSource = NsxyjbInfoItems;
                            foreach (JObject result1 in nsxy)
                            {
                                XuanChengLH_nsxyNo += 1;
                                
                                NsxyjbInfoItem nsxy1 = new NsxyjbInfoItem
                                { 
                                    ListNo = XuanChengLH_nsxyNo,
                                    qymc = (string)result1.GetValue("qymc"),
                                    sxnr = (string)result1.GetValue("sxnr"),
                                    pddj = (string)result1.GetValue("pddj"),
                                    fbbm = (string)result1.GetValue("fbbm"),
                                    pdnr = (string)result1.GetValue("pdnr"),
                                    pdnd = (string)result1.GetValue("pdnd"),
                                    pdrq = (string)result1.GetValue("pdrq"),
                                };

                                NsxyjbInfoItems.Add(nsxy1);
                            }
                            if (NsxyjbInfoItems.Count == 0)
                            {
                                nsxyMsg.Visibility = Visibility.Visible;
                            }
                            JArray jl = (JArray)data["jlInfo"];
                            jlListView.ItemsSource = JlInfoItems;
                            foreach (JObject result1 in jl)
                            {
                                
                                XuanChengLH_jlNo += 1;
                                
                                JlInfoItem jl1 = new JlInfoItem
                                {
                                    ListNo = XuanChengLH_jlNo,
                                    jlmc = (string)result1.GetValue("jlmc"),
                                    jlzsbh = (string)result1.GetValue("jlzsbh"),
                                    jlsj = (string)result1.GetValue("jlsj"),
                                    jllb_mc = (string)result1.GetValue("jllb_mc"),
                                    jldj = (string)result1.GetValue("jldj"),
                                    jlje = (string)result1.GetValue("jlje"),
                                    jlyy = (string)result1.GetValue("jlyy"),
                                };
                                JlInfoItems.Add(jl1);
                            }
                            if (JlInfoItems.Count == 0)
                            {
                                jlMsg.Visibility = Visibility.Visible;
                            }
                            JArray ry = (JArray)data["ryInfo"];
                            ryListView.ItemsSource = RyInfoItems;
                            foreach (JObject result1 in ry)
                            {
                               
                                XuanChengLH_ryNo += 1;
                                
                                RyInfoItem ry1 = new RyInfoItem
                                {
                                    ListNo = XuanChengLH_ryNo,
                                    jgmc = (string)result1.GetValue("jgmc"),
                                    jdswh = (string)result1.GetValue("jdswh"),
                                    rymc = (string)result1.GetValue("rymc"),
                                    rynr = (string)result1.GetValue("rynr"),
                                    bzdw = (string)result1.GetValue("bzdw"),
                                    qdsj = (string)result1.GetValue("qdsj"),

                                };
                                RyInfoItems.Add(ry1);

                                
                            }
                            if (RyInfoItems.Count == 0)
                            {
                                ryMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        case "BLXX":
                            SxbzxrInfoItems = new ObservableCollection<SxbzxrInfoItem>();
                            XzcfInfoItems = new ObservableCollection<XzcfInfoItem>();
                            SxqyInfoItems = new ObservableCollection<SxqyInfoItem>();
                            JObject BLXX_data = (JObject)@object["data"];
                            JArray sxbzxr = (JArray)BLXX_data["sxbzxrInfo"];
                            sxbzxrListView.ItemsSource = SxbzxrInfoItems;
                            foreach (JObject result1 in sxbzxr)
                            {
                               
                                XuanChengLH_sxbzxrNo += 1;                               
                                SxbzxrInfoItem sxbzxr1 = new SxbzxrInfoItem
                                {
                                    ListNo = XuanChengLH_sxbzxrNo,
                                    ajbh = (string)result1.GetValue("ajbh"),
                                    lxqk = (string)result1.GetValue("lxqk"),
                                    fbrq = (string)result1.GetValue("fbrq"),
                                    zxfy = (string)result1.GetValue("zxfy"),

                                };
                                SxbzxrInfoItems.Add(sxbzxr1);
                            }
                            if (SxbzxrInfoItems.Count == 0)
                            {
                                sxbzxrMsg.Visibility = Visibility.Visible;
                            }
                            JArray xzcf = (JArray)BLXX_data["xzcfInfo"];
                            xzcfListView.ItemsSource = XzcfInfoItems;
                            foreach (JObject result1 in xzcf)
                            {
                               
                                XuanChengLH_xzcfNo += 1;
                               
                                XzcfInfoItem xzcf1 = new XzcfInfoItem
                                {
                                    ListNo = XuanChengLH_xzcfNo,
                                    jdswh = (string)result1.GetValue("jdswh"),
                                    cfmc = (string)result1.GetValue("cfmc"),
                                    cflb1 = (string)result1.GetValue("cflb1"),
                                    cflb2 = (string)result1.GetValue("cflb2"),
                                    cfsy = (string)result1.GetValue("cfsy"),
                                    cfyj = (string)result1.GetValue("cfyj"),
                                    cfjg = (string)result1.GetValue("cfjg"),
                                    jdrq = (string)result1.GetValue("jdrq"),
                                    xzjg = (string)result1.GetValue("xzjg"),
                                    ztmc = (string)result1.GetValue("ztmc"),

                                };

                                XzcfInfoItems.Add(xzcf1);
                            }
                            if (XzcfInfoItems.Count == 0)
                            {
                                xzcfMsg.Visibility = Visibility.Visible;
                            }
                            JArray sxqy = (JArray)BLXX_data["sxqyInfo"];
                            sxqyListView.ItemsSource = SxqyInfoItems;
                            foreach (JObject result1 in sxqy)
                            {
                                
                                XuanChengLH_sxqyNo += 1;
                                
                                SxqyInfoItem sxqy1 = new SxqyInfoItem
                                {
                                    ListNo = XuanChengLH_sxqyNo,
                                    sxnr = (string)result1.GetValue("sxnr"),
                                    sxss = (string)result1.GetValue("sxss"),
                                    fbbm = (string)result1.GetValue("fbbm"),
                                    fbrq = (string)result1.GetValue("fbrq"),
                                    qymc = (string)result1.GetValue("qymc"),

                                };

                                SxqyInfoItems.Add(sxqy1);
                            }
                            if (SxqyInfoItems.Count == 0)
                            {
                                sxqyMsg.Visibility = Visibility.Visible;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string Msg = (string)@object.GetValue("msg");
                    Content = new HomePage(Msg);
                    Pages();
                }

            }
            catch
            {
                Content = new HomePage("接口解析异常，请联系开发人员");
                Pages();
            }
        }
        int ChangeNo = 1;
        private void FontWeight_Set()
        {
            Time.Countdown = 59;

            basicLable.FontWeight = FontWeightNormalLabel.FontWeight;
            XKLable.FontWeight = FontWeightNormalLabel.FontWeight;
            ZZLable.FontWeight = FontWeightNormalLabel.FontWeight;
            CBLable.FontWeight = FontWeightNormalLabel.FontWeight;
            LHLable.FontWeight = FontWeightNormalLabel.FontWeight;
            BLLable.FontWeight = FontWeightNormalLabel.FontWeight;
        }
        private void PageChange_Click(object sender, RoutedEventArgs e)
        {

            ChangeNo += 1;
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Basic":
                    FontWeight_Set();
                    basicLable.FontWeight = FontWeightBoldLabel.FontWeight;
                    Panel.SetZIndex(BasicInfoShow, ChangeNo);
                    break;
                case "XK":
                    FontWeight_Set();
                    XKLable.FontWeight = FontWeightBoldLabel.FontWeight;
                    Panel.SetZIndex(XKInfoShow, ChangeNo);
                    break;
                case "ZZ":
                    FontWeight_Set();
                    ZZLable.FontWeight = FontWeightBoldLabel.FontWeight;
                    Panel.SetZIndex(ZZInfoShow, ChangeNo);
                    break;
                case "CB":
                    FontWeight_Set();
                    CBLable.FontWeight = FontWeightBoldLabel.FontWeight;
                    Panel.SetZIndex(CBInfoShow, ChangeNo);
                    break;
                case "LH":
                    FontWeight_Set();
                    LHLable.FontWeight = FontWeightBoldLabel.FontWeight;
                    Panel.SetZIndex(LHInfoShow, ChangeNo);
                    break;
                case "BL":
                    FontWeight_Set();
                    BLLable.FontWeight = FontWeightBoldLabel.FontWeight;
                    Panel.SetZIndex(BLInfoShow, ChangeNo);
                    break;
                default:
                    break;

            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }


        private void Print_Click(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "正在下载报告请稍等";
            WaitShow.Visibility = Visibility.Visible;
            Thread therad = new Thread(() => XuanchengPDF())
            {
                IsBackground = true
            };
            therad.Start();

        }
        private void XuanchengPDF()
        {
            string FileName = "1.pdf";
            bool Sucess = PDF.DrawXuancheng(FileName, item1, XKXXItems, ZZXXItems, CBXXItems, NsxyjbInfoItems, JlInfoItems, RyInfoItems, SxbzxrInfoItems, XzcfInfoItems, SxqyInfoItems);

            Dispatcher.BeginInvoke(new Action(() => PageChange(Sucess, FileName)));
        }

        private void PageChange(bool Sucess,string FileName)
        {
            if (Sucess)
            {
                Content = new Pdfshow(FileName);
                Pages();
            }
            else
            {
                Content = new HomePage("PDF生成失败");
                Pages();
            }


        }


        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
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
        private void FontWeight_Set1()
        {
            Time.Countdown = 59;

            nsxyLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            jlLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            ryLabel.FontWeight = FontWeightNormalLabel.FontWeight;

        }
        private void LH_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "nsxyjbInfo":
                    FontWeight_Set1();
                    nsxyLabel.FontWeight = FontWeightBoldLabel.FontWeight;
                    nsxyGrid.Visibility = Visibility.Visible;
                    jlGrid.Visibility = Visibility.Hidden;
                    ryGrid.Visibility = Visibility.Hidden;
                    break;
                case "jlInfo":
                    FontWeight_Set1();
                    jlLabel.FontWeight = FontWeightBoldLabel.FontWeight;
                    nsxyGrid.Visibility = Visibility.Hidden;
                    jlGrid.Visibility = Visibility.Visible;
                    ryGrid.Visibility = Visibility.Hidden;
                    break;
                case "ryInfo":
                    FontWeight_Set1();
                    ryLabel.FontWeight = FontWeightBoldLabel.FontWeight;
                    nsxyGrid.Visibility = Visibility.Hidden;
                    jlGrid.Visibility = Visibility.Hidden;
                    ryGrid.Visibility = Visibility.Visible;
                    break;
                default:
                    break;

            }


        }
        private void FontWeight_Set2()
        {
            Time.Countdown = 59;

            sxbzxrLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            xzcfLabel.FontWeight = FontWeightNormalLabel.FontWeight;
            sxqyLabel.FontWeight = FontWeightNormalLabel.FontWeight;

        }
        private void BL_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "sxbzxrInfo":
                    FontWeight_Set2();
                    sxbzxrLabel.FontWeight = FontWeightBoldLabel.FontWeight;
                    sxbzxrGrid.Visibility = Visibility.Visible;
                    xzcfGrid.Visibility = Visibility.Hidden;
                    sxqyGrid.Visibility = Visibility.Hidden;
                    break;
                case "xzcInfo":
                    FontWeight_Set2();
                    xzcfLabel.FontWeight = FontWeightBoldLabel.FontWeight;
                    sxbzxrGrid.Visibility = Visibility.Hidden;
                    xzcfGrid.Visibility = Visibility.Visible;
                    sxqyGrid.Visibility = Visibility.Hidden;
                    break;
                case "sxqyInfo":
                    FontWeight_Set2();
                    sxqyLabel.FontWeight = FontWeightBoldLabel.FontWeight;
                    sxbzxrGrid.Visibility = Visibility.Hidden;
                    xzcfGrid.Visibility = Visibility.Hidden;
                    sxqyGrid.Visibility = Visibility.Visible;
                    break;
                default:
                    break;

            }
        }
    }
    #region //企业
    public class Basic
    {

       
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 社会统一信用代码
        /// </summary>
        public string uniscid { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string baseDwmc { get; set; }
        /// <summary>
        /// 法定代表人
        /// </summary>
        public string baseFddbr { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        public string baseZch { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string baseJgdm { get; set; }
        /// <summary>
        /// 注册资金
        /// </summary>
        public string baseZczj { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string baseHy { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string baseSzqh { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        public string baseClrq { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string baseXxdz { get; set; }
        /// <summary>
        /// 企业种类
        /// </summary>
        public string baseQyzl { get; set; }
        /// <summary>
        /// 主营产品
        /// </summary>
        public string baseZycp { get; set; }
        /// <summary>
        /// 单位简介
        /// </summary>
        public string baseDwjj { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 资金金额
        /// </summary>
        public string zjze { get; set; }
        /// <summary>
        /// 加载时间
        /// </summary>
        public int baseJzsj { get; set; }
        /// <summary>
        /// 信用代码
        /// </summary>
        public string xydm { get; set; }
        /// <summary>
        /// 税务登记号
        /// </summary>
        public string baseSwdjh { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        public string baseZcrq { get; set; }
        /// <summary>
        /// 纳税人状态
        /// </summary>
        public string baseNsrzt { get; set; }
        /// <summary>
        /// 国土属性
        /// </summary>
        public string baseGdsx { get; set; }
        /// <summary>
        /// 组织机构性质
        /// </summary>
        public string baseZzjgxz { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string baseJyfw { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string gxrq { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string baseLxdh { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string baseYzbm { get; set; }
    }
    public class XKXX : BaseUtil.ListItem
    {
        
        /// <summary>
        /// 物理主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 记录id
        /// </summary>
        public string recordId { get; set; }
        /// <summary>
        /// 区划id
        /// </summary>
        public string areaOid { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string bmbh { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string bmmc { get; set; }
        /// <summary>
        /// 信息分类
        /// </summary>
        public string xxfl { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string bh { get; set; }
        /// <summary>
        /// 决定书文号
        /// </summary>
        public string jdswh { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 审批类别
        /// </summary>
        public string splb { get; set; }
        /// <summary>
        /// 许可内容
        /// </summary>
        public string xknr { get; set; }
        /// <summary>
        /// 行政相对人名称
        /// </summary>
        public string xdrMc { get; set; }
        /// <summary>
        /// 行政相对人代码_1(统一社会信用代码)
        /// </summary>
        public string xdrShxym { get; set; }
        /// <summary>
        /// 行政相对人代码_2(组织机构代码)
        /// </summary>
        public string xdrZdm { get; set; }
        /// <summary>
        /// 行政相对人代码_3(工商登记码)
        /// </summary>
        public string xdrGsdj { get; set; }
        /// <summary>
        /// 行政相对人代码_4(税务登记号)
        /// </summary>
        public string xdrSwdj { get; set; }
        /// <summary>
        /// 行政相对人代码_5(身份证号)
        /// </summary>
        public string xdrSfz { get; set; }
        /// <summary>
        /// 法定代表人名称
        /// </summary>
        public string xdrFr { get; set; }
        /// <summary>
        /// 法定代表人类型
        /// </summary>
        public string xdrLx { get; set; }
        /// <summary>
        /// 决定日期
        /// </summary>
        public string jdrq { get; set; }
        /// <summary>
        /// 截至日期
        /// </summary>
        public string jzrq { get; set; }
        /// <summary>
        /// 许可机关
        /// </summary>
        public string xzjg { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 当前状态名称
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// 地方编码
        /// </summary>
        public string dfbm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 关联id
        /// </summary>
        public string glid { get; set; }
        /// <summary>
        /// 时间戳（创建时间）
        /// </summary>
        public string sjc { get; set; }
        /// <summary>
        /// 公示日期
        /// </summary>
        public string gsrq { get; set; }
        /// <summary>
        /// 文件上传时间
        /// </summary>
        public string wjscsj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string infoItem { get; set; }
    }
    public class ZZXX : BaseUtil.ListItem
    {
       
        /// <summary>
        /// 机构id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        public string zch { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string jgmc { get; set; }
        /// <summary>
        /// 信用代码
        /// </summary>
        public string shxydm { get; set; }
        /// <summary>
        /// 证书名称
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// 证书编号
        /// </summary>
        public string zsbh { get; set; }
        /// <summary>
        /// 资质类别
        /// </summary>
        public string zzlb { get; set; }
        /// <summary>
        /// 资质等级
        /// </summary>
        public string zzdj { get; set; }
        /// <summary>
        /// 资质内容
        /// </summary>
        public string zznr { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string fbrq { get; set; }
        /// <summary>
        /// 获取日期
        /// </summary>
        public string hqrq { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public int jzsj { get; set; }
        /// <summary>
        /// 证书状态
        /// </summary>
        public string zszt { get; set; }
    }
    public class CBXX : BaseUtil.ListItem
    {
        
        /// <summary>
        /// 企业关联id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string dwmc { get; set; }
        /// <summary>
        /// 社会信用码
        /// </summary>
        public string shxym { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        public string zch { get; set; }
        /// <summary>
        /// 加载时间
        /// </summary>
        public string jzsj { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 参保日期
        /// </summary>
        public string cbrq { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>
        public string cbzt { get; set; }
        /// <summary>
        /// 参保状态名称
        /// </summary>
        public string cbzt_mc { get; set; }
        /// <summary>
        /// 比例类别
        /// </summary>
        public string bllb { get; set; }
        /// <summary>
        /// 首次参保日期
        /// </summary>
        public string sccbrq { get; set; }
        /// <summary>
        /// 险种类型-名称
        /// </summary>
        public string xzlx_mc { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string xzlx { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public string hqsj { get; set; }
    }
    public class NsxyjbInfoItem : BaseUtil.ListItem
    {
        
        /// <summary>
        /// 企业关联id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string qymc { get; set; }
        /// <summary>
        /// 守信内容
        /// </summary>
        public string sxnr { get; set; }
        /// <summary>
        /// 评定等级
        /// </summary>
        public string pddj { get; set; }
        /// <summary>
        /// 发布部门
        /// </summary>
        public string fbbm { get; set; }
        /// <summary>
        /// 评定内容
        /// </summary>
        public string pdnr { get; set; }
        /// <summary>
        /// 评定年度
        /// </summary>
        public string pdnd { get; set; }
        /// <summary>
        /// 评定日期
        /// </summary>
        public string pdrq { get; set; }
        /// <summary>
        /// 创建时间（获取时间）
        /// </summary>
        public string hqsj { get; set; }
    }
    public class JlInfoItem : BaseUtil.ListItem
    {
       
        /// <summary>
        /// 企业id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string qymc { get; set; }
        /// <summary>
        /// 社会信用代码
        /// </summary>
        public string shxym { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        public string zch { get; set; }
        /// <summary>
        /// 税务登记号
        /// </summary>
        public string swdjh { get; set; }
        /// <summary>
        /// 奖励名称
        /// </summary>
        public string jlmc { get; set; }
        /// <summary>
        /// 奖励证书编号
        /// </summary>
        public string jlzsbh { get; set; }
        /// <summary>
        /// 奖励时间
        /// </summary>
        public string jlsj { get; set; }
        /// <summary>
        /// 奖励类别
        /// </summary>
        public string jllb { get; set; }
        /// <summary>
        /// 奖励类别名称
        /// </summary>
        public string jllb_mc { get; set; }
        /// <summary>
        /// 奖励等级
        /// </summary>
        public string jldj { get; set; }
        /// <summary>
        /// 奖励金额
        /// </summary>
        public string jlje { get; set; }
        /// <summary>
        /// 奖励原因
        /// </summary>
        public string jlyy { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public string hqsj { get; set; }
    }
    public class RyInfoItem : BaseUtil.ListItem
    {
       
        /// <summary>
        /// 企业关联id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string jgmc { get; set; }
        /// <summary>
        /// 决定书文号
        /// </summary>
        public string jdswh { get; set; }
        /// <summary>
        /// 社会信用代码
        /// </summary>
        public string shxym { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        public string zch { get; set; }
        /// <summary>
        /// 荣誉称号
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// 荣誉内容
        /// </summary>
        public string rynr { get; set; }
        /// <summary>
        /// 表彰单位
        /// </summary>
        public string bzdw { get; set; }
        /// <summary>
        /// 取得时间
        /// </summary>
        public string qdsj { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public string hqsj { get; set; }
    }
    public class SxbzxrInfoItem : BaseUtil.ListItem
    {
        
        /// <summary>
        /// 主键          
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 案件编号
        /// </summary>
        public string ajbh { get; set; }
        /// <summary>
        /// 履行情况
        /// </summary>
        public string lxqk { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string fbrq { get; set; }
        /// <summary>
        /// 执行法院
        /// </summary>
        public string zxfy { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public string hqrq { get; set; }
    }
    public class XzcfInfoItem : BaseUtil.ListItem
    {
       
        /// <summary>
        /// 物理主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 记录id
        /// </summary>
        public string recordId { get; set; }
        /// <summary>
        /// 区划id
        /// </summary>
        public string areaOid { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string bmbh { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string bmmc { get; set; }
        /// <summary>
        /// 信息分类
        /// </summary>
        public string xxfl { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string bh { get; set; }
        /// <summary>
        /// 决定书文号
        /// </summary>
        public string jdswh { get; set; }
        /// <summary>
        /// 处罚名称
        /// </summary>
        public string cfmc { get; set; }
        /// <summary>
        /// 处罚类别1
        /// </summary>
        public string cflb1 { get; set; }
        /// <summary>
        /// 处罚类别2
        /// </summary>
        public string cflb2 { get; set; }
        /// <summary>
        /// 处罚事由
        /// </summary>
        public string cfsy { get; set; }
        /// <summary>
        /// 处罚依据
        /// </summary>
        public string cfyj { get; set; }
        /// <summary>
        /// 行政相对人名称
        /// </summary>
        public string xdrMc { get; set; }
        /// <summary>
        /// 行政相对人代码_1(统一社会信用代码)
        /// </summary>
        public string xdrShxym { get; set; }
        /// <summary>
        /// 行政相对人代码_2(组织机构代码)
        /// </summary>
        public string xdrZdm { get; set; }
        /// <summary>
        /// 行政相对人代码_3(工商登记码)
        /// </summary>
        public string xdrGsdj { get; set; }
        /// <summary>
        /// 行政相对人代码_4(税务登记号)
        /// </summary>
        public string xdrSwdj { get; set; }
        /// <summary>
        /// 行政相对人代码_5(身份证号)
        /// </summary>
        public string xdrSfz { get; set; }
        /// <summary>
        /// 法定代表人名称
        /// </summary>
        public string xdrFr { get; set; }
        /// <summary>
        /// 法定代表人类型 
        /// </summary>
        public string xdrLx { get; set; }
        /// <summary>
        /// 处罚结果
        /// </summary>
        public string cfjg { get; set; }
        /// <summary>
        /// 决定日期
        /// </summary>
        public string jdrq { get; set; }
        /// <summary>
        /// 处罚机关
        /// </summary>
        public string xzjg { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 当前状态名称
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// 地方编码
        /// </summary>
        public string dfbm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 关联id
        /// </summary>
        public string glid { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string sjc { get; set; }
        /// <summary>
        /// 公示期限
        /// </summary>
        public string gsqx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string infoItem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gsrq { get; set; }
        /// <summary>
        /// 文件上传时间
        /// </summary>
        public string wjscsj { get; set; }
    }

    public class SxqyInfoItem : BaseUtil.ListItem
    {
       
        /// <summary>
        /// 企业关联id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string qymc { get; set; }
        /// <summary>
        /// 失信内容
        /// </summary>
        public string sxnr { get; set; }
        /// <summary>
        /// 失信事实
        /// </summary>
        public string sxss { get; set; }
        /// <summary>
        /// 发布部门
        /// </summary>
        public string fbbm { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string fbrq { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public string hqsj { get; set; }
    }
    #endregion
}
