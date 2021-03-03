using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using BaseDLL;
using BaseUtil;

namespace REC
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
            Global.PageType = null;

            MainWindow.WindowsData.Status1 = "翻页：" + ESerialPort.CheckDeviceCode[ESerialPort.CheckDevice1()];
            MainWindow.WindowsData.Status2 = "盖章：" + ESerialPort.CheckDeviceCode[ESerialPort.CheckDevice2()];

            Storyboard sb = new Storyboard();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.WindowsData.Status1 == "翻页：正常" && MainWindow.WindowsData.Status2 == "盖章：正常")
            {
                Global.PageType = "Self";
                Content = new FunctionPage3("1.pdf");
                Pages();
            }
            else
            {
                int i = ESerialPort.CheckDevice1();
                MainWindow.WindowsData.Status1 = "翻页：" + ESerialPort.CheckDeviceCode[i];

                int j = ESerialPort.CheckDevice2();
                MainWindow.WindowsData.Status2 = "盖章：" + ESerialPort.CheckDeviceCode[j];
                if (i != 0 || j != 0)
                {
                    if (MainWindow.WindowsData.Status1 == "翻页：缺证")
                    {
                        Content = new HomePage("请联系工作人员放置证书");
                        Pages();  
                    }
                    else
                    {
                        Content = new HomePage("机器故障，请联系工作人员");
                        Pages();
                    }
                }
                else
                {
                    Content = new FunctionPage3("1.pdf");
                    Pages();
                }
               
            }
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }

        private async void PopAlert(string Msg, int time)
        {
            Log.Write("HomePagePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(1));

            POP.Visibility = Visibility.Hidden;

        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
    }
}
