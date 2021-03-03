using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BaseDLL;
using BaseUtil;

namespace REC
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



        Estate estate = new Estate();
        private void Page_Initialized(object sender, EventArgs e)
        {
            Global.PageType = null;
            string text = System.IO.File.ReadAllText("Base\\打证须知.txt");
            DisclaimerContent.Text = text;
            VbarAll.Backlight(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
            {
           
               Global.PageType = "Self";

            Content = new FunctionPage3();
            Pages();
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }

        private async void PopAlert(string Msg, int time)
        {
            Log.Write("HomePagePOP:" + Msg);

            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
    }
}
