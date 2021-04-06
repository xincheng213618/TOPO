using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;

namespace ECRService
{
    public class FaceControlKeeper
    {
        static private FaceControlKeeper faceControlKeeper = null;
        private WindowsFormsHost faceFormsHost = null;
        public WindowsFormsHost FormsHost { get { return faceFormsHost; } }
        public AxAmFaceControlLib.AxAmFaceControl FaceControl { get { return faceFormsHost.Child as AxAmFaceControlLib.AxAmFaceControl; } }

        private FaceControlKeeper()
        {
            faceFormsHost = new WindowsFormsHost();
            AxAmFaceControlLib.AxAmFaceControl faceControl = new AxAmFaceControlLib.AxAmFaceControl();
            faceControl.Height = 480;
            faceControl.Width = 640;
            faceControl.BeginInit();
            faceFormsHost.Child = faceControl;
            faceControl.EndInit();
        }

        static public FaceControlKeeper GetFaceControlKeeper()
        {
            if (faceControlKeeper == null)
                faceControlKeeper = new FaceControlKeeper();
            return faceControlKeeper;
        }
    }
}
