using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using BaseDLL;
using BaseUtil;

namespace REC
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer;
        public MainWindow()
        {
            //WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);//本来是60，不过没必要刷新这么快，就1s1次就好。
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Log.Write("初始化REC程序：");
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new HomePage())));
            DataContext = WindowsData;
            TechnicaLabel.DataContext = Global.Config;
            TechnicamailLabel.DataContext = Global.Config;
            //打开翻页机
            int i = ESerialPort.Open1(Global.Config.EstatePort1);
            if (i != 0)
                MessageBox.Show("翻页机:"+ ESerialPort.OpenCode[i]);

            Log.Write("翻页机连接端口："+ Global.Config.EstatePort1+Environment.NewLine +"翻页机代码状态："+i +Environment.NewLine +  "翻页机状态："+ ESerialPort.OpenCode[i]);

            //打开盖章机
            int j = ESerialPort.Open2(Global.Config.EstatePort2);
            if (j != 0)
                MessageBox.Show("盖章机:" + ESerialPort.OpenCode[j]);

            Log.Write("盖章机连接端口：" + Global.Config.EstatePort2 + Environment.NewLine + "盖章机代码状态：" + j + Environment.NewLine + "盖章机状态：" + ESerialPort.OpenCode[j] );

        }

        private void TimeRun()
        {
            WindowsData.Date = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");

            Log.LogInput();// 每个一秒刷新一次日志

            //if (DateTime.Now.Hour == 14 && DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)   
        }
        public static RECDate WindowsData = new RECDate();



        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(Global.Related.PageType == "")
                Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new SettingPage())));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Log.Write("软件关闭：" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
            Environment.Exit(0);
        }
    }

    public class RECDate : INotifyPropertyChanged
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

        private string status1;
        public string Status1
        {
            get { return status1; }
            set { status1 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status1")); }
        }
        private string status2;

        public string Status2
        {
            get { return status2; }
            set { status2 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status2")); }
        }
    }
}
