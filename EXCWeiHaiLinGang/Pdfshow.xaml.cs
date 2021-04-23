using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Windows.Threading;
using System.Collections.ObjectModel;

using System.Threading;
using BaseUtil;
using BaseDLL;
using System.Drawing.Printing;
using iTextSharp.text.pdf;

namespace EXC
{
    //Desgined By Mr.Xin 2020.5.6 逻辑页面重构与调用
    /// <summary>
    /// Pdfshow.xaml 的交互逻辑
    /// </summary>
    public partial class Pdfshow : Page
    {
        private int PageNum=1;
        private int PageAllNum =0;
        private string filePath = null;
        private List<Border> list;
        private bool PrintRun =false;
        private bool AllowPrint = true;

        public Pdfshow(int PrintAllNum =10000)
        {
            this.PrintAllNum = PrintAllNum;
            InitializeComponent();

            list = new List<Border>() { Button8 };
            for (int i = 0; i < list.Count; i++)
                list[i].Visibility = Visibility.Hidden;
        }
        int PrintAllNum;


        public Pdfshow(string filePath,int PrintAllNum =200,bool AllowPrint =true)
        {
            this.filePath = filePath;
            this.PrintAllNum = PrintAllNum;
            this.AllowPrint = AllowPrint;
            InitializeComponent();

            list = new List<Border>() { Button0, Button8 };
            if (Global.Related.PageType != null)
            {
                list = new List<Border>() { Button0 };
            }
            for (int i = 0; i < list.Count; i++)
                list[i].Visibility = Visibility.Hidden;  

            OpenPDF(filePath);
        }

        private void Page_Initialized(object sender, EventArgs e)
        {   Countdown_timer();
            this.DataContext = Time;
            AcrobatHelper.pdfControl.BeginInit();
            formsHost.Child = AcrobatHelper.pdfControl;
            AcrobatHelper.pdfControl.EndInit();

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            switch (Global.Related.PageType)
            {  
                case "ReportGRWeiHai":
                    Content = new HomePage();
                    break;
                case "ReportWeiHai":
                    Content = new HomePage();
                    break;
                default:
                    Content = new HomePage();
                    break;
            }
            Pages();
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }


        private void Prepdfpage_Click(object sender, RoutedEventArgs e)
        {
            if (PDFListView.SelectedIndex > 0)
            {
                AcrobatHelper.pdfControl.gotoPreviousPage();
                PDFListView.SelectedIndex -= 1;
            }
        }

        private void Nextpdfpage_Click(object sender, RoutedEventArgs e)
        {
            if (PDFListView.SelectedIndex < PageAllNum-1)
            {
                AcrobatHelper.pdfControl.gotoNextPage();
                PDFListView.SelectedIndex += 1;
            }

        }

        private void Fistpdfpage_Click(object sender, RoutedEventArgs e)
        {
            AcrobatHelper.pdfControl.gotoFirstPage();
            PDFListView.SelectedIndex = 0;

        }
        private void Lastpdfpage_Click(object sender, RoutedEventArgs e)
        {
            AcrobatHelper.pdfControl.gotoLastPage();
            PDFListView.SelectedIndex = PageAllNum-1;
        }

