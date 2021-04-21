using BaseDLL;
using BaseUtil;
using System;
using System.IO;
using System.Xml.Serialization;

namespace EXC
{
    public class Related
    {
        public Guid UUID;
        public string PageType = "";
        public IDCardData IDCardData ;


        public void Initialized()
        {
            UUID = Guid.NewGuid();
        }
     
    }
    public static class Global
    {
        public static ConfigData Config = new ConfigData();
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
            string sPath = "Temp";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
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


    //http://10.33.1.210:8800/xycx-web/creditSearch/pdfDownloadTUser.do


    [Serializable]
    public class ConfigData
    {
        public int BackgroundWindow { get; set; } = 0; //终端机不明确窗口

        public string LocateUser { get; set; } = "合肥信用报告终端打印一体机";
        //终端名称
        public string TerminalName { get; set; } = "一号机";
        // 技术支持人员和电话
        public string Technica { get; set; } = "技术支持：江苏同袍科技信息有限公司";

        public string Technicamail { get; set; }= "联系方式：400-889-8805";

        public string AdminPassword { get; set; } = "admin";

        public string HeiFeiListUrl { get; set; } = "http://10.33.1.164:8088/WinHall-HeFei/vxybgbsqrxx/list";
        public string HeifeiReportUrl { get; set; } = "http://10.33.1.164:8088/WinHall-HeFei/vxybgbsqrxx/pdfdownloadt";
        public string HeifeiGRReportUrl { get; set; } = "http://10.33.1.210:8800/xycx-web/creditSearch/pdfDownloadTUser.do";
        public string HeifeiPrintSave { get; set; } = "http://10.33.1.164:8088/WinHall-HeFei/server/save";
        public bool FunctionOpenGR { get; set; } = false;
        public bool FunctionOpenReportHeFei { get; set; } = true;
        public bool FunctionOpenReportHeFei1 { get; set; } = true;




    }
}
