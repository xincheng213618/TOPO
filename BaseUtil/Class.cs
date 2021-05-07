using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtil
{
    public class TimeCount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int countdown = 59;
        public int Countdown
        {
            get { return countdown; }
            set
            {
                countdown = value;
                if (PropertyChanged != null)
                {
                    Show = Countdown.ToString() + "秒后自动返回";
                    PropertyChanged(this, new PropertyChangedEventArgs("Countdown"));
                }
            }

        }

        private string buttonClor = "#3668d3";

        public string ButtonClor
        {
            get { return buttonClor; }
            set { buttonClor = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonClor")); }
        }

        private string show;
        public string Show
        {
            get { return show; }
            set { show = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Show")); }
        }

    }


    /// <summary>
    ///列表基础类 >>>>>>>>
    ///  Designed By Mr.xin 2020.6.2 11.46
    /// </summary>
    public class ListItem
    {
        public int ListNo { get; set; }
        /// <summary>
        ///是否选中 
        /// </summary>
        public bool Ischeck { get; set; } 

        /// <summary>
        ///是否可见 
        /// </summary>
        public string Visible { get; set; }

        private string background = null;
        /// <summary>
        /// 背景
        /// </summary>
        public string Background
        {
            get
            {
                return background != null ? background : ListNo % 2 == 1 ? "#F7F7F7" : "#FFFFFF";
            }
            set { background = value; } 
        }

        /// <summary>
        ///文件名称 
        /// </summary>
        public string FileName { get; set; }

    }
    /// <summary>
    ///PDF 页面展示类
    /// </summary>
    public class CheckItem : ListItem
    {
        public string CheckVisible
        {
            get;
            set;
        }
    }
    /// <summary>
    ///公司信息相关基础类
    /// </summary>
    public class CompanyItem : ListItem
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        ///  统一社会信用代码
        /// </summary>
        public string USCI { get; set; } 
        /// <summary>
        ///纳税人识别号
        /// </summary>
        public string TaxNo { get; set; }
        /// <summary>
        ///组织机构代码 
        /// </summary>
        public string OC { get; set; }      
        /// <summary>
        ///工商注册号 
        /// </summary>
        public string BRN { get; set; }
        /// <summary>
        ///企业类型
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        ///企业法定代表人
        /// </summary>
        public string LegalRepresentative { get; set; }
        /// <summary>
        ///企业法定代表人身份证号 
        /// </summary>
        public string LegalRepresentativeIDCardNo { get; set; }
        /// <summary>
        ///公司官网 
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        ///经营状态 
        /// </summary>
        public string BusinessStatus { get; set; }

        /// <summary>
        ///成立日期 
        /// </summary>
        public string Establishment { get; set; }
        /// <summary>
        ///经营许可日期 
        /// </summary>
        public string LicenseDate { get; set; }

        /// <summary>
        ///行业 
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        ///经营范围 
        /// </summary>
        public string BusinessScope { get; set; }
        /// <summary>
        ///注册资本 
        /// </summary>
        public string RegisteredCapital { get; set; }
        /// <summary>
        ///注册地址 
        /// </summary>
        public string RegisteredAddress { get; set; }
        public string Phone { get; set; }
    }

    public class CompayQueryListItem : CompanyItem
    {
        public string CompanyID { get; set; }

        public string Repname { get; set; }

        public string TaxANum { get; set; }
    }

    /// <summary>
    ///基本信息 
    /// </summary>
    public class CompayQueryDetailItem : CompanyItem
    {
        /// <summary>
        ///登记机关 
        /// </summary>
        public string regOrgan { get; set; }

        /// <summary>
        ///注册资本 
        /// </summary>
        public string regMoney { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        public string entType { get; set; }
    }
    public class XinHuaTaxA : CompayQueryListItem
    {
        public string annualEvaluation { get; set; }
        public string status { get; set; }
        public string taxBracket { get; set; }
        public string updateBy { get; set; }
        public string updateDate { get; set; }
    }

    public class CompanyReportItem : CompanyItem
    {
        /// <summary>
        ///申请人 
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        ///申请人身份证号 
        /// </summary>
        public string ApplicantIDcardNo { get; set; }
        /// <summary>
        ///申请日期  
        /// </summary>
        public string ApplicationData { get; set; }
        /// <summary>
        ///文件地址 
        /// </summary>
        public string FilePath { get; set; } 
    }
    /// <summary>
    ///模板ID 
    /// </summary>
    public class TemplateItem : ListItem
    {
        /// <summary>
        ///模板名称 
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        ///模板ID 
        /// </summary>
        public string TemplateID { get; set; } 

        public string TemplateNakeName { get; set; }

    }


    /// <summary>
    ///个人信息 
    /// </summary>
    public class PersonItem : ListItem
    {
        /// <summary>
        ///姓名 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///身份证号 
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        ///联系方式 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///性别 
        /// </summary>
        public string Sex { get; set; }

    }
    /// <summary>
    /// 
    ///房屋信息
    /// </summary>
    public class HouseItem : PersonItem
    {
        /// <summary>
        ///房屋编号 
        /// </summary>
        public string HouseID { get; set; }

        /// <summary>
        ///位置 
        /// </summary>
        public string Location { get; set; } 

        public string FilePath { get; set; }
        public string FilePage { get; set; }
    }
    public class SZArchiveMenuItem : SZArchiveListItem
    {
        public string Category { get; set; }
    }

    public class SZArchiveListItem : PersonItem
    {
        public string name { get; set; }
        public string beLocated { get; set; }
        public string archivecode { get; set; }
        public string uniquehouseno { get; set; }
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

        /// <summary>
        ///重点人群 
        /// </summary>
        public string recordId { get; set; }
        /// <summary>
        ///重点人群传参 
        /// </summary>
        public string tableId { get; set; }
        /// <summary>
        ///重点人群传参 
        /// </summary>
        public string typeConfigId { get; set; }


    }

    public class VersionItem: ListItem
    {
        public string TemplateID { get; set; }

        public string TemplateName { get; set; }
    }




}
