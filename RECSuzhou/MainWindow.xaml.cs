using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RECSuzhou
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private Timer timer;
        SuZhouDate Time = new SuZhouDate();
        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);//本来是60，不过没必要刷新这么快，就1s1次就好。
        }
        private void TimeRun()
        {
            Time.Date = DateTime.Now.ToString("MM月dd日 HH:mm:ss");
            Log.LogInput();// 每个一秒刷新一次日志

            //规定的时间内删除缓存
            if (DateTime.Now.Hour == 14 && DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
            {

            }
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            if (Global.Config.PstampOpen == 0)
            {
                Stamp.StampPort = Global.Config.PStampPort;
                Stamp.Kind = "PStamp";
            }
            else if (Global.Config.PstampOpen == 1)
            {
                Stamp.StampPort = Global.Config.StampPort;
                Stamp.Kind = "StampPrinter";
            }
            Stamp.OpenDevice();
            DateLabel.DataContext = Time;
            DataContext = Global.Config;
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new HomePage())));
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Opacity = 30;
            //DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
            //BeginAnimation(OpacityProperty, daV);
            //Application.Current.MainWindow = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stamp.Close();
            Environment.Exit(0);
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Global.Config.SettingOptimiz == "0")
            {
                if (Global.PageType == null)
                    Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new EXCPassword())));
            }

        }
    }

    public class SuZhouDate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int countdown = 59;
        public int Countdown
        {
            get { return countdown; }
            set { countdown = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Countdown")); }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date")); }
        }


    }
}
