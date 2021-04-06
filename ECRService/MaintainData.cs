using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ECRService
{
    public class MaintainData
    {
        public static bool maintainLock = false;
        public static Timer maintainLockTimer = null;
        public static Page returnPage = null;
    }
}
