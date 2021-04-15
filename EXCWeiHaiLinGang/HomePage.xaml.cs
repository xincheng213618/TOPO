using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using BaseUtil;
using BaseDLL;
using System.Text;
using System.Windows.Threading;
using Background;

namespace EXC
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        } public HomePage(string msg)
        {
                PopAlert(msg, 3);
            InitializeComponent();
        }

     

        private void Page_Initialized(object sender, EventArgs e)
        {
           
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;

            Global.Related.PageType = null;

            BackgroundItem.Kind = true;
            BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");

            //BackgroundItem.Picture.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            //BackgroundItem.Picture.Auto = true;
            App.backgroundWindow.Updated();
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
                case "ReportGRWeiHai":
                    //IDCardData iDCardData = new IDCardData()
                    //{
                    //    Name = "董嗣来",
                    //    IDCardNo = "370282198111250818",
                    //    PhotoFileName = @"王滨滨_37078119870610487X.bmp"
                    //};

                    //Content = new Report(iDCardData);
                    Content = new IDCardPage();
                    Pages();

                    break;
                case "ReportGRWeiHaiHBF":
                    // iDCardData = new IDCardData()
                    //{
                    //    Name = "董嗣来",
                    //    IDCardNo = "370282198111250818",
                    //    PhotoFileName = @"王滨滨_37078119870610487X.bmp"
                    //};

                    //Content = new Report(iDCardData);
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "ReportWeiHai":
                    Content = new SearchPage();
                    Pages();
                    break;
            }
        }

        //样例初始化
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();

            switch ((string)button.Tag)
            {
                case "ReportGRWeiHai":
                    IDCardData iDCardData = new IDCardData()
                    {
                        Name = "胡洪珂",
                        IDCardNo = "411327200103063136",
                    };
                    Content = new Report();
                    //Content = new IDCardPage();
                    Pages();

                    break;
                case "ReportWeiHai":
                    iDCardData = new IDCardData()
                    {
                        Name = "胡洪珂",
                        IDCardNo = "411327200103063136"
                    };
                    Content = new SearchPage();
                    //Content = new IDCardPage();
                    Pages();
                    break;

                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            zcjdGrid.Visibility = Visibility.Hidden;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string strs = File.ReadAllText(System.Environment.CurrentDirectory+ "/Base/政策解读.txt", Encoding.UTF8);
            DisclaimerContent.Text = strs;
            zcjdGrid.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(10));

            zcjdGrid.Visibility = Visibility.Hidden;
        }


    }
}
