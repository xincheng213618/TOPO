using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Documents;

using BaseDLL;

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
        }    public HomePage( string msg)
        {
                PopAlert(msg, 3);//吉林 原本为3
            InitializeComponent();
        }
     

        private void Page_Initialized(object sender, EventArgs e)
        {

            Global.Related.Initialized();
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;
            //BackgroundItem.Kind = true;
            //BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            ////BackgroundItem.Picture.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            ////BackgroundItem.Picture.Auto = true;
            //App.backgroundWindow.Updated();

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
        //Page 跳转 所有的逻辑从这里进行转跳
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
                case "YiXingPerson":
                case "YiXingBanch":
                    Content = new IDCardPage();
                    Pages();

                    //Content = new Report();
                    break;
                case "YiXingNew":
                    Content = new YiXingNew();
                    Pages();

                    break;
                default:
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
                case "YiXingPerson":
                case "YiXingBanch":
                    Global.Related.IDCardData = new IDCardData() { Name = "沈华兵", IDCardNo = "342423198910188616" };
                    Content = new Report( );
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
