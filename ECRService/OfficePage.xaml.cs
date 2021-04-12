using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace ECRService
{
    /// <summary>
    /// OfficePage.xaml 的交互逻辑
    /// </summary>
    public partial class OfficePage : Page, INotifyPropertyChanged
    {

        //秒表倒计时字段
        private const int TIMEOUT = 60;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        public int Countdown
        {
            get { return countdown; }
            set { countdown = value; OnPropertyChanged("Countdown"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public OfficePage()
        {
            InitializeComponent();
            //通过Context传值到前台,显示秒数,实现实时变动
            this.DataContext = this;
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                //时间倒计时
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;

                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }
                //timerLabel.Content = countdown.ToString();
            });
            pageTimer.IsEnabled = true;
        }
        private void CaizhengjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            //lambad表达式:跳转窗口
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            //值存入Application中，别的窗口根据这个判断
            Application.Current.Properties["aa"] = "Caizhengju";
        }

        private void ChengguanjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Chengguanju";
        }

        private void DanganjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Danganju";
        }

        private void FagaijuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Fagaiju";
        }

        private void GuihuafenjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Guihuafenju";
        }

        private void GuoshuifenjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Guoshuifenju";
        }

        private void GuotufenjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Guotufenju";
        }

        private void HuanbaojuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Huanbaoju";
        }

        private void JiaoyunjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage(),button9.Tag)));
            Application.Current.Properties["aa"] = "Jiaoyunju";

        }

        private void MinzhengjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Minzhengju";
        }

        private void RenfangbanIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Renfangban";
        }

        private void RenshejuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Rensheju";
        }

        private void ShangwujuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Shangwuju";
        }

        private void ShijianguanIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Shijianguan";
        }

        private void ShuiwujuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Shuiwuju";
        }

        private void SifajuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Sifaju";
        }

        private void WeijijuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Weijiju";
        }

        private void WenlvjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Wenlvju";
        }

        private void WujiajuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Wujiaju";
        }

        private void YaojianjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Yaojianju";
        }

        private void ZhijianjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Zhijianju";
        }

        private void ZhujianjuIndex_Click(object sender, RoutedEventArgs e)
        {
            //定时器关闭
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
            Application.Current.Properties["aa"] = "Zhujianju";
        }

        private void Page_Initialized(object sender, EventArgs e)
        {

        }

        private void BackIndex_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
