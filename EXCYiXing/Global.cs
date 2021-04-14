using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EXCYiXing
{
        public class Global
        {
            //页面状态，用来判断转跳s
            public static string PageType = null;
            public static string IP = Info.IPAdress()[0];
            public static string MAC = Info.MACAdress()[0];


        public static string HomeError = null;
        public static IDCardData iDCard = new IDCardData();

            public static void Initialized()
            {
                ReadConfigData();
            }

            // 调用数据
            public static ConfigData configData = new ConfigData();

            private static void WriteConfigFile(string sFile)
            {
                XmlSerializer ser = new XmlSerializer(typeof(ConfigData), typeof(ConfigData).GetNestedTypes());
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
                    MessageBox.Show("找不到配置文件");
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


        [Serializable]
        [XmlSerializerAssembly("EXCResources.XmlSerializers")]
        public class ConfigData
        {
            #region 基本信息
            //使用单位信息
            public string LocateUser { get; set; }
            //终端名称
            public string TerminalName { get; set; }
            // 技术支持人员和电话
            public string Technica { get; set; }
            public string Technicamail { get; set; }


            public int PrintDelay { get; set; }
            #endregion

            #region 配置类信息

            //setting 页面开启(0 开，其他关)
            public string SettingOptimiz { get; set; }
            //证明打印开关
            public bool PrintOptimize { get; set; }

            //摄像头验证错误次数
            public int CameraTryCount { get; set; }

             public int PstampOpen { get; set; }


            //管理员密码
             public string Adminpassword { get; set; }
            //缓存清理日期
            public int CacheClearDay { get; set; }

            #endregion

            #region WebService 地址
            public string WebRequestUser { get; set; }
            public string WebRequestPassword { get; set; }
            //宁阳
            public string NingYangWebservicesUrl { get; set; }
            //诸城
            public string ZhuChengWebservicesUrl { get; set; }
            //深圳
            public string ShenZhenWebservicesUrl { get; set; }
            //青岛
            public string QingDaoWebservicesUrl { get; set; }
            //南京
            public string NanJingWebserviecsUrl { get; set; }
            //新泰
            public string XinTaiWebserviecsUrl { get; set; }
            //潍坊
            public string WeiFangWebserviecsUrl { get; set; }
            //黄石
            public string HuangShiWebserviecsUrl { get; set; }
            //舟山
            public string ZhouShanWebserviecsUrl { get; set; }
            #endregion

            #region Http 地址
            //青岛
            public string QingDaoLegalPersonUrl { get; set; }

            #region 苏州
            public string AreaCodes { get; set; }
            public string AddActionUrl { get; set; }
            //十区无房证明
            public string NoHomeUrl { get; set; }
            //六区无房证明
            public string NoHomeSixUrl { get; set; }
            //十区房屋套数
            public string HomeCountUrl { get; set; }
            //十区权属证明
            public string OwnerShipUrl { get; set; }
            //资金管理
            public string MoneryUrl { get; set; }
            //进程管理
            public string ProcessUrl { get; set; }
            //档案列表
            public string SZArchiveUrl { get; set; }
            //档案菜单
            public string SZArchiveListUrl { get; set; }
            public string SZArchiveMenuUrl { get; set; }

            public string ShowImage { get; set; }
            #endregion



            #region 合肥
            public string HeiFeiListUrl { get; set; }
            public string HeifeiReportUrl { get; set; }
            public string HeifeiGRReportUrl { get; set; }

            #endregion

            #region 宜兴
            public string YiXingListUrl { get; set; }
            public string YiXingPersonUrl { get; set; }
            public string YiXingBankUrl { get; set; }
            #endregion

            #endregion


            #region 盖章机调用

            #region 新盖章机基本参数
            //端口
            public int PStampPort { get; set; }
            //设置一次过纸页数 默认1
            public int PStampTiPage { get; set; }
            //设置盖章在左还是右 0右，1左
            public int PStampTiLeftCount { get; set; }
            //设置盖章的上下位置
            public int PStampTiPos { get; set; }
            #endregion

            #region 旧盖章机基本参数
            public int StampPort { get; set; }
            public int ILeftFormatNum { get; set; }
            public int IRightFormatNum { get; set; }
            public int IPaperNumber { get; set; }
            public int NLeftPosOne { get; set; }
            public int NLeftPosTwo { get; set; }
            public int NLeftPosThree { get; set; }
            public int NRightPosOne { get; set; }
            public int NRightPosTwo { get; set; }
            public int NRightPosThree { get; set; }
            public int NLeftPosExOne { get; set; }
            public int NLeftPosExTwo { get; set; }
            public int NRightPosExOne { get; set; }
            public int NRightPosExTwo { get; set; }
            #endregion

            #endregion
        }



}
