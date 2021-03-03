using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using Syncfusion.Windows.PdfViewer;

namespace EXCSearch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void MainWindows_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "X":
                    Close();
                    break;
                case "-":
                    WindowState = WindowState.Minimized;
                    break;
                default:
                    break;
            }
        }
        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Hidden;
        }
    }
}
