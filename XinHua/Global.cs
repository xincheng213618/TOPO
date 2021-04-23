using BaseDLL;
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
        
        public static ConfigData Config = new ConfigData();
        public static string IP = Info.IPAdress()[0];
        public static string MAC = Info.MACAdress()[0];
        public static Related Related = new Related();

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

    [Serializable]
    public class ConfigData
    {
        //使用单位信息
        public string LocateUser { get; set; } = "长春市信用服务区信用终端机";
        //终端名称
        public string TerminalName { get; set; } = "一号机"; 
        // 技术支持人员和电话
        public string Technica { get; set; } = "技术支持：中国经济信息社新华信用";
        public string Technicamail { get; set; } = "联系方式：18811799008 18943789380";


        public string AdminPassword { get; set; } = "admin";

    }
}
