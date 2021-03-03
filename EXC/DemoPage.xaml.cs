using Resources;
using Microsoft.Win32;
using System;
using System.Drawing.Printing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
    
namespace EXC
{
    /// <summary>
    /// DemoPage.xaml 的交互逻辑
    /// </summary>
    public partial class DemoPage : Page
    {
        public DemoPage()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            MainWindow.PDFControl.BeginInit();
            formsHost.Child = MainWindow.PDFControl;
            MainWindow.PDFControl.EndInit();

            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//默认打印机名

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports\" + sDefault, RegistryKeyPermissionCheck.Default, System.Security.AccessControl.RegistryRights.QueryValues);
            if (key != null)
            {
                string IP = (string)key.GetValue("IPAddress", String.Empty, RegistryValueOptions.DoNotExpandEnvironmentNames);
            }
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                comboBox.Items.Add(sPrint);
                if (sPrint == sDefault)
                    comboBox.SelectedIndex = comboBox.Items.IndexOf(sPrint);
            }

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = XFile.OpenFileDialog("所有PDF文件(*.pdf)|*.pdf");
            //MainWindow.pdfControl.setShowScrollbars(false);
            //MainWindow.pdfControl.setShowToolbar(false);
            Dispatcher.Invoke(new Action(() => PrintPDFFile(filePath)));
        }

        //打印PDF文件
        private void PrintPDFFile(string pdfPath)
        {
            MainWindow.PDFControl.LoadFile(pdfPath);
            string msg = Stamp.Start(1);
            if (msg != null)
            {
                Content = new HomePage(msg);
                Pages();
                return;
            }
            MainWindow.PDFControl.printAll();
            //timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(async () => await TimeRunAsync(2))), null,1000);
        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        Timer timer =null;
        int Ostatue = 0;
        bool Print = false;
        int PrintTime = 0;
        private async Task TimeRunAsync(int PageAllNum)
        {
            int Nstatue = Prints.PrinterStatue(Global.PrinterDefaultName);
            Print = Ostatue != 0 && Nstatue == 0;
            Ostatue = Ostatue == Nstatue ? Ostatue : Nstatue;
            PopLabel.Content = Prints.PrinterStatusCodes[Nstatue].ToString();
            PrintTime += 1;
            if (PrintTime > 20)
                Print = true;
            if (Print)
            {
                timer.Dispose();
                PopLabel.Content = "PDF发送到打印机";
                await Task.Delay(2 * 1000);
                PopLabel.Content = "打印机正在打印";
                await Task.Delay(PageAllNum * 1000);
                PopLabel.Content = "打印完毕";

                Dispatcher.Invoke(new Action(() => Media.Player(null, 7)));
                await Task.Delay(5 * 1000);

                PopLabel.Content = "";
                Print = false;
                Ostatue = 0;

            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Print.PrinterStatusCodes[Print.PrinterStatue(comboBox.Text)].ToString(), "EXC", MessageBoxButton.OK);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
