using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BaseDLL;
using System.Net;
using BaseUtil;

using System.Windows.Threading;
using TimeCount = BaseUtil.TimeCount;
using System.Collections.Generic;

namespace EXCXuanCheng
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    partial class Report : Page
    {
        /// <summary>
        /// 身份信息
        /// </summary>
        public RowsItem rowsItem;
        /// <summary>
        /// 房产信息
        /// </summary>
        public List<FcInfoItem> fc;
        /// <summary>
        /// 林权信息
        /// </summary>
        public List<LqdjInfoItem> lq;
        /// <summary>
        /// 车辆信息
        /// </summary>
        public List<ClInfoItem> cl;
        /// <summary>
        /// 参保信息
        /// </summary>
        public CbinfoItem cbinfoItem;
        /// <summary>
        /// 荣誉信息
        /// </summary>
        public List<GRRyInfoItem> ry;
        /// <summary>
        /// 共青团志愿者
        /// </summary>
        public List<GqdzyzInfoItem> gqt;
        /// <summary>
        /// 纳税信用A级
        /// </summary>
        public List<NsxyajInfoItem> nsa  ;
        /// <summary>
        /// 重大税收违法案件当事人
        /// </summary>
        public List<ZdsswfajdsrInfoItem> Zd;
        /// <summary>
        /// 失信被执行人信息
        /// </summary>
        public List<sxbzxrInfo> sx;
        /// <summary>
        /// 公积金贷款逾期信息
        /// </summary>
        public List<GjjdkyqInfoItem> gjj  ;
        /// <summary>
        /// 行政处罚信息
        /// </summary>
        public List<GRXzcfInfoItem> xzcf  ;
        public Report()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            PopLabel.Content = "正在查询报告中";
            PopBorder.Visibility = Visibility.Visible;
            Thread worker = new Thread(() => RequestUrl());
            worker.IsBackground = true;
            worker.Start();
        }


        private DispatcherTimer pageTimer = null;
        TimeCount Time = new TimeCount();
        private void Countdown_timer()
        {
            this.DataContext = Time;
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

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <returns>身份接口是否正常</returns>
        private string[] start()
        {
            /// 身份信息
            rowsItem = new RowsItem();
            /// 房产信息
            fc = new List<FcInfoItem>();
            /// 林权信息
            lq = new List<LqdjInfoItem>();
            /// 车辆信息
            cl = new List<ClInfoItem>();
            /// 参保信息
            cbinfoItem = new CbinfoItem();
            /// 荣誉信息
            ry = new List<GRRyInfoItem>();
            /// 共青团志愿者
            gqt = new List<GqdzyzInfoItem>();
            /// 纳税信用A级
            nsa = new List<NsxyajInfoItem>();
            /// 重大税收违法案件当事人
            Zd = new List<ZdsswfajdsrInfoItem>();
            /// 失信被执行人信息
            sx = new List<sxbzxrInfo>();
            /// 公积金贷款逾期信息
            gjj = new List<GjjdkyqInfoItem>();
            /// 行政处罚信息
            xzcf = new List<GRXzcfInfoItem>();
            #region//身份信息
            string[] zt = new string[] { "false", "false" };
            string grsfxx = Http.XuanCheng.GRSFXX("身份证号");
            JObject sfdata = (JObject)JsonConvert.DeserializeObject(grsfxx);
            if (sfdata == null)
            {
                return zt;
            }
            else
            {
                zt[0] = "true";
            }
            JArray sfjObjecta = (JArray)sfdata["rows"];
            if (sfjObjecta.Count == 0)
            {
                return zt;
            }
            else
            {
                zt[1] = "true";
            }

            JObject sfjObject = (JObject)sfdata["rows"][0];
            rowsItem = new RowsItem();

            foreach (var item in sfjObject)
            {
                rowsItem[item.Key] = item.Value.ToString();
            }
            #endregion

            #region//资产信息
            string grzcxx = Http.XuanCheng.GRZCXX("身份证号");
            JObject datazc = (JObject)JsonConvert.DeserializeObject(grzcxx);
            JObject jObjectzc = (JObject)datazc["data"];
            if (jObjectzc != null)
            {
                //房产
                JArray jObjectfc = (JArray)jObjectzc["fcInfo"];
                for (int i = 0; i < jObjectfc.Count; i++)
                {
                    FcInfoItem fcInfoItem = new FcInfoItem();
                    JObject je = (JObject)jObjectfc[i];
                    foreach (var item in je)
                    {
                        fcInfoItem[item.Key] = item.Value.ToString();
                    }
                    fc.Add(fcInfoItem);
                }
                //林权
                JArray jObjectlq = (JArray)jObjectzc["lqdjInfo"];
                for (int i = 0; i < jObjectlq.Count; i++)
                {
                    LqdjInfoItem lqdjInfoItem = new LqdjInfoItem();
                    JObject je = (JObject)jObjectlq[i];
                    foreach (var item in je)
                    {
                        lqdjInfoItem[item.Key] = item.Value.ToString();
                    }
                    lq.Add(lqdjInfoItem);
                }
                //车辆
                JArray jObjectcl = (JArray)jObjectzc["clInfo"];
                for (int i = 0; i < jObjectcl.Count; i++)
                {
                    ClInfoItem clInfoItem = new ClInfoItem();
                    JObject je = (JObject)jObjectcl[i];
                    foreach (var item in je)
                    {
                        clInfoItem[item.Key] = item.Value.ToString();
                    }
                    cl.Add(clInfoItem);
                }
            }
            #endregion
            #region//参保信息
            string grcvxx = Http.XuanCheng.GRCBXX("身份证号");
            JObject data = (JObject)JsonConvert.DeserializeObject(grcvxx);
            if (data != null)
            {
                try
                {
                    JArray jObjectcbc = (JArray)data["rows"];
                    if (jObjectcbc.Count != 0)
                    {
                        JObject jObjectcb = (JObject)data["rows"][0];
                        foreach (var item in jObjectcb)
                        {
                            cbinfoItem[item.Key] = item.Value.ToString();
                        }
                    }
                }
                catch
                {

                }
            }
            #endregion

            #region //良好信息
            string grlhxx = Http.XuanCheng.GRLHXX("身份证号");
            JObject lh = (JObject)JsonConvert.DeserializeObject(grlhxx);
            if (lh != null)
            {
                JArray jObjectlhd = (JArray)lh["rows"];
                if (jObjectlhd.Count != 0)
                {
                    JObject jObjectlh = (JObject)lh["rows"][0];
                    //荣誉
                    JArray jObjectry = (JArray)jObjectlh["ryInfo"];
                    for (int i = 0; i < jObjectry.Count; i++)
                    {
                        GRRyInfoItem gRRyInfoItem = new GRRyInfoItem();
                        JObject jObject = (JObject)jObjectry[i];
                        foreach (var item in jObject)
                        {
                            gRRyInfoItem[item.Key] = item.Value.ToString();
                        }
                        ry.Add(gRRyInfoItem);
                    }
                    //共青团志愿者
                    JArray jObjectgqt = (JArray)jObjectlh["gqdzyzInfo"];
                    for (int i = 0; i < jObjectgqt.Count; i++)
                    {
                        GqdzyzInfoItem gqdzyzInfoItem = new GqdzyzInfoItem();
                        JObject jObject = (JObject)jObjectgqt[i];
                        foreach (var item in jObject)
                        {
                            gqdzyzInfoItem[item.Key] = item.Value.ToString();
                        }
                        gqt.Add(gqdzyzInfoItem);
                    }
                    //纳税信用a级
                    JArray jObjectnsa = (JArray)jObjectlh["nsxyajInfo"];
                    for (int i = 0; i < jObjectnsa.Count; i++)
                    {
                        NsxyajInfoItem nsxyajInfoItem = new NsxyajInfoItem();
                        JObject jObject = (JObject)jObjectnsa[i];
                        foreach (var item in jObject)
                        {
                            nsxyajInfoItem[item.Key] = item.Value.ToString();
                        }
                        nsa.Add(nsxyajInfoItem);
                    }
                }

            }
            #endregion

            #region//不良信息
            string grblxx = Http.XuanCheng.GRBLXX("身份证号");
            JObject bl = (JObject)JsonConvert.DeserializeObject(grblxx);
            if (bl != null)
            {
                JArray jObjectbld = (JArray)bl["rows"];
                if (jObjectbld.Count != 0)
                {
                    JObject jObjectbl = (JObject)bl["rows"][0];
                    //重大税收违法案件当事人
                    JArray jObjectZdsswfajdsr = (JArray)jObjectbl["zdsswfajdsrInfo"];
                    for (int i = 0; i < jObjectZdsswfajdsr.Count; i++)
                    {
                        ZdsswfajdsrInfoItem zdsswfajdsrInfoItem = new ZdsswfajdsrInfoItem();
                        JObject jObject = (JObject)jObjectZdsswfajdsr[i];
                        foreach (var item in jObject)
                        {
                            zdsswfajdsrInfoItem[item.Key] = item.Value.ToString();
                        }
                        Zd.Add(zdsswfajdsrInfoItem);
                    }
                    //失信被执行人
                    JArray jObjectsxbzxr = (JArray)jObjectbl["sxbzxrInfo"];

                    for (int i = 0; i < jObjectsxbzxr.Count; i++)
                    {
                        sxbzxrInfo sxbzxrInfo = new sxbzxrInfo();
                        JObject jObject = (JObject)jObjectsxbzxr[i];
                        foreach (var item in jObject)
                        {
                            sxbzxrInfo[item.Key] = item.Value.ToString();
                        }
                        sx.Add(sxbzxrInfo);
                    }
                    //公积金贷款
                    JArray jObjectgGjjdkyq = (JArray)jObjectbl["gjjdkyqInfo"];
                    for (int i = 0; i < jObjectgGjjdkyq.Count; i++)
                    {
                        GjjdkyqInfoItem gjjdkyqInfoItem = new GjjdkyqInfoItem();
                        JObject jObject = (JObject)jObjectgGjjdkyq[i];
                        foreach (var item in jObject)
                        {
                            gjjdkyqInfoItem[item.Key] = item.Value.ToString();
                        }
                        gjj.Add(gjjdkyqInfoItem);
                    }
                    //行政处罚
                    JArray jObjectnXzcf = (JArray)jObjectbl["xzcfInfo"];
                    for (int i = 0; i < jObjectnXzcf.Count; i++)
                    {
                        GRXzcfInfoItem xzcfInfoItem = new GRXzcfInfoItem();

                        JObject jObject = (JObject)jObjectnXzcf[i];
                        foreach (var item in jObject)
                        {
                            xzcfInfoItem[item.Key] = item.Value.ToString();
                        }
                        xzcf.Add(xzcfInfoItem);
                    }
                }

            }
            #endregion
            return zt;
        }
        private void RequestUrl()
        {
            string[] s = null;
            try
            {
                s = start();
            }
            catch
            {

                Content = new HomePage("接口异常");
                Pages();
            }

            if (s[0] == "false")
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Content = new HomePage("身份证接口调用异常");
                    Pages();
                }));
            }
            else if (s[1] == "false")
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Content = new HomePage("数据库查询不到信息");
                    Pages();
                }));
            }

            string FileName = Guid.NewGuid().ToString() + ".pdf";
            PDF.DrawXuancheng_GR(FileName, rowsItem,fc,lq,cl,cbinfoItem,ry,gqt,nsa,Zd,sx,gjj,xzcf);
            //end方法
            end();

            Dispatcher.BeginInvoke(new Action(() =>
            {
                Content = new Pdfshow(FileName);
                Pages();
            }));
        }



          public void end()
        {
            rowsItem = new RowsItem();
            fc.Clear();
            lq.Clear();
            cl.Clear();
            cbinfoItem = new CbinfoItem();
            ry.Clear();
            gqt.Clear();
            nsa.Clear();
            Zd.Clear();
            sx.Clear();
            gjj.Clear();
            xzcf.Clear();
        }



        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

    }



}
