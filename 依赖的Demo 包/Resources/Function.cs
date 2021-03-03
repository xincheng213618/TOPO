using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EXCResources
{
    //其他的额外的功能
    public class Function
    {
        public static void IDCardDataToImage(IDCardData IDCardData)
        {
            System.Drawing.Bitmap BmpBack = XCovert.BitmapMakeTransparent(new System.Drawing.Bitmap(@"陈信成_320323199712213618.bmp"));

            //BmpBack.Save("1.png");
            BitmapSource image = XCovert.CreateBitmapSourceFromBitmap(BmpBack);

            //获取背景图
            BitmapSource bgImage = new BitmapImage(new Uri(@"Bin\Images\empty.png", UriKind.Relative));

            //BitmapSource image = new BitmapImage(new Uri(@"1.png", UriKind.Relative));

            //创建一个RenderTargetBitmap 对象，将WPF中的Visual对象输出
            RenderTargetBitmap composeImage = new RenderTargetBitmap(bgImage.PixelWidth, bgImage.PixelHeight, bgImage.DpiX, bgImage.DpiY, PixelFormats.Default);

            //定义成新的字体对象
            FontFamily myFontFamily = new FontFamily(Environment.CurrentDirectory + @"Bin\Fonts\hei.ttf");
            FontFamily myFontFamily1 = new FontFamily(Environment.CurrentDirectory + @"Bin\Fonts\fzhei.ttf");

            Typeface heiti_font = new Typeface(myFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            Typeface fzhei_font = new Typeface(myFontFamily1, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            Typeface ocrb10bt_font = new Typeface(new FontFamily(Environment.CurrentDirectory + @"Bin\Fonts\ocrb10bt.ttf"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawImage(bgImage, new Rect(0, 0, bgImage.Width, bgImage.Height));

            drawingContext.DrawImage(image, new Rect(1500, 720, image.Width * 5.1, image.Height * 5.1));
            Drawing(drawingContext, $"{IDCardData.Name}", heiti_font, 75, new Point(650, 685), 85);
            Drawing(drawingContext, $"{IDCardData.Sex}", heiti_font, 65, new Point(650, 835), 65);
            Drawing(drawingContext, $"{IDCardData.Nation}", heiti_font, 65, new Point(1030, 835), 65);  

            Drawing(drawingContext, $"{IDCardData.Born.Substring(0, 4)}", heiti_font, 65, new Point(650, 968), 65);
            Drawing(drawingContext, $"{IDCardData.Born.Substring(5, 2)}", heiti_font, 65, new Point(945, 968), 65);
            Drawing(drawingContext, $"{IDCardData.Born.Substring(8, 2)}", heiti_font, 65, new Point(1140, 968), 65);

            Drawing(drawingContext, $"{IDCardData.Address.Substring(0,11)}", heiti_font, 65, new Point(650, 1120), 72);
            Drawing(drawingContext, $"{IDCardData.Address.Substring(11,11)}", heiti_font, 65, new Point(650, 1220), 70);
            Drawing(drawingContext, $"{IDCardData.Address.Substring(22)}", heiti_font, 65, new Point(650, 1320), 70);
            Drawing(drawingContext, $"{IDCardData.IDCardNo}", ocrb10bt_font, 72, new Point(920, 1470), 110);


            Drawing(drawingContext, $"{IDCardData.GrantDept}", ocrb10bt_font, 65, new Point(1050, 2750), 70);
            Drawing(drawingContext, $"{IDCardData.UserLifeBegin.Substring(0, 4)}.{IDCardData.UserLifeBegin.Substring(5, 2)}.{IDCardData.UserLifeBegin.Substring(8, 2)}-{IDCardData.UserLifeEnd.Substring(0, 4)}.{IDCardData.UserLifeEnd.Substring(5, 2)}.{IDCardData.UserLifeEnd.Substring(8, 2)}", ocrb10bt_font, 65, new Point(1050, 2895), 75);

            drawingContext.Close();
            composeImage.Render(drawingVisual);

            //定义一个JPG编码器
            JpegBitmapEncoder bitmapEncoder = new JpegBitmapEncoder();
            //加入第一帧
            bitmapEncoder.Frames.Add(BitmapFrame.Create(composeImage));

            //保存至文件（不会修改源文件，将修改后的图片保存至程序目录下）
            string savePath = Directory.GetCurrentDirectory() + @"\陈信成.jpg";
            FileStream stream = new FileStream(savePath, FileMode.Create);
            bitmapEncoder.Save(stream);
            stream.Close();
            List<string> fileNameList = new List<string>();
            fileNameList.Add("陈信成.jpg");
            Function.JPGToPDF(fileNameList.ToArray(), "IDcard.PDF", "ID");
        }
        private static DrawingContext Drawing(DrawingContext DrawingContext, string str, Typeface Typeface, double size, Point Point, double Weight)
        {
            double Fontscing = 0;
            Regex regChinese = new Regex("[\u4e00-\u9fa5]");
            Regex regNum = new Regex("^[0-9]");

            for (int i = 0; i < str.Length; i++)
            {
                FormattedText Name = new FormattedText(str[i].ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface, size, Brushes.Black);
                DrawingContext.DrawText(Name, new Point(Point.X + Fontscing, Point.Y));
                Fontscing += regNum.Match(str[i].ToString()).Success || str[i].ToString() == "-" ? Weight / 2 : str[i].ToString() == "." ? Weight / 4 : Weight;
            }
            return DrawingContext;
        }


        public static void JPGToPDF(string[] imgs, string FilePath, string mode = "fit")
        {
            var document = new Document(PageSize.A4, 25, 25, 25, 25);
            using (var stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();
                for (int i = 0; i < imgs.Length; i++)
                {
                    try
                    {
                        using (var imageStream = new FileStream(imgs[i], FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            var image = Image.GetInstance(imageStream);
                            if (mode == "fit")
                            {
                                if (image.Height > PageSize.A4.Height - 25)
                                {
                                    image.ScaleToFit(PageSize.A4.Width - 25, PageSize.A4.Height - 25);
                                }
                                else if (image.Width > PageSize.A4.Width - 25)
                                {
                                    image.ScaleToFit(PageSize.A4.Width - 25, PageSize.A4.Height - 25);
                                }
                                image.Alignment = Element.ALIGN_MIDDLE;

                            }
                            else if (mode == "ID")
                            {
                                image.ScaleToFit(336, 475);
                                image.Alignment = Element.ALIGN_CENTER;
                            }
                            document.NewPage();
                            document.Add(image);
                        }
                    }
                    catch
                    {

                    }

                }

                document.Close();
            }

        }

        public static void PDFMark(string FilePath, string text, string text2 = null)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            string tempPath = Path.GetDirectoryName(FilePath) + Path.GetFileNameWithoutExtension(FilePath) + "_temp.pdf";

            try
            {
                pdfReader = new PdfReader(FilePath);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(tempPath, FileMode.Create));
                int total = pdfReader.NumberOfPages + 1;
                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                PdfContentByte content;
                BaseFont font = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\SIMFANG.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                PdfGState gs = new PdfGState();
                for (int i = 1; i < total; i++)
                {
                    content = pdfStamper.GetOverContent(i);//在内容上方加水印
                                                           //content = pdfStamper.GetUnderContent(i);//在内容下方加水印

                    gs.FillOpacity = 0.3f;                                              //透明度
                    content.SetGState(gs);
                    //content.SetGrayFill(0.3f);
                    //开始写入文本
                    content.BeginText();
                    content.SetColorFill(BaseColor.GRAY);
                    content.SetFontAndSize(font, 45);
                    content.SetTextMatrix(0, 0);
                    if (text2 != null)
                    {
                        content.ShowTextAligned(Element.ALIGN_MIDDLE, text, 120, 150, 60);
                        content.ShowTextAligned(Element.ALIGN_MIDDLE, text2, 180, 150, 60);
                    }
                    else
                    {
                        content.ShowTextAligned(Element.ALIGN_MIDDLE, text, 150, 150, 60);
                    }

                    //content.SetColorFill(BaseColor.BLACK);
                    //content.SetFontAndSize(font, 8);
                    //content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, 0, 0, 0);
                    content.EndText();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pdfStamper != null)
                    pdfStamper.Close();

                if (pdfReader != null)
                    pdfReader.Close();
                File.Copy(tempPath, FilePath, true);
                File.Delete(tempPath);
            }
        }

    }
}
