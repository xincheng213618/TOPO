using BaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace XinHua
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AxAcroPDFLib.AxAcroPDF PDFControl = new AxAcroPDFLib.AxAcroPDF();
        public static XinHuaDate Time = new XinHuaDate();
        private Timer timer;
        private int ExitNum = 0;

        public MainWindow()
        {
            InitializeComponent();
            //1S刷新一次
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);
        }
        private void TimeRun()
        {
            Time.Date = DateTime.Now.ToString("MM月dd日 HH:mm:ss");
            Log.LogInput();// 每个一秒刷新一次日志
            if (DateTime.Now.Second == 0)
            {
                ExitNum = 0; //让计数归零，防止用户误触退出程序
            }
            //规定的时间内删除缓存
            if (DateTime.Now.Hour == 14 && DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
            {
                
            }
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            DateLabel.DataContext = Time;
            DataContext = Global.Config;
            Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new HomePage())));
        }
        
        private void Exit(object sender, RoutedEventArgs e)
        {
            ExitNum += 1;
            if (ExitNum == 4)
                MessageBox.Show("再点击一次即将退出程序");
            if (ExitNum > 5)
            {
                Environment.Exit(0);
                //this.Close();
            }
        }

        private void openKeyboard(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.O });
        }

        private void Setting_Click(object sender, MouseButtonEventArgs e)
        {
            if (Global.Related.PageType == null)
                Dispatcher.BeginInvoke(new Action(() => frame.Navigate(new EXCPassword())));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
    public class XinHuaDate : INotifyPropertyChanged
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
