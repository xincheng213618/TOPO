using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;


namespace SimpleWindow
{
    /// <summary>
    /// AboutMsg.xaml 的交互逻辑
    /// </summary>
    public partial class AboutMsg : Window
    {
        public AboutMsg()
        {
            InitializeComponent();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
