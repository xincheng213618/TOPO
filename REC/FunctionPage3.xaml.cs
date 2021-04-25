using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AForge.Video.DirectShow;
using BaseDLL;
using BaseUtil;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
   
namespace REC
{
    /// <summary>
    /// FunctionPage3.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPage3 : Page
    {
        private string FileName;
        public FunctionPage3(string FileName)
        {
            this.FileName = FileName;
            InitializeComponent();
        }
        PrintDate printDate = new PrintDate();
        private void Page_Initialized(object sender, EventArgs e)
        {
            Log.Write(FileName);

            //添加委托事件
            ESerialPort.DevMsg += RealEstate_Dev_devMsg;
            WaitShow.DataContext = printDate;
            printDate.StatusCode = "正在初始化打印机";

            foreach (FilterInfo device in VideoHelper.VideoDevices)
                if (device.Name == "USB2.0 Camera")
                {
                    VideoHelper.VideoDevice = new VideoCaptureDevice(device.MonikerString);
                    VideoHelper.VideoDevice.VideoResolution = VideoHelper.VideoDevice.VideoCapabilities[0];
                    vispShoot.VideoSource = VideoHelper.VideoDevice;//把摄像头赋给控件
                    vispShoot.Start();//开启摄像头
                    break;
                }
            //下面为异步操作，分开
            Print();
        }
        private async void Print()
        {
            ESerialPort.RunStart();
            //这里不做设置会导致问题  防止没有证本还要发送盖章数据 
            await Task.Delay(9000);

            if (ESerialPort.CheckDevice2() == 0 && ESerialPort.CheckDevice1() == 0)
            {
                printDate.StatusCode = "初始化完成，正在上证";
                AcrobatHelper.pdfControl.LoadFile(FileName);
                AcrobatHelper.pdfControl.printAll();
            }
            else
            {
                await Dispatcher.BeginInvoke(new Action(() => ErrorMsg("缺证，请联系C13号窗口工作人员")));
            }
        }
        private async void RealEstate_Dev_devMsg(int Code, string data)
        {
            printDate.StatusCode = data;

            if (Code == 13)
            {
                await Task.Delay(2000);
                printDate.StatusCode = "打印完成，正在返回首页";
                await Task.Delay(2000);
                Media.Play("Base\\Media\\请取走您的不动产权证书.mp3");
                await Dispatcher.BeginInvoke(new Action(() => Msg("请取走您的证书")));
            }
            else if (Code == 1)
            {
                await Task.Delay(500);
                printDate.StatusCode = "正在打印第三页";
            }
            else if (Code == 10)
            {
                await Task.Delay(500);
                printDate.StatusCode = "正在打印第二页";
            }
            else if (Code == 11)
            {
                await Task.Delay(500);
                printDate.StatusCode = "正在打印第一页";
            }
            else if (Code == 12)
            {
                await Task.Delay(1000);
                printDate.StatusCode = "打印结束，准备盖章";
            }
            else if (Code == 3)
            {
                //准备拍照;
                OCR();
            }
            else if (Code == -1)
            {
                await Dispatcher.BeginInvoke(new Action(() => ErrorMsg("打印状态更新超时，请联系C13号窗口工作人员")));
            }
        }

        private void Msg(string Msg)
        {
            Content = new HomePage(Msg);
            Pages();
        }

        private void ErrorMsg(string ErrorMsg)
        {
            Content = new ErrorPage(ErrorMsg);
            Pages();
        }


        Thread thread;
        private async void OCR()
        {
            await Task.Delay(200);
            printDate.StatusCode = "拍摄证书编号中";

            //等待证书到位
            Bitmap img = vispShoot.GetCurrentVideoFrame();
            img.Save("Temp\\ocr.jpg");

            //预处理
            EmguHelper.PreOCR("Temp\\ocr.jpg");
            await Task.Delay(300);

            printDate.StatusCode = "正在识别编号";
            //旋转并识别
            await Task.Delay(500);

            thread = new Thread(() => Rorc())
            {
                IsBackground = true
            };
            thread.Start();

        }

        List<int> lisocr = new List<int>() { 0,-1,1,-2,2,3,-3,4,-4};
        //识别角度不能设置太高
        private void Rorc()
        {
            string YinZhihao="";
            bool Sucess =false;
            foreach (var i in lisocr)
            {
                printDate.StatusCode = "印制号识别中："+i.ToString();

                if (OCRMath("Temp\\ocr_result1.jpg", i, out YinZhihao))
                {
                    printDate.StatusCode = i.ToString() + "识别成功" + Environment.NewLine + YinZhihao;
                    Global.Related.Fix_OCR_Data = YinZhihao;
                    Sucess = true;
                    break;
                }
            }
            Requests.File_Upload("Temp\\ocr_result1.jpg", YinZhihao, true, Global.Related.RECData.QLR, Global.Related.RECData.QLRZJH, Global.Related.RECData.BDCQZH);
            Dispatcher.BeginInvoke(new Action(() => OCRover(Sucess)));
        }

        /// <summary>
        /// OCR 匹配
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="i"></param>
        /// <param name="YinZhiHao"></param>
        /// <returns></returns>
        private bool OCRMath(string FileName,int i,out string YinZhiHao)
        {
            YinZhiHao = "";
            Mat mat = new Mat("Temp\\ocr_result.jpg", ImreadModes.Color);
            Mat mat1 = EmguHelper.Rotate(mat, i);
            mat1.Save("Temp\\ocr_result1.jpg");
            var text = ParseText("Temp\\ocr_result1.jpg", "eng");


            Match math = Regex.Match(text, @"(\d{11})");
            Match math1 = Regex.Match(text, @"(\d{10})");
            Match math2 = Regex.Match(text, @"(\d{9})");
            Match math3 = Regex.Match(text, @"(\d{8})");


            if (math.Success)
            {
                YinZhiHao = math.Value;
                if (YinZhiHao.Substring(0, 3) != "320")
                {
                    YinZhiHao = "320" + YinZhiHao.Substring(3, math.Value.Length - 3);
                    Log.Write("执行印制号修正:" + YinZhiHao);
                }
                return true;
            }
            else if (math1.Success)
            {
                YinZhiHao = math1.Value;
                if (YinZhiHao.Substring(0, 3) != "20")
                {
                    YinZhiHao = "320" + YinZhiHao.Substring(2, math1.Value.Length - 2);
                    Log.Write("执行印制号修正:" + YinZhiHao);
                }
                return true;
            }
            else if (math2.Success)
            {
                YinZhiHao = math2.Value;
                if (YinZhiHao.Substring(0, 3) != "320")
                {
                    YinZhiHao = "0" + YinZhiHao.Substring(1, math2.Value.Length - 1);
                    Log.Write("执行印制号修正:" + YinZhiHao);
                }
                return true;
            }
            else if (math3.Success)
            {
                YinZhiHao = math3.Value;
                YinZhiHao = "0" + YinZhiHao.Substring(0, math3.Value.Length - 0);
                Log.Write("执行印制号修正:" + YinZhiHao);
                return true;
            }
            else
            {
                return false;
            }

        }



        private void OCRover(bool Success)
        {
            if (Success)
            {
                printDate.StatusCode = "印制号回填中:"+ Global.Related.Fix_OCR_Data;
                Thread thread1 = new Thread(() => RequestOCR())
                {
                    IsBackground = true
                };
                thread1.Start();
            }
            else
            {
                //ESerialPort.Run2(); //这里要求 异常时提示卡证,由工作人员进行处理.
                Dispatcher.BeginInvoke(new Action(() => ErrorMsg("印制号识别失败，请联系C13号窗口工作人员")));
            }
        }
        private void RequestOCR()
        {
            string response = Http.OCR_Upload(Global.Related.RECData.SLBH, Global.Related.RECData.QLRZJH, DateTime.Now.ToString("yyyyMMdd"), Global.Related.RECData.ZSID, Global.Related.Fix_OCR_Data);
            Dispatcher.BeginInvoke(new Action(() => GetPhrase(response)));
        }

        private void GetPhrase(string response)
        {
            if (response != null)
            {
                try
                {
                    JObject RECResponse = (JObject)JsonConvert.DeserializeObject(response);
                    string code = (string)RECResponse.GetValue("code");
                    if (code.Equals("0"))
                    {
                        ESerialPort.Run2();
                        Log.Write("印制号回填成功:"+ Environment.NewLine + Global.Related.Fix_OCR_Data);
                    }
                    else
                    {
                        Log.Write("印制号回填失败:"+ Environment.NewLine + (string)RECResponse.GetValue("data"));
                        JObject GTresponse = (JObject)JsonConvert.DeserializeObject((string)RECResponse.GetValue("data"));
                        Content = new ErrorPage("请联系C13号窗口工作人员:"+(string)GTresponse.GetValue("msg"));
                        Pages();
                    }
                }
                catch
                {
                    Content = new ErrorPage("印制号回传接口解析错误,请联系C13号窗口工作人员");
                    Pages();


                }
            }
            else
            {
                Content = new ErrorPage("印制号回传接口连接错误，请联系C13号窗口工作人员");
                Pages();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    //PopAlert("打印期间不允许主动返回，请耐心等待",3);
                    break;
            }

        }

                                                       

        private void Pages()
        {
            vispShoot.SignalToStop();
            vispShoot.WaitForStop();
            vispShoot.VideoSource = null;
            ESerialPort.DevMsg -= RealEstate_Dev_devMsg; //委托事件要注销掉

            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private string ParseText(string imageFile, params string[] lang)
        {
            string output = string.Empty;
            var tempOutputFile = Path.GetTempPath() + Guid.NewGuid();

            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    Arguments =
                    "/c tesseract.exe " +
                    imageFile + " " +
                    tempOutputFile +
                    " -l " + string.Join("+", lang)
                };

                // Start tesseract.
                Process process = Process.Start(info);
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    output = File.ReadAllText(tempOutputFile + ".txt");
                    Log.Write(output);
                }
                else
                {
                    throw new Exception("Error. Tesseract stopped with an error code = " + process.ExitCode);
                }
            }
            finally
            {
                File.Delete(tempOutputFile + ".txt");
            }

            return output;
        }
    }


    //显示
    public class PrintDate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string statusCode ;
        public string StatusCode
        {
            get { return statusCode; }
            set { 
                statusCode = value;
                Log.Write(StatusCode);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusCode")); 
            }

        }
    }


}
