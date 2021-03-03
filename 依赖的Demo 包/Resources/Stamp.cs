using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXCResources
{
    public class Stamp
    {
        /// <summary>
        /// 初始化盖章机调用
        /// </summary>
        public static void Initialized()
        {
            if (Global.configData.PstampOpen == "0")
            {
                int code = PStamp.PsOpen(Global.configData.PStampPort);
                if (code != 0)
                    MessageBox.Show("盖章机报错信息：" + PStamp.PStampCode[code]);
            }
            if (Global.configData.PstampOpen == "1")
            {
                int code = StampPrinter.OpenPort(Global.configData.StampPort);
                if (code != 0)
                    MessageBox.Show("盖章机报错信息：" + StampPrinter.CheckDeviceIsReadyCode[code]);
            }
        }
        public static void Close()
        {
            //if (GlobalData.configData.PstampOpen == "0")
            //{
            //    PStamp.PsClose();
            //}
            if (Global.configData.PstampOpen == "1")
            {
                StampPrinter.StopStamp();
            }
        }

        public static string Start(int PageAllNum)
        {
            if (Global.configData.PstampOpen == "1")
            {
                StampPrinter.StopStamp();
            }

            if (Global.configData.PstampOpen == "0")
            {
                Log.Write("盖章机正在初始化");
                int code = PStamp.PsOpen(Global.configData.PStampPort);
                if (code==0)
                {
                    PStamp.PsSetOne(PageAllNum, Global.configData.PStampTiLeftCount, Global.configData.PStampTiPos);
                    PStamp.PsStamp(1);
                    return null;
                }
                else
                {
                    return PStamp.PStampCode[code];
                }
            }
            else if (Global.configData.PstampOpen == "1")
            {

                int code = StampPrinter.CheckDeviceIsReady();
                if (StampPrinter.CheckDeviceIsReady() == 0)
                {
                    StampPrinter.Start(PageAllNum);
                    return null;
                }
                else
                {
                    StampPrinter.Start(PageAllNum);
                    return StampPrinter.CheckDeviceIsReadyCode[code];
                }
            }
            return null;
        }
    }
    public class PStamp
    {
        public static Dictionary<int, string> PStampCode = new Dictionary<int, string>()
        {
            { 0,"成功" },
            { -1,"无设备,串口被占" },
            { -2,"无应答" },
            { -3,"超长时间无纸进入盖章模块" },
            { -5,"通道异物" },
            { -13,"重复定义" },
            { -14,"超过11种模式了" },
            { -15,"无效模式" },
            { -16,"超出页数" },
            { -17,"页数小于1" },
        };

        //打开连接盖章模块的串口
        [DllImport("Bin\\PStamp.dll", EntryPoint = "psOpen", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsOpen(int tiCommPort);

        //断开连接， 关闭串口
        [DllImport("bin\\PStamp.dll", EntryPoint = "psClose", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsClose();

        // 获得完成 盖章的页数
        [DllImport("bin\\PStamp.dll", EntryPoint = "psCount", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsCount();

        // 获得尚 未盖章的页数
        [DllImport("bin\\PStamp.dll", EntryPoint = "psLeft", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsLeft();


        // 启动盖章 tiMode=0/1=盖完返回 /发完指令立即返回
        [DllImport("bin\\PStamp.dll", EntryPoint = "psStamp", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsStamp(int tiMode);


        // 获取状态
        [DllImport("bin\\PStamp.dll", EntryPoint = "psStatus", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsStatus();


        // 停止盖章
        [DllImport("bin\\PStamp.dll", EntryPoint = "psStop",  CallingConvention = CallingConvention.StdCall)]
        public static extern int PsStop();

        // 暂停盖章任务
        [DllImport("bin\\PStamp.dll", EntryPoint = "psSuspend", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSuspend();

        // 设置速度
        [DllImport("bin\\PStamp.dll", EntryPoint = "psSpeedSet", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSpeedSet(int tiSpeed);

        // 设置过纸页数
        [DllImport("bin\\PStamp.dll", EntryPoint = "psSetNone", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSetNone(int tiPage);

        // 设置盖一个章的页数
        //tiLeftCount 如果等于 1，章盖在左侧；如果是 0，章盖在右侧
        //tiPos 盖章的位置，单位 mm，限制在由 psParamPos设置的最顶和最底之间
        [DllImport("bin\\PStamp.dll", EntryPoint = "psSetOne", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSetOne(int tiPage, int tiLeftCount, int tiPos);

        //设置 整页 盖章的页数 ，同侧的有效章位要按由小到大的顺序排列 
        //tiPage 页数 ，页数要大于零
        //tiLeftPos1 tiLeftPos2 tiLeftPos3 设置 左侧三 个 章的位置
        //tiRightPos1 tiRightPos2 tiRightPos3右侧三 个 章的位置
        //如果章位 为 -1 表示少盖一个章；有效章位将被排序；不适合的章位将被调整
        [DllImport("bin\\PStamp.dll", EntryPoint = "psSetPage", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSetPage(int tiPage, int tiLeftPos1, int tiLeftPos2, int tiLeftPos3, int tiRightPos1, int tiRightPos2, int tiRightPos3);

    }
    public class StampPrinter
    {
        private static readonly int[] iLeftFormatNum = new int[100];
        private static readonly int[] iRightFormatNum = new int[100];
        public static void Start(int PageAllNum = 0)
        {
            //设置印章位置
            int setStampPos = SetStampPos(Global.configData.NLeftPosOne, Global.configData.NLeftPosTwo, Global.configData.NLeftPosThree, Global.configData.NRightPosOne, Global.configData.NRightPosTwo, Global.configData.NRightPosThree);
            Log.Write("设置印章位置:" + setStampPos);
            //设置第8种类型印章位置
            int setStampPosEx = SetStampPosEx(Global.configData.NLeftPosExOne, Global.configData.NLeftPosExTwo, Global.configData.NRightPosExOne, Global.configData.NRightPosExTwo);
            Log.Write("设置第8种类型印章位置:" + setStampPosEx);
            for (int i = 0; i < PageAllNum; i++)
            {
                iLeftFormatNum[i] = Global.configData.ILeftFormatNum;
                iRightFormatNum[i] = Global.configData.IRightFormatNum;
            }
            //设置不同印章类型
            int setStampParams = SetStampParams(iLeftFormatNum, iRightFormatNum, PageAllNum);
            Log.Write("设置不同印章类型:" + setStampParams);

            //启动盖章机
            int run = Stamp();
            Log.Write("启动盖章机:" + run);

        }

        //打开盖章机端口
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "OpenPort")]
        public static extern int OpenPort(int nComPort);
        public static Dictionary<int, string> OpenPortCode = new Dictionary<int, string>()
        {
            { 0,"就绪" },
            { -1,"打开串口失败,可能端口被占用" },
        };


        /* 查询印章机是否准备就绪
         * 备注：一组纸张印章完成后，查询CheckDeviceIsReady返回-34（漏章错误），之后查询此函数获取每张纸张的漏章数量。
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "CheckDeviceIsReady")]
        public static extern int CheckDeviceIsReady();
        public static Dictionary<int, string> CheckDeviceIsReadyCode = new Dictionary<int, string>()
        {
            { 0,"就绪" },
            { -1,"命令成功无返回" },
            { -2,"卡纸,卡纸--通道有纸，但是没有任何一个电机在运行" },
            { -3,"正在导纸（电机在转就返回正在导纸状态）" },
            { -4,"正在盖章" },
            { -30,"状态错误，纸张长度异常" },
            { -31,"状态错误，电压异常" },
            { -32,"状态错误，电压异常" },
            { -33,"状态错误，印章转换错误" },
            { -34,"状态错误，漏章错误" }, 
        };

        /* 查询走纸张数
         * 
         * return   成功：返回走纸计数张数（盖章停止后该数据清空）
         *          失败：-1
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "GetStampCount")]
        public static extern int GetStampCount();

        /* 设置印章位置
         * 
         * param 依次表示左边第一个盖章位置，第二个盖章位置，第三个盖章位置，右边第一个盖章位置，第二个盖章位置，第三个盖章位置。单位为步数，每一步大约0.1554mm。
         *         最小印章位置设置值为145，最大印章位置设置值为1600，最小间距为610。
         *  
         * return   0  成功    -1 失败
         * 
         * 备注：1)、如果上层不调用此函数，按照默认值进行印章。
         *   2)、如果全部设置为0，按照默认值进行印章。
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "SetStampPos")]
        public static extern int SetStampPos(int nLeftPos1, int nLeftPos2, int nLeftPos3, int nRightPos1, int nRightPos2, int nRightPos3);

        /* 设置第8种类型印章位置
         * 
         * param 依次表示左边第一个盖章位置，第二个盖章位置，右边第一个盖章位置，第二个盖章位置。单位为步数，每一步大约0.1554mm。
         *        最小印章位置设置值范围为194~634，最大印章位置设置值范围为1149~1590。
         *        
         * return  0  成功    -1 失败
         * 
         *  备注：如果上层不调用此函数，按照默认值进行印章。
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "SetStampPosEx")]
        public static extern int SetStampPosEx(int nLeftPos1, int nLeftPos2, int nRightPos1, int nRightPos2);

        /* 设置不同印章类型
         * 
         * param 左边盖章机nLeft[]: 0 表示不盖章
		 *		                    1 表示第一个位置盖章
		 *		                    2 表示第二个位置盖章
		 *		                    3 表示第三个位置盖章
		 *		                    4 表示第一二个位置盖章
		 *		                    5 表示第一三个位置盖章
		 *		                    6 表示第二三个位置盖章
		 *		                    7 表示第一二三个位置盖章
         *                          8 表示A4纸张上下部分特定位置盖章
		 *		                    9 表示A4纸张上部分特定位置盖章
		 *		                    10 表示A4纸张下部分特定位置盖章	
         *		              
	     *        左边盖章机nRight[]: 0 表示不盖章
		 *	            	          1 表示第一个位置盖章
		 *	            	          2 表示第二个位置盖章
		 *	            	          3 表示第三个位置盖章
		 *	            	          4 表示第一二个位置盖章
		 *	            	          5 表示第一三个位置盖章
		 *	            	          6 表示第二三个位置盖章
		 *	            	          7 表示第一二三个位置盖章
         *                            8 表示A4纸张上下部分特定位置盖章
		 *	            	          9 表示A4纸张上部分特定位置盖章
		 *	            	          10 表示A4纸张下部分特定位置盖章	
         *		               
	     *   nLength表示数组长度，nLefts与nRights等长 数组每一位为一张纸盖章控制参数，一共为nLength个参数，可以连续为nLength张纸盖章，并且每张纸的盖章位置不一致。nLength范围：0---65535.
	     *   若输入纸张超出nLength，执行最后一张的盖章设置或不盖章（印章机EEPROM可设置两种方式）。
         *   
         * return   -1 设备运行状态错误(盖章完毕后，印章电机还在运行)
	     *           0  成功停止
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "SetStampParams")]
        public static extern int SetStampParams(int[] nLefts, int[] nRights, int nLength);

        /* 强制复位盖章机（清空走纸张数，盖章参数设置为仅走纸不盖章，盖章位置列表（功能6）清空）
         * 
         * return   -1;失败	0  成功
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "Reset")]
        public static extern int Reset();

        /* 测试走纸
         * 测试盖章机走纸动作是否能正常完成，通常用于设备监控
         * 
         * return 0表示成功 -1表示失败
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "TestPaperRun")]
        public static extern int TestPaperRun();

        /*测试盖章机盖章动作是否能正常完成，通常用于设备监控
         * 
         * return 0表示成功 -1表示失败
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "TestStampRun")]
        public static extern int TestStampRun();

        /* 启动盖章机盖章（清空上次走纸张数，清除漏章错误）
         *  
         *  return  0    盖章成功
         *          -1   串口接受数据失败
         *          -2   状态信息，卡纸 
         *          -5    盖章机错误，设备未就绪，不可进行盖章，如正在走纸、正在印章状态。
         *          -8    其他错误
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "Stamp")]
        public static extern int Stamp();

        /* 停止盖章机（盖章参数设置为仅走纸不盖章，盖章位置列表（功能6）清空）
         * 
         * return   -1 设备运行状态错误(盖章完毕后，印章电机还在运行)
	     *           0  成功停止
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "StopStamp")]
        public static extern int StopStamp();

        /* 关闭一打开的串口
         *  
         * reutrn 0表示成功 -1表示失败
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "ClosePort")]
        public static extern int ClosePort();

        /* 注油控制
         * 
         * param iCtrlCode：注油控制，0---停止注油，1---左侧注油，2---右侧注油
         * 
         * return   0表示成功 -1表示失败
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "CtrlAddInk")]
        public static extern int CtrlAddInk(int iCtrlCode);

        /* 查询印章个数
         * 
         * param int iLeftStampNum, //输出左侧印章个数
         *       int RightStampNum //输出右侧印章个数
         *           
         * return  0表示成功 -1表示失败
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "GetStampNumber")]
        public static extern int GetStampNumber(ref int iLeftStampNum, ref int iRightStampNum);

        /* 清空印章个数
         * 
         * param int iLeft, //清空左侧印章个数，0---不清空，1-清空
	     *        int iRight //清空右侧印章个数，0---不清空，1-清空
         * 
         * return  0表示成功 -1表示失败
         */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "ResetStampNumber")]
        public static extern int ResetStampNumber(int iLeft, int iRight);

        /* 查询固件版本信息
        * 
        * param  char* pszVersion, //固件版本信息，25个字节长度，格式举例：Main Firmware: FV1.000.13
        * 
        * return  0表示成功 -1表示失败
        */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "GetStampVersion")]
        public static extern int GetStampVersion(ref char pszVersion);

        /* 查询每张纸张的漏章数量
        * 
        * param char* iCount//每张纸张的漏章数量数组
	            int nLength //纸张张数
        * 
        * return  0表示成功 -1表示失败
        * 
        */
        [DllImport("bin\\LibStampPrinter.dll", SetLastError = true, EntryPoint = "GetMissStampCount")]
        public static extern int GetMissStampCount(ref char iCount, int nLength);

        //端口状态
        public static string PortStatus(int num)
        {
            string status = null;
            if (num == 0)
            {
                status = "成功打开串口";
            }
            if (num == -1)
            {
                status = "打开串口失败。可能端口被占用或者串口连接线错误";
            }
            return status;
        }

        //盖章机启动状态
        public static string StampStartStatus(int num)
        {
            string status = null;
            if (num == 0)
            {
                status = "盖章成功";
            }
            if (num == -1)
            {
                status = "串口接受数据失败";
            }
            if (num == -2)
            {
                status = "状态信息，卡纸 ";
            }
            if (num == -5)
            {
                status = "盖章机错误，设备未就绪，不可进行盖章，如正在走纸、正在印章状态。";
            }
            if (num == -8)
            {
                status = "其他错误，请检查设备";
            }

            return status;
        }

        //盖章机停止状态
        public static string StampStoptatus(int num)
        {
            string status = null;
            if (num == 0)
            {
                status = "成功停止";
            }
            if (num == -1)
            {
                status = "设备运行状态错误(盖章完毕后，印章电机还在运行)";
            }

            return status;
        }
    }
}
