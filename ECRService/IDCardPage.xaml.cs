using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;
using System.Threading;

namespace ECRService
{
    /// <summary>
    /// IDCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class IDCardPage : Page
    {
        public IDCardPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static int m_iPort;  //端口
        public static int read_success = -1;  //是否读取到身份证信息
        IDCardData idcardData = new IDCardData();

        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount() { Countdown = 25 };

        private void Page_Initialized(object sender, EventArgs e)
        {
            m_iPort = IDcard.IDcardSet();
            this.DataContext = timeCount;
            Countdown();
        }

        //逻辑修正
        private void Countdown()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1), };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (m_iPort != 0)
                {
                    if (--timeCount.Countdown > 0)
                    {
                        if (timeCount.Countdown % 8 == 0)
                        {
                            Media.Player(3);
                        }
                        //程序定义
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


        private void IDcard_reader()
        {
            read_success = IDcard.IDcardRead(m_iPort, ref idcardData);

            if (read_success == 1 || read_success == 0)
            {
                Media.Player(15);//读取成功
                idcardData.Name = idcardData.Name.Trim();
                idcardData.IDCardNo = idcardData.IDCardNo.Trim();
                name.Content = "*" + idcardData.Name.Substring(1);
                cardNo.Content = idcardData.IDCardNo.Substring(0, 10) + "******" + idcardData.IDCardNo.Substring(16);
                idcardPicture.Source = Covert.FileToImage(idcardData.PhotoFileName);
                sex.Content = idcardData.Sex;
                bir.Content = idcardData.Born;
                placesOfIssue.Content = idcardData.GrantDept;
                validDate.Content = idcardData.UserLifeBegin + " - " + idcardData.UserLifeEnd;
            }
        }


        private void SwitchPage()
        {
            switch (Global.PageType)//可以在这里进行定义判定这里可以直接跳转
            {
                default:
                    Content = new CameraPage(idcardData);
                    Pages();
                    break;
            }
        }

        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
