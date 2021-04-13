using Background;
using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Background.BackgroundWindow;

namespace RECSuzhou
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        public HomePage(string Msg)
        {
            InitializeComponent();
            PopAlert(Msg, 3);
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Global.Related.Initialized();
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;


            BackgroundItem.Kind = false;
            BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");

            BackgroundItem.Picture.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            BackgroundItem.Picture.Auto = true;
            BackgroundItem.Picture.Intervaltime = 5000;// 千分秒
            App.backgroundWindow.Updated();
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


        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
                case "NoHome":
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "OwnerShipPages":
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "HomeCountPages":
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "NoHomeChild":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZWZArchivePages":
                case "SZHQArchivePages":
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "SZMoneyPages":
                    Content = new SZMoneyPage();
                    Pages();
                    break;
                default:
                    break;
            }
        }

        private void TestButton_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
                case "NoHome":
                    Global.Related.IDCardData = new IDCardData { Name = "陈信成", IDCardNo = "320323199712213618" };
                    Content = new NoHomePages();
                    Pages();
                    break;
                case "OwnerShipPages":
                    Global.Related.IDCardData = new IDCardData { Name = "王留英", IDCardNo = "320502196312122544" };
                    Content = new OwnerShipPages();
                    Pages();
                    break;
                case "HomeCountPages":
                    Global.Related.IDCardData = new IDCardData { Name = "奚玉远", IDCardNo = "152322198703291816" };
                    Content = new HomeCountPages();
                    Pages();
                    break;
                case "NoHomeChild":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZHQArchivePages":
                    Global.Related.IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "140108198708253219" };
                    Content = new SZArchivePage();
                    Pages();
                    break;
                case "SZWZArchivePages":
                    Global.Related.IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "140108198708253219" };
                    Content = new SZArchivePage();
                    Pages();
                    break;
                case "SZMoneyPages":
                    Content = new SZMoneyPage();
                    Pages();
                    break;
                default:
                    break;
            }
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
    }
}
