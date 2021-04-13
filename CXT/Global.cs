using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XinHua
{
    public static class Global
    {
        public static string PageType = null;
        public static ConfigData Config = new ConfigData();
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
                using (var stream = File.OpenRead(sFile))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData), typeof(ConfigData).GetNestedTypes());
                    Config = serializer.Deserialize(stream) as ConfigData;
                }

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

    [Serializable]
    public class ConfigData
    {
        //使用单位信息
        public string LocateUser { get; set; } 
        //终端名称
        public string TerminalName { get; set; } = "1号机";
        // 技术支持人员和电话
        public string Technica { get; set; } = "技术支持：江苏同袍信息科技有限公司";
        public string Technicamail { get; set; } = "联系电话：025—*****";


        public string AdminPassword { get; set; } = "admin";
        #region WebService 地址
        public string WebRequestUser { get; set; } = "topo@webService";
        public string WebRequestPassword { get; set; } = "e689e0f2a799b0189b1aa05fb09b8fa9";
        //南京
        public string NanJingWebservicesUrl { get; set; } = "http://58.213.162.194:5444/WinHall/services/creditreport?wsdl";
        #endregion
    }
}
