using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BaseDLL;
using BaseUtil;
    
using System.Windows.Threading;
using System.Linq;

namespace EXC
{
    /// <summary>
    /// VersionPage.xaml 的交互逻辑
    /// </summary>
    public partial class VersionPage : Page
    {
        IDCardData iDCardData;
        
        public VersionPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {

   
            iDCardData = Global.Related.IDCardData;
            Countdown_timer();
            PopBorder.Visibility = Visibility.Visible;
            InfoLabel.Content = "获取模板中";

            Thread worker = new Thread(() => RequestsUrl())
            {
                IsBackground = true
            };
            worker.Start();
        }

        private ObservableCollection<VersionItem> VersionItem = new ObservableCollection<VersionItem>();
        private int VersionNo = 0;
        private void RequestsUrl()
        {
            WebService.GetPersonInfo(iDCardData);//只是上传不做处理

            JObject response = WebService.GetReportTemplate();
            Dispatcher.BeginInvoke(new Action(() => Phrase(response)));
        }

        private void Phrase(JObject response)
        {
            PopBorder.Visibility = Visibility.Hidden;
            Media.Player(1);
            switch (Global.Related.PageType)
            {
                case "ReportWeiHai":
                    InfoLabel.Content = "请选择企业信用报告模板";
                    break;
                case "ReportGRWeiHai":
                    InfoLabel.Content = "请选择个人信用报告模板";
                    ContentBorder1.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
            Global.Related.CameraPass = true;
            if (!Global.Related.CameraPass)
            {
                InfoLabel.Content = "人脸对比失败，仅提供简易模板";
            }
            string[] GRLShow = Global.Config.GRLShow.Split(new char[] { ',', '，' });
            string[] ELShow = Global.Config.ELShow.Split(new char[] { ',', '，' });

            string[] ENShow = Global.Config.ENShow.Split(new char[] { ',', '，' });
            string[] GRNShow = Global.Config.GRNShow.Split(new char[] { ',', '，' });


            if ((string)response.GetValue("code") == "0")
            { 
                JArray jArray = (JArray)JsonConvert.DeserializeObject((string)response.GetValue("data"));
                foreach (JObject result in jArray)
                {
                    VersionItem item = new VersionItem();
                    if ((string)result.GetValue("isenable") == "1")
                    {
                       
                        item.TemplateID = (string)result.GetValue("templateid");
                        item.TemplateName = (string)result.GetValue("templatename");
                        //失败时，仅提供设定的基础模板，成功时排除不需要的模板，不设定时自动时全部，分成个人和企业两个部分
                        if ((Global.Related.PageType == "ReportWeiHai"&&((Global.Related.CameraPass && !ENShow.Contains(item.TemplateName)) || (!Global.Related.CameraPass && ELShow.Contains(item.TemplateName))))
                            ||(Global.Related.PageType == "ReportGRWeiHai"&&((Global.Related.CameraPass && !GRNShow.Contains(item.TemplateName)) || (!Global.Related.CameraPass && GRLShow.Contains(item.TemplateName)))))
                        {
                            item.ListNo = VersionNo++;
                            VersionItem.Add(item);

                            Button button = new Button
                            {
                                Content = item.TemplateName,
                                Visibility = Visibility.Visible,
                                FontSize = 25,
                                Height = 300,
                                Width = 200,
                                BorderThickness = new Thickness(3)
                            };

                            button.Click += Button_Click;
                            button.Tag = item.ListNo;
                            button.Template = buttonmb.Template;
                            button.Margin = new Thickness(10, 0, 10, 0);
                            button.Visibility = Visibility.Visible;
                            cGrid.Children.Add(button);
                        }
                    }
                }
                if (cGrid.Children.Count<1)
                {
                    Content = new HomePage("暂无可显示的报告");
                    Pages();
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
            Button button = sender as Button;
            string TemplateID = VersionItem[int.Parse(button.Tag.ToString())].TemplateID; //这里正常应该做报错检测，但是由于数据时自动生成，因此不会出错
            Global.Related.TemplateID = TemplateID;
            switch (Global.Related.PageType)
            {
                case "ReportWeiHai":
                    Content = new Report( );
                    Pages();
                    break;
                case "ReportGRWeiHai":
                    Content = new Report( );
                    Pages();
                    break;
            }

        }
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
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();
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

    }

}
