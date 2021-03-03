using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Resources;
using BaseDLL;
using System.Drawing.Printing;

namespace EXC
{
    /// <summary>
    /// NoHomePages.xaml 的交互逻辑
    /// </summary>
    public partial class NoHomePages : Page
    {
        string filePath;
        bool PrintAlready = false;
        
        private IDCardData idcardData;

        //无房页面 
        public NoHomePages(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }
        //页面初始化
        private void Page_Initialized(object sender, EventArgs e)
        {
            MainWindow.PDFControl.BeginInit();
            formsHost.Child = MainWindow.PDFControl;
            MainWindow.PDFControl.EndInit();

            TotalLabel.Content = idcardData.Name.Replace(" ", "") + TotalLabel.Content;
            DataContext = Time;

            Countdown_timer();

            //开启查询的时候要启动一个线程
            Thread worker2 = new Thread(() => NoHomeRequests())
            {
                IsBackground = true
            };
            worker2.Start();
            WaitShow.Visibility = Visibility.Visible;
        }

        private void NoHomeRequests()
        {
            string response = Http.RealEstate.NoHome(idcardData.Name, idcardData.IDCardNo);
            Dispatcher.BeginInvoke(new Action(() => Parse(response)));
        } 
        //private List<string> AreaNameList = new List<string>() { "姑苏区","吴中区", "相城区", "虎丘区", "吴江区", "工业园区", "常熟市", "昆山市", "太仓市", "张家港市" };
        private void Parse(string response)
        {
            if (response ==null)
            {
                Content = new HomePage("该接口连接错误");
                Pages();
            }
            try
            {
                JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                string code = (string)jsons.GetValue("code");
                string msg = (string)jsons.GetValue("Message"); 

                if (code == "0")
                {
                    string pdfbs4 = (string)jsons.GetValue("report");
                    filePath = "Temp//" + (string)jsons.GetValue("filename");
                    XCovert.Base64ToFile(pdfbs4, filePath);

                    WaitShow.Visibility = Visibility.Hidden;
                    List<Label> AreaList = new List<Label>() { resLabel0, resLabel1, resLabel2, resLabel3, resLabel4, resLabel5, resLabel6, resLabel7, resLabel8, resLabel9 };
                    bool have = false;
                    for (int i = 0; i < AreaList.Count; i++)
                    {
                        string count = (string)jsons["body"][i]["count"];
                        AreaList[i].Content = count != "0" ? jsons["body"][i]["qxmc"] + "：" + count + "套房子" : jsons["body"][i]["qxmc"] + "：" + "无房";
                        if (count != "0")
                            have = true;
                    }

                    PrintButton.Visibility = Visibility.Visible; //不管如何都提供打印功能  (原版设计不满足条件不提供打印)

                    if (!have)
                    {
                        if (Global.configData.PrintOptimize)
                        {
                            WaitShow.Visibility = Visibility.Visible;
                            Dispatcher.Invoke(new Action(() => PrintOpen()));
                        }
                    }
                    else
                    {
                        PopAlert("您名下有房，可以查询房产信息", 3);
                    }

                }
                else
                {
                    Content = new HomePage(msg);
                    Pages();
                }
            }
            catch 
            {
                Content = new HomePage("该接口解析错误");
                Pages();
            }
        }
        private async void PopAlert(string Msg, int time)
        {
            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
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
                MessageBox.Show("正在打印中，请耐心等待", "EXC", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            pageTimer = new DispatcherTimer(){IsEnabled = true,Interval = TimeSpan.FromSeconds(1)};
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
                Dispatcher.BeginInvoke(new Action(async () => await sssAsync()));
            }

        }
        private async Task sssAsync()
        {
            Dispatcher.Invoke(new Action(() => WaitShow.Visibility = Visibility.Visible));
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
            //Dispatcher.BeginInvoke(new Action(() => alert("打印", 3))); //相城就跳这个逻辑即可

            Http.RealEstate.AddAction(idcardData.Name, idcardData.IDCardNo, "dayinszliuquwufang");
            Log.Write("打印无房证明");
        }


        Timer timer;
        int Ostatue = 0;
        bool Print = false;
        int PrintTime = 0;
        static PrintDocument print = new PrintDocument();
        private  string PrinterDefaultName = print.PrinterSettings.PrinterName;//默认打印机名
        private async Task TimeRunAsync(int PageAllNum)
        {
            int Nstatue = Prints.PrinterStatue(PrinterDefaultName);
            Print = Ostatue != 0 && Nstatue == 0;
            Ostatue = Ostatue == Nstatue ? Ostatue : Nstatue;
            WaitLabel.Content = Prints.PrinterStatusCodes.ContainsKey(Nstatue)?Prints.PrinterStatusCodes[Nstatue].ToString(): $"{Nstatue}";
            PrintTime += 1;
            if (PrintTime > 20)
                Print = true;

            if (Print)
            {
                timer.Dispose();
                WaitLabel.Content = "PDF发送到打印机";
                await Task.Delay(2 * 1000);
                WaitLabel.Content = "打印机正在打印";
                await Task.Delay(PageAllNum * 1000);
                WaitLabel.Content = "打印完毕，正在返回";

                Dispatcher.Invoke(new Action(() => Media.Player(null, 7)));
                await Task.Delay(5 * 1000);

                WaitLabel.Content = "";
                Print = false;
                Ostatue = 0;
                WaitLabel.Visibility = Visibility.Hidden;
                PrintAlready = false;
                Content = new HomePage();
                Pages();
            }
        }

        private async void alert(string msg, int time)
        {
            if (msg == "打印")
            {
                WaitLabel.Content = "正在初始化打印机";

                WaitLabel.Visibility = Visibility.Visible;
                await Task.Delay(TimeSpan.FromSeconds(10));

                WaitLabel.Content = "正在打印中";
                await Task.Delay(TimeSpan.FromSeconds(time+ Global.configData.PrintDelay));

                WaitLabel.Content = "打印完毕，正在返回";
                Dispatcher.Invoke(new Action(() => Media.Player(null, 7)));
                await Task.Delay(TimeSpan.FromSeconds(10));

                WaitLabel.Visibility = Visibility.Hidden;
                Content = new HomePage();
                Pages();
            }
            if(msg == "有房")
            {
                WaitLabel.Content = "请查询有房证明";
                WaitLabel.Visibility = Visibility.Visible;
                await Task.Delay(TimeSpan.FromSeconds(time));
                WaitLabel.Visibility = Visibility.Hidden;
            }
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
    }
}
