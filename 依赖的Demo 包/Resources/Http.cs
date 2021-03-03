using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXCResources
{
    public static class Http
    {
        public static class XinCheng
        {
            //public static string[] BaiduAPi(string wd)
            //{
            //    //JObject jsons = (JObject)JsonConvert.DeserializeObject(Regex.Unescape(Requests.Get($"https://sp0.baidu.com/5a1Fazu8AA54nxGko9WTAnF6hhy/su?wd='{wd}'&json=1")).Replace("window.baidu.sug(", "").Replace(")", ""));
            //    //int a = jsons["s"].Count();
            //    //string[] ss = new string[a];
            //    //for (int i = 0; i < a; i++)
            //    //{
            //    //    ss[i] = (string)jsons["s"][i];
            //    //}
            //    return ss;
            //}
        }

        public class Fun
        {
            public string Quotes()
            {
                string url = "https://favqs.com/api/qotd";
                string response = Requests.Get(url,timeout:10000);
                try
                {
                    JObject json = (JObject)JsonConvert.DeserializeObject(response);
                    if (json != null && !json.Equals(""))
                    {
                        return json["quote"]["body"].ToString();
                    }
                    else
                    {
                        return response;
                    }
                }
                catch
                {
                    return "解析出错";
                }



            }
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
        /// 同袍全国查询
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
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
                    { "requestUser", Global.configData.RequestUser },
                    { "requestPwd", Global.configData.RequestPassword },
                    { "id", ID },
                };
                string response = Requests.Get(url, dic);
                return response;
            }
        }

        /// <summary>
        /// 苏州不动产
        /// </summary>
        public class RealEstate
        {
            public static string AddAction(string Name, string IDCardNo, string OprCode)
            {
                string url = Global.configData.AddActionUrl;

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo },
                    { "opr_code", OprCode },
                    { "zzjid", Global.configData.TerminalName }
                };
                string response = Requests.Get(url, dic, 200);

                //MessageBox.Show(response);
                return response;
            }

            //十区联查无房证明
            public static string NoHome(string Name, string IDCardNo)
            {
                string url = Global.configData.NoHomeUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo },
                    { "opr_code", "chaszliuquwufang" },
                    { "zzjid", Global.configData.TerminalName },
                    { "areaCode", Global.configData.AreaCodes }
                };
                string response = Requests.Get(url, dic);

                //Log.Write(response);
                //MessageBox.Show(response);
                return response;
            }

            public static string NoHomeSix(string Name, string IDCardNo)
            {
                string url = Global.configData.NoHomeSixUrl;

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },

                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo },
                    { "opr_code", "chaszliuquwufang" },
                    { "zzjid", Global.configData.TerminalName },
                    { "areaCode", "320505,320506,320507,320508,320509,320513" }
                };
                string response = Requests.Get(url, dic);

                return response;
            }
            //十区联查权属
            public static string OwnerShip(string Name, string IDCardNo)
            {
                string url = Global.configData.OwnerShipUrl;
                if (url == null)
                    url = "http://192.200.200.30:8080/RealEstateSZXQBDC/app/houseInfo/all";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo },
                    { "opr_code", "chaquanshu" },
                    { "head_picture", "1" },
                    { "zzjid", Global.configData.TerminalName },
                    { "queryUser", "1" },
                    { "areaCode",  Global.configData.AreaCodes}
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //十区联查住房套数
            public static string HomeCount(string Name, string IDCardNo)
            {
                string url = Global.configData.HomeCountUrl;
                if (url == null)
                    url = "http://192.200.200.30:8080/RealEstateSZWZBDC/app/quanshu/isExitHouse.do";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo },
                    { "head_picture", "1" },
                    { "zzjid", Global.configData.TerminalName },
                    { "opr_code", "chafangwutaoshu" },
                    { "areaCode",  Global.configData.AreaCodes}
                };

                string response = Requests.Get(url, dic);
                //response = "{\"code\":\"0\",\"filename\":\"445977665694892032.pdf\",\"pageNumber\":1,\"Message\":\"查询成功\",\"bianhao\":\"有房2020区号97850054\",\"report\":\"JVBERi0xLjQKJeLjz9MKNCAwIG9iago8PC9Db2xvclNwYWNlL0RldmljZUdyYXkvU3VidHlwZS9J\\r\\nbWFnZS9IZWlnaHQgMTE0OC9GaWx0ZXIvRmxhdGVEZWNvZGUvVHlwZS9YT2JqZWN0L1dpZHRoIDIw\\r\\nNjcvTGVuZ3RoIDIwNzU2L0JpdHNQZXJDb21wb25lbnQgOD4+c3RyZWFtCnic7N35w2Vz/QDw55mF\\r\\nhgwiY4kGiQkl2iylvqFSjSLRpoiSlDLtlqQoRZQWSoSiJNQgEbKNJQzZxjY8s5x/o+9z7z2fc7dz\\r\\n7r3n3HPvfTzP6/UD85z9nvu557zP53w+78/YGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\n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\\r\\n7WcNZiSAvf8z8YthjzEQzB/mj2jR49EjB/f2Xbzms3kPbDImOCn/ITF4W1+x9rzhvxMbjEqNXlw1\\r\\nHQ+0c9sJJTe9mKJeuzZ66PM5hnDe4fKL9h3aw8b0KmQzxWHvLm8E9t7tG0V/nvIlZYOZEtwu+sYA\\r\\n60g/PZwmkuQ3fYaB2/mS6OT4n3MnHzduWbLjSA9nmL5xaP7EWcM0fQoZA3XSoUMMVYHpbfvkenLA\\r\\nkV2TCwEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\n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\\r\\nqXep39NAaZhphGvkauzROKvxdA5tjssc7pySOYfn3NaENc00IzRXaO7THNCc1tLW8tPK0qrSOqP1\\r\\nVJuu7aGdqr1D+4T2pA5Vx01HoLND56TOY4YKw5ORzqhk9DGmdDV1/XUluvW6g7ozesZ6UXqFeu16\\r\\n9/QJ+iz9JP0d+r36UwY6BiEGBQatBrcN8YYswxTDXYb9hq+NjI1ijDYYdRk9MlYzDjDON241vmtC\\r\\nNnE3WWbSYHLNFGPKMk0z3W162Qw2szdLMasxGzKHzR3MBea7zYct0BZOFkKLBosbTBLTk5nDbGWO\\r\\nWtItgy0LLbssn1kZWMVbbbPqt/pobW+dbt1ofceGYhNoU2jTY/OrrZkt17bG9tpc8lzfuavnds99\\r\\nbmdux7fbY3fTnmofYr/Bvtf+g4Ojg8ihzWHS0cAx0bHW8QaLxgpjbWadd0I7eTmtdjrm9NbZwVns\\r\\nfNj5FxemS5pLi8ujecbz+PMa54256rlyXOtdpW4Mt0S3vW5Sd113jnuD+wMPfQ+eR5PHhKepZ6rn\\r\\nQc9nXtZeIq8Or9dsZ/ZK9ilvxNvPu8R70IfiE+VT7XPfV8832bfVd8rP3m+F3yl/tH+Q/zb/GwFa\\r\\nAdyA5oCpQMfAlYF9QaSgBUHVQQ+CzYJFwT0hcEhgyPaQu/MN5wvnd4WC0IDQ7aH3wozDloV9H44J\\r\\nDwuvCX8YYRNRENG/gLpgyYKWBa8ivSLLIu9EmURJonqjFaMTopujX8d4x5THSGOtYlfGXorTiBPE\\r\\ndcdj46Pjm+KnF/os3LlwPME+oTjh+iLjRXmLLizWWJy++PgSxSWcJUcS0YkxiS2J7zmhnAbO9NKA\\r\\npbVLp7hs7i7uE54Hbwdvku/KL+dPJLkmlSc9SnZN3p48meKeUpHyVMAWVAuep/qn1qW+TgtN25/2\\r\\nKT0mvT0Dl5GYcVRIEaYJ+zK1M/Myh7PMs4qzpMucl+1cNiUKEjVlQ9mLsrvFNNnP1IDERLJeMprj\\r\\nllOT8yY3OvdInnKeMG9gudnyTcsn8n3zv16BWsFd0VugW7C2YHSl58r6VdCqpat6V+uvLlo9vsZv\\r\\nzYG1hLVpa38otC4sL3y5LmZdT5FW0ZqisfV+61uLFYpFxTc2uGyo24jaKNg4uGnupqpNH0t4JRdL\\r\\nrUsrSt9v5m6++JXNV5VffdqStGWwzKFsz1bMVuHW69vctx0oVy7PLx/bHrK9cwdjR8mOlzuX7LxQ\\r\\nYVdRt4uwS7JLWhlc2V1lULW16n11SvVIjVdNe61m7aba17t5u6/s8djTVqdVV1r3bq9g7816v/rO\\r\\nBqOGin2YfTn7HjZGN/Z/zfq6uUmjqbTpw37hfumBiAN9zY7NzS2aLWWtcKukdfJgwsHL33h/093G\\r\\nbKtvp7eXHgKHJIcef5v47fXDQYd7j7COtH1n+F1tB7WjpBPqXN451ZXSJe2O6x4+Gni0t8elp+N7\\r\\ny+/3H9M9VnNc5XjZCcKJohOfTuafnD6Vderp6eTTY71Leu+ciT1zrS+8b/Bs0Nnz53zPnen37D95\\r\\n3vX8sQvOF45eZF3suuRwqXPAfqDjB/sfOgYdBjuHHIe6Lztd7hmeN3ziivuV01e9r567FnDt0sj8\\r\\nkeHrUddv3ki4Ib3Ju/noVvqt57dzbs/cWXMXfbfkntK9ivua9xt+NP2xXeogPT7qPTrwYMGDO2Pc\\r\\nsSc/Zf/0frzoIflhxYTORPMj20fHJn0nLz9e+Hj8SdaTmafFPyv/XPvM5Nl3v3j8MjAVOzX+XPT8\\r\\n06+bX6i/2P/S7mXvdNj0/VcZr2Zel7xRf3PgLett/7uYdxMzue+x7ys/mH7o+Rj08e6njE+ffgP3\\r\\nhPP7CmVuZHN0cmVhbQplbmRvYmoKNiAwIG9iago8PC9Db2xvclNwYWNlWy9JQ0NCYXNlZCA1IDAg\\r\\nUl0vU3VidHlwZS9JbWFnZS9IZWlnaHQgMTE0OC9GaWx0ZXIvRmxhdGVEZWNvZGUvVHlwZS9YT2Jq\\r\\nZWN0L1dpZHRoIDIwNjcvU01hc2sgNCAwIFIvTGVuZ3RoIDEwOTI0L0JpdHNQZXJDb21wb25lbnQg\\r\\nOD4+c3RyZWFtCnic7NxBbvTGuYbR/e8to6wh8DZy0Yjw06Ilkd0s1qPqe840Azcff/VmIMD//S8A\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\n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\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\\r\\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPjyAOv8+/YKZW5kc3RyZWFt\\r\\nCmVuZG9iago3IDAgb2JqCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggMTM0ND4+c3RyZWFt\\r\\nCnicrVdNaCRFFC73sEIvLB7EQ051nOhOpX66qqsDEtjRXRDpOD8a4rbZZTRplI1LstldloTohg24\\r\\nzEE9iBePwnrcix5EkCCKeAno1R90GfDmwSV7EBlfVXdnujPVMxHN0JPuru+97/X3qr6pXvPWvDol\\r\\nEsOXVooKnP+3dzXFilL86qq3hus69GEMU/jU/TDAcBBfYGkBeOb11YThZ67gpne24zELY9j3LYbi\\r\\nzqo3TNB5zSueP9vxmlCHISyw5+VILIBBCFtGIXWdc02YSnPPnGNYKNxZgSwhTrwa4oiaY2kD/ssk\\r\\n7iKG5HIfYfv/AEmkWidhiN38frrzhkchxpZhCgESTrFm3BTHOCWU47qwpRoeGAMeGFwHMgV/Ab7h\\r\\n1aJ4vfVJdBfFtfn5C7sonr6+2bs3/+ulK0m/99v2D8lHvb2V0ykVBDIIqWepbWLQlOmUgWPDvOLV\\r\\ncAq348IvIFiOeOuJ9sPBVBJvdOfyB27Pth+iEAVIwzNS8/DIN3nqJsOxqMy4OiTy7fj2vnmMQbzS\\r\\nffLWny1qLpYPPngwmBqAsseXehBvQI6FuBbVX/r78sWd/Z39V35+5/nlufbswi+uEmrNXQhI+i0K\\r\\nQUZR4Ft8+eqdd39MU/X2ogMjwSBlhTIEHBzOQxAgAFZzHcK1hkNBUCdej06BYlE/OpX1LO/UkQIm\\r\\naMRHTiaqaseP3hWUCJ6dSz08H5uN85zXNsnO/Nrio5d/t63mx5xUvg9Z/CNZQCeSz9noazNrQar4\\r\\nzVRp6EKvn3e/uXv9diahkY+esOSQ9D+Qc0R2+skZS3u60J+tObiI0K0H0HRTT3FgtvnN/8ItgBvm\\r\\n49KJQvqofvGrbh/mS4K2v71z//3vksa/5KpsvOOu5KW1l9b1dmfrr2uy99NgCiaygknOEKVo9OOs\\r\\n3ZYqjzkflHbQu6jMB+Ej124bsAVA4qLTm4Pj54D2vHX9G8ZvldSYMWuv68teOxtggqUjSsF1eYzD\\r\\nD5CsiPOpzsdKkaVfJ9BFKUGguuwnJHO76P7GyehzUzkUXMBzGhKfuWPQzChepEVUU6SLajTQ14qE\\r\\nk4iqdBSVOsKID9PWrUWIle+g29psPTWeUAbMhMnAaJO6WYE4sLczTACtkKMYJgWBDcV4EIc70LAM\\r\\nJDTh2gFSmgThBJCgAaGT6IQGMfgEkK8oYcEEOj/UxJcTQFIoElQ/XalVXBIOkupgtFtJoxVPu6au\\r\\nloSF7pjMMdovmqgcH4YWH+QzotZsuCYqCyQJfHfehRduP+1YQ0KYXZsz4lpce+9xRwiIYjaarpAP\\r\\nuzf/cCw8BrvLirKMM5pfL0dUCDwVGt1NGr0951JVnOiKbsDyXimKmmOHqm5tLvRGc0rKCK3olu1w\\r\\nMWkOHiZd/+zSI46kIiSiQvikEcdLpaQZ2Jm0ygeYSGPsgtduH0gx6XIaweQ+MBaU+0AKSlfKKCjz\\r\\ngbGg3AfG0uU+MBaU+8BYutwHxoJyH6gAuXwABDevPqWGpntkhw3Ae5V0h9idMrUvDRoOHz60OCU0\\r\\nIxIWoS+y0Jp5s0Dn0VmztU/3ACW4sMbAw0P4uWyjoDIW5rATxWyUq74o3mg2IpQ0iywsW1GFqtRS\\r\\nN01/4eNq93ERdB9bXCjmzrHD3ObNzmVPIKqoEFWBmAR00i6TYrbVzjhuVQ1RHb65/VbF2gS8OrLy\\r\\nc1NAz0ITzbePpIMPVj6v4NvZhxfXQ9J4eme/xJYa45DscKNZRmlS6rgxzc0zJYgOSQCSMnlYdj7X\\r\\nzNaWjzNYV9m5wbrt09nkT7tfVHujK+LqvdaXWcQ/rYDfRwplbmRzdHJlYW0KZW5kb2JqCjkgMCBv\\r\\nYmoKPDwvQ29udGVudHMgNyAwIFIvVHlwZS9QYWdlL1Jlc291cmNlczw8L0ZvbnQ8PC9GMSAyIDAg\\r\\nUi9GMiAzIDAgUj4+L1hPYmplY3Q8PC9pbWcxIDYgMCBSL2ltZzAgNCAwIFI+Pj4+L1BhcmVudCA4\\r\\nIDAgUi9NZWRpYUJveFswIDAgNTk1IDg0Ml0+PgplbmRvYmoKMTAgMCBvYmoKPDwvRGVzY2VudCAt\\r\\nMTIwL0NhcEhlaWdodCA4ODAvU3RlbVYgOTMvVHlwZS9Gb250RGVzY3JpcHRvci9GbGFncyA2L1N0\\r\\neWxlPDwvUGFub3NlKAEFAgIEAAAAAAAAACk+Pi9Gb250QkJveCBbLTI1IC0yNTQgMTAwMCA4ODBd\\r\\nL0ZvbnROYW1lL1NUU29uZy1MaWdodC9JdGFsaWNBbmdsZSAwL0FzY2VudCA4ODA+PgplbmRvYmoK\\r\\nMTEgMCBvYmoKPDwvRFcgMTAwMC9TdWJ0eXBlL0NJREZvbnRUeXBlMC9DSURTeXN0ZW1JbmZvPDwv\\r\\nU3VwcGxlbWVudCA0L1JlZ2lzdHJ5KEFkb2JlKS9PcmRlcmluZyhHQjEpPj4vVHlwZS9Gb250L0Jh\\r\\nc2VGb250L1NUU29uZy1MaWdodC9Gb250RGVzY3JpcHRvciAxMCAwIFIvVyBbMVsyMDddOSAxMCAz\\r\\nNzQgMTRbMzc1IDIzOCAzMzRdMTcgMjYgNDYyIDI3WzIzOF0zNVs1NjBdMzlbNTExIDcyOV1dPj4K\\r\\nZW5kb2JqCjIgMCBvYmoKPDwvU3VidHlwZS9UeXBlMC9UeXBlL0ZvbnQvQmFzZUZvbnQvU1RTb25n\\r\\nLUxpZ2h0LVVuaUdCLVVDUzItSC9FbmNvZGluZy9VbmlHQi1VQ1MyLUgvRGVzY2VuZGFudEZvbnRz\\r\\nWzExIDAgUl0+PgplbmRvYmoKMyAwIG9iago8PC9TdWJ0eXBlL1R5cGUxL1R5cGUvRm9udC9CYXNl\\r\\nRm9udC9IZWx2ZXRpY2EvRW5jb2RpbmcvV2luQW5zaUVuY29kaW5nPj4KZW5kb2JqCjEgMCBvYmoK\\r\\nPDwvU3VidHlwZS9Gb3JtL0ZpbHRlci9GbGF0ZURlY29kZS9UeXBlL1hPYmplY3QvTWF0cml4IFsx\\r\\nIDAgMCAxIDAgMF0vRm9ybVR5cGUgMS9SZXNvdXJjZXM8PD4+L0JCb3hbMCAwIDEwMCAxMDBdL0xl\\r\\nbmd0aCA4Pj5zdHJlYW0KeJwDAAAAAAEKZW5kc3RyZWFtCmVuZG9iago4IDAgb2JqCjw8L0tpZHNb\\r\\nOSAwIFJdL1R5cGUvUGFnZXMvQ291bnQgMT4+CmVuZG9iagoxMiAwIG9iago8PC9UeXBlL0NhdGFs\\r\\nb2cvUGFnZXMgOCAwIFI+PgplbmRvYmoKMTMgMCBvYmoKPDwvTW9kRGF0ZShEOjIwMjAwNTE1MTU1\\r\\nNjAxKzA4JzAwJykvQ3JlYXRpb25EYXRlKEQ6MjAyMDA1MTUxNTU2MDErMDgnMDAnKS9Qcm9kdWNl\\r\\ncihpVGV4dK4gNS41LjUgqTIwMDAtMjAxNCBpVGV4dCBHcm91cCBOViBcKEFHUEwtdmVyc2lvblwp\\r\\nKT4+CmVuZG9iagp4cmVmCjAgMTQKMDAwMDAwMDAwMCA2NTUzNSBmIAowMDAwMDM2OTI4IDAwMDAw\\r\\nIG4gCjAwMDAwMzY3MTUgMDAwMDAgbiAKMDAwMDAzNjg0MCAwMDAwMCBuIAowMDAwMDAwMDE1IDAw\\r\\nMDAwIG4gCjAwMDAwMjA5MzEgMDAwMDAgbiAKMDAwMDAyMzYxOSAwMDAwMCBuIAowMDAwMDM0NzIx\\r\\nIDAwMDAwIG4gCjAwMDAwMzcwOTEgMDAwMDAgbiAKMDAwMDAzNjEzMyAwMDAwMCBuIAowMDAwMDM2\\r\\nMjg4IDAwMDAwIG4gCjAwMDAwMzY0ODAgMDAwMDAgbiAKMDAwMDAzNzE0MiAwMDAwMCBuIAowMDAw\\r\\nMDM3MTg4IDAwMDAwIG4gCnRyYWlsZXIKPDwvSW5mbyAxMyAwIFIvSUQgWzxiYmFiNjg3NTI5Zjgx\\r\\nNTE3NGJjMjE4NTczYmVkZmE2Yj48YmJhYjY4NzUyOWY4MTUxNzRiYzIxODU3M2JlZGZhNmI+XS9S\\r\\nb290IDEyIDAgUi9TaXplIDE0Pj4KJWlUZXh0LTUuNS41CnN0YXJ0eHJlZgozNzM0NgolJUVPRgo=\",\"houselist\":[{\"zuoluo\":\"三元一村6幢501室\",\"name\":\"奚玉远\"}]}";
                return response;
            }
            public static string MoneyProgressQuery(string code = null)
            {
                string url = Global.configData.MoneryUrl;

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "zzjid", Global.configData.TerminalName },
                    { "xybh", code },
                    { "opr_code", "chazijin" }

                };
                string response = Requests.Get(url, dic);

                //Log.Write(response);
                //MessageBox.Show(response);
                return response;
            }
            public static string ProcessProgressQuery(string code = null)
            {
                string url = Global.configData.ProcessUrl;
                if (url == null)
                    url = "http://192.200.200.30:8080";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "zzjid", Global.configData.TerminalName },
                    { "strFlowid", code },
                    { "opr_code", "chajindu" }
                };

                string response = Requests.Get(url, dic);

                //Log.Write(response);
                //MessageBox.Show(response);
                return response;
            }

            public static string SZArchiveList(string Name, string IDCardNo, string PageNo, string PageSize)
            {
                string url = Global.configData.SZArchiveListUrl;
                if (url == null)
                    url = "http://192.200.200.30:8080";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo },
                    { "zzjid", Global.configData.TerminalName },
                    { "opr_code", "chadangan" },
                    { "pageNo", PageNo },
                    { "pageSize", PageSize },
                    { "head_picture", ""  }
                };
                string response = Requests.Post(url, dic);
                Log.Write(url + "    " + response);
                //MessageBox.Show(response);
                return response;
            }   
            public static string SZArchive(string IDCardNo, string archivecode)
            {
                string url = Global.configData.SZArchiveUrl;
                if (url == null)
                    url = "http://192.200.200.30:8080";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "id_card_no", IDCardNo },
                    { "archivecode", archivecode }
                };
                string response = Requests.Get(url, dic);
                response = "";
                return response;
            }

            public static string SZArchiveMenu(string IDCardNo, string Archivecode)
            {
                string url = Global.configData.SZArchiveMenuUrl;
                if (url == null)
                    url = "http://192.200.200.30:8080";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "ip", Global.configData.IP },
                    { "mac", Global.configData.MAC },
                    { "zzjid", Global.configData.TerminalName },
                    { "SLBH", Archivecode },
                    { "DALX", "bdc" },
                    { "id_card_no", IDCardNo }
                };
                string response = Requests.Post(url, dic);
                //Log.Write(response);
                //MessageBox.Show(response);
                return response;
            }

            public static bool ShowImage(Dictionary<string, object> dic, string FilePath)
            {
                string url = Global.configData.ShowImage;

                return Requests.Downloade(XCovert.DicAndUrl(url, dic), FilePath);
            }
        }



        public class Provincial
        {
            //大汉登录
            public static string DALogin(string loginname, string password)
            {
                string url = Global.configData.DALoginUrl;
                //url = "http://192.200.200.30:8080/loginauthenticate/auth/getfrdata1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "loginname", loginname },
                    { "password", password }
                };

                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //手机号发送信息
            public static string DASendMsg(string mobile, string usertype)
            {
                string url = Global.configData.DASendMsgUrl;
                //string url = "http://192.200.200.30:8080/loginauthenticate/auth/getfrcompany1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "mobile", mobile },
                    { "usertype", usertype }
                };

                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //手机号验证
            public static string DAMsgVerify(string mobile, string usertype, string randcode)
            {
                string url = Global.configData.DAMsgVerifyurl;
                //string url = "http://192.200.200.30:8080/loginauthenticate/auth/getfrcompany1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "mobile", mobile },
                    { "usertype", usertype },
                    { "randcode", randcode }
                };

                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //身份证验证
            public static string DAIDcrdLogin(string IDCardNo, string usertype)
            {
                string url = Global.configData.DAIDcardLoginUrl;
                //string url = "http://192.200.200.30:8080/loginauthenticate/auth/getfrcompany1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "papersnumber", IDCardNo },
                    { "usertype", usertype }
                };

                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //大汉获取list
            public static string DAList(string LoginName)
            {
                string url = Global.configData.DAListUrl;
                //string url = "http://192.200.200.30:8080/loginauthenticate/auth/getfrcompany1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "loginName", LoginName }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }

            //莱斯数据
            public static string LSReport(string CompanyName, string sqdx, string Name, string IDCardNo, string PhoneNum, string Level)
            {
                string url = Global.configData.LSReportUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "qymc", CompanyName },
                    { "sqdx", 1 },
                    { "jbrxm", Name },
                    { "jbrsfzh", IDCardNo },
                    { "jbrsjhm", PhoneNum },
                    { "mbid", Level }
                };

                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            //二维码扫报告
            public static string LSQRReport(string ComPanyCode, string CompanyName, string Name, string IDCardNo)
            {
                string url = Global.configData.LSQRreportUrl;
                //string url = "http://192.200.200.30:8080/WinHall-JSS/report/smreport1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "bgbh", ComPanyCode },
                    { "qymc", CompanyName },
                    //dic.Add("sqdx", 1);
                    { "jbrxm", Name },
                    { "jbrsfzh", IDCardNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }

            //莱斯获取模板id
            public static string LSreporttemp(string Name, string IDCardNo)
            {
                string url = Global.configData.LSReporttemp;
                //string url = "http://192.200.200.30:8080/WinHall-JSS/report/gettemp1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo }
                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
        }

        /// <summary>
        /// 新华信用
        /// </summary>
        public class XinHua
        {
            private static Dictionary<string, object> PassWdic = new Dictionary<string, object>
            {
                { "chooseType", "printMachineTerminal" },
                { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
            };
            public static string XinHuaSearch(string CompanyName, int pageNo)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/search/companyHighSearchByPage";
                var json = new JObject
                {
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "Name", CompanyName }
                };

                string response = Requests.Post(XCovert.DicAndUrl(url, PassWdic), json: json);
                return response;
            }

            public static string XinHuaTaxA(string CompanyName , int pageNo)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/findPageCompanyA";
                var json = new JObject
                {
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "name", CompanyName }
                };

                string response = Requests.Post(XCovert.DicAndUrl(url, PassWdic), json:json);
                return response;
            }
            public static string XinHuaTaxADetail(string CompanyName, string Uniscid = "")
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/queryCompanyA";
                var json = new JObject
                {
                    { "name", CompanyName },
                    { "discernNumber", Uniscid }
                };
                string response = Requests.Post(XCovert.DicAndUrl(url, PassWdic), json: json);
                return response;
            }
            public static string XinHuaTaxV(string Name,int pageNo)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/findPageQueryDelinquentParty";
                var json = new JObject
                {
                    { "name", Name },
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                };
                string response = Requests.Post(XCovert.DicAndUrl(url, PassWdic), json: json);
                return response;
            }
            public static string XinHuaTaxVDetail(string companyName, string companyId)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/queryDelinquentParty";
                var json = new JObject
                {
                    { "companyId", companyId },
                    { "companyName", companyName }
                };
                string response = Requests.Post(XCovert.DicAndUrl(url, PassWdic), json: json);
                return response;
            }


            public static string XinHuaDishonestyPeople(string CompanyName, int pageNo)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/findPageQueryPerson";
                var json = new JObject
                {
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "name", CompanyName }
                };

                string response = Requests.Post(XCovert.DicAndUrl(url, PassWdic), json: json);
                return response;
            }
            public static string XinHuaLostCreditDetail(string companyName, string companyId)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/queryPerson";
                var json = new JObject
                {
                    { "companyId", companyId },
                    { "name", companyName }
                };
                string response = Requests.Post(XCovert.DicAndUrl(url, PassWdic), json: json);
                return response;
            }


            public static string XinHuaReport(string companyName, string companyId)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/report/createReportByQi";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" },
                    { "reportType",1 },
                    { "companyId", companyId},
                    { "companyName", companyName }
                };
                string response = Requests.Get(url, dic: dic);
                return response;
            }
            /// <summary>
            /// 新华报告
            /// </summary>
            /// <param name="path"></param>
            /// <param name="FilePath"></param>
            /// <returns></returns>
            public static bool GetXinHuaReport(string path,string FilePath)
            {
                string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/report/downloadReport";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "path" ,path },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" },
                };                ;
                return Requests.Downloade(XCovert.DicAndUrl(url, dic), FilePath);
            }
        }


    }
}
