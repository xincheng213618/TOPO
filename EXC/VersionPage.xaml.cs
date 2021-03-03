using Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using BaseDLL;

namespace EXC
{
    /// <summary>
    /// VersionPage.xaml 的交互逻辑
    /// </summary>
    public partial class VersionPage : Page
    {
        IDCardData iDCardData ;
        string CompanyID;
        public VersionPage()
        {
            switch (Global.PageType)
            {
                case "ReportWeiHai":
                    //iDCardData = IDCardInfo.ToiDCardData();
                    CompanyID = CompanyInfo.CompanyID;
                    break;
                case "ReportGRWeiHai":
                    //iDCardData = IDCardInfo.ToiDCardData();
                    break;
            }

            InitializeComponent();
        }

        public VersionPage( IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
        }
        public VersionPage(IDCardData iDCardData,string CompanyID)
        {
            this.iDCardData = iDCardData;
            this.CompanyID = CompanyID;
            CompanyInfo.CompanyID = CompanyID;
            InitializeComponent();
        }



        private void Page_Initialized(object sender, EventArgs e)
        {
            PopBorder.Visibility = Visibility.Visible;
            InfoLabel.Content = "获取模板中";

            //IDCardInfo.Set(iDCardData);
            Thread worker = new Thread(() => RequestsUrl())
            {
                IsBackground = true
            };
            worker.Start();
        }

        private ObservableCollection<VersionItem> VersionItem = new ObservableCollection<VersionItem>();
        private int VersionNo =0;
        private void RequestsUrl()
        {
            Webservice.WeiHai.GetPersonInfo(iDCardData);//只是上传不做处理

            JObject response = Webservice.WeiHai.GetReportTemplate();
            Dispatcher.BeginInvoke(new Action(() => Phrase(response)));


        }
        private void Phrase(JObject response)
        {
            if ((string)response.GetValue("code") == "0")
            {
                JArray jArray = (JArray)JsonConvert.DeserializeObject((string)response.GetValue("data"));
                foreach (JObject result in jArray)
                {
                    VersionItem item = new VersionItem();
                    item.ListNo = VersionNo++;
                    item.TemplateID = (string)result.GetValue("templateid");
                    item.TemplateName = (string)result.GetValue("templatename");
                    VersionItem.Add(item);
                }
                PopBorder.Visibility = Visibility.Hidden;
                Media.Player(null, 1);
                switch (Global.PageType)
                {
                    case "ReportWeiHai":
                        ContentBorder2.Visibility = Visibility.Visible;
                        InfoLabel.Content = "请选择企业信用报告模板";
                        break;
                    case "ReportGRWeiHai":
                        InfoLabel.Content = "请选择个人信用报告模板";
                        ContentBorder1.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Content = new HomePage((string)response.GetValue("msg"));
                Pages();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string TemplateID ="";
            Button button = sender as Button;
            TemplateID = VersionItem[int.Parse(button.Tag.ToString())].TemplateID;

            switch (Global.PageType)
            {
                case "ReportWeiHai":
                    Content = new Report(iDCardData, TemplateID,CompanyID);
                    Pages();
                    break;
                case "ReportGRWeiHai":
                    Content = new Report(iDCardData, TemplateID);
                    Pages();
                    break;
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
    }

}    
