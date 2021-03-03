using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public BitmapImage InkBitmapImage = new BitmapImage();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Tag)
            {
                case "Save":
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 0, 0, PixelFormats.Pbgra32);
                    rtb.Render(this.inkCanvas);
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    string file = "temp.png";
                    using (Stream stm = File.Create(file))
                    {
                        encoder.Save(stm);
                    }
                    BmpBitmapEncoder encoder1 = new BmpBitmapEncoder();
                    encoder1.Frames.Add(BitmapFrame.Create(rtb));  

                    using (MemoryStream stream = new MemoryStream())
                    {
                        encoder1.Save(stream);
                        InkBitmapImage.BeginInit();
                        InkBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        InkBitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        InkBitmapImage.StreamSource = new MemoryStream(stream.ToArray());
                        InkBitmapImage.EndInit();
                    }

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
