using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BaseDLL;
using BaseUtil;

namespace sv
{
    public static class PDF
    {
        static BaseFont simhei = BaseFont.CreateFont("C:\\Windows\\Fonts\\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//获取系统的字体
        static Font simhei30 = new Font(simhei, 30, Font.BOLD);
        static Font simhei15 = new Font(simhei, 15, Font.NORMAL);


        public static bool PDFMark(string FilePath, string text)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            string tempPath = Path.GetDirectoryName(FilePath) + Path.GetFileNameWithoutExtension(FilePath) + "_temp.pdf";
            try
            {
                pdfReader = new PdfReader(FilePath);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(tempPath, FileMode.Create));
                int total = pdfReader.NumberOfPages + 1;
                Rectangle psize = pdfReader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                PdfContentByte content;
                BaseFont font = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\SIMFANG.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                PdfGState gs = new PdfGState();
                for (int i = 1; i < total; i++)
                {
                    content = pdfStamper.GetOverContent(i);//在内容上方加水印
                                                           //content = pdfStamper.GetUnderContent(i);//在内容下方加水印

                    gs.FillOpacity = 0.3f;  //透明度
                    content.SetGState(gs); //开始写入文本

                    content.BeginText();
                    content.SetColorFill(BaseColor.GRAY);
                    content.SetFontAndSize(font, 45);
                    content.SetTextMatrix(0, 0);
                    content.ShowTextAligned(Element.ALIGN_MIDDLE, text, 100, 100, 45);
                    content.EndText();
                }
                pdfStamper.Close();
                pdfReader.Close();

                File.Copy(tempPath, FilePath, true);
                File.Delete(tempPath);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public static void DrawPDF(string FilePath, IDCardData IDCardData, BitmapSource CardImage, BitmapSource FaceImage)
        {
            Document document = new Document(PageSize.A4, 25, 25, 25, 25);
            FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

            PdfWriter.GetInstance(document, stream);

            document.Open();
            document.NewPage();

            Image images1 = Image.GetInstance(IDCardDataToImage(IDCardData, CardImage));
            images1.ScaleToFit(336, 475);
            images1.SetAbsolutePosition(125, 370);
            document.Add(images1);

            Image images2 = Image.GetInstance(Covert.BitmapSourceToByte(FaceImage));
            images2.ScaleToFit(300, 450);
            images2.SetAbsolutePosition(150, 150);
            document.Add(images2);

            Paragraph PrintTime = new Paragraph(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "   在苏州市不动产登记中心虎丘分中心人证核验成功", simhei15);// 设置字体样式
            PrintTime.SetLeading(0, 48); //后面行数，前面是字数
            document.Add(PrintTime);
            document.Close();
        }


        public static byte[] IDCardDataToImage(IDCardData IDCardData, BitmapSource FaceImage)
        {
            //System.Drawing.Bitmap BmpBack = XCovert.BitmapMakeTransparent(new System.Drawing.Bitmap(@"Mr.Xin_0000000000000000.bmp"));
            //BitmapSource image = XCovert.CreateBitmapSourceFromBitmap(BmpBack); //原先的写法
            BitmapSource image = Covert.BitmapMakeTransparent(FaceImage);
            //获取背景图
            BitmapSource bgImage = new BitmapImage(new Uri(@"PDF\empty.png", UriKind.Relative));

            //创建一个RenderTargetBitmap 对象，将WPF中的Visual对象输出
            RenderTargetBitmap composeImage = new RenderTargetBitmap(bgImage.PixelWidth, bgImage.PixelHeight, bgImage.DpiX, bgImage.DpiY, PixelFormats.Default);

            //定义成新的字体对象
            FontFamily myFontFamily = new FontFamily(@"PDF\hei.ttf");
            FontFamily myFontFamily1 = new FontFamily(@"PDF\fzhei.ttf");

            Typeface heiti_font = new Typeface(myFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            Typeface fzhei_font = new Typeface(myFontFamily1, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            Typeface ocrb10bt_font = new Typeface(new FontFamily(Environment.CurrentDirectory + @"PDF\ocrb10bt.ttf"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            drawingContext.DrawImage(bgImage, new Rect(0, 0, bgImage.Width, bgImage.Height));

            drawingContext.DrawImage(image, new Rect(1500, 720, image.Width * 5.1, image.Height * 5.1));
            Drawing(drawingContext, $"{IDCardData.Name}", heiti_font, 75, new Point(650, 685), 85);

            string Sex = int.Parse(IDCardData.IDCardNo.Substring(16, 1)) % 2 == 0 ? "女" : "男";
            Drawing(drawingContext, $"{Sex}", heiti_font, 65, new Point(650, 835), 65);
            Drawing(drawingContext, $"{IDCardData.Nation}", heiti_font, 65, new Point(1030, 835), 65);

            Drawing(drawingContext, $"{IDCardData.IDCardNo.Substring(6, 4)}", heiti_font, 65, new Point(650, 968), 65);
            Drawing(drawingContext, $"{IDCardData.IDCardNo.Substring(10, 2)}", heiti_font, 65, new Point(945, 968), 65);
            Drawing(drawingContext, $"{IDCardData.IDCardNo.Substring(12, 2)}", heiti_font, 65, new Point(1140, 968), 65);

            if (IDCardData.Address.Length < 12)
            {
                Drawing(drawingContext, $"{IDCardData.Address.Substring(0, IDCardData.Address.Length)}", heiti_font, 65, new Point(650, 1120), 72);
            }
            else if (IDCardData.Address.Length < 22)
            {
                Drawing(drawingContext, $"{IDCardData.Address.Substring(0, 11)}", heiti_font, 65, new Point(650, 1120), 72);
                Drawing(drawingContext, $"{IDCardData.Address.Substring(11, IDCardData.Address.Length)}", heiti_font, 65, new Point(650, 1220), 70);
            }
            else
            {
                Drawing(drawingContext, $"{IDCardData.Address.Substring(0, 11)}", heiti_font, 65, new Point(650, 1120), 72);
                Drawing(drawingContext, $"{IDCardData.Address.Substring(11, 11)}", heiti_font, 65, new Point(650, 1220), 70);
                Drawing(drawingContext, $"{IDCardData.Address.Substring(22)}", heiti_font, 65, new Point(650, 1320), 70);
            }

            Drawing(drawingContext, $"{IDCardData.IDCardNo}", ocrb10bt_font, 72, new Point(920, 1470), 110);

            Drawing(drawingContext, $"{IDCardData.GrantDept}", ocrb10bt_font, 65, new Point(1050, 2750), 70);
            Drawing(drawingContext, $"{IDCardData.UserLifeBegin.Substring(0, 4)}.{IDCardData.UserLifeBegin.Substring(5, 2)}.{IDCardData.UserLifeBegin.Substring(8, 2)}-{IDCardData.UserLifeEnd.Substring(0, 4)}.{IDCardData.UserLifeEnd.Substring(5, 2)}.{IDCardData.UserLifeEnd.Substring(8, 2)}", ocrb10bt_font, 65, new Point(1050, 2895), 75);

            drawingContext.Close();
            composeImage.Render(drawingVisual);

            //定义一个JPG编码器
            JpegBitmapEncoder bitmapEncoder = new JpegBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(composeImage));

            return JpegBitmapEncoderToByte(bitmapEncoder);
        }

        private static byte[] JpegBitmapEncoderToByte(JpegBitmapEncoder encoder)
        {
            byte[] data;

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }



        private static DrawingContext Drawing(DrawingContext DrawingContext, string str, Typeface Typeface, double size, Point Point, double Weight)
        {
            double Fontscing = 0;
            Regex regChinese = new Regex("[\u4e00-\u9fa5]");
            Regex regNum = new Regex("^[0-9]");

            for (int i = 0; i < str.Length; i++)
            {
                FormattedText Name =  new FormattedText(str[i].ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface, size, Brushes.Black);
                DrawingContext.DrawText(Name, new Point(Point.X + Fontscing, Point.Y));
                Fontscing += regNum.Match(str[i].ToString()).Success || str[i].ToString() == "-" ? Weight / 2 : str[i].ToString() == "." ? Weight / 4 : Weight;
            }
            return DrawingContext;
        }


    }
}
