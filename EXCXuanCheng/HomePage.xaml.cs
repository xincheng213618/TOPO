using Background;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EXCXuanCheng
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
            PopAlert(Msg, 6);
        }
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
                case "TopoSearch":
                    Content = new SearchPage();
                    Pages();
                    break;
                case "FaRen":
                    Content = new SearchPage();
                    Pages();
                    break;
                case "ZiRanRen":
                    Content = new Report();
                    Pages();
                    break;
                case "XuanChengQiYi":
                    Content = new SearchPage1();
                    Pages();
                    break;
                default:
                    break;
            }
         } 
        private void Page_Initialized(object sender, EventArgs e)
        {
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;

            Global.Related.Initialized();

            BackgroundItem.Kind = true;
            BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");

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
    }
}
