using iTextSharp.text.pdf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace REC
{
    /// <summary>
    /// FunctionPage2.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPage2 : Page
    {
        private AxAcroPDFLib.AxAcroPDF pdfControl = null;
        private int PageAllNum = 0;
        private string FilePath;
        IDCardData iDCardData;
        public FunctionPage2(IDCardData iDCardData,string FilePath)
        {
            this.FilePath = FilePath;
            this.iDCardData = iDCardData;
            InitializeComponent();
            OpenPDF(FilePath);
        }

        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount();
        private void Page_Initialized(object sender, EventArgs e)
        {
            TopGrid.DataContext = timeCount;
            Countdown_timer();
            pdfControl = new AxAcroPDFLib.AxAcroPDF();
            pdfControl.BeginInit();
            formsHost.Child = pdfControl;
            pdfControl.EndInit();

        }

        private void OpenPDF(string PDFpath)
        {
            pdfControl.LoadFile(PDFpath);
            pdfControl.setShowScrollbars(false);
            pdfControl.setShowToolbar(false);

            PdfReader reader = new PdfReader(FilePath);
            PageAllNum = reader.NumberOfPages;
            reader.Close();

            if (PageAllNum == 0)
            {
                Content = new HomePage("PDF打开失败,请联系工作人员进行处理");
                Pages();
            }

        }

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1), };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown <= 0)
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

        int CurrentNum = 1;
        private BrushConverter Use1 = new BrushConverter();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "HomePage":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Return":
                    Content = new FunctionPage(iDCardData);
                    Pages();
                    break;
                case "NextPDF":
                    if (CurrentNum < PageAllNum)
                    {
                        pdfControl.gotoNextPage();
                        CurrentNum += 1;
                        if (CurrentNum == PageAllNum)
                        {
                            NextPDFBorder.Background = Brushes.Gray;
                        }
                        else
                        {
                            NextPDFBorder.Background = (Brush)Use1.ConvertFrom("#60d0ff");
                            PrePDFBorder.Background = (Brush)Use1.ConvertFrom("#60d0ff");
                        }
                    }
                    break;
                case "PrePDF":
                    if (CurrentNum >1)
                    {
                        pdfControl.gotoPreviousPage();
                        CurrentNum -= 1;
                        if (CurrentNum == 1)
                        {
                            PrePDFBorder.Background = Brushes.Gray;
                        }  
                        else
                        {
                            PrePDFBorder.Background = (Brush)Use1.ConvertFrom("#60d0ff");
                            NextPDFBorder.Background = (Brush)Use1.ConvertFrom("#60d0ff");
                        }
                    }
                    break;
                case "Signed":
                    timeCount.Countdown = 59;
                    inkPage = new InkPage();
                    inkPage.Closed += InkImageAdd;
                    inkPage.Show();
                    Signed = true;
                    break;
                case "Print":
                    if (Signed)
                    {

                        Content = new FunctionPage3();
                        Pages();
                    }
                    else
                    {
                        MessageBox.Show("请先签名");
                    }
                    break;
                default:
                    break;
            }

        }


        InkPage inkPage;
        bool Signed = false;

        private void InkImageAdd(object sender, EventArgs e )
        {
            InkPage inkPage = sender as InkPage;
            InkImage.Source = inkPage.InkBitmapImage;
            InkButtonLabel.Content = "重新签字";
            PrintBorder.Background =(Brush)Use1.ConvertFrom("#60d0ff");
        }

    }
}
