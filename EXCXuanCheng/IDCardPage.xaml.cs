using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace EXCXuanCheng
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
            DataContext = Time;
            Countdown_timer();
            m_iPort = IDcard.IDcardSet();

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
                            Thread.Sleep(1000);//给与时间去看身份证信息的正确与否
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
            switch (Global.PageType)
            {
                case "":
                
                default:
                    Content = new CameraPage(idcardData);
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
