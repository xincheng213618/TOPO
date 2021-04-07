using System.Collections.Generic;
using System.Runtime.InteropServices;



namespace BaseDLL
{

    //不使用盖章机的时候不调用该DLL
    //Designed for 盖章机
    public class Stamp
    {

        public static string Kind = "";
        public static int StampPort = 0;

        /// <summary>
        /// 初始化盖章机调用
        /// </summary>
        public static int OpenDevice()
        {
            if (Kind == "PStamp")
            {
                int code = PStamp.PsOpen(StampPort);
                return code ;
            }
            else if (Kind == "StampPrinter")
            {
                int code = StampPrinter.OpenPort(StampPort);
                return code;
            }
            return -999;
        }



        public static void Close()
        {
            if (Kind == "StampPrinter")
            {
                StampPrinter.StopStamp();
            }
        }

        public static int Start(int PageAllNum)
        {
            if (Kind == "PStamp")
            {
                int code = PStamp.PsOpen(StampPort);
                if (code==0)
                {
                    PStamp.PsSetOne(PageAllNum, 0, 230);
                    PStamp.PsStamp(1);
                    return 0;
                }
                else
                {
                    return code;
                }
            }
            else if (Kind == "StampPrinter")
            {
                StampPrinter.StopStamp();

                int code = StampPrinter.CheckDeviceIsReady();
                if (code == 0)
                {
                    StampPrinter.Start(PageAllNum);
                    return 0;
                }
                else
                {
                    return code;
                }
            }
            return -1;
        }
    }


}
