using BaseDLL;
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
    public class Related
    {
        public Guid UUID ;
        public string PageType;
        public string transtionId;
        public IDCardData IDCardData;
        public CameraData CameraData;

        public string OCR_Data;
        public string Fix_OCR_Data;

        public void Initialized()
        {
            Clear();
            UUID = Guid.NewGuid();
            PageType = "";
            IDCardData = new IDCardData();
            CameraData = new CameraData();
            transtionId = "";
            OCR_Data = "";
            Fix_OCR_Data = "";
        }
        public void Clear()
        {
            try
            {
                File.Delete(IDCardData.PhotoFileName);
                File.Delete(IDCardData.szPath);
                File.Delete(CameraData.ImageName);
                File.Delete(CameraData.ImageName1);
            }
            catch
            {

            }
        }
    }

    public static class Global
    {
        public static ConfigData Config = new ConfigData() { };
        public static Related Related = new Related();
        public static string IP = Info.IPAdress()[0];
        public static string MAC = Info.MACAdress()[0];

        public static string DisclaimerContent = File.ReadAllText("Base\\打证须知.txt");


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
            OcrRegion = Config.OcrRegion.Split(',');
            PDF.PageChaneX = Config.PageChangeX;
            PDF.PageChaneY = Config.PageChangeY;
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
        public int EstatePort1 { get; set; } = 0;

        public int EstatePort2 { get; set; } = 0;
        public string OcrRegion { get; set; } = "0,0,1280,720";
        public int OcrThreshold { get; set; } = 160;
        public int OcrRotate{ get; set; } = -90;

        public int PageChangeX { get; set; } = 0;
        public int PageChangeY { get; set; } = 0;
        
        public string AdminPassword { get; set; } = "admin";
        public string Technica { get; set; } = "技术支持：江苏同袍科技信息有限公司";

        public string Technicamail { get; set; } = "联系方式：400-889-8805";

        public string MachineCode{ get; set; }= "000";// 是一个约定值
        public string MachineName { get; set; } = "一号机";//没什么实际的意义

        public string GetInfoUrl { get; set; } = "http://127.0.0.1:8080/RealEstateJSQDBDC/fangchan/list";

        public string OCR_UploadUrl { get; set; } = "http://127.0.0.1:8080/RealEstateJSQDBDC/fangchan/backfill";

        public string UploadUrl { get; set; } = "http://127.0.0.1:8080/RealEstateJSQDBDC/fangchan/upload";
    }
}
