﻿using BaseUtil;
using System;
using System.Collections.Generic;
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

namespace RECSuzhou
{
    /// <summary>
    /// SZMoneyPage.xaml 的交互逻辑
    /// </summary>
    public partial class SZMoneyPage : Page
    {
        public SZMoneyPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();
        }
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
               
                case "SZHQMoney":
                case "SZHQProgress":
                    Global.Related.PageType = button.Tag.ToString();
                    Content = new SearchPage();
                    break;
               
            }
            Pages();

        }
        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();

        private void Countdown_timer()
        {
            this.DataContext = Time;
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
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
