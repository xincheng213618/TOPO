using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml;
using ECRService.ReportServiceReference;
using System.ComponentModel;

namespace ECRService
{
    /// <summary>
    /// QuarantineCreditCertificatePage.xaml 的交互逻辑
    /// </summary>
    public partial class QuarantineCreditCertificatePage : Page
    {
        string FileName;
        public QuarantineCreditCertificatePage(string FileName)
        {
            this.FileName = FileName;
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            Countdown_timer();

            AcrobatHelper.pdfControl = new AxAcroPDFLib.AxAcroPDF();
            AcrobatHelper.pdfControl.BeginInit();
            formsHost.Child = AcrobatHelper.pdfControl;
            AcrobatHelper.pdfControl.EndInit();
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

        private void OpenPDF(string PDFpath)
        {
            try
            {
                AcrobatHelper.pdfControl.LoadFile(PDFpath);
                AcrobatHelper.pdfControl.setShowScrollbars(false);
                AcrobatHelper.pdfControl.setShowToolbar(false);
                AcrobatHelper.pdfControl.setView("FitH");
                formsHost.Visibility = Visibility.Visible;
                formsHost2.Visibility = Visibility.Visible;

                hintBorder2.Visibility = Visibility.Visible;
                hintLabel2.Content = "上下滑动翻页";
            }
            catch
            {
                Content = new HomePage("PDF打开失败请检查PDF文档是否正确");
                Pages();
            }
        }


        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new QuarantineCreditQueryPage();
            Pages();
        }

        private Timer timer;
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            AcrobatHelper.pdfControl.printAll();
            printButton.Visibility = Visibility.Hidden;
            Webservice.NanJing.updatereportstatus(FileName, "2");
            pageTimer.IsEnabled = false;
            hintLabel2.Content = "正在打印出入境检验检疫企业信用等级证明……";
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompletedInfo())), null, TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);
        }

        private void PrintCompletedInfo()
        {
            hintLabel2.Content = "打印完成，请取走出入境检验检疫企业信用等级证明！";

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => PrintCompleted())), null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }

        private void PrintCompleted()
        {
            Content = new HomePage();
            Pages();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            AcrobatHelper.pdfControl.gotoNextPage();
        }

        private void pre_Click(object sender, RoutedEventArgs e)
        {
            AcrobatHelper.pdfControl.gotoPreviousPage();
        }
    }
}
