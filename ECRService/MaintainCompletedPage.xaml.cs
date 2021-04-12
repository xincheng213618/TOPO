using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ECRService
{
    /// <summary>
    /// MaintainCompletedPage.xaml 的交互逻辑
    /// </summary>
    public partial class MaintainCompletedPage : Page
    {
        public MaintainCompletedPage()
        {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
        }

        private void PowerOffButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
