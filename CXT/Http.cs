using BaseUtil;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace XinHua
{
    public class Http
    {
        //Xinhua
        #region
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
    }
    #endregion
    public class TOPO
    {
        public static readonly string RequestUser = "Das4D24fGh";
        public static readonly string RequestPassword = "e10adc3949ba59abbe56e057f20f883e";

        //公司列表
        public static string CompanySearch(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/list";
            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "name", name },
                    { "pageNo", pageNo }
                };

            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        //基本信息
        public static string CompanyBasicInfo(string companyName)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/baseinfo";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", companyName }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        //软件注册权
        public static string SoftwareCopyRightUrl(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/zzqlist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "name", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Trademark(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/sblist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Referee(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/cpwslist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Announcement(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/fygglist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;

        }
        //专利信息
        public static string PatentInfo(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/zhuanlilist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "name", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        //股份信息
        public static string Partners(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/gdlist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        //投资信息
        public static string Investment(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/dwtz";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        //分支机构
        public static string Branch(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/fzjg";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        //经营异常名录
        public static string BusinessAbnormalion(string name, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/ycjy";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", name },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }

        public static string Punish(string CompanyName, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzcflist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", CompanyName },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            return response;
        }

        public static string PunishDetail(string ID)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzcfdetail";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "id", ID },
                };
            string response = Requests.Get(url, dic);
            return response;
        }


        public static string Allow(string CompanyName, int pageNo = 1)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzxklist";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "companyName", CompanyName },
                    { "pageNo", pageNo }
                };
            string response = Requests.Get(url, dic);
            //MessageBox.Show(response);
            return response;
        }
        public static string AllowDetail(string ID)
        {
            string url = "http://58.213.162.194:5443/InterfaceManagement/app/interface/xzxkdetail";

            Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "requestUser", RequestUser },
                    { "requestPwd", RequestPassword },
                    { "id", ID },
                };
            string response = Requests.Get(url, dic);
            return response;
        }
    }

}
