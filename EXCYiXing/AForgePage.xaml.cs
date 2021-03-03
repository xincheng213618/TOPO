using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.Drawing.Printing;
using System.Drawing;

namespace EXCYiXing
{
    /// <summary>
    /// AForge.xaml 的交互逻辑
    /// </summary>
    public partial class AForgePage : Page
    {
        private FilterInfoCollection videoDevices;//所有摄像设备
        private VideoCapabilities[] videoCapabilities;//摄像头分辨率
        private readonly List<string> Filename =new List<string>() { };

        public static bool VideoStatue = false;

        public static VideoCaptureDevice VideoDevice;//摄像设备

        //摄像头页面
        public AForgePage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
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

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!VideoStatue)
            {
                Button button = sender as Button;
                switch ((string)button.Tag)
                {
                    case "Home":
                        Content = new HomePage();
                        Pages();
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
                        //string name = cout.ToString() + ".jpg";
                        //cout += 1;
                        //Filename.Add(name);
                        //Bitmap img = vispShoot.GetCurrentVideoFrame();//拍照
                        //picbPreview1.Image = img;
                        ////这里可以根据情况，把照片存到某个路径下
                        //img.Save(name);
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
    }
}
