using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.ComponentModel;

namespace Upgrade
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void showWarn(string message)
        {
            MessageBox.Show(message);
        }


        private void Down_Click(object sender, RoutedEventArgs e)
        {
            Function.Visibility = Visibility.Hidden;
            Updrade.Visibility = Visibility.Visible;


            string url = "http://xc213618.ddns.net:9090/s/REC.zip";

            Uri uri = new Uri(url);
            WebClient webClient = new WebClient();

            webClient.DownloadProgressChanged += client_DownloadProgressChanged;
            webClient.DownloadFileCompleted += client_DownloadFileCompleted;

            webClient.DownloadFileAsync(uri, "REC.zip");


        }


        private void Upgrade()
        {
            try
            {
                Process[] existing = Process.GetProcessesByName("REC");
                foreach (Process p in existing)
                {
                    string path = p.MainModule.FileName;
                    if (path == Path.Combine(Environment.CurrentDirectory, "REC.exe"))
                    {
                        p.Kill();
                        p.WaitForExit(100);
                    }
                }
            }
            catch (Exception ex)
            {
                showWarn("Failed to close EXC(关闭EXC失败).\n" + "Close it manually, or the upgrade may fail.(请手动关闭正在运行的EXC，否则可能升级失败。\n\n" + ex.StackTrace);
            }
            StringBuilder sb = new StringBuilder();
            try
            {

                string thisAppOldFile = Environment.CurrentDirectory + ".tmp";
                File.Delete(thisAppOldFile);
                string startKey = "Debug/";


                using (ZipArchive archive = ZipFile.OpenRead("REC.zip"))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        try
                        {
                            if (entry.Length == 0)
                            {
                                continue;
                            }
                            string fullName = entry.FullName;
                            if (fullName.StartsWith(startKey))
                            {
                                fullName = fullName.Substring(startKey.Length, fullName.Length - startKey.Length);
                            }


                            string entryOuputPath =   Path.Combine(Environment.CurrentDirectory,fullName);

                            FileInfo fileInfo = new FileInfo(entryOuputPath);
                            fileInfo.Directory.Create();
                            entry.ExtractToFile(entryOuputPath, true);
                        }
                        catch (Exception ex)
                        {
                            sb.Append(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                showWarn("Upgrade Failed(升级失败)." + ex.StackTrace);
                return;
            }
            if (sb.Length > 0)
            {
                showWarn("Upgrade Failed,Hold ctrl + c to copy to clipboard.\n" +
                    "(升级失败,按住ctrl+c可以复制到剪贴板)." + sb.ToString());
                return;
            }

            Process.Start("REC.exe");
            Close();
        }


        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            TotalLabel.Content = "正在下载";
            Upgrade();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBarDown.Minimum = 0;
            ProgressBarDown.Maximum = (int)e.TotalBytesToReceive;
            ProgressBarDown.Value = (int)e.BytesReceived;
            InofLabel.Content = e.ProgressPercentage + "% " + string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        
       
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
