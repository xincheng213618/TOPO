using BaseInk;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            PopAlert(Msg,6);
        }
        private async void PopAlert(string Msg,int time)
        {
            Log.Write("HomePage:"+Msg);
            PopTips.Text = Msg;
            Pop.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(time));
            Pop.Visibility = Visibility.Hidden;
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            //App.InkWindows.Hide();
            //InkPut.delegates();z
            Global.Related.Initialized();          
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            Log.Write(Global.Related.PageType);
            switch (Global.Related.PageType)
            {
                case "XinHuaQiYeXinXi":
                case "CreditChinaQiYeXinXi":
                case "NaShuiXinYongA":
                case "ShuiShouWeiFa":
                case "ShiXinRen":
                    Content = new SearchPage();
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
