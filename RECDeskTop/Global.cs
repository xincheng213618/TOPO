using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace REC
{
    public static class Global
    {
        public static string PageType = null;
        public static ConfigData Config = new ConfigData()
        {
            EstatePort1 = 0,
            EstatePort2 = 0,
            AdminPassword = "admin",
        };
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

    [Serializable]
    public class ConfigData
    {
        public int EstatePort1 { get; set; }

        public int EstatePort2 { get; set; }

        public string AdminPassword { get; set; }

        public string GetInfoUrl { get; set; }

    }
}
