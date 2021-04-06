using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.InteropServices;
using BaseDLL;

namespace ECRService
{
    public class QuarantineCreditQueryCondition
    {
        public string Institution { get; set; }
        public string Record { get; set; }
    }

    /// <summary>
    /// QuarantineCreditQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class QuarantineCreditQueryPage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        private byte[] keyboardState = new byte[256];

        private Thread worker_PrinterStatus = null;

        public int Countdown
        {
            get { return countdown; }
            set { countdown = value; OnPropertyChanged("Countdown"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        IDCardData iDCardData = new IDCardData();

        public QuarantineCreditQueryPage(IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
            this.DataContext = this;

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }

                if (Countdown % 15 == 0)
                {
                    media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/3、放置身份证.mp3", UriKind.Absolute);
                    media.Position = TimeSpan.Zero;
                    //media.Play();
                }

                //timerLabel.Content = countdown.ToString();
            });
            pageTimer.IsEnabled = true;
        }

        private string CheckPrinterStatus()
        {
            string errorMessage = null;


            return errorMessage;
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            if (institution.Text.Length == 0)
            {
                hintLabel.Content = "请输入统一社会信用代码/组织机构代码！";
                hintBorder.Visibility = Visibility.Visible;
                return;
            }

            if (record.Text.Length == 0)
            {
                hintLabel.Content = "请输入备案号码！";
                hintBorder.Visibility = Visibility.Visible;
                return;
            }

            if (worker_PrinterStatus != null)
            {
                worker_PrinterStatus.Join();
                worker_PrinterStatus = null;
            }

            QuarantineCreditQueryCondition queryCondition = new QuarantineCreditQueryCondition();
            queryCondition.Institution = institution.Text;
            queryCondition.Record = record.Text;

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new QuarantineCreditCertificatePage(this))));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
        }

        private void GetPrinterStatus()
        {
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            FocusManager.SetFocusedElement(this, institution);

            // 启动读取打印机状态工作线程
            worker_PrinterStatus = new Thread(_ => GetPrinterStatus());
            worker_PrinterStatus.Start();
        }

        public void ResetTimer()
        {
            Countdown = TIMEOUT;
            pageTimer.IsEnabled = true;
        }

        private void Institution_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
        }

        private void Record_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
        }

        private void Keyboard_SendKeyPress(char ch)
        {

        }

        private void Keyboard_1_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('1');
        }

        private void Keyboard_2_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('2');
        }

        private void Keyboard_3_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('3');
        }

        private void Keyboard_4_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('4');
        }

        private void Keyboard_5_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('5');
        }

        private void Keyboard_6_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('6');
        }

        private void Keyboard_7_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('7');
        }

        private void Keyboard_8_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('8');
        }

        private void Keyboard_9_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('9');
        }

        private void Keyboard_0_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('0');

        }

        private void Keyboard_Q_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('Q');
        }

        private void Keyboard_W_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('W');
        }

        private void Keyboard_E_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('E');
        }

        private void Keyboard_R_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('R');
        }

        private void Keyboard_T_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('T');
        }

        private void Keyboard_Y_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('Y');
        }

        private void Keyboard_U_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('U');
        }

        private void Keyboard_I_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('I');
        }

        private void Keyboard_O_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('O');
        }

        private void Keyboard_P_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('P');
        }

        private void Keyboard_A_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('A');
        }

        private void Keyboard_S_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('S');
        }

        private void Keyboard_D_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('D');
        }

        private void Keyboard_F_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('F');
        }

        private void Keyboard_G_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('G');
        }

        private void Keyboard_H_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('H');
        }

        private void Keyboard_J_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('J');
        }

        private void Keyboard_K_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('K');
        }

        private void Keyboard_L_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('L');
        }

        private void Keyboard_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox focus = FocusManager.GetFocusedElement(this) as TextBox;

            if (focus != null)
            {
                focus.Text = "";
            }
        }

        private void Keyboard_Z_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('Z');
        }

        private void Keyboard_X_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('X');
        }

        private void Keyboard_C_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('C');
        }

        private void Keyboard_V_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('V');
        }

        private void Keyboard_B_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('B');
        }

        private void Keyboard_N_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('N');
        }

        private void Keyboard_M_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('M');
        }

        private void Keyboard_Backspace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new PrintIndexPage())));
        }
    }
}
