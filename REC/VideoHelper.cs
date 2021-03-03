using AForge.Video.DirectShow;
using System.Collections.Generic;

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
