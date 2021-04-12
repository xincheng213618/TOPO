using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Serialization;
using System.ComponentModel;
using BaseUtil;

namespace ECRService
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

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);
        }


        private void TimeRun()
        {
            Log.LogInput();// 每个一秒刷新一次日志
            timeLabel.Content = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm");
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (frame.CanGoBack)
            {
                frame.RemoveBackEntry();
            }
        }



        private void Window_Initialized(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new HomePage())));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Dispose();

        }

        private void Window_Activated(object sender, EventArgs e)
        {

        }

        private void Button_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new SettingPage())));
        }
    }

    public class MainWidownData : INotifyPropertyChanged
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
