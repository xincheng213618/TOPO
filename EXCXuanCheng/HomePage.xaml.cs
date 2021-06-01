using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
            if (Run)
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
            else
            {
                PopAlert(checkStatus,3);
            }
           
         }
        private Timer timer;
        private bool Run = true;
        string checkStatus = null;
        private void Page_Initialized(object sender, EventArgs e)
        {
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;

            Global.Related.Initialized();
            if (Global.Config.PrintCheckOptimizat == "true")
            {
                timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => Printcheck())), null, 0, 10000);
            }
            
        }
        private void Printcheck()
        {
            Run = true;
            checkStatus = PrintStatus.PrintStatusCheck(Global.Config.PrinterIP);
            if(checkStatus != "idle")
            {
                Run = false;
            }
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
