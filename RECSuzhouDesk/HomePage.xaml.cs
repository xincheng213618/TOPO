using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;

            Global.Related.Initialized();
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


        private IDCardData IDCardData;
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
                case "NoHome":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "OwnerShipPages":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "HomeCountPages":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "NoHomeChild":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZWZArchivePages":
                case "SZHQArchivePages":
                    Content = new IDcardInputPage();
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
