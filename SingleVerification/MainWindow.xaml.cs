using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;
using Spire.Pdf;

namespace SingleVerification
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //设计上有只读卡和更新两种模式，目前模式取消
        public static SingleGlobal singleGlobal = new SingleGlobal();
        IDCardData idcardData = new IDCardData();

        public int Num = 0;
        public int NumRight = 0;

        bool Single = false;
       
        public static int read_success = -1;//身份证是否 读取成功
        public static int m_iPort;  

        public bool CameraRun = false;//是否处于摄像头的模式
        private static int ShootSucess = 1;//是否拍摄成功

        //判断硬件是否可行
        private bool CanCamera = false;
        private bool CanIDcard = false;
        //判断摄像头是否正确连接
        private bool CameraOpen = false; //摄像头功能是否启动
        private bool IDcardOpen = false; //读卡功能是否启动

        //是否自动
        private bool Auto = false; //自动读卡
        private bool AutoAll = false; //全自动

        private bool OneRun = false;


        private Timer timer;
        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => TimeRun())), null, 0, 1000);
        }

        bool LogEndAble = true;
        private void TimeRun()
        {
            singleGlobal.Date = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
            singleGlobal.NumLabel = $"共计{Num}人使用,人脸对比通过{NumRight}人次,失败{Num - NumRight}人次";

            if (LogEndAble)
                LogText.ScrollToEnd();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = 30;
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
            BeginAnimation(OpacityProperty, daV);
            Application.Current.MainWindow = this;
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private BrushConverter Use1 = new BrushConverter();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!CanIDcard)
            {
                singleGlobal.Msg = CanCamera? "读卡器没有正常连接，正在尝试重新连接！" : "读卡器和摄像头都没有正常连接，请检查硬件后重试";
                IDcard_Initialized();
                return;
            }

            Button button = sender as Button;
            switch ((string)button.Tag)
            {
                case "One":
                    if (!AutoAll)
                    {
                        if (CameraOpen)
                        {
                            if (!OneRun)
                            {
                                OneRun = true;
                                Auto = true;
                                IDCardRun();
                            }

                        }
                        else
                        {
                            singleGlobal.Msg = "请先打开摄像头";
                        }
                    }
                    else
                    {
                        singleGlobal.Msg = Single ? "请关闭自动读卡" : AutoAll ? "请先关闭自动对比" : "正在进行单次对比";
                    }
                    break;
                case "Auto":
                    if (CameraOpen)
                    {
                        AllButtonBorder.Background = AutoAll ? (Brush)Use1.ConvertFrom("#1473e6") : Brushes.Red;
                        Auto = true;
                        AutoAll = !AutoAll;
                        pageTimer.IsEnabled = AutoAll;
                        singleGlobal.Msg = Single ? "请关闭自动读卡" : AutoAll ? "开启自动对比" : "正在关闭自动对比";
                    }
                    else
                    {
                        singleGlobal.Msg = "请先打开摄像头";
                    }

                    break;
                case "IDCardOne":
                    Auto = false;
                    CameraOpen = false;
                    singleGlobal.Msg = "正在读取身份证";
                    IDCardRun();
                    break;
                case "IDCardAuto":
                    Auto = true;
                    CameraOpen = false;
                    singleGlobal.Msg = "身份证自动读取开启";
                    break;
                default:
                    singleGlobal.Msg = "程序出现了未知Tag";
                    break;
            }
        }

        private async void Alert(int code, double time = 4)
        {
            PopBorder.Visibility = Visibility.Visible;
            ErrorShow.Visibility = code != 0 ? Visibility.Visible : Visibility.Hidden;
            RightShow.Visibility = code == 0 ? Visibility.Visible : Visibility.Hidden;

            await Task.Delay(TimeSpan.FromSeconds(time));
            PopBorder.Visibility = Visibility.Hidden;

            pageTimer.IsEnabled = AutoAll; //循环是否继续进行
            CameraRun = false; //人证核验环节结束
            //清空姓名和身份证号
            singleGlobal.Name = null;  
            singleGlobal.IDcardNo = null;
        }

        //初始化
        private void Window_Initialized(object sender, EventArgs e)
        {
            DataContext = singleGlobal;
            Countdown_timer();
            pageTimer.IsEnabled = false;

            IDcard_Initialized();
            Camer_Initialized();
        }
        private void IDcard_Initialized()
        {
            m_iPort = IDcard.IDcardSet();
            singleGlobal.Msg = m_iPort == 0 ? "读卡器连接失败" : $"读卡器连接正常 端口为{m_iPort}";
            CanIDcard = m_iPort != 0;
            IDcardOpen = CanIDcard;
        }
        private void Camer_Initialized()
        {
            int i = AmLivingBodyApi.AmOpenDevice();
            singleGlobal.Msg = i != 0 ? $"摄像头打开错误：{AmLivingBodyApi.Ecode[i]}" : "摄像头DLL正常加载";

            if (i == 0)
            {
                formhost.Width = 500;
                formhost.Height = 400;
                AmLivingBodyApi.AmSetVideoWindowHandle(picturebox.Handle, 0, 0, 640, 480);
                AmLivingBodyApi.AmSetCaptureImageCallback(capture_image_callback, IntPtr.Zero);
                CameraOpen = true;
            }
            
            CanCamera = i == 0;
        }






        //倒计时模块
        private DispatcherTimer pageTimer = null;

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromTicks(500),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {

                if (!CameraRun)//人证核验环节没有正在运行
                {
                    if (read_success == 1 || read_success == 0)
                    {
                        read_success = -1;
                        singleGlobal.Msg = "已经抓拍到人脸正在进行比对";
                        CameraRun = true;
                    }
                    else
                    {
                        IDCardRun();
                    }
                }
                else //人证核验环节
                {
                    if (ShootSucess==0)
                    {
                        singleGlobal.Msg = "已经抓拍到人脸正在进行比对";
                        pageTimer.IsEnabled = false;
                        Comparison();
                    }
                    else if (ShootSucess > 30)
                    {
                        ShootSucess = 1;
                        singleGlobal.Msg = "人脸捕捉超时,请对准摄像头并重新操作";
                        OneRun = false;
                        pageTimer.IsEnabled = false;
                    }
                    else
                    {
                        CapturePhoto();
                    }
                }
            });
        }
        private string paths = Directory.GetCurrentDirectory() + "\\capture.jpg";
        private string paths_black = Directory.GetCurrentDirectory() + "\\capture_1.jpg";
        private double b, c;

        private void Comparison()
        {
            ShootSucess = 1;

            b = AmLivingBodyApi.AmVerify(idcardData.PhotoFileName, paths);
            c = AmLivingBodyApi.AmVerify(idcardData.PhotoFileName, paths_black);
            //插入数据  
            CSQLite.Insert.WriteIDCardData(idcardData, paths, paths_black, b);

            image1 = Covert.FileToImage(idcardData.PhotoFileName);
            image2 = Covert.FileToImage(paths);

            PrintBorder.Background = Brushes.Red;

            //处理完成之后删除文件
            File.Delete(paths);
            File.Delete(paths_black);
            File.Delete(idcardData.PhotoFileName);
            if(idcardData.szPath.Length !=0 )
            {
                File.Delete(idcardData.szPath);
            }
            

            singleGlobal.Msg = b.ToString();
            singleGlobal.Msg = b < 0 ? $" {AmLivingBodyApi.Ecode[(int)b]}" : "人脸相似度" + b;

            OneRun = false;
            if (b > 0.9 && c > 0.9)
            {
                NumRight += 1;
                Dispatcher.BeginInvoke(new Action(() => Alert(0)));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() => Alert(1)));
            }
        }

        //摄像头
        private void CapturePhoto()
        {
            ShootSucess += 1;
            AmLivingBodyApi.AmCaptureImage(Directory.GetCurrentDirectory() + $"\\capture.jpg", 3000);
        }
        AmLivingBodyApi.AmCaptureImageProc capture_image_callback = new AmLivingBodyApi.AmCaptureImageProc(OnCaptureImage);
        private unsafe static void OnCaptureImage(int code, string image, IntPtr data)
        {
            ShootSucess = code == 0 ? 0 : ShootSucess;
        }

        private void Camer_Click(object sender, EventArgs e)
        {
            if (CanCamera)
            {
                singleGlobal.Msg = CameraOpen ? "正在关闭摄像头功能" : "正在启动摄像头";

                if (CameraOpen)
                {
                    AmLivingBodyApi.AmCloseDevice();
                    AmLivingBodyApi.AmStopCapture();
                    CameraOpen = false;
                }
                else
                {
                    AmLivingBodyApi.AmOpenDevice();
                    CameraOpen = true;
                }
            }
            else
            {
                singleGlobal.Msg = "摄像头没有正确连接，正在尝试初始化,初始化提示成功后在进行连接";
                Camer_Initialized();
            }

        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            LogEndAble = false;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            LogEndAble = true;
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            singleGlobal.Log = null;
        }

        private void LogText_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            singleGlobal.Log = null;
        }

        private void Database_Click(object sender, RoutedEventArgs e)
        {
            SQLWindow window = new SQLWindow();
            window.Show();
        }
        private BitmapImage image1;
        private BitmapImage image2;
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (PrintBorder.Background == Brushes.Red)//判断pdf 生成
            {
                PrintBorder.Background = Brushes.Gray;
                string FilePath = "Temp_pdf.pdf";
                PDF.DrawPDF(FilePath, idcardData, image1, image2);
                PDF.PDFMark(FilePath, "苏州市不动产登记中心虎丘分中心");

                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.LoadFromFile(FilePath);

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    pdfDocument.Print();
                }
                File.Delete(FilePath);
            }
        }
        //拍照
        private void IDCardRun()
        {
            if (m_iPort != 0)
            {
                read_success = IDcard.IDcardRead(m_iPort, ref idcardData);

                if (read_success == 0 || read_success == 1)
                {
                    Num += 1;
                    idcardData.Format(); 
                    singleGlobal.Name  = idcardData.Name;
                    singleGlobal.IDcardNo = idcardData.IDCardNo;
                    singleGlobal.Msg = $"姓名：{ idcardData.Name} 身份证号：{ idcardData.IDCardNo}";
                    pageTimer.IsEnabled = true;
                }
                else
                {
                    OneRun = false;
                    if (read_success == -1)  
                        singleGlobal.Msg = $"{IDcard.Code[read_success]}";
                    if (CameraOpen&&!AutoAll)
                    {
                        singleGlobal.Msg = "未读取到身份证! 请放入身份证后重试";
                    }
                    if (!Auto)
                    {
                        singleGlobal.Msg = "未读取到身份证! 请放入身份证后重试";
                        Dispatcher.BeginInvoke(new Action(() => Alert(2)));
                    }
                    
                }
            }
            else
            {
                singleGlobal.Msg = "读卡器未能正确连接，正在尝试重连";
                m_iPort = IDcard.IDcardSet();
                singleGlobal.Msg = m_iPort == 0 ? "读卡器连接失败，请检查读卡器连接情况，确认正确连接后重试" : $"读卡器连接正常 端口为{m_iPort}";
            }
        }
    }



    //本页面全局函数在这里进行定义
    public class SingleGlobal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string date;
        public string Date
        {
            get { return date; }
            set { date = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date")); }
        }

        public string autoclolour= "";
        public string AutoColour
        {
            get { return autoclolour; }
            set { autoclolour = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AutoColour")); }
        }


        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }
        }
        public string idcardNo;
        public string IDcardNo
        {
            get { return idcardNo; }
            set { idcardNo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IDcardNo")); }
        }

        public string log;
        public string Log
        {
            get { return log; }
            set { log = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Log")); }
        }

        public string msg = "";

        public string Msg
        {
            get
            {
                return msg; 
            }
            set 
            {
                msg = value;
                Log += $"{DateTime.Now:yyyy年MM月dd日 HH:mm:ss} {Msg}" + Environment.NewLine;
            }
        }

        public string numlabe;
        public string NumLabel 
        {
            get { return numlabe; }
            set { numlabe = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumLabel")); }
        }

    }

}
