using BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXC
{
    public static class Webservice
    {
        //自定义的bing 时间很长传递数据很少。
        private static System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding
        {
            MaxReceivedMessageSize = 16777216,
            SendTimeout = TimeSpan.FromSeconds(300),
            ReceiveTimeout = TimeSpan.FromSeconds(300)
        };
        //南京
        public static class NingYang
        {
            private static readonly System.ServiceModel.EndpointAddress NingYangAddresss = new System.ServiceModel.EndpointAddress(Global.Config.NingYangWebservicesUrl);
            private static readonly NingYangWebservices.CreditreportDelegateClient NingYangclient = new NingYangWebservices.CreditreportDelegateClient(binding, NingYangAddresss);

            public static string GetReportGR(string IDCardNo = null)
            {
                return NingYangclient.getreportGR(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "1");
            }

            public static string GetReportList(string IDCardNo = null)
            {
                return NingYangclient.getreportlist(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "1");
            }

            public static string GetReport(string FileName = null, string USCCode = null, string type = "0")
            {
                try
                {
                    return NingYangclient.getreport(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, FileName, USCCode, type);
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message);
                    return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
                }
            }
        }

        /// <summary>
        /// TOPO Web接口  
        /// </summary>
        public static class NanJing
        {

            private static readonly System.ServiceModel.EndpointAddress NanJingAddresss = new System.ServiceModel.EndpointAddress("http://58.213.162.194:5444/WinHall/services/creditreport?wsdl");
            private static NingYangWebservices.CreditreportDelegateClient NanJingclient = new NingYangWebservices.CreditreportDelegateClient(binding, NanJingAddresss);

            //private static string RequestUser = "ckdtzzj001";
            //private static string Password = "3m50ekz8";

            public static string GetReportList(string IDCardNo = null)
            {
                return NanJingclient.getreportlist(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "1");
            }
            /// <summary>
            /// 企业信用报告
            /// </summary>
            /// <param name="ReportID"></param>
            /// <returns></returns>
            public static string GetReport(string ReportID = null)
            {
                return NanJingclient.getreport(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, ReportID, null, "1");
            }

            //自然人结果信息
            public static string NaturalPerson(string Name = null, string IDCardNo = null, string Type = null)
            {
                return NanJingclient.getZrrxyxxList(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, Name, IDCardNo, Type);
            }

            public static string NaturalPersonDetail(string Name = null, string IDCardNo = null, string Type = null)
            {
                return NanJingclient.getZrrxyxx(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, Type);
            }
            public static string PersonReport(string Name = null, string IDCardNo = null, string Type = null)
            {
                return NanJingclient.getZrrxyxx(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, Type);
            }

        }

        /// <summary>
        /// 新泰Web接口
        /// </summary>
        public static class XinTai
        {
            private static readonly System.ServiceModel.EndpointAddress XinTaiAddress = new System.ServiceModel.EndpointAddress(Global.Config.XinTaiWebserviecsUrl);
            private static XinTaiWebServices.CreditreportDelegateClient XinTaiclient = new XinTaiWebServices.CreditreportDelegateClient(binding, XinTaiAddress);

            public static string GetReportList(string IDCardNo = null)
            {
                try
                {
                    return XinTaiclient.getreportlist(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "1");
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message);
                    return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
                }
            }

            public static string GetReport(string FileName = null, string USCCode = null, string type = "1")
            {
                try
                {
                    return XinTaiclient.getreport(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, FileName, USCCode, "0");
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message);
                    return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
                }
            }

            public static string GetReportGR(string IDCardNo = null, string Name = null)
            {
                try
                {
                    return XinTaiclient.getreportGR(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "2");
                }
                catch (Exception ex)
                {
                    return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
                }
            }


        }

        /// <summary>
        /// 潍坊Web接口
        /// </summary>
        public static class WeiFang
        {

            private static readonly System.ServiceModel.EndpointAddress WeiFangAddress = new System.ServiceModel.EndpointAddress(Global.Config.WeiFangWebserviecsUrl);
            private static XinTaiWebServices.CreditreportDelegateClient WeiFangclient = new XinTaiWebServices.CreditreportDelegateClient(binding, WeiFangAddress);

            /// <summary>
            /// 自然人信息查询
            /// </summary>
            /// <param name="IDCardNo"></param>
            /// <returns></returns>
            public static string GetPersonInfo(string IDCardNo = null)
            {
                return WeiFangclient.getZrrxyxx(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "2");
            }
            public static string GetReportList(string IDCardNo = null)
            {
                return WeiFangclient.getreportGR(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "2");
            }

        }

        public static class QingDao
        {
            private static readonly System.ServiceModel.EndpointAddress QingDaoAddress = new System.ServiceModel.EndpointAddress("http://120.221.95.16:8080/CreditReportWebservice/FRReportToBase64.asmx");
            private static QingDao1.FRReportToBase64SoapClient QingDaoclient = new QingDao1.FRReportToBase64SoapClient(binding, QingDaoAddress);
            private static string APIKEY = "11BFF487-86CB-4196-906E-F28B6CB0F2E7";
            public static string GetReport(string MC = "", string DM = "")
            {
                string response = QingDaoclient.GetReport(APIKEY, MC, DM);
                return response;
            }

        }
        ////舟山代码
        //public static class ZhouShan
        //{

        //    private static readonly System.ServiceModel.EndpointAddress ZhouShanAddress = new System.ServiceModel.EndpointAddress(Global.configData.ZhouShanWebserviecsUrl);
        //    private static XinTaiWebServices.CreditreportDelegateClient ZhouShanclient = new XinTaiWebServices.CreditreportDelegateClient(binding, ZhouShanAddress);

        //    /// <summary>
        //    /// 自然人信息查询
        //    /// </summary>
        //    /// <param name="CompanyName"></param>
        //    /// <returns></returns>
        //    public static string ComPanyList(string CompanyName = null)
        //    {
        //        return ZhouShanclient.getQyxyxx(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, CompanyName, "");
        //    }
        //    public static string ComPanyInfo(string CompanyName = null)
        //    {
        //        string response = ZhouShanclient.getQyjbxx(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, CompanyName, "");
        //        return response;
        //    }
        //}
        //黄石接口调用
        public static class HuangShi
        {
            private static readonly System.ServiceModel.EndpointAddress HuangShiAddress = new System.ServiceModel.EndpointAddress(Global.Config.HuangShiWebserviecsUrl);
            private static XinTaiWebServices.CreditreportDelegateClient HuangShiclient = new XinTaiWebServices.CreditreportDelegateClient(binding, HuangShiAddress);

            /// <summary>
            /// 自然人信息查询
            /// </summary>
            /// <param name="CompanyName"></param>
            /// <returns></returns>
            public static string ComPanyList(string CompanyName = null)
            {
                return HuangShiclient.getQyxyxx(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, CompanyName, "");
            }
            public static string ComPanyInfo(string CompanyName = null)
            {
                string response = HuangShiclient.getQyjbxx(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, CompanyName, "");
                return response;
            }
            public static string GetReportList(string IDCardNo = null)
            {
                try
                {
                    return HuangShiclient.getreportlist(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "1");
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message);
                    return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
                }
            }
            public static string GetReport(string FileName = null, string USCCode = null, string type = "1")
            {
                try
                {
                    return HuangShiclient.getreport(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, FileName, USCCode, "0");
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message);
                    return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
                }
            }

            public static string GetReportGR(string IDCardNo = null, string Name = null)
            {
                try
                {
                    return HuangShiclient.getreportGR(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "2");
                }
                catch (Exception ex)
                {
                    return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
                }
            }
        }


        ////济宁
        //public static class JiNing
        //{
        //    private static readonly System.ServiceModel.EndpointAddress JiNingAddress = new System.ServiceModel.EndpointAddress(Global.configData.HuangShiWebserviecsUrl);
        //    private static XinTaiWebServices.CreditreportDelegateClient JiNingclient = new XinTaiWebServices.CreditreportDelegateClient(binding, JiNingAddress);

        //    /// <summary>
        //    /// 自然人信息查询
        //    /// </summary>
        //    /// <param name="CompanyName"></param>
        //    /// <returns></returns>
        //    public static string ComPanyList(string CompanyName = null)
        //    {
        //        return JiNingclient.getQyxyxx(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, CompanyName, "");
        //    }
        //    public static string ComPanyInfo(string CompanyName = null)
        //    {
        //        string response = JiNingclient.getQyjbxx(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, CompanyName, "");
        //        return response;
        //    }
        //    public static string GetReportList(string IDCardNo = null)
        //    {
        //        try
        //        {
        //            return JiNingclient.getreportlist(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, IDCardNo, "1");
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Write(ex.Message);
        //            return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
        //        }
        //    }
        //    public static string GetReport(string FileName = null, string USCCode = null, string type = "1")
        //    {
        //        try
        //        {
        //            return JiNingclient.getreport(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, FileName, USCCode, "0");
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Write(ex.Message);
        //            return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
        //        }
        //    }

        //    public static string GetReportGR(string IDCardNo = null, string Name = null)
        //    {
        //        try
        //        {
        //            return JiNingclient.getreportGR(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, IDCardNo, "2");
        //        }
        //        catch (Exception ex)
        //        {
        //            return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
        //        }
        //    }
        //}

        ////基本已经完成，在做到后期没有办法制作了，
        //public static class ShenZhen
        //{
        //    private static readonly System.ServiceModel.EndpointAddress ShenZhenAddress = new System.ServiceModel.EndpointAddress(Global.configData.HuangShiWebserviecsUrl);
        //    private static XinTaiWebServices.CreditreportDelegateClient ShenZhenclient = new XinTaiWebServices.CreditreportDelegateClient(binding, ShenZhenAddress);


        //    public static string CompanyDetail(string CompanyId, string type = "1")
        //    {
        //        try
        //        {
        //            return ShenZhenclient.getQyjbxx(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, CompanyId, type);
        //        }
        //        catch (Exception ex)
        //        {
        //            return $"<data><returncode>e</returncode><returnmsg>{ex.Message}</returnmsg></data>";
        //        }
        //    }
        //}

    }
}
