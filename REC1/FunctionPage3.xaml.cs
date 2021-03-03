using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
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

namespace REC
{
    /// <summary>
    /// FunctionPage3.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPage3 : Page
    {
       
        Estate estate = new Estate();

        public FunctionPage3()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            estate.devMsg += RealEstate_Dev_devMsg;
            WaitShow.DataContext = printDate;
            WaitShow.Visibility = Visibility.Visible;
            printDate.StatusCode = "正在初始化";
            foreach (FilterInfo device in VideoHelper.VideoDevices)
                if (device.Name == "USB Camera")
                {
                    VideoHelper.VideoDevice = new VideoCaptureDevice(device.MonikerString);
                    VideoHelper.VideoDevice.VideoResolution = VideoHelper.VideoDevice.VideoCapabilities[0];
                    vispShoot.VideoSource = VideoHelper.VideoDevice;//把摄像头赋给控件
                    vispShoot.Start();//开启摄像头
                    break;
                }

            Thread thread1 = new Thread(new ThreadStart(ComPort));
            thread1.IsBackground = true;
            thread1.Start();

        }
        //打开关闭串口标志位
        int BZW = 1;
        private void ComPort()
        {
            try
            {
                var response = estate.Open1(Global.Config.EstatePort1);
 
                if (response == 0)
                {
                    BZW = 0;
                   // estate.RESET_();   
                }
                else
                {
                    MessageBox.Show("端口打开失败");
                    Dispatcher.BeginInvoke(new Action(() => Home("端口打开失败")));
                }

                if (BZW == 0)
                {
                    estate.SEL_P();
                    Thread.Sleep(5000);
                    estate.Process_Print();

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                Dispatcher.BeginInvoke(new Action(() => Home(err.Message)));
            }
           
        }

        private void RealEstate_Dev_devMsg(object sender, int Code, string data)
        {
            printDate.StatusCode = data;
            #region
            if (Code == -170)
            {
                estate.RESET_();
                Thread.Sleep(1000);
                estate.Close();
                Log.Write(data);
                Dispatcher.BeginInvoke(new Action(() => Home(data + ",请联系管理员")));
            }
            if ((Code>-14 && Code <0)||Code ==-20||Code ==-21||Code ==59)
            {
                estate.RESET_();
                Thread.Sleep(1000);
                estate.Close();
                Log.Write(data);
                Dispatcher.BeginInvoke(new Action(() => Home(data + ",请联系管理员")));
            }
            if(Code == 30)
            {
                Dispatcher.BeginInvoke(new Action(() => OCR()));
            }
            #endregion    
            if (Code == 50) //第一个打印
            {
                Dispatcher.BeginInvoke(new Action(() => ZDTest_One()));
            }
            if (Code == 502)//第二个打印
            {
                Dispatcher.BeginInvoke(new Action(() => ZDTest()));
            }
            if (Code == 503)//第三个打印
            {
                Dispatcher.BeginInvoke(new Action(() => ZDTest_Three()));
            }


            if (Code == 1003)
            {
                estate.Close();
                Log.Write(data);
                Dispatcher.BeginInvoke(new Action(() => Home("制证完成，请取证")));
            }
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
        private void Rorc()
        {
            Match math = null;
            for (int i = 0; i < 10; i++)
            {
                Mat mat = new Mat("Temp\\ocr_result.jpg", ImreadModes.Color);
                Mat mat1 = EmguHelper.Rotate(mat, i);
                mat1.Save("Temp\\ocr_result1.jpg");
                var text = ParseText("Temp\\ocr.jpg", "eng");

                math = Regex.Match(text, @"(\d{11})");
                if (math.Success)
                {
                    printDate.StatusCode = i.ToString() + "识别成功" + Environment.NewLine + math.Value + Environment.NewLine;
                    //recdata.OCRresult = math.Value;
                    break;
                }

                if (i > 0)
                {
                    mat1 = EmguHelper.Rotate(mat, -i);
                    mat1.Save("Temp\\ocr_result1.jpg");
                    text = ParseText("Temp\\ocr_result1.jpg", "eng");

                    math = Regex.Match(text, @"(\d{11})");
                    if (math.Success)
                    {
                        printDate.StatusCode = (-i).ToString() + "识别成功" + Environment.NewLine + math.Value + Environment.NewLine;
                        //recdata.OCRresult = math.Value;

                        break;
                    }
                }
            }

            //bool success = Requests.File_Upload("Temp\\ocr_result1.jpg", math.Value, math.Success, recdata.QLR, recdata.QLRZJH, recdata.BDCQZH);
            //if (!success)
            //    Log.Write("文件上传失败");

            //Dispatcher.BeginInvoke(new Action(() => OCRover(math.Success)));
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
        PrintDate printDate = new PrintDate();
      
        private void Home(string Msg )
        {
            Content = new HomePage(Msg);
            Pages();
        }

        public void ZDTest_One()
        {
            //print.PrinterSettings.PrinterName
            PrintDocument ZDDocument = new PrintDocument();
            ZDDocument.OriginAtMargins = true;
            //是否为纵向打印
            ZDDocument.DefaultPageSettings.Landscape = true;

            //ZDDocument.PrinterSettings.PrinterName = pName;
            ZDDocument.PrinterSettings.PrintToFile = false;
            PrintController pc = new StandardPrintController();
            ZDDocument.PrintController = pc;

            PaperSize ps = new PaperSize("Custom Size 1", Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书高度"].ToString()) / 10.4 * 100), Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书宽度"].ToString()) / 12.4 * 100));
            ZDDocument.DefaultPageSettings.PaperSize = ps;

            //ZDDocument.DefaultPageSettings.PrinterSettings=new PrinterSettings()
            ZDDocument.DefaultPageSettings.Margins = new Margins(Convert.ToInt32(ConfigurationManager.AppSettings["左边距"].ToString()), 0, 0, 0);
            //ZDDocument.BeginPrint += new PrintEventHandler(ZDDocument_BeginPrint);
            ZDDocument.PrintPage += new PrintPageEventHandler(delegate (object sender2, PrintPageEventArgs e2)
            {
                e2.PageSettings.Margins.Left = 0;
                e2.PageSettings.Margins.Top = 0;
                e2.Graphics.PageUnit = GraphicsUnit.Millimeter;

                ///宽预留八十


                e2.Graphics.DrawString("登记结构" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(256, 270), new StringFormat());
                e2.Graphics.DrawString("2018年05月15日" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(252, 275), new StringFormat());
                e2.HasMorePages = false;
            });
            {
                ZDDocument.Print();
            }
        }
        public void ZDTest()
        {
            PrintDocument ZDDocument = new PrintDocument();
            ZDDocument.OriginAtMargins = true;
            //是否为纵向打印
            ZDDocument.DefaultPageSettings.Landscape = true;

            //ZDDocument.PrinterSettings.PrinterName = pName;
            ZDDocument.PrinterSettings.PrintToFile = false;
            PrintController pc = new StandardPrintController();
            ZDDocument.PrintController = pc;

            PaperSize ps = new PaperSize("Custom Size 1", Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书高度"].ToString()) / 10.4 * 100), Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书宽度"].ToString()) / 12.4 * 100));
            ZDDocument.DefaultPageSettings.PaperSize = ps;

            //ZDDocument.DefaultPageSettings.PrinterSettings=new PrinterSettings()
            ZDDocument.DefaultPageSettings.Margins = new Margins(Convert.ToInt32(ConfigurationManager.AppSettings["左边距"].ToString()), 0, 0, 0);
            //ZDDocument.BeginPrint += new PrintEventHandler(ZDDocument_BeginPrint);
            ZDDocument.PrintPage += new PrintPageEventHandler(delegate (object sender2, PrintPageEventArgs e2)
            {
                e2.PageSettings.Margins.Left = 0;
                e2.PageSettings.Margins.Top = 0;
                e2.Graphics.PageUnit = GraphicsUnit.Millimeter;

                ///宽预留八十

                e2.Graphics.DrawString(ConfigurationManager.AppSettings["PROVINCE"].ToString(), new Font("宋体", 12), Brushes.Black, new PointF(13, 94), new StringFormat());
                //X轴所留长度不够  打印无法完整   若需完整   会盖上证上的字
                e2.Graphics.DrawString(ConfigurationManager.AppSettings["TIME"].ToString(), new Font("宋体", 12), Brushes.Black, new PointF(26, 94), new StringFormat());

                //e2.Graphics.PageUnit = GraphicsUnit.Millimeter;
                e2.Graphics.DrawString(ConfigurationManager.AppSettings["THE_CITY"].ToString(), new Font("宋体", 12), Brushes.Black, new PointF(51, 94), new StringFormat());
                e2.Graphics.DrawString(ConfigurationManager.AppSettings["PROPERTY_RIGHTS_NUM"].ToString(), new Font("宋体", 12), Brushes.Black, new PointF(108, 94), new StringFormat());
                //权利人
                e2.Graphics.DrawString(ConfigurationManager.AppSettings["NAME"].ToString(), new Font("宋体", 12), Brushes.Black, new PointF(51, 112), new StringFormat());
                //共有情况
                e2.Graphics.DrawString(ConfigurationManager.AppSettings["THE_OWNERSHIP_OF"].ToString(), new Font("宋体", 12), Brushes.Black, new PointF(51, 126), new StringFormat());
                //坐落
                e2.Graphics.DrawString(ConfigurationManager.AppSettings["PLACE"].ToString(), new Font("宋体", 12), Brushes.Black, new PointF(51, 140), new StringFormat());
                //-----为了少打一点
                //不动产单元号
                //e2.Graphics.DrawString("216844 456456 975416 464644898", new Font("宋体", 12), Brushes.Black, new PointF(51, 154), new StringFormat());
                ////权利类型
                //e2.Graphics.DrawString("国有建设土地使用权/房屋使用权", new Font("宋体", 12), Brushes.Black, new PointF(51, 166), new StringFormat());
                ////权利性质
                //e2.Graphics.DrawString("商品房", new Font("宋体", 12), Brushes.Black, new PointF(51, 180), new StringFormat());
                ////用途
                //e2.Graphics.DrawString("城镇住宅用地/住宅", new Font("宋体", 12), Brushes.Black, new PointF(51, 194), new StringFormat());
                ////面积
                //e2.Graphics.DrawString("房屋建筑面积168m²", new Font("宋体", 12), Brushes.Black, new PointF(51, 207), new StringFormat());
                ////使用期限
                //e2.Graphics.DrawString("国有建设用地使用权2018年7月27日", new Font("宋体", 12), Brushes.Black, new PointF(51, 220), new StringFormat());
                ////权利其它状况
                //e2.Graphics.DrawString("分摊土地使用权面积：25.5m²" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(47, 250), new StringFormat());
                //e2.Graphics.DrawString("房屋结构：钢混" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(47, 255), new StringFormat());
                //e2.Graphics.DrawString("专有建筑面积133.24m²" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(47, 260), new StringFormat());
                //e2.Graphics.DrawString("房屋总层数：46层.所在层数：29层" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(47, 265), new StringFormat());
                //e2.Graphics.DrawString("房屋竣工时间：2018年3月4日" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(47, 270), new StringFormat());

                //打印二维码
                //e2.Graphics.DrawImage(CreateQuickMark(lblOCR_NUM.Text), new PointF(220, 180));

                //附记
                //e2.Graphics.DrawString(" （1）通过何种形式得到的这套房：买卖、赠与、继承等。" +"\r\n"+
                //    " （2）有无抵押。有抵押，还标明了抵押期限。有无租赁等情况。" +"\r\n"+
                //    " （3）其他情形。房产证上应该完整地体现权利人对房屋所拥有的权利" + "\r\n" +
                //    "     以及该房屋上存在的所有权利，" + "\r\n" +
                //    "     其具体内容应包括以下几个方面：" + "\r\n" +
                //    "  1、权利人情况。指权利人名称、身份证号（个人）、企业性质等；" + "\r\n" +
                //    "  2、土地使用情况。指土地坐落、面积、使用期限等；" + "\r\n" +
                //    "  3、房屋所有权情况。指房屋结构类型、高度、竣工日期、建筑面积等；" + "\r\n" +
                //    "  4、房地产其他权利情况。" + "\r\n" +
                //    "     如有抵押，则记录抵押权人、抵押金额、抵押期限等；", new Font("宋体", 12), Brushes.Black, new PointF(175, 120), new StringFormat());

                e2.Graphics.DrawString("登记结构" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(256, 270), new StringFormat());
                e2.Graphics.DrawString("2018年05月15日" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(252, 275), new StringFormat());
                e2.HasMorePages = false;
            });
            {
                ZDDocument.Print();
            }
        }
        public void ZDTest_Three()
        {
            PrintDocument ZDDocument = new PrintDocument();
            ZDDocument.OriginAtMargins = true;
            //是否为纵向打印
            ZDDocument.DefaultPageSettings.Landscape = true;

            //ZDDocument.PrinterSettings.PrinterName = pName;
            ZDDocument.PrinterSettings.PrintToFile = false;
            PrintController pc = new StandardPrintController();
            ZDDocument.PrintController = pc;

            PaperSize ps = new PaperSize("Custom Size 1", Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书高度"].ToString()) / 10.4 * 100), Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书宽度"].ToString()) / 12.4 * 100));
            ZDDocument.DefaultPageSettings.PaperSize = ps;

            //ZDDocument.DefaultPageSettings.PrinterSettings=new PrinterSettings()
            ZDDocument.DefaultPageSettings.Margins = new Margins(Convert.ToInt32(ConfigurationManager.AppSettings["左边距"].ToString()), 0, 0, 0);
            //ZDDocument.BeginPrint += new PrintEventHandler(ZDDocument_BeginPrint);
            ZDDocument.PrintPage += new PrintPageEventHandler(delegate (object sender2, PrintPageEventArgs e2)
            {
                e2.PageSettings.Margins.Left = 0;
                e2.PageSettings.Margins.Top = 0;
                e2.Graphics.PageUnit = GraphicsUnit.Millimeter;

                ///宽预留八十
                ///
                e2.Graphics.DrawString("---------------", new Font("宋体", 12), Brushes.Black, new PointF(51, 194), new StringFormat());
                //面积
                e2.Graphics.DrawString("----------------", new Font("宋体", 12), Brushes.Black, new PointF(51, 207), new StringFormat());
                //假装有图哈哈哈
                e2.Graphics.DrawString("图", new Font("宋体", 12), Brushes.Black, new PointF(51, 166), new StringFormat());


                e2.Graphics.DrawString("-" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(252, 275), new StringFormat());
                e2.HasMorePages = false;
            });
            {
                ZDDocument.Print();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    //PopAlert("打印期间不允许主动返回，请耐心等待",3);
                    Content = new HomePage();
                    Pages();
                    break;
              

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
