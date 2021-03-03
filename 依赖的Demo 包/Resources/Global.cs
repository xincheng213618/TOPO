using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Resources
{
    public class Global
    {
        //页面状态，用来判断转跳s
        public static string PageType = null;



        public static class Config
        {
            public static string IP = XInfo.IPAdress()[0];
            public static string MAC = XInfo.MACAdress()[0];
        }

        public static void Initialized()
        {
            ReadConfigData();
            //获取当前ip和mac 信息

            //清理缓存
            XFile.CacheDelete("Temp", Global.configData.CacheClearDay);
            XFile.CacheDelete("Logs", 90);
        }


        // 调用数据
        public static ConfigData configData = new ConfigData();

        private static void WriteConfigFile(string sFile)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ConfigData),typeof(ConfigData).GetNestedTypes());
            FileStream fs = File.Create(sFile);
            ser.Serialize(fs, configData);
            fs.Close();
        }

        //更新数据
        private static void UpdateConfigFile(string sFile)
        {
            XDocument Doc = XDocument.Load(sFile);

            WriteConfigFile("temp.xml");
            XDocument Doc1 = XDocument.Load("temp.xml");
            foreach (var col in Doc.Element("ConfigData").Elements())
            {
                Doc.Element("ConfigData").Element(col.Name).SetValue(Doc1.Element("ConfigData").Element(col.Name).Value);
            }

            Doc.Save(sFile);
            File.Delete("temp.xml");
        }

        public static void ReadConfigData()
        {
            try
            {
                //此调用方法多次运行会产生内存泄漏,参见 https://stackoverflow.com/questions/1127431/xmlserializer-giving-filenotfoundexception-at-constructor
                using (var stream = File.OpenRead("Config"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigData), typeof(ConfigData).GetNestedTypes());
                    configData = serializer.Deserialize(stream) as ConfigData;
                }
            }
            catch
            {  
                Environment.Exit(0);
            }
        }


        public static readonly string RequestUser = "Das4D24fGh";
        public static readonly string RequestPassword = "e10adc3949ba59abbe56e057f20f883e";


        //商标状态
        public static string BrandStatus(string type)
        {
            string status = "";
            if ("1".Equals(type))
            {
                status = "待审";
            }
            if ("2".Equals(type))
            {
                status = "不定";
            }
            if ("3".Equals(type))
            {
                status = "有效";
            }
            if ("4".Equals(type))
            {
                status = "不定";
            }
            return status;
        }

        //商标类型

    }

    public static class IDCardInfo
    {
        public static string Name = null;      //姓名   
        public static string Sex = null;       //性别
        public static string Nation = null;    //民族
        public static string Born = null;      //出生日期
        public static string Address = null;   //住址
        public static string IDCardNo = null;  //身份证号
        public static string GrantDept = null; //发证机关
        public static string UserLifeBegin = null; // 有效开始日期
        public static string UserLifeEnd = null;   // 有效截止日期
        public static string PassID = null;        //通行证号码
        public static string IssuesTimes = null;   //签发次数
        public static string reserved = null;      // 保留
        public static string PhotoFileName = null;// 照片路径   
        public static string CardType = null;// 证件类型     
        public static string EngName = null;// 英文名      
        public static string CertVol = null;// 证件版本号                
        public static string szPath = null;// 证件版本号  

        public static string Update = null;

        public static byte IDCardPicture;
        public static byte Picture1;
        public static byte Picture2;


        public static void Initialized()
        {
            Name = null;
            Sex = null;
            Nation = null;
            Born = null;
            Address = null;
            IDCardNo = null;
            GrantDept = null;
            UserLifeBegin = null;
            UserLifeEnd = null;
            PassID = null;
            IssuesTimes = null;
            reserved = null;
            PhotoFileName = null;
            CardType = null;
            EngName = null;
            CertVol = null;
            szPath = null;
            Update = null;
        }
    }
    public static class CompanyInfo
    {
        public static string CompanyID = null;
        public static string CompanyName = null;
        public static void Initialized()
        {
            CompanyID = null;
            CompanyName = null;
        }

    }

    public class Times: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int countdown =59;
        public int Countdown
        {
            get { return countdown; }
            set { countdown = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Countdown")); }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date")); }
        }

        public string LocateUser { get {return Global.configData.LocateUser; } set { Global.configData.LocateUser = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LocateUser")); } } 
        public string TerminalName { get { return Global.configData.TerminalName; } }
        public string Technica { get { return Global.configData.Technica; } }
        public string Technicamail { get { return Global.configData.Technicamail; } }

        private string buttonClor = "#3668d3";

        public string ButtonClor
        {
            get { return buttonClor; }
            set { buttonClor = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonClor"));  }
        }
    }

    public class TimeCount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int countdown = 59;
        public int Countdown
        {
            get { return countdown; }
            set
            {
                countdown = value;
                if (PropertyChanged != null)
                {
                    Show = Countdown.ToString() + "秒后自动返回";
                    PropertyChanged(this, new PropertyChangedEventArgs("Countdown"));
                }
            }

        }
        


        private string show;
        public string Show
        {
            get { return show; }
            set { show = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Show")); }
        }

    }






    public class Media
    {
        public static string[] MediaList = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Media\\");

        public static void Player(string path = null, int listnum = 0)
        {
            if (path == null)
            {
                path = MediaList[listnum];
            }
            MediaPlayer media = new MediaPlayer();
            media.Open(new Uri(path, UriKind.RelativeOrAbsolute));
            media.Position = TimeSpan.Zero;
            media.Play();
        }
    }

    //List 相关数据
    public class ListInfo
    {

    }


}
