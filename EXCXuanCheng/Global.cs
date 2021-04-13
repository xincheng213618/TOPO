using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EXCXuanCheng
{
    public static class Global
    {
        public static ConfigData Config = new ConfigData();
        public static UserDate UserDate = new UserDate();

        public static Related Related = new Related();
        public static string IP = Info.IPAdress()[0];
        public static string MAC = Info.MACAdress()[0];


        public static void Initialized()
        {
            if (File.Exists("Config"))
            {
                ReadConfigFile("Config");
            }
            else
            {
                WriteConfigFile("Config");
            }

        }

        public static void ReadConfigFile(string sFile)
        {
            try
            {
                //此调用方法多次运行会产生内存泄漏,参见 https://stackoverflow.com/questions/1127431/xmlserializer-giving-filenotfoundexception-at-constructor
                FileStream stream = File.OpenRead(sFile);
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigData), typeof(ConfigData).GetNestedTypes());
                Config = serializer.Deserialize(stream) as ConfigData;
            }
            catch
            {
                Environment.Exit(0);
            }
        }
        public static void WriteConfigFile(string sFile)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ConfigData), typeof(ConfigData).GetNestedTypes());
            FileStream fs = File.Create(sFile);
            ser.Serialize(fs, Config);
            fs.Close();
        }
    }

    public class Related
    {
        public string UUID;
        public string PageType;
        public IDCardData IDCardData;
        public string transtionId;

        public void Initialized()
        {
            UUID = Guid.NewGuid().ToString();
            IDCardData = new IDCardData();
            transtionId = "";
            PageType = "";
        }
    }
    public class UserDate
    {
        public void Initialized()
        {
            Name = "";
            IDCardNo = "";
            CompanyName = "";
            CompanyID = "";
            Type = "";
        }
        public string Name = "";
        public string IDCardNo = "";
        public string CompanyName = "";
        public string CompanyID = "";
        public string Type = "";
    }
    [Serializable]
    
    public class ConfigData
    {
        public int BackgroundWindow { get; set; } = 0; //终端机不明确窗口

        public string LocateUser { get; set; } = "宣城信用报告终端打印一体机";
        //终端名称
        public string TerminalName { get; set; } = "一号机";
        // 技术支持人员和电话
        public string Technica { get; set; } = "技术支持：江苏同袍科技信息有限公司";

        public string Technicamail { get; set; } = "联系方式：400-889-8805";

        public string AdminPassword { get; set; } = "admin";


        public string XuanChengZCXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjCompany/getFrjbInfo";
        public string XuanChengXKXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjCompany/getFrxkInfo";
        public string XuanChengZZXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjCompany/getFrzzInfo";
        public string XuanChengCBXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjCompany/getFrcbInfo";
        public string XuanChengLHXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjCompany/getFrlhInfo";
        public string XuanChengBLXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjCompany/fr/getFrblInfo";

      
        public string tipszp { get; set; }   = "个人照片信息";
        /// <summary>
        /// 个人照片URL
        /// </summary>
        public string XuanChengGRZPXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjPersonal/getZp?sfzh=342501197109108708"; 
        public string tipssf { get; set; }   = "个人身份信息";
        /// <summary>
        /// 个人身份URL
        /// </summary>
        public string XuanChengGRSFXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjPersonal/getZrrjbInfo?sfzh=342401199010316914";
        public string tipszc { get; set; } = "个人资产信息";
        /// <summary>
        /// 个人资产URL
        /// </summary>
        public string XuanChengGRZCXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjPersonal/zrr/getZrrzcInfo?sfzh=342401199010316914";
        public string tipscb { get; set; } = "个人参保信息";
        /// <summary>
        /// 个人参保URL
        /// </summary>
        public string XuanChengGRCBXXUrl { get; set; } = " http://127.0.0.1:8083/restful-data/rest/ytjPersonal/getZrrcbInfo?sfzh=342401199010316914";
        public string tipslh { get; set; } = "个人良好信息";
        /// <summary>
        /// 个人良好URL
        /// </summary>
        public string XuanChengGRLHXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjPersonal/zrr/getZrrlhInfo?sfzh=342401199010316914";
        public string tipsbl { get; set; } = "个人不良信息";
        /// <summary>
        /// 个人不良URL
        /// </summary>
        public string XuanChengGRBLXXUrl { get; set; } = "http://127.0.0.1:8083/restful-data/rest/ytjPersonal/zrr/getZrrblInfo?sfzh=342401199010316914";


    }
}
