using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EXCYiXing
{
    public static class Http
    {
        /// <summary>
        /// 新接口直接走第三方数据，就接口走后台数据
        /// </summary>
        public class YinXingNew
        {
            //5.1 登记资料查询接口
            public static string DJZL(string Name,string IDCardNo)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-core/extQuery/djzl";
                JObject @object = new JObject()
                {
                    { "QLRMC", Name},
                    { "QLRZJH",IDCardNo},
                };
                string response = Requests.Post(url, json: @object);
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"A0031187\",\"YWLX\":null,\"FJ\":null,\"FJH\":\"308\",\"TDSYQMJ\":19.6,\"YYXX\":null,\"FTMJ\":0,\"ZCS\":\"6\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":null,\"SJC\":null,\"BDCDYH\":\"320282999999GB99999F00232555\",\"JZMJ\":118,\"MYC\":\"3\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"A0031187\",\"QLRMC\":\"孙勤峰\",\"GYFS\":null,\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"320223197906097374\"}],\"TDDYMJ\":0,\"QLQSSJ\":null,\"ZDDM\":\"320282999999GB99999\",\"YT\":\"成套住宅\",\"QLQTZK\":null,\"JZND\":null,\"YWH\":\"FC528371\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":19.6,\"FWJG\":\"混合结构\",\"TNMJ\":0,\"QLJSSJ\":\"2050-04-09 00:00:00\",\"ZL\":\"华益护卡膜厂综合楼308室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":null,\"ZSZBID\":\"1213488563\",\"DYXX\":null,\"FCZFZSJ\":\"1899-01-01 00:00:00\",\"QLXZ\":\"出让\",\"FWDM\":\"A0031187\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"1900-01-01 00:00:00\"}]}";
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"YWLX\":\"存量商品房转移加抵押（不动产证）\",\"FJ\":\"换证。\",\"FJH\":\"202\",\"TDSYQMJ\":48.5,\"YYXX\":null,\"FTMJ\":13.56,\"ZCS\":\"5\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"0027\",\"SJC\":\"2\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"JZMJ\":150.84,\"MYC\":\"2\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"谢丹丹\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"340322198702048463\"},{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"沈华兵\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"342423198910188616\"}],\"TDDYMJ\":null,\"QLQSSJ\":null,\"ZDDM\":\"320282100003GB00054\",\"YT\":\"成套住宅\",\"QLQTZK\":\"分摊土地使用权面积：48.50㎡\\r\\n房屋结构：混合结构\\r\\n专有建筑面积：137.28㎡，分摊建筑面积：13.56㎡\\r\\n房屋总层数：5层，所在层数：2层\\r\\n\",\"JZND\":null,\"YWH\":\"201910180220\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":48.5,\"ZDMJ\":null,\"FWJG\":\"混合结构\",\"TNMJ\":137.28,\"QLJSSJ\":\"2073-03-14 00:00:00\",\"ZL\":\"宜城街道祥和花园27幢72号202室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"存量商品房转移加抵押（不动产证）\",\"DYXX\":[{\"QT\":\"抵押方式:一般抵押\\r\\n被担保主债权数额:90.0000万元\\r\\n债务履行期限:2019年10月14日至2049年10月14日\\r\\n抵押面积:土地面积:48.50㎡;房屋面积:150.84㎡\\r\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":null,\"BDCDJZMH\":\"20190017685\",\"DYKSSJ\":\"2019-10-14 00:00:00\",\"DYJSSJ\":\"2049-10-14 00:00:00\",\"DYFSMC\":\"一般抵押\",\"YWH\":\"201910180220-1\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":90,\"DBFW\":\"详见合同\",\"DYQR\":\"中国邮政储蓄银行股份有限公司宜兴市支行\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2019-10-21 09:05:09\"},{\"QT\":\"不动产权证号：苏（2019）宜兴不动产权第0027675号\\n被担保主债权数额/最高债权数额：人民币132.9051万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-12~2031-01-06\\n房屋抵押面积：150.84㎡\\n土地抵押面积：48.5㎡\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210001098\",\"DYKSSJ\":\"2021-01-12 00:00:00\",\"DYJSSJ\":\"2031-01-06 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021011200256\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":132.91,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2021-01-12 14:01:24\"}],\"FCZFZSJ\":\"2019-10-22 09:53:42\",\"QLXZ\":\"出让\",\"TDZH\":null,\"FWDM\":\"A0061276\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"2019-10-21 09:04:09\"}]}";
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人2\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人1\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                return response;
            }

            //5.2 办件进度
            //业务号查询
            public static string BJJD(string YWH)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-bizz/extQuery/bjjd";
                JObject @object = new JObject()
                {
                    { "YWH ", YWH},
                };
                string response = Requests.Post(url, json: @object);
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":{\"YWH\":\"\",\"BJZT\":\"\",\"SLRQ\":\"\",\"BLZT\":\"\",\"SQRXX\":[{\"SQRMC\":\"\",\"SQRZJH\":\"\"}]}}";
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":\"null\"";
                return response;
            }
            //5.3 家庭住房查询

            public static string JTZF(string Name, string IDCardNo)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-core/extQuery/jtzf";
                JObject @object = new JObject()
                {
                    { "QLRMC ", Name},
                    {"QLRZJH", IDCardNo}
                };
                  string response = Requests.Post(url, json: @object);
                //string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":{\"FZTS\":1,\"FZFWLIST \":[{\"QLRMC\":\"许建忠\",\"ZJLX\":\"身份证\",\"QLRZJH\":\"320222197304030379\",\"HOUSELIST\":[{\"QLBL\":null,\"FWZL\":\"安镇安南村南张巷6\",\"BDCQZH\":\"20170209472\",\"QDFS\":\"划拨\",\"FZWDM\":\"32020500200944411005100002\",\"QLRMCS\":\"许建忠\",\"YWLXMC\":null,\"JZMJ\":240.55,\"SFDZ\":null,\"DJLX\":\"首次登记/\",\"QDJG\":0,\"TDXZ\":\"划拨\",\"QLLXMC\":\"集体建设用地使用权/房屋（构筑物）所有权\",\"GHYTMC\":\"成套住宅\",\"QLRZJHMS\":\"320222197304030379\",\"FWDM\":\"00944411005100002\",\"GHYT\":\"11\",\"DJSJ\":\"2017年11月14日\"}]}]}}";
                return response;
            }

            //5.4	产权证书查询接口
            public static string CQZS(string BDCDYH, string ZL,string FWDM,string CQZH)
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:1100/service-core/extQuery/cqzs";
                JObject @object = new JObject()
                {
                    { "BDCDYH", BDCDYH},
                    { "ZL",ZL},
                    { "FWDM", FWDM},
                    { "CQZH",CQZH},
                };
                // string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"A0031187\",\"YWLX\":null,\"FJ\":null,\"FJH\":\"308\",\"TDSYQMJ\":19.6,\"YYXX\":null,\"FTMJ\":0,\"ZCS\":\"6\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":null,\"SJC\":null,\"BDCDYH\":\"320282999999GB99999F00232555\",\"JZMJ\":118,\"MYC\":\"3\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"A0031187\",\"QLRMC\":\"孙勤峰\",\"GYFS\":null,\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"320223197906097374\"}],\"TDDYMJ\":0,\"QLQSSJ\":null,\"ZDDM\":\"320282999999GB99999\",\"YT\":\"成套住宅\",\"QLQTZK\":null,\"JZND\":null,\"YWH\":\"FC528371\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":19.6,\"FWJG\":\"混合结构\",\"TNMJ\":0,\"QLJSSJ\":\"2050-04-09 00:00:00\",\"ZL\":\"华益护卡膜厂综合楼308室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":null,\"ZSZBID\":\"1213488563\",\"DYXX\":null,\"FCZFZSJ\":\"1899-01-01 00:00:00\",\"QLXZ\":\"出让\",\"FWDM\":\"A0031187\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"1900-01-01 00:00:00\"}]}";
                // string response= "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2021）宜兴不动产权第0000004号\",\"YWLX\":\"房屋首次转移登记（含商品房、经适房、安置房）\",\"FJ\":\"登记类型：房屋首次转移登记（含商品房、经适房、安置房）\\n业务小类：市场化商品房转移登记\\n登记日期：2021-01-04\\n本土地使用者依法享有本建筑区划内全体业主共有的公共用地土地使用权\\n\",\"FJH\":\"202\",\"TDSYQMJ\":962.6,\"YYXX\":null,\"FTMJ\":398.17,\"ZCS\":\"3\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"4\",\"SJC\":\"3\",\"BDCDYH\":\"320282104025GB00056F00040202\",\"JZMJ\":2481.11,\"MYC\":\"3\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2021）宜兴不动产权第0000004号\",\"QLRMC\":\"王小华\",\"GYFS\":\"单独所有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"320421196906301738\"}],\"TDDYMJ\":0,\"QLQSSJ\":\"2014-05-20 00:00:00\",\"ZDDM\":\"320282104025GB00056\",\"YT\":\"商业服务\",\"QLQTZK\":\"专有建筑面积：2082.94㎡\\n分摊建筑面积：398.17㎡\\n分摊土地使用权面积：962.6㎡\\n房屋结构：钢筋混凝土结构\\n房屋总层数：3\\n所在层：3\\n\",\"JZND\":\"2018\",\"YWH\":\"2021010400043\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"商服用地\",\"TDFTMJ\":962.6,\"ZDMJ\":null,\"FWJG\":\"钢筋混凝土结构\",\"TNMJ\":2082.94,\"QLJSSJ\":\"2054-05-19 00:00:00\",\"ZL\":\"徐舍镇宜云路202号\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"转移登记\",\"ZSZBID\":null,\"DYXX\":[{\"QT\":\"不动产权证号：苏（2021）宜兴不动产权第0000004号\\n被担保主债权数额/最高债权数额：人民币1241万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-07~2023-12-18\\n房屋抵押面积：2481.11㎡\\n土地抵押面积：962.6㎡\\n\",\"CQZH\":\"苏（2021）宜兴不动产权第0000004号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210000662\",\"DYKSSJ\":\"2021-01-07 00:00:00\",\"DYJSSJ\":\"2023-12-18 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021010700419\",\"BDCDYH\":\"320282104025GB00056F00040202\",\"DYJE\":1241,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司徐舍支行\",\"QSZT\":\"现势\",\"FWDM\":\"120180903004\",\"DYR\":\"王小华\",\"DJSJ\":\"2021-01-08 09:59:43\"}],\"FCZFZSJ\":\"2021-01-05 09:27:35\",\"QLXZ\":\"出让\",\"FWDM\":\"120180903004\",\"FWLX\":null,\"CFXX\":null,\"DJSJ\":\"2021-01-04 10:01:55\"}]}";
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"QLLX\":\"国有建设用地使用权/房屋（构筑物）所有权\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"YWLX\":\"存量商品房转移加抵押（不动产证）\",\"FJ\":\"换证。\",\"FJH\":\"202\",\"TDSYQMJ\":48.5,\"YYXX\":null,\"FTMJ\":13.56,\"ZCS\":\"5\",\"BDCLX\":\"包含土地、包含建筑物\",\"ZRZH\":\"0027\",\"SJC\":\"2\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"JZMJ\":150.84,\"MYC\":\"2\",\"QLR\":[{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"谢丹丹\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"340322198702048463\"},{\"QLBL\":null,\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"QLRMC\":\"沈华兵\",\"GYFS\":\"共同共有\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"342423198910188616\"}],\"TDDYMJ\":null,\"QLQSSJ\":null,\"ZDDM\":\"320282100003GB00054\",\"YT\":\"成套住宅\",\"QLQTZK\":\"分摊土地使用权面积：48.50㎡\\r\\n房屋结构：混合结构\\r\\n专有建筑面积：137.28㎡，分摊建筑面积：13.56㎡\\r\\n房屋总层数：5层，所在层数：2层\\r\\n\",\"JZND\":null,\"YWH\":\"201910180220\",\"TDQLLX\":\"国有建设用地使用权\",\"TDYT\":\"城镇住宅用地\",\"TDFTMJ\":48.5,\"ZDMJ\":null,\"FWJG\":\"混合结构\",\"TNMJ\":137.28,\"QLJSSJ\":\"2073-03-14 00:00:00\",\"ZL\":\"宜城街道祥和花园27幢72号202室\",\"FWXZ\":\"市场化商品房\",\"DJLX\":\"存量商品房转移加抵押（不动产证）\",\"DYXX\":[{\"QT\":\"抵押方式:一般抵押\\r\\n被担保主债权数额:90.0000万元\\r\\n债务履行期限:2019年10月14日至2049年10月14日\\r\\n抵押面积:土地面积:48.50㎡;房屋面积:150.84㎡\\r\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":null,\"BDCDJZMH\":\"20190017685\",\"DYKSSJ\":\"2019-10-14 00:00:00\",\"DYJSSJ\":\"2049-10-14 00:00:00\",\"DYFSMC\":\"一般抵押\",\"YWH\":\"201910180220-1\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":90,\"DBFW\":\"详见合同\",\"DYQR\":\"中国邮政储蓄银行股份有限公司宜兴市支行\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2019-10-21 09:05:09\"},{\"QT\":\"不动产权证号：苏（2019）宜兴不动产权第0027675号\\n被担保主债权数额/最高债权数额：人民币132.9051万元\\n抵押方式：最高额抵押\\n债务履行期限/债权确定期间：2021-01-12~2031-01-06\\n房屋抵押面积：150.84㎡\\n土地抵押面积：48.5㎡\\n\",\"CQZH\":\"苏（2019）宜兴不动产权第0027675号\",\"FJ\":\"担保范围：详见合同\\n\",\"BDCDJZMH\":\"20210001098\",\"DYKSSJ\":\"2021-01-12 00:00:00\",\"DYJSSJ\":\"2031-01-06 00:00:00\",\"DYFSMC\":\"最高额抵押\",\"YWH\":\"2021011200256\",\"BDCDYH\":\"320282100003GB00054F00270202\",\"DYJE\":132.91,\"DBFW\":\"详见合同\",\"DYQR\":\"江苏宜兴农村商业银行股份有限公司\",\"QSZT\":\"现势\",\"FWDM\":\"A0061276\",\"DYR\":\"沈华兵,谢丹丹\",\"DJSJ\":\"2021-01-12 14:01:24\"}],\"FCZFZSJ\":\"2019-10-22 09:53:42\",\"QLXZ\":\"出让\",\"TDZH\":null,\"FWDM\":\"A0061276\",\"FWLX\":null,\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}],\"DJSJ\":\"2019-10-21 09:04:09\"}]}";

                string response = Requests.Post(url, json: @object);
                //   string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人2\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"BDCDJZMH\":\"不动产登记证明号\",\"DYQR\":\"抵押权人1\",\"DYJE\":\"抵押金额\",\"DYKSSJ\":\"2020-08-1911:11:11\",\"DYJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-19\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\",\"DYFS\":\"抵押方式\",\"DBFW\":\"担保范围\",\"DYR\":\"抵押人\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                // string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"DYXX\":\"null\",\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";
                // string response = "{\"code\":\"0\",\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"不动产单元号\",\"FWDM\":\"房屋代码\",\"CQZH\":\"产权证号-泰州他项(2008)第3333号\",\"BDCLX\":\"不动产类型\",\"YT\":\"房屋用途\",\"ZL\":\"坐落\",\"FWXZ\":\"房屋性质\",\"FWJG\":\"房屋结构\",\"SJC\":\"5\",\"ZCS\":\"5\",\"FWLX\":\"房屋类型\",\"MYC\":\"名义层\",\"ZRZH\":\"自然幢号\",\"FJH\":\"房间号\",\"JZMJ\":\"100\",\"TNMJ\":\"98\",\"FTMJ\":\"2\",\"DJSJ\":\"2020-08-1911:11:11\",\"FCZFZSJ\":\"2020-08-1911:11:11\",\"TDQLLX\":\"权利类型\",\"QLQSSJ\":\"2020-08-1911:11:11\",\"QLJSSJ\":\"2020-08-1911:11:11\",\"TDQLXZ\":\"权利性质\",\"TDYT\":\"土地用途\",\"FJ\":\"附记\",\"TDSYQMJ\":\"100\",\"TDFTMJ\":\"98\",\"TDDYMJ\":\"2\",\"QLLX\":\"权利类型\",\"DJLX\":\"登记类型\",\"YWLX\":\"业务类型\",\"JZND\":\"建筑年代\",\"YYXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"YYSX\":\"异议事项\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"YYSQR\":\"异议申请人\",\"YYSQRZJHM\":\"申请人证件号码\"}],\"QLR\":[{\"QLRMC\":\"产权人男\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064868\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"},{\"QLRMC\":\"产权人女\",\"QLRZJZL\":\"产权人证件种类\",\"QLRZJH\":\"320223195502064869\",\"GYFS\":\"共有方式\",\"QLBL\":\"共有比例\",\"CQZH\":\"产权证号\"}],\"CFXX\":[{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"},{\"FWDM\":\"房屋代码\",\"BDCDYH\":\"不动产单元号\",\"CFJG\":\"查封机构\",\"CFLX\":\"查封类型\",\"CFWH\":\"查封文号\",\"CFKSSJ\":\"2020-08-1911:11:11\",\"CFJSSJ\":\"2020-08-1911:11:11\",\"DJSJ\":\"2020-08-1911:11:11\",\"QSZT\":\"权属状态\",\"FJ\":\"附记\",\"CQZH\":\"产权证号\"}]}]}";

                return response;
            }
            //5.10抵押查询
            public static string DYCX(string BDCZMH)
            {

                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:9999/out/isaip/dycx_yx";  
                JObject @object = new JObject()
                {
                    { "BDCZMH", BDCZMH},
                };
                string response = Requests.Post(url, json: @object);

                // string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":{\"DYFS\":\"xxx抵押\",\"YWH\":\"TD011bb9b0c5611bb9b0c508708a8c8092\",\"ZWR\":\"王明珠\",\"BDCZMH\":\"泰州他项(2008)第3060号\",\"ZSBH\":\"9447\",\"DBFW\":\"详见合同\",\"ZGZQQDSSHSE\":1213.1130,\"FCDYMJ\":111.1000,\"TDDYMJ\":32.3000,\"BIZ\":\"人民币\",\"DJSJ\":\"1899-01-01 00:00:00\",\"ZQLXQX\":\"20070604至20170604\",\"ZXSJ\":\"2020-03-31 00:03:57\",\"ZT\":\"现势\",\"FJ\":\"1、抵押\\r\\n2、许可抵押面积(平方米)：32.30平方米\\r\\n3、许可抵押金额(大写)：土地资产不计入抵押担保值\",\"QT\":\"dsfasdgdfagsasd \",\"BDCZH\":\"泰州国用(2008)第5620号\",\"BZ\":\"bz333\",\"QLRXX\":{\"DYR\":[{\"DYR\":\"王明珠\",\"DYRZJZL\":\"身份证\",\"DYRZJHM\":\"320623198201293515\"}],\"DYQR\":[{\"DYQR\":\"中国建设银行股份有限公司泰州分行\",\"DYQRZJZL\":\"身份证\",\"DYQRZJHM\":\"320623198201293515\"}]}}}";
                ////string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":{\"dyfs\":\" 般抵押\",\"ywh\":\"201910180220-1\",\"zwr\":\"谢丹丹\",\"bdczmh\":\"苏（2019）宜兴不动产证明第0017685号\",\"zsbh\":\"32010239417\",\"dbfw\":\"详见合同\",\"zgzqqdsshse\":910,\"fcdymj\":150.84,\"tddymj\":null,\"biz\":\"人民币\",\"djsj\":\"2019-10-21 09:05:09\",\"zqlxqx\":\"2029-10-14~2059-10-14止\",\"zxsj\":null,\"zt\":\"现势\",\"fj\":null,\"qt\":\"抵押方式:一般抵押\\r\\n被担保主债权数额:90.0000万元\\r\\n债务履行期限:2019年10月14日至2049年10月14日\\r\\n抵押面积:土地面积:48.50㎡;房屋面积:150.84㎡\\r\\n\",\"bdczh\":\"苏（2019）宜兴不动产权第0027675号\",\"bz\":null,\"qlrxx\":{\"dyr\":[{\"dyr\":\"沈华兵\",\"dyrzjzl\":\"身份证\",\"dyrzjhm\":\"342423198910188616\"},{\"dyr\":\"谢丹丹\",\"dyrzjzl\":\"身份证\",\"dyrzjhm\":\"340322198702048463\"}],\"dyqr\":[{\"dyqr\":\"中国邮政储蓄银行股份有限公司宜兴市支行\",\"dyqrzjzl\":\"营业执照\",\"dyqrzjhm\":\"320282000199459\"}]}}}";
                //if (i == 0)
                //{
                //    response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":{\"dyfs\":\"一般抵押\",\"ywh\":\"201910180220-1\",\"zwr\":\"谢丹丹\",\"bdczmh\":\"苏（2019）宜兴不动产证明第0017685号\",\"zsbh\":\"32010239417\",\"dbfw\":\"详见合同\",\"zgzqqdsshse\":90,\"fcdymj\":150.84,\"tddymj\":null,\"biz\":\"人民币\",\"djsj\":\"2019-10-21 09:05:09\",\"zqlxqx\":\"2019-10-14~2049-10-14止\",\"zxsj\":null,\"zt\":\"现势\",\"fj\":null,\"qt\":\"抵押方式:一般抵押\\r\\n被担保主债权数额:90.0000万元\\r\\n债务履行期限:2019年10月14日至2049年10月14日\\r\\n抵押面积:土地面积:48.50㎡;房屋面积:150.84㎡\\r\\n\",\"bdczh\":\"苏（2019）宜兴不动产权第0027675号\",\"bz\":null,\"qlrxx\":{\"dyr\":[{\"dyr\":\"沈华兵\",\"dyrzjzl\":\"身份证\",\"dyrzjhm\":\"342423198910188616\"},{\"dyr\":\"谢丹丹\",\"dyrzjzl\":\"身份证\",\"dyrzjhm\":\"340322198702048463\"}],\"dyqr\":[{\"dyqr\":\"中国邮政储蓄银行股份有限公司宜兴市支行\",\"dyqrzjzl\":\"营业执照\",\"dyqrzjhm\":\"320282000199459\"}]}}}";
                //}
                //i = Math.Abs(i - 1);

                return response;
            }

            //5.11 预告信息查

            public static string YGCX(string YGZMH="",string FWDM="",string BDCDYH="",string FWZL="",string YGQLR="",string ZJHM="")
            {
                // string url = Global.configData.YiXingListUrl;
                string url = " http://139.140.150.2:9999/out/isaip/ygcx";
                JObject @object = new JObject()
                {
                    { "YGZMH", YGZMH},
                    { "FWDM", FWDM},
                    { "FWZL", FWZL},
                    { "YGQLR", YGQLR},
                    { "ZJHM", ZJHM},
                };
                string response = Requests.Post(url, json: @object);
                //string response = "{\"code\":0,\"msg\":\"查询成功\",\"data\":[{\"BDCDYH\":\"321202050002GB00019F00250107\",\"FWDM\":\"TS-190108093940-360061C2\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\",\"BDCLX\":\"包含土地、包含建筑物\",\"YT\":\"成套住宅\",\"ZL\":\"水映蓝庭25幢107室\",\"FWXZ\":\"市场化商品房\",\"FWJG\":\"aaa\",\"SJC\":\"1\",\"ZCS\":\"1\",\"FWLX\":\"bbb\",\"MYC\":\"1\",\"ZRZH\":\"0025\",\"FJH\":\"107\",\"JZMJ\":1.0000,\"TNMJ\":2.0000,\"FTMJ\":33.1100,\"DJSJ\":\"2019-01-09 10:32:36\",\"FCZFZSJ\":\"2019-02-13 00:00:00\",\"TDQLLX\":\"国有建设用地使用权\",\"QLQSSJ\":\"2017-04-01 10:02:03\",\"QLJSSJ\":\"2020-04-01 00:00:00\",\"TDQLXZ\":\"出让\",\"TDYT\":\"城镇住宅用地\",\"FJ\":\"fdsvdsfsdaf\",\"TDSYQMJ\":123123.0000,\"TDFTMJ\":3.0006,\"TDDYMJ\":1.0010,\"DYXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"BDCDJZMH\":\"20190008679\",\"DYQR\":\"无味\",\"DYJE\":123.000000,\"DYKSSJ\":\"2020-04-01 00:00:00\",\"DYJSSJ\":\"2021-04-28 09:04:56\",\"DJSJ\":\"2019-11-11 10:37:16\",\"QSZT\":\"现势\",\"FJ\":\"543523452345\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\",\"DYFS\":\"一般抵押\",\"DBFW\":\"详见抵押合同\",\"DYR\":\"吴海洋,魏爱霞\"}],\"CFXX\":[{\"FWDM\":\"TS-190108093940-360061C2\",\"BDCDYH\":\"321202050002GB00019F00250107\",\"CFJG\":\"泰州法院\",\"CFLX\":\"预查封\",\"CFWH\":\"2020-0241\",\"CFKSSJ\":\"2020-03-19 00:00:00\",\"CFJSSJ\":\"2021-03-18 00:00:00\",\"DJSJ\":\"2020-03-19 17:09:44\",\"QSZT\":\"现势\",\"FJ\":\"123123123\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}],\"QLR\":[{\"QLRMC\":\"吴海洋\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"32128419821129103X\",\"GYFS\":\"共同共有\",\"QLBL\":\"30%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"},{\"QLRMC\":\"魏爱霞\",\"QLRZJZL\":\"身份证\",\"QLRZJH\":\"321284198202010021\",\"GYFS\":\"共同共有\",\"QLBL\":\"70%\",\"CQZH\":\"苏(2019)泰州不动产证明第0000510号\"}]}]}";
                return response;
            }
             
        }

    }


    public class Requests
    {
        //Designed By Mr.Xin 2020.4.23
        //Change By Mr.Xin 2020.5.6 解决缓存问题，引入缓存数据库  (只限于GET结构,POST 和其他数据结构另外进行添加)
        //BUG Fix By Mr.Xin 2020.5.7 禁止返回信息未空的数据
        public static string Get(string url, Dictionary<string, object> dic = null, int timeout = 10000, string UserAgent = null)
        {
            url = DicAndUrl(url, dic);
            //string response = CSQLite.Slect.CacheGet(url);
            //if (response != null)
            //return response;
            Log.WriteUrl(url, null);
            string response = Request("GET", url, dic: dic, Timeout: timeout);
            Log.WriteUrl(url, response);
            //if (response!=null)
            //    CSQLite.Insert.WriteCache(url, response);

            return response;
        }
        public static string Gets(string url, Dictionary<string, object> dic = null, int timeout = 10000, string UserAgent = null)
        {
            url = DicAndUrl(url, dic);
            string response = Request("GET", url, dic: dic, Timeout: timeout);
            return response;
        }

        private static string DicAndUrl(string url, Dictionary<string, object> dic)
        {
            string param = "";
            if (dic != null)
                for (int count = 0; count < dic.Count; count++)
                    param += dic.ElementAt(count).Key + "=" + dic.ElementAt(count).Value.ToString() + "&";
            return url += "?" + param;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="dic">参数</param>
        /// <param name="json"></param>
        /// <param name="timeout"></param>
        /// <param name="UserAgent"></param>
        /// <returns></returns>
        //这种写法很简单，但是不方面在其他的位置进行调用
        public static string Post(string url, Dictionary<string, object> dic = null, JObject json = null, int timeout = 10000, string UserAgent = null)
        {
            string response = Request("POST", url, dic: dic, json: json, Timeout: timeout, UserAgent: UserAgent);

            Log.WriteUrl(url,response);

            return response;
        }

        public static string Put(string url, Dictionary<string, object> dic = null, int timeout = 10000, string UserAgent = null)
        {
            return Request("PUT", url, dic: dic, Timeout: timeout, UserAgent: UserAgent);
        }



        public static bool Downloade(string url, string filePath)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                Stream stream = new FileStream(filePath, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///requets 查询
        public static string Request(string Method, string url, Dictionary<string, object> dic = null, string ContentType = null, string data = null, JObject json = null, string headers = null, string cookies = null, string UserAgent = null, int Timeout = 30000, string Referer = null, string Proxy = null, bool KeepAlive = true)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = Method;
                request.Referer = Referer ?? null;
                request.KeepAlive = KeepAlive;
                request.Timeout = Timeout;
                request.ServerCertificateValidationCallback = delegate { return true; };
                request.Headers.Add("cookie", cookies);
                request.UserAgent = UserAgent ?? "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36";
                request.ServicePoint.Expect100Continue = false;
                if (Proxy != null)
                {
                    WebProxy WebProxy = new WebProxy(Proxy);
                    request.Proxy = WebProxy;
                }

                if (Method != "GET")
                {
                    byte[] bytePost = { };
                    if (json != null)
                    {
                        request.ContentType = "application/json;charset=utf-8";
                        bytePost = Encoding.UTF8.GetBytes(json.ToString());
                    }
                    if (dic != null)
                    {
                        request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                        string param = "";
                        for (int count = 0; count < dic.Count; count++)
                            param += dic.ElementAt(count).Key + "=" + dic.ElementAt(count).Value.ToString() + "&";
                        bytePost = Encoding.UTF8.GetBytes(param.ToString());
                    }

                    request.ContentLength = bytePost.Length;
                    request.GetRequestStream().Write(bytePost, 0, bytePost.Length);
                    request.GetRequestStream().Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                string r = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                return r;
            }
            catch
            {
                return null;
            }
        }
    }
}
