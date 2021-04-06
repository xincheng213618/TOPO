using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECRLibrary;

namespace ECRService
{
    public class GlobalData
    {
        // 静态数据
        public static ConfigData configData = null;

        // 状态数据
        public static bool explorerExist = false;
        public static bool busy = false;
    }
}
