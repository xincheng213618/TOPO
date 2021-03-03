using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ACE;
using BaseDLL;

//Designed By Mr.Xin  2020.7.13  
//Modify  2020.7.17
/*系统优化设计*/

namespace Startup
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        private StartWindow()
        {
            InitializeComponent();
        }
        bool ACEcheck =false;
        StartupGlobal startupGlobal = new StartupGlobal() ;
        Window window;
        public StartWindow(Window windows, StartupGlobal startupGlobal)
        {
            this.startupGlobal = startupGlobal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Topmost = true;
            window = windows;
            InitializeComponent();
        }

        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
                  
        private void Window_Initialized(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(async () => await DetectionhardwareAsync()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag.ToString())
            {
                case "Copy":
                    Clipboard.SetText(MachineCode.Text);
                    break;
                case "Paste":
                    if (Clipboard.ContainsText())
                        ActivationCode.Text = Clipboard.GetText();
                    break;
                case "Activation":
                    if (ACEcheck)
                    {
                        string LicensePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TOPO";
                        if (!Directory.Exists(LicensePath))
                        {
                            Directory.CreateDirectory(LicensePath);
                        }
                        File.WriteAllText(LicensePath + "\\license", ActivationCode.Text);
                        MessageBox.Show("注册成功", "TOPO", MessageBoxButton.OK, MessageBoxImage.Information);
                        Environment.Exit(0);
                    }
                    break;
                case "Login":  
                    break;
                default:
                    break;
            }
        }

        //硬件
        private async Task DetectionhardwareAsync()
        {
            PromptLabel.Content = "正在检测注册情况";
            await Task.Delay(100);
            if (License.Check())
            {
                PromptLabel.Content = "正在检测硬件可用性";
                DetectionBorder.Visibility = Visibility.Visible;
                ActivationBorder.Visibility = Visibility.Hidden;

                PromptLabel.Content = "正在检测摄像头";
                await Task.Delay(100);

                bool b = startupGlobal.CameraTest && AmLivingBodyApi.AmOpenDevice() == 0;
                //打开摄像头要关掉
                if (b)
                    AmLivingBodyApi.AmCloseDevice();

                CameraCode.Source = b ? RightCode.Source : ErrorCode.Source;
                try
                {
                    PromptLabel.Content = "正在检测读卡器";
                    await Task.Delay(500);
                    CardCode.Source = startupGlobal.IDcardTest && IDcard.IDcardSet() != 0 ? RightCode.Source : ErrorCode.Source;
                }
                catch
                {
                    CardCode.Source = ErrorCode.Source;
                }

                PromptLabel.Content = "正在扫检测码器";
                await Task.Delay(500);
                if (startupGlobal.VarbTest)
                {
                    bool Open = VbarAll.Open();

                    StampCode.Source = Open ? RightCode.Source : ErrorCode.Source;
                    //这里配置码值
                    if (Open)
                    {
                        VbarAll.AddSymbolType(7, true);
                        VbarAll.AddSymbolType(1, true);
                        VbarAll.Backlight(false);
                    }
                }
                PromptLabel.Content = CameraCode.Source == RightCode.Source && CardCode.Source == RightCode.Source ? "正在进入系统" : "请检测硬件是否连接完毕";
                await Task.Delay(2000);
                window.Show();
                GetWindow(this).Close();
            }
            else
            {
                PromptLabel.Content = "软件尚未激活，请先激活";
                MachineCode.Text = License.GetMachineCode(); ;
                ActivationBorder.Visibility = Visibility.Visible;
                DetectionBorder.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
            Environment.Exit(0);
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ActivationCodeVerify(object sender, TextChangedEventArgs e)
        {
            ACEcheck = License.Check(ActivationCode.Text);
            Activationinvalid.Visibility = ACEcheck ? Visibility.Hidden : Visibility.Visible;
            ActivationCode.Foreground = ACEcheck ? Brushes.DarkBlue : Brushes.Red;
        }

    }

    public class StartupGlobal
    {
        public bool IDcardTest = false;
        public bool CameraTest = false;
        public bool StampTest = false;
        public bool VarbTest = false;
    }
}
