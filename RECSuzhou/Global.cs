using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RECSuzhou
{

    public static class Global
    {
        public static Config Config = new Config();
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
                XmlSerializer serializer = new XmlSerializer(typeof(Config), typeof(Config).GetNestedTypes());
                Config = serializer.Deserialize(stream) as Config;

            }
            catch
            {
                Environment.Exit(0);
            }
        }
        public static void WriteConfigFile(string sFile)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Config), typeof(Config).GetNestedTypes());
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
            IDCardData = new IDCardData();
            PageType = "";
        }
    }


    [Serializable]
    public class Config
    {
        //使用单位信息
        public string LocateUser { get; set; } = "苏州不动产自助查询终端";
        //终端名称
        public string TerminalName { get; set; } = "一号机";
        // 技术支持人员和电话
        public string Technica { get; set; }
        public string Technicamail { get; set; }
        public string PDFJiaZai { get; set; }

        //setting 页面开启(0 开，其他关)
        public string SettingOptimiz { get; set; } = "0";


        //设置查询地区
        public string SuZhouGuSu { get; set; }
        public string SuZhouHQAll { get; set; }
        public string SuZhouWZAll { get; set; }
        //进入设置页面需要的密码
        public string AdminPassword { get; set; } = "admin";


        #region 苏州
        public string AreaCodes { get; set; } = "320505,320506,320507,320508,320509,320513,320581,320582,320583,320585";
        public string AddActionUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/sc/adddongzuo.do";
        //十区无房证明
        public string NoHomeUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/sc/findAllAreaForNoRoom";
        //六区无房证明
        public string NoHomeSixUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/sc/findAllAreaForNoRoom";
        //十区房屋套数
        public string HomeCountUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/quanshu/isExitHouse.do";
        //十区权属证明
        public string OwnerShipUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/houseInfo/all";
        //资金管理
        public string MoneryUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/moneyProgress/progress.do";
        //进程管理
        public string ProcessUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/sc/chaxjindu.do";
        //档案列表
        public string SZArchiveListUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/sc/chaxdanganlist.do";
        //档案内容
        public string SZArchiveUrl { get; set; } = "http://10.5.27.190:8512/RealEstateSZXQBDC/app/sc/chaxdangan.do";

        public string SZArchiveMenuUrl { get; set; } = "http://10.5.0.232:8500/RealEstateSZGSBDC/app/sc/chaxdangan.do";

        public string ShowImage { get; set; } = "http://192.168.0.254:8512";
        #endregion


        #region 盖章机调用

        public int PstampOpen { get; set; } = 0;

        #region 新盖章机基本参数
        /// <summary>
        /// 端口
        /// </summary>
        public int PStampPort { get; set; } = 1;
        /// <summary>
        /// 设置一次过纸页数 默认1
        /// </summary>
        public int PStampTiPage { get; set; } = 1;
        /// <summary>
        /// 设置盖章在左还是右 0右，1左
        /// </summary>
        public int PStampTiLeftCount { get; set; } = 0;
        /// <summary>
        /// 设置盖章的上下位置
        /// </summary>
        public int PStampTiPos { get; set; } = 230;
        #endregion

        #region 旧盖章机基本参数
        /// <summary>
        /// 端口
        /// </summary>
        public int StampPort { get; set; } = 1;
        /// <summary>
        /// 左边盖章机 0表示不盖章
        /// </summary>
        public int ILeftFormatNum { get; set; } = 0;
        /// <summary>
        /// 右边盖章机 0表示不盖章
        /// </summary>
        public int IRightFormatNum { get; set; } = 10;
        /// <summary>
        /// 固定盖章个数
        /// </summary>
        public int IPaperNumber { get; set; } = 5;
        public int NLeftPosOne { get; set; } = 180;
        public int NLeftPosTwo { get; set; } = 820;
        public int NLeftPosThree { get; set; } = 1460;
        public int NRightPosOne { get; set; } = 180;
        public int NRightPosTwo { get; set; } = 820;
        public int NRightPosThree { get; set; } = 1460;
        public int NLeftPosExOne { get; set; } = 531;
        public int NLeftPosExTwo { get; set; } = 1486;
        public int NRightPosExOne { get; set; }=180;
        public int NRightPosExTwo { get; set; } = 1486;
        #endregion

        #endregion

    }
}
