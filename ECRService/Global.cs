using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ECRService
{
    public static class Global
    {
        public static string PageType = null;
        public static ConfigData Config = new ConfigData() { };
        public static string IP = Info.IPAdress()[0];
        public static string MAC = Info.MACAdress()[0];

        public static string UUID = "";

        public static string[] OcrRegion;

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
    



    [Serializable]
    public class ConfigData
    {
        public string ServerURL { get; set; } = "http://192.200.200.55:8080/WinHall/services/creditreport?wsdl";
        public string LoginName { get; set; } = "topo@webService";
        public string LoginPassword { get; set; } = "e689e0f2a799b0189b1aa05fb09b8fa9";

        public string AdminPassword { get; set; } = "admin";
        public string Technica { get; set; } = "技术支持：江苏同袍科技信息有限公司";

        public string Technicamail { get; set; } = "联系方式：400-889-8805";

        public string MachineCode{ get; set; }= "000";// 是一个约定值
        public string MachineName { get; set; } = "一号机";//没什么实际的意义

    }
}
