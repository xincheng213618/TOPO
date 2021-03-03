using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Documents;
using System.IO;
using Background;
using BaseDLL;
using static Background.BackgroundWindow;
using BaseUtil;

namespace EXCYiXing
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        //Designed By Mr.Xin 2020.5.8 修订相关页面，并更新相关数据
        public HomePage()
        {
            InitializeComponent();
        }
        public HomePage(string Msg)
        {
            InitializeComponent();
            PopAlert(Msg, 3);//吉林 原本为3
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;
            BackgroundItem.Kind = true;
            BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            //BackgroundItem.Picture.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            //BackgroundItem.Picture.Auto = true;
            App.backgroundWindow.Updated();
            IDCardInfo.Initialized();
            CompanyInfo.Initialized();
            Global.PageType = null;
            Stamp.Close();
        }

        private async void PopAlert(string Msg, int time)
        {
            Log.Write("HomePagePOP:" + Msg);
            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(time));
            POP.Visibility = Visibility.Hidden;
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private IDCardData IDCardData;
        //Page 跳转 所有的逻辑从这里进行转跳
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Log.Write(Global.PageType);//PageType 收集是有用的
            Global.PageType = button.Tag.ToString();
            switch (Global.PageType)
            {
                case "YiXingPerson":
                case "YiXingBanch":
                    Content = new IDCardPage();
                    //Content = new Report(new IDCardData() { Name = "沈华兵", IDCardNo = "342423198910188616" });
                    //Content = new Report(new IDCardData() { Name = "钱丽丽", IDCardNo = "320223198002115220" });
                    //Content = new Report(new IDCardData() { Name = "吴国中", IDCardNo = "320223196402215416" });
                    break;
                case "YiXingNew":
                    Content = new YiXingNew();
                    break;
                default:
                    break;
            }
            Pages();
        }
        //样例初始化
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.PageType = button.Tag.ToString();
            switch ((string)button.Tag)
            {
                case "ReportNingYang":
                case "ReportGRNingYang":
                case "ReportNingYangAll":
                    Global.PageType = button.Tag.ToString();
                    IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "37092119790520542x" };
                    Content = new Report(IDCardData);
                    break;
                case "ReportNanJing":
                case "ReportGRNanJing":
                    Global.PageType = button.Tag.ToString();
                    IDCardData = new IDCardData { Name = "任正非", IDCardNo = "1231465789789" };
                    Content = new Report(IDCardData);
                    break;
                case "ReportXinTai":
                case "ReportGRXinTai":
                case "ReportGRWeiFang":
                    IDCardData = new IDCardData { Name = "曹丽梅", IDCardNo = "370724199105292614" };
                    Content = new Report(IDCardData);
                    break;
                case "ReportHeFei":
                case "ReportHeFei1":
                case "ReportGRHeFei":
                    IDCardData = new IDCardData { Name = "宋志磊", IDCardNo = "340122199210252875" };
                    Content = new Report(IDCardData);
                    break;
                case "YiXingPerson":
                case "YiXingBanch":
                    IDCardData = new IDCardData { Name = "毕明宇", IDCardNo = "371081198706050050" };
                    Content = new Report(IDCardData);
                    break;
                case "HeiFeiEnterprise":
                    break;

                default:
                    break;
            }
            Pages();
        }
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Content = new PrintTips();
            Pages();
        }


    }
}
