using Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
namespace EXC
{
    /// <summary>
    /// QRCode.xaml 的交互逻辑
    /// </summary>
    public partial class QRCodePage : Page
    {
        public QRCodePage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
            Vbarapi.OpenDevice();
            Vbarapi.Backlight(true);
            DecodeThread = new Thread(new ThreadStart(DecodeThreadMethod))
            {
                IsBackground = true
            };
            DecodeThread.Start();
        }


        public static Thread DecodeThread = null;
        public static bool bIsLoop = true;
        private string Msg=null; //存储扫描到的数据


        /// 方法：停止解码线程方法
        private void StopDecodeThread()
        {
            bIsLoop = false;
            if (DecodeThread != null)
            {
                DecodeThread.Abort();
                while (DecodeThread.ThreadState != ThreadState.Aborted)
                {
                    Thread.Sleep(50);
                }
            }
        }

        delegate void ShowRichTextBoxMsgRef(System.Windows.Forms.RichTextBox lpRichTextBox, string lpMsg);

        /// 方法：扫码线程方法
        private void DecodeThreadMethod()
        {
            do
            {
                bool decoderesult = Vbarapi.GetResultStr(out Msg, out int size);

                if (decoderesult && Msg != "")
                {
                    Dispatcher.BeginInvoke(new Action(() => ShowMsg()));
                }
            }
            while (bIsLoop);
        }

        private void ShowMsg()
        {
            ShowText.Text = Msg;
            TitleLabel.Content = "请选择下一步操作";
            FunctionButton.Tag = "Print";
        }


        private void DownPDF(string code)
        {
            try
            {
                //try
                //{
                //    string name = (string)dic["报告编号"];
                //    name = name.Substring(4, 6).Insert(0, "20").Insert(4, "-").Insert(7, "-");
                //    DateTime d1 = Convert.ToDateTime(name);
                //    int a = -d1.Date.Subtract(DateTime.Now.Date).Days;
                //    if (a > Global.configData.QRcodeDate)
                //    {
                //        Content = new HomePage("该二维码已经过期" + (a - Global.configData.QRcodeDate).ToString() + "天");
                //        Pages();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Log.WriteException(ex);
                //    Content = new HomePage("暂未支持此二维码");
                //    Pages();
                //}

                string response = Http.Provincial.LSQRReport(code);
                Dispatcher.BeginInvoke(new Action(() => phrase(response)));

            }
            catch
            {
                Content = new HomePage("接口异常");
                Pages();
            }
        }

        private void phrase(string response )
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
                    XCovert.Base64ToFile(bs64, filePath);

                }
                else
                {
                    Content = new HomePage("未查询到报告，您报告的预约事件可以已超过7天，或选择了他地出局报告，请查实");
                    Pages();
                    return;
                }
            }
            catch
            {
                Content = new HomePage("暂未支持此二维码");
                Pages();
                return;
            }
        }
        

        private void DownPDF_Click(object sender, RoutedEventArgs e)
        { 

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
                case "Print":
                    Content = new PrintTips();
                    Pages();
                    break;
                default:
                    break;
            }
        }

        private void Pages()
        {
            Vbarapi.Backlight(false);
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
    }
}
