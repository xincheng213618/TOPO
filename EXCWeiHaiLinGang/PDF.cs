using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXC
{
    class PDF
    {
        public static void InsertBackGround(string FilePath)
        {
            //string FilePath = XFile.OpenFileDialog("所有PDF文件(*.pdf)|*.pdf");

            PdfReader pdfReader = new PdfReader(FilePath);

            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream("1.pdf", FileMode.Create));

            // 添加内容

            PdfContentByte content = pdfStamper.GetUnderContent(1);
            Image image = Image.GetInstance("1.jpg");

            image.ScaleAbsolute(595, 275);
            image.SetAbsolutePosition(0, 568);

            content.AddImage(image);
            pdfStamper.Close();
            pdfReader.Close();
        }


        public static void PDFWeiHaiMark(string FilePath, string Text)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;//方便异常抛出
            string tempPath = Path.GetDirectoryName(FilePath) + Path.GetFileNameWithoutExtension(FilePath) + "_temp.pdf"; //缓存文件

            try
            {
                pdfReader = new PdfReader(FilePath);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(tempPath, FileMode.Create));
                int num = pdfReader.NumberOfPages + 1;

                //插入背景图片
                PdfContentByte contentByte;

                BaseFont Font = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\SIMFANG.TTF", "Identity-H", embedded: true);
                PdfGState pdfGState = new PdfGState();
                pdfGState.FillOpacity = 0.3f;

                for (int i = 1; i < num; i++)
                {
                    contentByte = pdfStamper.GetUnderContent(i);
                    Image image = Image.GetInstance("PDF//WenDeng");

                    image.ScaleAbsolute(595f, 842f);
                    image.SetAbsolutePosition(0, 0);
                    contentByte.AddImage(image);


                    contentByte.SetGState(pdfGState);
                    contentByte.BeginText();
                    contentByte.SetColorFill(BaseColor.GRAY);
                    contentByte.SetFontAndSize(Font, 20f);
                    contentByte.SetTextMatrix(0f, 0f);
                    contentByte.ShowTextAligned(5, Text, 350, 20, 0f);
                    contentByte.EndText();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pdfStamper?.Close();
                pdfReader?.Close();
                File.Copy(tempPath, FilePath, overwrite: true);
                File.Delete(tempPath);
            }
        }


        public static int PDFNum(string FileName)
        {
            int Num = 0;
            try
            {
                PdfReader reader = new PdfReader(FileName);
                Num = reader.NumberOfPages;
                reader.Close();
                reader.Dispose();
            }
            catch
            {
                Num = 0;
            }
            return Num;
        }
    } 
}
