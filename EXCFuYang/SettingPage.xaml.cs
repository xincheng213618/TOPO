using BaseDLL;
using BaseUtil;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace XinHua
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Top.DataContext = Time;
            Countdown_timer();
        }

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
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }

        //页面转跳
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }



        //功能切换
        // 2020.11.5 修正
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Close":
                    Environment.Exit(0);
                    break;

                case "WinSpace":
                    User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.SHIFT });
                    break;
                case "CtrlAltO":
                    User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.O });
                    break;

              
                default:
                    break;
            }
        }


    }
}
