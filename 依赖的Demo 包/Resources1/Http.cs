using System.Collections.Generic;
using BaseDLL;

namespace Resources
{
    public static class Http
    {
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
            public static string GetPersonList(IDCardData iDCardData)
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

            public static string GetPersonPDF(IDCardData iDCardData,string CQZH)
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
            public static string GetBankPDF(IDCardData iDCardData, string CQZH)
            {

                string url = Global.configData.YiXingBankUrl;
                //string url = "http://192.200.200.87:8084/RealEstateYXGYBDC/app/yxglz/txqlzm.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"QLRMC", iDCardData.Name},
                    {"QLRZJH",iDCardData.IDCardNo},
                    {"CQZH", CQZH}
                };
                string response = Requests.Get(url, dic: dic);
                //string response = "{\"code\":\"0\",\"filename\":\"481066342049415168.pdf\",\"pageNumber\":0,\"Message\":\"查询成功\",\"report\":\"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\\r\\nAb9UtsDXlD8ZK82Upg5tnrw/eH7yNIL7bjUHlHFVvBsXSF2zl1A44Cfvl8ZH5wcu1nqTZBBLbGwN\\r\\n1eMFrCK1ntJ4er15zBso4qOTu/rHq4v1q0P7457SD+7G1S0Daysd6E18bWnryX9P/XHi8sSmykvk\\r\\nVlVNQjsWvUV8//6+F5e2DlwqTQxufOfvo/7M7sMfVd4/9335sSR5mh4pIcIZcVbSNXnzrdsrd7b/\\r\\nAZiPgtAKZW5kc3RyZWFtCmVuZG9iago1IDAgb2JqCjw8L0NvbnRlbnRzIDMgMCBSL1R5cGUvUGFn\\r\\nZS9SZXNvdXJjZXM8PC9Gb250PDwvRjEgMSAwIFIvRjIgMiAwIFI+Pj4+L1BhcmVudCA0IDAgUi9N\\r\\nZWRpYUJveFswIDAgNTk1IDg0Ml0+PgplbmRvYmoKNiAwIG9iago8PC9EZXNjZW50IC0xMjAvQ2Fw\\r\\nSGVpZ2h0IDg4MC9TdGVtViA5My9UeXBlL0ZvbnREZXNjcmlwdG9yL0ZsYWdzIDYvU3R5bGU8PC9Q\\r\\nYW5vc2UoAQUCAgQAAAAAAAAAKT4+L0ZvbnRCQm94IFstMjUgLTI1NCAxMDAwIDg4MF0vRm9udE5h\\r\\nbWUvU1RTb25nLUxpZ2h0L0l0YWxpY0FuZ2xlIDAvQXNjZW50IDg4MD4+CmVuZG9iago3IDAgb2Jq\\r\\nCjw8L0RXIDEwMDAvU3VidHlwZS9DSURGb250VHlwZTAvQ0lEU3lzdGVtSW5mbzw8L1N1cHBsZW1l\\r\\nbnQgNC9SZWdpc3RyeShBZG9iZSkvT3JkZXJpbmcoR0IxKT4+L1R5cGUvRm9udC9CYXNlRm9udC9T\\r\\nVFNvbmctTGlnaHQvRm9udERlc2NyaXB0b3IgNiAwIFIvVyBbMVsyMDddOSAxMCAzNzQgMTRbMzc1\\r\\nIDIzOF0xNyAyMyA0NjIgMjUgMjYgNDYyIDI3WzIzOF04OVs0NjZdXT4+CmVuZG9iagoxIDAgb2Jq\\r\\nCjw8L1N1YnR5cGUvVHlwZTAvVHlwZS9Gb250L0Jhc2VGb250L1NUU29uZy1MaWdodC1VbmlHQi1V\\r\\nQ1MyLUgvRW5jb2RpbmcvVW5pR0ItVUNTMi1IL0Rlc2NlbmRhbnRGb250c1s3IDAgUl0+PgplbmRv\\r\\nYmoKMiAwIG9iago8PC9TdWJ0eXBlL1R5cGUxL1R5cGUvRm9udC9CYXNlRm9udC9IZWx2ZXRpY2Ev\\r\\nRW5jb2RpbmcvV2luQW5zaUVuY29kaW5nPj4KZW5kb2JqCjQgMCBvYmoKPDwvS2lkc1s1IDAgUl0v\\r\\nVHlwZS9QYWdlcy9Db3VudCAxPj4KZW5kb2JqCjggMCBvYmoKPDwvVHlwZS9DYXRhbG9nL1BhZ2Vz\\r\\nIDQgMCBSPj4KZW5kb2JqCjkgMCBvYmoKPDwvTW9kRGF0ZShEOjIwMjAwODIwMTE0NTUzKzA4JzAw\\r\\nJykvQ3JlYXRpb25EYXRlKEQ6MjAyMDA4MjAxMTQ1NTMrMDgnMDAnKS9Qcm9kdWNlcihpVGV4dK4g\\r\\nNS41LjUgqTIwMDAtMjAxNCBpVGV4dCBHcm91cCBOViBcKEFHUEwtdmVyc2lvblwpKT4+CmVuZG9i\\r\\nagp4cmVmCjAgMTAKMDAwMDAwMDAwMCA2NTUzNSBmIAowMDAwMDAyNDQ0IDAwMDAwIG4gCjAwMDAw\\r\\nMDI1NjggMDAwMDAgbiAKMDAwMDAwMDAxNSAwMDAwMCBuIAowMDAwMDAyNjU2IDAwMDAwIG4gCjAw\\r\\nMDAwMDE5MDQgMDAwMDAgbiAKMDAwMDAwMjAyNSAwMDAwMCBuIAowMDAwMDAyMjE2IDAwMDAwIG4g\\r\\nCjAwMDAwMDI3MDcgMDAwMDAgbiAKMDAwMDAwMjc1MiAwMDAwMCBuIAp0cmFpbGVyCjw8L0luZm8g\\r\\nOSAwIFIvSUQgWzw5ZGNlZTg3YmM2YThjN2I3MGRlMDRmMTU5MTQwYmUzNT48OWRjZWU4N2JjNmE4\\r\\nYzdiNzBkZTA0ZjE1OTE0MGJlMzU+XS9Sb290IDggMCBSL1NpemUgMTA+PgolaVRleHQtNS41LjUK\\r\\nc3RhcnR4cmVmCjI5MDkKJSVFT0YK\"}";
                return response;


            }
        }

    }
}
