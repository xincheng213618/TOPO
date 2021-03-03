using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace RECSuzhou
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            DataContext = Time;
            Countdown_timer();
        }


        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Border border = sender as Border;
            //border.Background = Brushes.AliceBlue;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Border border = sender as Border;
            //border.Background = Brushes.Transparent;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();


        private void Countdown_timer()
        {

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
       
        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Foreground = Brushes.HotPink;
          
          
             
            switch ((string)btn.Tag)
            {
                case "Function":
                    
                    FunctionScrollViewer.Visibility = Visibility.Visible;
                    PageScrollViewer.Visibility = Visibility.Hidden;
                    TestScrollViewer.Visibility = Visibility.Hidden;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                case "Page":
                    FunctionScrollViewer.Visibility = Visibility.Hidden;
                    PageScrollViewer.Visibility = Visibility.Visible;
                    TestScrollViewer.Visibility = Visibility.Hidden;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                case "Test":
                    FunctionScrollViewer.Visibility = Visibility.Hidden;
                    PageScrollViewer.Visibility = Visibility.Hidden;
                    TestScrollViewer.Visibility = Visibility.Visible;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                default:
                    break;
            }
        }
        private IDCardData IDCardData;
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Log.Write(Global.PageType);
            Global.PageType = button.Tag.ToString();
            switch (Global.PageType)
            {
                case "NoHome":
                    IDCardData = new IDCardData { Name = "陈信成", IDCardNo = "320323199712213618" };
                    Content = new NoHomePages(IDCardData);
                    Pages();
                    break;
                case "OwnerShipPages":
                    IDCardData = new IDCardData { Name = "王留英", IDCardNo = "320502196312122544" };
                    Content = new OwnerShipPages(IDCardData);
                    Pages();
                    break;
                case "HomeCountPages":
                    IDCardData = new IDCardData { Name = "奚玉远", IDCardNo = "152322198703291816" };
                    Content = new HomeCountPages(IDCardData);
                    Pages();
                    break;
                case "NoHomeChild":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZHQArchivePages":
                    IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "140108198708253219" };
                    Content = new SZArchivePage(IDCardData);
                    Pages();
                    break;
                case "SZWZArchivePages":
                    IDCardData = new IDCardData { Name = "张林", IDCardNo = "320823198102244241" };
                    Content = new SZArchivePage(IDCardData);
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

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
               
                case "PDF":
                    Content = new Pdfshow();
                    break;

             
                default:
                    break;
            }
            Pages();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
              
                case "Close":
                    //(Application.Current.MainWindow as MainWindow).Close();
                    Environment.Exit(0);
                    break;
              
                default:
                    break;
            }
        }
    }
}
