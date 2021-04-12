using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
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

namespace RECSuzhou
{
    /// <summary>
    /// HomeCountPages.xaml 的交互逻辑
    /// </summary>
    public partial class HomeCountPages : Page
    {
        string FileName;

        public HomeCountPages()
        {
            InitializeComponent();
        }
        private IDCardData idcardData;
        public HomeCountPages(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            AcrobatHelper.pdfControl.BeginInit();
            formsHost.Child = AcrobatHelper.pdfControl;
            AcrobatHelper.pdfControl.EndInit();

            TotalLabel.Content = idcardData.Name.Replace(" ", "") + TotalLabel.Content;
            DataContext = Time;
            Countdown_timer();
            Thread worker2 = new Thread(() => HomeCount());
            worker2.IsBackground = true;
            worker2.Start();
            PopTips.Text = "正在查询中";
            WaitShow.Visibility = Visibility.Visible;
        }
        private void Msg(string mes)
        {
            Content = new HomePage(mes);
            Pages();
        }

        private void HomeCount()
        {
            string response = Http.HomeCount(idcardData.Name, idcardData.IDCardNo);

            Dispatcher.BeginInvoke(new Action(() => Parse(response)));
        }

        private int HomeCountNum = 0;
        private ObservableCollection<HouseItem> HouseItem = new ObservableCollection<HouseItem>();
        private void Parse(string response)
        {
            if (response != null)
            {
                WaitShow.Visibility = Visibility.Hidden;
                try
                {
                    HomeCountListView.ItemsSource = HouseItem;
                    JObject jsons = (JObject)JsonConvert.DeserializeObject(response);

                    if (jsons["code"].ToString() == "0")
                    {
                        JArray houselist = (JArray)jsons["houselist"];

                        foreach (JObject house in houselist)
                        {
                            HouseItem item = new HouseItem
                            {
                                ListNo = ++HomeCountNum,
                                HouseID = (string)house.GetValue("zuoluo"),
                                Location = (string)house.GetValue("zuoluo"),
                                Name = (string)house.GetValue("name")
                            };
                            HouseItem.Add(item);
                        }

                        if (HouseItem.Count == 0)
                            HomeCountMsg.Visibility = Visibility.Visible;

                        FileName = Environment.CurrentDirectory + "\\Temp\\" + (string)jsons.GetValue("filename");
                        string report = (string)jsons.GetValue("report");
                        if (!Covert.Base64ToFile(report, FileName))
                            FileName = null;

                        PrintButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Content = new HomePage("查不到房产信息");
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("该接口解析错误");
                    Pages();
                }

            }
            else
            {
                Content = new HomePage("该接口连接错误");
                Pages();
            }

        }
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            WaitShow.Visibility = Visibility.Visible;
            PopTips.Text = "正在打印";
            pageTimer.IsEnabled = false;
            Time.ButtonClor = "#60d0ff";
            AcrobatHelper.pdfControl.LoadFile(FileName);
            int run = Stamp.Start(5);
            Log.Write("启动盖章机：" + run);
            if(!"0".Equals(run.ToString()))
            {
                Content = new HomePage("盖章机启动失败，请重启盖章机");
                Pages();
                return;
            }
            AcrobatHelper.pdfControl.printAll();

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(async () => await TimeRunAsync(1))), null, 0, 500);
        }

        Timer timer; //打印用
        int Ostatue = 0;
        bool PrintSucess = false;
        int PrintTimeNo = 0;//防止出现什么意外导致迟迟卡死在此页面，一定次数之后直接判定超时

        static PrintDocument print = new PrintDocument();

        private async Task TimeRunAsync(int PageAllNum)
        {
            int Nstatue = PrintStatus.PrinterStatue(print.PrinterSettings.PrinterName);
            PrintSucess = Ostatue != 0 && Nstatue == 0;
            Ostatue = Ostatue == Nstatue ? Ostatue : Nstatue;
            PopTips.Text = PrintStatus.PrinterStatusCodes.ContainsKey(Nstatue) ? PrintStatus.PrinterStatusCodes[Nstatue].ToString() : $"{Nstatue}";
            PrintTimeNo += 1;

            if (PrintSucess || (PrintTimeNo < 20 + PageAllNum))
            {
                timer.Dispose();
                PopTips.Text = "PDF已经发送到打印机";
                await Task.Delay(2 * 1000);
                PopTips.Text = "打印机正在打印";
                await Task.Delay(PageAllNum * 1000);
                PopTips.Text = "打印完成";
                await Task.Delay(3 * 1000);
                PopTips.Text = "";
                Ostatue = 0;

                Content = new PrintTips();
                Pages();
            }
            else
            {
                timer.Dispose();
                Ostatue = 0;

                Content = new HomePage("打印失败，请联系维修人员");
                Pages();
            }

        }


        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
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

        private void HomeCountListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
