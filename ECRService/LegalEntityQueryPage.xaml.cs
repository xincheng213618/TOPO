using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Xml;
using ECRService.ReportServiceReference;
using BaseUtil;

namespace ECRService
{
    public class LegalEntityListItem
    {
        public int xh { get; set; }
        public string qyid { get; set; }
        public string qymc { get; set; }
        public string fddbr { get; set; }
        public string tyshxydm { get; set; }
    }

    /// <summary>
    /// LegalEntityQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class LegalEntityQueryPage : Page
    {
        private const int TIMEOUT = 90;
        private DispatcherTimer pageTimer = null;

        private string[] queryTypeName = new string[5] { "工商企业", "个体工商户", "民办非企业单位", "社会团体", "事业单位" };

        private bool querying = false;
        private bool queried = false;
        private string legalEntityName = null;
        private ObservableCollection<LegalEntityListItem> listItems = null;

        private  int Kinds = 0;
        public LegalEntityQueryPage(int Kinds)
        {
            this.Kinds = Kinds;
            InitializeComponent();

        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = MainWidownData;

            Countdown_timer();
            FocusManager.SetFocusedElement(this, textBox);
            title.Content = queryTypeName[Kinds - 1] + "信息查询";
            label.Content = queryTypeName[Kinds - 1] + "名称：";

            column1.Header = queryTypeName[Kinds - 1] + "名称";
            column1Label.Content = queryTypeName[Kinds - 1] + "名称";

            column2.Header = Kinds == 2 ? "姓名" : "法定代表人";
            column2Label.Content = column2.Header;
        }

        private MainWidownData MainWidownData = new MainWidownData() { Countdown = 90 };

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--MainWidownData.Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }
            });
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new LegalEntityQueryIndexPage();
            Pages();
        }

        private void GetLegalEntityListCompleted(string response)
        {
            querying = false;
            queried = true;
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(response);

                string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;

                if (returncode.Equals("1"))
                {
                    int i = 0;
                    listItems = new ObservableCollection<LegalEntityListItem>();
                    listView.ItemsSource = listItems;
                    XmlNodeList xnList = document.SelectNodes("//data/result/record");
                    foreach (XmlNode xn in xnList)
                    {
                        LegalEntityListItem item = new LegalEntityListItem();

                        item.xh = ++i;
                        item.qyid = xn["qyid"].InnerText;
                        item.qymc = xn["qymc"].InnerText;
                        item.fddbr = xn["fddbr"].InnerText;
                        item.tyshxydm = xn["tyshxydm"].InnerText;
                        listItems.Add(item);
                    }

                    listViewHeader.Visibility = Visibility.Visible;
                    listView.Visibility = Visibility.Visible;
                }
                else if (returncode.Equals("0"))
                {
                    listViewHeader.Visibility = Visibility.Hidden;
                    listView.Visibility = Visibility.Hidden;
                    hintBorder.Visibility = Visibility.Visible;
                    hintLabel.Content = returnmsg;
                }
                else if (returncode.Equals("e"))
                {
                    throw new Exception(returnmsg);
                }
            }
            catch (Exception  )
            {
                pageTimer.IsEnabled = false;
            }
       }

        private void GetLegalEntityListProc()
        {
            System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
            binding.MaxReceivedMessageSize = 16777216;
            binding.SendTimeout = TimeSpan.FromSeconds(30);
            binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
            System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
            ReportService queryService = new ReportServiceClient(binding, endpointAddress);
            string response = queryService.getQyxyxx(Global.Config.LoginName, Global.Config.LoginPassword, legalEntityName, Kinds.ToString());

            /*
       string response = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><data><returncode>1</returncode><returnmsg>调用成功</returnmsg><result>"
           + "<record><qyid>123232312312313</qyid><qymc>江苏同袍科技有限公司</qymc><fddbr>李龙丹</fddbr><tyshxydm>EW234234234234X</tyshxydm></record>"
           + "<record><qyid>123232312312313</qyid><qymc>江苏同袍科技有限公司</qymc><fddbr>李龙丹</fddbr><tyshxydm>EW234234234234X</tyshxydm></record></result></data>";
           */

            Dispatcher.BeginInvoke(new Action(() => GetLegalEntityListCompleted(response)));
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length == 0)
            {
                hintBorder.Visibility = Visibility.Visible;
                hintLabel.Content = "请输入" + queryTypeName[Kinds - 1] + "名称！";
                return;
            }

            if (querying)
                return;


            querying = true;
            legalEntityName = textBox.Text;

            hintBorder.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询" + queryTypeName[Kinds - 1] + "，请稍候！";

            Thread worker = new Thread(() => GetLegalEntityListProc());
            worker.Start();
        }

        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            LegalEntityListItem item = (LegalEntityListItem)((ListViewItem)sender).Content;

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new LegalEntityDetailPage(this, item.qyid,Kinds))));
        }

        public void ResetTimer()
        {
            pageTimer.IsEnabled = true;

            FocusManager.SetFocusedElement(this, null);
        }



        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            listViewHeader.Visibility = Visibility.Hidden;
            listView.Visibility = Visibility.Hidden;

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            if (queried)
            {
                listViewHeader.Visibility = Visibility.Visible;
                listView.Visibility = Visibility.Visible;
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
        }
    }
}
