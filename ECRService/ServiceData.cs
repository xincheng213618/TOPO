using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;

namespace ECRService
{
    public class ServiceData
    {
        public static IDCardInfo idCardInfo = null;
        public static int authenticationCount = 0;
        public static int type = 0;
        public static object data = null;
        //public static ObservableCollection<ReportItem> items = null;

        public static string reportID = null; //全局接受报告ID

        public static void Initialize()
        {
            idCardInfo = null;
            authenticationCount = 0;
            type = 0;
            data = null;
            //items = null;
        }
    }
}
