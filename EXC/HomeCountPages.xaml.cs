using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Resources;
using BaseDLL;
using System.Drawing.Printing;

namespace EXC
{
    /// <summary>
    /// HomeCountPages.xaml 的交互逻辑
    /// </summary>
    public partial class HomeCountPages : Page
    {
        string filePath;
        bool PrintAlready = false;

        private IDCardData idcardData;
        //苏州不动产 房屋套数
        public HomeCountPages()
        {
            InitializeComponent();
        }

        public HomeCountPages(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            MainWindow.PDFControl.BeginInit();
            formsHost.Child = MainWindow.PDFControl;
            MainWindow.PDFControl.EndInit();

            HomeCountLabel.Content= idcardData.Name.Replace(" ", "") + HomeCountLabel.Content;
            DataContext = Time;
            Countdown_timer();
            Thread worker2 = new Thread(() => HomeCount());
            worker2.IsBackground = true;
            worker2.Start();
            PopLabel.Content = "正在查询中";
            PopBorder.Visibility = Visibility.Visible;
        }
        private void Msg(string mes)
        {
            Content = new HomePage(mes);
            Pages();
        }
        private void HomeCount()
        {
            string response = Http.RealEstate.HomeCount(idcardData.Name, idcardData.IDCardNo);

            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => Msg("该接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => Parse(response)));
        }
        private int HomeCountNum = 0;
        private ObservableCollection<HouseItem> HouseItem = new ObservableCollection<HouseItem>();
        private void Parse(string response)
        {
            PopBorder.Visibility = Visibility.Hidden;
            try
            {
                HomeCountListView.ItemsSource = HouseItem;
                JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                string Code = jsons["code"].ToString();
                string Msg = jsons["Message"].ToString();

                if (Code != "0")
                {
                    Content = new HomePage(Msg);
                    Pages();
                    return;
                }

                JArray houselist = (JArray)jsons["houselist"];
                if (houselist.Count > 0)
                {
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
                }
                else
                {
                    HomeCountMsg.Visibility = Visibility.Visible;
                }

                filePath = Environment.CurrentDirectory + "\\Temp\\" + (string)jsons.GetValue("filename");
                string report = (string)jsons.GetValue("report");
                if (!XCovert.Base64ToFile(report, filePath))
                {
                    filePath = null;
                }
                PrintButton.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                Dispatcher.BeginInvoke(new Action(() => Msg("该接口解析错误")));
                return;
            }

        }

        private void HomeCountListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintAlready)
            {
                Content = new HomePage();
                Pages();
            }
            else
            {
                MessageBox.Show("正在打印中，请耐心等待","EXC",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }

        }

        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        //倒计时模块
        private DispatcherTimer pageTimer = null;

        public Times Time = new Times();

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


        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintAlready)
            {
                PrintAlready = true;
                Dispatcher.BeginInvoke(new Action(async () => await SssAsync()));
            }

        }
        private async Task SssAsync()
        {
            Dispatcher.Invoke(new Action(() => PopBorder.Visibility = Visibility.Visible));
            Time.ButtonClor = "#60d0ff";
            await Task.Delay(500);
            Dispatcher.Invoke(new Action(() => PrintOpen()));
        }
        private void PrintOpen()
        {
            MainWindow.PDFControl.LoadFile(filePath);
            Stamp.Start(1);

            MainWindow.PDFControl.printAll();

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(async () => await TimeRunAsync(3))), null, 0, 500);
            Http.RealEstate.AddAction(idcardData.Name, idcardData.IDCardNo, "dayinzhuzhaitaoshu");
        }


        Timer timer;
        int Ostatue = 0;
        bool Print = false;
        int PrintTime = 0;
        static PrintDocument print = new PrintDocument();
        private string PrinterDefaultName = print.PrinterSettings.PrinterName;//默认打印机名
        private async Task TimeRunAsync(int PageAllNum)
        {
            int Nstatue = Prints.PrinterStatue(PrinterDefaultName);
            Print = Ostatue != 0 && Nstatue == 0;
            Ostatue = Ostatue == Nstatue ? Ostatue : Nstatue;
            PopLabel.Content = Prints.PrinterStatusCodes.ContainsKey(Nstatue) ? Prints.PrinterStatusCodes[Nstatue].ToString() : $"{Nstatue}";
            PrintTime += 1;
            if (PrintTime > 20)
                Print = true;

            if (Print)
            {
                timer.Dispose();
                PopLabel.Content = "PDF发送到打印机";
                await Task.Delay(2 * 1000);
                PopLabel.Content = "打印机正在打印";
                await Task.Delay(PageAllNum * 1000);
                PopLabel.Content = "打印完毕，正在返回主界面";

                Dispatcher.Invoke(new Action(() => Media.Player(null, 7)));
                await Task.Delay(5 * 1000);

                PopLabel.Content = "";
                Print = false;
                Ostatue = 0;
                PopBorder.Visibility = Visibility.Hidden;
                PrintAlready = false;

                Content = new HomePage();
                Pages();
            }
        }

        private async void Alert(string msg, int time)
        {
            PopLabel.Content = "正在初始化打印机";
            PopBorder.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(5));
            PopLabel.Content = "正在打印中";
            await Task.Delay(TimeSpan.FromSeconds(time + Global.configData.PrintDelay));

            PopLabel.Content = "打印完毕，返回主界面";

            Dispatcher.Invoke(new Action(() => Media.Player(null, 7)));
            await Task.Delay(TimeSpan.FromSeconds(5));
            PopBorder.Visibility = Visibility.Hidden;
            Content = new HomePage();
            Pages();

        }

    }

}
