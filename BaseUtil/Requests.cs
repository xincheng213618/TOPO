using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtil
{
    //public static class Requests
    //{
    //    public static string PostDAdd(string url, string FileName, string FileName1)
    //    {
    //        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
    //        request.AllowAutoRedirect = true;
    //        request.Method = "POST";

    //        string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
    //        byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
    //        byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

    //        request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;


    //        Stream postStream = request.GetRequestStream();

    //        postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
    //        postStream = FormData(postStream, "file", FileName);

    //        postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
    //        postStream = FormData(postStream, "file", FileName1);

    //        postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
    //        postStream.Close();


    //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
    //        Stream instream = response.GetResponseStream();
    //        StreamReader sr = new StreamReader(instream, Encoding.UTF8);
    //        string content = sr.ReadToEnd();
    //        return content;
    //    }
    //    public static Stream FormData(Stream postStream, string Key, string Value)
    //    {
    //        byte[] bArr = Covert.FileToByte(Value);
    //        Value = Value.Substring(Value.LastIndexOf("\\") + 1);
    //        StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n", Key, Value));
    //        byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
    //        postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
    //        postStream.Write(bArr, 0, bArr.Length);
    //        return postStream;
    //    }

    //    public static string DicAndUrl(string url, Dictionary<string, string> dic)
    //    {
    //        if (dic != null)
    //        {
    //            var list = new List<string>();
    //            foreach (var item in dic)
    //                list.Add(item.Key + "=" + item.Value);
    //            return url += "?" + string.Join("&", list);
    //        }
    //        else
    //        {
    //            return url;
    //        }
    //    }

    //    public static string Get(string url)
    //    {
    //        return Request("Get", url);
    //    }

    //    public static string Post(string url)
    //    {
    //        return Request("POST", url);
    //    }

    //    public static string Post(string url, Dictionary<string, string> dic)
    //    {
    //        var list = new List<string>();
    //        foreach (var item in dic)
    //            list.Add(item.Key + "=" + item.Value);
    //        return Request("POST", url, "application/x-www-form-urlencoded;charset=utf-8", Encoding.UTF8.GetBytes(string.Join("&", list)));
    //    }

    //    public static string Post(string url, JObject json)
    //    {
    //        byte[] bytePost = { };
    //        bytePost = Encoding.UTF8.GetBytes(json.ToString());
    //        return Request("POST", url, "application/json;charset=utf-8", bytePost);
    //    }

    //    public static string Request(string Method, string url, string cookies = null, string UserAgent = null, int Timeout = 30000, string Referer = null, bool KeepAlive = true)
    //    {
    //        try
    //        {
    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

    //            request.Method = Method;
    //            request.Referer = Referer ?? null;
    //            request.KeepAlive = KeepAlive;

    //            request.Timeout = Timeout;
    //            request.ServerCertificateValidationCallback = delegate { return true; };
    //            request.Headers.Add("cookie", cookies);

    //            request.UserAgent = UserAgent ?? "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36";

    //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
    //            string r = myStreamReader.ReadToEnd();
    //            myStreamReader.Close();
    //            response.Close();
    //            return r;

    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    ///requets 查询
    //    public static string Request(string Method, string url, string ContentType, byte[] bytePost, string cookies = null, string UserAgent = null, int Timeout = 30000, string Referer = null, bool KeepAlive = true)
    //    {
    //        try
    //        {
    //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

    //            request.Method = Method;
    //            request.Referer = Referer ?? null;
    //            request.KeepAlive = KeepAlive;

    //            request.Timeout = Timeout;
    //            request.ServerCertificateValidationCallback = delegate { return true; };
    //            request.Headers.Add("cookie", cookies);

    //            request.UserAgent = UserAgent ?? "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36";

    //            request.ContentType = ContentType;
    //            request.ContentLength = bytePost.Length;

    //            Stream postStream = request.GetRequestStream();
    //            postStream.Write(bytePost, 0, bytePost.Length);
    //            postStream.Close();


    //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //            StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
    //            string r = myStreamReader.ReadToEnd();
    //            myStreamReader.Close();
    //            response.Close();
    //            return r;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }
    //}


}
