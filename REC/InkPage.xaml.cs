using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BaseUtil;
namespace REC
{
    /// <summary>
    /// InkPage.xaml 的交互逻辑
    /// </summary>
    public partial class InkPage : Window
    {

        DrawingAttributes drawingAttributes;
        public InkPage()
        {
            Topmost = true;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            drawingAttributes = new DrawingAttributes();
            inkCanvas.DefaultDrawingAttributes = drawingAttributes;

            drawingAttributes.Width = 4;
            drawingAttributes.Height = 4;
            drawingAttributes.FitToCurve = true;
        }
        //2020.12.29 重构签名的部分
        public BitmapImage InkBitmapImage = new BitmapImage();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Tag)
            {
                case "Save":
                    string file = "Temp\\ink.png";
                    Bitmap bit = new Bitmap(900, 510);//截取的大小 为100 * 100
                    Graphics g = Graphics.FromImage(bit);
                    g.CopyFromScreen(new System.Drawing.Point(510, 290), new System.Drawing.Point(0, 0), bit.Size);
                    bit.Save(file);
                    InkBitmapImage = Covert.FileToImage(file);
                    this.Close();
                    break;
                case "Clear":
                    inkCanvas.Strokes.Clear();
                    break;
                default:
                    break;

            }
        }


    }
}
