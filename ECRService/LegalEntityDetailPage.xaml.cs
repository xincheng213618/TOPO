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
using System.ComponentModel;

namespace ECRService
{
    public class LegalEntityDetailItem
    {
        public string label { get; set; }
        public string text { get; set; }
    }

    /// <summary>
    /// LegalEntityDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class LegalEntityDetailPage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        private LegalEntityQueryPage returnPage = null;
        private string identifier = null;

        private string[] queryTypeName = new string[5] { "工商企业", "个体工商户", "民办非企业单位", "社会团体", "事业单位" };
        private ObservableCollection<LegalEntityDetailItem> listItems = null;

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
        public LegalEntityDetailPage(LegalEntityQueryPage returnPage, string identifier, int Kinds)
        {
            this.Kinds = Kinds;
            this.returnPage = returnPage;
            this.identifier = identifier;

            InitializeComponent();
            this.DataContext = this;

            listItems = new ObservableCollection<LegalEntityDetailItem>();
            listView.ItemsSource = listItems;


        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            title.Content = queryTypeName[Kinds - 1] + "信息";
            hintBorder.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询" + queryTypeName[Kinds - 1] + "，请稍候！";

            Thread worker = new Thread(() => GetLegalEntityDetailProc());
            worker.Start();
        }

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
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

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = returnPage;
            returnPage.ResetTimer();
            Pages();
        }

        private void GetLegalEntityDetailCompleted(string response)
        {
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
                    string innerText;

                    innerText = document.SelectSingleNode("//data/result/record/qymc").InnerText;
                    if (innerText.Length > 0)
                    {
                        listViewHeader.Content = innerText;
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = queryTypeName[Kinds- 1] + "名称",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/tyshxydm").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "统一社会信用代码",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/zzjgdm").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "组织机构代码",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/fddbr").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "法定代表人",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/zczj").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "注册资金",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/qylxmc").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "企业类型名称",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/slrq").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "设立日期",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/zs").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "地址",
                            text = innerText
                        });
                    }

                    innerText = document.SelectSingleNode("//data/result/record/jyfw").InnerText;
                    if (innerText.Length > 0)
                    {
                        listItems.Add(new LegalEntityDetailItem()
                        {
                            label = "经营范围",
                            text = innerText
                        });
                    }
                }
                else if (returncode.Equals("0"))
                {
                    hintBorder.Visibility = Visibility.Visible;
                    border.Visibility = Visibility.Visible;
                    hintLabel.Content = returnmsg;
                }
                else if (returncode.Equals("e"))
                {
                    throw new Exception(returnmsg);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GetLegalEntityDetailProc()
        {
            try
            {
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = 16777216;
                binding.SendTimeout = TimeSpan.FromSeconds(30);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                ReportService queryService = new ReportServiceClient(binding, endpointAddress);
                string response = queryService.getQyjbxx(Global.Config.LoginName, Global.Config.LoginPassword, identifier);
               
                /*
                string response = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><data><returncode>1</returncode><returnmsg>调用成功</returnmsg><result>"
                    + "<record><qyid>123232312312313</qyid><qymc>江苏同袍科技有限公司</qymc><fddbr>李龙丹</fddbr><tyshxydm>EW234234234234X</tyshxydm>"
                    + "<zzjgdm>123232312312313</zzjgdm><zczj>11111</zczj><qylxmc>1111</qylxmc><slrq>3434</slrq><zs>erwe</zs><jyfw>343443</jyfw></record></result></data>";
                */
                Dispatcher.BeginInvoke(new Action(() => GetLegalEntityDetailCompleted(response)));
            }
            catch (Exception  )
            {

            }
        }



        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();

        }
    }
}
