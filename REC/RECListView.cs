
using BaseUtil;
using System;

namespace REC
{

    public class Certificate: ListItem
    {
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string QLR { get; set; } = "";
        /// <summary>
        /// 权利人证件号
        /// </summary>
        public string QLRZJH { get; set; } = "";
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; } = "";
        /// <summary>
        /// 不动产权证号
        /// </summary>
        public string BDCQZH { get; set; } = "";
        /// <summary>
        /// 用途
        /// </summary>
        public string YT { get; set; } = "";
        /// <summary>
        /// 其他
        /// </summary>
        public string QT { get; set; } = "";
        /// <summary>
        /// 权力其他情况
        /// </summary>
        public string QLQTZK { get; set; } = "";

        /// <summary>
        /// 权利类型
        /// </summary>
        public string QLLX { get; set; } = "";

        /// <summary>
        /// 权利性质
        /// </summary>
        public string QLXZ { get; set; } = "";
        /// <summary>
        /// 使用期限
        /// </summary>
        public string SYQX { get; set; } = "";
        /// <summary>
        /// 共有情况
        /// </summary>
        public string GYQK { get; set; } = "";

        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; } = "";
        /// <summary>
        /// 面积
        /// </summary>
        public string MJ { get; set; } = "";

        /// <summary>
        /// 户型图
        /// </summary>
        public string HST { get; set; } = "";
        /// <summary>
        /// 其他 附记
        /// </summary>
        public string FJ { get; set; } = "";
        /// <summary>
        /// DJLX
        /// </summary>
        public string DJLX { get; set; } = "";
    }
    


    /// <summary>
    /// 证书里基本内容
    /// </summary>
    public class RECData : Certificate
    {
        /// <summary>
        /// 打印的证书名称
        /// </summary>
        public string PrintName { get; set; } = "";

        public bool HSTSucess { get; set; } = false;
        /// <summary>
        /// 办理状态
        /// </summary>
        public string HandlingStatus { get; set; } = "";

        /// <summary>
        /// 流水编号
        /// </summary>
        public string SLBH { get; set; }

        public string PROID { get; set; }
        /// <summary>
        /// 证书id
        /// </summary>
        public string ZSID { get; set; }

        /// <summary>
        /// 印制号
        /// </summary>
        public string OCRresult { get; set; }

    }
}
