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
        private int PageAllNum = 0;
        private string FilePath;
        RECData rECListView;

        public FunctionPage2(RECData rECListView)
        {
            this.FilePath = rECListView.FileName;
            this.rECListView = rECListView;
            InitializeComponent();
            OpenPDF(FilePath);
        }

        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount();
        private void Page_Initialized(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => Media.Play("Base\\Media\\请确认证书信息是否正确，无误后，请先签字然后打印证书.mp3")));
            TopGrid.DataContext = timeCount;
            Countdown_timer();

            AcrobatHelper.pdfControl.BeginInit();
            formsHost.Child = AcrobatHelper.pdfControl;
            AcrobatHelper.pdfControl.EndInit();

        }

        private void OpenPDF(string PDFpath)
        {
            try
            {
                PdfReader reader = new PdfReader(Environment.CurrentDirectory +"//"+ FilePath);
                PageAllNum = reader.NumberOfPages;
                reader.Close();
                reader.Dispose();
                if (PageAllNum > 0)
                {
                    AcrobatHelper.pdfControl.LoadFile(PDFpath);
                    AcrobatHelper.pdfControl.setShowScrollbars(false);
                    AcrobatHelper.pdfControl.setShowToolbar(false);

                    AcrobatHelper.pdfControl.gotoNextPage();
                    CurrentNum =2;
                    NextPDFBorder.Background = (Brush)Use1.ConvertFrom("#60d0ff");
                    PrePDFBorder.Background = (Brush)Use1.ConvertFrom("#60d0ff");
                }
                else
                {
                    Content = new HomePage("PDF打开失败,请联系管理人员");
                    Pages();
                }
            }
            catch
            {
                Content = new HomePage("PDF打开失败,请联系管理人员");
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
                    Content = new FunctionPage();
                    Pages();
                    break;
                case "NextPDF":
                    if (CurrentNum < PageAllNum)
                    {
                        AcrobatHelper.pdfControl.gotoNextPage();
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
                        AcrobatHelper.pdfControl.gotoPreviousPage();
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
                        Content = new FunctionPage3(rECListView.PrintName, rECListView);
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
            PrintBorder.BorderBrush = Brushes.HotPink;
            InkBorder.BorderBrush = Brushes.White;
            Requests.Ink_Upload(Global.Related.IDCardData.IDCardNo, Global.Related.IDCardData.Name, "Temp//ink.png");
        }

    }
}
