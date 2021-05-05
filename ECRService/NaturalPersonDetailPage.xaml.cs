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
    public class NaturalPersonDetailItem
    {
        public string label { get; set; }
        public string text { get; set; }
    }

    /// <summary>
    /// NaturalPersonDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPersonDetailPage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        NaturalPersonQueryPage returnPage = null;
        string identifier;

        private string[] queryTypeName = new string[13] { "执业药师", "价格鉴证师", "工程建设领域执业人员", "执业律师", "信用管理师", "食品检验员", "公证员", "注册房地产估价师", "注册房地产经纪人", "监理工程师", "信息系统工程监理资质工程师", "计算机信息系统集成高级项目经理", "计算机信息系统集成项目经理" };
        private string[] queryType = new string[13] { "ZYYS", "JGJZ", "GCJS", "ZYLS", "XYGL", "SQJY", "GZY", "FDCGJ", "FDCJJ", "JLGC", "XXXTGCJL", "XTJCGJXMJL", "XTJCXMJL" };
        private ObservableCollection<NaturalPersonDetailItem> listItems = null;

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
        private int kinds = 0;
        public NaturalPersonDetailPage(NaturalPersonQueryPage returnPage, string identifier,int kinds)
        {
            this.returnPage = returnPage;
            this.identifier = identifier;
            this.kinds = kinds;
            InitializeComponent();
            this.DataContext = this;

            listItems = new ObservableCollection<NaturalPersonDetailItem>();
            listView.ItemsSource = listItems;

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    returnPage.ResetTimer();
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage)));
                }

                if (Countdown % 15 == 0)
                {
                    media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/7、取走身份证和报告.mp3", UriKind.Absolute);
                    media.Position = TimeSpan.Zero;
                    //media.Play();
                }

                //timerLabel.Content = countdown.ToString();
            });
            pageTimer.IsEnabled = true;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            returnPage.ResetTimer();
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(returnPage)));
        }

        private void GetZYYS(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/yslx").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "药师类型",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zylx").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "执业类型",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/xxds").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "学习地市",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zyfw").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "职业范围",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/rdnf").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "认定年份",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/xb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "性别",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/mz").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "民族",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/xl").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "学历",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zc").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "职称",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/gzdw").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "工作单位",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/lxdh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "联系电话",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sjhm").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "手机号码",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/yb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "邮编",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/txdz").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "通讯地址",
                    text = innerText
                });
            }
        }

        private void GetJGJZ(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zgzs").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "资格证书",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/xb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "性别",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zylb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "执业类别",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zydw").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "执业单位",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zcrq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "注册日期",
                    text = innerText
                });
            }
        }

        private void GetGCJS(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsmc").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书名称",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zcrq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "注册日期",
                    text = innerText
                });
            }
        }

        private void GetZYLS(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zyzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "执业证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/lsswsmc").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "律师事务所名称",
                    text = innerText
                });
            }
        }

        private void GetXYGL(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/gzdw").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "工作单位",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/qfrq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "签发日期",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/jb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "级别",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/xb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "性别",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }
        }

        private void GetSQJY(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/fwjg").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "服务机构",
                    text = innerText
                });
            }
        }

        private void GetGZY(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/jgmc").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "机构名称",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/jgzyzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "机构执业证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/jgdz").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "机构地址",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/jgdh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "机构电话",
                    text = innerText
                });
            }
        }

        private void GetFDCGJ(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsmc").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书名称",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sprq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "审批日期",
                    text = innerText
                });
            }
        }

        private void GetFDCJJ(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsmc").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书名称",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sprq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "审批日期",
                    text = innerText
                });
            }
        }

        private void GetJLGC(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/xb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "性别",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/ssdw").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "所属单位",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsmc").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书名称",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zshm").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书号码",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/fzdw").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "发证单位",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sid").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "识别号",
                    text = innerText
                });
            }
        }

        private void GetXXXTGCJL(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sprq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "审批日期",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/schzrq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "生成核准日期",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zt").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "状态",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/fwjg").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "服务机构",
                    text = innerText
                });
            }
        }

        private void GetXTJCGJXMJL(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sprq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "审批日期",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/fwjg").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "服务机构",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsjb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书级别",
                    text = innerText
                });
            }
        }

        private void GetXTJCXMJL(XmlDocument document)
        {
            string innerText;

            innerText = document.SelectSingleNode("//data/result/record/xm").InnerText;
            if (innerText.Length > 0)
            {
                listViewHeader.Content = innerText + "的信息";
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "姓名",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sfzh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "身份证号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsbh").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书编号",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/sprq").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "审批日期",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/fwjg").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "服务机构",
                    text = innerText
                });
            }

            innerText = document.SelectSingleNode("//data/result/record/zsjb").InnerText;
            if (innerText.Length > 0)
            {
                listItems.Add(new NaturalPersonDetailItem()
                {
                    label = "证书级别",
                    text = innerText
                });
            }
        }

        private void GetNaturalPersonDetailCompleted(string response)
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
                    switch(kinds)
                    {
                        case 1:
                            GetZYYS(document);
                            break;
                        case 2:
                            GetJGJZ(document);
                            break;
                        case 3:
                            GetGCJS(document);
                            break;
                        case 4:
                            GetZYLS(document);
                            break;
                        case 5:
                            GetXYGL(document);
                            break;
                        case 6:
                            GetSQJY(document);
                            break;
                        case 7:
                            GetGZY(document);
                            break;
                        case 8:
                            GetFDCGJ(document);
                            break;
                        case 9:
                            GetFDCJJ(document);
                            break;
                        case 10:
                            GetJLGC(document);
                            break;
                        case 11:
                            GetXXXTGCJL(document);
                            break;
                        case 12:
                            GetXTJCGJXMJL(document);
                            break;
                        case 13:
                            GetXTJCXMJL(document);
                            break;
                    }
                }
                else if (returncode.Equals("0"))
                {
                    hintBorder.Visibility = Visibility.Visible;
                    hintLabel.Content = returnmsg;
                    /*
                        pageTimer.IsEnabled = false;
                        Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new MessagePage(returnmsg))));
                    */
                }
                else if (returncode.Equals("e"))
                {
                    throw new Exception(returnmsg);
                }
            }
            catch (Exception  )
            {

            }
        }

        private void GetNaturalPersonDetailProc()
        {
            try
            {
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = 16777216;
                binding.SendTimeout = TimeSpan.FromSeconds(30);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                ReportService queryService = new ReportServiceClient(binding, endpointAddress);
                string response = queryService.getZrrxyxx(Global.Config.LoginName, Global.Config.LoginPassword, identifier, queryType[kinds - 1]);

                Dispatcher.BeginInvoke(new Action(() => GetNaturalPersonDetailCompleted(response)));
            }
            catch (Exception  )
            {

            }
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            title.Content = queryTypeName[kinds - 1] + "信息";

            hintBorder.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询" + queryTypeName[kinds - 1] + "信息，请稍候！";

            Thread worker = new Thread(() => GetNaturalPersonDetailProc());
            worker.Start();
        }

        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
        }
    }
}
