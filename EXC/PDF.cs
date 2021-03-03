
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

namespace EXC
{

    public class PDF
    {

        private static ObservableCollection<QLRItem> QLRItems;
        private static ObservableCollection<DYItem> DYItems;
        private static ObservableCollection<CFItem> CFItems;
        private static ObservableCollection<QLRItem> YGQLRItems; //预告义务人


        /// 宜兴市不动产登记证明 权利人
        public static bool DrawYiXing1(string FilePath, IDCardData IDCardData, string CQZH)
        {
            HouseBasic houseBasic = new HouseBasic();
            QLRItems = new ObservableCollection<QLRItem>();
            DYItems = new ObservableCollection<DYItem>();
            CFItems = new ObservableCollection<CFItem>();
            YGQLRItems = new ObservableCollection<QLRItem>();
            string ygywr = "";
            JObject jObject = null; ;
            string CQZSresponse = Http.YinXingNew.CQZS("", "", "", CQZH);

            try
            {
                JObject CQZSdata = (JObject)JsonConvert.DeserializeObject(CQZSresponse);

                if ((string)CQZSdata.GetValue("code") == "0")
                {
                    jObject = (JObject)CQZSdata["data"][0];

                    houseBasic.BDCDYH = (string)jObject.GetValue("BDCDYH");
                    houseBasic.ZL = (string)jObject.GetValue("ZL");
                    houseBasic.CQZH = (string)jObject.GetValue("CQZH");
                    houseBasic.ZDDM = (string)jObject.GetValue("ZDDM");
                    houseBasic.ZDMJ = (string)jObject.GetValue("ZDMJ");
                    houseBasic.YT = (string)jObject.GetValue("YT");
                    houseBasic.TDYT = (string)jObject.GetValue("TDYT");
                    houseBasic.TDSYQMJ = (string)jObject.GetValue("TDSYQMJ");
                    houseBasic.FJ = (string)jObject.GetValue("FJ");
                    houseBasic.ZCS = (int)jObject.GetValue("ZCS");
                    houseBasic.TDQLXZ = (string)jObject.GetValue("QLXZ");
                    houseBasic.FJH = (string)jObject.GetValue("FJH");
                    houseBasic.ZRZH = (string)jObject.GetValue("ZRZH");
                    houseBasic.TDQLXZ = (string)jObject.GetValue("TDQLXZ");
                    houseBasic.MYC = (string)jObject.GetValue("MYC");
                    houseBasic.FWXZ = (string)jObject.GetValue("FWXZ");
                    houseBasic.FWJG = (string)jObject.GetValue("FWJG");
                    houseBasic.JZMJ = (string)jObject.GetValue("JZMJ");
                    houseBasic.QLQTZK = (string)jObject.GetValue("QLQTZK");
                    houseBasic.QLJSSJ = (string)jObject.GetValue("QLJSSJ");
                    houseBasic.QT = (string)jObject.GetValue("QT");
                    try
                    {
                        JArray jObjectQLR = (JArray)jObject["QLR"];

                        foreach (JObject item in jObjectQLR)
                        {
                            QLRItem qLRItem = new QLRItem
                            {
                                QLRMC = (string)item.GetValue("QLRMC"),
                                QLRZJZL = (string)item.GetValue("QLRZJZL"),
                                QLRZJH = (string)item.GetValue("QLRZJH"),
                                GYFS = (string)item.GetValue("GYFS"),
                                QLBL = (string)item.GetValue("QLBL"),
                                CQZH = (string)item.GetValue("CQZH")
                            };
                            QLRItems.Add(qLRItem);
                        }

                    }
                    catch
                    {
                    }
                    try
                    {
                        JArray jObjectDY = (JArray)jObject["DYXX"];
                        if (jObjectDY != null)
                        {
                            foreach (JObject item in jObjectDY)
                            {
                                DYItem DYItem = new DYItem
                                {
                                    FWDM = (string)item.GetValue("FWDM"),
                                    BDCDYH = (string)item.GetValue("BDCDYH"),
                                    BDCDJZMH = (string)item.GetValue("BDCDJZMH"),
                                    DYJE = (string)item.GetValue("DYJE"),
                                    DBFW = (string)item.GetValue("DBFW"),
                                    DYQR = (string)item.GetValue("DYQR"),
                                    DYR = (string)item.GetValue("DYR"),
                                    QT = (string)item.GetValue("QT"),
                                    CQZH = (string)item.GetValue("CQZH"),
                                    DYKSSJ = (string)item.GetValue("DYKSSJ"),
                                    DYJSSJ = (string)item.GetValue("DYJSSJ"),
                                    FJ = (string)item.GetValue("FJ"),
                                    QSZT = (string)item.GetValue("QSZT"),
                                    DJSJ = (string)item.GetValue("DJSJ"),
                                    DYFS = (string)item.GetValue("DYFSMC"),
                                    ZGZQQDSSHSE = (string)item.GetValue("DYJE"),
                                };
                                try
                                {
                                    string DYCXresponse = Http.YinXingNew.DYCX(DYItem.BDCDJZMH);
                                    JObject DYCXdata = (JObject)JsonConvert.DeserializeObject(DYCXresponse);
                                    if ((string)DYCXdata.GetValue("code") == "0")
                                    {
                                        JObject jObject1 = (JObject)DYCXdata["data"];
                                        DYItem.ZQLXQX = (string)jObject1.GetValue("ZQLXQX");
                                        DYItem.DYFS = (string)jObject1.GetValue("DYFS");
                                        DYItem.ZGZQQDSSHSE = jObject1.GetValue("ZGZQQDSSHSE").ToString();
                                        DYItem.FCDYMJ = jObject1.GetValue("FCDYMJ").ToString();
                                        DYItem.TDDYMJ = jObject1.GetValue("TDDYMJ").ToString();
                                    }
                                }
                                catch
                                {

                                }
                                DYItems.Add(DYItem);
                            }
                        }


                    }
                    catch
                    {


                    }
                    try
                    {
                        string YGCXresponse = Http.YinXingNew.YGCX(BDCDYH: houseBasic.BDCDYH);

                        JObject YGCXdata = (JObject)JsonConvert.DeserializeObject(YGCXresponse);
                        if ((string)YGCXdata.GetValue("code") == "0")
                        {
                            JObject jObject1 = (JObject)YGCXdata["data"][0];
                            ygywr = (string)jObject1.GetValue("YWR");
                            JArray jObjectQLR = (JArray)jObject1["QLR"];
                            foreach (JObject item in jObjectQLR)
                            {
                                QLRItem qLRItem = new QLRItem
                                {
                                    QLRMC = (string)item.GetValue("QLRMC"),
                                    QLRZJZL = (string)item.GetValue("QLRZJZL"),
                                    QLRZJH = (string)item.GetValue("QLRZJH"),
                                    GYFS = (string)item.GetValue("GYFS"),
                                    QLBL = (string)item.GetValue("QLBL"),
                                    CQZH = (string)item.GetValue("CQZH")
                                };
                                YGQLRItems.Add(qLRItem);
                            }
                        }


                    }
                    catch
                    {

                    }

                  

                    try
                    {
                        JArray jObjectCF = (JArray)jObject["CFXX"];

                        foreach (JObject item in jObjectCF)
                        {
                            CFItem CFItem = new CFItem
                            {
                                FWDM = (string)item.GetValue("FWDM"),
                                BDCDYH = (string)item.GetValue("BDCDYH"),
                                CFJG = (string)item.GetValue("CFJG"),
                                CFWH = (string)item.GetValue("CFWH"),
                                CFLX = (string)item.GetValue("CFLX"),
                                CFKSSJ = (string)item.GetValue("CFKSSJ"),
                                CFJSSJ = (string)item.GetValue("CFJSSJ"),
                                DJSJ = (string)item.GetValue("DJSJ"),
                                QSZT = (string)item.GetValue("QSZT"),
                                CQZH = (string)item.GetValue("CQZH"),
                                FJ = (string)item.GetValue("FJ"),
                            };

                            CFItems.Add(CFItem);
                        }


                    }
                    catch
                    {

                    }


                }
                else
                {
                    return false;
                }
            }
            catch
            {


                return false;

            }
            JObject djzldata = new JObject();

            JObject dyxxdata = new JObject();
            JObject ygxxdata = new JObject();

            //打印时间	   
            BaseFont bfChinese = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font headfont = new Font(bfChinese, 12, Font.NORMAL);// 设置字体大小
            Font keyfont = new Font(bfChinese, 21, Font.BOLD);// 设置字体大小
            Font textfont = new Font(bfChinese, 9, Font.NORMAL);// 设置字体大小
            Font textfont2 = new Font(bfChinese, 10, Font.NORMAL);// 设置字体大小
            Font textfont3 = new Font(bfChinese, 13, Font.NORMAL);
            Font textfont4 = new Font(bfChinese, 15, Font.NORMAL);

            Document document = new Document(PageSize.A4, 25, 25, 25, 25);
            Stream s = new FileStream(FilePath, FileMode.Create);
            PdfWriter.GetInstance(document, s);
            document.Open();

            Paragraph pt = new Paragraph("宜兴市不动产登记簿证明\n（权利人）", keyfont)
            {
                Alignment = 1,
            };
            document.Add(pt);
            document.Add(new Paragraph("\n"));

            float[] titlecell = { 0.3f, 0.3f, 0.3f };
            PdfPTable titletable = new PdfPTable(titlecell)
            {
                WidthPercentage = 100
            };
            Paragraph pt1 = new Paragraph("编号：" + DateTime.Now.ToString("yyyyMMddHHmmss"), textfont)
            {
                Alignment = 0
            };
            PdfPCell cell1 = new PdfPCell(pt1)
            {
                MinimumHeight = 24f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            cell1.DisableBorderSide(15);

            Paragraph pt2 = new Paragraph("查询人：" + IDCardData.Name, textfont)
            {
                Alignment = 0
            };
            PdfPCell cell2 = new PdfPCell(pt2)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            cell2.DisableBorderSide(15);

            Paragraph pt3 = new Paragraph("打印时间：" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), textfont)
            {
                Alignment = 2
            };
            PdfPCell cell3 = new PdfPCell(pt3)
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            cell3.DisableBorderSide(15);

            titletable.AddCell(cell1);
            titletable.AddCell(cell2);
            titletable.AddCell(cell3);

            document.Add(titletable);



            ////////////////////////////////////

            //==============================================


            string djzlqlrmc = "";//登记资料权利人名称
            string djzlqlrzjh = "";//权利人证件号码
            try
            {
                foreach (QLRItem item in QLRItems)
                {
                    djzlqlrmc += item.QLRMC + " ";
                    djzlqlrzjh += item.QLRZJH + " ";
                }
            }
            catch (Exception)
            {

            }



            float[] qlrwidhts = { 0.03f, 0.17f, 0.1f, 0.1f, 0.1f, 0.18f, 0.13f, 0.1f, 0.1f, 0.05f };
            PdfPTable qlrtable = new PdfPTable(qlrwidhts);
            qlrtable.WidthPercentage = 100; // 宽度100%填充

            //====第一行开始=================
            PdfPCell ycell = new PdfPCell(new PdfPCell(new Paragraph("权利人", textfont)));
            ycell.MinimumHeight = 24f;
            ycell.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell.Colspan = 2;

            PdfPCell ycell2 = new PdfPCell(new PdfPCell(new Paragraph(djzlqlrmc, textfont)));
            ycell2.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell2.Colspan = 3;

            PdfPCell ycell3 = new PdfPCell(new PdfPCell(new Paragraph("权利人证件号码", textfont)));
            ycell3.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell3.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell ycell4 = new PdfPCell(new PdfPCell(new Paragraph(djzlqlrzjh, textfont)));
            ycell4.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell4.Colspan = 4;

            qlrtable.AddCell(ycell);
            qlrtable.AddCell(ycell2);
            qlrtable.AddCell(ycell3);
            qlrtable.AddCell(ycell4);
            //===第一行结束==================

            //===第二行开始==================
            PdfPCell ecell = new PdfPCell(new PdfPCell(new Paragraph("不动产权证号", textfont)));
            ecell.MinimumHeight = 24f;
            ecell.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell.VerticalAlignment = Element.ALIGN_MIDDLE;
            ecell.Colspan = 2;

            PdfPCell ecell2 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.CQZH, textfont)));
            ecell2.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            ecell2.Colspan = 3;

            PdfPCell ecell3 = new PdfPCell(new PdfPCell(new Paragraph("不动产单元号", textfont)));
            ecell3.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell3.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell ecell4 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.BDCDYH, textfont)));
            ecell4.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            ecell4.Colspan = 4;

            qlrtable.AddCell(ecell);
            qlrtable.AddCell(ecell2);
            qlrtable.AddCell(ecell3);
            qlrtable.AddCell(ecell4);
            //===第二行结束==================

            //===第三行开始==================
            PdfPCell scell = new PdfPCell(new PdfPCell(new Paragraph("不动产坐落", textfont)));
            scell.MinimumHeight = 24f;
            scell.HorizontalAlignment = Element.ALIGN_CENTER;
            scell.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell.Colspan = 2;

            PdfPCell scell2 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.ZL, textfont)));
            scell2.HorizontalAlignment = Element.ALIGN_CENTER;
            scell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell2.Colspan = 8;

            qlrtable.AddCell(scell);
            qlrtable.AddCell(scell2);
            //===第三行结束==================

            //===土地信息开始==================
            PdfPCell dixxcell = new PdfPCell(new PdfPCell(new Paragraph("土地信息", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Rowspan = 3
            };

            PdfPCell tdxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("宗地代码", textfont)))
            {
                MinimumHeight = 24f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell tdxxcell3 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.ZDDM, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 3
            };

            PdfPCell tdxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("土地用途", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell tdxxcell5 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.TDYT, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 4
            };

            PdfPCell tdxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("宗地面积", textfont)))
            {
                MinimumHeight = 24f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell tdxxcell7 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.ZDMJ, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 3
            };

            PdfPCell tdxxcell8 = new PdfPCell(new PdfPCell(new Paragraph("土地使用权面积", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell tdxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.TDSYQMJ, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 4
            };

            PdfPCell tdxxcell10 = new PdfPCell(new PdfPCell(new Paragraph("终止日期", textfont)))
            {
                MinimumHeight = 24f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell tdxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.QLJSSJ, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,

                Colspan = 3
            };
            PdfPCell tdxxcell12 = new PdfPCell(new PdfPCell(new Paragraph("权利性质", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };


            PdfPCell tdxxcell13 = new PdfPCell(new PdfPCell(new Paragraph((string)jObject.GetValue("QLXZ").ToString(), textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 4
            };

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

            PdfPCell fwzkcell = new PdfPCell(new PdfPCell(new Paragraph("房屋状况", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Rowspan = 3
            };

            PdfPCell fwzkcell2 = new PdfPCell(new PdfPCell(new Paragraph("幢号", textfont)))
            {
                MinimumHeight = 24f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell3 = new PdfPCell(new PdfPCell(new Paragraph("房号", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell4 = new PdfPCell(new PdfPCell(new Paragraph("结构", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            PdfPCell fwzkcell5 = new PdfPCell(new PdfPCell(new Paragraph("房屋总层数", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell6 = new PdfPCell(new PdfPCell(new Paragraph("名义层", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell7 = new PdfPCell(new PdfPCell(new Paragraph("建筑面积（㎡）", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell8 = new PdfPCell(new PdfPCell(new Paragraph("规划用途", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            qlrtable.AddCell(fwzkcell);
            qlrtable.AddCell(fwzkcell2);
            qlrtable.AddCell(fwzkcell3);
            qlrtable.AddCell(fwzkcell4);
            qlrtable.AddCell(fwzkcell5);
            qlrtable.AddCell(fwzkcell6);
            qlrtable.AddCell(fwzkcell7);
            qlrtable.AddCell(fwzkcell8);



            PdfPCell fwzkcell21 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.ZRZH, textfont)))
            {
                MinimumHeight = 24f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell31 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.FJH, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell41 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.FWJG, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            PdfPCell fwzkcell51 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.ZCS.ToString(), textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell61 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.MYC, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell71 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.JZMJ, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell fwzkcell81 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.YT, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            qlrtable.AddCell(fwzkcell21);
            qlrtable.AddCell(fwzkcell31);
            qlrtable.AddCell(fwzkcell41);
            qlrtable.AddCell(fwzkcell51);
            qlrtable.AddCell(fwzkcell61);
            qlrtable.AddCell(fwzkcell71);
            qlrtable.AddCell(fwzkcell81);


            PdfPCell fwblank1 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)))
            {
                MinimumHeight = 24f
            };

            PdfPCell fwblank2 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

            PdfPCell fwblank3 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));
            fwblank3.Colspan = 2;

            PdfPCell fwblank4 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

            PdfPCell fwblank5 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

            PdfPCell fwblank6 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));

            PdfPCell fwblank7 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));
            fwblank7.Colspan = 2;

            for (int i = 0; i < 1; i++)
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
            DYItems.Add(new DYItem());

            PdfPCell dyxxcell = new PdfPCell(new PdfPCell(new Paragraph("抵押信息", textfont)));
            dyxxcell.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell.Rowspan = DYItems.Count + 1;

            PdfPCell dyxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("抵押权人", textfont)));
            dyxxcell2.MinimumHeight = 24f;
            dyxxcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell2.Colspan = 2;

            PdfPCell dyxxcell3 = new PdfPCell(new PdfPCell(new Paragraph("登记日期", textfont)));
            dyxxcell3.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell3.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell dyxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("抵押方式", textfont)));
            dyxxcell4.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell4.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell dyxxcell5 = new PdfPCell(new PdfPCell(new Paragraph("不动产证明号", textfont)));
            dyxxcell5.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell5.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell dyxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("被担保主债权数额/最高债权额（万元）", textfont)));
            dyxxcell6.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell6.Colspan = 2;

            PdfPCell dyxxcell7 = new PdfPCell(new PdfPCell(new Paragraph("债务履行期限", textfont)));
            dyxxcell7.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell7.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell7.Colspan = 2;

            qlrtable.AddCell(dyxxcell);
            qlrtable.AddCell(dyxxcell2);
            qlrtable.AddCell(dyxxcell3);
            qlrtable.AddCell(dyxxcell4);
            qlrtable.AddCell(dyxxcell5);
            qlrtable.AddCell(dyxxcell6);
            qlrtable.AddCell(dyxxcell7);


            foreach (DYItem dyItem in DYItems)
            {

                PdfPCell dyxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.DYQR, textfont)));
                dyxxcell8.MinimumHeight = 24f;
                dyxxcell8.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                dyxxcell8.Colspan = 2;

                PdfPCell dyxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.DJSJ, textfont)));
                dyxxcell9.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell9.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell dyxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.DYFS, textfont)));
                dyxxcell10.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell10.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell dyxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.BDCDJZMH, textfont)));
                dyxxcell11.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell11.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell dyxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.ZGZQQDSSHSE == "" ? "" : dyItem.ZGZQQDSSHSE.ToString(), textfont)));
                dyxxcell12.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell12.VerticalAlignment = Element.ALIGN_MIDDLE;
                dyxxcell12.Colspan = 2;

                PdfPCell dyxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.ZQLXQX, textfont)));
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


            //===抵押信息结束==================

            //===查封信息开始==================
            CFItems.Add(new CFItem());

            PdfPCell cfxxcell = new PdfPCell(new PdfPCell(new Paragraph("查封信息", textfont)));
            cfxxcell.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell.Rowspan = CFItems.Count + 1;

            PdfPCell cfxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("查封法院", textfont)));
            cfxxcell2.MinimumHeight = 24f;
            cfxxcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell2.Colspan = 2;

            PdfPCell cfxxcell3 = new PdfPCell(new PdfPCell(new Paragraph("查封文号", textfont)));
            cfxxcell3.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell3.Colspan = 2;

            PdfPCell cfxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("查封日期", textfont)));
            cfxxcell4.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell4.Colspan = 2;

            PdfPCell cfxxcell5 = new PdfPCell(new PdfPCell(new Paragraph("结束期限", textfont)));
            cfxxcell5.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell5.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell cfxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("限制类别", textfont)));
            cfxxcell6.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell6.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell cfxxcell7 = new PdfPCell(new PdfPCell(new Paragraph("类别", textfont)));
            cfxxcell7.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell7.VerticalAlignment = Element.ALIGN_MIDDLE;

            qlrtable.AddCell(cfxxcell);
            qlrtable.AddCell(cfxxcell2);
            qlrtable.AddCell(cfxxcell3);
            qlrtable.AddCell(cfxxcell4);
            qlrtable.AddCell(cfxxcell5);
            qlrtable.AddCell(cfxxcell6);
            qlrtable.AddCell(cfxxcell7);

            foreach (CFItem cFItem in CFItems)
            {
                PdfPCell cfxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFJG, textfont)));
                cfxxcell8.MinimumHeight = 24f;
                cfxxcell8.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                cfxxcell8.Colspan = 2;
                PdfPCell cfxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFWH, textfont)));
                cfxxcell9.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell9.VerticalAlignment = Element.ALIGN_MIDDLE;
                cfxxcell9.Colspan = 2;
                PdfPCell cfxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.DJSJ, textfont)));
                cfxxcell10.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell10.VerticalAlignment = Element.ALIGN_MIDDLE;
                cfxxcell10.Colspan = 2;

                PdfPCell cfxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFJSSJ, textfont)));
                cfxxcell11.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell11.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell cfxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFLX, textfont)));
                cfxxcell12.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell12.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell cfxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.QSZT, textfont)));
                cfxxcell13.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell13.VerticalAlignment = Element.ALIGN_MIDDLE;

                qlrtable.AddCell(cfxxcell8);
                qlrtable.AddCell(cfxxcell9);
                qlrtable.AddCell(cfxxcell10);
                qlrtable.AddCell(cfxxcell11);
                qlrtable.AddCell(cfxxcell12);
                qlrtable.AddCell(cfxxcell13);
            }


            //===查封信息结束==================

            //===预告开始==================

            PdfPCell ygcell = new PdfPCell(new PdfPCell(new Paragraph("预告", textfont)));
            ygcell.MinimumHeight = 24f;
            ygcell.HorizontalAlignment = Element.ALIGN_CENTER;
            ygcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell ygcell2 = new PdfPCell(new PdfPCell(new Paragraph("权利人", textfont)));
            ygcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            ygcell2.VerticalAlignment = Element.ALIGN_MIDDLE;


            string ygqlr = "";//登记资料权利人名称
            foreach (QLRItem item in YGQLRItems)
            {
                ygqlr += item.QLRMC + " ";
            }

            PdfPCell ygcell3 = new PdfPCell(new PdfPCell(new Paragraph(ygqlr, textfont)));
            ygcell3.HorizontalAlignment = Element.ALIGN_CENTER;
            ygcell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            ygcell3.Colspan = 3;

            PdfPCell ygcell4 = new PdfPCell(new PdfPCell(new Paragraph("义务人", textfont)));
            ygcell4.HorizontalAlignment = Element.ALIGN_CENTER;
            ygcell4.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell ygcell5 = new PdfPCell(new PdfPCell(new Paragraph(ygywr, textfont)));
            ygcell5.HorizontalAlignment = Element.ALIGN_CENTER;
            ygcell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            ygcell5.Colspan = 4;

            qlrtable.AddCell(ygcell);
            qlrtable.AddCell(ygcell2);
            qlrtable.AddCell(ygcell3);
            qlrtable.AddCell(ygcell4);
            qlrtable.AddCell(ygcell5);

            //===预告结束==================

            //===权利人其他情况开始==================

            PdfPCell qlrcell = new PdfPCell(new PdfPCell(new Paragraph("权利人其他情况", textfont)));
            qlrcell.HorizontalAlignment = Element.ALIGN_CENTER;
            qlrcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell qlrcell2 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.QLQTZK, textfont)));
            qlrcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            qlrcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            qlrcell2.Colspan = 9;

            qlrtable.AddCell(qlrcell);
            qlrtable.AddCell(qlrcell2);


            PdfPCell fjcell = new PdfPCell(new PdfPCell(new Paragraph("附记", textfont)));
            fjcell.MinimumHeight = 24f;
            fjcell.HorizontalAlignment = Element.ALIGN_CENTER;
            fjcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell fjcell2 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.FJ, textfont)));
            fjcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            fjcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            fjcell2.Colspan = 9;

            qlrtable.AddCell(fjcell);
            qlrtable.AddCell(fjcell2);

            //===附记结束==================

            //===备注开始==================
            //	        JSONArray ygdyxx = ygInfo.getDYXX();
            //	        int ygdyxxSize = ygdyxx.size();

            PdfPCell bzcell = new PdfPCell(new PdfPCell(new Paragraph("备注", textfont)));
            bzcell.MinimumHeight = 24f;
            bzcell.HorizontalAlignment = Element.ALIGN_CENTER;
            bzcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell bzcell2 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.QT, textfont)));
            bzcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            bzcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            bzcell2.Colspan = 9;

            qlrtable.AddCell(bzcell);
            qlrtable.AddCell(bzcell2);
            //===备注结束==================

            //===特别提示开始==================
            //	        JSONArray ygdyxx = ygInfo.getDYXX();
            //	        int ygdyxxSize = ygdyxx.size();

            PdfPCell tbtscell = new PdfPCell(new PdfPCell(new Paragraph("特别提示：" + "\r\n" + "1、请当场核实本查询结果证明，如有异议，请向工作人员或查询窗口提出复核；因隐瞒真实信息或提供虚假信息所产生的一切法律责任，均由查询人自行承担。" + "\r\n" + "2、请妥善保管本人身份证件和查询结果证明，如涉及国家机密、个人隐私或商业秘密，查询人负有保密责任；因保管不当、信息泄露或不正当使用所产生的一切法律责任，均由查询人自行承担。", textfont)));
            // tbtscell.MinimumHeight=70f);
            tbtscell.HorizontalAlignment = Element.ALIGN_LEFT;
            tbtscell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tbtscell.PaddingLeft = 5f;
            tbtscell.PaddingRight = 5f;
            tbtscell.PaddingTop = 5f;
            tbtscell.PaddingBottom = 5f;
            tbtscell.Colspan = 10;

            qlrtable.AddCell(tbtscell);
            //===特别提示结束==================

            //==最后一行开始=======================================================================
            PdfPCell zhcell = new PdfPCell(new PdfPCell(new Paragraph("出具人", headfont)));
            zhcell.MinimumHeight = 24f;
            zhcell.HorizontalAlignment = Element.ALIGN_CENTER;
            zhcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            zhcell.Colspan = 2;

            PdfPCell zhcell2 = new PdfPCell(new PdfPCell(new Paragraph("系统管理员", headfont)));
            zhcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            zhcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            zhcell2.Colspan = 3;

            PdfPCell zhcell3 = new PdfPCell(new PdfPCell(new Paragraph("证明出具单位", headfont)));
            zhcell3.HorizontalAlignment = Element.ALIGN_CENTER;
            zhcell3.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell zhcell4 = new PdfPCell(new PdfPCell(new Paragraph("宜兴市自然资源和规划局（盖章）", headfont)));
            zhcell4.HorizontalAlignment = Element.ALIGN_CENTER;
            zhcell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            zhcell4.Colspan = 4;

            qlrtable.AddCell(zhcell);
            qlrtable.AddCell(zhcell2);
            qlrtable.AddCell(zhcell3);
            qlrtable.AddCell(zhcell4);

            //==最后一行结束========================================================================

            document.Add(qlrtable);


            document.Close();

            return true;
        }

        public static bool DrawYiXing1_Bank(string FilePath, string CQZH)
        {
            HouseBasic houseBasic = new HouseBasic();
            QLRItems = new ObservableCollection<QLRItem>();
            DYItems = new ObservableCollection<DYItem>();
            CFItems = new ObservableCollection<CFItem>();
            YGQLRItems = new ObservableCollection<QLRItem>();
            string ygywr = "";
            string CQZSresponse = Http.YinXingNew.CQZS("", "", "", CQZH);
            try
            {
                JObject CQZSdata = (JObject)JsonConvert.DeserializeObject(CQZSresponse);

                if ((string)CQZSdata.GetValue("code") == "0")
                {

                    JObject jObject = (JObject)CQZSdata["data"][0];
                    if (jObject.GetValue("DYXX").ToString() == "null")
                        return false;

                    houseBasic.BDCDYH = (string)jObject.GetValue("BDCDYH");
                    houseBasic.ZL = (string)jObject.GetValue("ZL");
                    houseBasic.CQZH = (string)jObject.GetValue("CQZH");
                    houseBasic.ZDDM = (string)jObject.GetValue("ZDDM");
                    houseBasic.ZDMJ = (string)jObject.GetValue("ZDMJ");
                    houseBasic.YT = (string)jObject.GetValue("YT");
                    houseBasic.TDYT = (string)jObject.GetValue("TDYT");
                    houseBasic.TDSYQMJ = (string)jObject.GetValue("TDSYQMJ");
                    houseBasic.FJ = (string)jObject.GetValue("FJ");
                    houseBasic.ZCS = (int)jObject.GetValue("ZCS");
                    houseBasic.TDQLXZ = (string)jObject.GetValue("TDQLXZ");
                    houseBasic.FJH = (string)jObject.GetValue("FJH");
                    houseBasic.ZRZH = (string)jObject.GetValue("ZRZH");
                    houseBasic.TDQLXZ = (string)jObject.GetValue("TDQLXZ");
                    houseBasic.MYC = (string)jObject.GetValue("MYC");
                    houseBasic.FWXZ = (string)jObject.GetValue("FWXZ");
                    houseBasic.FWJG = (string)jObject.GetValue("FWJG");
                    houseBasic.JZMJ = (string)jObject.GetValue("JZMJ");
                    houseBasic.QLQTZK = (string)jObject.GetValue("QLQTZK");
                    houseBasic.QLJSSJ = (string)jObject.GetValue("QLJSSJ");
                    houseBasic.QT = (string)jObject.GetValue("QT");

                    try
                    {
                        JArray jObjectQLR = (JArray)jObject["QLR"];

                        foreach (JObject item in jObjectQLR)
                        {
                            QLRItem qLRItem = new QLRItem
                            {
                                QLRMC = (string)item.GetValue("QLRMC"),
                                QLRZJZL = (string)item.GetValue("QLRZJZL"),
                                QLRZJH = (string)item.GetValue("QLRZJH"),
                                GYFS = (string)item.GetValue("GYFS"),
                                QLBL = (string)item.GetValue("QLBL"),
                                CQZH = (string)item.GetValue("CQZH")
                            };

                            QLRItems.Add(qLRItem);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string YGCXresponse = Http.YinXingNew.YGCX(FWDM: houseBasic.FWDM);
                        JObject YGCXdata = (JObject)JsonConvert.DeserializeObject(YGCXresponse);
                        if ((string)YGCXdata.GetValue("code") == "0")
                        {
                            JObject jObject1 = (JObject)YGCXdata["data"][0];
                            ygywr = (string)jObject1.GetValue("YWR");
                            JArray jObjectQLR = (JArray)jObject1["QLR"];

                            foreach (JObject item in jObjectQLR)
                            {
                                QLRItem qLRItem = new QLRItem
                                {
                                    QLRMC = (string)item.GetValue("QLRMC"),
                                    QLRZJZL = (string)item.GetValue("QLRZJZL"),
                                    QLRZJH = (string)item.GetValue("QLRZJH"),
                                    GYFS = (string)item.GetValue("GYFS"),
                                    QLBL = (string)item.GetValue("QLBL"),
                                    CQZH = (string)item.GetValue("CQZH")
                                };

                                YGQLRItems.Add(qLRItem);
                            }
                        }
                    }
                    catch
                    {

                    }

                    try
                    {
                        JArray jObjectDY = (JArray)jObject["DYXX"];
                        if (jObjectDY != null)
                        {
                            foreach (JObject item in jObjectDY)
                            {
                                DYItem DYItem = new DYItem
                                {
                                    FWDM = (string)item.GetValue("FWDM"),
                                    BDCDYH = (string)item.GetValue("BDCDYH"),
                                    BDCDJZMH = (string)item.GetValue("BDCDJZMH"),
                                    DYJE = (string)item.GetValue("DYJE"),
                                    DBFW = (string)item.GetValue("DBFW"),
                                    DYQR = (string)item.GetValue("DYQR"),
                                    DYR = (string)item.GetValue("DYR"),
                                    QT = (string)item.GetValue("QT"),
                                    CQZH = (string)item.GetValue("CQZH"),
                                    DYKSSJ = (string)item.GetValue("DYKSSJ"),
                                    DYJSSJ = (string)item.GetValue("DYJSSJ"),
                                    FJ = (string)item.GetValue("FJ"),
                                    QSZT = (string)item.GetValue("QSZT"),
                                    DJSJ = (string)item.GetValue("DJSJ"),
                                };

                                try
                                {

                                    string DYCXresponse = Http.YinXingNew.DYCX(DYItem.BDCDJZMH);

                                    JObject DYCXdata = (JObject)JsonConvert.DeserializeObject(DYCXresponse);
                                    if ((string)DYCXdata.GetValue("code") == "0")
                                    {
                                        JObject jObject1 = (JObject)DYCXdata["data"];
                                        DYItem.ZQLXQX = (string)jObject1.GetValue("ZQLXQX");
                                        DYItem.DYFS = (string)jObject1.GetValue("DYFS");
                                        DYItem.ZGZQQDSSHSE = jObject1.GetValue("ZGZQQDSSHSE").ToString();
                                        DYItem.FCDYMJ = jObject1.GetValue("FCDYMJ").ToString();
                                        DYItem.TDDYMJ = jObject1.GetValue("TDDYMJ").ToString();

                                    }
                                }
                                catch
                                {

                                }
                             
                                DYItems.Add(DYItem);
                            }
                        }
                    }
                    catch
                    {

                    }

                    try
                    {
                        JArray jObjectCF = (JArray)jObject["CFXX"];
                        foreach (JObject item in jObjectCF)
                        {
                            CFItem CFItem = new CFItem
                            {
                                FWDM = (string)item.GetValue("FWDM"),
                                BDCDYH = (string)item.GetValue("BDCDYH"),
                                CFJG = (string)item.GetValue("CFJG"),
                                CFWH = (string)item.GetValue("CFWH"),
                                CFLX = (string)item.GetValue("CFLX"),
                                CFKSSJ = (string)item.GetValue("CFKSSJ"),
                                CFJSSJ = (string)item.GetValue("CFJSSJ"),
                                DJSJ = (string)item.GetValue("DJSJ"),
                                QSZT = (string)item.GetValue("QSZT"),
                                CQZH = (string)item.GetValue("CQZH"),
                                FJ = (string)item.GetValue("FJ"),
                            };
                            CFItems.Add(CFItem);
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            //打印时间	   
            BaseFont bfChinese = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font headfont = new Font(bfChinese, 12, Font.NORMAL);// 设置字体大小
            Font keyfont = new Font(bfChinese, 21, Font.BOLD);// 设置字体大小
            Font textfont = new Font(bfChinese, 9, Font.NORMAL);// 设置字体大小
            Font textfont2 = new Font(bfChinese, 10, Font.NORMAL);// 设置字体大小
            Font textfont3 = new Font(bfChinese, 13, Font.NORMAL);
            Font textfont4 = new Font(bfChinese, 18, Font.NORMAL);

            Document document = new Document(PageSize.A4, 25, 25, 25, 25);
            Stream s = new FileStream(FilePath, FileMode.Create);
            PdfWriter.GetInstance(document, s);
            document.Open();
            Paragraph pt = new Paragraph("他项权利证明", keyfont)
            {
                Alignment = 1,
            };
            document.Add(pt);
            document.Add(new Paragraph("\n"));
            float[] titlecell = { 0.3f, 0.3f, 0.3f };
            PdfPTable titletable = new PdfPTable(titlecell)
            {
                WidthPercentage = 100
            };
            Paragraph pt3 = new Paragraph("出具时间：" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss") + "\n", textfont)
            {
                Alignment = 2
            };
            PdfPCell cell3 = new PdfPCell(pt3)
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT,
                Colspan = 10
            };
            cell3.DisableBorderSide(15);
            titletable.AddCell(cell3);
            document.Add(titletable);



            string djzlqlrmc = "";//登记资料权利人名称
            string djzlqlrzjh = "";//权利人证件号码
            DYItem d = null;
            foreach (DYItem item in DYItems)
            {
                djzlqlrmc = item.DYR + " ";
                djzlqlrzjh = item.DYQR + " ";
                d = item;
                break;
            }


            float[] qlrwidhts = { 0.03f, 0.17f, 0.1f, 0.1f, 0.1f, 0.18f, 0.13f, 0.1f, 0.1f, 0.05f };
            PdfPTable qlrtable = new PdfPTable(qlrwidhts);
            qlrtable.WidthPercentage = 100; // 宽度100%填充

            //====第一行开始=================
            PdfPCell ycell = new PdfPCell(new PdfPCell(new Paragraph("抵押人", textfont)));
            ycell.MinimumHeight = 30f;
            ycell.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell.Colspan = 2;

            PdfPCell ycell2 = new PdfPCell(new PdfPCell(new Paragraph(djzlqlrmc, textfont)));
            ycell2.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell2.Colspan = 8;


            qlrtable.AddCell(ycell);
            qlrtable.AddCell(ycell2);

            //===第一行结束==================


            PdfPCell ycell3 = new PdfPCell(new PdfPCell(new Paragraph("抵押权人", textfont)));
            ycell3.MinimumHeight = 30f;
            ycell3.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell3.Colspan = 2;

            PdfPCell ycell4 = new PdfPCell(new PdfPCell(new Paragraph(djzlqlrzjh, textfont)));
            ycell4.HorizontalAlignment = Element.ALIGN_CENTER;
            ycell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            ycell4.Colspan = 8;


            qlrtable.AddCell(ycell3);
            qlrtable.AddCell(ycell4);

            //===第二行开始==================


            PdfPCell ecell = new PdfPCell(new PdfPCell(new Paragraph("不动产权证号", textfont)));
            ecell.MinimumHeight = 30f;
            ecell.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell.VerticalAlignment = Element.ALIGN_MIDDLE;
            ecell.Colspan = 2;

            PdfPCell ecell2 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.CQZH, textfont)));
            ecell2.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            ecell2.Colspan = 8;



            qlrtable.AddCell(ecell);
            qlrtable.AddCell(ecell2);

            //===第二行结束==================
            PdfPCell ecell3 = new PdfPCell(new PdfPCell(new Paragraph("他项权证号", textfont)));
            ecell3.MinimumHeight = 30f;
            ecell3.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            ecell3.Colspan = 2;
            PdfPCell ecell4 = new PdfPCell(new PdfPCell(new Paragraph(d.BDCDJZMH, textfont)));
            ecell4.HorizontalAlignment = Element.ALIGN_CENTER;
            ecell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            ecell4.Colspan = 8;

            qlrtable.AddCell(ecell3);
            qlrtable.AddCell(ecell4);
            //===第三行开始==================

            PdfPCell scell = new PdfPCell(new PdfPCell(new Paragraph("抵押方式", textfont)));
            scell.MinimumHeight = 30f;
            scell.HorizontalAlignment = Element.ALIGN_CENTER;
            scell.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell.Colspan = 2;

            PdfPCell scell2 = new PdfPCell(new PdfPCell(new Paragraph(d.DYFS, textfont)));
            scell2.HorizontalAlignment = Element.ALIGN_CENTER;
            scell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell2.Colspan = 3;

            PdfPCell scel3 = new PdfPCell(new PdfPCell(new Paragraph("担保范围", textfont)));
            scel3.MinimumHeight = 30f;
            scel3.HorizontalAlignment = Element.ALIGN_CENTER;
            scel3.VerticalAlignment = Element.ALIGN_MIDDLE;
            scel3.Colspan = 2;

            PdfPCell scell4 = new PdfPCell(new PdfPCell(new Paragraph(d.DBFW, textfont)));
            scell4.HorizontalAlignment = Element.ALIGN_CENTER;
            scell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell4.Colspan = 3;

            qlrtable.AddCell(scell);
            qlrtable.AddCell(scell2); qlrtable.AddCell(scel3);
            qlrtable.AddCell(scell4);


            #region 土地信息

            PdfPCell scell11 = new PdfPCell(new PdfPCell(new Paragraph("债权数额", textfont)));
            scell11.MinimumHeight = 30f;
            scell11.HorizontalAlignment = Element.ALIGN_CENTER;
            scell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell11.Colspan = 2;

            PdfPCell scell22 = new PdfPCell(new PdfPCell(new Paragraph(d.ZGZQQDSSHSE.ToString(), textfont)));
            scell22.HorizontalAlignment = Element.ALIGN_CENTER;
            scell22.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell22.Colspan = 3;

            PdfPCell scel33 = new PdfPCell(new PdfPCell(new Paragraph("不动产价值（万元）", textfont)));
            scel33.MinimumHeight = 30f;
            scel33.HorizontalAlignment = Element.ALIGN_CENTER;
            scel33.VerticalAlignment = Element.ALIGN_MIDDLE;
            scel33.Colspan = 2;

            PdfPCell scell44 = new PdfPCell(new PdfPCell(new Paragraph("", textfont)));
            scell44.HorizontalAlignment = Element.ALIGN_CENTER;
            scell44.VerticalAlignment = Element.ALIGN_MIDDLE;
            scell44.Colspan = 3;

            qlrtable.AddCell(scell11);
            qlrtable.AddCell(scell22); qlrtable.AddCell(scel33);
            qlrtable.AddCell(scell44);

            PdfPCell qishi = new PdfPCell(new PdfPCell(new Paragraph("起始时间", textfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            PdfPCell qishi1 = new PdfPCell(new PdfPCell(new Paragraph(d.DYKSSJ.ToString(), textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 3
            };

            PdfPCell zhongzhi = new PdfPCell(new PdfPCell(new Paragraph("终止时间 ", textfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            PdfPCell zhongzhi1 = new PdfPCell(new PdfPCell(new Paragraph(d.DYJSSJ, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 3
            };

            qlrtable.AddCell(qishi);
            qlrtable.AddCell(qishi1); qlrtable.AddCell(zhongzhi);
            qlrtable.AddCell(zhongzhi1);

            //===房屋状况开始==================


            PdfPCell FCDYMJ = new PdfPCell(new PdfPCell(new Paragraph("房屋抵押面积", textfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            PdfPCell FCDYMJ1 = new PdfPCell(new PdfPCell(new Paragraph(d.FCDYMJ.ToString(), textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 3
            };

            PdfPCell TDDYMJ = new PdfPCell(new PdfPCell(new Paragraph("土地抵押面积 ", textfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            PdfPCell TDDYMJ1 = new PdfPCell(new PdfPCell(new Paragraph(d.TDDYMJ, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 3
            };

            qlrtable.AddCell(FCDYMJ);
            qlrtable.AddCell(FCDYMJ1); qlrtable.AddCell(TDDYMJ);
            qlrtable.AddCell(TDDYMJ1);
            #endregion 



            PdfPCell Zl = new PdfPCell(new PdfPCell(new Paragraph("坐落", textfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };
            PdfPCell Zl1 = new PdfPCell(new PdfPCell(new Paragraph(houseBasic.ZL, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 8
            };

            qlrtable.AddCell(Zl);
            qlrtable.AddCell(Zl1);



            PdfPCell zgzqqdss = new PdfPCell(new PdfPCell(new Paragraph("最高债权确定事实", textfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };
            PdfPCell zgzqqdss1 = new PdfPCell(new PdfPCell(new Paragraph("  ", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 8
            };

            qlrtable.AddCell(zgzqqdss);
            qlrtable.AddCell(zgzqqdss1);

            PdfPCell fg = new PdfPCell(new PdfPCell(new Paragraph("附记", textfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };
            PdfPCell fg1 = new PdfPCell(new PdfPCell(new Paragraph(d.FJ, textfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 8
            };

            qlrtable.AddCell(fg);
            qlrtable.AddCell(fg1);


            #region 查封信息开始
            CFItems.Add(new CFItem());
            PdfPCell cfxxcell = new PdfPCell(new PdfPCell(new Paragraph("查封信息", textfont)));
            cfxxcell.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell.Rowspan = CFItems.Count + 1;

            PdfPCell cfxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("查封法院", textfont)));
            cfxxcell2.MinimumHeight = 30f;
            cfxxcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell2.Colspan = 2;

            PdfPCell cfxxcell3 = new PdfPCell(new PdfPCell(new Paragraph("查封文号", textfont)));
            cfxxcell3.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell3.Colspan = 2;

            PdfPCell cfxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("查封日期", textfont)));
            cfxxcell4.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cfxxcell4.Colspan = 2;

            PdfPCell cfxxcell5 = new PdfPCell(new PdfPCell(new Paragraph("结束期限", textfont)));
            cfxxcell5.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell5.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell cfxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("限制类别", textfont)));
            cfxxcell6.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell6.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell cfxxcell7 = new PdfPCell(new PdfPCell(new Paragraph("类别", textfont)));
            cfxxcell7.HorizontalAlignment = Element.ALIGN_CENTER;
            cfxxcell7.VerticalAlignment = Element.ALIGN_MIDDLE;

            qlrtable.AddCell(cfxxcell);
            qlrtable.AddCell(cfxxcell2);
            qlrtable.AddCell(cfxxcell3);
            qlrtable.AddCell(cfxxcell4);
            qlrtable.AddCell(cfxxcell5);
            qlrtable.AddCell(cfxxcell6);
            qlrtable.AddCell(cfxxcell7);

            foreach (CFItem cFItem in CFItems)
            {
                PdfPCell cfxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFJG, textfont)));
                cfxxcell8.MinimumHeight = 30f;
                cfxxcell8.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                cfxxcell8.Colspan = 2;
                PdfPCell cfxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFWH, textfont)));
                cfxxcell9.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell9.VerticalAlignment = Element.ALIGN_MIDDLE;
                cfxxcell9.Colspan = 2;
                PdfPCell cfxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.DJSJ, textfont)));
                cfxxcell10.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell10.VerticalAlignment = Element.ALIGN_MIDDLE;
                cfxxcell10.Colspan = 2;

                PdfPCell cfxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFJSSJ, textfont)));
                cfxxcell11.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell11.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell cfxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.CFLX, textfont)));
                cfxxcell12.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell12.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell cfxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(cFItem.QSZT, textfont)));
                cfxxcell13.HorizontalAlignment = Element.ALIGN_CENTER;
                cfxxcell13.VerticalAlignment = Element.ALIGN_MIDDLE;

                qlrtable.AddCell(cfxxcell8);
                qlrtable.AddCell(cfxxcell9);
                qlrtable.AddCell(cfxxcell10);
                qlrtable.AddCell(cfxxcell11);
                qlrtable.AddCell(cfxxcell12);
                qlrtable.AddCell(cfxxcell13);
            }

            #endregion

            #region 抵押
            //===抵押信息开始==================
            DYItems.Add(new DYItem());

            PdfPCell dyxxcell = new PdfPCell(new PdfPCell(new Paragraph("抵押信息", textfont)));
            dyxxcell.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell.Rowspan = DYItems.Count + 1;

            PdfPCell dyxxcell2 = new PdfPCell(new PdfPCell(new Paragraph("抵押权人", textfont)));
            dyxxcell2.MinimumHeight = 30f;
            dyxxcell2.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell2.Colspan = 2;

            PdfPCell dyxxcell3 = new PdfPCell(new PdfPCell(new Paragraph("登记日期", textfont)));
            dyxxcell3.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell3.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell dyxxcell4 = new PdfPCell(new PdfPCell(new Paragraph("抵押方式", textfont)));
            dyxxcell4.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell4.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell dyxxcell5 = new PdfPCell(new PdfPCell(new Paragraph("不动产证明号", textfont)));
            dyxxcell5.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell5.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell dyxxcell6 = new PdfPCell(new PdfPCell(new Paragraph("被担保主债权数额/最高债权额（万元）", textfont)));
            dyxxcell6.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell6.Colspan = 2;

            PdfPCell dyxxcell7 = new PdfPCell(new PdfPCell(new Paragraph("债务履行期限", textfont)));
            dyxxcell7.HorizontalAlignment = Element.ALIGN_CENTER;
            dyxxcell7.VerticalAlignment = Element.ALIGN_MIDDLE;
            dyxxcell7.Colspan = 2;

            qlrtable.AddCell(dyxxcell);
            qlrtable.AddCell(dyxxcell2);
            qlrtable.AddCell(dyxxcell3);
            qlrtable.AddCell(dyxxcell4);
            qlrtable.AddCell(dyxxcell5);
            qlrtable.AddCell(dyxxcell6);
            qlrtable.AddCell(dyxxcell7);


            foreach (DYItem dyItem in DYItems)
            {

                PdfPCell dyxxcell8 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.DYQR, textfont)));
                dyxxcell8.MinimumHeight = 30f;
                dyxxcell8.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                dyxxcell8.Colspan = 2;

                PdfPCell dyxxcell9 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.DJSJ, textfont)));
                dyxxcell9.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell9.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell dyxxcell10 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.DYFS, textfont)));
                dyxxcell10.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell10.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell dyxxcell11 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.BDCDJZMH, textfont)));
                dyxxcell11.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell11.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell dyxxcell12 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.ZGZQQDSSHSE == "" ? "" : dyItem.ZGZQQDSSHSE.ToString(), textfont)));
                dyxxcell12.HorizontalAlignment = Element.ALIGN_CENTER;
                dyxxcell12.VerticalAlignment = Element.ALIGN_MIDDLE;
                dyxxcell12.Colspan = 2;

                PdfPCell dyxxcell13 = new PdfPCell(new PdfPCell(new Paragraph(dyItem.ZQLXQX, textfont)));
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
            //===抵押信息结束==================
            #endregion




            //===特别提示开始==================
            PdfPCell tbtscell = new PdfPCell(new PdfPCell(new Paragraph("特别提示：" + "\r\n" + "1、请当场核实本查询结果证明，如有异议，请向工作人员或查询窗口提出复核；因隐瞒真实信息或提供虚假信息所产生的一切法律责任，均由查询人自行承担。" + "\r\n" + "2、请妥善保管本人身份证件和查询结果证明，如涉及国家机密、个人隐私或商业秘密，查询人负有保密责任；因保管不当、信息泄露或不正当使用所产生的一切法律责任，均由查询人自行承担。", textfont)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingLeft = 5f,
                PaddingRight = 5f,
                PaddingTop = 5f,
                PaddingBottom = 5f,
                Colspan = 10
            };

            qlrtable.AddCell(tbtscell);
            //===特别提示结束==================

            //==最后一行开始=======================================================================
            PdfPCell zhcell = new PdfPCell(new PdfPCell(new Paragraph("出具人", headfont)))
            {
                MinimumHeight = 30f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 2
            };

            PdfPCell zhcell2 = new PdfPCell(new PdfPCell(new Paragraph("自助查询", headfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 3
            };

            PdfPCell zhcell3 = new PdfPCell(new PdfPCell(new Paragraph("证明出具单位", headfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            PdfPCell zhcell4 = new PdfPCell(new PdfPCell(new Paragraph("宜兴市自然资源和规划局（盖章）", headfont)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = 4
            };

            qlrtable.AddCell(zhcell);
            qlrtable.AddCell(zhcell2);
            qlrtable.AddCell(zhcell3);
            qlrtable.AddCell(zhcell4);


            document.Add(qlrtable);
            document.Close();
            return true;
        }

    }


    public class HouseBasic
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; } = "";

        /// <summary>
        /// 房屋代码
        /// </summary>
        public string FWDM { get; set; } = "";

        /// <summary>
        /// 产权证号
        /// </summary>
        public string CQZH { get; set; } = "";
        /// <summary>
        /// 不动产类型
        /// </summary>
        public string BDCLX { get; set; } = "";

        /// <summary>
        /// 房屋用途
        /// </summary>
        public string YT { get; set; } = "";


        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; } = "";


        /// <summary>
        /// 房屋性质
        /// </summary>
        public string FWXZ { get; set; } = "";


        /// <summary>
        /// 房屋结构
        /// </summary>
        public string FWJG { get; set; } = "";


        /// <summary>
        /// 实际层
        /// </summary>
        public int SJC { get; set; } = 0;


        /// <summary>
        /// 总层数
        /// </summary>
        public int ZCS { get; set; } = 0;

        /// <summary>
        /// 房屋类型
        /// </summary>
        public string FWLX { get; set; } = "";


        /// <summary>
        /// 名义层
        /// </summary>
        public string MYC { get; set; } = "";


        /// <summary>
        /// 房间号
        /// </summary>
        public string FJH { get; set; } = "";


        /// <summary>
        /// 建筑面积
        /// </summary>
        public string JZMJ { get; set; } = "";


        /// <summary>
        /// 套内面积
        /// </summary>
        public string TNMJ { get; set; } = "";


        /// <summary>
        /// 分摊面积
        /// </summary>
        public string FTMJ { get; set; } = "";


        /// <summary>
        /// 登记时间
        /// </summary>
        public string DJSJ { get; set; } = "";


        /// <summary>
        /// 发证时间
        /// </summary>
        public string FCZFZSJ { get; set; } = "";


        /// <summary>
        /// 权利类型
        /// </summary>
        public string TDQLLX { get; set; } = "";


        /// <summary>
        /// 权利起始时间
        /// </summary>
        public string QLQSSJ { get; set; } = "";


        /// <summary>
        /// 权利结束时间
        /// </summary>
        public string QLJSSJ { get; set; } = "";


        /// <summary>
        /// 权利性质
        /// </summary>
        public string TDQLXZ { get; set; } = "";


        /// <summary>
        /// 土地用途
        /// </summary>
        public string TDYT { get; set; } = "";


        /// <summary>
        /// 附记
        /// </summary>
        public string FJ { get; set; } = "";


        /// <summary>
        /// 土地使用权面积
        /// </summary>
        public string TDSYQMJ { get; set; } = "";


        /// <summary>
        /// 分摊土地面积
        /// </summary>
        public string TDFTMJ { get; set; } = "";


        /// <summary>
        /// 独用土地面积
        /// </summary>
        public string TDDYMJ { get; set; } = "";


        /// <summary>
        /// 权利类型
        /// </summary>
        public string QLLX { get; set; } = "";


        /// <summary>
        /// 登记类型
        /// </summary>
        public string DJLX { get; set; } = "";


        /// <summary>
        /// 业务类型
        /// </summary>
        public string YWLX { get; set; } = "";

        /// <summary>
        /// 自然栋号
        /// </summary>
        public string ZRZH { get; set; } = "";

        /// <summary>
        /// 建筑年代
        /// </summary>
        public string JZND { get; set; } = "";

        /// <summary>
        /// 宗地面积
        /// </summary>
        public string ZDMJ { get; set; } = "";


        /// <summary>
        /// 宗地代码
        /// </summary>
        public string ZDDM { get; set; } = "";

        /// <summary>
        /// 权利其他状况
        /// </summary>
        public string QLQTZK { get; set; } = "";


        /// <summary>
        /// 土地证号
        /// </summary>
        public string TDZH { get; set; } = "";

        /// <summary>
        /// 其他    
        /// </summary>
        public string QT { get; set; } = "";

    }

    /// <summary>
    /// 权利人
    /// </summary>    
    public class QLRItem : HouseBasic
    {

        /// <summary>
        /// 产权人
        /// </summary>
        public string QLRMC { get; set; } = "";

        /// <summary>
        /// 产权人证件种类
        /// </summary>
        public string QLRZJZL { get; set; } = "";


        /// <summary>
        /// 产权证证件号码
        /// </summary>
        public string QLRZJH { get; set; } = "";

        /// <summary>
        /// 共有方式
        /// </summary>
        public string GYFS { get; set; } = "";

        /// <summary>
        /// 共有比例    
        /// </summary>
        public string QLBL { get; set; } = "";




    }
    //抵押
    public class DYItem : HouseBasic
    {
        /// <summary>
        /// 不动产登记证明号
        /// </summary>
        public string BDCDJZMH { get; set; } = "";

        /// <summary>
        /// 抵押权人
        /// </summary>
        public string DYQR { get; set; } = "";

        /// <summary>
        /// 抵押金额
        /// </summary>
        public string DYJE { get; set; } = "";


        /// <summary>
        /// 抵押开始时间
        /// </summary>
        public string DYKSSJ { get; set; } = "";

        /// <summary>
        /// 抵押结束时间
        /// </summary>
        public string DYJSSJ { get; set; } = "";

        /// <summary>
        /// 权属状态    
        /// </summary>
        public string QSZT { get; set; } = "";


        /// <summary>
        /// 抵押方式
        /// </summary>
        public string DYFS { get; set; } = "";

        /// <summary>
        /// 担保范围
        /// </summary>
        public string DBFW { get; set; } = "";

        /// <summary>
        /// 抵押人
        /// </summary>
        public string DYR { get; set; } = "";


        /// <summary>
        /// 债务履行期限/债权确定期间
        /// </summary>
        public string ZQLXQX { get; set; } = "";


        /// <summary>
        /// 房产抵押面积
        /// </summary>
        public string FCDYMJ { get; set; } = "";

        /// <summary>
        /// 债权数额
        /// </summary>
        public string ZGZQQDSSHSE { get; set; } = "";


        /// <summary>
        ///土地抵押面积
        /// </summary>
        public string TDDYMJ { get; set; } = "";

    }

    //查封
    public class CFItem : HouseBasic
    {
        /// <summary>
        /// 查封机构
        /// </summary>
        public string CFJG { get; set; } = "";

        /// <summary>
        /// 查封类型
        /// </summary>
        public string CFLX { get; set; } = "";

        /// <summary>
        /// 查封文号
        /// </summary>
        public string CFWH { get; set; } = "";


        /// <summary>
        /// 查封开始时间
        /// </summary>
        public string CFKSSJ { get; set; } = "";

        /// <summary>
        /// 查封结束时间
        /// </summary>
        public string CFJSSJ { get; set; } = "";


        /// <summary>
        /// 权属状态
        /// </summary>
        public string QSZT { get; set; } = "";
    }



}
