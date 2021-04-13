using BaseDLL;
using BaseUtil;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace XinHua
{

    //2020.11.5 身份证读卡页面代码微调
    /// <summary>
    /// IDCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class IDCardPage : Page
    {
        ///// 端口号
        public static int m_iPort;
        public static int read_success = -1;
        
        public IDCardPage()
        {
            InitializeComponent();
        }

        TimeCount Time = new TimeCount();

        private void Page_Initialized(object sender, EventArgs e)
        {
            TOP.DataContext = Time;
            Countdown_timer();

            m_iPort = IDcard.IDcardSet();
        }

        private void IDcard_reader()
        {
            read_success = IDcard.IDcardRead(m_iPort, ref Global.Related.IDCardData);

            if (read_success == 1 || read_success == 0)
            {
                Media.Player( 15);//读取成功

                Global.Related.IDCardData.Name = Global.Related.IDCardData.Name.Trim();
                Global.Related.IDCardData.IDCardNo = Global.Related.IDCardData.IDCardNo.Trim();
                searchTitle.Content = "请检查身份证信息";
                IDcardPicture.Visibility = Visibility.Hidden;
                ShowIDcardData.Visibility = Visibility.Visible;
                name.Content = "*" + Global.Related.IDCardData.Name.Substring(1);
                cardNo.Content = Global.Related.IDCardData.IDCardNo.Substring(0, 10) + "******" + Global.Related.IDCardData.IDCardNo.Substring(16);
                idcardPicture.Source = Covert.FileToImage(Global.Related.IDCardData.PhotoFileName);
                sex.Content = Global.Related.IDCardData.Sex;
                bir.Content = Global.Related.IDCardData.Born;
                placesOfIssue.Content = Global.Related.IDCardData.GrantDept;
                validDate.Content = Global.Related.IDCardData.UserLifeBegin + " - " + Global.Related.IDCardData.UserLifeEnd;

            }
        }


        //倒计时
        DispatcherTimer pageTimer ;
        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true,  Interval = TimeSpan.FromSeconds(1)};
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (m_iPort != 0)
                {
                    if (--Time.Countdown > 0) //这里进行修正，因为原来的写法有点问题+
                    {
                        if (Time.Countdown % 8 == 0)
                        {
                            Media.Player(3);
                        }
                        if (read_success == 1 || read_success == 0)
                        {
                            pageTimer.IsEnabled = false;
                            read_success = -1;
                            AmLivingBodyApi.AmOpenDevice();
                            SwitchPage();
                        }
                        else
                        {
                            IDcard_reader();
                        }
                    }
                    else
                    {
                        Content = new HomePage();
                        Pages();
                    }
                }
                else
                {
                    Content = new HomePage("读卡器没有正确连接");
                    Pages();
                }
            });
        }

        private void SwitchPage()
        {
            switch (Global.Related.PageType)
            {
                default:
                    Content = new CameraPage();
                    Pages();
                    break;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;
                default:
                    break;
            }
        }

        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
    }
}
