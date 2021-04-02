using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer;
        public Service1()
        {
            InitializeComponent();
            timer = new Timer(_ => Run(), null, 0, 1000);
        }
        private bool Isable = false;

        protected override void OnStart(string[] args)
        {
            Log("Start");
            Isable = true;
        }



        protected override void OnStop()
        {
            Isable = false;
            Log("Stop");
        }
        protected override void OnPause()
        {
            Isable = false;
            Log("Pause");
        }
        protected override void OnContinue()
        {
            Isable = true;
            Log("Continue");
        }

        private void Run()
        {
             if (Isable)
            {
                Process[] existing = Process.GetProcessesByName("REC");
                if (existing.Length > 0)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Users\TOPO\Desktop\log.txt", true))
                    {
                        Log("REC 正在运行中");
                    }
                }
                else
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Users\TOPO\Desktop\log.txt", true))
                    {
                        Log("REC 已经关闭");
                    }
                }
            }
        }



        private void Log(string Msg)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Users\TOPO\Desktop\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + Msg);
            }


        }
    }
}
