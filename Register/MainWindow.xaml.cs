using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using ACE;

namespace Register
{
    //2020.7.31 更新图标
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            MachineCode.Text = License.GetMachineCode();
        }

        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
                case "Copy":
                    Clipboard.SetText(ActivationCode.Text);
                    MessageBox.Show("复制成功", "TOPO", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "Paste":
                    if (Clipboard.ContainsText())
                    {
                        MachineCode.Text = Clipboard.GetText();
                        MessageBox.Show("粘贴成功", "TOPO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                case "Activation":
                    string LicensePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TOPO";
                    if (!Directory.Exists(LicensePath))
                    {
                        Directory.CreateDirectory(LicensePath);
                    }
                    File.WriteAllText(LicensePath+ "\\license", ActivationCode.Text);
                    MessageBox.Show("注册成功", "TOPO", MessageBoxButton.OK,MessageBoxImage.Information);
                    break;
                default:
                    break;
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
            Environment.Exit(0);
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MachineCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivationCode.Text = License.Create(MachineCode.Text);
        }

    }
}
