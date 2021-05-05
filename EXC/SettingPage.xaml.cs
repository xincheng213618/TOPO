using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Net;
using System.Windows.Media;
using BaseDLL;
using BaseUtil;

namespace EXC
{

    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            Countdown_timer();
        }
        ////定时器

        private DispatcherTimer pageTimer = null;

        public TimeCount Time = new TimeCount();


        private void Countdown_timer()
        {
            this.DataContext = Time;
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {

                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }




        //页面转跳
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        //小功能
        //清除日志
        private void LogClear_Click(object sender, RoutedEventArgs e)
        {
            Log.Clearall();
        }
        //清除缓存
        private void CacheClear_Click(object sender, RoutedEventArgs e)
        {
            string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";
            string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";
            File.Delete(paths);
            File.Delete(paths_black);

            string path = "Temp";
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    MessageBox.Show("该命令需要管理员权限,请选择以管理员打开程序");
                }
            }
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }


       private void Update(string url = null)
        {
            if (url == null)
            {
                url = "http://xincheng.ddns.net:9090/s/2019.10.22.rar";

            }
            string[] str = url.Split('/');
            string filename = str.Last();
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, filename);
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("提交成功");
        }


        private int Index = 0;
        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Foreground = Brushes.HotPink;
            Index++;
            switch ((string)btn.Tag)
            {
                case "Function":
                    Panel.SetZIndex(FunctionScrollViewer, Index);
                    btn.FontWeight = FontWeights.Bold;
                    break;
                case "Page":
                    Panel.SetZIndex(PageScrollViewer, Index);
                    btn.FontWeight = FontWeights.Bold;
                    break;
                case "Test":
                    Panel.SetZIndex(TestScrollViewer, Index);
                    btn.FontWeight = FontWeights.Bold;
                    break;
                default:
                    break;
            }

        }
        //键盘屏蔽
        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = Brushes.AliceBlue;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = Brushes.Transparent;
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;
            HeaderShadow.Visibility = scroll.ContentVerticalOffset < 10 ? Visibility.Hidden : Visibility.Visible;
        }
        //功能切换
        // 2020.6.24 修正
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
                case "WinSpace":
                    User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.SHIFT });
                    break;
                case "CtrlAltO":
                    User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.O });
                    break;
                case "Close":
                    (Application.Current.MainWindow as MainWindow).Close();
                    break;
                case "Shield":
                    switch ((string)button.Content)
                    {
                        case "开启":
                            //button.Content =KeyboardFilter.ClearKeyboardFilter() ? "关闭" : "关闭";
                            break;
                        case "关闭":
                            //button.Content = KeyboardFilter.StartKeyboardFilter() ? "开启" : "开启";
                            break;
                        default:
                            button.Content = "关闭";
                            break;
                    }
                    break;
                case "Drag":

                    break;

                case "Log":
                    //GlobalData.configData.PrintOptimize = !GlobalData.configData.PrintOptimize;
                    //btn.Content = GlobalData.configData.PrintOptimize ? "允许" : "禁止";
                    break;
                case "Stamp":

                case "Camera":
                    int code = AmLivingBodyApi.AmOpenDevice();
                    button.Content = code == 0 ? "正常" : AmLivingBodyApi.Ecode[code];
                        AmLivingBodyApi.AmCloseDevice();
                    break;
                default:
                    break;
            }
        }
        //Page 跳转
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    break;
                case "QingDaoReport":
                case "TopoSearch":
                    Global.Related.PageType = button.Tag.ToString();
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

                case "ReportNingYang":
                case "ReportGRNingYang":
                case "ReportNanJing":
                case "ReportGRNanJing":
                case "ReportNingYangAll":
                case "ReportXinTai":
                case "ReportGRXinTai":
                    Global.Related.PageType = button.Tag.ToString();
                    Content = new IDCardPage();
                    break;
                default:
                    break;
            }
            Pages();
        }
        //样例初始化
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            switch ((string)button.Tag)
            {
                case "ReportNingYang":
                case "ReportGRNingYang":
                case "ReportNingYangAll":
                    Global.Related.IDCardData = new IDCardData { Name = "", IDCardNo = "37092119790520542x" };
                    Content = new Report();
                    break;
                case "ReportNanJing":
                case "ReportGRNanJing":
                    Global.Related.IDCardData = new IDCardData { Name = "陈信成", IDCardNo = "320323199712213618" };
                    Content = new Report();
                    break;
                case "ReportXinTai":
                case "ReportGRXinTai":
                    Global.Related.IDCardData = new IDCardData { Name = "李玉", IDCardNo = "370982198006245273" };
                    Content = new Report();
                    break;
                default:
                    break;
            }
            Pages();
        }
    }
}
