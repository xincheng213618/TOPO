using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EXC
{
    public static class Http
    {
       public static readonly HttpClient httpclient ;
        static Http()
        {
            httpclient = new HttpClient();
        }
        public class Credit
        {
            //青岛
            public static string QingDaoLegalPerson(string companyName = "", string tyxydm = "")
            {
                if (companyName == null && tyxydm == null)
                    return null;
                string url = Global.configData.QingDaoLegalPersonUrl;
                if (url == null)
                    url = "http://192.200.200.30:8080/WinHall-QinDao/frreport/frbg";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyName", companyName },
                    { "tyxydm", tyxydm }
                };

                string response = Requests.Get(url, dic);
                return response;

            }
        }

        /// <summary>
        /// 同袍查询
        /// </summary>   
        public class TOPO
        {
            public static readonly string RequestUser = "Das4D24fGh";
            public static readonly string RequestPassword = "e10adc3949ba59abbe56e057f20f883e";

            //公司列表
            public static string CompanySearch(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/list";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "name", name },
                    { "pageNo", pageNo }
                };

                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //基本信息
            public static string CompanyBasicInfo(string companyName)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/baseinfo";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", companyName }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //软件注册权
            public static string SoftwareCopyRightUrl(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/zzqlist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "name", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }

            public static string Trademark(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/sblist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }

            public static string Referee(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/cpwslist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }

            public static string Announcement(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/fygglist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;

            }
            //专利信息
            public static string PatentInfo(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/zhuanlilist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "name", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //股份信息
            public static string Partners(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/gdlist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //投资信息
            public static string Investment(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/dwtz";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //分支机构
            public static string Branch(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/fzjg";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //经营异常名录
            public static string BusinessAbnormalion(string name, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/ycjy";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }

            public static string Punish(string CompanyName, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzcflist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", CompanyName },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                return response;
            }

            public static string PunishDetail(string ID)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzcfdetail";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "id", ID },
                };
                string response = Requests.Get(url, dic);
                return response;
            }


            public static string Allow(string CompanyName, int pageNo = 1)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzxklist";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", CompanyName },
                    { "pageNo", pageNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            public static string AllowDetail(string ID)
            {
                string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzxkdetail";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "id", ID },
                };
                string response = Requests.Get(url, dic);
                return response;
            }
        }


        public class YiXing
        {
            public static string GetPersonList(IDCardData iDCardData, string s)
            {

                string url = Global.configData.YiXingListUrl;
                //string url = "http://192.200.200.87:8084/RealEstateYXGYBDC/app/yxglz/fclist.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"QLRMC", iDCardData.Name},
                    {"QLRZJH",iDCardData.IDCardNo},
                };
                string response = Requests.Get(url, dic: dic);
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3060号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-19 11:11:11\",\"FCZFZSJ\":\"2020-08-19 11:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-19 11:11:11\",\"QLJSSJ\":\"2020-08-19 11:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]},{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-19 11:11:11\",\"FCZFZSJ\":\"2020-08-19 11:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-19 11:11:11\",\"QLJSSJ\":\"2020-08-19 11:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                return response;
            }


            public static string GetPersonPDF(IDCardData iDCardData, string CQZH)
            {
                string url = Global.configData.YiXingPersonUrl;
                //string url = "http://192.200.200.87:8084/RealEstateYXGYBDC/app/yxglz/bdcdjbzm.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"QLRMC", iDCardData.Name},
                    {"QLRZJH",iDCardData.IDCardNo},
                    {"CQZH", CQZH}
                };
                string response = Requests.Get(url, dic: dic);
                //string response = "{\"code\":\"0\",\"filename\":\"481059616877215744.pdf\",\"pageNumber\":1,\"Message\":\"查询成功\",\"report\":\"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\\r\\nGFXOowQjr1hCl/Yd7bVqWROqu+/owcDAWQ+FZHQ9NGfYyne0YGbmo+XMuYC5SNO/RauUhNLB3CS0\\r\\njBY7A4S0PDqm8VKjmD0LOThnF8ceQzhw7pcS4+Dt80FTla2FldIexhbw7qA5SI5B47HVUiU96pZa\\r\\nKm1dj3eWyFLDGhjfn2QPttONgkPu2cXxjjsMZe/aI2l6ZKs79xt299pTyPEvD5KoLFUPESQHiQ5S\\r\\nlfR/Bkk8RHdYkLjeYZAccukglb1zBqlwBw9SUaBYaZW/vIRUsSBf0lsHEacie7kaVNZH4nYNosYw\\r\\nSksenz/+NW/w9fosW/kufi61KwDCTZ4Zu4JZkyr2pcAYl5daB12sXDePymvDSHVweJivDaAWA/M8\\r\\nyW2UMF2cSecQTOUjntxBMFnssW8imJDOcy6HGrtpthSoxlxyZ/Y6Ris65qVTw/J+xHo+mQY672Kb\\r\\nBZ03UZXFYT/WPu+RxaWLssO7q2ZZlF+UzfwTM6t8Ad2wSoXIvDP59SNiW8G+q6R3dmHssaO3F4bz\\r\\n305tbVmE0WNBT2d9sjk+/EI08VZ70PzUCk6Ub5gN0EZ67PCv2+mpyV2dd+B1Z731zefX6mmarLZ+\\r\\nmvWZlmdWmp8dvd24dzidfSHa+M5znyxfmv/DfP/EW/VPp17K+o7err/+m5+NHrDvnkyunu6fW0q8\\r\\nsd0zH012Ln4v+S90On7gNCu7Vsyi/Fdf5z+/8Gx2LrsZ7Co3KyaUx42Cw386tFS/u3ilPZis/u7t\\r\\n5CtzW916taLm+CvND577auJ6e3jiVtCT/CBZXb60cCPrO/R0smfh44lb0Cg3e7EfhNfvTtyyfoIy\\r\\n0zlsECebQY+RM/P+Cs/64IMXj042y54V09lesBG6MYHlqymz22Sb1a74uV7jXmMeLQBxbI6eVYJz\\r\\nG1xtasbK9N88VP8JUspgTsYCoeW/UTT3Hhf3//JS61Vza5KmT8LpcO7OmU82T4f/A6ryDBQKZW5k\\r\\nc3RyZWFtCmVuZG9iago2IDAgb2JqCjw8L0NvbnRlbnRzIDQgMCBSL1R5cGUvUGFnZS9SZXNvdXJj\\r\\nZXM8PC9Gb250PDwvRjEgMiAwIFIvRjIgMyAwIFI+Pj4+L1BhcmVudCA1IDAgUi9NZWRpYUJveFsw\\r\\nIDAgNTk1IDg0Ml0+PgplbmRvYmoKNyAwIG9iago8PC9EZXNjZW50IC0xMjAvQ2FwSGVpZ2h0IDg4\\r\\nMC9TdGVtViA5My9UeXBlL0ZvbnREZXNjcmlwdG9yL0ZsYWdzIDYvU3R5bGU8PC9QYW5vc2UoAQUC\\r\\nAgQAAAAAAAAAKT4+L0ZvbnRCQm94IFstMjUgLTI1NCAxMDAwIDg4MF0vRm9udE5hbWUvU1RTb25n\\r\\nLUxpZ2h0L0l0YWxpY0FuZ2xlIDAvQXNjZW50IDg4MD4+CmVuZG9iago4IDAgb2JqCjw8L0RXIDEw\\r\\nMDAvU3VidHlwZS9DSURGb250VHlwZTAvQ0lEU3lzdGVtSW5mbzw8L1N1cHBsZW1lbnQgNC9SZWdp\\r\\nc3RyeShBZG9iZSkvT3JkZXJpbmcoR0IxKT4+L1R5cGUvRm9udC9CYXNlRm9udC9TVFNvbmctTGln\\r\\naHQvRm9udERlc2NyaXB0b3IgNyAwIFIvVyBbMVsyMDddOSAxMCAzNzQgMTNbMjM4IDM3NV0xNlsz\\r\\nMzRdMTcgMjYgNDYyIDI3WzIzOF02N1s1MDNdOTFbNDA3XV0+PgplbmRvYmoKMiAwIG9iago8PC9T\\r\\ndWJ0eXBlL1R5cGUwL1R5cGUvRm9udC9CYXNlRm9udC9TVFNvbmctTGlnaHQtVW5pR0ItVUNTMi1I\\r\\nL0VuY29kaW5nL1VuaUdCLVVDUzItSC9EZXNjZW5kYW50Rm9udHNbOCAwIFJdPj4KZW5kb2JqCjMg\\r\\nMCBvYmoKPDwvU3VidHlwZS9UeXBlMS9UeXBlL0ZvbnQvQmFzZUZvbnQvSGVsdmV0aWNhL0VuY29k\\r\\naW5nL1dpbkFuc2lFbmNvZGluZz4+CmVuZG9iagoxIDAgb2JqCjw8L1N1YnR5cGUvRm9ybS9GaWx0\\r\\nZXIvRmxhdGVEZWNvZGUvVHlwZS9YT2JqZWN0L01hdHJpeCBbMSAwIDAgMSAwIDBdL0Zvcm1UeXBl\\r\\nIDEvUmVzb3VyY2VzPDw+Pi9CQm94WzAgMCAxMDAgMTAwXS9MZW5ndGggOD4+c3RyZWFtCnicAwAA\\r\\nAAABCmVuZHN0cmVhbQplbmRvYmoKNSAwIG9iago8PC9LaWRzWzYgMCBSXS9UeXBlL1BhZ2VzL0Nv\\r\\ndW50IDE+PgplbmRvYmoKOSAwIG9iago8PC9UeXBlL0NhdGFsb2cvUGFnZXMgNSAwIFI+PgplbmRv\\r\\nYmoKMTAgMCBvYmoKPDwvTW9kRGF0ZShEOjIwMjAwODIwMTExOTEwKzA4JzAwJykvQ3JlYXRpb25E\\r\\nYXRlKEQ6MjAyMDA4MjAxMTE5MTArMDgnMDAnKS9Qcm9kdWNlcihpVGV4dK4gNS41LjUgqTIwMDAt\\r\\nMjAxNCBpVGV4dCBHcm91cCBOViBcKEFHUEwtdmVyc2lvblwpKT4+CmVuZG9iagp4cmVmCjAgMTEK\\r\\nMDAwMDAwMDAwMCA2NTUzNSBmIAowMDAwMDAzMjIxIDAwMDAwIG4gCjAwMDAwMDMwMDkgMDAwMDAg\\r\\nbiAKMDAwMDAwMzEzMyAwMDAwMCBuIAowMDAwMDAwMDE1IDAwMDAwIG4gCjAwMDAwMDMzODQgMDAw\\r\\nMDAgbiAKMDAwMDAwMjQ2NSAwMDAwMCBuIAowMDAwMDAyNTg2IDAwMDAwIG4gCjAwMDAwMDI3Nzcg\\r\\nMDAwMDAgbiAKMDAwMDAwMzQzNSAwMDAwMCBuIAowMDAwMDAzNDgwIDAwMDAwIG4gCnRyYWlsZXIK\\r\\nPDwvSW5mbyAxMCAwIFIvSUQgWzw3NzFiY2U3ZDNkNzE1ZDczMWE2NDU1NmFhYjdjZmMzOD48Nzcx\\r\\nYmNlN2QzZDcxNWQ3MzFhNjQ1NTZhYWI3Y2ZjMzg+XS9Sb290IDkgMCBSL1NpemUgMTE+PgolaVRl\\r\\neHQtNS41LjUKc3RhcnR4cmVmCjM2MzgKJSVFT0YK\"}";
                return response;
            }
            public static string GetBankPDF(string Name, string IDCardNo, string CQZH)
            {

                string url = Global.configData.YiXingBankUrl;
                //string url = "http://192.200.200.87:8084/RealEstateYXGYBDC/app/yxglz/txqlzm.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"QLRMC", Name},
                    {"QLRZJH",IDCardNo},
                    {"CQZH", CQZH}
                };
                string response = Requests.Get(url, dic: dic);
                //string response = "{\"code\":\"0\",\"filename\":\"481066342049415168.pdf\",\"pageNumber\":0,\"Message\":\"查询成功\",\"report\":\"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\\r\\nAb9UtsDXlD8ZK82Upg5tnrw/eH7yNIL7bjUHlHFVvBsXSF2zl1A44Cfvl8ZH5wcu1nqTZBBLbGwN\\r\\n1eMFrCK1ntJ4er15zBso4qOTu/rHq4v1q0P7457SD+7G1S0Daysd6E18bWnryX9P/XHi8sSmykvk\\r\\nVlVNQjsWvUV8//6+F5e2DlwqTQxufOfvo/7M7sMfVd4/9335sSR5mh4pIcIZcVbSNXnzrdsrd7b/\\r\\nAZiPgtAKZW5kc3RyZWFtCmVuZG9iago1IDAgb2JqCjw8L0NvbnRlbnRzIDMgMCBSL1R5cGUvUGFn\\r\\nZS9SZXNvdXJjZXM8PC9Gb250PDwvRjEgMSAwIFIvRjIgMiAwIFI+Pj4+L1BhcmVudCA0IDAgUi9N\\r\\nZWRpYUJveFswIDAgNTk1IDg0Ml0+PgplbmRvYmoKNiAwIG9iago8PC9EZXNjZW50IC0xMjAvQ2Fw\\r\\nSGVpZ2h0IDg4MC9TdGVtViA5My9UeXBlL0ZvbnREZXNjcmlwdG9yL0ZsYWdzIDYvU3R5bGU8PC9Q\\r\\nYW5vc2UoAQUCAgQAAAAAAAAAKT4+L0ZvbnRCQm94IFstMjUgLTI1NCAxMDAwIDg4MF0vRm9udE5h\\r\\nbWUvU1RTb25nLUxpZ2h0L0l0YWxpY0FuZ2xlIDAvQXNjZW50IDg4MD4+CmVuZG9iago3IDAgb2Jq\\r\\nCjw8L0RXIDEwMDAvU3VidHlwZS9DSURGb250VHlwZTAvQ0lEU3lzdGVtSW5mbzw8L1N1cHBsZW1l\\r\\nbnQgNC9SZWdpc3RyeShBZG9iZSkvT3JkZXJpbmcoR0IxKT4+L1R5cGUvRm9udC9CYXNlRm9udC9T\\r\\nVFNvbmctTGlnaHQvRm9udERlc2NyaXB0b3IgNiAwIFIvVyBbMVsyMDddOSAxMCAzNzQgMTRbMzc1\\r\\nIDIzOF0xNyAyMyA0NjIgMjUgMjYgNDYyIDI3WzIzOF04OVs0NjZdXT4+CmVuZG9iagoxIDAgb2Jq\\r\\nCjw8L1N1YnR5cGUvVHlwZTAvVHlwZS9Gb250L0Jhc2VGb250L1NUU29uZy1MaWdodC1VbmlHQi1V\\r\\nQ1MyLUgvRW5jb2RpbmcvVW5pR0ItVUNTMi1IL0Rlc2NlbmRhbnRGb250c1s3IDAgUl0+PgplbmRv\\r\\nYmoKMiAwIG9iago8PC9TdWJ0eXBlL1R5cGUxL1R5cGUvRm9udC9CYXNlRm9udC9IZWx2ZXRpY2Ev\\r\\nRW5jb2RpbmcvV2luQW5zaUVuY29kaW5nPj4KZW5kb2JqCjQgMCBvYmoKPDwvS2lkc1s1IDAgUl0v\\r\\nVHlwZS9QYWdlcy9Db3VudCAxPj4KZW5kb2JqCjggMCBvYmoKPDwvVHlwZS9DYXRhbG9nL1BhZ2Vz\\r\\nIDQgMCBSPj4KZW5kb2JqCjkgMCBvYmoKPDwvTW9kRGF0ZShEOjIwMjAwODIwMTE0NTUzKzA4JzAw\\r\\nJykvQ3JlYXRpb25EYXRlKEQ6MjAyMDA4MjAxMTQ1NTMrMDgnMDAnKS9Qcm9kdWNlcihpVGV4dK4g\\r\\nNS41LjUgqTIwMDAtMjAxNCBpVGV4dCBHcm91cCBOViBcKEFHUEwtdmVyc2lvblwpKT4+CmVuZG9i\\r\\nagp4cmVmCjAgMTAKMDAwMDAwMDAwMCA2NTUzNSBmIAowMDAwMDAyNDQ0IDAwMDAwIG4gCjAwMDAw\\r\\nMDI1NjggMDAwMDAgbiAKMDAwMDAwMDAxNSAwMDAwMCBuIAowMDAwMDAyNjU2IDAwMDAwIG4gCjAw\\r\\nMDAwMDE5MDQgMDAwMDAgbiAKMDAwMDAwMjAyNSAwMDAwMCBuIAowMDAwMDAyMjE2IDAwMDAwIG4g\\r\\nCjAwMDAwMDI3MDcgMDAwMDAgbiAKMDAwMDAwMjc1MiAwMDAwMCBuIAp0cmFpbGVyCjw8L0luZm8g\\r\\nOSAwIFIvSUQgWzw5ZGNlZTg3YmM2YThjN2I3MGRlMDRmMTU5MTQwYmUzNT48OWRjZWU4N2JjNmE4\\r\\nYzdiNzBkZTA0ZjE1OTE0MGJlMzU+XS9Sb290IDggMCBSL1NpemUgMTA+PgolaVRleHQtNS41LjUK\\r\\nc3RhcnR4cmVmCjI5MDkKJSVFT0YK\"}";
                return response;
            }

            public static HttpWebResponse CreatePostHttpResponse(string url, JObject parameters)
            {
                HttpWebRequest request = null;
                //如果是发送HTTPS请求 
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    //request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8;";
                //发送POST数据 
                if (!(parameters == null || parameters.Count == 0))
                {
                    byte[] data = Encoding.UTF8.GetBytes(parameters.ToString());

                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                string[] values = request.Headers.GetValues("Content-Type");
                return request.GetResponse() as HttpWebResponse;
            }

            /// <summary>
            /// 获取请求的数据
            /// </summary>
            public static string GetResponseString(HttpWebResponse webresponse)
            {
                using (Stream s = webresponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(s, Encoding.UTF8);
                    return reader.ReadToEnd();

                }
            }


            /// <summary>
            /// 宜兴新的接口 2020.12.25
            /// </summary>
            /// <param name="CQZH">房产证号</param>
            /// <returns></returns>
            public static string[] NewGetBankPDF(string CQZH)
            {
                //**********************************
                //根据产权证号查询   银行用   接口是 5.4	产权证书查询接口
                Log.Write("Yixing_139.140.150.2:1100/service-core/extQuery/cqzs_Start //根据产权证号查询   银行用   接口是 5.4	产权证书查询接口");
                string url1 = "http://139.140.150.2:1100/service-core/extQuery/cqzs";
                JObject job1 = new JObject()
                 {
                     {"FWDM", ""},
                     {"BDCDYH",""},
                     {"ZL",""},
                     {"CQZH", CQZH}
                 };
                string response0 = GetResponseString(CreatePostHttpResponse(url1, job1));
                Log.Write("Yixing_139.140.150.2:1100/service-core/extQuery/cqzs_End  //根据产权证号查询   银行用   接口是 5.4	产权证书查询接口");

                //**********************************
                // 不动产登记号 
                string BDCZNH = null;
                //房屋代码
                string FWDM = null;
                //+++++++++++++++++++++++++++++++++++
                // BDCDJZMH  不动产登记证明号 ，查抵押要用，从response0（产权证书查询接口）里面取出
                //5.10抵押查询（新增需求）
                Log.Write("Yixing_139.140.150.2:9999/out/isaip/dycx_yx_Start //查抵押");
                JObject djzldata = (JObject)JsonConvert.DeserializeObject(response0);
                string dyxx = djzldata["data"][0]["DYXX"].ToString(); ;
                if (dyxx.Length>10)
                {
                    BDCZNH = djzldata["data"][0]["DYXX"][0]["BDCDJZMH"].ToString();
                    JObject job2 = new JObject()
                    {
                     {"BDCZMH",BDCZNH}
                    };
                    string url2 = "http://139.140.150.2:9999/out/isaip/dycx_yx";

                    dyxx = GetResponseString(CreatePostHttpResponse(url2, job2));
                }
                else
                {
                    dyxx = "{\"code\":0,\"msg\":\"查询成功\",\"data\":\"null\"}";
                }
                Log.Write("Yixing_139.140.150.2:9999/out/isaip/dycx_yx_End //查抵押");
                FWDM = djzldata["data"][0]["FWDM"].ToString(); //下面要用，在这取出来



                //FWDM
                //查预告信息 5.12   预告信息查询（新增需求）
                Log.Write("Yixing_139.140.150.2:9999/out/isaip/ygcx_Start //查预告信息");
                string ygxx = "";
                JObject job3 = new JObject()
                 {
                     {"FWDM",FWDM}
                 };
                    string url3 = "http://139.140.150.2:9999/out/isaip/ygcx";
                 ygxx = GetResponseString(CreatePostHttpResponse(url3, job3));
                Log.Write("Yixing_139.140.150.2:9999/out/isaip/ygcx_End //查预告信息");

                //---------------------------------------------
                //5.1	登记资料查询接口
                //这个接口的数据银行不用，个人需要，所以根据刷身份证时赋的值来查，但是银行调用这个方法也会把这个数据传过去，但是没关系，绘制的时候没做处理
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3060号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-19 11:11:11\",\"FCZFZSJ\":\"2020-08-19 11:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-19 11:11:11\",\"QLJSSJ\":\"2020-08-19 11:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]},{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-19 11:11:11\",\"FCZFZSJ\":\"2020-08-19 11:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-19 11:11:11\",\"QLJSSJ\":\"2020-08-19 11:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"产权证证件号码\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-19 11:11:11\",\"DYJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-19 11:11:11\",\"CFJSSJ\":\"2020-08-19 11:11:11\",\"DJSJ\":\"2020-08-19 11:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                //绘制PDF的方法只用data数据，所以替它取出来
                // string responsea = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"A0031187\",\"YWLX\":null,\"FJ\":null,\"FJH\":\"308\",\"TDSYQMJ\":19.6,\"YYXX\":null,\"FTMJ\":0,\"ZCS\":\"6\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":null,\"SJC\":null,\"BDCDYH\":\"320282999999GB99999F00232555\",\"JZMJ\":118,\"MYC\":\"3\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"A0031187\",\"QLRMC\":\"孙勤峰\",\"GYFS\":null,\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"320223197906097374\"}],\"TDDYMJ\":0,\"QLQSSJ\":null,\"ZDDM\":\"320282999999GB99999\",\"YT\":\"成套住宅\",\"QLQTZK\":null,\"JZND\":null,\"YWH\":\"FC528371\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":19.6,\"FWJG\":\"混合结构\",\"TNMJ\":0,\"QLJSSJ\":\"2050-04-09 00:00:00\",\"ZL\":\"华益护卡膜厂综合楼308室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":null,\"ZSZBID\":\"1213488563\",\"DYXX\":null,\"FCZFZSJ\":\"1899-01-01 00:00:00\",\"QLXZ\":\"出让\",\"FWDM\":\"A0031187\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"1900-01-01 00:00:00\"}]}";
                return new string[] { response0, dyxx, ygxx };
                //  return new string[] { responsea, "{\"code\":0,\"msg\":\"查询成功\",\"data\":\"null\"}", "{\"code\":0,\"msg\":\"查询成功\",\"data\":\"null\"}" };
            }
        }


        // 新接口直接走第三方数据，就接口走后台数据
        public class YinXingNew
        {
            //5.1 登记资料查询接口
            public static string DJZL(string Name,string IDCardNo)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-core/extQuery/djzl";
                JObject @object = new JObject()
                {
                    { "QLRMC", Name},
                    { "QLRZJH",IDCardNo},
                };
                 string response = Requests.Post(url, json: @object);
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"A0031187\",\"YWLX\":null,\"FJ\":null,\"FJH\":\"308\",\"TDSYQMJ\":19.6,\"YYXX\":null,\"FTMJ\":0,\"ZCS\":\"6\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":null,\"SJC\":null,\"BDCDYH\":\"320282999999GB99999F00232555\",\"JZMJ\":118,\"MYC\":\"3\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"A0031187\",\"QLRMC\":\"孙勤峰\",\"GYFS\":null,\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"320223197906097374\"}],\"TDDYMJ\":0,\"QLQSSJ\":null,\"ZDDM\":\"320282999999GB99999\",\"YT\":\"成套住宅\",\"QLQTZK\":null,\"JZND\":null,\"YWH\":\"FC528371\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":19.6,\"FWJG\":\"混合结构\",\"TNMJ\":0,\"QLJSSJ\":\"2050-04-09 00:00:00\",\"ZL\":\"华益护卡膜厂综合楼308室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":null,\"ZSZBID\":\"1213488563\",\"DYXX\":null,\"FCZFZSJ\":\"1899-01-01 00:00:00\",\"QLXZ\":\"出让\",\"FWDM\":\"A0031187\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"1900-01-01 00:00:00\"}]}";
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"YWLX\":\"存量商品房转移加抵押（不动产证）\",\"FJ\":\"换证。\",\"FJH\":\"202\",\"TDSYQMJ\":48.5,\"YYXX\":null,\"FTMJ\":13.56,\"ZCS\":\"5\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"0027\",\"SJC\":\"2\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"JZMJ\":150.84,\"MYC\":\"2\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"谢丹丹\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"340322198702048463\"},{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"沈华兵\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"342423198910188616\"}],\"TDDYMJ\":null,\"QLQSSJ\":null,\"ZDDM\":\"320282100003GB00054\",\"YT\":\"成套住宅\",\"QLQTZK\":\"分摊土地使用权面积：48.50㎡\\r\\n房屋结构：混合结构\\r\\n专有建筑面积：137.28㎡，分摊建筑面积：13.56㎡\\r\\n房屋总层数：5层，所在层数：2层\\r\\n\",\"JZND\":null,\"YWH\":\"201910180220\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":48.5,\"ZDMJ\":null,\"FWJG\":\"混合结构\",\"TNMJ\":137.28,\"QLJSSJ\":\"2073-03-14 00:00:00\",\"ZL\":\"宜城街道祥和花园27幢72号202室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"存量商品房转移加抵押（不动产证）\",\"DYXX\":[{\"QT\":null,\"CQZH\":\"1000143872\",\"FJ\":null,\"BDCDJZMH\":\"1000103140\",\"DYKSSJ\":\"2015-12-09 00:00:00\",\"DYJSSJ\":\"2045-12-09 00:00:00\",\"DYFSMC\":null,\"YWH\":\"DYFC338013\",\"BDCDYH\":\"320282100024GB00071F00391401\",\"DYJE\":120,\"DBFW\":null,\"DYQR\":\"无锡市住房置业担保有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"100010224196\",\"DYR\":\"钱晶香,张力\",\"DJSJ\":\"2015-12-28 13:53:05\"},{\"QT\":\"不动产权证号：苏（2019）宜兴不动产权第0027675号\\n被担保主债权数额/最高债权数额：人民币132.9051万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-12~2031-01-06\\n房屋抵押面积：150.84㎡\\n土地抵押面积：48.5㎡\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210001098\",\"DYKSSJ\":\"2021-01-12 00:00:00\",\"DYJSSJ\":\"2031-01-06 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021011200256\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":132.91,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2021-01-12 14:01:24\"}],\"FCZFZSJ\":\"2019-10-22 09:53:42\",\"QLXZ\":\"出让\",\"TDZH\":null,\"FWDM\":\"A0061276\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"2019-10-21 09:04:09\"}]}";
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人2\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人1\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";

                return response;
            }

            //5.2 办件进度
            //业务号查询
            public static string BJJD(string YWH)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-bizz/extQuery/bjjd";
                JObject @object = new JObject()
                {
                    { "YWH ", YWH},
                };
                string response = Requests.Post(url, json: @object);
                // string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":{\"YWH\":\"\",\"BJZT\":\"\",\"SLRQ\":\"\",\"BLZT\":\"\",\"SQRXX\":[{\"SQRMC\":\"\",\"SQRZJH\":\"\"}]}}";
                //  string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":\"null\"";
                return response;
            }
            //5.3 家庭住房查询

            public static string JTZF(string Name, string IDCardNo)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-core/extQuery/jtzf";
                JObject @object = new JObject()
                {
                    { "QLRMC ", Name},
                    {"QLRZJH", IDCardNo}
                };
                  string response = Requests.Post(url, json: @object);
                //  string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":{\"FZTS\":1,\"FZFWLIST \":[{\"QLRMC\":\"许建忠\",\"ZJLX\":\"身份证\",\"QLRZJH\":\"320222197304030379\",\"HOUSELIST\":[{\"QLBL\":null,\"FWZL\":\"安镇安南村南张巷6\",\"BDCQZH\":\"20170209472\",\"QDFS\":\"划拨\",\"FZWDM\":\"32020500200944411005100002\",\"QLRMCS\":\"许建忠\",\"YWLXMC\":null,\"JZMJ\":240.55,\"SFDZ\":null,\"DJLX\":\"首次登记/\",\"QDJG\":0,\"TDXZ\":\"划拨\",\"QLLXMC\":\"集体建设用地使用权/房屋（构筑物）所有权\",\"GHYTMC\":\"成套住宅\",\"QLRZJHMS\":\"320222197304030379\",\"FWDM\":\"00944411005100002\",\"GHYT\":\"11\",\"DJSJ\":\"2017年11月14日\"}]}]}}";
                return response;
            }

            //5.4	产权证书查询接口
            public static string CQZS(string BDCDYH, string ZL,string FWDM,string CQZH)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-core/extQuery/cqzs";
                JObject @object = new JObject()
                {
                    { "BDCDYH", BDCDYH},
                    { "ZL",ZL},
                    { "FWDM", FWDM},
                    { "CQZH",CQZH},
                };
               string response = Requests.Post(url, json: @object);

                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"A0031187\",\"YWLX\":null,\"FJ\":null,\"FJH\":\"308\",\"TDSYQMJ\":19.6,\"YYXX\":null,\"FTMJ\":0,\"ZCS\":\"6\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":null,\"SJC\":null,\"BDCDYH\":\"320282999999GB99999F00232555\",\"JZMJ\":118,\"MYC\":\"3\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"A0031187\",\"QLRMC\":\"孙勤峰\",\"GYFS\":null,\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"320223197906097374\"}],\"TDDYMJ\":0,\"QLQSSJ\":null,\"ZDDM\":\"320282999999GB99999\",\"YT\":\"成套住宅\",\"QLQTZK\":null,\"JZND\":null,\"YWH\":\"FC528371\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":19.6,\"FWJG\":\"混合结构\",\"TNMJ\":0,\"QLJSSJ\":\"2050-04-09 00:00:00\",\"ZL\":\"华益护卡膜厂综合楼308室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":null,\"ZSZBID\":\"1213488563\",\"DYXX\":null,\"FCZFZSJ\":\"1899-01-01 00:00:00\",\"QLXZ\":\"出让\",\"FWDM\":\"A0031187\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"1900-01-01 00:00:00\"}]}";
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2021）宜兴不动产权第0000004号\",\"YWLX\":\"房屋首次转移登记（含商品房、经适房、安置房）\",\"FJ\":\"登记类型：房屋首次转移登记（含商品房、经适房、安置房）\\n业务小类：市场化商品房转移登记\\n登记日期：2021-01-04\\n本土地使用者依法享有本建筑区划内全体业主共有的公共用地土地使用权\\n\",\"FJH\":\"202\",\"TDSYQMJ\":962.6,\"YYXX\":null,\"FTMJ\":398.17,\"ZCS\":\"3\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"4\",\"SJC\":\"3\",\"BDCDYH\":\"320282104025GB00056F00040202\",\"JZMJ\":2481.11,\"MYC\":\"3\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2021）宜兴不动产权第0000004号\",\"QLRMC\":\"王小华\",\"GYFS\":\"单独所有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"320421196906301738\"}],\"TDDYMJ\":0,\"QLQSSJ\":\"2014-05-20 00:00:00\",\"ZDDM\":\"320282104025GB00056\",\"YT\":\"商业服务\",\"QLQTZK\":\"专有建筑面积：2082.94㎡\\n分摊建筑面积：398.17㎡\\n分摊土地使用权面积：962.6㎡\\n房屋结构：钢筋混凝土结构\\n房屋总层数：3\\n所在层：3\\n\",\"JZND\":\"2018\",\"YWH\":\"2021010400043\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"商服用地\",\"TDFTMJ\":962.6,\"ZDMJ\":null,\"FWJG\":\"钢筋混凝土结构\",\"TNMJ\":2082.94,\"QLJSSJ\":\"2054-05-19 00:00:00\",\"ZL\":\"徐舍镇宜云路202号\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"转移登记\",\"ZSZBID\":null,\"DYXX\":[{\"QT\":\"不动产权证号：苏（2021）宜兴不动产权第0000004号\\n被担保主债权数额/最高债权数额：人民币1241万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-07~2023-12-18\\n房屋抵押面积：2481.11㎡\\n土地抵押面积：962.6㎡\\n\",\"CQZH\":\"苏（2021）宜兴不动产权第0000004号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210000662\",\"DYKSSJ\":\"2021-01-07 00:00:00\",\"DYJSSJ\":\"2023-12-18 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021010700419\",\"BDCDYH\":\"320282104025GB00056F00040202\",\"DYJE\":1241,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司徐舍支行\",\"QSZT\":\"现势\",\"FWDM\":\"120180903004\",\"DYR\":\"王小华\",\"DJSJ\":\"2021-01-08 09:59:43\"}],\"FCZFZSJ\":\"2021-01-05 09:27:35\",\"QLXZ\":\"出让\",\"FWDM\":\"120180903004\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"2021-01-04 10:01:55\"}]}";
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"YWLX\":\"存量商品房转移加抵押（不动产证）\",\"FJ\":\"换证。\",\"FJH\":\"202\",\"TDSYQMJ\":48.5,\"YYXX\":null,\"FTMJ\":13.56,\"ZCS\":\"5\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"0027\",\"SJC\":\"2\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"JZMJ\":150.84,\"MYC\":\"2\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"谢丹丹\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"340322198702048463\"},{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"沈华兵\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"342423198910188616\"}],\"TDDYMJ\":null,\"QLQSSJ\":null,\"ZDDM\":\"320282100003GB00054\",\"YT\":\"成套住宅\",\"QLQTZK\":\"分摊土地使用权面积：48.50㎡\\r\\n房屋结构：混合结构\\r\\n专有建筑面积：137.28㎡，分摊建筑面积：13.56㎡\\r\\n房屋总层数：5层，所在层数：2层\\r\\n\",\"JZND\":null,\"YWH\":\"201910180220\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":48.5,\"ZDMJ\":null,\"FWJG\":\"混合结构\",\"TNMJ\":137.28,\"QLJSSJ\":\"2073-03-14 00:00:00\",\"ZL\":\"宜城街道祥和花园27幢72号202室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"存量商品房转移加抵押（不动产证）\",\"DYXX\":[{\"QT\":\"抵押方式:一般抵押\\r\\n被担保主债权数额:90.0000万元\\r\\n债务履行期限:2019年10月14日至2049年10月14日\\r\\n抵押面积:土地面积:48.50㎡;房屋面积:150.84㎡\\r\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":null,\"BDCDJZMH\":\"20190017685\",\"DYKSSJ\":\"2019-10-14 00:00:00\",\"DYJSSJ\":\"2049-10-14 00:00:00\",\"DYFSMC\":\"一般抵押\",\"YWH\":\"201910180220-1\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":90,\"DBFW\":\"详见合同\",\"DYQR\":\"中国邮政储蓄银行股份有限公司宜兴市支行\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2019-10-21 09:05:09\"},{\"QT\":\"不动产权证号：苏（2019）宜兴不动产权第0027675号\\n被担保主债权数额/最高债权数额：人民币132.9051万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-12~2031-01-06\\n房屋抵押面积：150.84㎡\\n土地抵押面积：48.5㎡\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210001098\",\"DYKSSJ\":\"2021-01-12 00:00:00\",\"DYJSSJ\":\"2031-01-06 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021011200256\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":132.91,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2021-01-12 14:01:24\"}],\"FCZFZSJ\":\"2019-10-22 09:53:42\",\"QLXZ\":\"出让\",\"TDZH\":null,\"FWDM\":\"A0061276\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"2019-10-21 09:04:09\"}]}";
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"YWLX\":\"存量商品房转移加抵押（不动产证）\",\"FJ\":\"换证。\",\"FJH\":\"202\",\"TDSYQMJ\":48.5,\"YYXX\":null,\"FTMJ\":13.56,\"ZCS\":\"5\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"0027\",\"SJC\":\"2\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"JZMJ\":150.84,\"MYC\":\"2\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"谢丹丹\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"340322198702048463\"},{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"沈华兵\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"342423198910188616\"}],\"TDDYMJ\":null,\"QLQSSJ\":null,\"ZDDM\":\"320282100003GB00054\",\"YT\":\"成套住宅\",\"QLQTZK\":\"分摊土地使用权面积：48.50㎡\\r\\n房屋结构：混合结构\\r\\n专有建筑面积：137.28㎡，分摊建筑面积：13.56㎡\\r\\n房屋总层数：5层，所在层数：2层\\r\\n\",\"JZND\":null,\"YWH\":\"201910180220\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":48.5,\"ZDMJ\":null,\"FWJG\":\"混合结构\",\"TNMJ\":137.28,\"QLJSSJ\":\"2073-03-14 00:00:00\",\"ZL\":\"宜城街道祥和花园27幢72号202室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"存量商品房转移加抵押（不动产证）\",\"DYXX\":[{\"QT\":null,\"CQZH\":\"1000143872\",\"FJ\":null,\"BDCDJZMH\":\"1000103140\",\"DYKSSJ\":\"2015-12-09 00:00:00\",\"DYJSSJ\":\"2045-12-09 00:00:00\",\"DYFSMC\":null,\"YWH\":\"DYFC338013\",\"BDCDYH\":\"320282100024GB00071F00391401\",\"DYJE\":120,\"DBFW\":null,\"DYQR\":\"无锡市住房置业担保有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"100010224196\",\"DYR\":\"钱晶香,张力\",\"DJSJ\":\"2015-12-28 13:53:05\"},{\"QT\":\"不动产权证号：苏（2019）宜兴不动产权第0027675号\\n被担保主债权数额/最高债权数额：人民币132.9051万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-12~2031-01-06\\n房屋抵押面积：150.84㎡\\n土地抵押面积：48.5㎡\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210001098\",\"DYKSSJ\":\"2021-01-12 00:00:00\",\"DYJSSJ\":\"2031-01-06 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021011200256\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":132.91,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2021-01-12 14:01:24\"}],\"FCZFZSJ\":\"2019-10-22 09:53:42\",\"QLXZ\":\"出让\",\"TDZH\":null,\"FWDM\":\"A0061276\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"2019-10-21 09:04:09\"}]}";
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"YWLX\":\"存量商品房转移加抵押（不动产证）\",\"FJ\":\"换证。\",\"FJH\":\"202\",\"TDSYQMJ\":48.5,\"YYXX\":null,\"FTMJ\":13.56,\"ZCS\":\"5\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"0027\",\"SJC\":\"2\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"JZMJ\":150.84,\"MYC\":\"2\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"谢丹丹\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"340322198702048463\"},{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"沈华兵\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"342423198910188616\"}],\"TDDYMJ\":null,\"QLQSSJ\":null,\"ZDDM\":\"320282100003GB00054\",\"YT\":\"成套住宅\",\"QLQTZK\":\"分摊土地使用权面积：48.50㎡\\r\\n房屋结构：混合结构\\r\\n专有建筑面积：137.28㎡，分摊建筑面积：13.56㎡\\r\\n房屋总层数：5层，所在层数：2层\\r\\n\",\"JZND\":null,\"YWH\":\"201910180220\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":48.5,\"ZDMJ\":null,\"FWJG\":\"混合结构\",\"TNMJ\":137.28,\"QLJSSJ\":\"2073-03-14 00:00:00\",\"ZL\":\"宜城街道祥和花园27幢72号202室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"存量商品房转移加抵押（不动产证）\",\"DYXX\":[{\"QT\":\"2\",\"CQZH\":\"1000143872\",\"FJ\":null,\"BDCDJZMH\":\"1000103140\",\"DYKSSJ\":\"2015-12-09 00:00:00\",\"DYJSSJ\":\"2045-12-09 00:00:00\",\"DYFSMC\":null,\"YWH\":\"DYFC338013\",\"BDCDYH\":\"320282100024GB00071F00391401\",\"DYJE\":120,\"DBFW\":null,\"DYQR\":\"无锡市住房置业担保有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"100010224196\",\"DYR\":\"钱晶香,张力\",\"DJSJ\":\"2015-12-28 13:53:05\"},{\"QT\":\"不动产权证号：苏（2019）宜兴不动产权第0027675号\\n被担保主债权数额/最高债权数额：人民币132.9051万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-12~2031-01-06\\n房屋抵押面积：150.84㎡\\n土地抵押面积：48.5㎡\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210001098\",\"DYKSSJ\":\"2021-01-12 00:00:00\",\"DYJSSJ\":\"2031-01-06 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021011200256\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":132.91,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2021-01-12 14:01:24\"}],\"FCZFZSJ\":\"2019-10-22 09:53:42\",\"QLXZ\":\"出让\",\"TDZH\":null,\"FWDM\":\"A0061276\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"2019-10-21 09:04:09\"}]}";

                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人2\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人1\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":\"null\",\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";

                return response;
            }

            //5.10抵押查询
            public static string DYCX(string BDCZMH)
            {

                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:9999/out/isaip/dycx_yx";  
                JObject @object = new JObject()
                {
                    { "BDCZMH", BDCZMH},
                };
                  string response = Requests.Post(url, json: @object);
               
                // string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":{\"DYFS\":\"xxx抵押\",\"YWH\":\"TD011bb9b0c5611bb9b0c508708a8c8092\",\"ZWR\":\"王明珠\",\"BDCZMH\":\"泰州他项(2008)第3060号\",\"ZSBH\":\"9447\",\"DBFW\":\"详见合同\",\"ZGZQQDSSHSE\":1213.1130,\"FCDYMJ\":111.1000,\"TDDYMJ\":32.3000,\"BIZ\":\"人民币\",\"DJSJ\":\"1899-01-01 00:00:00\",\"ZQLXQX\":\"20070604至20170604\",\"ZXSJ\":\"2020-03-31 00:03:57\",\"ZT\":\"现势\",\"FJ\":\"1、抵押\\r\\n2、许可抵押面积(平方米)：32.30平方米\\r\\n3、许可抵押金额(大写)：土地资产不计入抵押担保值\",\"QT\":\"dsfasdgdfagsasd \",\"BDCZH\":\"泰州国用(2008)第5620号\",\"BZ\":\"bz333\",\"QLRXX\":{\"DYR\":[{\"DYR\":\"王明珠\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"320623198201293515\"}],\"DYQR\":[{\"DYQR\":\"中国建设银行股份有限公司泰州分行\",\"DYQRZJZL\":\"身份证\",\"DYQRZJHM\":\"320623198201293515\"}]}}}";
                // string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":{\"DYFS\":\" 般抵押\",\"YWH\":\"201910180220-1\",\"ZWR\":\"谢丹丹\",\"BDCZMH\":\"苏（2019）宜兴不动产证明第0017685号\",\"ZSBH\":\"32010239417\",\"DBFW\":\"详见合同\",\"ZGZQQDSSHSE\":910,\"FCDYMJ\":150.84,\"TDDYMJ\":null,\"BIZ\":\"人民币\",\"DJSJ\":\"2019-10-21 09:05:09\",\"ZQLXQX\":\"2029-10-14~2059-10-14止\",\"ZXSJ\":null,\"ZT\":\"现势\",\"FJ\":null,\"QT\":\"抵押方式:一般抵押\\r\\n被担保主债权数额:90.0000万元\\r\\n债务履行期限:2019年10月14日至2049年10月14日\\r\\n抵押面积:土地面积:48.50㎡;房屋面积:150.84㎡\\r\\n\",\"BDCZH\":\"苏（2019）宜兴不动产权第0027675号\",\"BZ\":null,\"QLRXX\":{\"DYR\":[{\"DYR\":\"沈华兵\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"342423198910188616\"},{\"DYR\":\"谢丹丹\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"340322198702048463\"}],\"DYQR\":[{\"DYQR\":\"中国邮政储蓄银行股份有限公司宜兴市支行\",\"DYQRZJZL\":\"营业执照\",\"DYQRZJHM\":\"320282000199459\"}]}}}";
                //   response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":{\"DYFS\":\"一般抵押\",\"YWH\":\"201910180220-1\",\"ZWR\":\"谢丹丹\",\"BDCZMH\":\"苏（2019）宜兴不动产证明第0017685号\",\"ZSBH\":\"32010239417\",\"DBFW\":\"详见合同\",\"ZGZQQDSSHSE\":90,\"FCDYMJ\":150.84,\"TDDYMJ\":null,\"BIZ\":\"人民币\",\"DJSJ\":\"2019-10-21 09:05:09\",\"ZQLXQX\":\"2019-10-14~2049-10-14止\",\"ZXSJ\":null,\"ZT\":\"现势\",\"FJ\":null,\"QT\":\"抵押方式:一般抵押\\r\\n被担保主债权数额:90.0000万元\\r\\n债务履行期限:2019年10月14日至2049年10月14日\\r\\n抵押面积:土地面积:48.50㎡;房屋面积:150.84㎡\\r\\n\",\"BDCZH\":\"苏（2019）宜兴不动产权第0027675号\",\"BZ\":null,\"QLRXX\":{\"DYR\":[{\"DYR\":\"沈华兵\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"342423198910188616\"},{\"DYR\":\"谢丹丹\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"340322198702048463\"}],\"DYQR\":[{\"DYQR\":\"中国邮政储蓄银行股份有限公司宜兴市支行\",\"DYQRZJZL\":\"营业执照\",\"DYQRZJHM\":\"320282000199459\"}]}}}";               
                return response;
            }

            //5.11 预告信息查

            public static string YGCX(string YGZMH="",string FWDM="",string BDCDYH="",string FWZL="",string YGQLR="",string ZJHM="")
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:9999/out/isaip/ygcx";
                JObject @object = new JObject()
                {
                    { "YGZMH", YGZMH},
                    { "FWDM", FWDM},
                    { "FWZL", FWZL},
                    { "YGQLR", YGQLR},
                    { "ZJHM", ZJHM},
                    { "BDCDYH", BDCDYH},
                };
               string response = Requests.Post(url, json: @object);
                //  string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"321202050002GB00019F00250107\",\"FWDM\":\"TS-190108093940-360061C2\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\",\"BDCLX\":\"包含土地、包含建筑物\",\"YT\":\"成套住宅\",\"ZL\":\"水映蓝庭25幢107室\",\"FWXZ\":\"市场化商品房\",\"FWJG\":\"aaa\",\"SJC\":\"1\",\"ZCS\":\"1\",\"FWLX\":\"bbb\",\"MYC\":\"1\",\"ZRZH\":\"0025\",\"FJH\":\"107\",\"JZMJ\":1.0000,\"TNMJ\":2.0000,\"FTMJ\":33.1100,\"DJSJ\":\"2019-01-09 10:32:36\",\"FCZFZSJ\":\"2019-02-13 00:00:00\",\"TDQLLX\":\"国有建设用地使用权\",\"QLQSSJ\":\"2017-04-01 10:02:03\",\"QLJSSJ\":\"2020-04-01 00:00:00\",\"TDQLXZ\":\"出让\",\"TDYT\":\"城镇住宅用地\",\"FJ\":\"fdsvdsfsdaf\",\"TDSYQMJ\":123123.0000,\"TDFTMJ\":3.0006,\"TDDYMJ\":1.0010,\"DYXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"BDCDJZMH\":\"20190008679\",\"DYQR\":\"无味\",\"DYJE\":123.000000,\"DYKSSJ\":\"2020-04-01 00:00:00\",\"DYJSSJ\":\"2021-04-28 09:04:56\",\"DJSJ\":\"2019-11-11 10:37:16\",\"QSZT\":\"现势\",\"FJ\":\"543523452345\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\",\"DYFS\":\"一般抵押\",\"DBFW\":\"详见抵押合同\",\"DYR\":\"吴海洋,魏爱霞\"}],\"CFXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"CFJG\":\"泰州法院\",\"CFLX\":\"预查封\",\"CFWH\":\"2020-0241\",\"CFKSSJ\":\"2020-03-19 00:00:00\",\"CFJSSJ\":\"2021-03-18 00:00:00\",\"DJSJ\":\"2020-03-19 17:09:44\",\"QSZT\":\"现势\",\"FJ\":\"123123123\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}],\"QLR\":[{\"QLRMC\":\"吴海洋\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"32128419821129103X\",\"GYFS\":\"共同共有\",\"QLBL\":\"30%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"},{\"QLRMC\":\"魏爱霞\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"321284198202010021\",\"GYFS\":\"共同共有\",\"QLBL\":\"70%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}]}]}";
                return response;
            }

        }

    }


    public class Requests
    {
        //Designed By Mr.Xin 2020.4.23
        //Change By Mr.Xin 2020.5.6 解决缓存问题，引入缓存数据库  (只限于GET结构,POST 和其他数据结构另外进行添加)
        //BUG Fix By Mr.Xin 2020.5.7 禁止返回信息未空的数据
        public static string Get(string url, Dictionary<string, object> dic = null, int timeout = 10000, string UserAgent = null)
        {
            url = DicAndUrl(url, dic);
            string response = Request("GET", url, dic: dic, Timeout: timeout);
            Log.WriteUrl(url, response);
            return response;
        }
        public static string Gets(string url, Dictionary<string, object> dic = null, int timeout = 10000, string UserAgent = null)
        {
            url = DicAndUrl(url, dic);
            string response = Request("GET", url, dic: dic, Timeout: timeout);
            return response;
        }

        private static string DicAndUrl(string url, Dictionary<string, object> dic)
        {
            string param = "";
            if (dic != null)
                for (int count = 0; count < dic.Count; count++)
                    param += dic.ElementAt(count).Key + "=" + dic.ElementAt(count).Value.ToString() + "&";
            return url += "?" + param;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="dic">参数</param>
        /// <param name="json"></param>
        /// <param name="timeout"></param>
        /// <param name="UserAgent"></param>
        /// <returns></returns>
        //这种写法很简单，但是不方面在其他的位置进行调用
        public static string Post(string url, Dictionary<string, object> dic = null, JObject json = null, int timeout = 10000, string UserAgent = null)
        {
            string response = Request("POST", url, dic: dic, json: json, Timeout: timeout, UserAgent: UserAgent);

            Log.WriteUrl(url,response);

            return response;
        }

        public static string Put(string url, Dictionary<string, object> dic = null, int timeout = 10000, string UserAgent = null)
        {
            return Request("PUT", url, dic: dic, Timeout: timeout, UserAgent: UserAgent);
        }



        public static bool Downloade(string url, string filePath)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                Stream stream = new FileStream(filePath, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///requets 查询
        public static string Request(string Method, string url, Dictionary<string, object> dic = null, string ContentType = null, string data = null, JObject json = null, string headers = null, string cookies = null, string UserAgent = null, int Timeout = 30000, string Referer = null, string Proxy = null, bool KeepAlive = true)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = Method;
                request.Referer = Referer ?? null;
                request.KeepAlive = KeepAlive;
                request.Timeout = Timeout;
                request.ServerCertificateValidationCallback = delegate { return true; };
                request.Headers.Add("cookie", cookies);
                request.ServicePoint.Expect100Continue = false;
                request.UserAgent = UserAgent ?? "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36";
                if (Proxy != null)
                {
                    WebProxy WebProxy = new WebProxy(Proxy);
                    request.Proxy = WebProxy;
                }
                if (Method != "GET")
                {
                    byte[] bytePost = { };
                    if (json != null)
                    {
                        request.ContentType = "application/json;charset=utf-8";
                        bytePost = Encoding.UTF8.GetBytes(json.ToString());
                    }
                    if (dic != null)
                    {
                        request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                        string param = "";
                        for (int count = 0; count < dic.Count; count++)
                            param += dic.ElementAt(count).Key + "=" + dic.ElementAt(count).Value.ToString() + "&";
                        bytePost = Encoding.UTF8.GetBytes(param.ToString());
                    }
                    //request.ContentLength = bytePost.Length;
                    request.ContentLength = bytePost.Length;
                    request.GetRequestStream().Write(bytePost, 0, bytePost.Length);
                    request.GetRequestStream().Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                string r = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                return r;
            }
            catch
            {
                return null;
            }
        }
   
    public static async Task<string> Request_NewAsync()
    {
        var person = new CQZH();
        person.BDCZMH = "1000103140";
        var j = JsonConvert.SerializeObject(person);
        var d = new StringContent(j, Encoding.UTF8, "application/json");
        var u = "http://httpbin.org/post";
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36");
        var response = await client.PostAsync(u, d);
        string result = response.Content.ReadAsStringAsync().Result;
        return result;
        }
    }
    class CQZH
    {
        public string BDCZMH { get; set; }
        public override string ToString()
        {
            return $"{BDCZMH}: {BDCZMH}";
        }
    }
}


 
