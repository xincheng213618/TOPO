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
using BaseDLL;
using BaseUtil;

namespace EXC
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    public partial class Report : Page
    {
        private IDCardData idcardData;


        public Report()
        {
            InitializeComponent();
        }

        //public static 
        private void Page_Initialized(object sender, EventArgs e)
        {
            this.idcardData = Global.Related.IDCardData;
            //缓存数据


            WaitShow.Visibility = Visibility.Visible;
            switch (Global.Related.PageType)
            {
                default:
                    InfoLabel.Content = "正在下载报告";
                    break;
            }
            Thread worker = new Thread(() => RequestUrl());
            worker.IsBackground = true;
            worker.Start();
        }
        string AuthType;
        private void RequestUrl()
        {
            string response;
            switch (Global.Related.PageType)
            {
                case "ReportHeFei":
                    AuthType = "1";
                    response = Http.HeFei.GetReportList(Global.Related.IDCardData.IDCardNo, Global.Related.IDCardData.Name, "0");
                    Dispatcher.BeginInvoke(new Action(() => ReportHeFei(response)));
                    break;
                case "ReportHeFei1":
                    AuthType = "2";
                    response = Http.HeFei.GetReportList(Global.Related.IDCardData.IDCardNo, Global.Related.IDCardData.Name, "1");
                    Dispatcher.BeginInvoke(new Action(() => ReportHeFei(response)));
                    break;
                case "ReportGRHeFei":
                    response = Http.HeFei.GetGRReport(idcardData.IDCardNo, idcardData.Name);
                    string filePath = "Temp\\" + idcardData.Name + ".pdf";
                    Dispatcher.BeginInvoke(new Action(() => GetReportGRHeiFei(response, filePath)));
                    break;
            }
        }




        #region 数据一段解析

        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();

        private void ReportHeFei(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            ReportGrid.Visibility = Visibility.Visible;//要把表格显示出来
            if (response != null)
            {
                try
                {
                    JObject resObjs = (JObject)JsonConvert.DeserializeObject(response);
                    ReportListView.ItemsSource = CompanyReportItem;
                    string code = (string)resObjs.GetValue("code");
                    if (code == "0")
                    {
                        JArray resultArray = (JArray)resObjs["list"];
                        int xh = 0;
                        foreach (JObject result in resultArray)
                        {
                            CompanyReportItem item = new CompanyReportItem();
                            item.ListNo = ++xh;
                            item.Applicant = idcardData.Name;
                            item.CompanyName = (string)result.GetValue("QYMC");
                            item.USCI = (string)result.GetValue("SFZH");
                            item.FileName = null;
                            CompanyReportItem.Add(item);
                            if (item.USCI == null)
                            {
                                CompanyReportItem.Remove(item);
                            }
                        }
                        if (CompanyReportItem.Count == 0)
                        {
                            Content = new HomePage("暂无数据");
                            Pages();
                        }
                    }
                    else if (code == "1")
                    {
                        JArray resultArray = (JArray)resObjs["list"];
                        int xh = 0;
                        foreach (JObject result in resultArray)
                        {
                            CompanyReportItem item = new CompanyReportItem();
                            item.ListNo = ++xh;
                            item.Applicant = idcardData.Name;
                            item.CompanyName = (string)result.GetValue("QY_MC");
                            item.USCI = (string)result.GetValue("QY_SHXYM");
                            item.ApplicationData = (string)result.GetValue("CREATE_TIME");
                            item.FileName = null;
                            CompanyReportItem.Add(item);
                        }

                        if (CompanyReportItem.Count == 0)
                        {
                            Content = new HomePage("暂无数据");
                            Pages();
                        }
                    }
                    else
                    {
                        Content = new HomePage(resObjs["msg"].ToString());
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("暂无数据");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("接口连接错误，请检查网络连接或者后台服务");
                Pages();
            }
        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        string CompanyName;
        string USCI;
        private void ReportListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hintLabel.Content = "正在申请报告请稍等";
            WaitShow.Visibility = Visibility.Visible;
            if (ReportListView.SelectedIndex > -1)
            {
                switch (Global.Related.PageType)
                {
                    case "ReportHeFei":
                    case "ReportHeFei1":
                        CompanyName = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).CompanyName.ToString();
                        USCI = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).USCI.ToString();
                        string filePath = "Temp\\" + USCI + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                        Thread worker11 = new Thread(() => GetReportHeiFei(USCI, filePath))
                        {
                            IsBackground = true
                        };
                        worker11.Start();
                        break;
                    default:
                        break;
                }
            }


        }


        public void GetReportHeiFei(string USCI, string FilePath)
        {
            string response = Http.HeFei.GetReport(USCI);
            Dispatcher.BeginInvoke(new Action(() => GetReportHeiFeiPhrase(response, FilePath)));
        }

        // 直接调用原生接口解析
        public void GetReportGRHeiFei(string response, string FilePath)
        {
            if (response != null)
            {
                try
                {
                    JObject resObjs = (JObject)JsonConvert.DeserializeObject(response);
                    bool flag = (bool)resObjs["flag"];
                    if (flag)
                    {
                        string url = (string)resObjs.GetValue("data");
                        bool Sucess = Http.HeFei.ReportDL(url, FilePath);
                        hintLabel.Content = "正在下载报告文件";
                        Log.Write(url);
                        
                        if (File.Exists(FilePath))
                        {
                            Content = new Pdfshow(FilePath);
                            Pages();
                        }
                        else
                        {
                            Content = new HomePage("报告下载失败：" + Environment.NewLine + url);
                            Pages();
                        }
                    }
                    else
                    {
                        Content = new HomePage((string)resObjs.GetValue("msg"));
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("暂无数据");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("接口连接错误，请检查网络连接或者后台服务");
                Pages();
            }
        }

        //个人接口解析
        //public void GetReportGRHeiFei(string response, string FilePath)
        //{
        //    if (response != null)
        //    {
        //        try
        //        {
        //            JObject resObjs = (JObject)JsonConvert.DeserializeObject(response);
        //            string code = (string)resObjs.GetValue("code");
        //            if (code == "0")
        //            {
        //                JObject data = (JObject)JsonConvert.DeserializeObject(response);
        //                bool flag = (bool)data["flag"];
        //                if (flag)
        //                {
        //                    string url = (string)data.GetValue("data");
        //                    bool Sucess = Http.HeFei.ReportDL(url, FilePath);
        //                    hintLabel.Content = "正在下载报告文件";
        //                    Log.Write(url);
        //                    if (File.Exists(FilePath))
        //                    {
        //                        Global.PageType = null;
        //                        Content = new Pdfshow(FilePath);
        //                        Pages();
        //                    }
        //                    else
        //                    {
        //                        Content = new HomePage("报告下载失败：" + Environment.NewLine + url);
        //                        Pages();
        //                    }
        //                }
        //                else
        //                {
        //                    Content = new HomePage((string)data.GetValue("msg"));
        //                    Pages();
        //                }
        //            }
        //            else
        //            {
        //                Content = new HomePage((string)resObjs.GetValue("msg"));
        //                Pages();
        //            }

        //        }
        //        catch
        //        {
        //            Content = new HomePage("接口解析错误");
        //            Pages();
        //        }
        //    }
        //    else
        //    {
        //        Content = new HomePage("接口连接错误，请检查网络连接或者后台服务");
        //        Pages();
        //    }
        //}

        public void GetReportHeiFeiPhrase(string response, string FilePath)
        {
            if (response != null)
            {
                try
                {
                    JObject resObjs = (JObject)JsonConvert.DeserializeObject(response);
                    string code = (string)resObjs.GetValue("code");
                    if (code == "0")
                    {
                        JObject data = (JObject)resObjs.GetValue("data");
                        bool flag = (bool)data["flag"];
                        if (flag)
                        {
                            string url = (string)data["data"];
                            bool Sucess = Http.HeFei.ReportDL(url, FilePath);
                            hintLabel.Content = "正在下载报告文件";
                            //记录打印记录
                            Http.HeFei.AddAction(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo, CompanyName,AuthType, USCI);
                            if (Sucess)
                            {
                                Content = new Pdfshow(FilePath);
                                Pages();
                            }
                            else
                            {
                                Content = new HomePage("报告下载失败：" + Environment.NewLine + url);
                                Pages();
                            }
                        }
                        else
                        {
                           
                            Content = new HomePage((string)data.GetValue("msg"));
                            Pages();
                        }
                    }
                    else
                    {
                        Content = new HomePage(resObjs["msg"].ToString());
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("暂无数据");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("接口连接错误,请检查网络连接");
                Pages();
            }

        }
        #endregion

        private void ReportListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
    }




}
