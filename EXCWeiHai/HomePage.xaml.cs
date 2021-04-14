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
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            if (Global.WHDatas.HomeError!=null)
            {
                PopAlert(Global.WHDatas.HomeError, 3);
                Global.WHDatas.HomeError = null;
            }

            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;

            Global.PageType = null;

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

            Global.PageType = button.Tag.ToString();
            switch (Global.PageType)
            {
                case "ReportWeiHai"://威海
                case "ReportGRWeiHai":
                    Content = new Disclaimer();
                    Pages();
                    break;
            }
        }

        //样例初始化
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.PageType = button.Tag.ToString();

            switch ((string)button.Tag)
            {
                case "ReportWeiHai":
                case "ReportGRWeiHai":
                    Content = new Disclaimer();
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
