
using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BaseDLL;

//Designed By Mr.Xin
namespace EXC
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        static public int m_iPort;
        static public int read_success = -1;
        private IDCardData idcardData = new IDCardData();
        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(IDCardData IDCardData)
        {
            InitializeComponent();
        }


        private void Page_Initialized(object sender, EventArgs e)
        {
            ReportData.IDCardNo = "";
            ReportData.LoginName = "";
            ReportData.Name = "";
            ReportData.PhoneNum = "";
            ReportData.Success = false;
            Countdown_timer();
            m_iPort = IDcard.IDcardSet();
            //idcardData.IDCardNo = "510603198310096190";
            //Thread worker1 = new Thread(() => DAIDcrdLogin());
            //worker1.Start();
            Dispatcher.BeginInvoke(new Action(() => Media.Player(null, 14)));
        }
        private void HomeMsg(string msg)
        {
            Content = new HomePage(msg);
            Pages();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        private string accountA;
        private string PasswordA;
        private void PassWordLogin_Click(object sender, RoutedEventArgs e)
        {
            if (account.Text == "")
            {
                ErrorLabel.Visibility = Visibility.Visible;
                ErrorLabel.Content = "帐号或密码不能为空";
                return;
            }
            accountA = account.Text;
            PasswordA = Password.Password;
            Thread worker2 = new Thread(() => LoginAction());
            worker2.Start();
        }

        private void LoginAction()
        {
            string response = Http.Provincial.DALogin(accountA, PasswordA);
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("该接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => PassWordLoginPrase(response))); 
        }

        private void PassWordLoginPrase( string response )
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
                    //Content = new ReportShowPage();
                    //Pages();
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => HomeMsg("帐号密码错误")));
                }
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("该接口解析错误：" + ex.Message)));
            }
        }

        private void Login_1_Click(object sender, RoutedEventArgs e)
        {
            //pageTimer.IsEnabled = false;
            FocusManager.SetFocusedElement(this, account);
            Login_2.Visibility = Visibility.Visible;
            Login_3.Visibility = Visibility.Hidden;
            PasswordLogin.Visibility = Visibility.Visible;
            PhoneLogin.Visibility = Visibility.Hidden;

        }

        private void Login_2_Click(object sender, RoutedEventArgs e)
        {
            //pageTimer.IsEnabled = false;

            FocusManager.SetFocusedElement(this, account);

            Login_2.Visibility = Visibility.Hidden;
            Login_3.Visibility = Visibility.Visible;
            PasswordLogin.Visibility = Visibility.Hidden;
            PhoneLogin.Visibility = Visibility.Visible;

        }


        private void IDcard_reader()
        {
            int nRet;
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            byte[] ctmp = new byte[255];
            byte[] szPathP = new byte[260];

            nRet = ReadCardAPI.Syn_OpenPort(m_iPort);

            if (nRet == 0 || nRet == 1)
            {
                DateTime startTime = DateTime.Now;
                nRet = ReadCardAPI.Syn_StartFindIDCard(m_iPort, ref pucIIN, 0);
                nRet = ReadCardAPI.Syn_SelectIDCard(m_iPort, ref pucSN, 0);
                read_success = ReadCardAPI.Syn_ReadFPMsg(m_iPort, 0, ref idcardData, szPathP);
                idcardData.szPath = Encoding.Default.GetString(szPathP).Replace("\0", string.Empty);
                int dTime = (int)(DateTime.Now - startTime).TotalMilliseconds;

                idcardData.Name = idcardData.Name.Trim();
                idcardData.IDCardNo = idcardData.IDCardNo.Trim();
                if (read_success == 1 || read_success == 0)
                {
                    Thread worker1 = new Thread(() => DAIDcrdLogin());
                    worker1.Start();
                }

            }
        }

        private void DAIDcrdLogin()
        {
            string response = Http.Provincial.DAIDcrdLogin(idcardData.IDCardNo, "2");

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
                    //Content = new ReportShowPage();
                    //Pages();
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => HomeMsg("查不到法人数据")));
                }
            }
            catch(Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("身份证接口解析错误："+ex.Message)));
            }

        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public Times Time = new Times();

        private void Countdown_timer()
        {
            this.DataContext = this;
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
                //程序定义
                if (read_success == 1 || read_success == 0)
                {
                    read_success = -1;
                    Global.PageType = "CameraPage";
                    Content = new CameraPage(idcardData);
                    Pages();
                }
                else
                {
                    IDcard_reader();
                }
            });
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private string PhoneNumA;
        private void PhoneVerify_Click(object sender, RoutedEventArgs e)
        {
            if(!XCheck.IsPhoneNumber(PhoneNum.Text))
            {
                ErrorLabel1.Visibility = Visibility.Visible;
                ErrorLabel1.Content = "请输入正确的手机号 ";
                return;
            }
            PhoneNumA = PhoneNum.Text;
            ErrorLabel1.Visibility = Visibility.Visible;
            ErrorLabel1.Content = "短信正在发送，请耐心等待";
            Thread worker3 = new Thread(() => DASendMsg());
            worker3.Start();
        }
        private void DASendMsg()
        {
            string response = Http.Provincial.DASendMsg(PhoneNumA, "2");
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("该接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => DASendMsgPrase(response)));
        }

        private void DASendMsgPrase(string response)
        {
            Log.Write(response);
            try
            {
                JObject Jsonresponse = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = Jsonresponse["resultCode"].ToString();
                ErrorLabel1.Visibility = Visibility.Visible;
                ErrorLabel1.Content = Jsonresponse["resultMsg"].ToString();
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("短信接口解析错误：" + ex.Message)));
            }
        }

        private string VerifyCodeA;
        private bool PhoneAlready=false;
        private void PhoneLogin_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneNum.Text == "")
            {
                ErrorLabel1.Visibility = Visibility.Visible;
                ErrorLabel1.Content = "手机号或验证码未通过 ";
                return;
            }
            PhoneNumA = PhoneNum.Text;
            VerifyCodeA = VerifyCode.Text;
            PhoneAlready = true;
            if (PhoneAlready)
            {
                Thread worker2 = new Thread(() => DAMsgVerify());
                worker2.Start();
            }

        }

        private void DAMsgVerify()
        {
            string response = Http.Provincial.DAMsgVerify(PhoneNumA,"2",VerifyCodeA);

            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("手机验证接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => DAMsgVerifyPrase(response)));
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
                    //ReportData.Success = true;
                    //Content = new ReportShowPage();
                    //Pages();
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => HomeMsg("查不到法人数据")));
                }
                PhoneAlready = false;
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(() => HomeMsg("手机验证接口解析错误：" + ex.Message)));
            }
        }

        private void PhoneNum_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Visibility = Visibility.Hidden;
            ErrorLabel1.Visibility = Visibility.Hidden;
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
