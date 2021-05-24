using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RECSuzhou
{
    /// <summary>
    /// NoHomePages.xaml 的交互逻辑
    /// </summary>
    public partial class NoHomePages : Page
    {
        string FileName="";

        //无房页面 
        public NoHomePages()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            AcrobatHelper.pdfControl.BeginInit();
            formsHost.Child = AcrobatHelper.pdfControl;
            AcrobatHelper.pdfControl.EndInit();

            TotalLabel.Content = Global.Related.IDCardData.Name.Replace(" ", "") + TotalLabel.Content;

            CoutLabel.DataContext = Time;
            Countdown_timer();
            WaitShow.Visibility = Visibility.Visible;

            //开启查询的时候要启动一个线程
            Thread thread = new Thread(() => NoHomeRequests())
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void NoHomeRequests()
        {          
                string response = Http.NoHome(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo);
                Dispatcher.BeginInvoke(new Action(() => Parse(response)));
         
        }

        private void Parse(string response)
        {
            if (response != null)
            {
                WaitShow.Visibility = Visibility.Hidden;
                try
                {
                    JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                    string code = (string)jsons.GetValue("code");
                    string msg = (string)jsons.GetValue("Message");

                    if (code == "0")
                    {
                        string pdfbs4 = (string)jsons.GetValue("report");
                        FileName = "Temp//" + (string)jsons.GetValue("filename");
                        Covert.Base64ToFile(pdfbs4, FileName);

                        WaitShow.Visibility = Visibility.Hidden;
                        List<Label> AreaList = new List<Label>() { resLabel0, resLabel1, resLabel2, resLabel3, resLabel4, resLabel5, resLabel6, resLabel7, resLabel8, resLabel9 };
                        //bool have = false;
                        for (int i = 0; i < AreaList.Count; i++)
                        {
                            string count = (string)jsons["body"][i]["count"];
                            AreaList[i].Content = count != "0" ? jsons["body"][i]["qxmc"] + "：" + count + "套房子" : jsons["body"][i]["qxmc"] + "：" + "无房";
                            //if (count != "0")
                                //have = true;
                        }
                        PrintButton.Visibility = Visibility.Visible; //不管如何都提供打印功能  (原版设计不满足条件不提供打印)
                    }
                    else
                    {
                        Content = new HomePage(msg);
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("无房接口解析错误");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("无房接口连接错误");
                Pages();
            }
        }



        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }


        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if(Global.Related.PageType == "NoHome")
            { 
                Http.AddAction(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo, "dayinwufang","");
            }
            else
            {
                Http.AddAction(Global.Related.IDCardData.Name, Global.Related.IDCardData.IDCardNo, "Childdayinwufang", Global.Related.OprName+"||"+Global.Related.OprCardNo);
            }
                
            WaitShow.Visibility = Visibility.Visible;
            PopTips.Text = "正在打印";
            pageTimer.IsEnabled = false;
            Time.ButtonClor = "#60d0ff";
            AcrobatHelper.pdfControl.LoadFile(FileName);
            //int I = Stamp.Start(1);
            int run = Stamp.Start(1);
            Log.Write("启动盖章机：" + run);
            //if (!"0".Equals(run.ToString()))
            //{
            //    Content = new HomePage("盖章机启动失败，请重启盖章机");
            //    Pages();
            //    return;
            //}
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(async () => await TimeRunAsync(5))), null, 0, 500);

            AcrobatHelper.pdfControl.printAll();
        }   

        Timer timer; //打印用
        int Ostatue = 0;
        bool PrintSucess = false;
        int PrintTimeNo = 0;//防止出现什么意外导致迟迟卡死在此页面，一定次数之后直接判定超时

        static readonly PrintDocument print = new PrintDocument();

        private async Task TimeRunAsync(int PageAllNum)
        {
            int Nstatue = PrintStatus.PrinterStatue(print.PrinterSettings.PrinterName);
            PrintSucess = Ostatue != 0 && Nstatue == 0;
            Ostatue = Ostatue == Nstatue ? Ostatue : Nstatue;
            PopTips.Text = PrintStatus.PrinterStatusCodes.ContainsKey(Nstatue) ? PrintStatus.PrinterStatusCodes[Nstatue].ToString() : $"{Nstatue}";
            PrintTimeNo += 1;

            if (PrintSucess||(PrintTimeNo < 20 + PageAllNum))
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
                    Content = new HomePage("页面超时自动返回");
                    Pages();
                }
            });
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
    }
}
