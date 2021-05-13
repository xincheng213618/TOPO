using System.Collections.Generic;
using BaseUtil;
using BaseDLL;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace EXC
{
    public static class Http
    {
        public class HeFei
        {
            public static string GetReportList(string IDCardNo, string Name, string authMethod)
            {
                string url = Global.Config.HeiFeiListUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"grxm",Name},
                    {"authSfz",IDCardNo },
                    {"authMethod", authMethod},
                };
                string response = Requests.Get(url, dic: dic);
                //string response = "{\"msg\":\"查询成功\",\"code\":0,\"list\":[{\"GRXM\":\"任正非\",\"LXDH\":\"LXDH2\",\"SHXYM\":\"SHXYM2\",\"SFZH\":\"431221123696699656\",\"QYMC\":\"华为技术\",\"ID\":\"1003\",\"ROW_ID\":1}]}";
                return response;
            }
            public static string GetReport(string USCI)
            {
                string url = Global.Config.HeifeiReportUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "creditCode", USCI},
                };
                string response = Requests.Get(url, dic: dic);
                //string response = "{\"msg\":\"查询成功\",\"code\":0,\"data\":{\"msg\":null,\"flag\":true,\"data\":\"http://172.31.254.164:8191/ESS/file/sealFilePath/2020071413?name=ihQdkaT15aeowa-p72gtsb-CA4_v1kWSd8g4f3baApiu8.pdf\",\"rows\":null}}";
                return response;
            }
            public static string GetGRReport(string IDCardNo, string Name)
            {
                string url = Global.Config.HeifeiGRReportUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"creditName",Name},
                    {"creditCode",IDCardNo },
                };
                string response = Requests.Get(url, dic: dic, timeout: 60000);
                return response;
            }


            public static bool ReportDL(string url, string FilePath)
            {
                Log.Write(url + FilePath);
                return Requests.Downloade(url, FilePath);
            }



            //行政许可列表
            public static string GetXZXKPageInfo(string PageNo, string Keyword = "", string PageSize = "10")
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/doublePublicity/getXZXKPageInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"currentPageNo", PageNo},
                    {"pageSize",PageSize},
                    { "keyword",Keyword}
                };

                return Requests.Get(url, dic);
            }
            //行政许可详情 
            public static string GetLicensingListByNameAndCode(string xdrMc, string id)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/doublePublicity/getLicensingListByNameAndCode.do";

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"xdrMc", xdrMc},
                    {"id",id},
                };

                return Requests.Get(url, dic);
            }
            //行政处罚
            public static string GetXZCFPageInfo(string PageNo, string Keyword = "", string PageSize = "10")
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/doublePublicity/getXZCFPageInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"currentPageNo", PageNo},
                    {"pageSize",PageSize},
                    { "keyword",Keyword}
                };
                return Requests.Get(url, dic);
            }
            //行政处罚详情
            public static string GetSanctionListByNameAndCode(string xdrMc, string id)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/doublePublicity/getSanctionListByNameAndCode.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"xdrMc", xdrMc},
                    {"id",id},
                };
                return Requests.Get(url, dic);
            }
            //红名单列表
            public static string GetRedPageInfo(string PageNo, string Keyword = "", string PageSize = "10")
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/redAndBlack/getRedPageInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"currentPageNo", PageNo},
                    {"pageSize",PageSize},
                    { "keyword",Keyword}
                };
                return Requests.Get(url, dic);
            }
            //红名单详情
            public static string GetRedListByNameAndCode(string xdrMc, string id)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/redAndBlack/getRedListByNameAndCode.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"xzxdrmc", xdrMc},
                    {"id",id},
                };
                return Requests.Get(url, dic);
            }
            //黑名单列表
            public static string GetBlackPageInfo(string PageNo, string Keyword = "", string PageSize = "10")
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/redAndBlack/getBlackPageInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"currentPageNo", PageNo},
                    {"pageSize",PageSize},
                    { "keyword",Keyword}
                };
                return Requests.Get(url, dic);
            }
            //黑名单详情
            public static string GetBlackListByNameAndCode(string xdrMc, string id)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/redAndBlack/getBlackListByNameAndCode.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {

                    {"xzxdrmc", xdrMc},
                    {"id",id},

                };
                return Requests.Get(url, dic);
            }

            public static string GettypeConfigId()
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/focusGroupsManage/getFileGroupsTypeList.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"pageSize","10"},
                };
                return Requests.Get(url, dic);
            }

            //重点人群

            public static string GetGroupsFileListByPager(string PageNo, string Keyword, string typeConfigId, string PageSize = "10")
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/focusGroupsManage/getGroupsFileListByPager.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"currentPageNo", PageNo},
                    {"pageSize",PageSize},
                    { "searchParam",Keyword},
                    { "typeConfigId",typeConfigId},//重点人群代码
                    { "flag","1" }//0为管理员，1为非管理人员
                };
                return Requests.Get(url, dic);
            }
            public static string GetGroupsFileDetail(string tableId, string recordId, string typeConfigId)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/focusGroupsManage/getGroupsFileDetail.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"tableId", tableId},
                    {"recordId",recordId},
                    { "typeConfigId",typeConfigId},//重点人群代码
                    { "flag","1" }//0为管理员，1为非管理人员
                };
                return Requests.Get(url, dic);


            }



            //企业信息查询列表

            public static string GetXYXXPageInfo(string PageNo, string creditQuery)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/topSearch/getXYXXPageInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"currentPageNo", PageNo},
                    {"pageSize","10"},
                    { "creditQuery",creditQuery}
                };
                string response = Requests.Get(url, dic);
                //中间可以加上虚拟数据
                return response;
            }

            //企业基本信息
            public static string getCompanyRelaInfo(string CompanyID)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/creditCompany/getCompanyRelaInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"id", CompanyID},
                };
                string response = Requests.Get(url, dic);
                //string response = "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"msgDesc\":\"数据加载成功！\",\"rows\":{\"xzxkCount\":0,\"blackCount\":0,\"xzcfCount\":0,\"xycnCount\":0,\"company\":{\"id\":13596237,\"uniscid\":\"91340102MA2UND367L\",\"baseDwmc\":\"合肥俊巡装饰有限公司\",\"baseFddbr\":\"余洋洋\",\"baseZch\":\"340102000304844\",\"baseJgdm\":null,\"baseZczj\":\"1000000\",\"baseHy\":\"建筑业\",\"baseSzqh\":\"安徽省合肥市瑶海区\",\"baseClrq\":\"2020-04-17 00:00:00\",\"baseXxdz\":\"安徽省合肥市瑶海区长江路与龙岗路交口伟华大厦B座2202室\",\"baseQyzl\":\"有限责任公司\",\"baseZycp\":\"建筑装饰工程；建筑幕墙工程；室内外装饰装修工程；园林绿化工程；钢结构工程；防水工程；水电安装工程；公路工程；建筑劳务分包（除劳务派遣）；建材市场管理服务；安全系统监控服务；建筑材料、装饰材料（除危险品）、五金交电、电线电缆、机电设备销售；工艺美术品及收藏品零售（除文物）；国内广告设计、制作、代理、发布。（依法须经批准的项目，经相关部门批准后方可开展经营活动）\",\"baseDwjj\":\"合肥俊巡装饰有限公司是一家专业从事建筑装饰工程；建筑幕墙工程；室内外装饰装修工程；园林绿化工程；钢结构工程；防水工程；水电安装工程；公路工程；建筑劳务分包（除劳务派遣）；建材市场管理服务；安全系统监控服务；建筑材料、装饰材料（除危险品）、五金交电、电线电缆、机电设备销售；工艺美术品及收藏品零售（除文物）；国内广告设计、制作、代理、发布（依法须经批准的项目，经相关部门批准后方可开展经营活动）的企业\",\"bz\":\"元\",\"zjze\":\"1000000.00元\",\"baseJzsj\":\"2020-04-21 01:05:17\",\"xydm\":null,\"baseSwdjh\":null,\"baseZcrq\":\"2020-04-17\",\"baseNsrzt\":null,\"baseGdsx\":null,\"baseZzjgxz\":null,\"baseJyfw\":\"建筑装饰工程；建筑幕墙工程；室内外装饰装修工程；园林绿化工程；钢结构工程；防水工程；水电安装工程；公路工程；建筑劳务分包（除劳务派遣）；建材市场管理服务；安全系统监控服务；建筑材料、装饰材料（除危险品）、五金交电、电线电缆、机电设备销售；工艺美术品及收藏品零售（除文物）；国内广告设计、制作、代理、发布。（依法须经批准的项目，经相关部门批准后方可开展经营活动）\",\"gxrq\":\"2020-04-20\",\"baseLxdh\":\"18156572873\",\"baseYzbm\":\"230000\",\"gmsfhm\":null},\"redCount\":0},\"data\":null}";

                //中间可以加上虚拟数据
                return response;
            }

            //企业行政许可列表
            public static string GetCompanyXZXKInfo(string CompanyID, string PageNo)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/creditCompany/getCompanyXZXKInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"glid", CompanyID},
                    {"currentPageNo", PageNo},
                    {"pageSize", "10"},

                };
                string response = Requests.Get(url, dic);
                //string response = "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"msgDesc\":\"数据加载成功！\",\"rows\":[{\"id\":\"67856C059F3A2307E053292ACB3B5A95\",\"bmbh\":\"34016100000000000000\",\"bmmc\":\"合肥高新区\",\"xxfl\":\"公开\",\"jdswh\":\"JY33401910016965\",\"xmmc\":\"单位食堂\",\"splb\":\"普通\",\"xknr\":\"热食类食品制售\",\"xdrMc\":\"科大讯飞股份有限公司\",\"xdrShxym\":\"91340000711771143J\",\"xdrShxymSrc\":null,\"xdrZdm\":null,\"xdrGsdj\":null,\"xdrSwdj\":null,\"xdrSfz\":null,\"xdrFr\":\"杨军\",\"xdrLx\":\"组织\",\"jdrq\":\"2017-09-21\",\"jzrq\":\"2022-09-20\",\"xzjg\":\"合肥高新技术产业开发区食品药品监督管理局\",\"zt\":\"0\",\"ztmc\":\"正常\",\"dfbm\":\"340100\",\"bz\":null,\"bh\":null,\"recordId\":null,\"areaOid\":null,\"sjc\":\"2017-12-04\",\"xdrFrsfzh\":null,\"xdrZjlx\":null,\"xdrSydw\":null,\"xdrShzz\":null,\"xkXkzs\":null,\"xkXkbh\":null,\"xkYxqz\":null,\"xkXkjgdm\":null,\"xkLydw\":null,\"xkLydwbm\":null,\"xdrLb\":null,\"frZjlx\":null,\"gsrq\":null}],\"data\":null,\"total\":1}";
                //中间可以加上虚拟数据
                return response;
            }
            //企业行政处罚列表
            public static string GetCompanyXZCFInfo(string CompanyID, string PageNo)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/creditCompany/getCompanyXZCFInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"glid", CompanyID},
                    {"currentPageNo", PageNo},
                    {"pageSize", "10"},

                };
                string response = Requests.Get(url, dic);
                //中间可以加上虚拟数据
                return response;
            }
            //企业红名单列表
            public static string GetCompanyRedInfo(string CompanyID, string PageNo)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/creditCompany/getCompanyRedInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"glid", CompanyID},
                    {"currentPageNo", PageNo},
                    {"pageSize", "10"},

                };
                string response = Requests.Get(url, dic);
                //中间可以加上虚拟数据
                return response;
            }
            //企业黑名单列表
            public static string GetCompanyBlackInfo(string CompanyID, string PageNo)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/creditCompany/getCompanyBlackInfo.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"glid", CompanyID},
                    {"currentPageNo", PageNo},
                    {"pageSize", "10"},
                };
                string response = Requests.Get(url, dic);
                //中间可以加上虚拟数据
                return response;
            }

            //企企业信用承诺列表
            public static string GetXycnCnsDetailsBySubjectCode(string USCI, string PageNo)
            {
                string url = "http://credit.hefei.gov.cn/credit-website/publicity/xycn/getXycnCnsDetailsBySubjectCode.do";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"subjectCode", USCI},
                    {"currentPageNo", PageNo},
                    {"pageSize", "10"},

                };
                string response = Requests.Get(url, dic);
                //中间可以加上虚拟数据
                return response;
            }



        }
    }

    public class Requests
    {
        //Designed By Mr.Xin 2020.4.23
        //Change By Mr.Xin 2020.5.6 解决缓存问题，引入缓存数据库  (只限于GET结构,POST 和其他数据结构另外进行添加)
        //BUG Fix By Mr.Xin 2020.5.7 禁止返回信息未空的数据
        public static string Get(string url, Dictionary<string, object> dic = null, int timeout = 30000, string UserAgent = null)
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


        //这种写法很简单，但是不方面在其他的位置进行调用
        public static string Post(string url, Dictionary<string, object> dic = null, JObject json = null, int timeout = 10000, string UserAgent = null)
        {
            return Request("POST", url, dic: dic, json: json, Timeout: timeout, UserAgent: UserAgent);
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
    }
}
