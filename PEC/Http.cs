using System.Collections.Generic;
using BaseUtil;
using BaseDLL;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Text;

namespace PEC
{
    public static class Http
    {
        public class Provincial
        {
            //大汉登录
            public static string DALogin(string loginname, string password)
            {
                string url = Global.Config.DALoginUrl;
                //url = "http://192.200.200.30:8080/loginauthenticate/auth/getfrdata1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "loginname", loginname },
                    { "password", password }
                };

                 string response = Requests.Get(url, dic);
                // string response = " {\"resultCode\":\"1\",\"msg\":\"大汉接口-查询成功\",\"data\":{\"loginname\":\"大汉接口-登录名\",\"mobile\":\"大汉接口-手机号\",\"name\":\"大汉接口-姓名\",\"cardid\":\"大汉接口-身份证号\"}}";
                //MessageBox.Show(response);
                return response;
            }
            //手机号发送信息
            public static string DASendMsg(string mobile, string usertype)
            {
                string url = Global.Config.DASendMsgUrl;
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
                string url = Global.Config.DAMsgVerifyurl;
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
                string url = Global.Config.DAIDcardLoginUrl;
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
                string url = Global.Config.DAListUrl;
                //string url = "http://192.200.200.30:8080/loginauthenticate/auth/getfrcompany1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "loginName", LoginName }
                };
               string response = Requests.Get(url, dic);
                //  string response = "{\"resultCode\":\"1\",\"msg\":\"查询成功\",\"data\":[{\"pgName\":\"pgName_1\",\"creditCode\":\"creditCode_1\",\"realName\":\"realName_1\",\"cardNumber\":\"cardNumber_1\"},{\"pgName\":\"pgName_2\",\"creditCode\":\"creditCode_2\",\"realName\":\"realName_2\",\"cardNumber\":\"cardNumber_2\"}]}";
                //MessageBox.Show(response);
                return response;
            }

            //莱斯获取模板id
            public static string LSreporttemp(string Name, string IDCardNo)
            {
                string url = Global.Config.LSReporttemp;
                //string url = "http://192.200.200.30:8080/WinHall-JSS/report/gettemp";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "strPersonName", Name },
                    { "id_card_no", IDCardNo }
                };
               string response = Requests.Get(url, dic);
                // string response = "{\"resultCode\":\"1\",\"msg\":\"查询成功\",\"data\":[{\"summary\":\"summary_1\",\"id\":\"id_1\",\"name\":\"模板_1\"},{\"summary\":\"summary_2\",\"id\":\"id_2\",\"name\":\"模板_2\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"}]}";
                // string response="{\"resultCode\":\"1\",\"msg\":\"查询成功\",\"data\":[{\"summary\":\"summary_1\",\"id\":\"id_1\",\"name\":\"模板_1\"},{\"summary\":\"summary_2\",\"id\":\"id_2\",\"name\":\"模板_2\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"},{\"summary\":\"summary_3\",\"id\":\"id_3\",\"name\":\"模板_3\"}]}";
                return response;
            }
            static int i = 0;
            //莱斯数据
            public static string LSReport(string CompanyName, string sqdx, string Name, string IDCardNo, string PhoneNum, string Level)
            {
                string url = Global.Config.LSReportUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "qymc", CompanyName },
                    { "sqdx", 1 },
                    { "jbrxm", Name },
                    { "jbrsfzh", IDCardNo },
                    { "jbrsjhm", PhoneNum },
                    { "mbid", Level },
                    { "callType","3"},
                    { "zzjCode", Global.Config.ZzjCode },
                    { "extendParams", "" }
                };

                //string response = Requests.Get(url, dic);
                //  string s = File.ReadAllText(@"1.pdf");
                //  string response = null;
                //if (i > 0)
                //{
                //    response = "{\"resultCode\":1,\"msg\":\"查询成功\",\"data\":{\"bgbh\":\"111111111\",\"bgwj\":\"abc\"}}";

                //}
                //else
                //{
                //    response = "{\"resultCode\":1,\"msg\":\"查询成功\",\"data\":{\"bgbh\":\"2222222222\",\"bgwj\":\"abc\"}}";

                //}
                //MessageBox.Show(response);
                // i = System.Math.Abs(i - 1);
                string response = Requests.Get(url, dic);
                return response;
            }
            //二维码扫报告
            public static string LSQRReport(string Msg)
            {
                string url = Global.Config.LSScanUrl;
                //string url = "http://192.200.200.30:8080/WinHall-JSS/report/smreport1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "bgbh", Msg },
                    { "callType","3"},
                    { "zzjCode", Global.Config.ZzjCode },
                    { "extendParams", "" }

                };
                string response = Requests.Get(url, dic);
                //MessageBox.Show(response);
                return response;
            }
            public static string GetGRReport(string Name, string IDCardNo)
            {
                string url = Global.Config.GRReport;
                //string url = "http://192.200.200.30:8080/WinHall-JSS/report/gettemp1";
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "xm", Name },
                    { "zjhm", IDCardNo },
                    { "callType", "3" },
                    { "zzjCode", Global.Config.ZzjCode },
                    { "extendParams", "" }
                };
                string response = Requests.Get(url, dic, timeout: 30);
                //MessageBox.Show(response);
                // string response = "{\"resultCode\":1,\"msg\":\"查询成功\",\"data\":{\"bgbh\":\"2222222222\",\"bgwj\":\"abc\"}}";

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
