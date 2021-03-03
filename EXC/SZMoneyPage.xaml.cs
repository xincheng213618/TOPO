using Microsoft.Ink;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using Resources;

namespace EXC
{
    /// <summary>
    /// SZMoneyPage.xaml 的交互逻辑
    /// </summary>
    public partial class SZMoneyPage : Page
    {
        public SZMoneyPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();

            //Thread worker2 = new Thread(() => NoHomePage());
            //worker2.IsBackground = true;
            //worker2.Start();
        }

        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public Times Time = new Times();

        private void Countdown_timer()
        {
            this.DataContext = Time;
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }



        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    break;
                case "XinHuaSearch":
                case "XinHuaTaxA":
                case "XinHuaTaxV":
                case "LostCredit":
                case "QingDaoReport":
                case "TopoSearch":
                case "SZHQMoney":
                case "SZHQProgress":
                    Global.PageType = button.Tag.ToString();
                    Content = new SearchPage();
                    break;
                case "PDF":
                    Content = new Pdfshow();
                    break;

                case "AForge":
                    Content = new AForgePage();
                    break;
                case "OutOfServicePage":
                    Content = new OutOfServicePage();
                    break;
                case "NoHomePages":
                case "OwnerShipPages":
                case "HomeCountPages":
                case "NoHomeChild":
                case "SZHQArchivePages":
                case "ReportNingYang":
                case "ReportGRNingYang":
                case "ReportNanJing":
                case "ReportGRNanJing":
                case "ReportNingYangAll":
                case "ReportXinTai":
                case "ReportGRXinTai":
                    Global.PageType = button.Tag.ToString();
                    Content = new IDCardPage();
                    break;
                case "SZMoneyPages":
                    Content = new SZMoneyPage();
                    break;
                default:
                    break;
            }
            Pages();

        }
    }
}
