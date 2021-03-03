using iTextSharp.text;
using iTextSharp.text.pdf;
using java.io;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz.Util;
using System;
using System.IO;

namespace EXC
{
    public class DrawPDF
    {
        public static void txqlzmPdf1()
        {
            String strdjzldata = "{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-19 11:11:11\",\"FCZFZSJ\":\"2020-08-19 11:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-19 11:11:11\",\"QLJSSJ\":\"2020-08-19 11:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人2\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人1\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}";
            String strdyxxdata = "{\"DYFS\":\"xxx抵押\",\"YWH\":\"TD011bb9b0c5611bb9b0c508708a8c8092\",\"ZWR\":\"王明珠\",\"BDCZMH\":\"泰州他项(2008)第3060号\",\"ZSBH\":\"9447\",\"DBFW\":\"详见合同\",\"ZGZQQDSSHSE\":1213.113,\"FCDYMJ\":111.1,\"TDDYMJ\":32.3,\"BIZ\":\"人民币\",\"DJSJ\":\"1899-01-01 00:00:00\",\"ZQLXQX\":\"20070604至20170604\",\"ZXSJ\":\"2020-03-31 00:03:57\",\"ZT\":\"现势\",\"FJ\":\"1、抵押\\r\\n2、许可抵押面积(平方米)：32.30平方米\\r\\n3、许可抵押金额(大写)：土地资产不计入抵押担保值\",\"QT\":\"dsfasdgdfagsasd \",\"BDCZH\":\"泰州国用(2008)第5620号\",\"BZ\":\"bz333\",\"QLRXX\":{\"DYR\":[{\"DYR\":\"王明珠\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"320623198201293515\"}],\"DYQR\":[{\"DYQR\":\"中国建设银行股份有限公司泰州分行\",\"DYQRZJZL\":\"身份证\",\"DYQRZJHM\":\"320623198201293516\"}]}}";
            String strygxxdata = "{\"BDCDYH\":\"321202050002GB00019F00250107\",\"FWDM\":\"TS-190108093940-360061C2\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"包含土地、包含建筑物\",\"YT\":\"成套住宅\",\"ZL\":\"水映蓝庭25幢107室\",\"FWXZ\":\"市场化商品房\",\"FWJG\":\"aaa\",\"SJC\":\"1\",\"ZCS\":\"1\",\"FWLX\":\"bbb\",\"MYC\":\"1\",\"ZRZH\":\"0025\",\"FJH\":\"107\",\"JZMJ\":1,\"TNMJ\":2,\"FTMJ\":33.11,\"DJSJ\":\"2019-01-09 10:32:36\",\"FCZFZSJ\":\"2019-02-13 00:00:00\",\"TDQLLX\":\"国有建设用地使用权\",\"QLQSSJ\":\"2017-04-01 10:02:03\",\"QLJSSJ\":\"2020-04-01 00:00:00\",\"TDQLXZ\":\"出让\",\"TDYT\":\"城镇住宅用地\",\"FJ\":\"fdsvdsfsdaf\",\"TDSYQMJ\":123123,\"TDFTMJ\":3.0006,\"TDDYMJ\":1.001,\"YWR\":\"预告义务人\",\"ZDMJ\":\"宗地面积\",\"ZDDM\":\"宗地代码\",\"QLQTZK\":\"权利其他状况\",\"TDZH\":\"土地证号\",\"DYXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"BDCDJZMH\":\"20190008679\",\"DYQR\":\"无味\",\"DYJE\":123,\"DYKSSJ\":\"2020-04-01 00:00:00\",\"DYJSSJ\":\"2021-04-28 09:04:56\",\"DJSJ\":\"2019-11-11 10:37:16\",\"QSZT\":\"现势\",\"FJ\":\"543523452345\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\",\"DYFS\":\"一般抵押\",\"DBFW\":\"详见抵押合同\",\"DYR\":\"吴海洋,魏爱霞\"}],\"CFXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"CFJG\":\"泰州法院\",\"CFLX\":\"预查封\",\"CFWH\":\"2020-0241\",\"CFKSSJ\":\"2020-03-19 00:00:00\",\"CFJSSJ\":\"2021-03-18 00:00:00\",\"DJSJ\":\"2020-03-19 17:09:44\",\"QSZT\":\"现势\",\"FJ\":\"123123123\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"},{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"CFJG\":\"泰州法院1\",\"CFLX\":\"预查封\",\"CFWH\":\"2020-0241\",\"CFKSSJ\":\"2020-03-19 00:00:00\",\"CFJSSJ\":\"2021-03-18 00:00:00\",\"DJSJ\":\"2020-03-19 17:09:44\",\"QSZT\":\"现势\",\"FJ\":\"123123123\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}],\"QLR\":[{\"QLRMC\":\"吴海洋\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"32128419821129103X\",\"GYFS\":\"共同共有\",\"QLBL\":\"30%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"},{\"QLRMC\":\"魏爱霞1\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"321284198202010021\",\"GYFS\":\"共同共有\",\"QLBL\":\"70%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}]}";
            //JSONObject djzldata = JSONObject.parseObject(strdjzldata);
            //JSONObject dyxxdata = JSONObject.parseObject(strdyxxdata);
            //JSONObject ygxxdata = JSONObject.parseObject(strygxxdata);
            JObject djzldata = (JObject)JsonConvert.DeserializeObject(strdjzldata);
            JObject dyxxdata = (JObject)JsonConvert.DeserializeObject(strdyxxdata);
            JObject ygxxdata = (JObject)JsonConvert.DeserializeObject(strygxxdata);
            //String bottomTime = "2020年08年03月 10:40:10";
            // title="房屋权属登记信息查询结果证明";
            //FileOutputStream file = new FileOutputStream(Environment.CurrentDirectory+"\\Temp");
            BaseFont bfChinese = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font headfont = new Font(bfChinese, 12, Font.NORMAL);// 设置字体大小
            Font keyfont = new Font(bfChinese, 21, Font.BOLD);// 设置字体大小
            Font textfont = new Font(bfChinese, 9, Font.NORMAL);// 设置字体大小
            Font textfont2 = new Font(bfChinese, 10, Font.NORMAL);// 设置字体大小
            Font textfont3 = new Font(bfChinese, 13, Font.NORMAL);

            Stream file = new FileStream(Environment.CurrentDirectory + "\\Temp\\2.pdf", FileMode.Create);
            // 1.新建document对象
            // 第一个参数是页面大小。接下来的参数分别是左、右、上和下页边距。
            Document document = new Document(PageSize.A4, 10, 10, 0, 10);
            // 2.建立一个书写器(Writer)与document对象关联，通过书写器(Writer)可以将文档写入到磁盘中。
            // 创建 PdfWriter 对象 第一个参数是对文档对象的引用，第二个参数是文件的实际名称，在该名称中还会给出其输出路径。
            PdfWriter writer = PdfWriter.GetInstance(document, file);

            // 3.打开文档
            document.Open();
            try
            {

                float[] blanktablew = { 0.1f };
                PdfPTable blanktable = new PdfPTable(4)
                {
                    WidthPercentage = 100f
                };

                //float[] blanktablew = { 0.1f };
                //PdfPTable blanktable = new PdfPTable(blanktablew);
                //blanktable.SetWidthPercentage=100; // 宽度100%填充
                PdfPCell blankcell = new PdfPCell(new PdfPCell(new Paragraph(" ", textfont)));
                blankcell.DisableBorderSide(15);
                blankcell.MinimumHeight = 7f;
                blanktable.AddCell(blankcell);

                // 4.向文档中添加内容
                // 通过 com.lowagie.text.Paragraph 来添加文本。可以用文本及其默认的字体、颜色、大小等等设置来创建一个默认段落
                Paragraph pt = new Paragraph("他项权利证明", keyfont);// 设置字体样式
                pt.Alignment = 1;// 设置文字居中 0靠左 1，居中 2，靠右
                document.Add(pt);
                document.Add(new Paragraph("\n"));
                Paragraph pt1 = new Paragraph("出具时间：" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), textfont);

                pt1.Alignment = 0;// 设置文字居中 0靠左 1，居中 2，靠右
                                  //		pt1.setIndentationLeft(340);
                float[] blanktablew1 = { 0.1f };
                PdfPTable blanktable1 = new PdfPTable(blanktablew1);
                blanktable1.WidthPercentage = 100; // 宽度100%填充
                PdfPCell blankcell1 = new PdfPCell(new PdfPCell(pt1));
                blankcell1.DisableBorderSide(15);
                blankcell1.MinimumHeight = 20f;
                blanktable1.AddCell(blankcell1);
                document.Add(blanktable1);
                //document.add(new Paragraph("\n"));
                ////////////////////////////////////



                //=======第一个表格开始================================================
                float[] txqlzmbase = { 0.16f, 0.34f, 0.16f, 0.34f };
                PdfPTable basetable = new PdfPTable(txqlzmbase);
                basetable.WidthPercentage = 100; // 宽度100%填充
                String dyr = "";//抵押人
                String basedyqr = "";//抵押权人
                JObject qlrxx = (JObject)dyxxdata.GetValue("QLRXX");//权利人
                //JArray dyrArray = (JObject)qlrxx.GetValue("DYR");//抵押人
                //JObject dyqrArray = (JObject)qlrxx.GetValue("DYQR");//抵押权人

                JArray dyrArray = (JArray)qlrxx["DYR"];
                JArray dyqrArray = (JArray)qlrxx["DYQR"];
                foreach (JObject dyr1 in dyrArray)
                {
                    dyr = (string)dyr1.GetValue("DYR");
                }
                foreach (JObject basedyqr1 in dyqrArray)
                {
                    dyr = (string)basedyqr1.GetValue("DYQR");
                }

                //for (int i = 0; i < dyrArray.Count; i++)
                //{
                //    JSONObject dyrArray = dyrArray.getJSONObject(i);
                //    dyr = dyrObject.GetValue("DYR").ToString() + ",";
                //}
                //if (dyr != "")
                //{
                //    dyr = dyr.Substring(0, dyr.Length - 1);
                //}

                //for (int i = 0; i < dyqrArray.Count; i++)
                //{
                //    JSONObject dyqrObject = dyqrArray.getJSONObject(i);
                //    basedyqr = dyqrObject.get("DYQR").toString() + ",";
                //}
                //if (basedyqr != "")
                //{
                //    basedyqr = basedyqr.Substring(0, basedyqr.Length - 1);
                //}



                PdfPCell basecell1 = new PdfPCell(new PdfPCell(new Paragraph("抵押人", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };


                PdfPCell basecell2 = new PdfPCell(new PdfPCell(new Paragraph(dyr, textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 3
                };


                PdfPCell basecell3 = new PdfPCell(new PdfPCell(new Paragraph("抵押权人", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };


                PdfPCell basecell4 = new PdfPCell(new PdfPCell(new Paragraph(basedyqr, textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 3
                };


                PdfPCell basecell5 = new PdfPCell(new PdfPCell(new Paragraph("不动产证号", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell basecell6 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("BDCZH").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 3
                };

                PdfPCell basecell7 = new PdfPCell(new PdfPCell(new Paragraph("他项权证号", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell basecell8 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("BDCZMH").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 3
                };

                PdfPCell basecell9 = new PdfPCell(new PdfPCell(new Paragraph("抵押方式", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };


                PdfPCell basecell10 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("DYFS").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };


                PdfPCell basecell11 = new PdfPCell(new PdfPCell(new Paragraph("担保范围", textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell basecell12 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("DBFW").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell basecell13 = new PdfPCell(new PdfPCell(new Paragraph("债权数额(万元)", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell basecell14 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("ZGZQQDSSHSE").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };


                PdfPCell basecell15 = new PdfPCell(new PdfPCell(new Paragraph("不动产价值(万元)", textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell basecell16 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };


                PdfPCell basecell17 = new PdfPCell(new PdfPCell(new Paragraph("起始时间", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell basecell18 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("QLQSSJ").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };


                PdfPCell basecell19 = new PdfPCell(new PdfPCell(new Paragraph("结束时间", textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell basecell20 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("QLJSSJ").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell basecell21 = new PdfPCell(new PdfPCell(new Paragraph("房屋抵押面积", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                PdfPCell basecell22 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("FCDYMJ").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };
                PdfPCell basecell23 = new PdfPCell(new PdfPCell(new Paragraph("土地抵押面积", textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };

                PdfPCell basecell24 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("TDDYMJ").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                };


                PdfPCell basecell25 = new PdfPCell(new PdfPCell(new Paragraph("坐落", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                PdfPCell basecell26 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("ZL").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 3
                };

                PdfPCell basecell27 = new PdfPCell(new PdfPCell(new Paragraph("最高债权确定事实", textfont2)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell basecell28 = new PdfPCell(new PdfPCell(new Paragraph("", textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 3
                };


                PdfPCell basecell29 = new PdfPCell(new PdfPCell(new Paragraph("附记", textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell basecell30 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("FJ").ToString(), textfont2)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    PaddingLeft = 5f,
                    PaddingRight = 5f,
                    PaddingTop = 5f,
                    PaddingBottom = 5f,
                    Colspan = 3,
                };


                basetable.AddCell(basecell1);
                basetable.AddCell(basecell2);
                basetable.AddCell(basecell3);
                basetable.AddCell(basecell4);
                basetable.AddCell(basecell5);
                basetable.AddCell(basecell6);
                basetable.AddCell(basecell7);
                basetable.AddCell(basecell8);
                basetable.AddCell(basecell9);
                basetable.AddCell(basecell10);
                basetable.AddCell(basecell11);
                basetable.AddCell(basecell12);
                basetable.AddCell(basecell13);
                basetable.AddCell(basecell14);
                basetable.AddCell(basecell15);
                basetable.AddCell(basecell16);
                basetable.AddCell(basecell17);
                basetable.AddCell(basecell18);
                basetable.AddCell(basecell19);
                basetable.AddCell(basecell20);
                basetable.AddCell(basecell21);
                basetable.AddCell(basecell22);
                basetable.AddCell(basecell23);
                basetable.AddCell(basecell24);
                basetable.AddCell(basecell25);
                basetable.AddCell(basecell26);
                basetable.AddCell(basecell27);
                basetable.AddCell(basecell28);
                basetable.AddCell(basecell29);
                basetable.AddCell(basecell30);
                document.Add(basetable);
                //=======第一个表格结束================================================
                document.Add(blanktable);


                document.Add(blankcell);

                //==第二个表格开始==========
                //document.add(new Paragraph("\n"));
                float[] ygwidhts2 = { 0.1f, 0.18f, 0.18f, 0.18f, 0.18f, 0.18f };
                PdfPTable bTable2 = new PdfPTable(ygwidhts2);
                bTable2.WidthPercentage = 100; // 宽度100%填充
                PdfPCell twocell = new PdfPCell(new PdfPCell(new Paragraph("查封信息", textfont)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 6

                };


                PdfPCell twocell1 = new PdfPCell(new PdfPCell(new Paragraph("序号", textfont)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };


                PdfPCell twocell2 = new PdfPCell(new PdfPCell(new Paragraph("查封法院", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell twocell3 = new PdfPCell(new PdfPCell(new Paragraph("查封文号", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell twocell4 = new PdfPCell(new PdfPCell(new Paragraph("查封日期", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell twocell5 = new PdfPCell(new PdfPCell(new Paragraph("有效日期", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                PdfPCell twocell6 = new PdfPCell(new PdfPCell(new Paragraph("限制内容", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };


                bTable2.AddCell(twocell);
                bTable2.AddCell(twocell1);
                bTable2.AddCell(twocell2);
                bTable2.AddCell(twocell3);
                bTable2.AddCell(twocell4);
                bTable2.AddCell(twocell5);
                bTable2.AddCell(twocell6);

                int xuhao = 1;//序号
                int i = 0;
                String cfjg = "";//查封机构
                String cfwh = "";//查封文号
                String cfrq = "";//查封日期
                String yxq = "";//有效期
                String xznr = "";//限制内容
                JArray cfxx = (JArray)ygxxdata["CFXX"];

                //int cfxxSize = cfxx.Count;
                foreach (JObject cfxxObj in cfxx)
                {

                    xuhao = xuhao + i;
                    string xuhaostr = xuhao.ToString();

                    cfjg = cfxxObj.GetValue("CFJG") == null ? "" : cfxxObj.GetValue("CFJG").ToString();
                    cfwh = cfxxObj.GetValue("CFWH") == null ? "" : cfxxObj.GetValue("CFWH").ToString();
                    cfrq = cfxxObj.GetValue("CFKSSJ") == null ? "" : cfxxObj.GetValue("CFKSSJ").ToString();
                    yxq = cfxxObj.GetValue("CFJSSJ") == null ? "" : cfxxObj.GetValue("CFJSSJ").ToString();
                    xznr = cfxxObj.GetValue("CFLX") == null ? "" : cfxxObj.GetValue("CFLX").ToString();
                    i++;
                    PdfPCell twocell7 = new PdfPCell(new PdfPCell(new Paragraph(xuhaostr, textfont)))
                    {
                        MinimumHeight = 24f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    PdfPCell twocell8 = new PdfPCell(new PdfPCell(new Paragraph(cfjg, textfont)))
                    {

                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    PdfPCell twocell9 = new PdfPCell(new PdfPCell(new Paragraph(cfwh, textfont)))
                    {

                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    PdfPCell twocell10 = new PdfPCell(new PdfPCell(new Paragraph(cfrq.Substring(0, 10), textfont)))
                    {

                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    PdfPCell twocell11 = new PdfPCell(new PdfPCell(new Paragraph(yxq.Substring(0, 10), textfont)))
                    {

                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    PdfPCell twocell12 = new PdfPCell(new PdfPCell(new Paragraph(xznr, textfont)))
                    {

                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    bTable2.AddCell(twocell7);
                    bTable2.AddCell(twocell8);
                    bTable2.AddCell(twocell9);
                    bTable2.AddCell(twocell10);
                    bTable2.AddCell(twocell11);
                    bTable2.AddCell(twocell12);
                }
                //for (int i = 0; i < cfxxSize; i++)
                //{

                //    xuhao = xuhao + i;
                //    string xuhaostr = xuhao.ToString();

                //    JObject cfxxObj = (JObject)cfxx.GetValue(i);

                //    cfjg = cfxxObj.get("CFJG") == null ? "" : cfxxObj.get("CFJG").toString();
                //    cfwh = cfxxObj.get("CFWH") == null ? "" : cfxxObj.get("CFWH").toString();
                //    cfrq = cfxxObj.get("CFKSSJ") == null ? "" : cfxxObj.get("CFKSSJ").toString();
                //    yxq = cfxxObj.get("CFJSSJ") == null ? "" : cfxxObj.get("CFJSSJ").toString();
                //    xznr = cfxxObj.get("CFLX") == null ? "" : cfxxObj.get("CFLX").toString();



                //}

                document.Add(bTable2);
                //==第二个表格结束==========


                document.Add(blanktable);

                //==第三个表格开始==========
                // document.add(new Paragraph("\n"));
                float[] dywidhts2 = { 0.1f, 0.3f, 0.08f, 0.11f, 0.11f, 0.08f, 0.11f, 0.11f };
                PdfPTable cTable2 = new PdfPTable(dywidhts2);
                cTable2.WidthPercentage = 100; // 宽度100%填充
                PdfPCell threecell = new PdfPCell(new PdfPCell(new Paragraph("抵押信息", textfont)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 8
                };


                PdfPCell threecell1 = new PdfPCell(new PdfPCell(new Paragraph("序号", textfont)))
                {
                    MinimumHeight = 24f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };

                PdfPCell threecell2 = new PdfPCell(new PdfPCell(new Paragraph("抵押权人", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };

                PdfPCell threecell3 = new PdfPCell(new PdfPCell(new Paragraph("抵押金额", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };
                PdfPCell threecell4 = new PdfPCell(new PdfPCell(new Paragraph("房产抵押面积", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };
                PdfPCell threecell5 = new PdfPCell(new PdfPCell(new Paragraph("土地抵押面积", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };
                PdfPCell threecell6 = new PdfPCell(new PdfPCell(new Paragraph("登记日期", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };
                PdfPCell threecell7 = new PdfPCell(new PdfPCell(new Paragraph("抵押起始日期", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };
                PdfPCell threecell8 = new PdfPCell(new PdfPCell(new Paragraph("抵押结束日期", textfont)))
                {

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,

                };

                cTable2.AddCell(threecell);
                cTable2.AddCell(threecell1);
                cTable2.AddCell(threecell2);
                cTable2.AddCell(threecell3);
                cTable2.AddCell(threecell4);
                cTable2.AddCell(threecell5);
                cTable2.AddCell(threecell6);
                cTable2.AddCell(threecell7);
                cTable2.AddCell(threecell8);

                int dyxuhao = 1;//序号
                int j = 0;
                String dyqr = "";//抵押權人
                String dyje = "";//抵押金額
                String fcdymj = "";//房產抵押面積
                String tddymj = "";//土地抵押面積
                String djrq = "";//登記日期
                String dyqsrq = "";//抵押起始日期
                String dyjsrq = "";//抵押結束日期

                //JSONArray dyqlrxx = (JSONArray)djzldata.get("DYXX");
                JArray dyqlrxx = (JArray)ygxxdata["DYXX"];
                //JSONArray dyqrlist = dyqlrxx.getJSONArray("DYQR");
                //int dyxxSize = dyqlrxx.size();
                foreach (JObject dyxxObj in dyqlrxx)
                {

                    dyxuhao = dyxuhao + j;
                    String xuhaostr = dyxuhao.ToString();
                    String BDCDJZMH = (string)dyxxObj.GetValue("BDCDJZMH");
                    String dyxxurl = "http://192.168.11.2:9999/out/isaip/dycx_yx";//抵押信息接口
                    String paramsJson = "{\"BDCZMH\":\"" + BDCDJZMH + "\"}";
                    String tmplist = FileUtil.ResolveFile(Environment.CurrentDirectory + "\\抵押.json");
                    //			JSONObject tmpJsonObj = (JSONObject) JSONObject.fromObject(tmplist);
                    JObject tmpJsonObj = (JObject)JsonConvert.DeserializeObject(tmplist);
                    //JSONObject tmpJsonObj = (JSONObject)JSONObject.parseObject(tmplist);
                    JObject tmpDataObj = (JObject)tmpJsonObj.GetValue("data");

                    dyqr = dyxxObj.GetValue("DYQR") == null ? "" : dyxxObj.GetValue("DYQR").ToString();
                    dyje = dyxxObj.GetValue("DYJE") == null ? "" : dyxxObj.GetValue("DYJE").ToString();
                    fcdymj = tmpDataObj.GetValue("FCDYMJ") == null ? "" : tmpDataObj.GetValue("FCDYMJ").ToString();
                    tddymj = tmpDataObj.GetValue("TDDYMJ") == null ? "" : tmpDataObj.GetValue("TDDYMJ").ToString();
                    //fcdymj = "";
                    //tddymj = "";
                    djrq = dyxxObj.GetValue("DJSJ") == null ? "" : dyxxObj.GetValue("DJSJ").ToString();
                    dyqsrq = dyxxObj.GetValue("DYKSSJ") == null ? "" : dyxxObj.GetValue("DYKSSJ").ToString();
                    dyjsrq = dyxxObj.GetValue("DYJSSJ") == null ? "" : dyxxObj.GetValue("DYJSSJ").ToString();
                    j++;
                    PdfPCell threecell19 = new PdfPCell(new PdfPCell(new Paragraph(xuhaostr, textfont)))
                    {
                        MinimumHeight = 24f,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    PdfPCell threecell20 = new PdfPCell(new PdfPCell(new Paragraph(dyqr, textfont)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    PdfPCell threecell21 = new PdfPCell(new PdfPCell(new Paragraph(dyje, textfont)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    PdfPCell threecell22 = new PdfPCell(new PdfPCell(new Paragraph(fcdymj, textfont)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    PdfPCell threecell23 = new PdfPCell(new PdfPCell(new Paragraph(tddymj, textfont)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    PdfPCell threecell24 = new PdfPCell(new PdfPCell(new Paragraph(djrq, textfont)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    PdfPCell threecell25 = new PdfPCell(new PdfPCell(new Paragraph(dyqsrq, textfont)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };

                    PdfPCell threecell26 = new PdfPCell(new PdfPCell(new Paragraph(dyjsrq, textfont)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };


                    cTable2.AddCell(threecell19);
                    cTable2.AddCell(threecell20);
                    cTable2.AddCell(threecell21);
                    cTable2.AddCell(threecell22);
                    cTable2.AddCell(threecell23);
                    cTable2.AddCell(threecell24);
                    cTable2.AddCell(threecell25);
                    cTable2.AddCell(threecell26);
                }
                //    for (int i = 0; i < dyxxSize; i++)
                //{

                //    //循环查询


                //    dyxuhao = dyxuhao + i;
                //    String xuhaostr = Integer.toString(dyxuhao);

                //    JSONObject dyxxObj = dyqlrxx.getJSONObject(i);

                //    String BDCDJZMH = dyxxObj.getString("BDCDJZMH");
                //    String dyxxurl = "http://192.168.11.2:9999/out/isaip/dycx_yx";//抵押信息接口
                //    String paramsJson = "{\"BDCZMH\":\"" + BDCDJZMH + "\"}";

                //    //			String dyInfoList = RequestUtil.doPost(dyxxurl, paramsJson);
                //    String tmplist = FileUtil.readToString("E:\\svn\\bdc\\json\\抵押.json");
                //    //			JSONObject tmpJsonObj = (JSONObject) JSONObject.fromObject(tmplist);
                //    JSONObject tmpJsonObj = (JSONObject)JSONObject.parseObject(tmplist);
                //    JSONObject tmpDataObj = (JSONObject)tmpJsonObj.get("data");
                //    //JSONObject dyxxdata = (JSONObject) dyxxjson.get("data");

                //    dyqr = dyxxObj.get("DYQR") == null ? "" : dyxxObj.get("DYQR").toString();
                //    dyje = dyxxObj.get("DYJE") == null ? "" : dyxxObj.get("DYJE").toString();
                //    fcdymj = tmpDataObj.get("FCDYMJ") == null ? "" : tmpDataObj.get("FCDYMJ").toString();
                //    tddymj = tmpDataObj.get("TDDYMJ") == null ? "" : tmpDataObj.get("TDDYMJ").toString();
                //    //fcdymj = "";
                //    //tddymj = "";
                //    djrq = dyxxObj.get("DJSJ") == null ? "" : dyxxObj.get("DJSJ").toString();
                //    dyqsrq = dyxxObj.get("DYKSSJ") == null ? "" : dyxxObj.get("DYKSSJ").toString();
                //    dyjsrq = dyxxObj.get("DYJSSJ") == null ? "" : dyxxObj.get("DYJSSJ").toString();

                //    PdfPCell threecell19 = new PdfPCell(new PdfPCell(new Paragraph(xuhaostr, textfont)));
                //    threecell19.MinimumHeight=24f;
                //    threecell19.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell19.VerticalAlignment=Element.ALIGN_MIDDLE;
                //    PdfPCell threecell20 = new PdfPCell(new PdfPCell(new Paragraph(dyqr, textfont)));
                //    threecell20.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell20.VerticalAlignment=Element.ALIGN_MIDDLE;
                //    PdfPCell threecell21 = new PdfPCell(new PdfPCell(new Paragraph(dyje, textfont)));
                //    threecell21.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell21.VerticalAlignment=Element.ALIGN_MIDDLE;
                //    PdfPCell threecell22 = new PdfPCell(new PdfPCell(new Paragraph(fcdymj, textfont)));
                //    threecell22.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell22.VerticalAlignment=Element.ALIGN_MIDDLE;
                //    PdfPCell threecell23 = new PdfPCell(new PdfPCell(new Paragraph(tddymj, textfont)));
                //    threecell23.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell23.VerticalAlignment=Element.ALIGN_MIDDLE;
                //    PdfPCell threecell24 = new PdfPCell(new PdfPCell(new Paragraph(djrq, textfont)));
                //    threecell24.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell24.VerticalAlignment=Element.ALIGN_MIDDLE;
                //    PdfPCell threecell25 = new PdfPCell(new PdfPCell(new Paragraph(dyqsrq, textfont)));
                //    threecell25.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell25.VerticalAlignment=Element.ALIGN_MIDDLE;
                //    PdfPCell threecell26 = new PdfPCell(new PdfPCell(new Paragraph(dyjsrq, textfont)));
                //    threecell26.HorizontalAlignment=Element.ALIGN_CENTER;
                //    threecell26.VerticalAlignment=Element.ALIGN_MIDDLE;

                //    cTable2.AddCell(threecell19);
                //    cTable2.AddCell(threecell20);
                //    cTable2.AddCell(threecell21);
                //    cTable2.AddCell(threecell22);
                //    cTable2.AddCell(threecell23);
                //    cTable2.AddCell(threecell24);
                //    cTable2.AddCell(threecell25);
                //    cTable2.AddCell(threecell26);

                //}

                document.Add(cTable2);
                //==第三个表格结束===================
                document.Add(blanktable);
                //document.add(new Paragraph("\n"));
                float[] footwidhts2 = { 0.16f, 0.84f };
                PdfPTable footTable2 = new PdfPTable(footwidhts2);
                footTable2.WidthPercentage = 100; // 宽度100%填充
                PdfPCell footcell1 = new PdfPCell(new PdfPCell(new Paragraph("特别提示", textfont3)));
                footcell1.HorizontalAlignment = Element.ALIGN_CENTER;
                footcell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                PdfPCell footcell2 = new PdfPCell(new PdfPCell(new Paragraph("本证明之法律效力仅限于出具时点，超出该时点后，本证明不再具备相应的法律保障效力，相关法定权属信息一律以登记机关不动产电子登记簿即时记录为准。", textfont3)));
                footcell2.HorizontalAlignment = Element.ALIGN_CENTER;
                footcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                //		document.add(new Paragraph("\n"));
                footTable2.AddCell(footcell1);
                footTable2.AddCell(footcell2);

                document.Add(footTable2);

                Paragraph pt2 = new Paragraph("出具人：系统管理员", textfont);
                pt2.Alignment = 0;// 设置文字居中 0靠左 1，居中 2，靠右
                                  //		pt2.setIndentationLeft(340);
                                  //document.add(pt2);

                Paragraph pt3 = new Paragraph("证明出具单位：宜兴市自然资源和规划局", textfont);
                pt3.Alignment = 2;// 设置文字居中 0靠左 1，居中 2，靠右

                Paragraph pt4 = new Paragraph("(盖章)", textfont);
                pt4.Alignment = 1;// 设置文字居中 0靠左 1，居中 2，靠右
                                  //document.add(pt3);

                float[] titlecell = { 0.3f, 0.5f, 0.2f };
                //titlecell.disableBorderSide(15);
                PdfPTable endtable = new PdfPTable(titlecell);
                // titletable.getDefaultCell().MinimumHeight=20);
                endtable.WidthPercentage = 100;
                PdfPCell cell1 = new PdfPCell(pt2);
                cell1.MinimumHeight = 34f;
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                //cell1.setPaddingBottom(10f);
                cell1.DisableBorderSide(15);
                PdfPCell cell2 = new PdfPCell(pt3);
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell2.DisableBorderSide(15);
                PdfPCell cell3 = new PdfPCell(pt4);
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell3.DisableBorderSide(15);

                endtable.AddCell(cell1);
                endtable.AddCell(cell2);
                endtable.AddCell(cell3);

                document.Add(endtable);
                //==最后一行结束===================================================

            }
            catch (Exception e)
            {

            }
            finally
            {
                // 5.关闭文档
                document.Close();
                writer.Close();
                file.Close();
            }

        }

        public static void djzlPdf1()
            {
			String strdjzldata = "{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-19 11:11:11\",\"FCZFZSJ\":\"2020-08-19 11:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-19 11:11:11\",\"QLJSSJ\":\"2020-08-19 11:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}";
			String strdyxxdata = "{\"DYFS\":\"xxx抵押\",\"YWH\":\"TD011bb9b0c5611bb9b0c508708a8c8092\",\"ZWR\":\"王明珠\",\"BDCZMH\":\"泰州他项(2008)第3060号\",\"ZSBH\":\"9447\",\"DBFW\":\"详见合同\",\"ZGZQQDSSHSE\":1213.113,\"FCDYMJ\":111.1,\"TDDYMJ\":32.3,\"BIZ\":\"人民币\",\"DJSJ\":\"1899-01-01 00:00:00\",\"ZQLXQX\":\"20070604至20170604\",\"ZXSJ\":\"2020-03-31 00:03:57\",\"ZT\":\"现势\",\"FJ\":\"1、抵押\\r\\n2、许可抵押面积(平方米)：32.30平方米\\r\\n3、许可抵押金额(大写)：土地资产不计入抵押担保值\",\"QT\":\"dsfasdgdfagsasd \",\"BDCZH\":\"泰州国用(2008)第5620号\",\"BZ\":\"bz333\",\"QLRXX\":{\"DYR\":[{\"DYR\":\"王明珠\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"320623198201293515\"}],\"DYQR\":[{\"DYQR\":\"中国建设银行股份有限公司泰州分行\",\"DYQRZJZL\":\"身份证\",\"DYQRZJHM\":\"320623198201293516\"}]}}";
			String strygxxdata = "{\"BDCDYH\":\"321202050002GB00019F00250107\",\"FWDM\":\"TS-190108093940-360061C2\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"包含土地、包含建筑物\",\"YT\":\"成套住宅\",\"ZL\":\"水映蓝庭25幢107室\",\"FWXZ\":\"市场化商品房\",\"FWJG\":\"aaa\",\"SJC\":\"1\",\"ZCS\":\"1\",\"FWLX\":\"bbb\",\"MYC\":\"1\",\"ZRZH\":\"0025\",\"FJH\":\"107\",\"JZMJ\":1,\"TNMJ\":2,\"FTMJ\":33.11,\"DJSJ\":\"2019-01-09 10:32:36\",\"FCZFZSJ\":\"2019-02-13 00:00:00\",\"TDQLLX\":\"国有建设用地使用权\",\"QLQSSJ\":\"2017-04-01 10:02:03\",\"QLJSSJ\":\"2020-04-01 00:00:00\",\"TDQLXZ\":\"出让\",\"TDYT\":\"城镇住宅用地\",\"FJ\":\"fdsvdsfsdaf\",\"TDSYQMJ\":123123,\"TDFTMJ\":3.0006,\"TDDYMJ\":1.001,\"YWR\":\"预告义务人\",\"ZDMJ\":\"宗地面积\",\"ZDDM\":\"宗地代码\",\"QLQTZK\":\"权利其他状况\",\"TDZH\":\"土地证号\",\"DYXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"BDCDJZMH\":\"20190008679\",\"DYQR\":\"无味\",\"DYJE\":123,\"DYKSSJ\":\"2020-04-01 00:00:00\",\"DYJSSJ\":\"2021-04-28 09:04:56\",\"DJSJ\":\"2019-11-11 10:37:16\",\"QSZT\":\"现势\",\"FJ\":\"543523452345\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\",\"DYFS\":\"一般抵押\",\"DBFW\":\"详见抵押合同\",\"DYR\":\"吴海洋,魏爱霞\"}],\"CFXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"CFJG\":\"泰州法院\",\"CFLX\":\"预查封\",\"CFWH\":\"2020-0241\",\"CFKSSJ\":\"2020-03-19 00:00:00\",\"CFJSSJ\":\"2021-03-18 00:00:00\",\"DJSJ\":\"2020-03-19 17:09:44\",\"QSZT\":\"现势\",\"FJ\":\"123123123\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"},{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"CFJG\":\"泰州法院1\",\"CFLX\":\"预查封\",\"CFWH\":\"2020-0241\",\"CFKSSJ\":\"2020-03-19 00:00:00\",\"CFJSSJ\":\"2021-03-18 00:00:00\",\"DJSJ\":\"2020-03-19 17:09:44\",\"QSZT\":\"现势\",\"FJ\":\"123123123\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}],\"QLR\":[{\"QLRMC\":\"吴海洋\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"32128419821129103X\",\"GYFS\":\"共同共有\",\"QLBL\":\"30%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"},{\"QLRMC\":\"魏爱霞1\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"321284198202010021\",\"GYFS\":\"共同共有\",\"QLBL\":\"70%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}]}";

			//	    JSONObject djzldata = JSONObject.fromObject(strdjzldata);
			//	    JSONObject dyxxdata = JSONObject.fromObject(strdyxxdata);
			//	    JSONObject ygxxdata = JSONObject.fromObject(strygxxdata);
			//JSONObject djzldata = JSONObject.parseObject(strdjzldata);
			//JSONObject dyxxdata = JSONObject.parseObject(strdyxxdata);
			//JSONObject ygxxdata = JSONObject.parseObject(strygxxdata);

            JObject djzldata = (JObject)JsonConvert.DeserializeObject(strdjzldata);
            JObject dyxxdata = (JObject)JsonConvert.DeserializeObject(strdyxxdata);
            JObject ygxxdata = (JObject)JsonConvert.DeserializeObject(strygxxdata);
            //编号
            String fcode = DateTime.Now.ToString("yyyyMMdd");
            //打印时间	   
            BaseFont bfChinese = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font headfont = new Font(bfChinese, 12, Font.NORMAL);// 设置字体大小
            Font keyfont = new Font(bfChinese, 21, Font.BOLD);// 设置字体大小
            Font textfont = new Font(bfChinese, 9, Font.NORMAL);// 设置字体大小
            Font textfont2 = new Font(bfChinese, 10, Font.NORMAL);// 设置字体大小
            Font textfont3 = new Font(bfChinese, 13, Font.NORMAL);
            Font textfont4 = new Font(bfChinese, 15, Font.NORMAL);

            Document document = new Document(PageSize.A4, 10, 10, 0, 10);
            Stream stream = new FileStream("2.pdf", FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);

            document.Open();


            try
            {

				// 4.向文档中添加内容
				// 通过 com.lowagie.text.Paragraph 来添加文本。可以用文本及其默认的字体、颜色、大小等等设置来创建一个默认段落
				Paragraph pt = new Paragraph("宜兴市不动产登记簿证明\n（权利人）", textfont4);// 设置字体样式
				pt.Alignment=1;// 设置文字居中 0靠左 1，居中 2，靠右

				//pt.set
				document.Add(pt);
				document.Add(new Paragraph("\n"));

				String ywbh = fcode;

				Paragraph pt1 = new Paragraph("编号：" + ywbh, textfont);
				pt1.Alignment=0;// 设置文字居中 0靠左 1，居中 2，靠右
									//			pt1.setIndentationLeft(340);
									//document.add(pt1);

				Paragraph pt2 = new Paragraph("查询人：" + "", textfont);
				pt1.Alignment=0;// 设置文字居中 0靠左 1，居中 2，靠右
									//			pt1.setIndentationLeft(340);
									//document.add(pt2);

				Paragraph pt3 = new Paragraph("打印时间：" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), textfont);
				pt1.Alignment=2;// 设置文字居中 0靠左 1，居中 2，靠右
									//			pt1.setIndentationLeft(340);
									//document.add(pt3);

				//document.add(new Paragraph("\n"));

				float[] titlecell = { 0.3f, 0.3f, 0.3f };
				//titlecell.disableBorderSide(15);
				PdfPTable titletable = new PdfPTable(titlecell);
				// titletable.getDefaultCell().MinimumHeight=20);
				titletable.WidthPercentage=100;
				PdfPCell cell1 = new PdfPCell(pt1);
				cell1.MinimumHeight=24f;
				cell1.HorizontalAlignment=Element.ALIGN_LEFT;
				cell1.VerticalAlignment=Element.ALIGN_MIDDLE;
				//cell1.setPaddingBottom(3f);
				cell1.DisableBorderSide(15);
				PdfPCell cell2 = new PdfPCell(pt2);
				cell2.HorizontalAlignment=Element.ALIGN_CENTER;
				cell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				cell2.DisableBorderSide(15);
				PdfPCell cell3 = new PdfPCell(pt3);
				cell3.HorizontalAlignment=Element.ALIGN_RIGHT;
				cell3.VerticalAlignment=Element.ALIGN_MIDDLE;
				cell3.DisableBorderSide(15);

				titletable.AddCell(cell1);
				titletable.AddCell(cell2);
				titletable.AddCell(cell3);

				document.Add(titletable);
				////////////////////////////////////

				//==============================================
				//JSONArray djzlqlr = (JSONArray)djzldata.get("QLR");//权利人
				//JSONObject djzlqlrJson = new JSONObject();
				string djzlqlrmc = "";//登记资料权利人名称
				string  djzlqlrzjh = "";//权利人证件号码
                JArray djzlqlr = (JArray)djzldata["QLR"];
                //JArray qlrmcArray = (JArray)djzlqlr["QLRMC"];
                //JArray qlrzjhArray = (JArray)djzlqlr["QLRZJH"];
                foreach (JObject qlr in djzlqlr)
                {
                    djzlqlrmc += (string)qlr.GetValue("QLRMC") + " ";
                    djzlqlrzjh += (string)qlr.GetValue("QLRZJH") + " ";
                }
                //            for (int i = 0; i < djzlqlr.size(); i++)
                //{
                //	djzlqlrJson = djzlqlr.getJSONObject(i);
                //	djzlqlrmc += djzlqlrJson.get("QLRMC").toString() + ",";
                //	djzlqlrzjh += djzlqlrJson.get("QLRZJH").toString() + ",";
                //}
                //if (djzlqlrmc != "")
                //{
                //	djzlqlrmc = djzlqlrmc.substring(0, djzlqlrmc.length() - 1);
                //	djzlqlrzjh = djzlqlrzjh.substring(0, djzlqlrzjh.length() - 1);
                //}

                float[] qlrwidhts = { 0.03f, 0.17f, 0.1f, 0.1f, 0.1f, 0.18f, 0.13f, 0.1f, 0.1f, 0.05f };
				PdfPTable qlrtable = new PdfPTable(qlrwidhts);
				qlrtable.WidthPercentage=100; // 宽度100%填充

				//====第一行开始=================
				PdfPCell ycell = new PdfPCell(new PdfPCell(new Paragraph("权利人", textfont)));
				ycell.MinimumHeight=24f;
				ycell.HorizontalAlignment=Element.ALIGN_CENTER;
				ycell.VerticalAlignment=Element.ALIGN_MIDDLE;
				ycell.Colspan=2;

				PdfPCell ycell2 = new PdfPCell(new PdfPCell(new Paragraph(djzlqlrmc, textfont)));
				ycell2.HorizontalAlignment=Element.ALIGN_CENTER;
				ycell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				ycell2.Colspan=3;

				PdfPCell ycell3 = new PdfPCell(new PdfPCell(new Paragraph("权利人证件号码", textfont)));
				ycell3.HorizontalAlignment=Element.ALIGN_CENTER;
				ycell3.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell ycell4 = new PdfPCell(new PdfPCell(new Paragraph(djzlqlrzjh, textfont)));
				ycell4.HorizontalAlignment=Element.ALIGN_CENTER;
				ycell4.VerticalAlignment=Element.ALIGN_MIDDLE;
				ycell4.Colspan=4;

				qlrtable.AddCell(ycell);
				qlrtable.AddCell(ycell2);
				qlrtable.AddCell(ycell3);
				qlrtable.AddCell(ycell4);
				//===第一行结束==================

				//===第二行开始==================
				PdfPCell ecell = new PdfPCell(new PdfPCell(new Paragraph("不动产权证号", textfont)));
				ecell.MinimumHeight=24f;
				ecell.HorizontalAlignment=Element.ALIGN_CENTER;
				ecell.VerticalAlignment=Element.ALIGN_MIDDLE;
				ecell.Colspan=2;

				PdfPCell ecell2 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("CQZH") == null ? "" : djzldata.GetValue("CQZH").ToString(),textfont)));
				ecell2.HorizontalAlignment=Element.ALIGN_CENTER;
				ecell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				ecell2.Colspan=3;

				PdfPCell ecell3 = new PdfPCell(new PdfPCell(new Paragraph("不动产单元号", textfont)));
				ecell3.HorizontalAlignment=Element.ALIGN_CENTER;
				ecell3.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell ecell4 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("BDCDYH") == null ? "" : djzldata.GetValue("BDCDYH").ToString(),textfont)));
				ecell4.HorizontalAlignment=Element.ALIGN_CENTER;
				ecell4.VerticalAlignment=Element.ALIGN_MIDDLE;
				ecell4.Colspan=4;

				qlrtable.AddCell(ecell);
				qlrtable.AddCell(ecell2);
				qlrtable.AddCell(ecell3);
				qlrtable.AddCell(ecell4);
				//===第二行结束==================

				//===第三行开始==================
				PdfPCell scell = new PdfPCell(new PdfPCell(new Paragraph("不动产坐落", textfont)));
				scell.MinimumHeight=24f;
				scell.HorizontalAlignment=Element.ALIGN_CENTER;
				scell.VerticalAlignment=Element.ALIGN_MIDDLE;
				scell.Colspan=2;

				PdfPCell scell2 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("ZL") == null ? "" : djzldata.GetValue("ZL").ToString(), textfont)));
				scell2.HorizontalAlignment=Element.ALIGN_CENTER;
				scell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				scell2.Colspan=8;

				qlrtable.AddCell(scell);
				qlrtable.AddCell(scell2);
				//===第三行结束==================

				//===土地信息开始==================
				PdfPCell dixxcell = new PdfPCell(new PdfPCell(new Paragraph("土地信息", textfont)));
				dixxcell.HorizontalAlignment=Element.ALIGN_CENTER;
				dixxcell.VerticalAlignment=Element.ALIGN_MIDDLE;
				dixxcell.Rowspan=3;

				PdfPCell tdxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("宗地代码", textfont)));
				tdxxcell2.MinimumHeight=24f;
				tdxxcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell2.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell tdxxcell3 = new PdfPCell(new PdfPCell(new Paragraph(ygxxdata.GetValue("ZDDM") == null ? "" : ygxxdata.GetValue("ZDDM").ToString(), textfont)));
				tdxxcell3.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell3.VerticalAlignment=Element.ALIGN_MIDDLE;
				tdxxcell3.Colspan=3;

				PdfPCell tdxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("土地用途", textfont)));
				tdxxcell4.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell4.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell tdxxcell5 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("TDYT") == null ? "" : djzldata.GetValue("TDYT").ToString(), textfont)));
				tdxxcell5.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell5.VerticalAlignment=Element.ALIGN_MIDDLE;
				tdxxcell5.Colspan=4;

				PdfPCell tdxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("宗地面积", textfont)));
				tdxxcell6.MinimumHeight=24f;
				tdxxcell6.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell6.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell tdxxcell7 = new PdfPCell(new PdfPCell(new Paragraph(ygxxdata.GetValue("ZDMJ") == null ? "" : ygxxdata.GetValue("ZDMJ").ToString(), textfont)));
				tdxxcell7.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell7.VerticalAlignment=Element.ALIGN_MIDDLE;
				tdxxcell7.Colspan=3;

				PdfPCell tdxxcell8 = new PdfPCell(new PdfPCell(new Paragraph("土地使用权面积", textfont)));
				tdxxcell8.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell8.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell tdxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("TDSYQMJ") == null ? "" : djzldata.GetValue("TDSYQMJ").ToString(), textfont)));
				tdxxcell9.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell9.VerticalAlignment=Element.ALIGN_MIDDLE;
				tdxxcell9.Colspan=4;

				PdfPCell tdxxcell10 = new PdfPCell(new PdfPCell(new Paragraph("终止日期", textfont)));
				tdxxcell10.MinimumHeight=24f;
				tdxxcell10.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell10.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell tdxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("QLJSSJ") == null ? "" : djzldata.GetValue("QLJSSJ").ToString(), textfont)));
				tdxxcell11.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell11.VerticalAlignment=Element.ALIGN_MIDDLE;

				tdxxcell11.Colspan=3;
				PdfPCell tdxxcell12 = new PdfPCell(new PdfPCell(new Paragraph("权利性质", textfont)));
				tdxxcell12.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell12.VerticalAlignment=Element.ALIGN_MIDDLE;


				PdfPCell tdxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("TDQLXZ") == null ? "" : djzldata.GetValue("TDQLXZ").ToString(), textfont)));
				tdxxcell13.HorizontalAlignment=Element.ALIGN_CENTER;
				tdxxcell13.VerticalAlignment=Element.ALIGN_MIDDLE;

				tdxxcell13.Colspan=4;

				qlrtable.AddCell(dixxcell);
				qlrtable.AddCell(tdxxcell2);
				qlrtable.AddCell(tdxxcell3);
				qlrtable.AddCell(tdxxcell4);
				qlrtable.AddCell(tdxxcell5);
				qlrtable.AddCell(tdxxcell6);
				qlrtable.AddCell(tdxxcell7);
				qlrtable.AddCell(tdxxcell8);
				qlrtable.AddCell(tdxxcell9);
				qlrtable.AddCell(tdxxcell10);
				qlrtable.AddCell(tdxxcell11);
				qlrtable.AddCell(tdxxcell12);
				qlrtable.AddCell(tdxxcell13);

				//===土地信息结束==================

				//===房屋状况开始==================
				//	        JSONArray ygdyxx = ygInfo.getDYXX();
				//	        int ygdyxxSize = ygdyxx.size();

				PdfPCell fwzkcell = new PdfPCell(new PdfPCell(new Paragraph("房屋状况", textfont)));
				fwzkcell.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell.VerticalAlignment=Element.ALIGN_MIDDLE;
				fwzkcell.Rowspan=6;

				PdfPCell fwzkcell2 = new PdfPCell(new PdfPCell(new Paragraph("幢号", textfont)));
				fwzkcell2.MinimumHeight=24f;
				fwzkcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell2.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell3 = new PdfPCell(new PdfPCell(new Paragraph("房号", textfont)));
				fwzkcell3.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell3.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell4 = new PdfPCell(new PdfPCell(new Paragraph("结构", textfont)));
				fwzkcell4.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell4.VerticalAlignment=Element.ALIGN_MIDDLE;
				fwzkcell4.Colspan=2;

				PdfPCell fwzkcell5 = new PdfPCell(new PdfPCell(new Paragraph("房屋总层数", textfont)));
				fwzkcell5.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell5.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell6 = new PdfPCell(new PdfPCell(new Paragraph("名义层", textfont)));
				fwzkcell6.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell6.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell7 = new PdfPCell(new PdfPCell(new Paragraph("建筑面积（㎡）", textfont)));
				fwzkcell7.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell7.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell8 = new PdfPCell(new PdfPCell(new Paragraph("规划用途", textfont)));
				fwzkcell8.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell8.VerticalAlignment=Element.ALIGN_MIDDLE;
				fwzkcell8.Colspan=2;

				qlrtable.AddCell(fwzkcell);
				qlrtable.AddCell(fwzkcell2);
				qlrtable.AddCell(fwzkcell3);
				qlrtable.AddCell(fwzkcell4);
				qlrtable.AddCell(fwzkcell5);
				qlrtable.AddCell(fwzkcell6);
				qlrtable.AddCell(fwzkcell7);
				qlrtable.AddCell(fwzkcell8);

				PdfPCell fwzkcell21 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("ZRZH") == null ? "" : djzldata.GetValue("ZRZH").ToString(), textfont)));
				fwzkcell21.MinimumHeight=24f;
				fwzkcell21.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell21.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell31 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("FJH") == null ? "" : djzldata.GetValue("FJH").ToString(), textfont)));
				fwzkcell31.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell31.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell41 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("FWJG") == null ? "" : djzldata.GetValue("FWJG").ToString(), textfont)));
				fwzkcell41.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell41.VerticalAlignment=Element.ALIGN_MIDDLE;
				fwzkcell41.Colspan=2;

				PdfPCell fwzkcell51 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("ZCS") == null ? "" : djzldata.GetValue("ZCS").ToString(), textfont)));
				fwzkcell51.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell51.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell61 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("MYC") == null ? "" : djzldata.GetValue("MYC").ToString(), textfont)));
				fwzkcell61.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell61.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell71 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("JZMJ") == null ? "" : djzldata.GetValue("JZMJ").ToString(), textfont)));
				fwzkcell71.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell71.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell fwzkcell81 = new PdfPCell(new PdfPCell(new Paragraph(djzldata.GetValue("YT") == null ? "" : djzldata.GetValue("YT").ToString(), textfont)));
				fwzkcell81.HorizontalAlignment=Element.ALIGN_CENTER;
				fwzkcell81.VerticalAlignment=Element.ALIGN_MIDDLE;
				fwzkcell81.Colspan=2;

				//for(int i =0;i<5;i++)
				//{
				qlrtable.AddCell(fwzkcell21);
				qlrtable.AddCell(fwzkcell31);
				qlrtable.AddCell(fwzkcell41);
				qlrtable.AddCell(fwzkcell51);
				qlrtable.AddCell(fwzkcell61);
				qlrtable.AddCell(fwzkcell71);
				qlrtable.AddCell(fwzkcell81);
				//}

				PdfPCell fwblank1 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));
				fwblank1.MinimumHeight=24f;

				PdfPCell fwblank2 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

				PdfPCell fwblank3 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));
				fwblank3.Colspan=2;

				PdfPCell fwblank4 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

				PdfPCell fwblank5 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

				PdfPCell fwblank6 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

				PdfPCell fwblank7 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));
				fwblank7.Colspan=2;

				for (int i = 0; i < 4; i++)
				{
					qlrtable.AddCell(fwblank1);
					qlrtable.AddCell(fwblank2);
					qlrtable.AddCell(fwblank3);
					qlrtable.AddCell(fwblank4);
					qlrtable.AddCell(fwblank5);
					qlrtable.AddCell(fwblank6);
					qlrtable.AddCell(fwblank7);
				}



                //===房屋状况结束==================

                //===抵押信息开始==================
                JArray djzldyxx = (JArray)djzldata["DYXX"];
                int djzldyxxSize = djzldyxx.Count;
                PdfPCell dyxxcell = new PdfPCell(new PdfPCell(new Paragraph("抵押信息", textfont)));
				dyxxcell.HorizontalAlignment=Element.ALIGN_CENTER;
				dyxxcell.VerticalAlignment=Element.ALIGN_MIDDLE;
				dyxxcell.Rowspan=djzldyxxSize + 1;

				PdfPCell dyxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("抵押权人", textfont)));
				dyxxcell2.MinimumHeight=24f;
				dyxxcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				dyxxcell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				dyxxcell2.Colspan=2;

				PdfPCell dyxxcell3 = new PdfPCell(new PdfPCell(new Paragraph("登记日期", textfont)));
				dyxxcell3.HorizontalAlignment=Element.ALIGN_CENTER;
				dyxxcell3.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell dyxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("抵押方式", textfont)));
				dyxxcell4.HorizontalAlignment=Element.ALIGN_CENTER;
				dyxxcell4.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell dyxxcell5 = new PdfPCell(new PdfPCell(new Paragraph("不动产证明号", textfont)));
				dyxxcell5.HorizontalAlignment=Element.ALIGN_CENTER;
				dyxxcell5.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell dyxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("被担保主债权数额/最高债权额（万元）", textfont)));
				dyxxcell6.HorizontalAlignment=Element.ALIGN_CENTER;
				dyxxcell6.VerticalAlignment=Element.ALIGN_MIDDLE;
				dyxxcell6.Colspan=2;

				PdfPCell dyxxcell7 = new PdfPCell(new PdfPCell(new Paragraph("债务履行期限", textfont)));
				dyxxcell7.HorizontalAlignment=Element.ALIGN_CENTER;
				dyxxcell7.VerticalAlignment=Element.ALIGN_MIDDLE;
				dyxxcell7.Colspan=2;

				qlrtable.AddCell(dyxxcell);
				qlrtable.AddCell(dyxxcell2);
				qlrtable.AddCell(dyxxcell3);
				qlrtable.AddCell(dyxxcell4);
				qlrtable.AddCell(dyxxcell5);
				qlrtable.AddCell(dyxxcell6);
				qlrtable.AddCell(dyxxcell7);

				String dyqr2 = "";//抵押权人
				String djrq = "";//登记日期
				String dyfs = "";//抵押方式
				String bdczmh = "";//不动产证明号
				String zgzqe = "";//最高债权额
				String zwlxqk = "";//债务履行情况

				JObject djzldyqrJson;
				String djzldyqr = "";
				String djzldjsj = "";
				String djzldyfs = "";
				String djzlbdcdjzmh = "";
				String djzldyje = "";
               
                foreach (JObject djzldyxx1 in djzldyxx)
                {
                    
                    djzldyqr = djzldyxx1.GetValue("DYQR") == null ? "" : djzldyxx1.GetValue("DYQR").ToString();
                    djzldjsj = djzldyxx1.GetValue("DJSJ") == null ? "" : djzldyxx1.GetValue("DJSJ").ToString();
                    djzldyfs = djzldyxx1.GetValue("DYFS") == null ? "" : djzldyxx1.GetValue("DYFS").ToString();
                    djzlbdcdjzmh = djzldyxx1.GetValue("BDCDJZMH") == null ? "" : djzldyxx1.GetValue("BDCDJZMH").ToString();
                    djzldyje = djzldyxx1.GetValue("DYJE") == null ? "" : djzldyxx1.GetValue("DYJE").ToString();

                    PdfPCell dyxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(djzldyqr, textfont)));
                    dyxxcell8.MinimumHeight = 24f;
                    dyxxcell8.HorizontalAlignment = Element.ALIGN_CENTER;
                    dyxxcell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dyxxcell8.Colspan = 2;

                    PdfPCell dyxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(djzldjsj, textfont)));
                    dyxxcell9.HorizontalAlignment = Element.ALIGN_CENTER;
                    dyxxcell9.VerticalAlignment = Element.ALIGN_MIDDLE;

                    PdfPCell dyxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(djzldyfs, textfont)));
                    dyxxcell10.HorizontalAlignment = Element.ALIGN_CENTER;
                    dyxxcell10.VerticalAlignment = Element.ALIGN_MIDDLE;

                    PdfPCell dyxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(djzlbdcdjzmh, textfont)));
                    dyxxcell11.HorizontalAlignment = Element.ALIGN_CENTER;
                    dyxxcell11.VerticalAlignment = Element.ALIGN_MIDDLE;

                    PdfPCell dyxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(djzldyje, textfont)));
                    dyxxcell12.HorizontalAlignment = Element.ALIGN_CENTER;
                    dyxxcell12.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dyxxcell12.Colspan = 2;

                    PdfPCell dyxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("ZQLXQX") == null ? "" : dyxxdata.GetValue("ZQLXQX").ToString(), textfont)));
                    dyxxcell13.HorizontalAlignment = Element.ALIGN_CENTER;
                    dyxxcell13.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dyxxcell13.Colspan = 2;

                    qlrtable.AddCell(dyxxcell8);
                    qlrtable.AddCell(dyxxcell9);
                    qlrtable.AddCell(dyxxcell10);
                    qlrtable.AddCell(dyxxcell11);
                    qlrtable.AddCell(dyxxcell12);
                    qlrtable.AddCell(dyxxcell13);
                }
                //            for (int i = 0; i < djzldyxxSize; i++)
                //{
                //	djzldyqrJson = djzldyxx.getJSONObject(i);
                //	djzldyqr = djzldyqrJson.get("DYQR") == null ? "" : djzldyqrJson.get("DYQR").toString();//djzlygqlrJson.get("QLRMC").toString();
                //	djzldjsj = djzldyqrJson.get("DJSJ") == null ? "" : djzldyqrJson.get("DJSJ").toString();
                //	djzldyfs = djzldyqrJson.get("DYFS") == null ? "" : djzldyqrJson.get("DYFS").toString();
                //	djzlbdcdjzmh = djzldyqrJson.get("BDCDJZMH") == null ? "" : djzldyqrJson.get("BDCDJZMH").toString();
                //	djzldyje = djzldyqrJson.get("DYJE") == null ? "" : djzldyqrJson.get("DYJE").toString();

                //	PdfPCell dyxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(djzldyqr, textfont)));
                //	dyxxcell8.MinimumHeight=24f;
                //	dyxxcell8.HorizontalAlignment=Element.ALIGN_CENTER;
                //	dyxxcell8.VerticalAlignment=Element.ALIGN_MIDDLE;
                //	dyxxcell8.Colspan=2;

                //	PdfPCell dyxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(djzldjsj, textfont)));
                //	dyxxcell9.HorizontalAlignment=Element.ALIGN_CENTER;
                //	dyxxcell9.VerticalAlignment=Element.ALIGN_MIDDLE;

                //	PdfPCell dyxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(djzldyfs, textfont)));
                //	dyxxcell10.HorizontalAlignment=Element.ALIGN_CENTER;
                //	dyxxcell10.VerticalAlignment=Element.ALIGN_MIDDLE;

                //	PdfPCell dyxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(djzlbdcdjzmh, textfont)));
                //	dyxxcell11.HorizontalAlignment=Element.ALIGN_CENTER;
                //	dyxxcell11.VerticalAlignment=Element.ALIGN_MIDDLE;

                //	PdfPCell dyxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(djzldyje, textfont)));
                //	dyxxcell12.HorizontalAlignment=Element.ALIGN_CENTER;
                //	dyxxcell12.VerticalAlignment=Element.ALIGN_MIDDLE;
                //	dyxxcell12.Colspan=2;

                //	PdfPCell dyxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(dyxxdata.GetValue("ZQLXQX") == null ? "" : dyxxdata.GetValue("ZQLXQX").ToString(), textfont)));
                //	dyxxcell13.HorizontalAlignment=Element.ALIGN_CENTER;
                //	dyxxcell13.VerticalAlignment=Element.ALIGN_MIDDLE;
                //	dyxxcell13.Colspan=2;

                //	qlrtable.AddCell(dyxxcell8);
                //	qlrtable.AddCell(dyxxcell9);
                //	qlrtable.AddCell(dyxxcell10);
                //	qlrtable.AddCell(dyxxcell11);
                //	qlrtable.AddCell(dyxxcell12);
                //	qlrtable.AddCell(dyxxcell13);

                //}
                //			PdfPCell fwzkcell211 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
                //			fwzkcell211.Colspan=2);
                //	        PdfPCell fwzkcell311 = new PdfPCell(new PdfPCell(new Paragraph("以下空白",textfont)));
                //	        PdfPCell fwzkcell411 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
                //	        PdfPCell fwzkcell511 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
                //	        PdfPCell fwzkcell611 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
                //	        fwzkcell611.Colspan=2);
                //	        PdfPCell fwzkcell711 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
                //	        fwzkcell711.Colspan=2);
                //	        
                //	        qlrtable.AddCell(fwzkcell211);
                //	        qlrtable.AddCell(fwzkcell311);
                //	        qlrtable.AddCell(fwzkcell411);
                //	        qlrtable.AddCell(fwzkcell511);
                //	        qlrtable.AddCell(fwzkcell611);
                //	        qlrtable.AddCell(fwzkcell711);
                //===抵押信息结束==================

                //===查封信息开始==================
                //	        JSONArray ygcfxx = ygInfo.getCFXX();
                //	        int ygcfxxSize = ygcfxx.size();

                JArray djzlcfxx = (JArray)djzldata["CFXX"];
                int djzlcfxxSize = djzlcfxx.Count;
                //JSONArray djzlcfxx = (JSONArray)djzldata.get("CFXX");
                //int djzlcfxxSize = djzlcfxx.size();
                PdfPCell cfxxcell = new PdfPCell(new PdfPCell(new Paragraph("查封信息", textfont)));
				cfxxcell.HorizontalAlignment=Element.ALIGN_CENTER;
				cfxxcell.VerticalAlignment=Element.ALIGN_MIDDLE;
				cfxxcell.Rowspan=djzlcfxxSize + 1;

				PdfPCell cfxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("查封法院", textfont)));
				cfxxcell2.MinimumHeight=24f;
				cfxxcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				cfxxcell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				cfxxcell2.Colspan=2;

				PdfPCell cfxxcell3 = new PdfPCell(new PdfPCell(new Paragraph("查封文号", textfont)));
				cfxxcell3.HorizontalAlignment=Element.ALIGN_CENTER;
				cfxxcell3.VerticalAlignment=Element.ALIGN_MIDDLE;
				cfxxcell3.Colspan=2;

				PdfPCell cfxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("查封日期", textfont)));
				cfxxcell4.HorizontalAlignment=Element.ALIGN_CENTER;
				cfxxcell4.VerticalAlignment=Element.ALIGN_MIDDLE;
				cfxxcell4.Colspan=2;

				PdfPCell cfxxcell5 = new PdfPCell(new PdfPCell(new Paragraph("限制期限", textfont)));
				cfxxcell5.HorizontalAlignment=Element.ALIGN_CENTER;
				cfxxcell5.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell cfxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("限制类别", textfont)));
				cfxxcell6.HorizontalAlignment=Element.ALIGN_CENTER;
				cfxxcell6.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell cfxxcell7 = new PdfPCell(new PdfPCell(new Paragraph("类别", textfont)));
				cfxxcell7.HorizontalAlignment=Element.ALIGN_CENTER;
				cfxxcell7.VerticalAlignment=Element.ALIGN_MIDDLE;

				qlrtable.AddCell(cfxxcell);
				qlrtable.AddCell(cfxxcell2);
				qlrtable.AddCell(cfxxcell3);
				qlrtable.AddCell(cfxxcell4);
				qlrtable.AddCell(cfxxcell5);
				qlrtable.AddCell(cfxxcell6);
				qlrtable.AddCell(cfxxcell7);

				String cffy = "";//查封法院
				String cfwh = "";//查封文号
				String cfrq = "";//查封日期
				String xzrq = "";//限制日期
				String xzlb = "";//限制类别
				String lb = "";//类别
                foreach (JObject djzlcfxx1 in djzlcfxx)
                {
                    cffy = djzlcfxx1.GetValue("CFJG") == null ? "" : djzlcfxx1.GetValue("CFJG").ToString();
                    cfwh = djzlcfxx1.GetValue("CFWH") == null ? "" : djzlcfxx1.GetValue("CFWH").ToString();
                    cfrq = djzlcfxx1.GetValue("DJSJ") == null ? "" : djzlcfxx1.GetValue("DJSJ").ToString();
                    xzrq = djzlcfxx1.GetValue("CFJSSJ") == null ? "" : djzlcfxx1.GetValue("CFJSSJ").ToString();
                    xzlb = djzlcfxx1.GetValue("CFLX") == null ? "" : djzlcfxx1.GetValue("CFLX").ToString();
                    lb = djzlcfxx1.GetValue("QSZT") == null ? "" : djzlcfxx1.GetValue("QSZT").ToString();

                    PdfPCell cfxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(cffy, textfont)));
                    cfxxcell8.MinimumHeight = 24f;
                    cfxxcell8.HorizontalAlignment = Element.ALIGN_CENTER;
                    cfxxcell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cfxxcell8.Colspan = 2;

                    PdfPCell cfxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(cfwh, textfont)));
                    cfxxcell9.HorizontalAlignment = Element.ALIGN_CENTER;
                    cfxxcell9.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cfxxcell9.Colspan = 2;

                    PdfPCell cfxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(cfrq/* .substring(0, 10) */, textfont)));
                    cfxxcell10.HorizontalAlignment = Element.ALIGN_CENTER;
                    cfxxcell10.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cfxxcell10.Colspan = 2;

                    PdfPCell cfxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(xzrq/* .substring(0, 10) */, textfont)));
                    cfxxcell11.HorizontalAlignment = Element.ALIGN_CENTER;
                    cfxxcell11.VerticalAlignment = Element.ALIGN_MIDDLE;

