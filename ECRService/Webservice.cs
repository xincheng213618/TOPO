﻿using BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECRService
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
        public static class NanJing
        {
            private static readonly System.ServiceModel.EndpointAddress NanJingAddresss = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
            private static readonly ReportServiceReference.ReportServiceClient NanJingClient = new ReportServiceReference.ReportServiceClient(binding, NanJingAddresss);

            //public static string GetReportGR(string IDCardNo = null)
            //{
            //    return NanJingClient.getreportGR(Global.configData.WebRequestUser, Global.configData.WebRequestPassword, IDCardNo, "1");
            //}

            public static string GetReportList(string IDCardNo = "",string Kinds="")
            {
                string response = NanJingClient.getreportlist(Global.Config.LoginName, Global.Config.LoginPassword, IDCardNo, Kinds);
                // string response = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><data><returncode>1</returncode><returnmsg>调用成功</returnmsg><result><report><id>ABC0000000001</id><qymc>江苏同袍信息科技有限公司</qymc><sqrq>2017-09-09</sqrq></report></result></data>";

                return response;
            }

            public static string GetReport(string FileName = null, string USCI = null, string Kinds = "0")
            {
                return NanJingClient.getreport(Global.Config.LoginName, Global.Config.LoginPassword, FileName, USCI, Kinds);
            }
            public static void updatereportstatus(string Reportid,string Kinds)
            {
                NanJingClient.updatereportstatus(Global.Config.LoginName, Global.Config.LoginPassword, Reportid,"", Kinds);
            }
        }


    }
}
