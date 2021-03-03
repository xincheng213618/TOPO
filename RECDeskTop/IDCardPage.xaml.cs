using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace REC
{
    /// <summary>
    /// IDCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class IDCardPage : Page
    {
        public static int m_iPort;  //端口
        public static int read_success = -1;  //是否读取到身份证信息
        IDCardData idcardData = new IDCardData();


        public IDCardPage()
        {
            InitializeComponent();
        }

        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount() { Countdown = 25 };

        private void Page_Initialized(object sender, EventArgs e)
        {
            TopGrid.DataContext = timeCount;
            Countdown();
            m_iPort = IDcard.IDcardSet();
        }

        private void Countdown()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1), };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
                if (timeCount.Countdown % 8 == 0)
                {
                    Media.Player(3);
                }
                //程序定义
                if (read_success == 1 || read_success == 0)
                {

                    pageTimer.IsEnabled = false;
                    read_success = -1;
                    Thread.Sleep(1000);//给与时间去看身份证信息的正确与否
                    SwitchPage();
                }
                else
                {
                    if (m_iPort != 0)
                    {
                        IDcard_reader();
                    }
                    else
                    {
                        Content = new HomePage("读卡器没有正确连接");
                        Pages();
                    }
                }
            });
        }
        private void IDcard_reader()
        {
            read_success = IDcard.IDcardRead(m_iPort, ref idcardData);

            if (read_success == 1 || read_success == 0)
            {
                Media.Player(15);//读取成功

                OtherIDcardShow();
                //Function.IDCardDataToImage(idcardData);
            }
        }

        private void OtherIDcardShow()
        {
            //IDcard_info.Visibility = Visibility.Visible;
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
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private void SwitchPage()
        {
            switch (Global.PageType)//可以在这里进行定义判定这里可以直接跳转
            {
                default:
                    Content = new CameraPage(idcardData);
                    break;
            }
            Pages();
        }





    }
}
