using BaseDLL;
using BaseUtil;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;


namespace PEC
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
        public static Frame frames = null;

        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);//本来是60，不过没必要刷新这么快，就1s1次就好。
        }

        private void TimeRun()
        {
            data.Date = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            Log.LogInput();// 每个一秒刷新一次日志
            if (DateTime.Now.Hour == 14 && DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
            {

            }
        }
        private PECDate data = new PECDate();
        private void Window_Initialized(object sender, EventArgs e)
        {
            DataContext = Global.Config;
            timeLabel.DataContext = data;
            LabelTec.Content = Global.Config.Technica;
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new HomePage())));
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }



        private void Setting_Click(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new settingPage())));
        }

    }
    /// </summary>
    public class TimeCount : INotifyPropertyChanged
    {
        /// <summary>
        /// asdas
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int countdown = 59;
        /// <summary>
        /// 
        /// </summary>
        public int Countdown
        {
            get { return countdown; }
            set
            {
                countdown = value;
                if (PropertyChanged != null)
                {
                    Show = Countdown.ToString() + "秒后自动返回";
                    PropertyChanged(this, new PropertyChangedEventArgs("Countdown"));
                }

            }

        }

        private string show;
        public string Show
        {
            get { return show; }
            set { show = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Show")); }
        }

    }
    public class PECDate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date")); }
        }
    }
}
