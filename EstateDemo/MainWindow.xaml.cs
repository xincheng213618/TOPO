using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using BaseDLL;

namespace EstateDemo
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
            for (int i = 1; i < 10; i++)
            {
                comboBox.Items.Add(i);
            }
            comboBox.SelectedIndex = 0;
            estate.devMsg += RealEstate_Dev_devMsg;
        }
        Estate estate = new Estate();

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Open":
                    int i = estate.Open1(int.Parse(comboBox.Text));
                    MessageBox.Show(estate.OpenCode[i]);
                    break;
                case "Version":
                    estate.SEL_version();
                    break;
                case "Print":
                    //PrintPDF();
                    MessageBox.Show(estate.YES_OR_NO_Paper_.ToString());
                    estate.Process_Print();
                    break;
                case "Pages":
                    estate.SEL_P();
                    break;
                case "Reset":
                    estate.RESET_();
                    break;
                case "Close":
                    PrintPDF();
                    break;
                default:
                    break;
            }
        }

        static PrintDocument print = new PrintDocument();

        private void PrintPDF()
        {
            PrintDocument ZDDocument = new PrintDocument();
            ZDDocument.OriginAtMargins = true;
            //是否为纵向打印
            ZDDocument.DefaultPageSettings.Landscape = true;

            ZDDocument.PrinterSettings.PrinterName = print.PrinterSettings.PrinterName;
            ZDDocument.PrinterSettings.PrintToFile = false;
            PrintController pc = new StandardPrintController();
            ZDDocument.PrintController = pc;

            PaperSize ps = new PaperSize("Custom Size 1", Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书高度"].ToString()) / 10.4 * 100), Convert.ToInt32(Convert.ToInt32(ConfigurationManager.AppSettings["不动产权证书宽度"].ToString()) / 12.4 * 100));
            ZDDocument.DefaultPageSettings.PaperSize = ps;

            //ZDDocument.DefaultPageSettings.PrinterSettings=new PrinterSettings()
            ZDDocument.DefaultPageSettings.Margins = new Margins(Convert.ToInt32(ConfigurationManager.AppSettings["左边距"].ToString()), 0, 0, 0);
            //ZDDocument.BeginPrint += new PrintEventHandler(ZDDocument_BeginPrint);
            ZDDocument.PrintPage += new PrintPageEventHandler(delegate (object sender2, PrintPageEventArgs e2)
            {
                e2.PageSettings.Margins.Left = 0;
                e2.PageSettings.Margins.Top = 0;
                e2.Graphics.PageUnit = GraphicsUnit.Millimeter;

                ///宽预留八十


                e2.Graphics.DrawString("登记结构" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(256, 270), new StringFormat());
                e2.Graphics.DrawString("2018年05月15日" + "\r\n", new Font("宋体", 12), Brushes.Black, new PointF(252, 275), new StringFormat());
                e2.HasMorePages = false;
            });
            {
                ZDDocument.Print();
            }

        }


        private void RealEstate_Dev_devMsg(object sender, int Code, string data)
        {
            MessageBox.Show(data, Code.ToString());
        }



        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int[] newArray = new int[nums1.Length + nums2.Length];
            Array.Copy(nums1, 0, newArray, 0, nums1.Length);
            Array.Copy(nums2, 0, newArray, nums1.Length, nums2.Length);
            Array.Sort(newArray);
            int l, r;

            if ((newArray.Length & 1)==1)
            {
  
                l = r = newArray.Length >> 1;
            }
            else
            {
                l  = (newArray.Length >> 1)  -1;
                r  = (newArray.Length >> 1) ;
            }
            return ((double)newArray[l] + (double)newArray[r]) / 2;
        }
    }
}
