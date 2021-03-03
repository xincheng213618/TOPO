
using BaseUtil;

namespace REC
{
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