        private void OpenPDF_Click(object sender, RoutedEventArgs e)
        {
             filePath = XFile.OpenFileDialog("所有PDF文件(*.pdf)|*.pdf");
            if (filePath != null)
                OpenPDF(filePath);
        }
        private ObservableCollection<CheckItem> PDFItem ;
        private void OpenPDF(string PDFpath)
        {
            try
            {
                PageAllNum = PDF.PDFNum(PDFpath);
                if (PageAllNum != 0)
                {
                    AcrobatHelper.pdfControl.LoadFile(PDFpath);
                    AcrobatHelper.pdfControl.setShowScrollbars(false);
                    AcrobatHelper.pdfControl.setShowToolbar(false);

                    PageNumLabel.Content = PageNum.ToString() + " / " + PageAllNum.ToString();
                    if (PageAllNum == 1)
                    {
                        Button3.Visibility = Visibility.Hidden;
                        Button4.Visibility = Visibility.Hidden;
                        Button5.Visibility = Visibility.Hidden;
                        Button6.Visibility = Visibility.Hidden;
                        Button7.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Button3.Visibility = Visibility.Visible;
                        Button4.Visibility = Visibility.Visible;
                        Button5.Visibility = Visibility.Visible;
                        Button6.Visibility = Visibility.Visible;
                        Button7.Visibility = Visibility.Visible;
                    }

                    PDFItem = new ObservableCollection<CheckItem>();
                    PDFListView.ItemsSource = PDFItem;
                    for (int i = 1; i < PageAllNum + 1; i++)
                    {
                        CheckItem item = new CheckItem();
                        item.ListNo = i;
                        item.CheckVisible = PageAllNum > PrintAllNum ? "Visible" : "Hidden";
                        PDFItem.Add(item);
                    }

                    PDFListView.SelectedIndex = 0;
                    PDFListView.Visibility = PageAllNum <= 5 ? Visibility.Hidden : Visibility.Visible;
                    Button1.Visibility = AllowPrint ? Visibility.Visible : Visibility.Hidden;
                    Button2.Visibility = AllowPrint && PageAllNum > 4 ? Visibility.Visible : Visibility.Hidden;
                    Button3.Visibility = PageAllNum <= 1 ? Visibility.Hidden : Visibility.Visible;
                    Button7.Visibility = PageAllNum <= 3 ? Visibility.Hidden : Visibility.Visible;
                    Button6.Visibility = PageAllNum <= 2 ? Visibility.Hidden : Visibility.Visible;
                    Button5.Visibility = PageAllNum <= 2 ? Visibility.Hidden : Visibility.Visible;
                    Button1Label.Content = PageAllNum < PrintAllNum ? "全部打印" : "打印已经选择的页面";

                }
                else
                {
                    Content = new HomePage("PDF打开失败请检查PDF文档是否正确");
                    Pages();
                }
            }
            catch
            {
                Content = new HomePage("PDF打开失败请检查PDF文档是否正确");
                Pages();
            }

        }


        private void PrintPDFOne_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintRun)
            {
                PrintRun = true;
                PrintUtilWindow printUtil = new PrintUtilWindow(PageAllNum);
                printUtil.Show();
                AcrobatHelper.pdfControl.printPagesFit(PageNum, PageNum, true);
            }
        }

        private void PrintPDFAll_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintRun)
            {
                Http.HeFei.Upload();
                pageTimer.IsEnabled = false;
                PrintRun = true;
                PrintUtilWindow printUtil = new PrintUtilWindow(PageAllNum);
                printUtil.Closed += PrintOver;
                printUtil.Show();
                AcrobatHelper.pdfControl.printAllFit(true);
            }
        }

        private void PrintOver(object sender, EventArgs e)
        {
            PDFShow.Visibility = Visibility.Hidden;
            Content = new PrintTips();
            Pages();
        }

        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount() { Countdown = 90 };

        private void Countdown_timer()
        {
           
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                Time.Countdown -= 1;
                Timers.Content = Time.Countdown;
                if (Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PDFListView.SelectedIndex>-1)
            {
                AcrobatHelper.pdfControl.setCurrentPage(PDFItem.ElementAt(PDFListView.SelectedIndex).ListNo);
                PageNumLabel.Content = (PDFListView.SelectedIndex + 1).ToString() + " / " + PageAllNum.ToString();
                PageNum = PDFListView.SelectedIndex + 1;
                Time.Countdown = 60;
            }
        }


        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            PDFItem.ElementAt(int.Parse(checkbox.Tag.ToString()) - 1).Ischeck = checkbox.IsChecked.Value;
        }
    }
}
