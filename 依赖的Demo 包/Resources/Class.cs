

namespace Resources
{
    //Designed By Mr.xin 2020.6.2 11.46
    //列表基础类
    public class ListItem
    {
        public int ListNo { get; set; }
        public bool Ischeck { get ; set; } //是否选中

        public string Visible { get; set; }//是否可见

        private string background=null;
        public string Background
        {
            get
            {
                return background != null? background:ListNo % 2 == 1 ? "#F7F7F7" : "#FFFFFF";    
            }
            set { background = value; }//背景 
        }

        public string FileName { get; set; }//文件名称

    }
    //PDF 页面展示类
    public class CheckItem : ListItem
    {
        public string CheckVisible
        {
            get;
            set;
        }
    }
    //公司信息相关基础类
    public class CompanyItem:ListItem
    {
        public string CompanyName { get; set; }        //公司名称
        public string USCI { get; set; }        //统一社会信用代码
        public string TaxNo { get; set; }  //纳税人识别号
        public string OC { get; set; }        //组织机构代码
        public string BRN { get; set; }               //工商注册号
        public string Type { get; set; }  //企业类型

        public string LegalRepresentative { get; set; }//企业法定代表人
        public string LegalRepresentativeIDCardNo { get; set; }//企业法定代表人身份证号
        public string WebSite { get; set; }  //公司官网
        public string BusinessStatus { get; set; }//经营状态
       
        public string Establishment { get; set; }//成立日期
        public string LicenseDate { get; set; } //经营许可日期
        public string Industry { get; set; } //行业

        public string BusinessScope { get; set; }//经营范围
        public string RegisteredCapital { get; set; }//注册资本
        public string RegisteredAddress { get; set; }//注册地址
        public string Phone { get; set; }
    }

    public class CompayQueryListItem: CompanyItem
    {
        public string CompanyID { get; set; }

        public string Repname { get; set; }

        public string TaxANum { get; set; }
    }

    //基本信息
    public class CompayQueryDetailItem: CompanyItem
    {
        public string regOrgan { get; set; }//登记机关

        public string regMoney { get; set; }//注册资本

        public string email { get; set; }
        public string entType { get; set; }
    }
    public class XinHuaTaxA: CompayQueryListItem
    {
        public string annualEvaluation { get; set; }
        public string status { get; set; }
        public string taxBracket { get; set; }
        public string updateBy { get; set; }
        public string updateDate { get; set; }
    }

    public class CompanyReportItem : CompanyItem
    {
        public string Applicant { get; set; }//申请人
        public string ApplicantIDcardNo { get; set; }//申请人身份证号
        public string ApplicationData { get; set; }//申请日期
        public string FilePath { get; set; } //文件地址
    }
    //模板ID
    public class TemplateItem : ListItem
    {
        public string TemplateName { get; set; }//模板名称
        public string TemplateID { get; set; } //模板ID

        public string TemplateNakeName { get; set; }

    }


    //个人信息
    public class PersonItem : ListItem
    {
        public string Name { get; set; }//姓名
        public string IDCardNo { get; set; }//身份证号
        public string Phone { get; set; }//联系方式

        public string Sex { get; set; }//性别

    }

    //房屋信息
    public class HouseItem : PersonItem
    {
        public string HouseID { get; set; } //房屋编号

        public string Location { get; set; } //位置

        public string FilePath { get; set; }
        public string FilePage { get; set; }
    }
    public class SZArchiveMenuItem : SZArchiveListItem
    {
        public string Category { get; set; }
    }

    public class SZArchiveListItem: PersonItem
    {
        public string name { get; set; }
        public string beLocated { get; set; }
        public string archivecode { get; set; }
        public string uniquehouseno { get; set; }
    }

    public class VersionItem
    {
        public int ListNo{ get; set; }

        public string TemplateID{  get; set;  }

        public string TemplateName{ get;set;}
    }


    public class HeiFeiListItem
    {
        /// <summary>
        /// id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 行政相对人名称
        /// </summary>
        public string XdrMc { get; set; }

        /// <summary>
        /// 行政相对人代码
        /// </summary>
        public string XdrShxym { get; set; }
        public string XdrShxymSrc { get; set; }
        public string Xzjg { get; set; }
        public string XdrLx { get; set; }
        public string XdrFr { get; set; }
        public string Jdrq { get; set; }

        public string recordId { get; set; }//重点人群
        public string tableId { get; set; }//重点人群传参
        public string typeConfigId { get; set; }//重点人群传参


    }

    public class HeiFeiPeopleTypeItem
    {
        public int ListNo { get; set; }
        public string TypeName { get; set; }

        public string TypeConfigId { get; set; }

        public string ImageUrl { get; set; }

    }

    public class RECListView : ListItem
    {
        public string QLR { get; set; }//权利人名称
        public string QLRZJH { get; set; }//权利人证件号
        public string YT { get; set; }//用途
        public string QT { get; set; }//其他


        public string QLLX { get; set; } //权利类型

        public string QLXZ { get; set; }//权利性质
        public string SYQX { get; set; }//使用期限

        public string DJLX { get; set; }//DJLX

        public string BDCDYH { get; set; } //不动产单元号
        public string BDCQZH { get; set; }//不动产权证号

        public string GYQK { get; set; }//共有情况

        public string ZL { get; set; }//坐落
        public string MJ { get; set; }//面积


        public string HandlingStatus { get; set; }//办理状态

        public string Cost { get; set; }
    }
}
