using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using BaseUtil;
using BaseDLL;

namespace EXC
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
 

        public HomePage(string msg)
        {
            PopAlert(msg, 6);//吉林 原本为3
            InitializeComponent();
        }

        public HomePage()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;

            Global.Related.Initialized();

            //BackgroundItem.Kind = true;
            //BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");

            //App.backgroundWindow.Updated();
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

        //Page 跳转 所有的逻辑从这里进行转跳
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.Ps.machinecode = Global.Config.TerminalName;
            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
                case "ReportHeFei":
                    Global.Related.Ps.opeartiontype = "企业报告打印";
                    Global.Related.Ps.authcode = "企业报告打印";
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "ReportHeFei1":
                    Global.Related.Ps.opeartiontype = "企业报告查询";
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "ReportGRHeFei":
                    Global.Related.Ps.opeartiontype = "个人报告打印";
                    if (Global.Config.FunctionOpenGR)
                    {
                        Content = new IDCardPage();
                        Pages();
                    }
                    else
                    {
                        PopAlert("该功能暂时无法使用", 3);
                    }

                    break;
                case "HeiFeiRed"://合肥
                case "HeiFeiBlack":
                case "HeiFeiCF":
                case "HeiFeiXK":
                case "HeiFeiEnterprise":
                    Content = new SearchHeiFei();
                    Pages();
                    break;
                case "HeiFeiImportantPerson":
                    Content = new PersonalCertificate();
                    Pages();
                    break;
            }
        }

        //样例初始化
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();

            switch (Global.Related.PageType)
            {
                case "ReportHeFei":
                    Content = new Pdfshow();
                    Pages();
                    break;
                case "ReportHeFei1":
                case "ReportGRHeFei":
                    Global.Related.IDCardData = new IDCardData { Name = "宋志磊", IDCardNo = "340122199210252875" };
                    Content = new Report();
                    break;
                default:
                    break;
            }
            Pages();
        }
    }
}