                    PdfPCell cfxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(xzlb, textfont)));
                    cfxxcell12.HorizontalAlignment = Element.ALIGN_CENTER;
                    cfxxcell12.VerticalAlignment = Element.ALIGN_MIDDLE;

                    PdfPCell cfxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(lb, textfont)));
                    cfxxcell13.HorizontalAlignment = Element.ALIGN_CENTER;
                    cfxxcell13.VerticalAlignment = Element.ALIGN_MIDDLE;

                    qlrtable.AddCell(cfxxcell8);
                    qlrtable.AddCell(cfxxcell9);
                    qlrtable.AddCell(cfxxcell10);
                    qlrtable.AddCell(cfxxcell11);
                    qlrtable.AddCell(cfxxcell12);
                    qlrtable.AddCell(cfxxcell13);
                }
    //                for (int i = 0; i < djzlcfxxSize; i++)
				//{

				//	JSONObject djzlcfxxObj = djzlcfxx.getJSONObject(i);

				//	cffy = djzlcfxxObj.get("CFJG") == null ? "" : djzlcfxxObj.get("CFJG").toString();
				//	cfwh = djzlcfxxObj.get("CFWH") == null ? "" : djzlcfxxObj.get("CFWH").toString();
				//	cfrq = djzlcfxxObj.get("DJSJ") == null ? "" : djzlcfxxObj.get("DJSJ").toString();
				//	xzrq = djzlcfxxObj.get("CFJSSJ") == null ? "" : djzlcfxxObj.get("CFJSSJ").toString();
				//	xzlb = djzlcfxxObj.get("CFLX") == null ? "" : djzlcfxxObj.get("CFLX").toString();
				//	lb = djzlcfxxObj.get("QSZT") == null ? "" : djzlcfxxObj.get("QSZT").toString();

				//	PdfPCell cfxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(cffy, textfont)));
				//	cfxxcell8.MinimumHeight=24f;
				//	cfxxcell8.HorizontalAlignment=Element.ALIGN_CENTER;
				//	cfxxcell8.VerticalAlignment=Element.ALIGN_MIDDLE;
				//	cfxxcell8.Colspan=2;

				//	PdfPCell cfxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(cfwh, textfont)));
				//	cfxxcell9.HorizontalAlignment=Element.ALIGN_CENTER;
				//	cfxxcell9.VerticalAlignment=Element.ALIGN_MIDDLE;
				//	cfxxcell9.Colspan=2;

				//	PdfPCell cfxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(cfrq/* .substring(0, 10) */, textfont)));
				//	cfxxcell10.HorizontalAlignment=Element.ALIGN_CENTER;
				//	cfxxcell10.VerticalAlignment=Element.ALIGN_MIDDLE;
				//	cfxxcell10.Colspan=2;

				//	PdfPCell cfxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(xzrq/* .substring(0, 10) */, textfont)));
				//	cfxxcell11.HorizontalAlignment=Element.ALIGN_CENTER;
				//	cfxxcell11.VerticalAlignment=Element.ALIGN_MIDDLE;

				//	PdfPCell cfxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(xzlb, textfont)));
				//	cfxxcell12.HorizontalAlignment=Element.ALIGN_CENTER;
				//	cfxxcell12.VerticalAlignment=Element.ALIGN_MIDDLE;

				//	PdfPCell cfxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(lb, textfont)));
				//	cfxxcell13.HorizontalAlignment=Element.ALIGN_CENTER;
				//	cfxxcell13.VerticalAlignment=Element.ALIGN_MIDDLE;

				//	qlrtable.AddCell(cfxxcell8);
				//	qlrtable.AddCell(cfxxcell9);
				//	qlrtable.AddCell(cfxxcell10);
				//	qlrtable.AddCell(cfxxcell11);
				//	qlrtable.AddCell(cfxxcell12);
				//	qlrtable.AddCell(cfxxcell13);
				//}

				/*PdfPCell cfxxce32 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
				cfxxce32.Colspan=2);
				PdfPCell cfxxce33 = new PdfPCell(new PdfPCell(new Paragraph("以下空白",textfont)));
				cfxxce33.Colspan=2);
				PdfPCell cfxxce34 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
				cfxxce34.Colspan=2);
				PdfPCell cfxxce35 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
				PdfPCell cfxxce36 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));
				PdfPCell cfxxce37 = new PdfPCell(new PdfPCell(new Paragraph("",textfont)));

				qlrtable.AddCell(cfxxce32);
				qlrtable.AddCell(cfxxce33);
				qlrtable.AddCell(cfxxce34);
				qlrtable.AddCell(cfxxce35);
				qlrtable.AddCell(cfxxce36);
				qlrtable.AddCell(cfxxce37);
				*/
				//===查封信息结束==================

				//===预告开始==================

				PdfPCell ygcell = new PdfPCell(new PdfPCell(new Paragraph("预告", textfont)));
				ygcell.MinimumHeight=24f;
				ygcell.HorizontalAlignment=Element.ALIGN_CENTER;
				ygcell.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell ygcell2 = new PdfPCell(new PdfPCell(new Paragraph("权利人", textfont)));
				ygcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				ygcell2.VerticalAlignment=Element.ALIGN_MIDDLE;

				JArray ygdataqlr = (JArray)ygxxdata["QLR"];
				String qlrmcs = "";
                foreach(JObject ygdataqlr1 in ygdataqlr)
                {
                    qlrmcs += (string)ygdataqlr1.GetValue("QLRMC") + ",";
                }
				//for (int i = 0; i < ygdataqlr.size(); i++)
				//{
				//	qlrmcs += ygdataqlr.getJSONObject(i).get("QLRMC") + ",";
				//}
				//if (qlrmcs != "")
				//{
				//	qlrmcs = qlrmcs.substring(0, qlrmcs.length() - 1);
				//}

				PdfPCell ygcell3 = new PdfPCell(new PdfPCell(new Paragraph(qlrmcs, textfont)));
				ygcell3.HorizontalAlignment=Element.ALIGN_CENTER;
				ygcell3.VerticalAlignment=Element.ALIGN_MIDDLE;
				ygcell3.Colspan=3;

				PdfPCell ygcell4 = new PdfPCell(new PdfPCell(new Paragraph("义务人", textfont)));
				ygcell4.HorizontalAlignment=Element.ALIGN_CENTER;
				ygcell4.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell ygcell5 = new PdfPCell(new PdfPCell(new Paragraph(ygxxdata.GetValue("YWR") == null ? "" : ygxxdata.GetValue("YWR").ToString(), textfont)));
				ygcell5.HorizontalAlignment=Element.ALIGN_CENTER;
				ygcell5.VerticalAlignment=Element.ALIGN_MIDDLE;
				ygcell5.Colspan=4;

				qlrtable.AddCell(ygcell);
				qlrtable.AddCell(ygcell2);
				qlrtable.AddCell(ygcell3);
				qlrtable.AddCell(ygcell4);
				qlrtable.AddCell(ygcell5);

				//===预告结束==================

				//===权利人其他情况开始==================
				//	        JSONArray ygdyxx = ygInfo.getDYXX();
				//	        int ygdyxxSize = ygdyxx.size();

				PdfPCell qlrcell = new PdfPCell(new PdfPCell(new Paragraph("权利人其他情况", textfont)));
				qlrcell.HorizontalAlignment=Element.ALIGN_CENTER;
				qlrcell.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell qlrcell2 = new PdfPCell(new PdfPCell(new Paragraph(ygxxdata.GetValue("QLQTZK") == null ? "" : ygxxdata.GetValue("QLQTZK").ToString(), textfont)));
				qlrcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				qlrcell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				qlrcell2.Colspan=9;

				qlrtable.AddCell(qlrcell);
				qlrtable.AddCell(qlrcell2);

				//===权利人其他情况结束==================

				//===附记开始==================
				//	        JSONArray ygdyxx = ygInfo.getDYXX();
				//	        int ygdyxxSize = ygdyxx.size();

				PdfPCell fjcell = new PdfPCell(new PdfPCell(new Paragraph("附记", textfont)));
				fjcell.MinimumHeight=24f;
				fjcell.HorizontalAlignment=Element.ALIGN_CENTER;
				fjcell.VerticalAlignment=Element.ALIGN_MIDDLE;

				String fj = djzldata.GetValue("FJ") == null ? "" : djzldata.GetValue("FJ").ToString();
				PdfPCell fjcell2 = new PdfPCell(new PdfPCell(new Paragraph(fj, textfont)));
				fjcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				fjcell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				fjcell2.Colspan=9;

				qlrtable.AddCell(fjcell);
				qlrtable.AddCell(fjcell2);

				//===附记结束==================

				//===备注开始==================
				//	        JSONArray ygdyxx = ygInfo.getDYXX();
				//	        int ygdyxxSize = ygdyxx.size();

				PdfPCell bzcell = new PdfPCell(new PdfPCell(new Paragraph("备注", textfont)));
				bzcell.MinimumHeight=24f;
				bzcell.HorizontalAlignment=Element.ALIGN_CENTER;
				bzcell.VerticalAlignment=Element.ALIGN_MIDDLE;

				String bz = dyxxdata.GetValue("BZ") == null ? "" : dyxxdata.GetValue("BZ").ToString();
				// String bz ="";
				PdfPCell bzcell2 = new PdfPCell(new PdfPCell(new Paragraph(bz, textfont)));
				bzcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				bzcell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				bzcell2.Colspan=9;

				qlrtable.AddCell(bzcell);
				qlrtable.AddCell(bzcell2);
				//===备注结束==================

				//===特别提示开始==================
				//	        JSONArray ygdyxx = ygInfo.getDYXX();
				//	        int ygdyxxSize = ygdyxx.size();

				PdfPCell tbtscell = new PdfPCell(new PdfPCell(new Paragraph("特别提示：" + "\r\n" + "1、请当场核实本查询结果证明，如有异议，请向工作人员或查询窗口提出复核；因隐瞒真实信息或提供虚假信息所产生的一切法律责任，均由查询人自行承担。" + "\r\n" + "2、请妥善保管本人身份证件和查询结果证明，如涉及国家机密、个人隐私或商业秘密，查询人负有保密责任；因保管不当、信息泄露或不正当使用所产生的一切法律责任，均由查询人自行承担。", textfont)));
				// tbtscell.MinimumHeight=70f);
				tbtscell.HorizontalAlignment=Element.ALIGN_LEFT;
				tbtscell.VerticalAlignment=Element.ALIGN_MIDDLE;
				tbtscell.PaddingLeft=5f;
				tbtscell.PaddingRight=5f;
				tbtscell.PaddingTop=5f;
				tbtscell.PaddingBottom=5f;
				tbtscell.Colspan=10;

				qlrtable.AddCell(tbtscell);
				//===特别提示结束==================

				//==最后一行开始=======================================================================
				PdfPCell zhcell = new PdfPCell(new PdfPCell(new Paragraph("出具人", headfont)));
				zhcell.MinimumHeight=24f;
				zhcell.HorizontalAlignment=Element.ALIGN_CENTER;
				zhcell.VerticalAlignment=Element.ALIGN_MIDDLE;
				zhcell.Colspan=2;

				PdfPCell zhcell2 = new PdfPCell(new PdfPCell(new Paragraph(" ", headfont)));
				zhcell2.HorizontalAlignment=Element.ALIGN_CENTER;
				zhcell2.VerticalAlignment=Element.ALIGN_MIDDLE;
				zhcell2.Colspan=3;

				PdfPCell zhcell3 = new PdfPCell(new PdfPCell(new Paragraph("证明出具单位", headfont)));
				zhcell3.HorizontalAlignment=Element.ALIGN_CENTER;
				zhcell3.VerticalAlignment=Element.ALIGN_MIDDLE;

				PdfPCell zhcell4 = new PdfPCell(new PdfPCell(new Paragraph("宜兴市自然资源和规划局（盖章）", headfont)));
				zhcell4.HorizontalAlignment=Element.ALIGN_CENTER;
				zhcell4.VerticalAlignment=Element.ALIGN_MIDDLE;
				zhcell4.Colspan=4;

				qlrtable.AddCell(zhcell);
				qlrtable.AddCell(zhcell2);
				qlrtable.AddCell(zhcell3);
				qlrtable.AddCell(zhcell4);

				//==最后一行结束========================================================================

				document.Add(qlrtable);

			}
			catch (Exception e)
			{
				
			}
			finally
			{
				// 5.关闭文档
				document.Close();
                pdfWriter.Close();
                stream.Close();
			}
		}
    }
}
