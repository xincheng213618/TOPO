using BaseDLL;
using BaseUtil;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;

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
  //      private IDCardData IDCardData;
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.Related.PageType = button.Tag.ToString();
            switch (Global.Related.PageType)
            {
                case "NoHome":
                    Global.Related.IDCardData = new IDCardData { Name = "陈信成", IDCardNo = "320323199712213618" };
                    Content = new NoHomePages();
                    Pages();
                    break;
                case "OwnerShipPages":
                    Global.Related.IDCardData = new IDCardData { Name = "王留英", IDCardNo = "320502196312122544" };
                    Content = new OwnerShipPages();
                    Pages();
                    break;
                case "HomeCountPages":
                    Global.Related.IDCardData = new IDCardData { Name = "施丽华", IDCardNo = "320524197706045222" };
                    Content = new HomeCountPages();
                    Pages();
                    break;
                case "NoHomeChild":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZHQArchivePages":
                    Global.Related.IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "140108198708253219" };
                    Content = new SZArchivePage();
                    Pages();
                    break;
                case "SZWZArchivePages":
                    Global.Related.IDCardData = new IDCardData { Name = "张林", IDCardNo = "320823198102244241" };
                    //Global.Related.IDCardData = new IDCardData { Name = "施丽华", IDCardNo = "320524197706045222" };
                    //Global.Related.IDCardData = new IDCardData { Name = "陈东鸣", IDCardNo = "320504198411091255" };
                    Content = new SZArchivePage();
                    Pages();
                    break;
                case "SZMoneyPages":
                    Content = new SZMoneyPage();
                    Pages();
                    break;
                case "DegreePages":
                    Global.Related.IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "140108198708253219" };
                    Content = new DegreePage();
                    Pages();
                    break; 
                case "SZInvoiceyPages":                  
                    Content = new QRPage();
                    Pages();
                    break;
                default:
                    break;
            }
            Log.Write(Global.Related.PageType);
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
                    (Application.Current.MainWindow as MainWindow).Hide();
                    Environment.Exit(0);
                    break;
                case "CloseDegree":
                    Global.Config.DegreeOptimiz = "1";
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(Environment.CurrentDirectory+"\\Config");
                        //获得根节点
                        XmlNode rootNode = doc.DocumentElement;
                        //在根节点中寻找节点
                        foreach (XmlNode node in rootNode.ChildNodes)
                        {
                            //找到对应的节点
                            if (node.Name == "DegreeOptimiz")
                            {
                                node.InnerText = "1";
                            }
                         }
                           
                        
                        doc.Save(Environment.CurrentDirectory + "\\Config");
                    }
                    catch 
                    {
                        Content = new HomePage("学位查询功能关闭失败");
                        Pages();
                    }
                    Content = new HomePage("学位查询功能已经关闭");
                    Pages();
                    break;
                case "OpenDegree":
                    Global.Config.DegreeOptimiz = "0";
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(Environment.CurrentDirectory + "\\Config");
                        //获得根节点
                        XmlNode rootNode = doc.DocumentElement;
                        //在根节点中寻找节点
                        foreach (XmlNode node in rootNode.ChildNodes)
                        {
                            //找到对应的节点
                            if (node.Name == "DegreeOptimiz")
                            {
                                node.InnerText = "0";
                            }
                        }


                        doc.Save(Environment.CurrentDirectory + "\\Config");
                    }
                    catch
                    {
                        Content = new HomePage("学位查询功能开启失败");
                        Pages();
                    }
                    Content = new HomePage("学位查询功能已经开启");
                    Pages();
                    break;
                case "CloseDegreePrint":
                    if(Global.Config.DegreeOptimiz == "0")
                    {
                        Global.Config.DegreePrintOptimiz = "1";
                        try
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(Environment.CurrentDirectory + "\\Config");
                            //获得根节点
                            XmlNode rootNode = doc.DocumentElement;
                            //在根节点中寻找节点
                            foreach (XmlNode node in rootNode.ChildNodes)
                            {
                                //找到对应的节点
                                if (node.Name == "DegreePrintOptimiz")
                                {
                                    node.InnerText = "1";
                                }
                            }
                            doc.Save(Environment.CurrentDirectory + "\\Config");
                        }
                        catch
                        {
                            Content = new HomePage("学位打印功能关闭失败");
                            Pages();
                        }
                        Content = new HomePage("学位打印功能已经关闭");
                        Pages();
                    }else
                    {
                        Content = new HomePage("请先开启学位查询功能");
                        Pages();
                    }
                   
                    break;
                case "OpenDegreePrint":
                    if (Global.Config.DegreeOptimiz == "0")
                    {
                        Global.Config.DegreePrintOptimiz = "0";
                        try
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(Environment.CurrentDirectory + "\\Config");
                            //获得根节点
                            XmlNode rootNode = doc.DocumentElement;
                            //在根节点中寻找节点
                            foreach (XmlNode node in rootNode.ChildNodes)
                            {
                                //找到对应的节点
                                if (node.Name == "DegreePrintOptimiz")
                                {
                                    node.InnerText = "0";
                                }
                            }
                            doc.Save(Environment.CurrentDirectory + "\\Config");
                        }
                        catch
                        {
                            Content = new HomePage("学位打印功能开启失败");
                            Pages();
                        }
                        Content = new HomePage("学位打印功能已经开启");
                        Pages();
                    }else
                    {
                        Content = new HomePage("请先开启学位查询功能");
                        Pages();
                    }
                   
                    break;
                default:
                    break;
            }
        }
    }
}
