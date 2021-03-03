using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXCXuanCheng
{
    public static class Http
    {
        public static class XuanCheng
        {
            public static class Search
            {
                public static string queryList(string queryValue, string jglx)
                {
                    string url = "http://credit.ah.gov.cn:8096/jbxxWzym/list";
                    Dictionary<string, object> dic = new Dictionary<string, object>
                    {
                        { "jglx",jglx },
                        {"queryValue",queryValue },
                        {"pageNum","1" },
                        {"pageSize","10" }
                    };
                    string response = Requests.Get(url, dic);
                    //string response = "{\"msg\":\"操作成功\",\"code\":0,\"data\":{\"total\":1,\"rows\":[{\"gszch\":\"340000000016368\",\"nsrsbh\":\"91340000711771143J\",\"_version_\":\"1.68448662774376038E18\",\"jglx\":\"企业\",\"qymc\":\"科大讯飞股份有限公司\",\"id\":\"8f103a0a-97b3-496e-a965-e72520ee363f\",\"tyshxydm\":\"91340000711771143J\",\"zzjgdm\":\"711771143\"}],\"code\":0,\"msg\":0}}";
                    return response;
                }
                public static string queryDeatil(string CompanyName,string USCI, string type)
                {
                    string url = "http://credit.ah.gov.cn:8096/jbxxWzym/queryByQymcAndtyxydm";
                    Dictionary<string, object> dic = new Dictionary<string, object>
                    {
                        { "type", type },
                        {"qymc",CompanyName },
                        {"tyshxydm",USCI },
                    };
                    string response = Requests.Get(url, dic);
                    //string response = "{\"msg\":\"操作成功\",\"code\":0,\"data\":{\"sxjl\":[{\"code\":\"91340000711771143J\",\"objectTypeId\":\"2c90c39163d9b8a60163d9eb50ed004b\",\"jclx\":\"联合激励\",\"sources\":\"国家信用平台\",\"name\":\"科大讯飞股份有限公司\",\"objectname\":\"纳税信用A级纳税人-全国\",\"gxsj\":\"2021-02-08 04:10:12\",\"id\":\"usHKh+xGjtvgUxRDEKzEMA==\",\"type\":\"02\",\"mainType\":\"02\",\"fbsj\":\"2020-07-14 11:38:25\",\"ztlx\":\"法人\"},{\"code\":\"91340000711771143J\",\"objectTypeId\":\"2c90c39163d9b8a60163d9eb50ed004b\",\"jclx\":\"联合激励\",\"sources\":\"国家信用平台\",\"name\":\"科大讯飞股份有限公司\",\"objectname\":\"纳税信用A级纳税人-全国\",\"gxsj\":\"2021-02-08 04:10:12\",\"id\":\"usHKh+xHjtvgUxRDEKzEMA==\",\"type\":\"02\",\"mainType\":\"02\",\"fbsj\":\"2020-07-14 11:38:25\",\"ztlx\":\"法人\"}],\"dwQydj\":{\"hzrq\":\"2020年07月24日\",\"qylx\":\"其他股份有限公司(上市)\",\"clrq\":\"1999年12月30日\",\"zczb\":\"219787.2917\",\"qymc\":\"科大讯飞股份有限公司\",\"jyyxjzrq\":null,\"djjg\":\"安徽省工商行政管理局\",\"tyshxydm\":\"91340000711771143J\",\"gszch\":\"340000000016368\",\"jyfw\":\"增值电信业务；专业技术人员培训；计算机软、硬件开发、生产和销售及技术服务；系统工程、信息服务；电子产品、计算机通讯设备研发、生产、销售；移动通信设备的研发、销售；二类、三类医疗器械研发、制造与销售；图书、电子出版物销售；进出口业务（国家限定和禁止经营的除外）；安全技术防范工程；商用房及住宅房租赁；物业管理；设计、制作、代理、发布广告。（依法须经批准的项目，经相关部门批准后方可开展经营活动）\",\"jyyxqsrq\":\"1999年12月30日\",\"jydz\":\"合肥市高新开发区望江西路666号\",\"bz\":null,\"fddbr\":\"刘庆峰\",\"zt\":\"存续（在营、开业、在册）\"},\"sxcj\":\"\",\"xzcf\":\"\",\"xzxk\":[{\"xdrMc\":\"科大讯飞股份有限公司\",\"xzjg\":\"安徽省新闻出版广电局\",\"xdrFr\":\"刘庆峰\",\"jdrq\":\"2016年11月28日\",\"tyshxydm\":\"91340000711771143J\",\"jdswh\":\"皖新广审〔2016〕233号\",\"objectid\":\"E5200BD0FE32428C966FBDD31408DF10\"},{\"xdrMc\":\"科大讯飞股份有限公司\",\"xzjg\":\"安徽省新闻出版广电局\",\"xdrFr\":\"刘庆峰\",\"jdrq\":\"2016年11月28日\",\"tyshxydm\":\"91340000711771143J\",\"jdswh\":\"皖新广审〔2016〕233号\",\"objectid\":\"DD2AC300221F4A94A6017E8F06156278\"},{\"xdrMc\":\"科大讯飞股份有限公司\",\"xzjg\":\"合肥高新技术产业开发区食品药品监督管理局\",\"xdrFr\":\"杨军\",\"jdrq\":\"2017年09月21日\",\"tyshxydm\":\"91340000711771143J\",\"jdswh\":\"JY33401910016965\",\"objectid\":\"2804A77502834C4F88BCE8A20C08433E\"}]}}";
                    return response;
                }



            }


            #region //企业
            /// <summary>
            /// 注册信息
            /// </summary>
            /// <param name="shxym">社会统一信用编码</param>
            /// <param name="url1">接口地址</param>
            /// <returns></returns>
            public static string ZCXX(string shxym)
            {
                string url = Global.Config.XuanChengZCXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"tyshxydm", shxym},

                };
                //string response = Requests.Get(url, dic);
                string response = "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{\"id\":11371222,\"uniscid\":\"91341239895723141N\",\"baseDwmc\":\"测试用企业\",\"baseFddbr\":\"朱山东\",\"baseZch\":\"941N\",\"baseJgdm\":\"91341239895723141N\",\"baseZczj\":\"500000\",\"baseHy\":\"水利、环境和公共设施管理业\",\"baseSzqh\":\"安徽省泾县\",\"baseClrq\":\"2003 - 07 - 22 00:00:00\",\"baseXxdz\":\"安徽省宣城市泾县泾川镇茂林路148号\",\"baseQyzl\":\"有限责任公司\",\"baseZycp\":\"水能、水资源开发、城镇供水; 建筑材料销售。\",\"baseDwjj\":\"测试用企业是一家专业从事水能、水资源开发、城镇供水; 建筑材料销售的企业\",\"bz\":\"元\",\"zjze\":null,\"baseJzsj\":1513699200000,\"xydm\":null,\"baseSwdjh\":null,\"baseZcrq\":\"2003 - 07 - 22\",\"baseNsrzt\":null,\"baseGdsx\":null,\"baseZzjgxz\":null,\"baseJyfw\":\"水能、水资源开发、城镇供水; 建筑材料销售。\",\"gxrq\":\"2006 - 11 - 12\",\"baseLxdh\":\"5022851\",\"baseYzbm\":\"242000\"}],\"total\":null}";
                return response;
            }
            /// <summary>
            /// 许可信息
            /// </summary>
            /// <param name="shxym">社会统一信用编码</param>
            /// <param name="url1">接口地址</param>
            /// <returns></returns>
            public static string XKXX(string shxym)
            {
                string url = Global.Config.XuanChengXKXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"tyshxydm", shxym},

                };
                //string response = Requests.Get(url, dic);
                string response = "{ \"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{ \"id\":\"67856BFBC3D22307E053292ACB3B5A95\",\"recordId\":null,\"areaOid\":null,\"bmbh\":\"34010200000000000000\",\"bmmc\":\"瑶海区\",\"xxfl\":\"公开\",\"bh\":null,\"jdswh\":\"91340102MA2MU7425U\",\"xmmc\":\"市级权限内的企业登记\",\"splb\":\"认可\",\"xknr\":\"企业设立登记\",\"xdrMc\":\"合肥扬帆网咖\",\"xdrShxym\":\"91340102MA2MU7425U\",\"xdrZdm\":null,\"xdrGsdj\":null,\"xdrSwdj\":null,\"xdrSfz\":null,\"xdrFr\":\"钟飞\",\"xdrLx\":\"组织\",\"jdrq\":\"2016-03-31\",\"jzrq\":\"2099-12-31\",\"xzjg\":\"合肥市瑶海区市场监督管理局\",\"zt\":\"0\",\"ztmc\":\"正常\",\"dfbm\":\"340100\",\"bz\":null,\"glid\":\"11132003\",\"sjc\":\"2018-12-23\",\"gsrq\":\"2016年04月04\",\"wjscsj\":null,\"infoItem\":null}],\"total\":null}";
                return response;
            }
            /// <summary>
            /// 资质信息
            /// </summary>
            /// <param name="shxym">社会统一信用编码</param>
            /// <param name="url1">接口地址</param>
            /// <returns></returns>
            public static string ZZXX(string shxym)
            {
                string url = Global.Config.XuanChengZZXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"tyshxydm", shxym},

                };
                //string response = Requests.Get(url, dic);
                string response = "{ \"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{ \"id\":22292798,\"jgdm\":\"3412345678\",\"zch\":\"113412345678\",\"jgmc\":\"\n测试用企业\r\n\",\"shxydm\":\"91341239895723141N\",\"zsmc\":\"建筑\",\"zsbh\":\"123123\",\"zzlb\":\"建筑\",\"zzdj\":\"一级\",\"zznr\":null,\"fbrq\":\"2017年08月03日\",\"hqrq\":\"2017-08-03\",\"jzsj\":1501689600000,\"zszt\":\"1\" }],\"total\":null}";
                return response;
            }
            /// <summary>
            /// 参保信息
            /// </summary>
            /// <param name="shxym">社会统一信用编码</param>
            /// <param name="url1">接口地址</param>
            /// <returns></returns>
            public static string CBXX(string shxym)
            {
                string url = Global.Config.XuanChengCBXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"tyshxydm", shxym},

                };
                //string response = Requests.Get(url, dic);
                string response = "{ \"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{ \"id\":\"22292798\",\"dwmc\":\"测试用企业\",\"shxym\":\"91341239895723141N\",\"zch\":\"asdsa\",\"jzsj\":\"2018-05-14 07:30:41\",\"jgdm\":\"sdfsds\",\"cbrq\":\"2018年05月14日\",\"cbzt\":\"1\",\"cbzt_mc\":\"张弘毅张弘毅\",\"bllb\":\"1\",\"sccbrq\":\"2018年05月14日\",\"xzlx_mc\":\"啊实打实的\",\"xzlx\":\"啊实打实的\",\"hqsj\":null }],\"total\":null}";
                return response;
            }
            /// <summary>
            /// 良好信息
            /// </summary>
            /// <param name="shxym">社会统一信用编码</param>
            /// <returns></returns>
            public static string LHXX(string shxym)
            {
                string url = Global.Config.XuanChengLHXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"tyshxydm", shxym},
                };
                //string response = Requests.Get(url, dic);
                string response = "{ \"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"data\":{ \"nsxyjbInfo\":[{ \"id\":\"1\",\"jgdm\":\"33643872-7\",\"qymc\":\"安徽方佳商贸有限公司\",\"sxnr\":\"干的漂亮\",\"pddj\":\"A级\",\"fbbm\":\"安徽省\",\"pdnr\":\"好好好\",\"pdnd\":\"2017\",\"pdrq\":\"2017年08月03日\",\"hqsj\":\"2017-08-03\"}],\"jlInfo\":[{ \"id\":\"1\",\"qymc\":\"1\",\"shxym\":\"91340600336438727F\",\"jgdm\":\"1\",\"zch\":\"1\",\"swdjh\":\"1\",\"jlmc\":\"1\",\"jlzsbh\":\"1\",\"jlsj\":\"2021年01月20日\",\"jllb\":\"1\",\"jllb_mc\":\"1\",\"jldj\":\"1\",\"jlje\":\"1\",\"jlyy\":\"1\",\"hqsj\":\"2021-01-20\"}],\"ryInfo\":[{ \"id\":\"0\",\"jgmc\":\"33643872-7\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"市级教育工作先进集体\",\"rynr\":\"内容\",\"bzdw\":\"公安局\",\"qdsj\":\"2016年12月14日\",\"hqsj\":\"2016-12-14\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"市级（省级）文明单位\",\"rynr\":\"内容\",\"bzdw\":\"公安局\",\"qdsj\":\"2016年12月14日\",\"hqsj\":\"2016-12-14\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":\"111\",\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"全市先进基层党组织信息\",\"rynr\":\"全市先进基层党组织信息内容\",\"bzdw\":\"公安局\",\"qdsj\":\"2016年12月14日\",\"hqsj\":\"2016-12-14\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"“徽姑娘”专业合作社、“徽姑娘”来料加工示范基地\",\"rynr\":\"内容\",\"bzdw\":\"市政府\",\"qdsj\":\"2016年12月13日\",\"hqsj\":\"2016-12-13\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"妇女儿童维权示范岗、维权工作先进集体、全市妇联系统先进集体、家庭教育先进集体\",\"rynr\":\"妇女儿童维权示范岗、维权工作先进集体、全市妇联系统先进集体、家庭教育先进集体内容\",\"bzdw\":null,\"qdsj\":\"2016年12月13日\",\"hqsj\":\"2016-12-13\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"“徽姑娘”科技示范基地\",\"rynr\":\"内容\",\"bzdw\":\"市政府\",\"qdsj\":\"2016年12月13日\",\"hqsj\":\"2016-12-13\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"巾帼文明岗\",\"rynr\":\"内容\",\"bzdw\":\"市政府\",\"qdsj\":\"2016年12月13日\",\"hqsj\":\"2016-12-13\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"三八红旗集体\",\"rynr\":\"内容\",\"bzdw\":\"市政府\",\"qdsj\":\"2016年12月13日\",\"hqsj\":\"2016-12-13\"},{ \"id\":\"0\",\"jgmc\":\"安徽方佳商贸有限公司\",\"jdswh\":null,\"shxym\":\"91340600336438727F\",\"jgdm\":null,\"zch\":null,\"rymc\":\"“徽姑娘”电子商务示范基地\",\"rynr\":\"内容\",\"bzdw\":\"市政府\",\"qdsj\":\"2016年12月13日\",\"hqsj\":\"2016-12-13\"}]}}";
                return response;
            }

            /// <summary>
            /// 不良信息
            /// </summary>
            /// <param name="shxym">社会统一信用编码</param>
            /// <returns></returns>
            public static string BLXX(string shxym)
            {
                string url = Global.Config.XuanChengBLXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"tyshxydm", shxym},


                };
                //string response = Requests.Get(url, dic);
                string response = "{ \"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"data\":{ \"sxbzxrInfo\":[{ \"id\":\"0\",\"sfzh\":\"d6tn8KIU4Op11Kxj0MTTzTzQm9p2DLxms2ztGh5efOM=\",\"jgdm\":null,\"ajbh\":\"1223号\",\"lxqk\":\"失信被执行\",\"fbrq\":\"2017年01月01日\",\"zxfy\":null,\"hqrq\":null}],\"xzcfInfo\":[{ \"id\":\"6797D26D5F9035AEE053292ACB3B0F13\",\"recordId\":null,\"areaOid\":null,\"bmbh\":\"34012400000000000000\",\"bmmc\":\"庐江县\",\"xxfl\":\"公开\",\"bh\":null,\"jdswh\":\"庐公(城)行罚决字(2016)10417号\",\"cfmc\":\"null\",\"cflb1\":\"罚款\",\"cflb2\":null,\"cfsy\":\"盗窃\",\"cfyj\":\"null\",\"xdrMc\":\"无名氏\",\"xdrShxym\":\"913418025649604293\",\"xdrZdm\":null,\"xdrGsdj\":\"null\",\"xdrSwdj\":null,\"xdrSfz\":null,\"xdrFr\":null,\"xdrLx\":\"个人\",\"cfjg\":\"null\",\"jdrq\":\"2016-05-01\",\"xzjg\":\"庐江县公安局\",\"zt\":\"0\",\"ztmc\":\"正常\",\"dfbm\":\"340100\",\"bz\":null,\"glid\":null,\"sjc\":\"2018-12-10\",\"gsqx\":\"一年\",\"infoItem\":null,\"gsrq\":null,\"wjscsj\":null}],\"sxqyInfo\":[{ \"id\":\"1\",\"jgdm\":\"1\",\"qymc\":\"1\",\"sxnr\":\"1\",\"sxss\":\"1\",\"fbbm\":\"1\",\"zt\":\"1\",\"fbrq\":\"2021年01月27日\",\"hqsj\":\"2021-01-27\"}]}}";
                return response;
            }
            #endregion

            #region //个人
            /// <summary>
            /// 照片信息
            /// </summary>
            /// <param name="sfzh">身份证号</param>
            /// <returns></returns>
            public static string GRZPXX(string sfzh)
            {
                //照片  http://127.0.0.1:8083/restful-data/rest/ytjPersonal/getZp?sfzh=342501197109108708 身份证号
                string url = Global.Config.XuanChengGRZPXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"sfzh", sfzh},
                };
                //string response = Requests.Get(url, dic);
                string response = "";
                return response;

            }
            /// <summary>
            /// 身份信息
            /// </summary>
            /// <param name="sfzh">身份证号</param>
            /// <returns></returns>
            public static string GRSFXX(string sfzh)
            {

                // 身份信息  http://127.0.0.1:8083/restful-data/rest/ytjPersonal/getZrrjbInfo?sfzh=342401199010316914 sfzh 身份证号
                string url = Global.Config.XuanChengGRSFXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"sfzh", sfzh},
                };
                //string response = Requests.Get(url, dic);
                string response =   "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{\"id\":\"1362\",\"baseXm\":\"胡小波\",\"baseXb\":\"男\",\"baseIdcard\":\"身份证号\",\"baseCsrq\":\"19500202\",\"baseMz\":\"汉族\",\"baseHjqh\":\"340621\",\"baseXjz\":\"安徽省淮北市濉溪县刘桥镇关庙村孙东组\",\"baseWhcd\":null,\"baseHyzk\":\"初婚\",\"baseZzmm\":null,\"baseXx\":null,\"baseZjxy\":null,\"baseJkzk\":\" \",\"baseSg\":\"167\",\"baseCym\":null,\"baseHjlx\":\"非农业家庭户\",\"baseHjdz\":\"安徽省濉溪县刘桥镇关帝村孙庄东队\",\"baseJzdz\":\"安徽省淮北市濉溪县刘桥镇关庙村孙东组\",\"jyztMc\":null,\"baseHqsj\":\"2010-12-17\"}],\"total\":null}";
                return response;
            }

            /// <summary>
            /// 资产信息
            /// </summary>
            /// <param name="sfzh">身份证号</param>
            /// <returns></returns>
            public static string GRZCXX(string sfzh)
            {

                //3)资产信息（房产车辆林权） http://127.0.0.1:8083/restful-data/rest/ytjPersonal/zrr/getZrrzcInfo?sfzh=342401199010316914 身份证号
                string url = Global.Config.XuanChengGRZCXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"sfzh", sfzh},
                };
                //string response = Requests.Get(url, dic);
               string response =  "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"data\":{\"lqdjInfo\":[{\"id\":null,\"sfzh\":\"342401199010316914\",\"zh\":\"001\",\"zldz\":\"安徽\",\"mj\":\"1000\",\"fzjg\":\"林业局\",\"ldsyq\":\"2015年11月01日\",\"zzrq\":\"2031年11月01日\",\"jzsj\":\"2021-01-20\"},{\"id\":null,\"sfzh\":\"312331199010316914\",\"zh\":\"002\",\"zldz\":\"安徽2\",\"mj\":\"10000\",\"fzjg\":\"林 局\",\"ldsyq\":\"2022年11月01日\",\"zzrq\":\"2051年11月01日\",\"jzsj\":\"2022-01-20\"}],\"fcInfo\":[{\"id\":\"3\",\"sfzh\":\"342401199010316914\",\"gfsj\":\"2017年08月01日\",\"fwdz\":\"安徽省合肥市望江东路666号\",\"fwmj\":100.0,\"cqzh\":\"111\",\"gyr\":\"无\",\"hqtj\":\"购买\",\"hqsj\":\"2017-08-02\",\"dybj\":\"有\"},{\"id\":\"4\",\"sfzh\":\"312334199010316914\",\"gfsj\":\"2017年08月01日\",\"fwdz\":\"合肥市望江东路666号\",\"fwmj\":1010.0,\"cqzh\":\"1111\",\"gyr\":\"无\",\"hqtj\":\"购买\",\"hqsj\":\"2011-08-02\",\"dybj\":\"asd\"}],\"clInfo\":[{\"id\":\"2\",\"sfzh\":\"342401199010316914\",\"hphm\":\"皖123\",\"zwpp\":\"现代\",\"clxh\":\"IX25\",\"fdjh\":\"123\",\"cllx_mc\":\"suv\",\"syxz_mc\":null,\"clsbdh\":\"123\",\"ccdjrq\":\"2017年08月01日\",\"zjdjrq\":\"2017年08月01日\",\"sfdy_mc\":null,\"hqsj\":null,\"qzbfrq\":\"2019年08月31日\"},{\"id\":\"22\",\"sfzh\":\"342422229010316914\",\"hphm\":\"皖1222223\",\"zwpp\":\"现\",\"clxh\":\"IXs25\",\"fdjh\":\"123s\",\"cllx_mc\":\"suvs\",\"syxz_mc\":null,\"clsbdh\":\"123s\",\"ccdjrq\":\"2017年01月01日\",\"zjdjrq\":\"2017年01月01日\",\"sfdy_mc\":null,\"hqsj\":null,\"qzbfrq\":\"2019年01月31日\"}]}}";
                return response;
            }

            /// <summary>
            /// 参保信息
            /// </summary>
            /// <param name="sfzh">身份证号</param>
            /// <returns></returns>
            public static string GRCBXX(string sfzh)
            {

                string url = Global.Config.XuanChengGRCBXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"sfzh", sfzh},
                };
                //string response = Requests.Get(url, dic);
                string response =  "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{\"id\":\"3\",\"sfzh\":\"342401199010316914\",\"shbzkkh\":\"123\",\"ryzt_mc\":null,\"cjgzrq\":\"2017年08月01日\",\"jyzt_mc\":null,\"cbzt_mc\":\"张弘毅张弘毅\",\"yldylb_mc\":null,\"ltxrq\":\"2022年08月31日\",\"ksny\":\"201708\",\"zzny\":\"202906\",\"gz\":0.0,\"jbyljfjs\":0.0,\"jbyiljfjs\":0.0,\"syjfjs\":0.0,\"syujfjs\":0.0,\"xzlx\":\"医疗保险\",\"hqsj\":null}],\"total\":null}";
                return response;
            }
            /// <summary>
            /// 良好信息
            /// </summary>
            /// <param name="sfzh">身份证号</param>
            /// <returns></returns>
            public static string GRLHXX(string sfzh)
            {
                //良好信息 http://127.0.0.1:8083/restful-data/rest/ytjPersonal/zrr/getZrrlhInfo?sfzh=342401199010316914
                string url = Global.Config.XuanChengGRLHXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"sfzh", sfzh},
                };
                //string response = Requests.Get(url, dic);
                string response = "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{\"ryInfo\":[{\"id\":\"0\",\"sfhm\":\"342401199010316914\",\"jdswh\":\"1\",\"rymc\":\"1\",\"rynr\":\"1\",\"hdsj\":\"2021年01月27日\",\"hqsj\":\"2021-01-27\",\"bzdw\":\"1\"},{\"id\":\"00\",\"sfhm\":\"123123123123123\",\"jdswh\":\"2\",\"rymc\":\"2\",\"rynr\":\"2\",\"hdsj\":\"2021年01月27日\",\"hqsj\":\"2021-01-27\",\"bzdw\":\"1\"}],\"gqdzyzInfo\":[{\"id\":\"2\",\"xm\":\"程先虎\",\"sfzh\":\"342401199010316914\",\"sxnr\":null,\"pdnd\":\"11\",\"fbbm\":\"只是多发D\",\"hqsj\":\"2017-08-09\"},{\"id\":\"22\",\"xm\":\"撒旦\",\"sfzh\":\"wwwwwwwwwwww\",\"sxnr\":null,\"pdnd\":\"11\",\"fbbm\":\"只是多发DD\",\"hqsj\":\"2017-08-09\"}],\"nsxyajInfo\":[{\"id\":\"1\",\"xm\":\"陈少瀚\",\"sfzh\":\"342401199010316914\",\"sxnr\":null,\"pdnd\":\"11\",\"fbbm\":\"只是多发D\",\"hqsj\":\"2017-08-09\"},{\"id\":\"11\",\"xm\":\"asd\",\"sfzh\":\"342401199010316914\",\"sxnr\":null,\"pdnd\":\"11\",\"fbbm\":\"只是多发DD\",\"hqsj\":\"2017-08-09\"}]}],\"total\":null}";
                return response;
            }

            /// <summary>
            /// 不良信息
            /// </summary>
            /// <param name="sfzh">身份证号</param>
            /// <returns>返回数据未作任何处理</returns>
            public static string GRBLXX(string sfzh)
            {
                //不良信息 http://127.0.0.1:8083/restful-data/rest/ytjPersonal/zrr/getZrrlhInfo?sfzh=342401199010316914
                string url = Global.Config.XuanChengGRBLXXUrl;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {"sfzh", sfzh},
                };
                //string response = Requests.Get(url, dic);
                string response =  "{\"flag\":true,\"msg\":\"LOAD_SUCCESS\",\"rows\":[{\"sxbzxrInfo\":[],\"zdsswfajdsrInfo\":[{\"id\":\"1\",\"sfzh\":\"342622197301238171\",\"sxnr\":\"1\",\"fbrq\":\"2021年01月29日\",\"zt\":\"1\",\"fbbm\":\"1\",\"hqsj\":\"2021-01-29\"}],\"xzcfInfo\":[{\"id\":\"6797D26D5E5B35AEE053292ACB3B0F13\",\"recordId\":null,\"areaOid\":null,\"bmbh\":\"34012400000000000000\",\"bmmc\":\"巢湖市\",\"xxfl\":\"公开\",\"bh\":null,\"jdswh\":\"庐公(柯)行罚决字(2016)10055号\",\"cfmc\":\"null\",\"cflb1\":\"罚款\",\"cflb2\":null,\"cfsy\":\"赌博\",\"cfyj\":null,\"xdrMc\":null,\"xdrShxym\":null,\"xdrZdm\":null,\"xdrGsdj\":null,\"xdrSwdj\":null,\"xdrSfz\":null,\"xdrFr\":null,\"xdrLx\":null,\"cfjg\":\"null\",\"jdrq\":\"2018-11-13\",\"xzjg\":\"庐江县公安局\",\"zt\":\"0\",\"ztmc\":\"正常\",\"dfbm\":null,\"bz\":null,\"glid\":null,\"sjc\":null,\"gsqx\":null,\"infoItem\":null,\"gsrq\":null,\"wjscsj\":null}],\"gjjdkyqInfo\":[{\"id\":\"1\",\"xm\":\"1\",\"sfzh\":\"342622197301238171\",\"yqsj\":null,\"yqje\":null,\"zjhksj\":null,\"hqsj\":null}]}],\"total\":null}";
                return response;
            }
      
            #endregion
        }
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
}
