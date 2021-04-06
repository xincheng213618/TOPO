using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECRService
{
    interface IAmCaptureImageProc
    {
        void OnCaptureImage(int code, string image, object data);
    }
}
