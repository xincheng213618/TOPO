﻿using BaseDLL;
using BaseUtil;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RECSuzhou
{
    /// <summary>
    /// Pdfshow.xaml 的交互逻辑
    /// </summary>
    public partial class Pdfshow : Page
    {
        private int PageNum = 1;
        private int PageAllNum = 0;
        private string filePath = null;
       
        private List<Border> list;
        private bool PrintRun = false;
        private bool AllowPrint = true;
        private bool PrintAll = true;

        public Pdfshow(int PrintAllNum = 1000)
        {
            this.PrintAllNum = PrintAllNum;
            InitializeComponent();

            list = new List<Border>() { Button8 };
            for (int i = 0; i < list.Count; i++)
                list[i].Visibility = Visibility.Hidden;
        }
        int PrintAllNum;

       
        public Pdfshow(string filePath, int PrintAllNum = 1000, bool AllowPrint = true)
        {
           
            this.filePath = filePath;
            this.PrintAllNum = PrintAllNum;
            this.AllowPrint = AllowPrint;
            InitializeComponent();

            list = new List<Border>() { Button0, Button8 };
            if (Global.Related.PageType != "")
                list = new List<Border>() { Button0 };

            
            for (int i = 0; i < list.Count; i++)
                list[i].Visibility = Visibility.Hidden;

            Dispatcher.BeginInvoke(new Action(() => OpenPDF(filePath)));
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            this.DataContext = Time;
            Countdown_timer();

            AcrobatHelper.pdfControl = new AxAcroPDFLib.AxAcroPDF();
            AcrobatHelper.pdfControl.BeginInit();
            formsHost.Child = AcrobatHelper.pdfControl;
            AcrobatHelper.pdfControl.EndInit();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            switch (Global.Related.PageType)
            {
                case "DegreePages":
                    Content = new DegreePage();
                    break;
                case "OwnerShipPages":
                    Content = new OwnerShipPages();
                    break;
                case "SZHQArchivePages":
                    Content = new SZArchivePage();
                    break;
                case "SZWZArchivePages":
                    Content = new SZArchivePage();
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
            if (PDFListView.SelectedIndex < PageAllNum - 1)
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
            PDFListView.SelectedIndex = PageAllNum - 1;
        }

        private void OpenPDF_Click(object sender, RoutedEventArgs e)
        {
            filePath = XFile.OpenFileDialog("所有PDF文件(*.pdf)|*.pdf");
            if (filePath != null)
                OpenPDF(filePath);
        }
        private ObservableCollection<CheckItem> PDFItem;
        private void OpenPDF(string PDFpath)
        {
            try
            {
                Log.Write(PDFpath);
                AcrobatHelper.pdfControl.LoadFile(PDFpath);
                AcrobatHelper.pdfControl.setShowScrollbars(false);
                AcrobatHelper.pdfControl.setShowToolbar(false);

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
                        //item.CheckVisible = PageAllNum > 3 ? "Visible" : "Hidden";
                        item.CheckVisible = Global.Related.PageType == "SZHQArchivePages" ? "Visible" : "Hidden";
                        PDFItem.Add(item);
                    }

                    PDFListView.SelectedIndex = 0;

                    PDFListView.Visibility =  Visibility.Visible;
                    Button1.Visibility = AllowPrint && Global.Related.PageType != "SZWZArchivePages" && Global.Related.PageType != "DegreePages" ? Visibility.Visible : Visibility.Hidden;
                    Button2.Visibility = AllowPrint && Global.Related.PageType != "DegreePages" ? Visibility.Visible : Visibility.Hidden;
                    Button3.Visibility = PageAllNum <= 1 ? Visibility.Hidden : Visibility.Visible;
                    Button7.Visibility = PageAllNum <= 3 ? Visibility.Hidden : Visibility.Visible;
                    Button6.Visibility = PageAllNum <= 2 ? Visibility.Hidden : Visibility.Visible;
                    Button5.Visibility = PageAllNum <= 2 ? Visibility.Hidden : Visibility.Visible;
                    //Button1Label.Content = PageAllNum < PrintAllNum ? "全部打印" : "打印已经选择的页面";
                    Button1Label.Content = Global.Related.PageType != "SZHQArchivePages" ? "全部打印" : "打印已经选择的页面";
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
            PrintUtilWindow printUtil = new PrintUtilWindow(1);
            printUtil.Closed += PrintOneOver;
            printUtil.Show();
            //Stamp.Start(1);
            int run = Stamp.Start(1);
            Log.Write("启动盖章机：" + run);
            //if (!"0".Equals(run.ToString()))
            //{
            //    Content = new HomePage("盖章机启动失败，请重启盖章机");
            //    Pages();
            //    return;
            //}
            AcrobatHelper.pdfControl.printPagesFit(PageNum, PageNum, true);
        }
        private void PrintOneOver(object sender, EventArgs e)
        {
            pageTimer.IsEnabled = true;
            Stamp.Close();
        }
        private AxAcroPDFLib.AxAcroPDF pdfc;
        private int PageAllNumNew = 0;
        private void PrintPDFAll_Click(object sender, RoutedEventArgs e)
        {
            if (!PrintRun)
            {
                pageTimer.IsEnabled = false;
                if (PrintAll && Global.Related.PageType == "SZHQArchivePages")
                {                   
                    PopAlert(3);
                    //PrintRun = true;
                    //PrintUtilWindow printUtil = new PrintUtilWindow(PageAllNum);
                    //printUtil.Closed += PrintOver;
                    //printUtil.Show();
                    //Stamp.Start(PageAllNum);
                    //int run = Stamp.Start(PageAllNum);
                    //Log.Write("启动盖章机：" + run);
                    ////if (!"0".Equals(run.ToString()))
                    ////{
                    ////    Content = new HomePage("盖章机启动失败，请重启盖章机");
                    ////    Pages();
                    ////    return;
                    ////}
                    //AcrobatHelper.pdfControl.printAllFit(true);
                    return;
                }else if(PrintAll && Global.Related.PageType != "SZHQArchivePages")
                    {
                    PrintRun = true;
                    PrintUtilWindow printUtil = new PrintUtilWindow(PageAllNum);
                    printUtil.Closed += PrintOver;
                    printUtil.Show();
                    //Stamp.Start(PageAllNum);
                    int run = Stamp.Start(PageAllNum);
                    Log.Write("启动盖章机：" + run);
                    //if (!"0".Equals(run.ToString()))
                    //{
                    //    Content = new HomePage("盖章机启动失败，请重启盖章机");
                    //    Pages();
                    //    return;
                    //}
                    AcrobatHelper.pdfControl.printAllFit(true);
                    return;
                }
                
                else if(!PrintAll){ 
                
                string Pa = "";

                for (int i = 0; i < PDFItem.Count; i++)
                    Pa += PDFItem.ElementAt(i).Ischeck ? PDFItem.ElementAt(i).ListNo.ToString() + "," : "";

                if (Pa == "")
                    return;
                //只允许一次最多打印5张，怕打印文件过大，打印机吃不消
                //string[] strArray = Pa.Split(','); //字符串转数组

                //if (strArray.Length > 5)
                //{
                //    PopAlert(3);
                //    return;
                //}
                PdfReader reader = new PdfReader(filePath);
                reader.SelectPages(Pa);

                PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(@"Temp\temp.pdf", FileMode.Create));
                pdfStamper.Close();

                pdfc = new AxAcroPDFLib.AxAcroPDF();
                pdfc.BeginInit();
                formsHost1.Child = pdfc;
                pdfc.EndInit();
                pdfc.LoadFile(@"Temp\temp.pdf");
                PageAllNumNew = reader.NumberOfPages;
                PrintUtilWindow printUtil1 = new PrintUtilWindow(PageAllNumNew);
                printUtil1.Closed += PrintOneOver;
                printUtil1.Show();
                Stamp.Start(PageAllNumNew);
                pdfc.printAllFit(true);
                }

            }
        }

        private void PrintOver(object sender, EventArgs e)
        {
            //Stamp.Close();
            PDFShow.Visibility = Visibility.Hidden;
            Content = new PrintTips();
            Pages();
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

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PDFListView.SelectedIndex > 0)
            {               
                AcrobatHelper.pdfControl.setCurrentPage(PDFItem.ElementAt(PDFListView.SelectedIndex).ListNo);
                PageNumLabel.Content = (PDFListView.SelectedIndex + 1).ToString() + " / " + PageAllNum.ToString();
                PageNum = PDFListView.SelectedIndex + 1;
                Time.Countdown = 60;
                
            }
        }


        private void ProgressBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pageTimer.IsEnabled = !pageTimer.IsEnabled;
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
        string Pa;
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 60;
            string Pa = "";
            CheckBox checkbox = sender as CheckBox;
            PDFItem.ElementAt(int.Parse(checkbox.Tag.ToString()) - 1).Ischeck = checkbox.IsChecked.Value;
            for (int i = 0; i < PDFItem.Count; i++)
                Pa += PDFItem.ElementAt(i).Ischeck ? PDFItem.ElementAt(i).ListNo.ToString() + "," : "";

           

            string[] strArray = Pa.Split(','); //字符串转数组

            if (strArray.Length > 1)
            {
                PrintAll = false;
                Button1Label.Content =  "打印已经选择的页面";
            }
            else
            {   //虎丘要求关闭档案全部打印功能            
                PrintAll = true;
                //Button1Label.Content = "全部打印";
            }
           

        }
        private async void PopAlert(int time)
        {


            
            POP.Visibility = Visibility.Visible;

            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
        }

        private void CheckBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
            
        }

        
    }
}
