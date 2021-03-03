using System;
using System.Xml.Serialization;

namespace Resources
{
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
