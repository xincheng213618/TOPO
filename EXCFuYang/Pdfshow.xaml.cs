using BaseUtil;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace XinHua
{
    /// <summary>
    /// Pdfshow.xaml 的交互逻辑
    /// </summary>
    public partial class Pdfshow : Page
    {
        private int PageNum = 1;
        private int PageAllNum = 0;
        private bool PrintRun = false;
        private int PrintAllNum = 0;
        private string filePath = null;
        private bool AllowPrint = true;
        private List<Border> list;
        private AxAcroPDFLib.AxAcroPDF pdfControl = null;
        public Pdfshow(int PrintAllNum = 10000)
        {
            this.PrintAllNum = PrintAllNum;
            InitializeComponent();

            list = new List<Border>() { Button8 };
            for (int i = 0; i < list.Count; i++)
                list[i].Visibility = Visibility.Hidden;
        }
        public Pdfshow(string filePath, int PrintAllNum = 200, bool AllowPrint = true)
        {
            this.filePath = filePath;
            this.PrintAllNum = PrintAllNum;
            this.AllowPrint = AllowPrint;
            InitializeComponent();
            list = new List<Border>() { Button0, Button8 };
            if (Global.Related.PageType != "")
            {
                list = new List<Border>() { Button0 };
            }
            for (int i = 0; i < list.Count; i++)
                list[i].Visibility = Visibility.Hidden;
            OpenPDF(filePath);
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.DataContext = Time;
            Countdown_timer();

            pdfControl = new AxAcroPDFLib.AxAcroPDF();
            pdfControl.BeginInit();
            formsHost.Child = pdfControl;
            pdfControl.EndInit();

        }
        private ObservableCollection<CheckItem> PDFItem;
        
        private void OpenPDF(string PDFpath)
        {
            pdfControl.LoadFile(PDFpath);
            pdfControl.setShowScrollbars(false);
            pdfControl.setShowToolbar(false);
            
            PdfReader reader = new PdfReader(PDFpath);
            PageAllNum = reader.NumberOfPages;
            reader.Close();
            if (PageAllNum != 0)
            {
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
       
        //倒计时模块
        private DispatcherTimer pageTimer = null;
        private TimeCount Time = new TimeCount();

        private void Countdown_timer()
        {
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
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

        private void OpenPDF_Click(object sender, RoutedEventArgs e)
        {
            filePath = XFile.OpenFileDialog("所有PDF文件(*.pdf)|*.pdf");
            if (filePath != null)
                OpenPDF(filePath);
        }
        private void PrintPDFOne_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintRun)
            {
                PrintRun = true;
                pageTimer.IsEnabled = false;
                PrintUtilWindow printUtil = new PrintUtilWindow(1);
                printUtil.Show();
                pdfControl.printPagesFit(PageNum, PageNum, true);
            }
        }

        private void PrintPDFAll_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintRun)
            {
                pageTimer.IsEnabled = false;
                PrintRun = true;
                PrintUtilWindow printUtil = new PrintUtilWindow(PageAllNum);
                printUtil.Closed += PrintOver;
                printUtil.Show();
                pdfControl.printAllFit(true);
            }
        }

        private void PrintOver(object sender, EventArgs e)
        {
            PDFShow.Visibility = Visibility.Hidden;
            Content = new PrintTips();
            Pages();
        }


        private void Fistpdfpage_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.gotoFirstPage();
            PDFListView.SelectedIndex = 0;
        }

        private void Lastpdfpage_Click(object sender, RoutedEventArgs e)
        {
            pdfControl.gotoLastPage();
            PDFListView.SelectedIndex = PageAllNum - 1;
        }

        private void Prepdfpage_Click(object sender, RoutedEventArgs e)
        {
            if (PDFListView.SelectedIndex > 0)
            {
                pdfControl.gotoPreviousPage();
                PDFListView.SelectedIndex -= 1;
            }
        }

        private void Nextpdfpage_Click(object sender, RoutedEventArgs e)
        {
            if (PDFListView.SelectedIndex < PageAllNum - 1)
            {
                pdfControl.gotoNextPage();
                PDFListView.SelectedIndex += 1;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (PDFListView.SelectedIndex > -1)
            {
                pdfControl.setCurrentPage(PDFItem.ElementAt(PDFListView.SelectedIndex).ListNo);
                PageNumLabel.Content = (PDFListView.SelectedIndex + 1).ToString() + " / " + PageAllNum.ToString();
                PageNum = PDFListView.SelectedIndex + 1;
                Time.Countdown = 60;
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            PDFItem.ElementAt(int.Parse(checkbox.Tag.ToString()) - 1).Ischeck = checkbox.IsChecked.Value;
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pageTimer.IsEnabled = !pageTimer.IsEnabled;
        }
    }
}
