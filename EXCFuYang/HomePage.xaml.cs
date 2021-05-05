using BaseUtil;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace XinHua
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
            PopAlert(Msg);
        }
        private async void PopAlert(string Msg)
        {
            Log.Write("HomePage:" + Msg);
            PopTips.Text = Msg;
            Pop.Visibility = Visibility.Visible;
            await Task.Delay(4000);
            Pop.Visibility = Visibility.Hidden;
        }
        private void Page_Initialized(object sender, EventArgs e)
        {

            Global.Related.Initialized();
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;


            //BackgroundItem.Kind = false;
            //BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");

            //BackgroundItem.Picture.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            //BackgroundItem.Picture.Auto = true;
            //BackgroundItem.Picture.Intervaltime = 5000;// 千分秒
            //App.backgroundWindow.Updated();

            CompanyInfo.Initialized();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            Log.Write(Global.Related.PageType);
            switch (Global.Related.PageType)
            {
                case "QiYeXinXi":              
                    Content = new SearchPage();
                    break;              
                case "NanJingReport":
                    Content = new Report();
                    break;
                case "NanJingGRReport":
                    Content = new Report();
                    break;
                default:
                    break;
            }
            Pages();
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            Pop.Visibility = Visibility.Hidden;
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
       
    }
}
