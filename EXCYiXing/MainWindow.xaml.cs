using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using BaseDLL;
using BaseUtil;
using System.ComponentModel;

namespace EXCYiXing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AxAcroPDFLib.AxAcroPDF PDFControl = new AxAcroPDFLib.AxAcroPDF();

        private Timer timer;

        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            //DragMove();
        }

        public MainWindow()
        {
            //WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);//本来是60，不过没必要刷新这么快，就1s1次就好。
        }

        private void TimeRun()
        {
            DateDate.Date = DateTime.Now.ToString("MM月dd日 HH:mm:ss");
            Log.LogInput();// 每个一秒刷新一次日志
            if (DateTime.Now.Second == 0)
            {
                ExitNum = 0;
            }

        }
        public static EXCDate DateDate = new EXCDate();
        //初始化
        private void Window_Initialized(object sender, EventArgs e)
        {
            Vbarapi.CloseDevice();
            if (Global.configData.PstampOpen == 0)
            {
                Stamp.StampPort = Global.configData.PStampPort;
                Stamp.Kind = "PStamp";

            }
            else if (Global.configData.PstampOpen == 1)
            {
                Stamp.StampPort = Global.configData.StampPort;
                Stamp.Kind = "StampPrinter";
            }
            int i = Stamp.OpenDevice();
            Log.Write("盖章机初始化：" + i.ToString());

            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new HomePage())));

            DataContext = Global.configData;
            TimeLabel.DataContext = DateDate;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stamp.Close();
            App.taskbar.Dispose();
            Environment.Exit(0);
        }
        private int ExitNum = 0;
        private void Setting_Click(object sender, MouseButtonEventArgs e)
        {
            if (Global.configData.SettingOptimiz == "0")
                if (Global.Related.PageType == "")
                    Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new EXCPassword())));
        }
        private void KeyBoardOpen(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.O });
        }
        //淡入
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = 30;
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
            BeginAnimation(OpacityProperty, daV);
            Application.Current.MainWindow = this;
        }
    }
    public class EXCDate : INotifyPropertyChanged
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
