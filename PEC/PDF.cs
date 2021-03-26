
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
            BaseFont bfChinwse = BaseFont.CreateFont(@"JXBS.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bfChinwse2 = BaseFont.CreateFont(@"C:\Windows\Fonts\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font firstTitleFont = new Font(bfChinwse, 40, Font.NORMAL);
            Font thirdyTitleFont = new Font(bfChinwse, 16, Font.BOLD);
            Font tableTitleFont = new Font(bfChinwse2, 14, Font.NORMAL);
            Font thirdyWordsFont = new Font(bfChinwse, 16, Font.NORMAL);

            Font thirdyWordsLineFont = new Font(bfChinwse, 16, Font.UNDERLINE);
            Font tableConentFont = new Font(bfChinwse2, 14, Font.NORMAL);
            Font sixteenFont = new Font(bfChinwse2, 16, Font.BOLD);
            Font sixteenFontNormal = new Font(bfChinwse2, 16, Font.NORMAL);
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
                // 设置文字显示位置；例如剧中
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
                for (int i = 0; i < 13; i++)
                {
                    document.Add(new Paragraph("\n"));
                }
                Paragraph pdfLegalInfo = new Paragraph();
                pdfLegalInfo = new Paragraph("一、个人基本信息", thirdyTitleFont);
                
                pdfLegalInfo.IndentationLeft = 12;
                document.Add(pdfLegalInfo);
                pdfLegalInfo = new Paragraph("\n");
                document.Add(pdfLegalInfo);
                // 个人基本信息
                float[] baseinfoWidths = { 0.4f, 0.6f };
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
                baseinfoTable.TotalWidth = 300;
                document.Add(baseinfoTable);
                Paragraph p1 = new Paragraph();
                p1 = new Paragraph( "未查询到其他相关信息。", tableTitleFont);
                p1. Alignment=Element.ALIGN_CENTER;
                document.Add(p1);

                for (int i = 0; i <  3; i++)
                {
                    document.Add(new Paragraph("\n"));
                }

                Paragraph p = new Paragraph();
                Paragraph p2 = new Paragraph();
                Paragraph p3 = new Paragraph();
                p = new Paragraph( "        备注：本报告信用信息来源于国家、省或市有关政府单位，内容仅供", tableTitleFont);
                p2 = new Paragraph("        参考。如有疑问，请与我们联系：电话/传真：83300163，邮箱：", tableTitleFont);
                p3 = new Paragraph("        jiangsu_xyfw@126.com。", tableTitleFont);
                
                p.IndentationLeft = 12;
                document.Add(p);
                document.Add(p2);
                document.Add(p3);
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
