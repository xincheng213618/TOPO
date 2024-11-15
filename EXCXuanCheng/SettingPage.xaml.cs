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

namespace EXCXuanCheng
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            DataContext = Time;
            Countdown_timer();
        }


        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Border border = sender as Border;
            //border.Background = Brushes.AliceBlue;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Border border = sender as Border;
            //border.Background = Brushes.Transparent;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();


        private void Countdown_timer()
        {

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

        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Foreground = Brushes.HotPink;



            switch ((string)btn.Tag)
            {
                case "Function":

                    FunctionScrollViewer.Visibility = Visibility.Visible;
                    PageScrollViewer.Visibility = Visibility.Hidden;
                    TestScrollViewer.Visibility = Visibility.Hidden;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                case "Page":
                    FunctionScrollViewer.Visibility = Visibility.Hidden;
                    PageScrollViewer.Visibility = Visibility.Visible;
                    TestScrollViewer.Visibility = Visibility.Hidden;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                case "Test":
                    FunctionScrollViewer.Visibility = Visibility.Hidden;
                    PageScrollViewer.Visibility = Visibility.Hidden;
                    TestScrollViewer.Visibility = Visibility.Visible;
                    btn.FontWeight = FontWeights.Bold;
                    break;
                default:
                    break;
            }
        }
        
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Log.Write(Global.Related.PageType);
            Global.Related.PageType = button.Tag.ToString();
            //switch (Global.Related.PageType)
            //{
            //   //
            //}
        }

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {

                case "PDF":
                    Content = new Pdfshow();
                    break;


                default:
                    break;
            }
            Pages();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {

                case "Close":
                    //(Application.Current.MainWindow as MainWindow).Close();
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }
    }
}
