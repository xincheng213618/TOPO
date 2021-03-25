
using BaseDLL;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PEC
{
    class PDF
    {
        public static bool DrawYiXing1(string FilePath, IDCardData IDCardData)
        {
            //打印时间	   
            BaseFont bfChinwse = BaseFont.CreateFont(@"C:\Windows\Fonts\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fourteenFontRule = new Font(bfChinwse, 14, Font.BOLD);
            Font firstTitleFont = new Font(bfChinwse, 38, Font.BOLD);
            Font thirdyWordsFont = new Font(bfChinwse, 16, Font.NORMAL);
            Font thirdyWordsLineFont = new Font(bfChinwse, 16, Font.UNDERLINE);
            Font thirdyTitleFont = new Font(bfChinwse, 16, Font.NORMAL);
            Font tableTitleFont = new Font(bfChinwse, 14, Font.NORMAL);
            Font tableConentFont = new Font(bfChinwse, 14, Font.NORMAL);
            Font sixteenFont = new Font(bfChinwse, 16, Font.BOLD);
            Font sixteenFontNormal = new Font(bfChinwse, 16, Font.NORMAL);
            try
            {
                Stream stream = new FileStream(FilePath, FileMode.Create);
                Document document = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter.GetInstance(document, stream);
                document.Open();
                Paragraph report1 = new Paragraph();

                for (int i = 0; i < 10; i++)
                {
                    document.Add(new Paragraph("\n"));
                }
                // 封面开头
                report1 = new Paragraph("江苏省自然人公共信用信息", firstTitleFont);
                // 设置文字显示位置，；例如剧中
                report1.Alignment = Element.ALIGN_CENTER;
                document.Add(report1);
                report1 = new Paragraph("一体化查询报告", firstTitleFont);
                report1.Alignment = Element.ALIGN_CENTER;
                report1.Leading = 60;
                document.Add(report1);
                for (int i = 0; i< 18; i++)
                {
                    document.Add(new Paragraph("\n"));
                }
                report1 = new Paragraph("江苏省公共信用信息中心", sixteenFont);
                report1.Alignment = Element.ALIGN_CENTER;
                document.Add(report1);
                report1 = new Paragraph(DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日", sixteenFontNormal);
                report1.Alignment = Element.ALIGN_CENTER;
                document.Add(report1);
                for (int i = 0; i < 10; i++)
                {
                    document.Add(new Paragraph("\n"));
                }
                Paragraph pdfLegalInfo = new Paragraph();
                pdfLegalInfo = new Paragraph(" 个人基本信息", thirdyTitleFont);
                pdfLegalInfo.Alignment = Element.ALIGN_CENTER;
                pdfLegalInfo.IndentationLeft = 12;
                document.Add(pdfLegalInfo);
                pdfLegalInfo = new Paragraph("\n");
                document.Add(pdfLegalInfo);
                // 个人基本信息
                float[] baseinfoWidths = { 0.2f, 0.4f };
                PdfPTable baseinfoTable = new PdfPTable(baseinfoWidths);
                baseinfoTable.AddCell(getPDFCell("姓名", tableTitleFont));
                baseinfoTable.AddCell(getPDFCell(IDCardData.Name, tableConentFont));
                baseinfoTable.AddCell(getPDFCell("性别", tableTitleFont));
                baseinfoTable.AddCell(getPDFCell(IDCardData.Sex == null ? "" : IDCardData.Sex,tableConentFont));
                baseinfoTable.AddCell(getPDFCell("证件类别", tableTitleFont));
                baseinfoTable.AddCell(getPDFCell(IDCardData.CardType == null ? "" : IDCardData.CardType, tableConentFont));
                baseinfoTable.AddCell(getPDFCell("身份证号", tableTitleFont));
                baseinfoTable.AddCell(getPDFCell(IDCardData.IDCardNo == null ? "" : IDCardData.IDCardNo, tableConentFont));
                baseinfoTable.AddCell(getPDFCell("住址", tableTitleFont));
                baseinfoTable.AddCell(getPDFCell(IDCardData.Address == null ? "" : IDCardData.Address,tableConentFont));
                baseinfoTable.WidthPercentage = 100;
                baseinfoTable.TotalWidth = 300;
                baseinfoTable.LockedWidth = true;
                document.Add(baseinfoTable);
                pdfLegalInfo = new Paragraph("\n");
                pdfLegalInfo = new Paragraph("\n");
                Paragraph c3 = new Paragraph();
                c3 = new Paragraph("无法查询详细信息，请至服务窗口咨询打印。", thirdyTitleFont);
                c3.Alignment = Element.ALIGN_CENTER;
                c3.IndentationLeft = 12;
                // 将块添加到短语中
                document.Add(c3);
                document.Close();
                SetPdfBackground(FilePath);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public static void SetPdfBackground(string pdfFilePath)
        {

            //重新生成的 PDF 的路径x
            string destFile = @"sample.pdf";
            //create new pdf document
            FileStream stream = new FileStream(destFile, FileMode.Create, FileAccess.ReadWrite);

            PdfReader reader = new PdfReader(pdfFilePath);
            //read pdf stream 
            PdfStamper stamper = new PdfStamper(reader, stream);

            string imagePage = @"images/1.png";
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagePage);
            var img = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
            img.ScaleToFit(PageSize.A4);

            img.SetAbsolutePosition(0, 0);

            int totalPage = reader.NumberOfPages;
            for (int current = 1; current <= totalPage; current++)
            {
                var canvas = stamper.GetUnderContent(current);
                var page = stamper.GetImportedPage(reader, current);
                canvas.AddImage(img);
            }
            stamper.Close();
            reader.Close();
        }
        private static PdfPCell getPDFCell(string str, Font font)
        {
            // 创建单元格对象，将内容与字体放入段落中作为单元格内容
            PdfPCell cell = new PdfPCell(new Paragraph(str, font));
            cell.UseAscender = true;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Left = 6;
            // 设置最小单元格高度
            cell.MinimumHeight = 20;
            return cell;
        }

    }
    public static class AxAcroPDFutil
    {
      public static AxAcroPDFLib.AxAcroPDF pdfControl = new AxAcroPDFLib.AxAcroPDF();
    }
}
