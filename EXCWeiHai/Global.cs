﻿using BaseDLL;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EXC
{
 
    public class Related
    {
        /// <summary>
        /// 从刷身份证开始读取到的身份证信息，流程完成自动清理
        /// </summary>
        public IDCardData IDCardData;
        public string TemplateID;
        public string CompanyID;
        public Guid UUID;
        public string PageType = "";
        public bool CameraPass = false;


        public void Initialized()
        {
            UUID = Guid.NewGuid();
        }
    }
    public static class Global
    {
        public static ConfigData Config = new ConfigData();
        public static Related Related = new Related();
     
        public static string IP = Info.IPAdress()[0];
        public static string MAC = Info.MACAdress()[0];


    

        public static void Initialized()
        {
            if (File.Exists("Config"))
            {
                ReadConfigFile("Config");
            }
            else
            {
                WriteConfigFile("Config");
            }

        }

        public static void ReadConfigFile(string sFile)
        {
            try
            {
                //此调用方法多次运行会产生内存泄漏,参见 https://stackoverflow.com/questions/1127431/xmlserializer-giving-filenotfoundexception-at-constructor
                FileStream stream = File.OpenRead(sFile);
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigData), typeof(ConfigData).GetNestedTypes());
                Config = serializer.Deserialize(stream) as ConfigData;        
            }
            catch
            {
                Environment.Exit(0);
            }
        }
        public static void WriteConfigFile(string sFile)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ConfigData), typeof(ConfigData).GetNestedTypes());
            FileStream fs = File.Create(sFile);
            ser.Serialize(fs, Config);
            fs.Close();
        }
    }
    [Serializable]
    public class ConfigData
    {

        //使用单位信息
        public int BackgroundWindow { get; set; } = 0; //终端机不明确窗口

        public string LocateUser { get; set; } = "海贝分自助查询终端";
        //终端名称
        public string TerminalName { get; set; } = "一号机";
        // 技术支持人员和电话
        public string Technica { get; set; } = "文登区社会信用中心";
        public string Technicamail { get; set; }= "www.wdcredit.gov.cn ";
        public string AdminPassword { get; set; }= "admin";
        public string WeiHaiWebserviecsUrl { get; set; }


        //版本限制
        //企业不允许查看
        public string ENShow { get; set; } = "信用五进报告";
        //未通过人脸允许查看
        public string ELShow { get; set; } = "简易报告";

        public string GRNShow { get; set; } = "";
        public string GRLShow { get; set; }="简易报告";//企业未通过人脸
        }





}
