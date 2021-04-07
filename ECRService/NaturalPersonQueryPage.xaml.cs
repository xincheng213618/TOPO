using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using ECRService.ReportServiceReference;
using System.ComponentModel;

namespace ECRService
{
    public class NaturalPersonListItem
    {
        public int xh { get; set; }
        public string grid { get; set; }
        public string xm { get; set; }
        public string sfzjhm { get; set; }
    }

    /// <summary>
    /// NaturalPersonQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPersonQueryPage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;
        private CancellationTokenSource tokenSource = null;

        private string[] queryTypeName = new string[13] { "执业药师", "价格鉴证师", "工程建设领域执业人员", "执业律师", "信用管理师", "食品检验员", "公证员", "注册房地产估价师", "注册房地产经纪人", "监理工程师", "信息系统工程监理资质工程师", "计算机信息系统集成高级项目经理", "计算机信息系统集成项目经理"};
        private string[] queryType = new string[13] { "ZYYS", "JGJZ", "GCJS", "ZYLS", "XYGL", "SQJY", "GZY", "FDCGJ", "FDCJJ", "JLGC", "XXXTGCJL", "XTJCGJXMJL", "XTJCXMJL" };

        private bool querying = false;
        private bool queried = false;
        private string name = null;
        private string certificate = null;
        private ObservableCollection<NaturalPersonListItem> listItems = null;

        public int Countdown
        {
            get { return countdown; }
            set { countdown = value; OnPropertyChanged("Countdown"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int Kinds = 0;
        public NaturalPersonQueryPage(int Kinds)
        {
            this.Kinds = Kinds;
            InitializeComponent();
            this.DataContext = this;

            tokenSource = new CancellationTokenSource();
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    tokenSource.Cancel();
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }
            });

            title.Content = queryTypeName[Kinds - 1] + "信息查询";

            media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/3、放置身份证.mp3", UriKind.Absolute);
            media.Position = TimeSpan.Zero;
            //media.Play();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            tokenSource.Cancel();
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Kinds <= 8 ? (Page)new NaturalPersonQueryIndexPage() : (Page)new NaturalPersonQueryIndexPage())));
        }

        private void GetNaturalPersonListCompleted(string response)
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
                    listItems = new ObservableCollection<NaturalPersonListItem>();
                    listView.ItemsSource = listItems;

                    XmlNodeList xnList = document.SelectNodes("//data/result/record");
                    foreach (XmlNode xn in xnList)
                    {
                        NaturalPersonListItem item = new NaturalPersonListItem();

                        item.xh = ++i;
                        item.grid = xn["grid"].InnerText;
                        item.xm = xn["xm"].InnerText;
                        item.sfzjhm = xn["sfzjhm"].InnerText;
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
                    //border.Visibility = Visibility.Visible;
                    hintLabel.Content = returnmsg;
                }
                else if (returncode.Equals("e"))
                {
                    throw new Exception(returnmsg);
                }
            }
            catch (Exception ex)
            {
                pageTimer.IsEnabled = false;
            }

        }

        private void GetNaturalPersonListProc(CancellationToken token)
        {
            try
            {
                
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = 16777216;
                binding.SendTimeout = TimeSpan.FromSeconds(30);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                ReportService queryService = new ReportServiceClient(binding, endpointAddress);
                string response = queryService.getZrrxyxxList(Global.Config.LoginName, Global.Config.LoginPassword, name, certificate, queryType[Kinds - 1]);

                /*
                string response = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><data><returncode>1</returncode><returnmsg>调用成功</returnmsg><result>"
               + "<record><grid>1</grid><xm>张三</xm><sfzjhm>330231234567898767</sfzjhm></record>"
               + "<record><grid>1</grid><xm>张三</xm><sfzjhm>330231234567898767</sfzjhm></record></result></data>"; */
                if (!token.IsCancellationRequested)
                {
                    Dispatcher.BeginInvoke(new Action(() => GetNaturalPersonListCompleted(response)));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Length == 0)
            {
                hintBorder.Visibility = Visibility.Visible;
                hintLabel.Content = "请输入姓名！";
                return;
            }

            if (querying)
                return;


            querying = true;
            name = nameTextBox.Text;
            certificate = certificateTextBox.Text;

            hintBorder.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询" + queryTypeName[Kinds - 1] + "信息，请稍候！";

            Thread worker = new Thread(() => GetNaturalPersonListProc(tokenSource.Token));
            worker.Start();
        }

        public void ResetTimer()
        {
            Countdown = TIMEOUT;
            pageTimer.IsEnabled = true;

            FocusManager.SetFocusedElement(this, null);
        }

        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            NaturalPersonListItem item = (NaturalPersonListItem)((ListViewItem)sender).Content;

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonDetailPage(this, item.grid,Kinds))));
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            listViewHeader.Visibility = Visibility.Hidden;
            listView.Visibility = Visibility.Hidden;

        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            if (queried)
            {
                listViewHeader.Visibility = Visibility.Visible;
                listView.Visibility = Visibility.Visible;
            }
        }

        private void CertificateTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            listViewHeader.Visibility = Visibility.Hidden;
            listView.Visibility = Visibility.Hidden;

        }

        private void CertificateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (queried)
            {
                listViewHeader.Visibility = Visibility.Visible;
                listView.Visibility = Visibility.Visible;
            }
        }


        private void Page_Initialized(object sender, EventArgs e)
        {
            //nameTextBox.Focus();
            FocusManager.SetFocusedElement(this, nameTextBox);
        }



        private void Home_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate((Page)new HomePage())));
        }
    }
}
