using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BaseDLL;
using BaseUtil;

namespace REC
{
    /// <summary>
    /// FunctionPage3.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPage3 : Page
    {
        private AxAcroPDFLib.AxAcroPDF pdfControl = new AxAcroPDFLib.AxAcroPDF();
        private string FileName;

        private int test = 1;
        public FunctionPage3(string FileName)
        {
            this.FileName = FileName;
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {

            ESerialPort.DevMsg += RealEstate_Dev_devMsg;
            WaitShow.DataContext = printDate;

            WaitShow.Visibility = Visibility.Visible;

            pdfControl = new AxAcroPDFLib.AxAcroPDF();
            pdfControl.BeginInit();
            formsHost.Child = pdfControl;
            pdfControl.EndInit();
            printDate.StatusCode = test.ToString() + "正在初始化";
            pdfControl.LoadFile(FileName);    

            OnePrint();
        }

        PrintDate printDate = new PrintDate();

        private async void RealEstate_Dev_devMsg(int Code, string data)
        {
            printDate.StatusCode = test.ToString()+" "+data;
            Log.Write(printDate.StatusCode);
            if (Code == 13)
            {
                await Task.Delay(100);
                printDate.StatusCode = test.ToString()+"打印完成";
                test += 1;
                printDate.StatusCode = test.ToString() +"正在初始化";
                await Dispatcher.BeginInvoke(new Action(() => OnePrint()));

            }
            if (Code ==-1)
            {
                await Dispatcher.BeginInvoke(new Action(() => Home(data)));
            }
        }
        private void Home(string Msg )
        {
            Content = new HomePage(Msg);
            Pages();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;  
            switch (button.Tag)
            {
                case "HomePage":
                    PopAlert("打印期间不允许主动返回，请耐心等待",3);
                    break;
                case "Print":
                    break;
            }
        }
        private async void OnePrint() //按键操作禁用
        {
            ESerialPort.RunStart();
            await Task.Delay(9000);
                
            int i = ESerialPort.CheckDevice1();
            MainWindow.WindowsData.Status1 = "翻页：" + ESerialPort.CheckDeviceCode[i];  


            int j = ESerialPort.CheckDevice2();
            MainWindow.WindowsData.Status2 = "盖章：" + ESerialPort.CheckDeviceCode[j];


            if (i != 0 || j != 0)
            {
                await Dispatcher.BeginInvoke(new Action(() => Home(MainWindow.WindowsData.Status1 + Environment.NewLine + MainWindow.WindowsData.Status2)));
            }
            else
            {
                printDate.StatusCode = test.ToString() + "初始化完成，正在上证";
                pdfControl.printAll();
            }  
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
            pdfControl.Dispose();
        }


    }

    public class PrintDate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string statusCode ;
        public string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusCode")); }
        }



    }


}
