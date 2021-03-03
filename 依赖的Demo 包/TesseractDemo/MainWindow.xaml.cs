using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace TesseractDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private FilterInfoCollection videoDevices;//所有摄像设备
        private VideoCapabilities[] videoCapabilities;//摄像头分辨率
        private readonly List<string> Filename = new List<string>() { };

        public static bool VideoStatue = false;

        public static VideoCaptureDevice VideoDevice;//摄像设备


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);//得到机器所有接入的摄像设备

            if (videoDevices.Count != 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    cboVideo.Items.Add(device.Name);//把摄像设备添加到摄像列表中
                }
            }
            else
            {
                cboVideo.Items.Add("没有找到摄像头");
            }

            cboVideo.SelectedIndex = 0;//默认选择第一个

            if (VideoDevice != null)//如果摄像头不为空
            {
                if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                {
                    VideoDevice.VideoResolution = videoCapabilities[cboResolution.SelectedIndex];//摄像头分辨率
                    vispShoot.VideoSource = VideoDevice;//把摄像头赋给控件
                    vispShoot.Start();//开启摄像头
                }
            }
		}



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!VideoStatue)
            {
                Button button = sender as Button;
                switch ((string)button.Tag)
                {
                    case "Home":
                        break;
                    case "Connect":
                        if (VideoDevice != null)//如果摄像头不为空
                        {
                            if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                            {
                                VideoDevice.VideoResolution = videoCapabilities[cboResolution.SelectedIndex];//摄像头分辨率
                                vispShoot.VideoSource = VideoDevice;//把摄像头赋给控件
                                vispShoot.Start();//开启摄像头
                            }
                        }
                        break;
                    case "DisConnect":
                        if (vispShoot.VideoSource != null)
                        {
                            vispShoot.SignalToStop();
                            vispShoot.WaitForStop();
                            vispShoot.VideoSource = null;
                        }
                        break;
                    case "Shoot":
                        Bitmap img = vispShoot.GetCurrentVideoFrame();//拍照
                                                                      //这里可以根据情况，把照片存到某个路径下
                        //MessageBox.Show("正在拍摄");
                        img.Save("1.jpg");
                        //Thread thread = new Thread(() => SSSS(img))
                        //{
                        //    IsBackground = true
                        //};
                        //thread.Start();

                        break;

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("请先关闭已经打开的摄像头", "EXC", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SSSS(Bitmap img)
        {
            string imageFile = "0.jpg";

            //Color TempColor;
            //int with = img.Width;
            //int Heiht = img.Height;
            //int result;
            //for (int x = 0; x < with; x++)
            //{
            //    for (int y = 0; y < Heiht; y++)
            //    {
            //        TempColor = img.GetPixel(x, y);//获取当前坐标的像素值
            //        result = (TempColor.R + TempColor.G + TempColor.B) / 3;//取红绿蓝三色的平均值

            //        //绘图，把处理后的值赋值到刚才定义的bm对象里面
            //        img.SetPixel(x, y, Color.FromArgb(result, result, result));


            //        if (TempColor.R > 200 || TempColor.R < 10)
            //            img.SetPixel(x, y, Color.FromArgb(255, 255, 255));
            //        else
            //        {
            //            img.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            //        }
            //    }
            //}


            img.Save(imageFile);
            for (int i = 0; i<10; i++)
            {
                imageFile= i.ToString()+".jpg";
                var dsImage = BaseUtil.GetRotateImage(img, 90+i);
                dsImage.Save(imageFile);
                var text = ParseText("1", i.ToString()+".jpg", "eng");
                Match math = Regex.Match(text, @"(\d{11})");
                if (math.Success)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + math.Value + Environment.NewLine + text);
                    break;
                }
                if (i > 0)
                {
                    dsImage = BaseUtil.GetRotateImage(img, -i);
                    dsImage.Save(imageFile);
                    text = ParseText("1", imageFile, "eng");
                    math = Regex.Match(text, @"(\d{11})");
                    if (math.Success)
                    {
                        MessageBox.Show(i.ToString() + Environment.NewLine + math.Value + Environment.NewLine + text);
                        break;
                    }
                }
            }
        }


        private void cboVideo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (videoDevices.Count != 0)
            {
                //获取摄像头
                VideoDevice = new VideoCaptureDevice(videoDevices[cboVideo.SelectedIndex].MonikerString);

                cboResolution.Items.Clear();//清空列表
                videoCapabilities = VideoDevice.VideoCapabilities;//设备的摄像头分辨率数组
                foreach (VideoCapabilities capabilty in videoCapabilities)
                {
                    cboResolution.Items.Add($"{capabilty.FrameSize.Width} x {capabilty.FrameSize.Height}");
                }
                cboResolution.SelectedIndex = 0;//默认选择第一个
            }
        }
        
        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            var imageFile = "1.jpg";
            var text = ParseText("1", imageFile, "eng", "fra");

            Match math = Regex.Match(text, @"(\d{11})");

            MessageBox.Show(math.Value +Environment.NewLine+ text);
        }
        private static string ParseText(string tesseractPath, string imageFile, params string[] lang)
        {
            string output = string.Empty;
            var tempOutputFile = Path.GetTempPath() + Guid.NewGuid();

            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                //info.WorkingDirectory = "tesseract.exe";
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.UseShellExecute = false;
                info.FileName = "cmd.exe";
                info.CreateNoWindow = true;
                info.RedirectStandardOutput = true;
                info.Arguments =
                    "/c tesseract.exe " +
                    // Image file.
                    imageFile + " " +
                    // Output file (tesseract add '.txt' at the end)
                    tempOutputFile +
                    // Languages.
                    " -l " + string.Join("+", lang);

                // Start tesseract.
                Process process = Process.Start(info);
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    // Exit code: success.
                    output = File.ReadAllText(tempOutputFile + ".txt");
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

        private void Window_Closed(object sender, EventArgs e)
        {
            if (vispShoot.VideoSource != null)
            {
                vispShoot.SignalToStop();
                vispShoot.WaitForStop();
                vispShoot.VideoSource = null;
            }
            Environment.Exit(0);
        }
    }
}
