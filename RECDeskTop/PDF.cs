using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace REC
{
    public class PDF
    {

        public static void DrawPDF(string FilePath, RECListView Item)
        {
            PdfReader pdfReader = new PdfReader("PDF\\模板.pdf");
            Rectangle size = pdfReader.GetPageSizeWithRotation(1);
            Document document = new Document(size);
            Stream stream = new FileStream(FilePath, FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);

            BaseFont font = BaseFont.CreateFont(@"PDF\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            BaseFont STSONG = BaseFont.CreateFont(@"PDF\Fonts\STSONG.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//华文宋体

            PdfImportedPage pdfImportedPage;

            document.Open();
            PdfContentByte pdfContentByte = pdfWriter.DirectContent;

            #region 第一页
            document.NewPage();  
            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 1);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);


            pdfContentByte.BeginText();
            pdfContentByte.SetColorFill(BaseColor.BLACK);
            pdfContentByte.SetFontAndSize(font, 12);
            pdfContentByte.SetTextMatrix(0, 0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("yyyy"), 660, 213, 0); //第一页的时间
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("MM"), 695, 213, 0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("dd"), 725, 213, 0);

            pdfContentByte.EndText();
            #endregion

            #region 第二页
            document.NewPage();
            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 2);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);


            pdfContentByte.BeginText();
            pdfContentByte.SetColorFill(BaseColor.BLACK);
            pdfContentByte.SetFontAndSize(font, 9);
            pdfContentByte.SetTextMatrix(0, 0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.QLR, 220, 492, 0); //权利人
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.GYQK, 220, 462, 0);//共有情况
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.ZL, 220, 432, 0);//坐落
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.BDCDYH, 220, 402, 0);//不动产单元号
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.QLLX, 220, 372, 0);//权力类型
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.QLXZ, 220, 342, 0);//权利性质
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.YT, 220, 312, 0);//用途
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.MJ, 220, 282, 0);//面积
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.SYQX, 220, 252, 0);//使用期限
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Item.QT, 220, 222, 0);//其他
            pdfContentByte.EndText();
            #endregion

            #region 第三页
            document.NewPage();
            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 3);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);

            Image image = Image.GetInstance("1.jpg");
            image.ScaleAbsolute(300, 300*image.Height/ image.Width);
            image.SetAbsolutePosition(60, 300);
            pdfContentByte.AddImage(image);
            #endregion

            document.Close();

            pdfWriter.Close();
            pdfReader.Close();

            //PdfStamper pdfStamper  = new PdfStamper(pdfReader, stream);

        }

    }
}
