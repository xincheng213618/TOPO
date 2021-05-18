using ACE;
using EXCResources;
using EXCResources2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SingleVerification
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        //Designed By Mr.xin Revision 2020.5.14
        public StartWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            string Code = License.GetMachineCode();

            if (!File.Exists("license") || !License.Check(File.ReadAllText("license")))
            {
                PromptLabel.Content = "软件尚未激活，请先激活";
                MachineCode.Text = Code;
                ActivationBorder.Visibility = Visibility.Visible;
            }
            else
            {
                PromptLabel.Content = "正在检测硬件可用性";
                DetectionBorder.Visibility = Visibility.Visible;
                Thread worker = new Thread(() => Dispatcher.BeginInvoke(new Action(() => Detectionhardware())));
                worker.IsBackground = true;
                worker.Start();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag.ToString())
            {
                case "Copy":
                    try
                    {
                        Clipboard.SetText(MachineCode.Text);
                    }
                    catch
                    {
                        User32dll.KeyHelper.HotKey(new List<Byte>() { User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.C });
                    }
                    break;
                case "Paste":
                    try
                    {
                        if (Clipboard.ContainsText())
                            ActivationCode.Text = Clipboard.GetText();
                    }
                    catch
                    {
                        User32dll.KeyHelper.HotKey(new List<Byte>() { User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.V });
                    }
                    break;
                case "Activation":
                    if (License.Check(ActivationCode.Text))
                    {
                        File.WriteAllText("license", ActivationCode.Text);
                        Application.Current.Shutdown();
                    }
                    break;
                case "Login":
                    Login();
                    break;
                default:
                    break;
            }
        }

        //硬件
        private void Detectionhardware()
        {
            bool camerrun = AmLivingBodyApi.AmOpenDevice()==0;
            CameraCode.Source = camerrun ? RightCode.Source : ErrorCode.Source;

            CardCode.Source = IDcard.IDcardSet(false) == 0 ? ErrorCode.Source : RightCode.Source;

            StartLoginButton.Visibility = Visibility.Visible;
            PromptLabel.Content = CameraCode.Source == RightCode.Source && CardCode.Source == RightCode.Source ? "正在进入系统" : "请检测硬件是否连接完毕";

            if (camerrun)
            {
                AmLivingBodyApi.AmCloseDevice();
            }

            Thread worker1 = new Thread(() => Temp())
            {
                IsBackground = true
            };
            worker1.Start();
        }

        public void Temp()
        {
            Thread.Sleep(1000);
            Dispatcher.BeginInvoke(new Action(() => Login()));
        }

        private void Login()
        {
            Window window = new MainWindow();  
            window.Show();

            GetWindow(this).Close();
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

        private void ActivationCodeVerify(object sender, TextChangedEventArgs e)
        {
            bool check = License.Check(ActivationCode.Text);
            Activationinvalid.Visibility = check ? Visibility.Hidden : Visibility.Visible;
            ActivationCode.Foreground = check ? Brushes.DarkBlue : Brushes.Red;
        }
    }
}
