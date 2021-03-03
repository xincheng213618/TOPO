using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace PEC
{
    /// <summary>
    /// QRCode.xaml 的交互逻辑
    /// </summary>
    public partial class QRCode : Page
    {
        public QRCode()
        {

            InitializeComponent();
        }
        private Thread DecodeThread = null;
        private bool bIsLoop = true;

        private string Msg = null; //存储扫描到的数据

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown();
            if (VbarAll.Isconnected())
            {
                VbarAll.Backlight(true);
                DecodeThread = new Thread(new ThreadStart(DecodeThreadMethod))
                {
                    IsBackground = true
                };
                DecodeThread.Start();
            }
            else
            {
                TitleLabel.Content = "扫码器未连接";
            }

        }

        private TimeCount timeCount = new TimeCount() { Countdown = 30 };

        private DispatcherTimer pageTimer = null;

        private void Countdown()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }


        /// 方法：扫码线程方法
        private void DecodeThreadMethod()
        {
            do
            {
                bool decoderesult = VbarAll.GetResultStr(out Msg, out int size);
                if (decoderesult && Msg != "")
                {
                    VbarAll.BeepControl();
                    Dispatcher.BeginInvoke(new Action(() => ShowMsg()));
                    Thread.Sleep(1000);
                }
            }
            while (bIsLoop);
        }


        private void ShowMsg()
        {
            timeCount.Countdown = 30;
            ShowText.Text = Msg;
            FunctionButton.Tag = "Search";
            ButtonLabel.Content = "查询";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Search":
                    string Text;
                    if (DetectMsg(out Text))
                    {
                        pageTimer.IsEnabled = false;  //在网络请求开始的时候禁止计时
                        Thread thread = new Thread(() => Requests(Text))
                        {
                            IsBackground = true
                        };
                        thread.Start();
                    }
                    else
                    {
                        PopAlert("暂未支持此二维码，请检测二维码并重试", 3);
                        TitleLabel.Content = "您可重新扫描二维码";
                        FunctionButton.Tag = "HomePage";
                        ButtonLabel.Content = "返回主页";
                    }
                    break;
            }
        }
        private bool DetectMsg(out string Text)
        {
            try
            {
                string[] Strs = Msg.Replace("。", "").Split('；');
                Dictionary<string, object> dic = new Dictionary<string, object>();
                for (int i = 0; i < Strs.Length; i++)
                {
                    string[] temp = Strs[i].Split('：');
                    dic.Add(temp[0], temp[1]);
                }
                Text = (string)dic["报告编号"];
                return true;
            }
            catch
            {
                Text = "";
                return false;
            }
        }
        private void Requests(string Text)
        {
            string response = Http.Provincial.LSQRReport(Text);
            Dispatcher.BeginInvoke(new Action(() => Phrase(response)));
        }

        private void Phrase(string response)
        {
            if (response != null)
            {
                try
                {
                    JObject Jsonresponse = (JObject)JsonConvert.DeserializeObject(response);
                    string resultCode = Jsonresponse["resultCode"].ToString();
                    string filePath = null;
                    if (resultCode.Equals("1"))
                    {
                        JObject data = (JObject)JsonConvert.DeserializeObject((string)Jsonresponse.GetValue("data"));
                        filePath = "Cache\\" + (string)data.GetValue("bgbh") + ".pdf";
                        string bs64 = (string)data.GetValue("bgwj");
                        if (Covert.Base64ToFile(bs64, filePath))
                        {
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                PopBorder.Visibility = Visibility.Visible;
                            }));
                            AxAcroPDFLib.AxAcroPDF pdfControl = new AxAcroPDFLib.AxAcroPDF();
                            pdfControl.BeginInit();
                            formsHost.Child = pdfControl;
                            pdfControl.EndInit();
                            pdfControl.LoadFile(filePath);
                            pdfControl.printAllFit(true);
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                PopBorder.Visibility = Visibility.Hidden;
                            }));
                        }
                        else
                        {
                            Log.Write(response);
                            Content = new HomePage("报告生成错误，请联系维护人员");
                            Pages();
                        }
                    }
                    else
                    {
                        Content = new HomePage("未查询到信用报告，您报告的预约时间可能已超过7天或选择了他地出具报告，请查实。");
                        Pages();
                    }
                }
                catch
                {
                    Log.Write(response);
                    Content = new HomePage("莱斯报告接口解析异常，请联系维护人员");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("莱斯报告接口连接异常");
                Pages();
            }

        }


        private void Pages()
        {
            bIsLoop = false;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private async void PopAlert(string Msg, int time)
        {
            Log.Write("QRCodePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
    }
}
