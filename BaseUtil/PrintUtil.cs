using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtil
{
    public class PrintUtil
    {
          static PrintServer m_PrintServer = new PrintServer();
          PrintServer _PrintServer = new PrintServer();

        /// <summary>
        /// 所有打印机必须满足条件
        /// </summary>
        /// <returns></returns>
        public bool IsOfforNo()
        {

            foreach (PrintQueue queue in _PrintServer.GetPrintQueues())
            {
                if (queue.IsOffline || queue.NumberOfJobs > 0)//脱机和队列有任务满足一个
                {
                    return false;
                }
            }
            return true;

        }
        /// <summary>
        /// 指定打印机满足条件
        /// </summary>
        /// <param name="printName">指定打印机的名称</param>
        /// <returns></returns>
        public bool IsOfforNo(string printName)
        {
            PrintQueue queues = null;
            foreach (PrintQueue queue in _PrintServer.GetPrintQueues())
            {
                if (queue.Name == printName)
                {
                    queues = queue;
                    if (queue.IsOffline || queue.NumberOfJobs > 0)//脱机和队列有任务满足一个
                    {
                        return false;
                    }
                }
            }
            //if (queues==null)
            //{
            //    return false;
            //}
            return true;


        }

        public void delprint(string printName)
        {
            //PrintQueue queues = null;
            //foreach (PrintQueue queue in _PrintServer.GetPrintQueues())
            //{
            //    queue.PrintingIsCancelled;   
        //}
        }

    }
}
