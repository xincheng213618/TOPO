using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECSuzhou
{
    public static class PDF
    {
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

        public static void PDFMark(string FilePath, string text, string text2 = null, int angle = 60)
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
                        content.ShowTextAligned(Element.ALIGN_MIDDLE, text, 120, 150, angle);
                        content.ShowTextAligned(Element.ALIGN_MIDDLE, text2, 180, 150, angle);
                    }
                    else
                    {
                        content.ShowTextAligned(Element.ALIGN_MIDDLE, text, 100, 100, angle);
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
