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


namespace EXCYiXing
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

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.idcardData = Global.iDCard;

            WaitShow.Visibility = Visibility.Visible;
            switch (Global.PageType)
            {
                case "YiXingBanch":
                case "YiXingPerson":
                    InfoLabel.Content = "正在查询不动产证明信息";
                    break;
               
                default:
                    InfoLabel.Content = "正在下载报告";
                    break;
            }
            Thread worker = new Thread(() => RequestUrl());
            worker.IsBackground = true;
            worker.Start();
        }
        private void RequestUrl()
        {
            string response;
           
            switch (Global.PageType)
            {
          
                case "YiXingNew":
                case "YiXingPerson":
                    response = Http.YinXingNew.DJZL(idcardData.Name, idcardData.IDCardNo);
                
                    Dispatcher.BeginInvoke(new Action(() => ReportPersonYinXing(response)));
                  
                   
                    break;

            }
        }

        #region 数据一段解析

        private ObservableCollection<CompanyReportItem> CompanyReportItem = new ObservableCollection<CompanyReportItem>();
        /// <summary>
        /// 宜兴 表格数据更改
        /// </summary>
        /// <param name="response">请求的List数据</param>
        private void ReportPersonYinXing(string response)
        {
            InfoLabel.Content = "不动产信息";

            WaitShow.Visibility = Visibility.Hidden;
            InfoLabelTile2.Content = "产权证号";
            InfoLabelTile3.Content = "坐落";
            InfoLabelTile4.Content = "不动产类型";

            ReportGrid.Visibility = Visibility.Visible;//要把表格显示出来

            if (response == null)
            {
                Global.HomeError = "接口连接错误，返回值为null";
                Content = new HomePage();
                Pages();
                return;
            }
            try
            {
                JObject resObjs = (JObject)JsonConvert.DeserializeObject(response);
                ReportListView.ItemsSource = CompanyReportItem;
                string code = (string)resObjs.GetValue("code");
                if (code == "0")
                {
                    JArray resultArray = (JArray)resObjs["data"];
                    int xh = 0;
                    foreach (JObject result in resultArray)
                    {
                        CompanyReportItem item = new CompanyReportItem();
                        item.ListNo = ++xh;
                        item.Applicant = (string)result.GetValue("CQZH");//不动单元号
                                                                         //item.BRN = (string)result.GetValue("FWDM");
                        item.CompanyName = (string)result.GetValue("ZL");//坐落
                        item.ApplicationData = (string)result.GetValue("BDCLX");
                        item.BRN = (string)result.GetValue("FWDM");

                        CompanyReportItem.Add(item);
                    }
                    if (CompanyReportItem.Count == 0)
                    {
                Global.HomeError = "暂无数据";
                        Content = new HomePage();
                        Pages();
                    }
                }
                else
                {
                Global.HomeError = (string)resObjs.GetValue("msg");
                    Content = new HomePage();
                    Pages();
                }
            }
            catch
            {
                Global.HomeError = "暂无数据";
                Content = new HomePage( );
                Pages();
            }
           
        }
 
        #endregion
        private void HomeClcik(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        /// <summary>
        /// 列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hintLabel.Content = "正在下载报告请稍等";
            WaitShow.Visibility = Visibility.Visible;
            try
            {
           
                        string CQZH = CompanyReportItem.ElementAt(ReportListView.SelectedIndex).Applicant.ToString();//不动产权证书

                        WaitShow.Visibility = Visibility.Visible;

                        Thread therad = new Thread(() => RequestsYiXing(CQZH))
                        {
                            IsBackground = true
                        };
                        therad.Start();

                     
                 
                       
              
            }
            catch(Exception ex)
            {
                Log.WriteException(ex);
            }
         
        }

        private void RequestsYiXing(string CQZH)
        {
            string FileName = "1.pdf";
            Log.Write("Report 536 line");
            if (PDF.DrawYiXing1(FileName, idcardData, CQZH))
            {
                
                Dispatcher.BeginInvoke(new Action(() => PDFShow(FileName)));

            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() => { WaitShow.Visibility = Visibility.Hidden; }));
                
            }

        }

        private void PDFShow(string FileName)
        {
            WaitShow.Visibility = Visibility.Hidden;
            hintLabel.Content = " 正在绘制PDF";

            Content = new Pdfshow(FileName);
            Pages();
        }


      

        #region 数据解析2段
  

      


        #endregion

        private void ReportListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }



}
