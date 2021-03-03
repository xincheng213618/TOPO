using Background;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ScreenContorl
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
        public BackgroundWindow backgroundWindow;
        private void Window_Initialized(object sender, EventArgs e)
        {
            string Base = BaseUtil.Covert.FileToBase64(@"C:\Users\Xin\Desktop\1.pdf");

            BackgroundItem.Screens = 0;

            backgroundWindow =new BackgroundWindow();
            backgroundWindow.Show();

            BackgroundItem.Kind = true;
            BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            MessageBox.Show(Directory.GetCurrentDirectory() + "\\Background\\");

            //BackgroundItem.Picture.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            //BackgroundItem.Picture.Auto = true;
            backgroundWindow.Updated();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void comboBoxScreen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
