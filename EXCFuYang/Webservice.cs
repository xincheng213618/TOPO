using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XinHua
{
    class Webservice
    {
        //自定义的bing 时间很长传递数据很少。
        private static System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding
        {
            MaxReceivedMessageSize = 16777216,
            SendTimeout = TimeSpan.FromSeconds(300),
            ReceiveTimeout = TimeSpan.FromSeconds(300)
        };
        /// <summary>
        /// TOPO Web接口  
        /// </summary>
        public static class NanJing
        {

            private static readonly System.ServiceModel.EndpointAddress NanJingAddresss = new System.ServiceModel.EndpointAddress(Global.Config.NanJingWebservicesUrl);
            private static NanJingWebServices.CreditreportDelegateClient NanJingclient = new NanJingWebServices.CreditreportDelegateClient(binding, NanJingAddresss);

            public static string GetReportList(string IDCardNo = null)
            {
                //zero return NanJingclient.getreportlist(Global.Config.WebRequestUser, Global.Config.WebRequestPassword, IDCardNo, "1");
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<data><returncode>1</returncode><returnmsg>找到符合条件的报告</returnmsg><result><report><id>XYBG-Z20191217-482754F</id><qymc>宁阳县第二实验幼儿园</qymc><tyshxydm>张翠丽</tyshxydm><sqrq>2019-12-17</sqrq></report></result></data>";
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
    }
}
