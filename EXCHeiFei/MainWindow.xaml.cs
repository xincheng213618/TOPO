using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using BaseUtil;

namespace EXC
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
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
        private EXCDate data = new EXCDate();

        private void Window_Initialized(object sender, EventArgs e)
        {
            DataContext = Global.Config;
            timeLabel.DataContext = data;
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new HomePage())));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }



        private void Setting_Click(object sender, MouseButtonEventArgs e)
        {
        }

        private void KeyBoardOpen(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.O });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
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
