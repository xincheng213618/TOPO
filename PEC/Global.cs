using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Xml.Serialization;

namespace PEC
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

        public void Initialized()
        {
            UUID = Guid.NewGuid().ToString();
            PageType = "";
            IDCardData = new IDCardData();
        }

    }




    [Serializable]
    public class ConfigData
    {
        public string SN { get; set; }
        public int BackgroundWindow { get; set; } = 0;
        //终端名称
        public string TerminalName { get; set; } 
        // 技术支持人员和电话
        public string Technica { get; set; } = "江苏同袍科技有限责任公司";
        public string Technicamail { get; set; }
        public string AdminPassword { get; set; } = "admin";
        public string LoadAnimation { get; set; } = "true";
        public string PrintTipWindows { get; set; } = "B13";
        public string ZzjCode { get; set; } = "3200001";
        //大华登录
        public string DALoginUrl { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/authlocal/getfrdata";
        public string DAListUrl { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/authlocal/getfrcompany";
        public string DAIDcardLoginUrl { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/authlocal/getfrdatasfz";
        public string DAMsgVerifyurl { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/authlocal/getfrdatamsg";
        public string DASendMsgUrl { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/authlocal/sendmsg";

        //莱斯数据
        public string LSReportUrl { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/report/getreport";
        public string LSReporttemp { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/report/gettemp";

        public string LSQRreportUrl { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/report/smreport";
        //扫码取报告
        public string LSScanUrl { get; set; }
        //个人信用报告
        public string GRReport { get; set; } = "http://172.23.11.165:19090/WinHall-JSSCredit/report/getnaturalperson";

    }

    
}
