using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REC
{
    public static class VideoHelper
    {
        public static FilterInfoCollection VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);//所有摄像设备
        public static VideoCapabilities[] VideoCapabilities;//摄像头分辨率
        public static bool VideoStatue = false;
        public static VideoCaptureDevice VideoDevice;//摄像设备
    }
}
