using BaseUtil;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinHua
{
    public class Http
    {
        private static Dictionary<string, object> PassWdic = new Dictionary<string, object>
            {
                { "chooseType", "printMachineTerminal" },
                { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
            };
        public static string XinHuaSearch(string CompanyName, int pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/search/companyHighSearchByPage";

            var json = new JObject
                {
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "Name", CompanyName }
                };
            string response = Requests.Post(Covert.DicAndUrl(url, PassWdic), json: json);
            return response;
        }



        public static string XinHuaTaxA(string CompanyName, int pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/findPageCompanyA";
            var json = new JObject
                {
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "name", CompanyName }
                };

            string response = Requests.Post(Covert.DicAndUrl(url, PassWdic), json: json);
            return response;
        }

        public static string XinHuaTaxADetail(string CompanyName, string Uniscid = "")
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/queryCompanyA";
            var json = new JObject
                {
                    { "name", CompanyName },
                    { "discernNumber", Uniscid }
                };
            string response = Requests.Post(Covert.DicAndUrl(url, PassWdic), json: json);
            return response;
        }


        public static string XinHuaTaxV(string Name, int pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/findPageQueryDelinquentParty";
            var json = new JObject
                {
                    { "name", Name },
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                };
            string response = Requests.Post(Covert.DicAndUrl(url, PassWdic), json: json);
            return response;
        }
        public static string XinHuaTaxVDetail(string companyName, string companyId)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/queryDelinquentParty";
            var json = new JObject
                {
                    { "companyId", companyId },
                    { "companyName", companyName }
                };
            string response = Requests.Post(Covert.DicAndUrl(url, PassWdic), json: json);
            return response;
        }


        public static string XinHuaDishonestyPeople(string CompanyName, int pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/findPageQueryPerson";
            var json = new JObject
                {
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "name", CompanyName }
                };

            string response = Requests.Post(Covert.DicAndUrl(url, PassWdic), json: json);
            return response;
        }
        public static string XinHuaLostCreditDetail(string companyName, string companyId)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/radBlackList/queryPerson";
            var json = new JObject
                {
                    { "companyId", companyId },
                    { "name", companyName }
                };
            string response = Requests.Post(Covert.DicAndUrl(url, PassWdic), json: json);
            return response;
        }
        //基本信息
        public static string CompanyBasicInfo(string companyId)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getCompanyInfo";
            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo", "1" },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };

            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        public static string Investment(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getOperationInvestInfoByPage";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };

            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Partners(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getCompanyPartnersByPage";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };

            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Branch(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getCompanyBranchesByPage";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };

            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Referee(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getAssetCaseByPage";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };

            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        public static string Announcement(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getAssetChinacourtByPage";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo", pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };

            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;

        }

        public static string PatentInfo(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getAssetPatentDetailByPage";
            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo",pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        public static string SoftwareCopyRightUrl(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getOperationSoftwareCopyrightsByPage";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo",pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Trademark(string companyId, string pageNo)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/baseMsg/getAssetTmInfoByPage";
            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "companyId", companyId },
                    { "pageNo",pageNo },
                    { "pageSize", "10" },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string XinHuaReport(string companyName, string companyId)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/report/createReportByQi";
            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" },
                    { "reportType",1 },
                    { "companyId", companyId},
                    { "companyName", companyName }
                };
            string response = Requests.Get(url, dic: dic, timeout: 120000);
            return response;
        }
        /// <summary>
        /// 新华报告
        /// </summary>
        /// <param name="path"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static bool GetXinHuaReport(string path, string FilePath)
        {
            string url = "http://gdintegrity.credit100.com/xhsCreditApi/v1/enterprise/detail/xhsRest/report/downloadReport";
            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "path" ,path },
                    { "chooseType", "printMachineTerminal" },
                    { "machineKey", "175D9A78D74D11E8A42C005056B4753D" },
                }; ;
            return Requests.Downloade(Covert.DicAndUrl(url, dic), FilePath);
        }


        public static bool GetCreditchinaReport(string CompanyName,string USCI,string FileName)
        {
            string url = "https://public.creditchina.gov.cn/credit-check/pdf/download";

            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                { "companyName" ,CompanyName },
                { "tyshxydm", USCI },
                { "entityType", "1" }
            };
            return Requests.Downloade(Covert.DicAndUrl(url, dic), FileName);
        }

    }
}
