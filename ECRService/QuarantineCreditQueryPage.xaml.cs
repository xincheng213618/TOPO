using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.InteropServices;
using BaseDLL;
using System.Xml;
using System.IO.Compression;

namespace ECRService
{
    /// <summary>
    /// QuarantineCreditQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class QuarantineCreditQueryPage : Page
    {
        public QuarantineCreditQueryPage()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            FocusManager.SetFocusedElement(this, institution);
            Countdown_timer();
            DataContext = MainWidownData;
        }
        private DispatcherTimer pageTimer = null;
        private MainWidownData MainWidownData = new MainWidownData() { Countdown = 90 };

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--MainWidownData.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }



        private void Query_Click(object sender, RoutedEventArgs e)
        {
            if (institution.Text.Length == 0)
            {
                hintLabel.Content = "请输入统一社会信用代码/组织机构代码！";
                hintBorder.Visibility = Visibility.Visible;
            }
            else if (record.Text.Length == 0)
            {
                hintLabel.Content = "请输入备案号码！";
                hintBorder.Visibility = Visibility.Visible;
            }
            else
            {
                string CompanyName = institution.Text;
                string USCI = record.Text; 
                Thread worker = new Thread(() => Requests(CompanyName, USCI, "2"))
                {
                    IsBackground = true
                };
                worker.Start();
            }
        }

        private void Requests(string CompanyName, string USCI,string Kinds)
        {
            string response = Webservice.NanJing.GetReport(CompanyName, USCI, Kinds);
            Dispatcher.BeginInvoke(new Action(() => Phrase(response)));

        }
        private void Phrase(string response)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(response);

                string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;
                if (returncode.CompareTo("1") == 0)
                {
                    using (Stream stream = new MemoryStream(Convert.FromBase64String(document.SelectSingleNode("//data/result/report/content").InnerText)))
                    {
                        using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                            ZipFileExtensions.ExtractToDirectory(archive, ".");
                    }
                    string identifier = document.SelectSingleNode("//data/result/report/id").InnerText;
                    string Filename = $"{identifier}.pdf";
                    Content = new QuarantineCreditCertificatePage(Filename);
                    Pages();
                }
                else
                {
                    Content = new HomePage(returnmsg);
                    Pages();
                }
            }
            catch
            {
                Content = new HomePage("接口解析异常");
                Pages();
            }


        }






        private void Institution_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
        }

        private void Record_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
        }


        private void PageChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePages":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Return":
                    Content = new PrintIndexPage();
                    Pages();
                    break;
            }
        }
    }
}
