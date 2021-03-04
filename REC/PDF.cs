using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace REC
{
    public static class PDF
    {
        //页面偏移函数
        public static int PageChaneX = 0;
        public static int PageChaneY = 0;


        //参考https://news.lianjia.com/nj/baike/0403001.html

        /// <summary>
        /// 不动产证书报告
        /// </summary>
        /// <param name="FilePath">文件位置</param>
        /// <param name="Item">房屋信息</param>
        public static void DrawPDF(string FilePath, RECData Item)
        {

            string Tab0,Tab1,Tab2,Tab3;

            Match math = Regex.Match(Item.BDCQZH, @"(?<=^).*(?=\()");
            Tab0 = math.Success ? math.Value : "";

            math = Regex.Match(Item.BDCQZH, @"(?<=\().*?(?=\))");
            Tab1 = math.Success ? math.Value : "";

            math = Regex.Match(Item.BDCQZH, @"(?<=\)).*(?=不动产权)");
            Tab2 = math.Success ? math.Value : "";

            math = Regex.Match(Item.BDCQZH, @"(?<=不动产权第).*(?=号)");
            Tab3 = math.Success ? math.Value : "";

            if (Item.BDCDYH.Length == 28)
                Item.BDCDYH = Item.BDCDYH.Substring(0, 6) + " " + Item.BDCDYH.Substring(6, 3) + Item.BDCDYH.Substring(9, 3) + " " + Item.BDCDYH.Substring(12, 2) + Item.BDCDYH.Substring(14, 5) + " " + Item.BDCDYH.Substring(19, 1) + Item.BDCDYH.Substring(20, 8);



            PdfReader pdfReader = new PdfReader("PDF\\模板.pdf");
            Rectangle size = pdfReader.GetPageSizeWithRotation(1);
            Document document = new Document(size);
            Stream stream = new FileStream(FilePath, FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
            BaseFont HeiTi = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            //BaseFont font = BaseFont.CreateFont(@"PDF\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            //BaseFont STSONG = BaseFont.CreateFont(@"PDF\Fonts\STSONG.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);//华文宋体


            PdfImportedPage pdfImportedPage;

            document.Open();
            PdfContentByte pdfContentByte = pdfWriter.DirectContent;

            #region 第一页
            document.NewPage();  
            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 1);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);


            pdfContentByte.BeginText();
            pdfContentByte.SetColorFill(BaseColor.BLACK);
            pdfContentByte.SetFontAndSize(HeiTi, 9);
            pdfContentByte.SetTextMatrix(0, 0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("yyyy"), 660, 214, 0); //第一页的时间
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("MM"), 695, 214,0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("dd"), 725, 214, 0);

            pdfContentByte.EndText();
            #endregion

            #region 第二页
            document.NewPage();



            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 2);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);

            pdfContentByte.BeginText();
            pdfContentByte.SetColorFill(BaseColor.BLACK);
            pdfContentByte.SetFontAndSize(HeiTi, 10);
            pdfContentByte.SetTextMatrix(0, 0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Tab0, 65, 530, 0); //苏
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Tab1, 130, 530, 0); //时间
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Tab2, 184, 530, 0); //时间
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Tab3, 310, 530, 0); //编号
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QLR, 125, 492, 0); //权利人
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.GYQK, 125, 462, 0);//共有情况
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.ZL, 125, 432, 0);//坐落
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.BDCDYH, 125, 402, 0);//不动产单元号
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QLLX, 125, 372, 0);//权力类型
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QLXZ, 125, 342, 0);//权利性质
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.YT, 125, 312, 0);//用途
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.MJ, 125, 282, 0);//面积
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.SYQX, 125, 252, 0);//使用期限
            //pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QT, 125, 222, 0);//其他
            pdfContentByte = DrawMul(pdfContentByte, Item.QT, 125, 222, 0, 16);

            //pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.FJ, 600, 500, 0);//附记


            pdfContentByte.SetFontAndSize(HeiTi, 8);

            pdfContentByte = DrawMul(pdfContentByte, Item.FJ, 490, 490, 0,13);
            pdfContentByte.SetFontAndSize(HeiTi, 10);

            pdfContentByte.EndText();
            #endregion

            #region 第三页
            document.NewPage();
            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 3);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);
            if (Item.HSTSucess)
            {
                Image image = Image.GetInstance("Temp\\HST.jpg");
                image.ScaleAbsolute(300, 300 * image.Height / image.Width);
                image.SetAbsolutePosition(60, 300);
                pdfContentByte.AddImage(image);
            }
            #endregion

            document.Close();

            pdfWriter.Close();
            pdfReader.Close();
        }
        public static PdfContentByte DrawMul(PdfContentByte pdfContentByte ,string Content,int x,int y,int roxy,int length)
        {
            //2021.2.25 更新 空格也有可能出现
            string[] stringSeparators = new string[] { "\r\n" ," "};

            foreach (var item in Content.Split(stringSeparators, StringSplitOptions.None))
            {
                pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, item, x, y, roxy);//附记
                y = y - length;
            }

            return pdfContentByte;
        }




        /// <summary>
        /// 定位点  顺序
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Item"></param>
        public static void DrawPrintPDF(string FilePath, RECData Item)
        {
            CodeHelper.QRcode(Item.BDCQZH, "Temp\\BDCQZH.png");

            string Tab0, Tab1, Tab2, Tab3;

            Match math = Regex.Match(Item.BDCQZH, @"(?<=^).*(?=\()");
            Tab0 = math.Success ? math.Value : "";

            math = Regex.Match(Item.BDCQZH, @"(?<=\().*?(?=\))");
            Tab1 = math.Success ? math.Value : "";

            math = Regex.Match(Item.BDCQZH, @"(?<=\)).*(?=不动产权)");
            Tab2 = math.Success ? math.Value : "";

            math = Regex.Match(Item.BDCQZH, @"(?<=不动产权第).*(?=号)");
            Tab3 = math.Success ? math.Value : "";
            if (Item.BDCDYH.Length == 28)
                Item.BDCDYH = Item.BDCDYH.Substring(0, 6) + " " + Item.BDCDYH.Substring(6, 3) + Item.BDCDYH.Substring(9, 3) + " " + Item.BDCDYH.Substring(12, 2) + Item.BDCDYH.Substring(14, 5) + " " + Item.BDCDYH.Substring(19, 1) + Item.BDCDYH.Substring(20, 8);

            BaseFont HeiTi = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);   

            PdfReader pdfReader = new PdfReader("PDF\\Print.pdf");
            Rectangle size = pdfReader.GetPageSizeWithRotation(1);

            Document document = new Document(size);
            Stream stream = new FileStream(FilePath, FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
            PdfImportedPage pdfImportedPage;

            document.Open();
            PdfContentByte pdfContentByte = pdfWriter.DirectContent;
            #region 第一页
            document.NewPage();

            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 1);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);

            //Image image = Image.GetInstance("1.jpg");
            //image.ScaleAbsolute(300, 300 * image.Height / image.Width);
            //image.SetAbsolutePosition(60, 300);
            //pdfContentByte.AddImage(image);

            pdfContentByte.BeginText();
            pdfContentByte.SetColorFill(BaseColor.BLACK);
            pdfContentByte.SetFontAndSize(HeiTi, 11);
            pdfContentByte.SetTextMatrix(0, 0);
            //pdfContentByte.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA_BOLD).BaseFont, 20);

            //pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, "zuobiaodian", 0, 0, 0);//其他
            pdfContentByte.EndText();

            #endregion



            #region 第二页
            document.NewPage();
            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 2);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);

            pdfContentByte.BeginText();
            pdfContentByte.SetColorFill(BaseColor.BLACK);
            pdfContentByte.SetFontAndSize(HeiTi, 12);
            pdfContentByte.SetTextMatrix(0, 0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Tab0, 0+PageChaneX, 550 + PageChaneY, 0); //苏
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Tab1, 65+ PageChaneX, 550 + PageChaneY, 0); //时间
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, Tab2, 140+ PageChaneX, 550 + PageChaneY, 0); //时间
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Tab3, 290+ PageChaneX, 550 + PageChaneY, 0); //编号
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QLR, 63+ PageChaneX, 513 + PageChaneY, 0); //权利人
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.GYQK, 63+ PageChaneX, 475 + PageChaneY, 0);//共有情况
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.ZL, 63+ PageChaneX, 436 +PageChaneY, 0);//坐落
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.BDCDYH, 63 + PageChaneX, 398 + PageChaneY, 0);//不动产单元号
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QLLX, 63 + PageChaneX, 359 + PageChaneY, 0);//权力类型
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QLXZ, 63+ PageChaneX, 321+ PageChaneY, 0);//权利性质
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.YT, 63 + PageChaneX, 282 + PageChaneY, 0);//用途
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.MJ, 63 + PageChaneX, 244 + PageChaneY, 0);//面积
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.SYQX, 63+ PageChaneX, 205 + PageChaneY, 0);//使用期限
            //pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.QT, 63, 166, 0);//其他
            pdfContentByte = DrawMul(pdfContentByte, Item.QT, 63+ PageChaneX, 166 + PageChaneY , 0, 17);

            //pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, Item.FJ, 500, 500, 0);//附记
            pdfContentByte.SetFontAndSize(HeiTi, 10);
            pdfContentByte = DrawMul(pdfContentByte, Item.FJ, 500 + PageChaneX, 505 + PageChaneY, 0, 14);
            pdfContentByte.SetFontAndSize(HeiTi, 12);


            pdfContentByte.EndText();
            #endregion

            #region 第三页
            document.NewPage();
            pdfImportedPage = pdfWriter.GetImportedPage(pdfReader, 3);
            pdfContentByte.AddTemplate(pdfImportedPage, 0, 0);

            pdfContentByte.BeginText();
            pdfContentByte.SetColorFill(BaseColor.BLACK);
            pdfContentByte.SetFontAndSize(HeiTi, 12);
            pdfContentByte.SetTextMatrix(0, 0);

            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("yyyy"), 675 + PageChaneX, 141 + PageChaneY, 0); //第一页的时间
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("MM"), 720 + PageChaneX, 141 + PageChaneY, 0);
            pdfContentByte.ShowTextAligned(Element.ALIGN_CENTER, DateTime.Now.ToString("dd"), 760 + PageChaneX, 141 + PageChaneY, 0);
            pdfContentByte.EndText();

            Image image = Image.GetInstance("Temp\\BDCQZH.png");
            image.ScaleAbsolute(100, 100);
            image.SetAbsolutePosition(697, 433);
            pdfContentByte.AddImage(image);
            #endregion

            document.Close();


            pdfWriter.Close();
            pdfReader.Close();
        }






    }
}
