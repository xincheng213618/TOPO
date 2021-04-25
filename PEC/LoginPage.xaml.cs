using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace PEC
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        Thread t;
        private void Page_Initialized(object sender, EventArgs e)
        {
            m_iPort = IDcard.IDcardSet();
            t = new Thread(new ThreadStart(IDcard_reader));
             
            timeCount.Countdown = 30;
            Countdown();
        }
        private int m_iPort;

        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount();
        /// <summary>
        /// 读卡    
        /// </summary>
        IDCardData idcard = new IDCardData();
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
        private int read_success = -1;
        private void IDcard_reader()
        {
            while (true)
            {
                read_success = IDcard.IDcardRead(m_iPort, ref idcard);
                if (read_success == 1 || read_success == 0)
                {
                 
                    Media.Player(15);//读取成功
                    DAIDcrdLogin();
                }
                else
                {
                    continue;
                }
            }
        }

        string _user = null;
        string _Password = null;
        private void LoginAction()
        {
            string response = Http.Provincial.DALogin(_user, _Password);
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => throw new Exception("该接口连接错误")));
                return;
            }
        }
        private void Pages()
        {
            t.Abort();
            pageTimer.IsEnabled = false;
            (Application.Current.MainWindow as MainWindow).frame.Navigate(Content);
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            //POP.Visibility = Visibility.Hidden;
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            BorderLogin.Background = Brushes.AntiqueWhite;
            BorderLogin1.Background = Brushes.AntiqueWhite;
            List<Button> List1 = new List<Button>() { ButtonLogin, ButtonLogin1 };
            foreach (var item in List1)
            {
                item.FontSize = 20;
                item.FontWeight = FontWeights.Normal;
            }
            Button button = sender as Button;
            button.FontSize = 22;
            button.FontWeight = FontWeights.Bold;
            switch (button.Name)
            {
                case "ButtonLogin":
                    GridLogin.Visibility = Visibility.Visible;
                    GridLogin1.Visibility = Visibility.Hidden;
                    BorderLogin.Background = Brushes.White;
                    break;
                case "ButtonLogin1":
                    GridLogin1.Visibility = Visibility.Visible;
                    GridLogin.Visibility = Visibility.Hidden;
                    BorderLogin1.Background = Brushes.White;
                    break;
            }
        
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _user = account.Text;
            _Password = password.Text;
            switch (button.Tag)
            {
                case "Login":
                    if (_user.Length > 0 && _Password.Length > 0)
                    {
                        Thread thread = new Thread(() => RequestLogin(_user, _Password))
                        {
                            IsBackground = true
                        };
                        thread.Start();
                    }
                    else
                    {
                        Content = new HomePage("请输入账号或密码");
                        Pages();
                    }
                    break;
                case "Login1":
                    if (TextBoxPhoneNum.Text.Length >6 || TextBoxVertify.Text.Length > 1)
                    {
                        string phonenum = TextBoxPhoneNum.Text;
                        string Vertify = TextBoxVertify.Text;
                        PopBorder.Visibility = Visibility.Visible;
                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            string response = Http.Provincial.DAMsgVerify(phonenum, "2", Vertify);
                            Dispatcher.BeginInvoke(new Action(() => DAMsgVerifyPrase(response)));
                        }));
                        t.Start();
                    }
                    else
                    {
                        Label_Login1Msg.Content = "手机号或者验证码错误";
                        Label_Login1Msg.Visibility = Visibility.Visible;
                    }
                    break;

                default:
                    Content = new HomePage();
                    Pages();
                    break;
            }
        }

        private void DAMsgVerifyPrase(string response)
        {
            try
            {
                JObject Jsonresponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = Jsonresponse["resultCode"].ToString();
                if (resultCode.Equals("1"))
                {
                    JObject data = (JObject)JsonConvert.DeserializeObject(Jsonresponse.GetValue("data").ToString());
                    ReportData.LoginName = (string)data.GetValue("loginname");
                    ReportData.PhoneNum = (string)data.GetValue("mobile");
                    ReportData.Name = (string)data.GetValue("name");
                    ReportData.IDCardNo = (string)data.GetValue("cardid");
                    ReportData.Success = true;
                    Content = new ReportPage();
                    Pages();
                }
                else
                {
                    Content = new HomePage("查不到法人数据");
                    Pages();
                }
            }
            catch (Exception ex)
            {
                Content = new HomePage("手机验证接口解析错误：" + ex.Message);
                Pages();
            }
        }

        private void RequestLogin(string user, string pass)
        {
            string response = Http.Provincial.DALogin(user, pass);
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => throw new Exception("该接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => PhraseLogin(response)));
        }
        private void PhraseLogin(string response)
        {
            if (response != null)
            {
                try
                {
                    JObject jObject = (JObject)JsonConvert.DeserializeObject(response);
                    string resultCode = jObject["resultCode"].ToString();
                    if (resultCode.Equals("1"))
                    {
                        JObject data = (JObject)JsonConvert.DeserializeObject(jObject.GetValue("data").ToString());
                        ReportData.LoginName = (string)data.GetValue("loginname");
                        ReportData.PhoneNum = (string)data.GetValue("mobile");
                        ReportData.Name = (string)data.GetValue("name");
                        ReportData.IDCardNo = (string)data.GetValue("cardid");
                        ReportData.Success = true;
                        IDCardData iDCardData = new IDCardData();
                        iDCardData.Name = ReportData.Name;
                        iDCardData.IDCardNo = ReportData.IDCardNo;
                        Content = new ReportPage(iDCardData, ReportData.LoginName);
                        Pages();
                    }
                    else
                    {
                        Log.Write(response);
                        Content = new HomePage("帐号密码错误");
                        Pages();
                    }
                }
                catch (Exception ex)
                {
                    Dispatcher.BeginInvoke(new Action(() => throw new Exception("：" + ex.Message)));
                    Content = new HomePage("接口解析错误");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("接口链接错误");
                Pages();
            }
        }
        int Request_count = 0;
        private void Vertify_Click(object sender, RoutedEventArgs e)
        {
            string sTEXT = TextBoxPhoneNum.Text;
            Thread thread = new Thread(() => RequestSendMsg(sTEXT))
            {
                IsBackground = true
            };
            thread.Start();
            ((Button)sender).IsEnabled = false;


        }
        private void RequestSendMsg(string phonenum)
        {
            string response = Http.Provincial.DASendMsg(phonenum, "2");
            Dispatcher.BeginInvoke(new Action(() => PhraseSendMsg(response)));
        }

        private void PhraseSendMsg(string response)
        {
            if (response != null)
            {
                try
                {
                    JObject Jsonresponse = (JObject)JsonConvert.DeserializeObject(response);
                    string resultCode = Jsonresponse["resultCode"].ToString();
                    Label_Login1Msg.Content = "短信验证码已发送，可能会有延后请耐心等待";
                    Label_Login1Msg.Visibility = Visibility.Visible;

                }
                catch
                {
                    Content = new HomePage("接口解析错误");
                    Pages();
                }
            }
            else
            {
                Content = new HomePage("接口连接错误");
                Pages();
            }

        }
        private void DAIDcrdLogin()
        {
            string response = Http.Provincial.DAIDcrdLogin(idcard.IDCardNo, "2");

            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("该接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => DAIDcrdLoginPrase(response)));
        }
        private void DAIDcrdLoginPrase(string response)
        {
            try
            {
                JObject Jsonresponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = Jsonresponse["resultCode"].ToString();
                ReportData.Success = true;
                if (resultCode.Equals("1"))
                {
                    JObject data = (JObject)JsonConvert.DeserializeObject(Jsonresponse.GetValue("data").ToString());
                    ReportData.LoginName = (string)data.GetValue("loginname");
                    ReportData.PhoneNum = (string)data.GetValue("mobile");
                    ReportData.Name = (string)data.GetValue("name");
                    ReportData.IDCardNo = (string)data.GetValue("cardid");
                   Content = new ReportPage(idcard, ReportData.LoginName);
                    Pages();
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => HomeMsg("查不到法人数据")));
                }
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("身份证接口解析错误：" + ex.Message)));
            }

        }
        private void HomeMsg(string msg)
        {
            Content = new HomePage(msg);
            Pages();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Content = new IDCardPage();
            Pages();
        }
    }


    public class ReportData
    {
        public static string LoginName = "";
        public static string PhoneNum = "";
        public static string Name = "";
        public static string IDCardNo = "";
        public static bool Success = false;
    }
}
