using Newtonsoft.Json.Linq;
using System;
using BaseDLL;

namespace EXC
{
    // 这里调用WebService
    public class WebService
    {
        //这里修改自定义的封装地址
        private static System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding
        {
            MaxReceivedMessageSize = 16777216,
            SendTimeout = TimeSpan.FromSeconds(300),
            ReceiveTimeout = TimeSpan.FromSeconds(300)
        };

        private static readonly System.ServiceModel.EndpointAddress WeiHaiAddress = new System.ServiceModel.EndpointAddress("http://60.212.191.48:8704/credit-webservice-portal/webservice/creditreport?wsdl");
        private static ServiceReference.CreditReportClient WeiHaiClient = new ServiceReference.CreditReportClient(binding, WeiHaiAddress);

        public static JObject GetLegalPersonPage(string SearchInfo, int PageNo, int PageSize)
        {
            if (PageSize > 10)
                PageSize = 10;
            JObject Resposne = new JObject { };

            JObject json = new JObject
                {
                    { "qymc", SearchInfo},
                    { "pageNumber", PageNo },
                    { "pageSize", PageSize }
                };

            try
            {
                ServiceReference.jxCommonRequest arg = new ServiceReference.jxCommonRequest
                {
                    jsonStringParams = json.ToString()
                };

                ServiceReference.jxCommonResponse jxCommonResponse = WeiHaiClient.getLegalPersonPage(arg);
                Resposne.Add("code", "0");
                Resposne.Add("msg", "接口正常");
                Resposne.Add("data", jxCommonResponse.detail[0].value.ToString());
            }
            catch (Exception ex)
            {
                Resposne.Add("code", "-1");
                Resposne.Add("msg", $"接口异常：{ex.Message}");
                Resposne.Add("data", null);
            }
            return Resposne;
        }

        public static JObject ExportLegalPersonalPdf(IDCardData iDCardData, string xybsm, string templateid, string CompanyName = null, string USCI = null)
        {
            JObject Resposne = new JObject { };

            JObject json = new JObject
                {
                    {"xm",iDCardData.Name},
                    {"qymc", CompanyName},
                    {"tyshxydm",USCI},
                    {"xybsm",xybsm},
                    {"templateid",templateid}
                };
            try
            {
                ServiceReference.jxCommonRequest arg = new ServiceReference.jxCommonRequest
                {
                    jsonStringParams = json.ToString()
                };

                ServiceReference.jxCommonResponse jxCommonResponse = WeiHaiClient.exportlegalpersonalpdf(arg);
                Resposne.Add("code", "0");
                Resposne.Add("msg", "接口正常");
                Resposne.Add("data", jxCommonResponse.detail[0].value.ToString());
            }
            catch (Exception ex)
            {
                Resposne.Add("code", "-1");
                Resposne.Add("msg", $"接口异常：{ex.Message}");
                Resposne.Add("data", null);
            }
            return Resposne;
        }

        public static JObject GetReportTemplate()
        {
            JObject Resposne = new JObject { };
            try
            {
                ServiceReference.jxCommonRequest arg = new ServiceReference.jxCommonRequest
                {
                    jsonStringParams = "{\"pageNumber\":\"1\",\"pageSize\":\"10\"}"
                };
                ServiceReference.jxCommonResponse jxCommonResponse = WeiHaiClient.getreporttemplate(arg);
                Resposne.Add("code", "0");
                Resposne.Add("msg", "接口正常");
                Resposne.Add("data", jxCommonResponse.detail[0].value.ToString());
            }
            catch (Exception ex)
            {
                Resposne.Add("code", "-1");
                Resposne.Add("msg", $"接口异常：{ex.Message}");
                Resposne.Add("data", null);
            }
            return Resposne;
        }

        public static JObject Exportpersonalpdf(IDCardData iDCardData, string templateid = "10")
        {
            JObject Resposne = new JObject { };
            JObject json = new JObject
                {
                    { "xm",iDCardData.Name},
                    {"sfzh", iDCardData.IDCardNo},
                    { "templateid",templateid}
                };
            try
            {
                ServiceReference.jxCommonRequest arg = new ServiceReference.jxCommonRequest
                {
                    jsonStringParams = json.ToString()
                };
                ServiceReference.jxCommonResponse jxCommonResponse = WeiHaiClient.exportpersonalpdf(arg);
                Resposne.Add("code", "0");
                Resposne.Add("msg", "接口正常");
                Resposne.Add("data", jxCommonResponse.detail[0].value.ToString());
            }
            catch (Exception ex)
            {
                Resposne.Add("code", "-1");
                Resposne.Add("msg", $"接口异常：{ex.Message}");
                Resposne.Add("data", null);
            }
            return Resposne;
        }

        public static JObject GetPersonInfo(IDCardData iDCardData)
        {
            JObject Resposne = new JObject { };

            JObject json = new JObject
                {
                    { "xm", iDCardData.Name },
                    { "sfzh", iDCardData.IDCardNo},
                    { "xb", iDCardData.Sex },
                    { "mz", iDCardData.Nation},
                    { "zz", iDCardData.Address },
                    { "csrq", iDCardData.Born },
                    { "fzjg", iDCardData.GrantDept },
                    { "ksrq", iDCardData.UserLifeBegin },
                    { "jzrq", iDCardData.UserLifeEnd },
                    { "zzlx", iDCardData.CardType },
                    { "qt", iDCardData.reserved }
                };
            try
            {
                ServiceReference.jxCommonRequest arg = new ServiceReference.jxCommonRequest
                {
                    jsonStringParams = json.ToString()
                };
                ServiceReference.jxCommonResponse jxCommonResponse = WeiHaiClient.getpersoninfo(arg);
                Resposne.Add("code", "0");
                Resposne.Add("msg", "接口正常");
                Resposne.Add("data", jxCommonResponse.detail[0].value.ToString());
            }
            catch (Exception ex)
            {
                Resposne.Add("code", "-1");
                Resposne.Add("msg", $"接口异常：{ex.Message}" + Environment.NewLine + "请联系管理人员");
                Resposne.Add("data", null);
            }
            return Resposne;
        }
    }
}
