using BaseUtil;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace EXCXuanCheng
{
    public class PDF
    {
        public static bool DrawXuancheng(string FilePath, Basic basic, ObservableCollection<XKXX> XKXXItems, ObservableCollection<ZZXX> ZZXXItems, ObservableCollection<CBXX> CBXXItems, ObservableCollection<NsxyjbInfoItem> NsxyjbInfoItems, ObservableCollection<JlInfoItem> JlInfoItems, ObservableCollection<RyInfoItem> RyInfoItems, ObservableCollection<SxbzxrInfoItem> SxbzxrInfoItems, ObservableCollection<XzcfInfoItem> XzcfInfoItems, ObservableCollection<SxqyInfoItem> SxqyInfoItems)
        {



            string toLeft1 = "                         ";
            string toLeft2 = "                ";
            string bianhao = DateTime.Now.ToString("yyyyMMddHHmmss");
            CodeHelper.QRcode("报告编号：" + bianhao, "Temp\\QR.png");
            BaseFont Heiti = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont Kaiti = BaseFont.CreateFont(@"C:\Windows\Fonts\STKAITI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font headfont = new Font(Kaiti, 30, Font.BOLD);// 设置字体大小
            Font keyfont = new Font(Heiti, 14, Font.NORMAL);

            Document document = new Document(PageSize.A4, 25, 25, 25, 25);

            Stream stream = new FileStream(FilePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            PageEventHelper pageEventHelper = new PageEventHelper();
            writer.PageEvent = pageEventHelper;

            document.Open();

            Image image = Image.GetInstance("Temp\\QR.png");
            image.ScaleAbsolute(100, 100);
            image.SetAbsolutePosition(450, 650);
            document.Add(image);
            document.Add(new Paragraph("\n\n\n\n\n\n\n\n\n\n\n\n"));
            Paragraph pt = new Paragraph("法人和非法人组织/个人" + "\r\n" + "公共信用信息报告", headfont)
            {
                Alignment = 1,
            };
            document.Add(pt);
            document.Add(new Paragraph("\n\n\n\n"));
            Paragraph pt1 = new Paragraph(toLeft1 + "主体名称：" + basic.baseDwmc, keyfont)
            {
                Alignment = 0,
            };
            document.Add(pt1);
            document.Add(new Paragraph("\n"));
            Paragraph pt2 = new Paragraph(toLeft1 + "统一社会信用代码：" + basic.uniscid, keyfont)
            {
                Alignment = 0,
            };
            document.Add(pt2);
            document.Add(new Paragraph("\n"));
            Paragraph pt3 = new Paragraph(toLeft1 + "报告编号：" + bianhao, keyfont)
            {
                Alignment = 0,
            };
            document.Add(pt3);
            document.Add(new Paragraph("\n\n\n\n\n\n\n\n\n"));
            Paragraph pt4 = new Paragraph(toLeft1 + "生成日期：" + DateTime.Now.ToString("yyyy年MM月dd日"), keyfont)
            {
                Alignment = 0,
            };
            document.Add(pt4);
            document.Add(new Paragraph("\n"));
            Paragraph pt5 = new Paragraph(toLeft1 + "出具单位：宣城市发展和改革委员会", keyfont)
            {
                Alignment = 0,
            };
            document.Add(pt5);

            document.NewPage();
            document.Add(new Paragraph("\n\n\n"));
            Paragraph pt6 = new Paragraph("概要", new Font(Kaiti, 20, Font.BOLD))
            {
                Alignment = 1,
            };

            document.Add(pt6);
            document.Add(new Paragraph("\n\n"));
            Paragraph pt7 = new Paragraph(toLeft2 + "一、基础信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 0,
            };
            document.Add(pt7);
            //float[] widhts = { 1f };
            //PdfPTable table = new PdfPTable(widhts);
            //table.WidthPercentage = 50; // 宽度100%填充
            //PdfPCell basecell1 = new PdfPCell(new PdfPCell(new Paragraph(toLeft2 + "1.信用主体名称：" + "科大讯飞"+"\r"+ "2.统一社会信用代码：" + "2817318419" + "\r" + "3.法定代表人：" + "陈信成", new Font(Heiti, 15, Font.NORMAL))))
            //{
            //    HorizontalAlignment = Element.ALIGN_LEFT

            //};
            //table.AddCell(basecell1);
            //document.Add(table);
            Paragraph pt7_1 = new Paragraph(toLeft2 + "1.信用主体名称：" + basic.baseDwmc, new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt7_1);
            Paragraph pt7_2 = new Paragraph(toLeft2 + "2.统一社会信用代码：" + basic.uniscid, new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt7_2);
            Paragraph pt7_3 = new Paragraph(toLeft2 + "3.法定代表人：" + basic.baseFddbr, new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt7_3);
            Paragraph pt7_4 = new Paragraph(toLeft2 + "4.企业类型：" + basic.baseQyzl, new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt7_4);
            Paragraph pt7_5 = new Paragraph(toLeft2 + "5.成立日期：" + basic.baseClrq, new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt7_5);
            Paragraph pt7_6 = new Paragraph(toLeft2 + "6.地址：" + basic.baseSzqh, new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt7_6);
            Paragraph pt7_7 = new Paragraph(toLeft2 + "7.存续状态：" + basic.baseNsrzt, new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt7_7);

            document.Add(new Paragraph("\n"));
            Paragraph pt8 = new Paragraph(toLeft2 + "二、信用综述信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 0,
            };
            document.Add(pt8);
            Paragraph pt8_6 = new Paragraph(toLeft2 + "1.基本信息", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt8_6);
            Paragraph pt8_1 = new Paragraph(toLeft2 + "2.许可信息", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt8_1);
            Paragraph pt8_2 = new Paragraph(toLeft2 + "3.资质信息", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt8_2);
            Paragraph pt8_3 = new Paragraph(toLeft2 + "4.参保信息", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt8_3);
            Paragraph pt8_4 = new Paragraph(toLeft2 + "5.良好信息", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt8_4);
            Paragraph pt8_5 = new Paragraph(toLeft2 + "6.不良信息", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt8_5);
            document.Add(new Paragraph("\n"));
            Paragraph pt9 = new Paragraph(toLeft2 + "三、报告说明", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 0,
            };
            document.Add(pt9);
            Paragraph pt9_1 = new Paragraph(toLeft2 + "1.报告提供单位：宣城市发展和改革委员会", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt9_1);
            Paragraph pt9_2 = new Paragraph(toLeft2 + "2.本报告应用于合法途径，违法规定或约定使用本报告，责任由相关人承担。", new Font(Heiti, 15, Font.NORMAL))
            {
                Alignment = 0,
            };
            document.Add(pt9_2);
            document.NewPage();
            document.Add(new Paragraph("\n\n\n"));
            Paragraph pt10 = new Paragraph("正文", new Font(Kaiti, 20, Font.BOLD))
            {
                Alignment = 1,
            };

            document.Add(pt10);


            document.Add(new Paragraph("\n\n"));
            Paragraph pt11 = new Paragraph("一、基础信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt11);
            document.Add(new Paragraph("\n"));

            float[] basicwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            PdfPTable basictable = new PdfPTable(basicwidhts);
            basictable.WidthPercentage = 100; // 宽度100%填充

            //====第一行开始=================
            PdfPCell ycell = new PdfPCell(new Paragraph("法定代表人", new Font(Heiti, 15, Font.NORMAL)));
            ycell.MinimumHeight = 30f;
            ycell.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell.Colspan = 1;

            PdfPCell ycell2 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseFddbr, new Font(Kaiti, 15, Font.NORMAL))));
            ycell2.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell2.Colspan = 1;
            PdfPCell ycell3 = new PdfPCell(new Paragraph("注册号", new Font(Heiti, 15, Font.NORMAL)));
            ycell3.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell3.Colspan = 1;
            PdfPCell ycell4 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseZch, new Font(Kaiti, 15, Font.NORMAL))));
            ycell4.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell4.Colspan = 1;

            basictable.AddCell(ycell);
            basictable.AddCell(ycell2);
            basictable.AddCell(ycell3);
            basictable.AddCell(ycell4);
            //====第二行开始=================
            PdfPCell ycell5 = new PdfPCell(new PdfPCell(new Paragraph("单位名称", new Font(Heiti, 15, Font.NORMAL))));
            ycell5.MinimumHeight = 30f;
            ycell5.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell5.Colspan = 1;

            PdfPCell ycell6 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseDwmc, new Font(Kaiti, 15, Font.NORMAL))));
            ycell6.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell6.Colspan = 1;
            PdfPCell ycell7 = new PdfPCell(new PdfPCell(new Paragraph("社会统一信用代码", new Font(Heiti, 15, Font.NORMAL))));
            ycell7.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell7.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell7.Colspan = 1;
            PdfPCell ycell8 = new PdfPCell(new PdfPCell(new Paragraph(basic.uniscid, new Font(Kaiti, 15, Font.NORMAL))));
            ycell8.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell8.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell8.Colspan = 1;

            basictable.AddCell(ycell5);
            basictable.AddCell(ycell6);
            basictable.AddCell(ycell7);
            basictable.AddCell(ycell8);
            //====第三行开始=================
            PdfPCell ycell9 = new PdfPCell(new PdfPCell(new Paragraph("主营产品", new Font(Heiti, 15, Font.NORMAL))));
            ycell9.MinimumHeight = 30f;
            ycell9.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell9.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell9.Colspan = 1;

            PdfPCell ycell10 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseZycp, new Font(Kaiti, 15, Font.NORMAL))));
            ycell10.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell10.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell10.Colspan = 1;
            PdfPCell ycell11 = new PdfPCell(new PdfPCell(new Paragraph("行业", new Font(Heiti, 15, Font.NORMAL))));
            ycell11.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell11.Colspan = 1;
            PdfPCell ycell12 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseHy, new Font(Kaiti, 15, Font.NORMAL))));
            ycell12.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell12.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell12.Colspan = 1;

            basictable.AddCell(ycell9);
            basictable.AddCell(ycell10);
            basictable.AddCell(ycell11);
            basictable.AddCell(ycell12);
            //====第四行开始=================
            PdfPCell ycell13 = new PdfPCell(new PdfPCell(new Paragraph("成立日期", new Font(Heiti, 15, Font.NORMAL))));
            ycell13.MinimumHeight = 30f;
            ycell13.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell13.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell13.Colspan = 1;

            PdfPCell ycell14 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseClrq, new Font(Kaiti, 15, Font.NORMAL))));
            ycell14.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell14.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell14.Colspan = 1;
            PdfPCell ycell15 = new PdfPCell(new PdfPCell(new Paragraph("税务登记号", new Font(Heiti, 15, Font.NORMAL))));
            ycell15.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell15.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell15.Colspan = 1;
            PdfPCell ycell16 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseSwdjh, new Font(Kaiti, 15, Font.NORMAL))));
            ycell16.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell16.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell16.Colspan = 1;

            basictable.AddCell(ycell13);
            basictable.AddCell(ycell14);
            basictable.AddCell(ycell15);
            basictable.AddCell(ycell16);
            //====第五行开始=================
            PdfPCell ycell17 = new PdfPCell(new PdfPCell(new Paragraph("详细地址", new Font(Heiti, 15, Font.NORMAL))));
            ycell17.MinimumHeight = 30f;
            ycell17.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell17.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell17.Colspan = 1;
            PdfPCell ycell18 = new PdfPCell(new PdfPCell(new Paragraph(basic.baseXxdz, new Font(Kaiti, 15, Font.NORMAL))));
            ycell18.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell18.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell18.Colspan = 3;
            basictable.AddCell(ycell17);
            basictable.AddCell(ycell18);
            document.Add(basictable);

            document.Add(new Paragraph("\n"));
            Paragraph pt12 = new Paragraph("二、许可信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt12);
            document.Add(new Paragraph("\n"));
            float[] xkxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };

            if (XKXXItems.Count > 0)
            {
                foreach (XKXX xkItem in XKXXItems)
                {
                    PdfPTable xkxxtable = new PdfPTable(xkxxwidhts);
                    xkxxtable.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell19 = new PdfPCell(new PdfPCell(new Paragraph("决定书文号", new Font(Heiti, 15, Font.NORMAL))));
                    ycell19.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell19.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell19.Colspan = 1;
                    PdfPCell ycell20 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.jdswh, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell20.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell20.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell20.Colspan = 1;
                    PdfPCell ycell21 = new PdfPCell(new PdfPCell(new Paragraph("项目名称", new Font(Heiti, 15, Font.NORMAL))));
                    ycell21.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell21.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell21.Colspan = 1;
                    PdfPCell ycell22 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.xmmc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell22.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell22.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell22.Colspan = 1;
                    xkxxtable.AddCell(ycell19);
                    xkxxtable.AddCell(ycell20);
                    xkxxtable.AddCell(ycell21);
                    xkxxtable.AddCell(ycell22);

                    PdfPCell ycell23 = new PdfPCell(new PdfPCell(new Paragraph("审批类别", new Font(Heiti, 15, Font.NORMAL))));
                    ycell23.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell23.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell23.Colspan = 1;
                    PdfPCell ycell24 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.splb, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell24.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell24.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell24.Colspan = 1;
                    PdfPCell ycell25 = new PdfPCell(new PdfPCell(new Paragraph("许可内容", new Font(Heiti, 15, Font.NORMAL))));
                    ycell25.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell25.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell25.Colspan = 1;
                    PdfPCell ycell26 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.xknr, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell26.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell26.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell26.Colspan = 1;
                    xkxxtable.AddCell(ycell23);
                    xkxxtable.AddCell(ycell24);
                    xkxxtable.AddCell(ycell25);
                    xkxxtable.AddCell(ycell26);

                    PdfPCell ycell27 = new PdfPCell(new PdfPCell(new Paragraph("决定日期", new Font(Heiti, 15, Font.NORMAL))));
                    ycell27.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell27.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell27.Colspan = 1;
                    PdfPCell ycell28 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.jdrq, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell28.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell28.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell28.Colspan = 1;
                    PdfPCell ycell29 = new PdfPCell(new PdfPCell(new Paragraph("截至日期", new Font(Heiti, 15, Font.NORMAL))));
                    ycell29.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell29.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell29.Colspan = 1;
                    PdfPCell ycell30 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.jzrq, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell30.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell30.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell30.Colspan = 1;
                    xkxxtable.AddCell(ycell27);
                    xkxxtable.AddCell(ycell28);
                    xkxxtable.AddCell(ycell29);
                    xkxxtable.AddCell(ycell30);
                    PdfPCell ycell31 = new PdfPCell(new PdfPCell(new Paragraph("许可机关", new Font(Heiti, 15, Font.NORMAL))));
                    ycell31.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell31.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell31.Colspan = 1;
                    PdfPCell ycell32 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.xzjg, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell32.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell32.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell32.Colspan = 1;
                    PdfPCell ycell33 = new PdfPCell(new PdfPCell(new Paragraph("当前状态", new Font(Heiti, 15, Font.NORMAL))));
                    ycell33.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell33.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell33.Colspan = 1;
                    PdfPCell ycell34 = new PdfPCell(new PdfPCell(new Paragraph(xkItem.ztmc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell34.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell34.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell34.Colspan = 1;
                    xkxxtable.AddCell(ycell31);
                    xkxxtable.AddCell(ycell32);
                    xkxxtable.AddCell(ycell33);
                    xkxxtable.AddCell(ycell34);

                    xkxxtable.SpacingAfter = 8f;
                    document.Add(xkxxtable);
                }
            }
            else
            {
                PdfPTable xkxxtable = new PdfPTable(xkxxwidhts);
                xkxxtable.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell35 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell35.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell35.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell35.Colspan = 4;
                xkxxtable.AddCell(ycell35);
                document.Add(xkxxtable);

            }
            document.Add(new Paragraph("\n"));
            Paragraph pt13 = new Paragraph("三、资质信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt13);
            document.Add(new Paragraph("\n"));
            float[] zzxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            if (ZZXXItems.Count > 0)
            {
                foreach (ZZXX zzItem in ZZXXItems)
                {
                    PdfPTable zzxxtable = new PdfPTable(zzxxwidhts);
                    zzxxtable.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell36 = new PdfPCell(new PdfPCell(new Paragraph("证书名称", new Font(Heiti, 15, Font.NORMAL))));
                    ycell36.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell36.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell36.Colspan = 1;
                    PdfPCell ycell37 = new PdfPCell(new PdfPCell(new Paragraph(zzItem.zsmc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell37.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell37.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell37.Colspan = 1;
                    PdfPCell ycell38 = new PdfPCell(new PdfPCell(new Paragraph("证书编号", new Font(Heiti, 15, Font.NORMAL))));
                    ycell38.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell38.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell38.Colspan = 1;
                    PdfPCell ycell39 = new PdfPCell(new PdfPCell(new Paragraph(zzItem.zsbh, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell39.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell39.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell39.Colspan = 1;
                    zzxxtable.AddCell(ycell36);
                    zzxxtable.AddCell(ycell37);
                    zzxxtable.AddCell(ycell38);
                    zzxxtable.AddCell(ycell39);

                    PdfPCell ycell40 = new PdfPCell(new PdfPCell(new Paragraph("资质类别", new Font(Heiti, 15, Font.NORMAL))));
                    ycell40.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell40.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell40.Colspan = 1;
                    PdfPCell ycell41 = new PdfPCell(new PdfPCell(new Paragraph(zzItem.zzlb, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell41.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell41.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell41.Colspan = 1;
                    PdfPCell ycell42 = new PdfPCell(new PdfPCell(new Paragraph("资质等级", new Font(Heiti, 15, Font.NORMAL))));
                    ycell42.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell42.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell42.Colspan = 1;
                    PdfPCell ycell43 = new PdfPCell(new PdfPCell(new Paragraph(zzItem.zzdj, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell43.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell43.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell43.Colspan = 1;
                    zzxxtable.AddCell(ycell40);
                    zzxxtable.AddCell(ycell41);
                    zzxxtable.AddCell(ycell42);
                    zzxxtable.AddCell(ycell43);

                    PdfPCell ycell44 = new PdfPCell(new PdfPCell(new Paragraph("资质内容", new Font(Heiti, 15, Font.NORMAL))));
                    ycell44.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell44.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell44.Colspan = 1;
                    PdfPCell ycell45 = new PdfPCell(new PdfPCell(new Paragraph(zzItem.zznr, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell45.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell45.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell45.Colspan = 1;
                    PdfPCell ycell46 = new PdfPCell(new PdfPCell(new Paragraph("发布日期", new Font(Heiti, 15, Font.NORMAL))));
                    ycell46.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell46.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell46.Colspan = 1;
                    PdfPCell ycell47 = new PdfPCell(new PdfPCell(new Paragraph(zzItem.fbrq, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell47.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell47.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell47.Colspan = 1;
                    zzxxtable.AddCell(ycell44);
                    zzxxtable.AddCell(ycell45);
                    zzxxtable.AddCell(ycell46);
                    zzxxtable.AddCell(ycell47);

                    zzxxtable.SpacingAfter = 8f;
                    document.Add(zzxxtable);
                }
            }
            else
            {
                PdfPTable zzxxtable = new PdfPTable(zzxxwidhts);
                zzxxtable.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell48 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell48.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell48.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell48.Colspan = 4;
                zzxxtable.AddCell(ycell48);
                document.Add(zzxxtable);

            }
            document.Add(new Paragraph("\n"));
            Paragraph pt14 = new Paragraph("四、参保信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt14);
            document.Add(new Paragraph("\n"));
            float[] cbxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            if (CBXXItems.Count > 0)
            {
                foreach (CBXX cbItem in CBXXItems)
                {
                    PdfPTable cbxxtable = new PdfPTable(cbxxwidhts);
                    cbxxtable.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell49 = new PdfPCell(new PdfPCell(new Paragraph("参保日期", new Font(Heiti, 15, Font.NORMAL))));
                    ycell49.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell49.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell49.Colspan = 1;
                    PdfPCell ycell50 = new PdfPCell(new PdfPCell(new Paragraph(cbItem.cbrq, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell50.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell50.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell50.Colspan = 1;
                    PdfPCell ycell51 = new PdfPCell(new PdfPCell(new Paragraph("参保状态", new Font(Heiti, 15, Font.NORMAL))));
                    ycell51.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell51.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell51.Colspan = 1;
                    PdfPCell ycell52 = new PdfPCell(new PdfPCell(new Paragraph(cbItem.cbzt_mc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell52.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell52.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell52.Colspan = 1;
                    cbxxtable.AddCell(ycell49);
                    cbxxtable.AddCell(ycell50);
                    cbxxtable.AddCell(ycell51);
                    cbxxtable.AddCell(ycell52);

                    PdfPCell ycell53 = new PdfPCell(new PdfPCell(new Paragraph("比例类别", new Font(Heiti, 15, Font.NORMAL))));
                    ycell53.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell53.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell53.Colspan = 1;
                    PdfPCell ycell54 = new PdfPCell(new PdfPCell(new Paragraph(cbItem.bllb, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell54.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell54.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell54.Colspan = 1;
                    PdfPCell ycell55 = new PdfPCell(new PdfPCell(new Paragraph("险种类型", new Font(Heiti, 15, Font.NORMAL))));
                    ycell55.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell55.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell55.Colspan = 1;
                    PdfPCell ycell56 = new PdfPCell(new PdfPCell(new Paragraph(cbItem.xzlx_mc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell56.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell56.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell56.Colspan = 1;
                    cbxxtable.AddCell(ycell53);
                    cbxxtable.AddCell(ycell54);
                    cbxxtable.AddCell(ycell55);
                    cbxxtable.AddCell(ycell56);

                    cbxxtable.SpacingAfter = 8f;
                    document.Add(cbxxtable);
                }
            }
            else
            {
                PdfPTable cbxxtable = new PdfPTable(cbxxwidhts);
                cbxxtable.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell57 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell57.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell57.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell57.Colspan = 4;
                cbxxtable.AddCell(ycell57);
                document.Add(cbxxtable);

            }
            document.Add(new Paragraph("\n"));
            Paragraph pt15 = new Paragraph("五、良好信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt15);
            document.Add(new Paragraph("\n"));
            float[] lhxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            //PdfPTable lhxxtable = new PdfPTable(lhxxwidhts);
            //lhxxtable.WidthPercentage = 100;
            //PdfPCell ycell58 = new PdfPCell(new PdfPCell(new Paragraph("纳税信用A级", new Font(Heiti, 15, Font.NORMAL))));
            //ycell58.HorizontalAlignment = Element.ALIGN_CENTER;
            //ycell58.VerticalAlignment = Element.ALIGN_MIDDLE;
            //ycell58.Colspan = 4;
            //lhxxtable.AddCell(ycell58);
            //document.Add(lhxxtable);

            Paragraph pt15_1 = new Paragraph("(1)、纳税信用A级", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt15_1);
            document.Add(new Paragraph("\n"));
            if (NsxyjbInfoItems.Count > 0)
            {
                foreach (NsxyjbInfoItem nsxyjbInfoItem in NsxyjbInfoItems)
                {
                    PdfPTable lhxxtable1 = new PdfPTable(lhxxwidhts);
                    lhxxtable1.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell59 = new PdfPCell(new PdfPCell(new Paragraph("企业名称", new Font(Heiti, 15, Font.NORMAL))));
                    ycell59.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell59.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell59.Colspan = 1;
                    PdfPCell ycell60 = new PdfPCell(new PdfPCell(new Paragraph(nsxyjbInfoItem.qymc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell60.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell60.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell60.Colspan = 1;
                    PdfPCell ycell61 = new PdfPCell(new PdfPCell(new Paragraph("守信内容", new Font(Heiti, 15, Font.NORMAL))));
                    ycell61.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell61.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell61.Colspan = 1;
                    PdfPCell ycell62 = new PdfPCell(new PdfPCell(new Paragraph(nsxyjbInfoItem.sxnr, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell62.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell62.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell62.Colspan = 1;
                    lhxxtable1.AddCell(ycell59);
                    lhxxtable1.AddCell(ycell60);
                    lhxxtable1.AddCell(ycell61);
                    lhxxtable1.AddCell(ycell62);

                    PdfPCell ycell63 = new PdfPCell(new PdfPCell(new Paragraph("评定等级", new Font(Heiti, 15, Font.NORMAL))));
                    ycell63.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell63.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell63.Colspan = 1;
                    PdfPCell ycell64 = new PdfPCell(new PdfPCell(new Paragraph(nsxyjbInfoItem.pddj, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell64.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell64.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell64.Colspan = 1;
                    PdfPCell ycell65 = new PdfPCell(new PdfPCell(new Paragraph("发布部门", new Font(Heiti, 15, Font.NORMAL))));
                    ycell65.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell65.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell65.Colspan = 1;
                    PdfPCell ycell66 = new PdfPCell(new PdfPCell(new Paragraph(nsxyjbInfoItem.fbbm, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell66.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell66.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell66.Colspan = 1;
                    lhxxtable1.AddCell(ycell63);
                    lhxxtable1.AddCell(ycell64);
                    lhxxtable1.AddCell(ycell65);
                    lhxxtable1.AddCell(ycell66);

                    PdfPCell ycell67 = new PdfPCell(new PdfPCell(new Paragraph("评定内容", new Font(Heiti, 15, Font.NORMAL))));
                    ycell67.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell67.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell67.Colspan = 1;
                    PdfPCell ycell68 = new PdfPCell(new PdfPCell(new Paragraph(nsxyjbInfoItem.pdnr, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell68.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell68.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell68.Colspan = 1;
                    PdfPCell ycell69 = new PdfPCell(new PdfPCell(new Paragraph("评定年度", new Font(Heiti, 15, Font.NORMAL))));
                    ycell69.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell69.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell69.Colspan = 1;
                    PdfPCell ycell70 = new PdfPCell(new PdfPCell(new Paragraph(nsxyjbInfoItem.pdnd, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell70.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell70.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell70.Colspan = 1;
                    lhxxtable1.AddCell(ycell67);
                    lhxxtable1.AddCell(ycell68);
                    lhxxtable1.AddCell(ycell69);
                    lhxxtable1.AddCell(ycell70);

                    lhxxtable1.SpacingAfter = 8f;
                    document.Add(lhxxtable1);
                }
            }
            else
            {
                PdfPTable lhxxtable1 = new PdfPTable(lhxxwidhts);
                lhxxtable1.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell71 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell71.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell71.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell71.Colspan = 4;
                lhxxtable1.AddCell(ycell71);
                document.Add(lhxxtable1);

            }

            //PdfPTable lhxxtable2 = new PdfPTable(lhxxwidhts);
            //lhxxtable2.WidthPercentage = 100;
            //PdfPCell ycell72 = new PdfPCell(new PdfPCell(new Paragraph("奖励", new Font(Heiti, 15, Font.NORMAL))));
            //ycell72.HorizontalAlignment = Element.ALIGN_CENTER;
            //ycell72.VerticalAlignment = Element.ALIGN_MIDDLE;
            //ycell72.Colspan = 4;
            //lhxxtable2.AddCell(ycell72);
            //document.Add(lhxxtable2);

            Paragraph pt15_2 = new Paragraph("(2)、奖励", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt15_2);
            document.Add(new Paragraph("\n"));

            if (JlInfoItems.Count > 0)
            {
                foreach (JlInfoItem jlInfoItem in JlInfoItems)
                {
                    PdfPTable lhxxtable1 = new PdfPTable(lhxxwidhts);
                    lhxxtable1.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell73 = new PdfPCell(new PdfPCell(new Paragraph("奖励名称", new Font(Heiti, 15, Font.NORMAL))));
                    ycell73.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell73.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell73.Colspan = 1;
                    PdfPCell ycell74 = new PdfPCell(new PdfPCell(new Paragraph(jlInfoItem.jlmc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell74.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell74.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell74.Colspan = 1;
                    PdfPCell ycell75 = new PdfPCell(new PdfPCell(new Paragraph("奖励证书编号", new Font(Heiti, 15, Font.NORMAL))));
                    ycell75.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell75.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell75.Colspan = 1;
                    PdfPCell ycell76 = new PdfPCell(new PdfPCell(new Paragraph(jlInfoItem.jlzsbh, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell76.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell76.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell76.Colspan = 1;
                    lhxxtable1.AddCell(ycell73);
                    lhxxtable1.AddCell(ycell74);
                    lhxxtable1.AddCell(ycell75);
                    lhxxtable1.AddCell(ycell76);

                    PdfPCell ycell77 = new PdfPCell(new PdfPCell(new Paragraph("奖励时间", new Font(Heiti, 15, Font.NORMAL))));
                    ycell77.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell77.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell77.Colspan = 1;
                    PdfPCell ycell78 = new PdfPCell(new PdfPCell(new Paragraph(jlInfoItem.jlsj, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell78.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell78.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell78.Colspan = 1;
                    PdfPCell ycell79 = new PdfPCell(new PdfPCell(new Paragraph("奖励类别", new Font(Heiti, 15, Font.NORMAL))));
                    ycell79.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell79.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell79.Colspan = 1;
                    PdfPCell ycell80 = new PdfPCell(new PdfPCell(new Paragraph(jlInfoItem.jllb_mc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell80.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell80.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell80.Colspan = 1;
                    lhxxtable1.AddCell(ycell77);
                    lhxxtable1.AddCell(ycell78);
                    lhxxtable1.AddCell(ycell79);
                    lhxxtable1.AddCell(ycell80);

                    PdfPCell ycell81 = new PdfPCell(new PdfPCell(new Paragraph("奖励等级", new Font(Heiti, 15, Font.NORMAL))));
                    ycell81.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell81.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell81.Colspan = 1;
                    PdfPCell ycell82 = new PdfPCell(new PdfPCell(new Paragraph(jlInfoItem.jldj, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell82.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell82.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell82.Colspan = 1;
                    PdfPCell ycell83 = new PdfPCell(new PdfPCell(new Paragraph("奖励金额", new Font(Heiti, 15, Font.NORMAL))));
                    ycell83.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell83.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell83.Colspan = 1;
                    PdfPCell ycell84 = new PdfPCell(new PdfPCell(new Paragraph(jlInfoItem.jlje, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell84.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell84.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell84.Colspan = 1;
                    lhxxtable1.AddCell(ycell81);
                    lhxxtable1.AddCell(ycell82);
                    lhxxtable1.AddCell(ycell83);
                    lhxxtable1.AddCell(ycell84);

                    PdfPCell ycell85 = new PdfPCell(new PdfPCell(new Paragraph("奖励原因", new Font(Heiti, 15, Font.NORMAL))));
                    ycell85.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell85.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell85.Colspan = 1;
                    PdfPCell ycell86 = new PdfPCell(new PdfPCell(new Paragraph(jlInfoItem.jlyy, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell86.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell86.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell86.Colspan = 3;
                    lhxxtable1.AddCell(ycell85);
                    lhxxtable1.AddCell(ycell86);

                    lhxxtable1.SpacingAfter = 8f;
                    document.Add(lhxxtable1);
                }
            }
            else
            {
                PdfPTable lhxxtable1 = new PdfPTable(lhxxwidhts);
                lhxxtable1.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell87 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell87.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell87.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell87.Colspan = 4;
                lhxxtable1.AddCell(ycell87);
                document.Add(lhxxtable1);

            }

            //PdfPTable lhxxtable3 = new PdfPTable(lhxxwidhts);
            //lhxxtable3.WidthPercentage = 100;
            //PdfPCell ycell88 = new PdfPCell(new PdfPCell(new Paragraph("荣誉", new Font(Heiti, 15, Font.NORMAL))));
            //ycell88.HorizontalAlignment = Element.ALIGN_CENTER;
            //ycell88.VerticalAlignment = Element.ALIGN_MIDDLE;
            //ycell88.Colspan = 4;
            //lhxxtable3.AddCell(ycell88);
            //document.Add(lhxxtable3);

            Paragraph pt15_3 = new Paragraph("(3)、荣誉", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt15_3);
            document.Add(new Paragraph("\n"));

            if (RyInfoItems.Count > 0)
            {
                foreach (RyInfoItem ryInfoItem in RyInfoItems)
                {
                    PdfPTable lhxxtable1 = new PdfPTable(lhxxwidhts);
                    lhxxtable1.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell89 = new PdfPCell(new PdfPCell(new Paragraph("机构名称", new Font(Heiti, 15, Font.NORMAL))));
                    ycell89.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell89.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell89.Colspan = 1;
                    PdfPCell ycell90 = new PdfPCell(new PdfPCell(new Paragraph(ryInfoItem.jgmc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell90.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell90.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell90.Colspan = 1;
                    PdfPCell ycell91 = new PdfPCell(new PdfPCell(new Paragraph("决定书文号", new Font(Heiti, 15, Font.NORMAL))));
                    ycell91.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell91.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell91.Colspan = 1;
                    PdfPCell ycell92 = new PdfPCell(new PdfPCell(new Paragraph(ryInfoItem.jdswh, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell92.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell92.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell92.Colspan = 1;
                    lhxxtable1.AddCell(ycell89);
                    lhxxtable1.AddCell(ycell90);
                    lhxxtable1.AddCell(ycell91);
                    lhxxtable1.AddCell(ycell92);

                    PdfPCell ycell93 = new PdfPCell(new PdfPCell(new Paragraph("荣誉名称", new Font(Heiti, 15, Font.NORMAL))));
                    ycell93.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell93.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell93.Colspan = 1;
                    PdfPCell ycell94 = new PdfPCell(new PdfPCell(new Paragraph(ryInfoItem.rymc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell94.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell94.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell94.Colspan = 1;
                    PdfPCell ycell95 = new PdfPCell(new PdfPCell(new Paragraph("荣誉内容", new Font(Heiti, 15, Font.NORMAL))));
                    ycell95.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell95.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell95.Colspan = 1;
                    PdfPCell ycell96 = new PdfPCell(new PdfPCell(new Paragraph(ryInfoItem.rynr, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell96.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell96.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell96.Colspan = 1;
                    lhxxtable1.AddCell(ycell93);
                    lhxxtable1.AddCell(ycell94);
                    lhxxtable1.AddCell(ycell95);
                    lhxxtable1.AddCell(ycell96);

                    PdfPCell ycell97 = new PdfPCell(new PdfPCell(new Paragraph("表彰单位", new Font(Heiti, 15, Font.NORMAL))));
                    ycell97.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell97.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell97.Colspan = 1;
                    PdfPCell ycell98 = new PdfPCell(new PdfPCell(new Paragraph(ryInfoItem.bzdw, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell98.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell98.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell98.Colspan = 1;
                    PdfPCell ycell99 = new PdfPCell(new PdfPCell(new Paragraph("取得时间", new Font(Heiti, 15, Font.NORMAL))));
                    ycell99.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell99.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell99.Colspan = 1;
                    PdfPCell ycell100 = new PdfPCell(new PdfPCell(new Paragraph(ryInfoItem.qdsj, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell100.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell100.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell100.Colspan = 1;
                    lhxxtable1.AddCell(ycell97);
                    lhxxtable1.AddCell(ycell98);
                    lhxxtable1.AddCell(ycell99);
                    lhxxtable1.AddCell(ycell100);

                    lhxxtable1.SpacingAfter = 8f;
                    document.Add(lhxxtable1);
                }
            }
            else
            {
                PdfPTable lhxxtable1 = new PdfPTable(lhxxwidhts);
                lhxxtable1.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell101 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell101.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell101.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell101.Colspan = 4;
                lhxxtable1.AddCell(ycell101);
                document.Add(lhxxtable1);

            }

            document.Add(new Paragraph("\n"));
            Paragraph pt16 = new Paragraph("六、不良信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt16);
            document.Add(new Paragraph("\n"));
            float[] blxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            //PdfPTable blxxtable = new PdfPTable(blxxwidhts);
            //blxxtable.WidthPercentage = 100;
            //PdfPCell ycell102 = new PdfPCell(new PdfPCell(new Paragraph("失信被执行人", new Font(Heiti, 15, Font.NORMAL))));
            //ycell102.HorizontalAlignment = Element.ALIGN_CENTER;
            //ycell102.VerticalAlignment = Element.ALIGN_MIDDLE;
            //ycell102.Colspan = 4;
            //blxxtable.AddCell(ycell102);
            //document.Add(blxxtable);

            Paragraph pt16_1 = new Paragraph("(1)、失信被执行人", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt16_1);
            document.Add(new Paragraph("\n"));

            if (SxbzxrInfoItems.Count > 0)
            {
                foreach (SxbzxrInfoItem sxbzxrInfoItem in SxbzxrInfoItems)
                {
                    PdfPTable blxxtable1 = new PdfPTable(blxxwidhts);
                    blxxtable1.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell103 = new PdfPCell(new PdfPCell(new Paragraph("案件编号", new Font(Heiti, 15, Font.NORMAL))));
                    ycell103.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell103.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell103.Colspan = 1;
                    PdfPCell ycell104 = new PdfPCell(new PdfPCell(new Paragraph(sxbzxrInfoItem.ajbh, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell104.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell104.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell104.Colspan = 1;
                    PdfPCell ycell105 = new PdfPCell(new PdfPCell(new Paragraph("履行情况", new Font(Heiti, 15, Font.NORMAL))));
                    ycell105.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell105.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell105.Colspan = 1;
                    PdfPCell ycell106 = new PdfPCell(new PdfPCell(new Paragraph(sxbzxrInfoItem.lxqk, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell106.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell106.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell106.Colspan = 1;
                    blxxtable1.AddCell(ycell103);
                    blxxtable1.AddCell(ycell104);
                    blxxtable1.AddCell(ycell105);
                    blxxtable1.AddCell(ycell106);

                    PdfPCell ycell107 = new PdfPCell(new PdfPCell(new Paragraph("发布日期", new Font(Heiti, 15, Font.NORMAL))));
                    ycell107.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell107.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell107.Colspan = 1;
                    PdfPCell ycell108 = new PdfPCell(new PdfPCell(new Paragraph(sxbzxrInfoItem.fbrq, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell108.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell108.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell108.Colspan = 1;
                    PdfPCell ycell109 = new PdfPCell(new PdfPCell(new Paragraph("执行法院", new Font(Heiti, 15, Font.NORMAL))));
                    ycell109.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell109.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell109.Colspan = 1;
                    PdfPCell ycell110 = new PdfPCell(new PdfPCell(new Paragraph(sxbzxrInfoItem.zxfy, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell110.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell110.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell110.Colspan = 1;
                    blxxtable1.AddCell(ycell107);
                    blxxtable1.AddCell(ycell108);
                    blxxtable1.AddCell(ycell109);
                    blxxtable1.AddCell(ycell110);

                    blxxtable1.SpacingAfter = 8f;
                    document.Add(blxxtable1);
                }
            }
            else
            {
                PdfPTable blxxtable1 = new PdfPTable(blxxwidhts);
                blxxtable1.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell111 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell111.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell111.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell111.Colspan = 4;
                blxxtable1.AddCell(ycell111);
                document.Add(blxxtable1);

            }

            //PdfPTable blxxtable2 = new PdfPTable(blxxwidhts);
            //blxxtable2.WidthPercentage = 100;
            //PdfPCell ycell112 = new PdfPCell(new PdfPCell(new Paragraph("行政处罚", new Font(Heiti, 15, Font.NORMAL))));
            //ycell112.HorizontalAlignment = Element.ALIGN_CENTER;
            //ycell112.VerticalAlignment = Element.ALIGN_MIDDLE;
            //ycell112.Colspan = 4;
            //blxxtable2.AddCell(ycell112);
            //document.Add(blxxtable2);

            Paragraph pt16_2 = new Paragraph("(2)、行政处罚", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt16_2);
            document.Add(new Paragraph("\n"));

            if (XzcfInfoItems.Count > 0)
            {
                foreach (XzcfInfoItem xzcfInfoItem in XzcfInfoItems)
                {
                    PdfPTable blxxtable1 = new PdfPTable(blxxwidhts);
                    blxxtable1.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell113 = new PdfPCell(new PdfPCell(new Paragraph("决定书文号", new Font(Heiti, 15, Font.NORMAL))));
                    ycell113.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell113.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell113.Colspan = 1;
                    PdfPCell ycell114 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.jdswh, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell114.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell114.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell114.Colspan = 1;
                    PdfPCell ycell115 = new PdfPCell(new PdfPCell(new Paragraph("处罚名称", new Font(Heiti, 15, Font.NORMAL))));
                    ycell115.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell115.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell115.Colspan = 1;
                    PdfPCell ycell116 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.cfmc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell116.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell116.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell116.Colspan = 1;
                    blxxtable1.AddCell(ycell113);
                    blxxtable1.AddCell(ycell114);
                    blxxtable1.AddCell(ycell115);
                    blxxtable1.AddCell(ycell116);

                    PdfPCell ycell117 = new PdfPCell(new PdfPCell(new Paragraph("处罚类别1", new Font(Heiti, 15, Font.NORMAL))));
                    ycell117.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell117.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell117.Colspan = 1;
                    PdfPCell ycell118 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.cflb1, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell118.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell118.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell118.Colspan = 1;
                    PdfPCell ycell119 = new PdfPCell(new PdfPCell(new Paragraph("处罚类别2", new Font(Heiti, 15, Font.NORMAL))));
                    ycell119.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell119.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell119.Colspan = 1;
                    PdfPCell ycell120 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.cflb2, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell120.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell120.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell120.Colspan = 1;
                    blxxtable1.AddCell(ycell117);
                    blxxtable1.AddCell(ycell118);
                    blxxtable1.AddCell(ycell119);
                    blxxtable1.AddCell(ycell120);

                    PdfPCell ycell121 = new PdfPCell(new PdfPCell(new Paragraph("处罚事由", new Font(Heiti, 15, Font.NORMAL))));
                    ycell121.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell121.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell121.Colspan = 1;
                    PdfPCell ycell122 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.cfsy, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell122.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell122.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell122.Colspan = 1;
                    PdfPCell ycell123 = new PdfPCell(new PdfPCell(new Paragraph("处罚依据", new Font(Heiti, 15, Font.NORMAL))));
                    ycell123.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell123.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell123.Colspan = 1;
                    PdfPCell ycell124 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.cfyj, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell124.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell124.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell124.Colspan = 1;
                    blxxtable1.AddCell(ycell121);
                    blxxtable1.AddCell(ycell122);
                    blxxtable1.AddCell(ycell123);
                    blxxtable1.AddCell(ycell124);

                    PdfPCell ycell125 = new PdfPCell(new PdfPCell(new Paragraph("处罚结果", new Font(Heiti, 15, Font.NORMAL))));
                    ycell125.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell125.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell125.Colspan = 1;
                    PdfPCell ycell126 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.cfjg, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell126.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell126.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell126.Colspan = 1;
                    PdfPCell ycell127 = new PdfPCell(new PdfPCell(new Paragraph("决定日期", new Font(Heiti, 15, Font.NORMAL))));
                    ycell127.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell127.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell127.Colspan = 1;
                    PdfPCell ycell128 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.jdrq, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell128.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell128.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell128.Colspan = 1;
                    blxxtable1.AddCell(ycell125);
                    blxxtable1.AddCell(ycell126);
                    blxxtable1.AddCell(ycell127);
                    blxxtable1.AddCell(ycell128);

                    PdfPCell ycell129 = new PdfPCell(new PdfPCell(new Paragraph("处罚机关", new Font(Heiti, 15, Font.NORMAL))));
                    ycell129.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell129.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell129.Colspan = 1;
                    PdfPCell ycell130 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.xzjg, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell130.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell130.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell130.Colspan = 1;
                    PdfPCell ycell131 = new PdfPCell(new PdfPCell(new Paragraph("当前状态", new Font(Heiti, 15, Font.NORMAL))));
                    ycell131.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell131.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell131.Colspan = 1;
                    PdfPCell ycell132 = new PdfPCell(new PdfPCell(new Paragraph(xzcfInfoItem.ztmc, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell132.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell132.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell132.Colspan = 1;
                    blxxtable1.AddCell(ycell129);
                    blxxtable1.AddCell(ycell130);
                    blxxtable1.AddCell(ycell131);
                    blxxtable1.AddCell(ycell132);

                    blxxtable1.SpacingAfter = 8f;
                    document.Add(blxxtable1);
                }
            }
            else
            {
                PdfPTable blxxtable1 = new PdfPTable(blxxwidhts);
                blxxtable1.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell133 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell133.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell133.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell133.Colspan = 4;
                blxxtable1.AddCell(ycell133);
                document.Add(blxxtable1);

            }

            //PdfPTable blxxtable3 = new PdfPTable(blxxwidhts);
            //blxxtable3.WidthPercentage = 100;
            //PdfPCell ycell134 = new PdfPCell(new PdfPCell(new Paragraph("失信企业", new Font(Heiti, 15, Font.NORMAL))));
            //ycell134.HorizontalAlignment = Element.ALIGN_CENTER;
            //ycell134.VerticalAlignment = Element.ALIGN_MIDDLE;
            //ycell134.Colspan = 4;
            //blxxtable3.AddCell(ycell134);
            //document.Add(blxxtable3);


            Paragraph pt16_3 = new Paragraph("(3)、失信企业", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt16_3);
            document.Add(new Paragraph("\n"));

            if (SxqyInfoItems.Count > 0)
            {
                foreach (SxqyInfoItem sxqyInfoItem in SxqyInfoItems)
                {
                    PdfPTable blxxtable1 = new PdfPTable(blxxwidhts);
                    blxxtable1.WidthPercentage = 100; // 宽度100%填充

                    PdfPCell ycell135 = new PdfPCell(new PdfPCell(new Paragraph("失信内容", new Font(Heiti, 15, Font.NORMAL))));
                    ycell135.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell135.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell135.Colspan = 1;
                    PdfPCell ycell136 = new PdfPCell(new PdfPCell(new Paragraph(sxqyInfoItem.sxnr, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell136.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell136.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell136.Colspan = 1;
                    PdfPCell ycell137 = new PdfPCell(new PdfPCell(new Paragraph("失信事实", new Font(Heiti, 15, Font.NORMAL))));
                    ycell137.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell137.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell137.Colspan = 1;
                    PdfPCell ycell138 = new PdfPCell(new PdfPCell(new Paragraph(sxqyInfoItem.sxss, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell138.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell138.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell138.Colspan = 1;
                    blxxtable1.AddCell(ycell135);
                    blxxtable1.AddCell(ycell136);
                    blxxtable1.AddCell(ycell137);
                    blxxtable1.AddCell(ycell138);

                    PdfPCell ycell139 = new PdfPCell(new PdfPCell(new Paragraph("发布部门", new Font(Heiti, 15, Font.NORMAL))));
                    ycell139.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell139.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell139.Colspan = 1;
                    PdfPCell ycell140 = new PdfPCell(new PdfPCell(new Paragraph(sxqyInfoItem.fbbm, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell140.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell140.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell140.Colspan = 1;
                    PdfPCell ycell141 = new PdfPCell(new PdfPCell(new Paragraph("发布日期", new Font(Heiti, 15, Font.NORMAL))));
                    ycell141.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell141.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell141.Colspan = 1;
                    PdfPCell ycell142 = new PdfPCell(new PdfPCell(new Paragraph(sxqyInfoItem.fbrq, new Font(Kaiti, 15, Font.NORMAL))));
                    ycell142.HorizontalAlignment = Element.ALIGN_CENTER;
                    ycell142.VerticalAlignment = Element.ALIGN_MIDDLE;
                    ycell142.Colspan = 1;
                    blxxtable1.AddCell(ycell139);
                    blxxtable1.AddCell(ycell140);
                    blxxtable1.AddCell(ycell141);
                    blxxtable1.AddCell(ycell142);

                    blxxtable1.SpacingAfter = 8f;
                    document.Add(blxxtable1);
                }
            }
            else
            {
                PdfPTable blxxtable1 = new PdfPTable(blxxwidhts);
                blxxtable1.WidthPercentage = 100; // 宽度100%填充

                PdfPCell ycell143 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
                ycell143.HorizontalAlignment = Element.ALIGN_CENTER;
                ycell143.VerticalAlignment = Element.ALIGN_MIDDLE;
                ycell143.Colspan = 4;
                blxxtable1.AddCell(ycell143);
                document.Add(blxxtable1);

            }


            document.Close();
            return true;
        }

        public static bool DrawXuancheng_GR(string FilePath, RowsItem rowsItem, List<FcInfoItem> fc, List<LqdjInfoItem> lq, List<ClInfoItem> cl, CbinfoItem cbinfoItem, List<GRRyInfoItem> ry, List<GqdzyzInfoItem> gqt, List<NsxyajInfoItem> nsa, List<ZdsswfajdsrInfoItem> Zd, List<sxbzxrInfo> sx, List<GjjdkyqInfoItem> gjj, List<GRXzcfInfoItem> xzcf)

        {
            const string toLeft1 = "                         ";
            const string toLeft2 = "                ";
            CodeHelper.QRcode("报告编号：" + DateTime.Now.ToString("yyyyMMddHHmmss"), "Temp\\QR.png");
            BaseFont Heiti = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont Kaiti = BaseFont.CreateFont(@"C:\Windows\Fonts\STKAITI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font headfont = new Font(Kaiti, 30, Font.BOLD);// 设置字体大小
            Font keyfont = new Font(Heiti, 14, Font.NORMAL);
            Document document = new Document(PageSize.A4, 25, 25, 10, 10);
            Stream stream = new FileStream(FilePath, FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
            PageEventHelper pageEventHelper = new PageEventHelper();
            pdfWriter.PageEvent = pageEventHelper;

            document.Open();
            Image image = Image.GetInstance("Temp\\QR.png");
            image.ScaleAbsolute(100, 100);
            image.SetAbsolutePosition(450, 650);
            document.Add(image);
            document.Add(new Paragraph("\n\n\n\n\n\n\n\n\n\n\n\n"));
            string[] tip = new string[] { toLeft2 + "法人和非法人组织/个人" + "\r\n" + toLeft2 + "公共信用信息报告", toLeft1 + "自然人名称：", toLeft1 + "身份证号：", toLeft1 + "报告编号：" + DateTime.Now.ToString("yyyyMMddHHmmss"), toLeft1 + "生成日期：" + DateTime.Now.ToString("yyyy年MM月dd日"), toLeft1 + "出具单位：宣城市发展和改革委员会" };
            Font[] f = new Font[] { headfont, keyfont };
            Paragraph pa = new Paragraph();
            for (int i = 0; i < tip.Length; i++)
            {
                string strs = tip[i];
                switch (tip[i])
                {
                    case toLeft1 + "自然人名称：":
                        strs = strs + rowsItem.baseXm;
                        break;
                    case toLeft1 + "身份证号：":
                        strs = strs + rowsItem.baseIdcard;
                        break;
                    default:
                        break;
                }
                pa = Get_paragraph(strs, f[i > 0 ? 1 : 0], 0);
                pa.Alignment = 0;
                document.Add(pa);
                if (i == 0 || i == 3)
                {
                    document.Add(new Paragraph("\n\n\n\n"));
                }
                else
                {
                    document.Add(new Paragraph("\n"));
                }
            }
            document.NewPage();
            document.Add(new Paragraph("\n\n\n"));

            document.Add(Get_paragraph("概要", new Font(Kaiti, 20, Font.BOLD), 1));
            document.Add(new Paragraph("\n\n"));
            document.Add(Get_paragraph(toLeft2 + "一、基础信息", new Font(Heiti, 15, Font.BOLD), 0));
            string[] Jcxx = new string[] { toLeft2 + "1.姓名：" + rowsItem.baseXm, toLeft2 + "2.姓别：" + rowsItem.baseXb, toLeft2 + "3.身份证号：" + rowsItem.baseIdcard, toLeft2 + "4.出生日期：" + rowsItem.baseCsrq, toLeft2 + "5.居住地址：" + rowsItem.baseJzdz };
            for (int i = 0; i < Jcxx.Length; i++)
            {
                document.Add(Get_paragraph(Jcxx[i], new Font(Heiti, 15, Font.NORMAL), 0));
            }
            document.Add(new Paragraph("\n"));
            document.Add(Get_paragraph(toLeft2 + "二、信用综述信息", new Font(Heiti, 15, Font.BOLD), 0));
            string[] xyzs = new string[] { toLeft2 + "1.基本身份信息", toLeft2 + "2.资产信息", toLeft2 + "3.参保信息", toLeft2 + "4.良好信息", toLeft2 + "5.不良信息", };
            for (int i = 0; i < xyzs.Length; i++)
            {
                document.Add(Get_paragraph(xyzs[i], new Font(Heiti, 15, Font.NORMAL), 0));
            }
            document.Add(new Paragraph("\n"));
            document.Add(Get_paragraph(toLeft2 + "三、报告说明", new Font(Heiti, 15, Font.BOLD), 0));
            document.Add(Get_paragraph(toLeft2 + "1.报告提供单位：宣城市发展和改革委员会", new Font(Heiti, 15, Font.NORMAL), 0));
            document.Add(Get_paragraph(toLeft2 + "2.本报告应用于合法途径，违法规定或约定使用本报告，责任由相关人承担。", new Font(Heiti, 15, Font.NORMAL), 0));
            document.NewPage();
            document.Add(new Paragraph("\n\n\n"));
            document.Add(new Paragraph("正文", new Font(Kaiti, 20, Font.BOLD)) { Alignment = 1, });
            document.Add(new Paragraph("\n\n"));
            #region //基础信息
            document.Add(new Paragraph("一、基础信息", new Font(Heiti, 15, Font.BOLD)) { Alignment = 1, });
            document.Add(new Paragraph("\n"));
            float[] basicwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            PdfPTable basictable = new PdfPTable(basicwidhts);
            basictable.WidthPercentage = 100; // 宽度100%填充
            PdfPCell ycell = null;
            PdfPCell ycell2 = null;
            // //====第一行开始=================
            string[] neirong = new string[] { "baseXm", "baseXb", "baseIdcard", "baseCsrq", "baseMz", "baseHjqh", "baseXjz", "baseWhcd", "baseHyzk", "baseZzmm", "baseXx", "baseZjxy", "baseJkzk", "baseSg", "baseCym", "baseHjlx", "baseHjdz", "baseJzdz", "jyztMc", "baseHqsj" };
            string[] ziduan = new string[] { "姓名", "性别", "身份证号码", "出生日期", "民族", "户籍区划", "现住址", "文化程度", "婚姻状况", "政治面貌", "血型", "宗教信仰", "健康状况", "身高", "曾用名", "户籍类型", "户籍地址", "居住地址", "就业状态名称", "获取时间" };
            Dictionary<string, string> str = new Dictionary<string, string>();
            Dictionary<string, string> str2 = new Dictionary<string, string>();
            for (int i = 0; i < neirong.Length; i++)
            {
                str.Add(ziduan[i], neirong[i]);
            }
            foreach (var item in str)
            {
                ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                ycell2 = Get_PdfPCell(rowsItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                switch (item.Key.ToString())
                {
                    case "现住址":
                    case "户籍地址":
                    case "居住地址":
                    case "获取时间":
                        str2.Add(item.Key, item.Value);
                        break;
                    default:
                        basictable.AddCell(ycell);
                        basictable.AddCell(ycell2);
                        break;
                }
            }
            foreach (var item in str2)
            {
                ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                ycell2 = Get_PdfPCell(rowsItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 3);
                basictable.AddCell(ycell);
                basictable.AddCell(ycell2);
            }

            document.Add(basictable);
            #endregion
            document.Add(new Paragraph("\n"));

            Paragraph pt12 = new Paragraph("二、资产信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt12);
            document.Add(new Paragraph("\n"));
            #region //房产
            Paragraph pt122 = new Paragraph("（1）、房产", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt122);
            document.Add(new Paragraph("\n"));

            float[] xkxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };

            if (fc.Count > 0)
            {
                string[] neirong2 = new string[] { "sfzh", "gfsj", "fwdz", "fwmj", "cqzh", "gyr", "hqtj", "hqsj", "dybj" };
                string[] ziduan2 = new string[] { "身份证号", "购房时间", "房屋地址", "房屋面积", "产权证号", "共有人", "获取途径", "获取时间", "是否抵押标记" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (FcInfoItem xkItem in fc)
                {
                    PdfPTable zzxxtable = new PdfPTable(xkxxwidhts);
                    zzxxtable.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            case "房屋地址":
                            case "产权证号":
                            case "共有人":
                                fcList2.Add(item.Key, item.Value);
                                break;
                            default:
                                zzxxtable.AddCell(ycell);
                                zzxxtable.AddCell(ycell2);
                                break;
                        }
                    }
                    foreach (var item in fcList2)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        zzxxtable.AddCell(ycell);
                        zzxxtable.AddCell(ycell2);
                    }
                    zzxxtable.SpacingAfter = 5f;
                    document.Add(zzxxtable);
                    fcList2.Clear();
                }
            }
            else
            {
                document.Add(NoTable());
            }
            #endregion
            document.Add(new Paragraph("\n"));
            #region //车辆
            Paragraph pt1222 = new Paragraph("（2）、车辆", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt1222);
            document.Add(new Paragraph("\n"));
            float[] zzxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            if (cl.Count > 0)
            {
                string[] neirong2 = new string[] { "sfzh", "hphm", "zwpp", "clxh", "fdjh", "cllx_mc", "syxz_mc", "clsbdh", "ccdjrq", "zjdjrq", "sfdy_mc", "hqsj", "qzbfrq", };
                string[] ziduan2 = new string[] { "身份证号", "号牌号码", "中文品牌", "车辆型号", "发动机号", "车辆类型", "使用性质", "车辆识别代号", "初次登记日期", "最近定检日期", "是否抵押：0未抵押1已抵押", "获取时间", "强制报废日期", };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (ClInfoItem zzItem in cl)
                {
                    PdfPTable zzxxtable = new PdfPTable(zzxxwidhts);
                    zzxxtable.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(zzItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            case "强制报废日期":
                                fcList2.Add(item.Key, item.Value);
                                break;
                            default:
                                zzxxtable.AddCell(ycell);
                                zzxxtable.AddCell(ycell2);
                                break;
                        }
                    }
                    foreach (var item in fcList2)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(zzItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        zzxxtable.AddCell(ycell);
                        zzxxtable.AddCell(ycell2);
                    }
                    zzxxtable.SpacingAfter = 5f;
                    document.Add(zzxxtable);
                    fcList2.Clear();
                }
            }
            else
            {
                document.Add(NoTable());

            }
            #endregion
            document.Add(new Paragraph("\n"));
            #region //林权
            Paragraph pt12222 = new Paragraph("（3）、林权", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt12222);
            document.Add(new Paragraph("\n"));
            if (cl.Count > 0)
            {
                string[] neirong2 = new string[] { "sfzh", "zh", "zldz", "mj", "fzjg", "ldsyq", "zzrq", "jzsj" };
                string[] ziduan2 = new string[] { "身份证号", "证号", "坐落地址", "面积", "发证机关", "林地使用期", "终止日期", "加载时间" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (LqdjInfoItem zzItem in lq)
                {
                    PdfPTable zzxxtable = new PdfPTable(zzxxwidhts);
                    zzxxtable.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(zzItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);

                        switch (item.Key.ToString())
                        {
                            default:
                                zzxxtable.AddCell(ycell);
                                zzxxtable.AddCell(ycell2);
                                break;
                        }
                    }

                    zzxxtable.SpacingAfter = 5f;
                    document.Add(zzxxtable);
                }
            }
            else
            {
                document.Add(NoTable());

            }
            #endregion
            document.Add(new Paragraph("\n"));
            #region //参保信息
            Paragraph pt123 = new Paragraph("三、 参保信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(pt123);
            document.Add(new Paragraph("\n"));
            PdfPTable zzzxxtable = new PdfPTable(zzxxwidhts);
            zzzxxtable.WidthPercentage = 100; // 宽度100%填充
            neirong = new string[] { "sfzh", "shbzkkh", "ryzt_mc", "cjgzrq", "jyzt_mc", "cbzt_mc", "yldylb_mc", "ltxrq", "ksny", "zzny", "gz", "jbyljfjs", "jbyiljfjs", "syjfjs", "syujfjs", "xzlx", "hqsj" };
            ziduan = new string[] { "身份证号", "社会保障卡卡号", "人员状态", "参加工作日期", "就业状态", "参保状态", "医疗待遇类别", "离退休日期", "开始年月", "终止年月", "工资", "基本养老保险缴费基数", "基本医疗保险缴费基数", "失业保险缴费基数", "生育保险缴费基数", "险种类型", "获取时间" };
            str = new Dictionary<string, string>();
            str2 = new Dictionary<string, string>();
            for (int i = 0; i < neirong.Length; i++)
            {
                str.Add(ziduan[i], neirong[i]);
            }
            foreach (var item in str)
            {
                ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                ycell2 = Get_PdfPCell(cbinfoItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                switch (item.Key.ToString())
                {
                    default:
                        zzzxxtable.AddCell(ycell);
                        zzzxxtable.AddCell(ycell2);
                        break;
                }
            }
            document.Add(zzzxxtable);
            #endregion
            Paragraph lh = new Paragraph("四、良好信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(lh);
            document.Add(new Paragraph("\n"));
            #region//荣誉
            Paragraph ryT = new Paragraph("（1）、荣誉", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(ryT);
            document.Add(new Paragraph("\n"));
            if (ry.Count > 0)
            {
                string[] neirong2 = new string[] { "sfhm", "jdswh", "rymc", "rynr", "hdsj", "hqsj", "bzdw" };
                string[] ziduan2 = new string[] { "身份证号", "决定书文号", "所获称号", "所获称号内容", "荣誉获得时间", "信息获取时间", "表彰单位" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (GRRyInfoItem xkItem in ry)
                {
                    PdfPTable ryTable = new PdfPTable(xkxxwidhts);
                    ryTable.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            default:
                                ryTable.AddCell(ycell);
                                ryTable.AddCell(ycell2);
                                break;
                        }
                    }
                    ryTable.SpacingAfter = 5f;
                    document.Add(ryTable);
                }
            }
            else
            {
                document.Add(NoTable());

            }
            #endregion
            document.Add(new Paragraph("\n"));

            #region//纳税信用A级
            Paragraph ns = new Paragraph("（2）、纳税信用A级", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(ns);
            document.Add(new Paragraph("\n"));
            if (nsa.Count > 0)
            {

                string[] neirong2 = new string[] { "xm", "sfzh", "sxnr", "pdnd", "fbbm", "hqsj" };
                string[] ziduan2 = new string[] { "姓名", "身份证号", "守信内容", "评定年度", "主管税务机关", "获取时间" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (NsxyajInfoItem xkItem in nsa)
                {
                    PdfPTable nsT = new PdfPTable(xkxxwidhts);
                    nsT.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            default:
                                nsT.AddCell(ycell);
                                nsT.AddCell(ycell2);
                                break;
                        }
                    }
                    nsT.SpacingAfter = 5f;
                    document.Add(nsT);
                }

            }
            else
            {
                document.Add(NoTable());


            }
            #endregion
            document.Add(new Paragraph("\n"));
            #region //共青团志愿者
            Paragraph gqtp = new Paragraph("（3）、共青团志愿者", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(gqtp);
            document.Add(new Paragraph("\n"));
            if (gqt.Count > 0)
            {
                string[] neirong2 = new string[] { "xm", "sfzh", "sxnr", "pdnd", "fbbm", "hqsj" };
                string[] ziduan2 = new string[] { "姓名", "身份证号", "守信内容", "评定年度", "主管税务机关", "获取时间" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (GqdzyzInfoItem xkItem in gqt)
                {
                    PdfPTable gqtT = new PdfPTable(xkxxwidhts);
                    gqtT.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            default:
                                gqtT.AddCell(ycell);
                                gqtT.AddCell(ycell2);
                                break;
                        }
                    }
                    gqtT.SpacingAfter = 5f;
                    document.Add(gqtT);
                }
            }
            else
            {
                document.Add(NoTable());


            }
            #endregion
            document.Add(new Paragraph("\n"));
            Paragraph bl = new Paragraph("四、不良信息", new Font(Heiti, 15, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(bl);
            document.Add(new Paragraph("\n"));
            #region //行政处罚
            Paragraph xzcfp = new Paragraph("（1）、行政处罚", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(xzcfp);
            document.Add(new Paragraph("\n"));
            if (xzcf.Count > 0)
            {
                string[] neirong2 = new string[] { "bmbh", "bmmc", "xxfl", "bh", "jdswh", "cfmc", "cflb1", "cflb2", "cfsy", "cfyj", "xdrMc", "xdrShxym", "xdrZdm", "xdrGsdj", "xdrSwdj", "xdrSfz", "xdrFr", "xdrLx", "cfjg", "jdrq", "xzjg", "zt", "ztmc", "dfbm", "bz", "sjc", "gsqx", "wjscsj" };
                string[] ziduan2 = new string[] { "部门编号", "部门名称", "信息分类", "序号", "决定书文号", "处罚名称", "处罚类别1", "处罚类别2", "处罚事由", "处罚依据", "行政相对人名称", "行政相对人代码_1(统一社会信用代码)", "行政相对人代码_2(组织机构代码)", "行政相对人代码_3(工商登记码)", "行政相对人代码_4(税务登记号)", "行政相对人代码_5(身份证号)", "法定代表人名称", "法定代表人类型", "处罚结果", "决定日期", "处罚机关", "当前状态", "当前状态名称", "地方编码", "备注", "时间戳", "公示期限", "文件上传时间" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (GRXzcfInfoItem xkItem in xzcf)
                {
                    PdfPTable xzt = new PdfPTable(xkxxwidhts);
                    xzt.WidthPercentage = 100; // 宽度100%填充

                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            default:
                                xzt.AddCell(ycell);
                                xzt.AddCell(ycell2);
                                break;
                        }
                    }

                    xzt.SpacingAfter = 5f;
                    document.Add(xzt);
                }
            }
            else
            {
                document.Add(NoTable());

            }
            #endregion 
            document.Add(new Paragraph("\n"));

            #region //重大税收违法案件当事人
            Paragraph Zdp = new Paragraph("（2）、重大税收违法案件当事人", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(Zdp);
            document.Add(new Paragraph("\n"));
            if (Zd.Count > 0)
            {
                string[] neirong2 = new string[] { "sfzh", "sxnr", "fbrq", "zt", "fbbm", "hqsj" };
                string[] ziduan2 = new string[] { "身份证号", "失信内容", "发布日期", "发布状态", "发布部门", "获取时间" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (ZdsswfajdsrInfoItem xkItem in Zd)
                {
                    PdfPTable xzt = new PdfPTable(xkxxwidhts);
                    xzt.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            default:
                                xzt.AddCell(ycell);
                                xzt.AddCell(ycell2);
                                break;
                        }
                    }
                    xzt.SpacingAfter = 5f;
                    document.Add(xzt);
                }
            }
            else
            {
                document.Add(NoTable());

            }
            #endregion 
            document.Add(new Paragraph("\n"));
            #region //失信被执行人信息
            Paragraph sxp = new Paragraph("（3）、失信被执行人信息", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(sxp);
            document.Add(new Paragraph("\n"));
            if (sx.Count > 0)
            {
                string[] neirong2 = new string[] { "zxyjwh ", "zxfy", "bzxrxw", "fbrq", };
                string[] ziduan2 = new string[] { "执行依据文号", "执行法院", "被执行人行为具体情形", "发布日期" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (sxbzxrInfo xkItem in sx)
                {
                    PdfPTable xzt = new PdfPTable(xkxxwidhts);
                    xzt.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            default:
                                xzt.AddCell(ycell);
                                xzt.AddCell(ycell2);
                                break;
                        }
                    }
                    xzt.SpacingAfter = 5f;
                    document.Add(xzt);
                }
            }
            else
            {
                document.Add(NoTable());


            }
            #endregion 
            document.Add(new Paragraph("\n"));
            #region //公积金贷款逾期信息
            Paragraph gj = new Paragraph("（4）、公积金贷款逾期信息", new Font(Heiti, 10, Font.BOLD))
            {
                Alignment = 1,
            };
            document.Add(gj);
            document.Add(new Paragraph("\n"));
            if (gjj.Count > 0)
            {
                string[] neirong2 = new string[] { "xm", "sfzh", "yqsj", "yqje", "zjhksj", "hqsj" };
                string[] ziduan2 = new string[] { "姓名", "身份证号", "逾期时间", "逾期金额", "最近还款时间", "获取时间" };
                Dictionary<string, string> fcList = new Dictionary<string, string>();
                Dictionary<string, string> fcList2 = new Dictionary<string, string>();
                for (int i = 0; i < neirong2.Length; i++)
                {
                    fcList.Add(ziduan2[i], neirong2[i]);
                }
                foreach (GjjdkyqInfoItem xkItem in gjj)
                {
                    PdfPTable xzt = new PdfPTable(xkxxwidhts);
                    xzt.WidthPercentage = 100; // 宽度100%填充
                    foreach (var item in fcList)
                    {
                        ycell = Get_PdfPCell(item.Key, new Font(Heiti, 15, Font.NORMAL), 1);
                        ycell2 = Get_PdfPCell(xkItem[item.Value], new Font(Kaiti, 15, Font.NORMAL), 1);
                        switch (item.Key.ToString())
                        {
                            default:
                                xzt.AddCell(ycell);
                                xzt.AddCell(ycell2);
                                break;
                        }
                    }
                    xzt.SpacingAfter = 5f;
                    document.Add(xzt);
                }
            }
            else
            {

                document.Add(NoTable());
            }
            #endregion 
            document.Add(new Paragraph("\n"));
            document.Close();

            return true;
        }
        /// <summary>
        /// 无数据
        /// </summary>
        /// <returns>暂无信息表格 class_PdfPTable</returns>
        private static PdfPTable NoTable()
        {
            float[] xkxxwidhts = { 0.25f, 0.25f, 0.25f, 0.25f };
            PdfPTable zzxxtable = new PdfPTable(xkxxwidhts);
            zzxxtable.WidthPercentage = 100; // 宽度100%填充
            PdfPCell ycell35 = new PdfPCell(new PdfPCell(new Paragraph("暂无信息", new Font(Kaiti, 15, Font.NORMAL))));
            ycell35.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell35.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell35.Colspan = 4;
            zzxxtable.AddCell(ycell35);
            return zzxxtable;
        }

        static BaseFont Kaiti = BaseFont.CreateFont(@"C:\Windows\Fonts\STKAITI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        /// <summary>
        /// 返回Paragraph
        /// </summary>
        /// <param name="str">显示的内容</param>
        /// <param name="f">字体</param>
        /// <param name="i">Alignment属性</param>
        /// <returns></returns>
        private static Paragraph Get_paragraph(string str, Font f, int i)
        {

            Paragraph elements = new Paragraph(str, f)
            {
                Alignment = i

            };
            return elements;

        }
        private static PdfPCell Get_PdfPCell(string str, Font f, int i)
        {
            PdfPCell ycell = new PdfPCell(new Paragraph(str, f));
            ycell.MinimumHeight = 30f;
            ycell.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell.Colspan = i;
            return ycell;

        }
    }





    public class PageEventHelper : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate template;
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;
            String text = "第 " + pageN.ToString() + " 页";
            template.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\STKAITI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10);
            iTextSharp.text.Rectangle pageSize = document.PageSize;
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(BaseFont.CreateFont(@"C:\Windows\Fonts\STKAITI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10);
            cb.SetTextMatrix(document.LeftMargin, pageSize.GetBottom(document.BottomMargin));
            cb.ShowText(text);
            cb.EndText();
            cb.AddTemplate(template, document.LeftMargin + 10, pageSize.GetBottom(document.BottomMargin));
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }


    /// <summary>
    /// 房产
    /// </summary>
    public class FcInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "gfsj": gfsj = value != "" ? value : "暂无信息"; break;
                    case "gfsjValue": gfsjValue = value != "" ? value : "暂无信息"; break;
                    case "fwdz": fwdz = value != "" ? value : "暂无信息"; break;
                    case "fwmj": fwmj = value != "" ? value : "暂无信息"; break;
                    case "cqzh": cqzh = value != "" ? value : "暂无信息"; break;
                    case "gyr": gyr = value != "" ? value : "暂无信息"; break;
                    case "hqtj": hqtj = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    case "dybj": dybj = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "sfzh": return sfzh;
                    case "gfsj": return gfsj;
                    case "gfsjValue": return gfsjValue;
                    case "fwdz": return fwdz;
                    case "fwmj": return fwmj;
                    case "cqzh": return cqzh;
                    case "gyr": return gyr;
                    case "hqtj": return hqtj;
                    case "hqsj": return hqsj;
                    case "dybj": return dybj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        /// 购房时间
        /// </summary>
        public string gfsj { get; set; }
        /// <summary>
        /// 购房时间（转换后）
        /// </summary>
        public string gfsjValue { get; set; }

        /// <summary>
        ///房屋地址
        /// </summary>
        public string fwdz { get; set; }
        /// <summary>
        /// 房屋面积
        /// </summary>
        public string fwmj { get; set; }
        /// <summary>
        /// 产权证号
        /// </summary>
        public string cqzh { get; set; }
        /// <summary>
        /// 共有人
        /// </summary>
        public string gyr { get; set; }
        /// <summary>
        /// 获取途径
        /// </summary>
        public string hqtj { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public string hqsj { get; set; }
        /// <summary>
        /// 是否抵押标记
        /// </summary>
        public string dybj { get; set; }
    }
    /// <summary>
    /// 林权
    /// </summary>
    public class LqdjInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "zh": zh = value != "" ? value : "暂无信息"; break;
                    case "zldz": zldz = value != "" ? value : "暂无信息"; break;
                    case "mj": mj = value != "" ? value : "暂无信息"; break;
                    case "fzjg": fzjg = value != "" ? value : "暂无信息"; break;
                    case "ldsyq": ldsyq = value != "" ? value : "暂无信息"; break;
                    case "ldsyqValue": ldsyqValue = value != "" ? value : "暂无信息"; break;
                    case "zzrq": zzrq = value != "" ? value : "暂无信息"; break;
                    case "zzrqValue": zzrqValue = value != "" ? value : "暂无信息"; break;
                    case "jzsj": jzsj = value != "" ? value : "暂无信息"; break;

                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "sfzh": return sfzh;
                    case "zh": return zh;
                    case "zldz": return zldz;
                    case "mj": return mj;
                    case "fzjg": return fzjg;
                    case "ldsyq": return ldsyq;
                    case "ldsyqValue": return ldsyqValue;
                    case "zzrq": return zzrq;
                    case "zzrqValue": return zzrqValue;
                    case "jzsj": return jzsj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        /// 证号
        /// </summary>
        public string zh { get; set; }
        /// <summary>
        /// 坐落地址
        /// </summary>
        public string zldz { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public string mj { get; set; }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string fzjg { get; set; }
        /// <summary>
        /// 林地使用期
        /// </summary>
        public string ldsyq { get; set; }
        /// <summary>
        /// 林地使用期（转换后）
        /// </summary>
        public string ldsyqValue { get; set; }
        /// <summary>
        ///终止日期
        /// </summary>
        public string zzrq { get; set; }
        /// <summary>
        ///  终止日期（转换后）
        /// </summary>
        public string zzrqValue { get; set; }
        /// <summary>
        ///加载时间  
        /// </summary>
        public string jzsj { get; set; }
    }
    /// <summary>
    /// （车辆登记信息）
    /// </summary>
    public class ClInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "hphm": hphm = value != "" ? value : "暂无信息"; break;
                    case "zwpp": zwpp = value != "" ? value : "暂无信息"; break;
                    case "clxh": clxh = value != "" ? value : "暂无信息"; break;
                    case "fdjh": fdjh = value != "" ? value : "暂无信息"; break;
                    case "cllx_mc": cllx_mc = value != "" ? value : "暂无信息"; break;
                    case "syxz_mc": syxz_mc = value != "" ? value : "暂无信息"; break;
                    case "clsbdh": clsbdh = value != "" ? value : "暂无信息"; break;
                    case "ccdjrq": ccdjrq = value != "" ? value : "暂无信息"; break;
                    case "ccdjrqValue": ccdjrqValue = value != "" ? value : "暂无信息"; break;
                    case "zjdjrq": zjdjrq = value != "" ? value : "暂无信息"; break;
                    case "zjdjrqValue": zjdjrqValue = value != "" ? value : "暂无信息"; break;
                    case "sfdy_mc": sfdy_mc = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    case "qzbfrq": qzbfrq = value != "" ? value : "暂无信息"; break;
                    case "qzbfrqValue": qzbfrqValue = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "sfzh": return sfzh;
                    case "hphm": return hphm;
                    case "zwpp": return zwpp;
                    case "clxh": return clxh;
                    case "fdjh": return fdjh;
                    case "cllx_mc": return cllx_mc;
                    case "syxz_mc": return syxz_mc;
                    case "clsbdh": return clsbdh;
                    case "ccdjrq": return ccdjrq;
                    case "ccdjrqValue": return ccdjrqValue;
                    case "zjdjrq": return zjdjrq;
                    case "zjdjrqValue": return zjdjrqValue;
                    case "sfdy_mc": return sfdy_mc;
                    case "hqsj": return hqsj;
                    case "qzbfrq": return qzbfrq;
                    case "qzbfrqValue": return qzbfrqValue;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string hphm { get; set; }
        /// <summary>
        /// 中文品牌
        /// </summary>
        public string zwpp { get; set; }
        /// <summary>
        /// 车辆型号
        /// </summary>
        public string clxh { get; set; }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string fdjh { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string cllx_mc { get; set; }
        /// <summary>
        /// 使用性质
        /// </summary>
        public string syxz_mc { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string clsbdh { get; set; }
        /// <summary>
        /// 初次登记日期
        /// </summary>
        public string ccdjrq { get; set; }
        /// <summary>
        /// 初次登记日期（转换后）
        /// </summary>
        public string ccdjrqValue { get; set; }
        /// <summary>
        /// 最近定检日期
        /// </summary>
        public string zjdjrq { get; set; }
        /// <summary>
        ///  最近定检日期（转换后）
        /// </summary>
        public string zjdjrqValue { get; set; }
        /// <summary>
        ///是否抵押：0未抵押1已抵押
        /// </summary>
        public string sfdy_mc { get; set; }
        /// <summary>
        ///  获取时间
        /// </summary>
        public string hqsj { get; set; }
        /// <summary>
        /// 强制报废日期
        /// </summary>
        public string qzbfrq { get; set; }
        /// <summary>
        ///强制报废日期（转换后）
        /// </summary>
        public string qzbfrqValue { get; set; }
    }
    /// <summary>
    /// 身份信息
    /// </summary>
    public class RowsItem
    {
        public RowsItem()
        {

        }
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": Id = value != "" ? value : "暂无信息"; break;
                    case "baseXm": baseXm = value != "" ? value : "暂无信息"; break;
                    case "baseXb": baseXb = value != "" ? value : "暂无信息"; break;
                    case "baseIdcard": baseIdcard = value != "" ? value : "暂无信息"; break;
                    case "baseCsrq": baseCsrq = value != "" ? value : "暂无信息"; break;
                    case "baseMz": baseMz = value != "" ? value : "暂无信息"; break;
                    case "baseHjqh": baseHjqh = value != "" ? value : "暂无信息"; break;
                    case "baseXjz": baseXjz = value != "" ? value : "暂无信息"; break;
                    case "baseWhcd": baseWhcd = value != "" ? value : "暂无信息"; break;
                    case "baseHyzk": baseHyzk = value != "" ? value : "暂无信息"; break;
                    case "baseZzmm": baseZzmm = value != "" ? value : "暂无信息"; break;
                    case "baseXx": baseXx = value != "" ? value : "暂无信息"; break;
                    case "baseZjxy": baseZjxy = value != "" ? value : "暂无信息"; break;
                    case "baseJkzk": baseJkzk = value != "" ? value : "暂无信息"; break;
                    case "baseSg": baseSg = value != "" ? value : "暂无信息"; break;
                    case "baseCym": baseCym = value != "" ? value : "暂无信息"; break;
                    case "baseHjlx": baseHjlx = value != "" ? value : "暂无信息"; break;
                    case "baseHjdz": baseHjdz = value != "" ? value : "暂无信息"; break;
                    case "baseJzdz": baseJzdz = value != "" ? value : "暂无信息"; break;
                    case "jyztMc": jyztMc = value != "" ? value : "暂无信息"; break;
                    case "baseHqsj": baseHqsj = value != "" ? value : "暂无信息"; break;

                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return Id;
                    case "baseXm": return baseXm;
                    case "baseXb": return baseXb;
                    case "baseIdcard": return baseIdcard;
                    case "baseCsrq": return baseCsrq;
                    case "baseMz": return baseMz;
                    case "baseHjqh": return baseHjqh;
                    case "baseXjz": return baseXjz;
                    case "baseWhcd": return baseWhcd;
                    case "baseHyzk": return baseHyzk;
                    case "baseZzmm": return baseZzmm;
                    case "baseXx": return baseXx;
                    case "baseZjxy": return baseZjxy;
                    case "baseJkzk": return baseJkzk;
                    case "baseSg": return baseSg;
                    case "baseCym": return baseCym;
                    case "baseHjlx": return baseHjlx;
                    case "baseHjdz": return baseHjdz;
                    case "baseJzdz": return baseJzdz;
                    case "jyztMc": return jyztMc;
                    case "baseHqsj": return baseHqsj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// 姓名
        /// </summary>
        public string baseXm { get; set; } = "";
        /// <summary>
        ///     性别
        /// </summary>
        public string baseXb { get; set; } = "";
        /// <summary>
        ///         身份证号码
        /// </summary>
        public string baseIdcard { get; set; } = "";
        /// <summary>
        ///出生日期
        /// </summary>
        public string baseCsrq { get; set; } = "";
        /// <summary>
        ///民族
        /// </summary>
        public string baseMz { get; set; } = "";
        /// <summary>
        /// 户籍区划
        /// </summary>
        public string baseHjqh { get; set; } = "";
        /// <summary>
        ///现住址
        /// </summary>
        public string baseXjz { get; set; } = "";
        /// <summary>
        ///文化程度
        /// </summary>
        public string baseWhcd { get; set; } = "";
        /// <summary>
        ///婚姻状况
        /// </summary>
        public string baseHyzk { get; set; } = "";
        /// <summary>
        ///政治面貌
        /// </summary>
        public string baseZzmm { get; set; } = "";
        /// <summary>
        ///血型
        /// </summary>
        public string baseXx { get; set; } = "";
        /// <summary>
        ///宗教信仰
        /// </summary>
        public string baseZjxy { get; set; } = "";
        /// <summary>
        ///健康状况
        /// </summary>
        public string baseJkzk { get; set; } = "";
        /// <summary>
        ///身高
        /// </summary>
        public string baseSg { get; set; } = "";
        /// <summary>
        ///曾用名
        /// </summary>
        public string baseCym { get; set; } = "";
        /// <summary>
        ///户籍类型
        /// </summary>
        public string baseHjlx { get; set; } = "";
        /// <summary>
        /// 户籍地址
        /// </summary>
        public string baseHjdz { get; set; } = "";
        /// <summary>
        /// 居住地址   
        /// </summary>
        public string baseJzdz { get; set; } = "";
        /// <summary>
        ///就业状态名称
        /// </summary>
        public string jyztMc { get; set; } = "";
        /// <summary>     
        /// 获取时间
        /// </summary>
        public string baseHqsj { get; set; } = "";
    }
    /// <summary>
    /// 参保登记信息
    /// </summary>
    public class CbinfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "shbzkkh": shbzkkh = value != "" ? value : "暂无信息"; break;
                    case "ryzt_mc": ryzt_mc = value != "" ? value : "暂无信息"; break;
                    case "cjgzrq": cjgzrq = value != "" ? value : "暂无信息"; break;
                    case "cjgzrqValue": cjgzrqValue = value != "" ? value : "暂无信息"; break;
                    case "jyzt_mc": jyzt_mc = value != "" ? value : "暂无信息"; break;
                    case "cbzt_mc": cbzt_mc = value != "" ? value : "暂无信息"; break;
                    case "yldylb_mc": yldylb_mc = value != "" ? value : "暂无信息"; break;
                    case "ltxrq": ltxrq = value != "" ? value : "暂无信息"; break;
                    case "ltxrqValue": ltxrqValue = value != "" ? value : "暂无信息"; break;
                    case "ksny": ksny = value != "" ? value : "暂无信息"; break;
                    case "zzny": zzny = value != "" ? value : "暂无信息"; break;
                    case "gz": gz = value != "" ? value : "暂无信息"; break;
                    case "jbyljfjs": jbyljfjs = value != "" ? value : "暂无信息"; break;
                    case "jbyiljfjs": jbyiljfjs = value != "" ? value : "暂无信息"; break;
                    case "syjfjs": syjfjs = value != "" ? value : "暂无信息"; break;
                    case "syujfjs": syujfjs = value != "" ? value : "暂无信息"; break;
                    case "xzlx": xzlx = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id != null && id.Length > 0 ? id : "暂无信息";
                    case "sfzh": return sfzh != null && sfzh.Length > 0 ? sfzh : "暂无信息";
                    case "shbzkkh": return shbzkkh != null && shbzkkh.Length > 0 ? shbzkkh : "暂无信息";
                    case "ryzt_mc": return ryzt_mc != null && ryzt_mc.Length > 0 ? ryzt_mc : "暂无信息";
                    case "cjgzrq": return cjgzrq != null && cjgzrq.Length > 0 ? cjgzrq : "暂无信息";
                    case "cjgzrqValue": return cjgzrqValue != null && cjgzrqValue.Length > 0 ? cjgzrqValue : "暂无信息";
                    case "jyzt_mc": return jyzt_mc != null && jyzt_mc.Length > 0 ? jyzt_mc : "暂无信息";
                    case "cbzt_mc": return cbzt_mc != null && cbzt_mc.Length > 0 ? cbzt_mc : "暂无信息";
                    case "yldylb_mc": return yldylb_mc != null && yldylb_mc.Length > 0 ? yldylb_mc : "暂无信息";
                    case "ltxrq": return ltxrq != null && ltxrq.Length > 0 ? ltxrq : "暂无信息";
                    case "ltxrqValue": return ltxrqValue != null && ltxrqValue.Length > 0 ? ltxrqValue : "暂无信息";
                    case "ksny": return ksny != null && ksny.Length > 0 ? ksny : "暂无信息";
                    case "zzny": return zzny != null && zzny.Length > 0 ? zzny : "暂无信息";
                    case "gz": return gz != null && gz.Length > 0 ? gz : "暂无信息";
                    case "jbyljfjs": return jbyljfjs != null && jbyljfjs.Length > 0 ? jbyljfjs : "暂无信息";
                    case "jbyiljfjs": return jbyiljfjs != null && jbyiljfjs.Length > 0 ? jbyiljfjs : "暂无信息";
                    case "syjfjs": return syjfjs != null && syjfjs.Length > 0 ? syjfjs : "暂无信息";
                    case "syujfjs": return syujfjs != null && syujfjs.Length > 0 ? syujfjs : "暂无信息";
                    case "xzlx": return xzlx != null && xzlx.Length > 0 ? xzlx : "暂无信息";
                    case "hqsj": return hqsj != null && hqsj.Length > 0 ? hqsj : "暂无信息";
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        /// 社会保障卡卡号
        /// </summary>
        public string shbzkkh { get; set; }
        /// <summary>
        /// 人员状态
        /// </summary>
        public string ryzt_mc { get; set; }
        /// <summary>
        /// 参加工作日期（转换后）
        /// </summary>
        public string cjgzrq { get; set; }
        /// <summary>
        ///  参加工作日期（转换后）
        /// </summary>
        public string cjgzrqValue { get; set; }
        /// <summary>
        /// 就业状态
        /// </summary>
        public string jyzt_mc { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>
        public string cbzt_mc { get; set; }
        /// <summary>
        /// 医疗待遇类别
        /// </summary>
        public string yldylb_mc { get; set; }
        /// <summary>
        /// 离退休日期离退休日期（转换后）
        /// </summary>
        public string ltxrq { get; set; }
        /// <summary>
        ///  离退休日期（转换后）
        /// </summary>
        public string ltxrqValue { get; set; }
        /// <summary>
        /// 开始年月
        /// </summary>
        public string ksny { get; set; }
        /// <summary>
        /// 终止年月
        /// </summary>
        public string zzny { get; set; }
        /// <summary>
        /// 工资
        /// </summary>
        public string gz { get; set; }
        /// <summary>
        /// 基本养老保险缴费基数
        /// </summary>
        public string jbyljfjs { get; set; }
        /// <summary>
        /// 基本医疗保险缴费基数
        /// </summary>
        public string jbyiljfjs { get; set; }
        /// <summary>
        /// 失业保险缴费基数
        /// </summary>
        public string syjfjs { get; set; }
        /// <summary>
        /// 生育保险缴费基数
        /// </summary>
        public string syujfjs { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string xzlx { get; set; }
        /// <summary>
        /// 获取时间
        /// </summary>
        public string hqsj { get; set; }
    }

    #region//良好信息

    /// <summary>
    /// 荣誉信息
    /// </summary>
    public class GRRyInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "sfhm": sfhm = value != "" ? value : "暂无信息"; break;
                    case "jdswh": jdswh = value != "" ? value : "暂无信息"; break;
                    case "rymc": rymc = value != "" ? value : "暂无信息"; break;
                    case "rynr": rynr = value != "" ? value : "暂无信息"; break;
                    case "hdsj": hdsj = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    case "bzdw": bzdw = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "sfhm": return sfhm;
                    case "jdswh": return jdswh;
                    case "rymc": return rymc;
                    case "rynr": return rynr;
                    case "hdsj": return hdsj;
                    case "hqsj": return hqsj;
                    case "bzdw": return bzdw;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfhm { get; set; }
        /// <summary>
        /// 决定书文号
        /// </summary>
        public string jdswh { get; set; }
        /// <summary>
        /// 所获称号
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// 所获称号内容
        /// </summary>
        public string rynr { get; set; }
        /// <summary>
        /// 荣誉获得时间
        /// </summary>
        public string hdsj { get; set; }
        /// <summary>
        /// 信息获取时间
        /// </summary>
        public string hqsj { get; set; }
        /// <summary>
        /// 表彰单位
        /// </summary>
        public string bzdw { get; set; }


    }
    /// <summary>
    /// 共青团志愿者
    /// </summary>
    public class GqdzyzInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "xm": xm = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "sxnr": sxnr = value != "" ? value : "暂无信息"; break;
                    case "pdnd": pdnd = value != "" ? value : "暂无信息"; break;
                    case "fbbm": fbbm = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "xm": return xm;
                    case "sfzh": return sfzh;
                    case "sxnr": return sxnr;
                    case "pdnd": return pdnd;
                    case "fbbm": return fbbm;
                    case "hqsj": return hqsj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }
        /// <summary>
        ///主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        ///身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        ///守信内容
        /// </summary>
        public string sxnr { get; set; }
        /// <summary>
        ///评定年度
        /// </summary>
        public string pdnd { get; set; }
        /// <summary>
        ///主管税务机关
        /// </summary>
        public string fbbm { get; set; }
        /// <summary>
        ///获取时间
        /// </summary>
        public string hqsj { get; set; }

    }
    /// <summary>
    /// 纳税信用A级
    /// </summary>
    public class NsxyajInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "xm": xm = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "sxnr": sxnr = value != "" ? value : "暂无信息"; break;
                    case "pdnd": pdnd = value != "" ? value : "暂无信息"; break;
                    case "fbbm": fbbm = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "xm": return xm;
                    case "sfzh": return sfzh;
                    case "sxnr": return sxnr;
                    case "pdnd": return pdnd;
                    case "fbbm": return fbbm;
                    case "hqsj": return hqsj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }

        /// <summary>
        ///主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        ///身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        ///守信内容
        /// </summary>
        public string sxnr { get; set; }
        /// <summary>
        ///评定年度
        /// </summary>
        public string pdnd { get; set; }
        /// <summary>
        ///主管税务机关
        /// </summary>
        public string fbbm { get; set; }
        /// <summary>
        ///获取时间
        /// </summary>
        public string hqsj { get; set; }
    }

    #endregion
    #region//不良信息
    /// <summary>
    /// 重大税收违法案件当事人
    /// </summary>
    public class ZdsswfajdsrInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "sxnr": sxnr = value != "" ? value : "暂无信息"; break;
                    case "fbrq": fbrq = value != "" ? value : "暂无信息"; break;
                    case "zt": zt = value != "" ? value : "暂无信息"; break;
                    case "fbbm": fbbm = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "sfzh": return sfzh;
                    case "sxnr": return sxnr;
                    case "fbrq": return fbrq;
                    case "zt": return zt;
                    case "fbbm": return fbbm;
                    case "hqsj": return hqsj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }
        /// <summary>
        ///主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        ///失信内容
        /// </summary>
        public string sxnr { get; set; }
        /// <summary>
        ///发布日期
        /// </summary>
        public string fbrq { get; set; }
        /// <summary>
        ///发布状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        ///发布部门
        /// </summary>
        public string fbbm { get; set; }
        /// <summary>
        ///获取时间
        /// </summary>
        public string hqsj { get; set; }

    }
    /// <summary>
    /// 失信被执行人信息
    /// </summary>
    public class sxbzxrInfo
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "zxyjwh": zxyjwh = value != "" ? value : "暂无信息"; break;
                    case "zxfy": zxfy = value != "" ? value : "暂无信息"; break;
                    case "bzxrxw": bzxrxw = value != "" ? value : "暂无信息"; break;
                    case "fbrq": fbrq = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "zxyjwh": return zxyjwh;
                    case "zxfy": return zxfy;
                    case "bzxrxw": return bzxrxw;
                    case "fbrq": return fbrq;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }
        /// <summary>
        /// 执行依据文号
        /// </summary>
        public string zxyjwh { get; set; }
        /// <summary>
        ///执行法院
        /// </summary>
        public string zxfy { get; set; }
        /// <summary>
        /// 被执行人行为具体情形
        /// </summary>
        public string bzxrxw { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string fbrq { get; set; }
    }
    /// <summary>
    /// 行政处罚信息
    /// </summary>
    public class GRXzcfInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "recordId": recordId = value != "" ? value : "暂无信息"; break;
                    case "areaOid": areaOid = value != "" ? value : "暂无信息"; break;
                    case "bmbh": bmbh = value != "" ? value : "暂无信息"; break;
                    case "bmmc": bmmc = value != "" ? value : "暂无信息"; break;
                    case "xxfl": xxfl = value != "" ? value : "暂无信息"; break;
                    case "bh": bh = value != "" ? value : "暂无信息"; break;
                    case "jdswh": jdswh = value != "" ? value : "暂无信息"; break;
                    case "cfmc": cfmc = value != "" ? value : "暂无信息"; break;
                    case "cflb1": cflb1 = value != "" ? value : "暂无信息"; break;
                    case "cflb2": cflb2 = value != "" ? value : "暂无信息"; break;
                    case "cfsy": cfsy = value != "" ? value : "暂无信息"; break;
                    case "cfyj": cfyj = value != "" ? value : "暂无信息"; break;
                    case "xdrMc": xdrMc = value != "" ? value : "暂无信息"; break;
                    case "xdrShxym": xdrShxym = value != "" ? value : "暂无信息"; break;
                    case "xdrZdm": xdrZdm = value != "" ? value : "暂无信息"; break;
                    case "xdrGsdj": xdrGsdj = value != "" ? value : "暂无信息"; break;
                    case "xdrSwdj": xdrSwdj = value != "" ? value : "暂无信息"; break;
                    case "xdrSfz": xdrSfz = value != "" ? value : "暂无信息"; break;
                    case "xdrFr": xdrFr = value != "" ? value : "暂无信息"; break;
                    case "xdrLx": xdrLx = value != "" ? value : "暂无信息"; break;
                    case "cfjg": cfjg = value != "" ? value : "暂无信息"; break;
                    case "jdrq": jdrq = value != "" ? value : "暂无信息"; break;
                    case "xzjg": xzjg = value != "" ? value : "暂无信息"; break;
                    case "zt": zt = value != "" ? value : "暂无信息"; break;
                    case "ztmc": ztmc = value != "" ? value : "暂无信息"; break;
                    case "dfbm": dfbm = value != "" ? value : "暂无信息"; break;
                    case "bz": bz = value != "" ? value : "暂无信息"; break;
                    case "glid": glid = value != "" ? value : "暂无信息"; break;
                    case "sjc": sjc = value != "" ? value : "暂无信息"; break;
                    case "gsqx": gsqx = value != "" ? value : "暂无信息"; break;
                    case "infoItem": infoItem = value != "" ? value : "暂无信息"; break;
                    case "gsrq": gsrq = value != "" ? value : "暂无信息"; break;
                    case "wjscsj": wjscsj = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "recordId": return recordId;
                    case "areaOid": return areaOid;
                    case "bmbh": return bmbh;
                    case "bmmc": return bmmc;
                    case "xxfl": return xxfl;
                    case "bh": return bh;
                    case "jdswh": return jdswh;
                    case "cfmc": return cfmc;
                    case "cflb1": return cflb1;
                    case "cflb2": return cflb2;
                    case "cfsy": return cfsy;
                    case "cfyj": return cfyj;
                    case "xdrMc": return xdrMc;
                    case "xdrShxym": return xdrShxym;
                    case "xdrZdm": return xdrZdm;
                    case "xdrGsdj": return xdrGsdj;
                    case "xdrSwdj": return xdrSwdj;
                    case "xdrSfz": return xdrSfz;
                    case "xdrFr": return xdrFr;
                    case "xdrLx": return xdrLx;
                    case "cfjg": return cfjg;
                    case "jdrq": return jdrq;
                    case "xzjg": return xzjg;
                    case "zt": return zt;
                    case "ztmc": return ztmc;
                    case "dfbm": return dfbm;
                    case "bz": return bz;
                    case "glid": return glid;
                    case "sjc": return sjc;
                    case "gsqx": return gsqx;
                    case "infoItem": return infoItem;
                    case "gsrq": return gsrq;
                    case "wjscsj": return wjscsj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }

        /// <summary>
        ///物理主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///记录id
        /// </summary>
        public string recordId { get; set; }
        /// <summary>
        ///区划id
        /// </summary>
        public string areaOid { get; set; }
        /// <summary>
        ///部门编号
        /// </summary>
        public string bmbh { get; set; }
        /// <summary>
        ///部门名称
        /// </summary>
        public string bmmc { get; set; }
        /// <summary>
        ///信息分类
        /// </summary>
        public string xxfl { get; set; }
        /// <summary>
        ///序号
        /// </summary>
        public string bh { get; set; }
        /// <summary>
        ///决定书文号
        /// </summary>
        public string jdswh { get; set; }
        /// <summary>
        ///处罚名称
        /// </summary>
        public string cfmc { get; set; }
        /// <summary>
        ///处罚类别1
        /// </summary>
        public string cflb1 { get; set; }
        /// <summary>
        ///处罚类别2
        /// </summary>
        public string cflb2 { get; set; }
        /// <summary>
        ///处罚事由
        /// </summary>
        public string cfsy { get; set; }
        /// <summary>
        ///处罚依据
        /// </summary>
        public string cfyj { get; set; }
        /// <summary>
        ///行政相对人名称
        /// </summary>
        public string xdrMc { get; set; }
        /// <summary>
        ///行政相对人代码_1(统一社会信用代码)
        /// </summary>
        public string xdrShxym { get; set; }
        /// <summary>
        ///行政相对人代码_2(组织机构代码)
        /// </summary>
        public string xdrZdm { get; set; }
        /// <summary>
        ///行政相对人代码_3(工商登记码)
        /// </summary>
        public string xdrGsdj { get; set; }
        /// <summary>
        ///行政相对人代码_4(税务登记号)
        /// </summary>
        public string xdrSwdj { get; set; }
        /// <summary>
        ///行政相对人代码_5(身份证号)
        /// </summary>
        public string xdrSfz { get; set; }
        /// <summary>
        ///法定代表人名称
        /// </summary>
        public string xdrFr { get; set; }
        /// <summary>
        ///法定代表人类型
        /// </summary>
        public string xdrLx { get; set; }
        /// <summary>
        ///处罚结果
        /// </summary>
        public string cfjg { get; set; }
        /// <summary>
        ///决定日期
        /// </summary>
        public string jdrq { get; set; }
        /// <summary>
        ///处罚机关
        /// </summary>
        public string xzjg { get; set; }
        /// <summary>
        ///当前状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        ///当前状态名称
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        ///地方编码
        /// </summary>
        public string dfbm { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        ///关联id
        /// </summary>
        public string glid { get; set; }
        /// <summary>
        ///时间戳
        /// </summary>
        public string sjc { get; set; }
        /// <summary>
        ///公示期限
        /// </summary>
        public string gsqx { get; set; }
        /// <summary>
        ///？？
        /// </summary>
        public string infoItem { get; set; }
        /// <summary>
        ///？？
        /// </summary>
        public string gsrq { get; set; }
        /// <summary>
        ///文件上传时间
        /// </summary>
        public string wjscsj { get; set; }
    }

    /// <summary>
    /// 公积金贷款逾期信息
    /// </summary>
    public class GjjdkyqInfoItem
    {
        public string this[string index]
        {
            set
            {
                switch (index)
                {
                    case "id": id = value != "" ? value : "暂无信息"; break;
                    case "xm": xm = value != "" ? value : "暂无信息"; break;
                    case "sfzh": sfzh = value != "" ? value : "暂无信息"; break;
                    case "yqsj": yqsj = value != "" ? value : "暂无信息"; break;
                    case "yqje": yqje = value != "" ? value : "暂无信息"; break;
                    case "zjhksj": zjhksj = value != "" ? value : "暂无信息"; break;
                    case "hqsj": hqsj = value != "" ? value : "暂无信息"; break;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_set_索引index（字段）=" + index)); Log.Write(index);
                        break;
                }
            }
            get
            {
                switch (index)
                {
                    case "id": return id;
                    case "xm": return xm;
                    case "sfzh": return sfzh;
                    case "yqsj": return yqsj;
                    case "yqje": return yqje;
                    case "zjhksj": return zjhksj;
                    case "hqsj": return hqsj;
                    default:
                        Log.WriteException(new ArgumentOutOfRangeException(this.ToString() + "_get_索引index（字段）=" + index)); Log.Write(index);
                        return "";//空
                }
            }
        }
        /// <summary>
        ///主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        ///身份证号
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        ///逾期时间
        /// </summary>
        public string yqsj { get; set; }
        /// <summary>
        ///逾期金额
        /// </summary>
        public string yqje { get; set; }
        /// <summary>
        ///最近还款时间
        /// </summary>
        public string zjhksj { get; set; }
        /// <summary>
        ///获取时间
        /// </summary>
        public string hqsj { get; set; }
    }
    #endregion

}
