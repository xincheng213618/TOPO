using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseDLL
{
    public class PStamp
    {
        private const string DLLPath = @"BaseDLL\Stamp\PStamp\PStamp.dll";
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
        [DllImport(DLLPath, EntryPoint = "psOpen", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsOpen(int tiCommPort);

        //断开连接， 关闭串口
        [DllImport(DLLPath, EntryPoint = "psClose", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsClose();

        // 获得完成 盖章的页数
        [DllImport(DLLPath, EntryPoint = "psCount", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsCount();

        // 获得尚 未盖章的页数
        [DllImport(DLLPath, EntryPoint = "psLeft", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsLeft();


        // 启动盖章 tiMode=0/1=盖完返回 /发完指令立即返回
        [DllImport(DLLPath, EntryPoint = "psStamp", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsStamp(int tiMode);


        // 获取状态
        [DllImport(DLLPath, EntryPoint = "psStatus", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsStatus();


        // 停止盖章
        [DllImport(DLLPath, EntryPoint = "psStop", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsStop();

        // 暂停盖章任务
        [DllImport(DLLPath, EntryPoint = "psSuspend", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSuspend();

        // 设置速度
        [DllImport(DLLPath, EntryPoint = "psSpeedSet", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSpeedSet(int tiSpeed);

        // 设置过纸页数
        [DllImport(DLLPath, EntryPoint = "psSetNone", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSetNone(int tiPage);

        // 设置盖一个章的页数
        //tiLeftCount 如果等于 1，章盖在左侧；如果是 0，章盖在右侧
        //tiPos 盖章的位置，单位 mm，限制在由 psParamPos设置的最顶和最底之间
        [DllImport(DLLPath, EntryPoint = "psSetOne", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSetOne(int tiPage, int tiLeftCount, int tiPos);

        //设置 整页 盖章的页数 ，同侧的有效章位要按由小到大的顺序排列 
        //tiPage 页数 ，页数要大于零
        //tiLeftPos1 tiLeftPos2 tiLeftPos3 设置 左侧三 个 章的位置
        //tiRightPos1 tiRightPos2 tiRightPos3右侧三 个 章的位置
        //如果章位 为 -1 表示少盖一个章；有效章位将被排序；不适合的章位将被调整
        [DllImport(DLLPath, EntryPoint = "psSetPage", CallingConvention = CallingConvention.StdCall)]
        public static extern int PsSetPage(int tiPage, int tiLeftPos1, int tiLeftPos2, int tiLeftPos3, int tiRightPos1, int tiRightPos2, int tiRightPos3);

    }
}
