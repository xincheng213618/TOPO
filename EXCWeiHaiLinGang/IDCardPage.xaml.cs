using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Threading;
using BaseDLL;
using BaseUtil;

namespace EXC
{

    /// <summary>
    /// IDCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class IDCardPage : Page
    {
        private int m_iPort;
        private int read_success = -1;
        IDCardData idcardData = new IDCardData();

        public IDCardPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            m_iPort = IDcard.IDcardSet();

            DataContext = Time;
            Countdown_timer();
        }

        private void IDcard_reader()    
        {
            read_success=IDcard.IDcardRead(m_iPort, ref idcardData);

            if (read_success == 1 || read_success == 0)
            {
                Media.Player(15);//读取成功
                //Function.IDCardDataToImage(idcardData);
                IDcardPicture.Visibility = Visibility.Hidden;
                IDcard_info.Visibility = Visibility.Visible;


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


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount() { Countdown = 60 };
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
                    Global.HomeErrorText = "读卡器没有正确连接";
                    Content = new HomePage();
                    Pages();
                }
            });
        }

        private void SwitchPage()
        {
            switch (Global.PageType)
            {
          
           
                case "NoHome":
                    //CSQLite.Insert.WriteIDCardData(idcardData);
                    //IDcard.DeleteIDcardImages(idcardData);
                    //Content = new NoHomePages(idcardData);
                    //break;
                    
                default:
                    Global.iDCard = idcardData;
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
