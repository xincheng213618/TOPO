using BaseDLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace PEC
{
    /// <summary>
    /// settingPage.xaml 的交互逻辑
    /// </summary>
    public partial class settingPage : Page
    {
        public settingPage()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string str=((Border)sender).Name;
            IDCardData iDCardData = new IDCardData() { Name = "陈信成", IDCardNo = "222222" };
            switch (str)
            {
                case "Close":
                    (Application.Current.MainWindow as MainWindow).Close();
                    break;
                case "Back":
                    Content = new HomePage();
                    Pages();
                    break; 
                case "TestZireanren":
                    Global.PageType = "ProvincialPeople";
                    Content = new ReportPage(iDCardData);
                    Pages();
                    break; 
                case "TestQiye":
                    Global.PageType = "Provincial";
                    string response = Http.Provincial.DALogin("njcxt", "cxt888888");
                    if (response == null)
                    {
                        Dispatcher.BeginInvoke(new Action(() => throw new Exception("该接口连接错误")));
                        return;
                    }
                    Dispatcher.BeginInvoke(new Action(() => PhraseLogin(response)));
                    break;
            }
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
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textpassword.Password.Equals("admin"))
            {
                password.Visibility = Visibility.Hidden;
                test.Visibility = Visibility.Visible;
            }
            else
            {
                SetTip.Content = "密码错误";
            }
        }

        private void textpassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
