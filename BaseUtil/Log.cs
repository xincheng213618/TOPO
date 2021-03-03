using System;
using System.Diagnostics;
using System.IO;

namespace BaseUtil
{
    public class Log 
    {
        //Designed By Mr.Xin 2020.6.7
        public static string LogCache ="";
        public static string LogUrlCache = "";
        public static string LogExceptionCache ="";

        //Designed By Mr.Xin 2020.6.7
        public static string FilePath = Environment.CurrentDirectory + "\\Logs\\";

        public static void  LogInput()
        {
            if (LogCache != ""|| LogExceptionCache != ""|| LogUrlCache != "")
            {
                Initialized();
                if (LogCache != "")
                {
                    string path = FilePath + DateTime.Now.ToString("yyy-MM-dd") + ".log";
                    StreamWriter log = new StreamWriter(path,true);
                    log.Write(LogCache);
                    log.Flush();
                    log.Close();
                    LogCache = "";
                }
                if (LogExceptionCache != "")
                {
                    string path1 = $"{FilePath}{DateTime.Now:yyy-MM-dd}-Exception.log";
                    StreamWriter log = new StreamWriter(path1, true);
                    log.Write(LogExceptionCache);
                    log.Flush();
                    log.Close();
                    LogExceptionCache = "";
                }
                if (LogUrlCache != "")
                {
                    string path1 = $"{FilePath}{DateTime.Now:yyy-MM-dd}-Http.log";
                    StreamWriter log = new StreamWriter(path1, true);
                    log.Write(LogUrlCache);
                    log.Flush();
                    log.Close();
                    LogUrlCache = "";
                }
            }
        }

        public static void Initialized()
        {
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);
        }

        public static void Write(string logs)
        {
            logs = logs ?? "";
            LogCache += "Logs：当前时间：" + DateTime.Now.ToString() + Environment.NewLine + logs + Environment.NewLine+ Environment.NewLine;  
        }
        public static void WriteUrl( string Url,string response)
        {
            Url = Url ?? "";
            response = response ?? "";
            LogUrlCache += "Logs：当前时间：" + DateTime.Now.ToString() + Environment.NewLine + Url + Environment.NewLine + response + Environment.NewLine+ Environment.NewLine;
        }

        public static void WriteException(Exception ex)
        {
            Initialized();
            LogExceptionCache += "当前时间：" + DateTime.Now.ToString() + Environment.NewLine + "异常信息：" + ex.Message + Environment.NewLine + "异常对象：" + ex.Source + Environment.NewLine + "触发方法：" + ex.TargetSite + Environment.NewLine + "调用堆栈：" + ex.StackTrace.Trim() + Environment.NewLine;
        }

        public static void Clearall(string path = null)
        {
            Initialized();
            foreach (string name in Directory.GetFiles(FilePath))
                File.Delete(name);
        }

        [Obsolete]
        public static void SysLog()
        {
            EventLog eventLog = new EventLog();
            eventLog.Log = "System"; // System[系统日志] | Application[应用日志] | Security[安全日志]
            EventLogEntryCollection collection = eventLog.Entries;
            int Count = collection.Count;
            string info = "显示系统日志" + Count.ToString() + "个时间。";
            foreach (EventLogEntry entry in collection)
            {
                info += $"类型：{entry.EntryType.ToString()}{Environment.NewLine}";
                info += $"日期：{entry.TimeGenerated.ToLongDateString()}{Environment.NewLine}";
                info += $"时间：{entry.TimeGenerated.ToLongTimeString()}{Environment.NewLine}";
                info += $"来源：{entry.Source}{Environment.NewLine}";
                info += $"事件：{entry.EventID.ToString()}{Environment.NewLine}";
                info += $"用户：{entry.UserName}{Environment.NewLine}";
                info += $"计算机：{entry.MachineName}{Environment.NewLine}";
            }
        }
    }

}

