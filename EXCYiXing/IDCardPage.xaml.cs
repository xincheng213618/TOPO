using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Threading;
using BaseDLL;
using BaseUtil;

namespace EXCYiXing
{

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

        private void Page_Initialized(object sender, EventArgs e)
        {
            IDcardContent.Content = Global.Related.PageType != "NoHomeChild" ? IDcardContent.Content : "请放入监护人身份证";
            DataContext = Time;
            Countdown_timer();
            m_iPort = IDcard.IDcardSet();

        }


        private void IDcard_reader()    
        {
            read_success=IDcard.IDcardRead(m_iPort, ref Global.Related.IDCardData);

            if (read_success == 1 || read_success == 0)
            {
                Media.Player(15);//读取成功

                OtherIDcardShow();
            
                //Function.IDCardDataToImage(idcardData);
            }
        }

        private void OtherIDcardShow()
        {
            ReturnButton.Visibility = Visibility.Hidden;//此时拒绝返回操作
            IDcardPicture.Visibility = Visibility.Hidden;
            IDcard_info.Visibility = Visibility.Visible;


            Global.Related.IDCardData.Name = Global.Related.IDCardData.Name.Trim();
            Global.Related.IDCardData.IDCardNo = Global.Related.IDCardData.IDCardNo.Trim();
            name.Content = "*" + Global.Related.IDCardData.Name.Substring(1);
            cardNo.Content = Global.Related.IDCardData.IDCardNo.Substring(0, 10) + "******" + Global.Related.IDCardData.IDCardNo.Substring(16);
            idcardPicture.Source = Covert.FileToImage(Global.Related.IDCardData.PhotoFileName);
            sex.Content = Global.Related.IDCardData.Sex;
            bir.Content = Global.Related.IDCardData.Born;  
            placesOfIssue.Content = Global.Related.IDCardData.GrantDept;
            validDate.Content = Global.Related.IDCardData.UserLifeBegin + " - " + Global.Related.IDCardData.UserLifeEnd;
        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();
        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (m_iPort != 0)
                {
                    if (--Time.Countdown > 0)
                    {
                        if (Time.Countdown % 8 == 0)
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

        private void SwitchPage()
        {
            // 2020/12/25  因为个人要三个接口，就在这赋值到时候好取
            switch (Global.Related.PageType)
            {
                default:
                    Content = new CameraPage();
                    break;
            }
            Pages();
        }
        //
        private void Pages()  
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();      
        }
    }
}
