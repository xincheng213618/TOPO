﻿using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RECSuzhou
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        public HomePage(string Msg)
        {
            InitializeComponent();
            PopAlert(Msg, 3);
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            Global.PageType = null;
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;


            Global.PageType = null;
            Global.IDCardInfo = new SaveInfo();
        }


        private async void PopAlert(string Msg, int time)
        {
            Log.Write("HomePagePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }


        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }


        private IDCardData IDCardData;
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Log.Write(Global.PageType);
            Global.PageType = button.Tag.ToString();
            switch (Global.PageType)
            {
                case "NoHome":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "OwnerShipPages":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "HomeCountPages":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "NoHomeChild":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZWZArchivePages":
                case "SZHQArchivePages":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZMoneyPages":
                    Content = new SZMoneyPage();
                    Pages();
                    break;
                default:
                    break;
            }
        }

        private void TestButton_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Log.Write(Global.PageType);
            Global.PageType = button.Tag.ToString();
            switch (Global.PageType)
            {
                case "NoHome":
                    IDCardData = new IDCardData { Name = "陈信成", IDCardNo = "320323199712213618" };
                    Content = new NoHomePages(IDCardData);
                    Pages();
                    break;
                case "OwnerShipPages":
                    IDCardData = new IDCardData { Name = "王留英", IDCardNo = "320502196312122544" };
                    Content = new OwnerShipPages(IDCardData);
                    Pages();
                    break;
                case "HomeCountPages":
                    IDCardData = new IDCardData { Name = "奚玉远", IDCardNo = "152322198703291816" };
                    Content = new HomeCountPages(IDCardData);
                    Pages();
                    break;
                case "NoHomeChild":
                    Content = new IDcardInputPage();
                    Pages();
                    break;
                case "SZHQArchivePages":
                    IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "140108198708253219" };
                    Content = new SZArchivePage(IDCardData);
                    Pages();
                    break;
                case "SZWZArchivePages":
                    IDCardData = new IDCardData { Name = "杨洋", IDCardNo = "140108198708253219" };
                    Content = new SZArchivePage(IDCardData);
                    Pages();
                    break;
                case "SZMoneyPages":
                    Content = new SZMoneyPage();
                    Pages();
                    break;
                default:
                    break;
            }
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
    }
}