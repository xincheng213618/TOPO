﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XinHua.NanJingWebServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://creditws.topo360.com/", ConfigurationName="NanJingWebServices.CreditreportDelegate")]
    public interface CreditreportDelegate {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string syncReport(string RequestUser, string RequestPwd, string Data);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> syncReportAsync(string RequestUser, string RequestPwd, string Data);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getReportGetTime(string RequestUser, string RequestPwd, string strXml, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getReportGetTimeAsync(string RequestUser, string RequestPwd, string strXml, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string checkTheReport(string RequestUser, string RequestPwd, string reportId, string type, string comfrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> checkTheReportAsync(string RequestUser, string RequestPwd, string reportId, string type, string comfrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string sendMail(string RequestUser, string RequestPwd, string content, string toAddress, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> sendMailAsync(string RequestUser, string RequestPwd, string content, string toAddress, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getreport(string RequestUser, string RequestPwd, string Reportid, string Tyshxydm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getreportAsync(string RequestUser, string RequestPwd, string Reportid, string Tyshxydm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getQyjbxx(string RequestUser, string RequestPwd, string qyid, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getQyjbxxAsync(string RequestUser, string RequestPwd, string qyid, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getZrrxyxxList(string RequestUser, string RequestPwd, string Xm, string Sfzjhm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getZrrxyxxListAsync(string RequestUser, string RequestPwd, string Xm, string Sfzjhm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getQyxyxx(string RequestUser, string RequestPwd, string qymc, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getQyxyxxAsync(string RequestUser, string RequestPwd, string qymc, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string receiveResult(string RequestUser, string RequestPwd, string Sessionid, string Cardid);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> receiveResultAsync(string RequestUser, string RequestPwd, string Sessionid, string Cardid);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string sendTextMessage(string RequestUser, string RequestPwd, string content, string phoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> sendTextMessageAsync(string RequestUser, string RequestPwd, string content, string phoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getreportlist(string RequestUser, string RequestPwd, string Sfzjhm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getreportlistAsync(string RequestUser, string RequestPwd, string Sfzjhm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getZrrxyxx(string RequestUser, string RequestPwd, string Grid, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getZrrxyxxAsync(string RequestUser, string RequestPwd, string Grid, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string checkQyztbsByQymc(string RequestUser, string RequestPwd, string Qymc);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> checkQyztbsByQymcAsync(string RequestUser, string RequestPwd, string Qymc);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string updatereportstatus(string RequestUser, string RequestPwd, string Reportid, string CapturedImg, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> updatereportstatusAsync(string RequestUser, string RequestPwd, string Reportid, string CapturedImg, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getBusinessUndertaking(string RequestUser, string RequestPwd, string Reportid);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getBusinessUndertakingAsync(string RequestUser, string RequestPwd, string Reportid);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getBusinessUndertakingList(string RequestUser, string RequestPwd, string CardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getBusinessUndertakingListAsync(string RequestUser, string RequestPwd, string CardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getAllopatricReportList(string RequestUser, string RequestPwd, string Sfzjhm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getAllopatricReportListAsync(string RequestUser, string RequestPwd, string Sfzjhm, string Type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getAllopatricReport(string RequestUser, string RequestPwd, string reportId, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getAllopatricReportAsync(string RequestUser, string RequestPwd, string reportId, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string getImportQyxybg(string RequestUser, string RequestPwd, string XmlStr, string fyjBase64Str, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> getImportQyxybgAsync(string RequestUser, string RequestPwd, string XmlStr, string fyjBase64Str, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string checkXybgByYzm(string RequestUser, string RequestPwd, string Yzm, string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> checkXybgByYzmAsync(string RequestUser, string RequestPwd, string Yzm, string type);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CreditreportDelegateChannel : XinHua.NanJingWebServices.CreditreportDelegate, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreditreportDelegateClient : System.ServiceModel.ClientBase<XinHua.NanJingWebServices.CreditreportDelegate>, XinHua.NanJingWebServices.CreditreportDelegate {
        
        public CreditreportDelegateClient() {
        }
        
        public CreditreportDelegateClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CreditreportDelegateClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditreportDelegateClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditreportDelegateClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string syncReport(string RequestUser, string RequestPwd, string Data) {
            return base.Channel.syncReport(RequestUser, RequestPwd, Data);
        }
        
        public System.Threading.Tasks.Task<string> syncReportAsync(string RequestUser, string RequestPwd, string Data) {
            return base.Channel.syncReportAsync(RequestUser, RequestPwd, Data);
        }
        
        public string getReportGetTime(string RequestUser, string RequestPwd, string strXml, string type) {
            return base.Channel.getReportGetTime(RequestUser, RequestPwd, strXml, type);
        }
        
        public System.Threading.Tasks.Task<string> getReportGetTimeAsync(string RequestUser, string RequestPwd, string strXml, string type) {
            return base.Channel.getReportGetTimeAsync(RequestUser, RequestPwd, strXml, type);
        }
        
        public string checkTheReport(string RequestUser, string RequestPwd, string reportId, string type, string comfrom) {
            return base.Channel.checkTheReport(RequestUser, RequestPwd, reportId, type, comfrom);
        }
        
        public System.Threading.Tasks.Task<string> checkTheReportAsync(string RequestUser, string RequestPwd, string reportId, string type, string comfrom) {
            return base.Channel.checkTheReportAsync(RequestUser, RequestPwd, reportId, type, comfrom);
        }
        
        public string sendMail(string RequestUser, string RequestPwd, string content, string toAddress, string type) {
            return base.Channel.sendMail(RequestUser, RequestPwd, content, toAddress, type);
        }
        
        public System.Threading.Tasks.Task<string> sendMailAsync(string RequestUser, string RequestPwd, string content, string toAddress, string type) {
            return base.Channel.sendMailAsync(RequestUser, RequestPwd, content, toAddress, type);
        }
        
        public string getreport(string RequestUser, string RequestPwd, string Reportid, string Tyshxydm, string Type) {
            return base.Channel.getreport(RequestUser, RequestPwd, Reportid, Tyshxydm, Type);
        }
        
        public System.Threading.Tasks.Task<string> getreportAsync(string RequestUser, string RequestPwd, string Reportid, string Tyshxydm, string Type) {
            return base.Channel.getreportAsync(RequestUser, RequestPwd, Reportid, Tyshxydm, Type);
        }
        
        public string getQyjbxx(string RequestUser, string RequestPwd, string qyid, string Type) {
            return base.Channel.getQyjbxx(RequestUser, RequestPwd, qyid, Type);
        }
        
        public System.Threading.Tasks.Task<string> getQyjbxxAsync(string RequestUser, string RequestPwd, string qyid, string Type) {
            return base.Channel.getQyjbxxAsync(RequestUser, RequestPwd, qyid, Type);
        }
        
        public string getZrrxyxxList(string RequestUser, string RequestPwd, string Xm, string Sfzjhm, string Type) {
            return base.Channel.getZrrxyxxList(RequestUser, RequestPwd, Xm, Sfzjhm, Type);
        }
        
        public System.Threading.Tasks.Task<string> getZrrxyxxListAsync(string RequestUser, string RequestPwd, string Xm, string Sfzjhm, string Type) {
            return base.Channel.getZrrxyxxListAsync(RequestUser, RequestPwd, Xm, Sfzjhm, Type);
        }
        
        public string getQyxyxx(string RequestUser, string RequestPwd, string qymc, string Type) {
            return base.Channel.getQyxyxx(RequestUser, RequestPwd, qymc, Type);
        }
        
        public System.Threading.Tasks.Task<string> getQyxyxxAsync(string RequestUser, string RequestPwd, string qymc, string Type) {
            return base.Channel.getQyxyxxAsync(RequestUser, RequestPwd, qymc, Type);
        }
        
        public string receiveResult(string RequestUser, string RequestPwd, string Sessionid, string Cardid) {
            return base.Channel.receiveResult(RequestUser, RequestPwd, Sessionid, Cardid);
        }
        
        public System.Threading.Tasks.Task<string> receiveResultAsync(string RequestUser, string RequestPwd, string Sessionid, string Cardid) {
            return base.Channel.receiveResultAsync(RequestUser, RequestPwd, Sessionid, Cardid);
        }
        
        public string sendTextMessage(string RequestUser, string RequestPwd, string content, string phoneNumber) {
            return base.Channel.sendTextMessage(RequestUser, RequestPwd, content, phoneNumber);
        }
        
        public System.Threading.Tasks.Task<string> sendTextMessageAsync(string RequestUser, string RequestPwd, string content, string phoneNumber) {
            return base.Channel.sendTextMessageAsync(RequestUser, RequestPwd, content, phoneNumber);
        }
        
        public string getreportlist(string RequestUser, string RequestPwd, string Sfzjhm, string Type) {
            return base.Channel.getreportlist(RequestUser, RequestPwd, Sfzjhm, Type);
        }
        
        public System.Threading.Tasks.Task<string> getreportlistAsync(string RequestUser, string RequestPwd, string Sfzjhm, string Type) {
            return base.Channel.getreportlistAsync(RequestUser, RequestPwd, Sfzjhm, Type);
        }
        
        public string getZrrxyxx(string RequestUser, string RequestPwd, string Grid, string Type) {
            return base.Channel.getZrrxyxx(RequestUser, RequestPwd, Grid, Type);
        }
        
        public System.Threading.Tasks.Task<string> getZrrxyxxAsync(string RequestUser, string RequestPwd, string Grid, string Type) {
            return base.Channel.getZrrxyxxAsync(RequestUser, RequestPwd, Grid, Type);
        }
        
        public string checkQyztbsByQymc(string RequestUser, string RequestPwd, string Qymc) {
            return base.Channel.checkQyztbsByQymc(RequestUser, RequestPwd, Qymc);
        }
        
        public System.Threading.Tasks.Task<string> checkQyztbsByQymcAsync(string RequestUser, string RequestPwd, string Qymc) {
            return base.Channel.checkQyztbsByQymcAsync(RequestUser, RequestPwd, Qymc);
        }
        
        public string updatereportstatus(string RequestUser, string RequestPwd, string Reportid, string CapturedImg, string Type) {
            return base.Channel.updatereportstatus(RequestUser, RequestPwd, Reportid, CapturedImg, Type);
        }
        
        public System.Threading.Tasks.Task<string> updatereportstatusAsync(string RequestUser, string RequestPwd, string Reportid, string CapturedImg, string Type) {
            return base.Channel.updatereportstatusAsync(RequestUser, RequestPwd, Reportid, CapturedImg, Type);
        }
        
        public string getBusinessUndertaking(string RequestUser, string RequestPwd, string Reportid) {
            return base.Channel.getBusinessUndertaking(RequestUser, RequestPwd, Reportid);
        }
        
        public System.Threading.Tasks.Task<string> getBusinessUndertakingAsync(string RequestUser, string RequestPwd, string Reportid) {
            return base.Channel.getBusinessUndertakingAsync(RequestUser, RequestPwd, Reportid);
        }
        
        public string getBusinessUndertakingList(string RequestUser, string RequestPwd, string CardId) {
            return base.Channel.getBusinessUndertakingList(RequestUser, RequestPwd, CardId);
        }
        
        public System.Threading.Tasks.Task<string> getBusinessUndertakingListAsync(string RequestUser, string RequestPwd, string CardId) {
            return base.Channel.getBusinessUndertakingListAsync(RequestUser, RequestPwd, CardId);
        }
        
        public string getAllopatricReportList(string RequestUser, string RequestPwd, string Sfzjhm, string Type) {
            return base.Channel.getAllopatricReportList(RequestUser, RequestPwd, Sfzjhm, Type);
        }
        
        public System.Threading.Tasks.Task<string> getAllopatricReportListAsync(string RequestUser, string RequestPwd, string Sfzjhm, string Type) {
            return base.Channel.getAllopatricReportListAsync(RequestUser, RequestPwd, Sfzjhm, Type);
        }
        
        public string getAllopatricReport(string RequestUser, string RequestPwd, string reportId, string type) {
            return base.Channel.getAllopatricReport(RequestUser, RequestPwd, reportId, type);
        }
        
        public System.Threading.Tasks.Task<string> getAllopatricReportAsync(string RequestUser, string RequestPwd, string reportId, string type) {
            return base.Channel.getAllopatricReportAsync(RequestUser, RequestPwd, reportId, type);
        }
        
        public string getImportQyxybg(string RequestUser, string RequestPwd, string XmlStr, string fyjBase64Str, string type) {
            return base.Channel.getImportQyxybg(RequestUser, RequestPwd, XmlStr, fyjBase64Str, type);
        }
        
        public System.Threading.Tasks.Task<string> getImportQyxybgAsync(string RequestUser, string RequestPwd, string XmlStr, string fyjBase64Str, string type) {
            return base.Channel.getImportQyxybgAsync(RequestUser, RequestPwd, XmlStr, fyjBase64Str, type);
        }
        
        public string checkXybgByYzm(string RequestUser, string RequestPwd, string Yzm, string type) {
            return base.Channel.checkXybgByYzm(RequestUser, RequestPwd, Yzm, type);
        }
        
        public System.Threading.Tasks.Task<string> checkXybgByYzmAsync(string RequestUser, string RequestPwd, string Yzm, string type) {
            return base.Channel.checkXybgByYzmAsync(RequestUser, RequestPwd, Yzm, type);
        }
    }
}
