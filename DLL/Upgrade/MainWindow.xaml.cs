using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Windows;

namespace Upgrade
{

    //开机的时候访问请求一次获取到版本和连接。然后根据连接去下载并安装 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://xc213618.ddns.net:9090/s/Debug.rar";
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, "Debug.rar");

            //try
            //{
            //    Process[] existing = Process.GetProcessesByName("EXC");
            //    foreach (Process p in existing)
            //    {
            //        string path = p.MainModule.FileName;
            //        if (path == GetPath("EXC.exe"))
            //        {
            //            p.Kill();
            //            p.WaitForExit(100);
            //        }  
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Access may be denied without admin right. The user may not be an administrator.
            //    showWarn("Failed to close EXC(EXC).\n" +
            //        "Close it manually, or the upgrade may fail.(请手动关闭正在运行的EXC，否则可能升级失败。\n\n" + ex.StackTrace);
            //}

            //StringBuilder sb = new StringBuilder();
            //try
            //{
            //    if (!File.Exists(fileName))
            //    {
            //        if (File.Exists(defaultFilename))
            //        {
            //            fileName = defaultFilename;
            //        }
            //        else
            //        {
            //            showWarn("Upgrade Failed, File Not Exist(升级失败,文件不存在).");
            //            return;
            //        }
            //    }
            //    string thisAppOldFile = Environment.CurrentDirectory + "EXC.exe";
            //    File.Delete(thisAppOldFile);


            //}
            //catch (Exception ex)
            //{
            //    showWarn("Upgrade Failed(升级失败)." + ex.StackTrace);
            //    return;
            //}
            //if (sb.Length > 0)
            //{
            //    showWarn("Upgrade Failed,Hold ctrl + c to copy to clipboard.\n" +
            //        "(升级失败,按住ctrl+c可以复制到剪贴板)." + sb.ToString());
            //    return;
            //}

            //Process.Start("EXC.exe");
            //MessageBox.Show("Upgrade successed(升级成功)");

            //Close();
        }




        public static string GetPath(string fileName)
        {
            string startupPath = Environment.CurrentDirectory;
            if (string.IsNullOrEmpty(fileName))
            {
                return startupPath;
            }
            return Path.Combine(startupPath, fileName);
        }


        private void showWarn(string message)
        {
            MessageBox.Show(message);
        }

    }
}
