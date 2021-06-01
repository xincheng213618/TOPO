using SnmpSharpNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BaseDLL
{
    public class PrintStatus
    {
        //Desgined By Mr.Xin 2020.6.17
        public static int PrinterStatue(string PrinterDevice)
        {
            int ret = 0;
            string path = @"win32_printer.DeviceId='" + PrinterDevice + "'";
            ManagementObject printer = new ManagementObject(path);
            printer.Get();
            ret = Convert.ToInt32(printer.Properties["PrinterState"].Value);
            string a = printer.Properties["PortName"].Value.ToString();

            //MessageBox.Show(printer.Properties["WorkOffline"].Value.ToString());
            //string a = PrintCode[ret];

            return ret;
        }

        public static Dictionary<int, string> PrinterStatusCodes = new Dictionary<int, string>()
        {
            { 0,"打印机准备就绪" },
            { 1,"打印机已暂停" },
            { 2,"打印机错误" },
            { 4,"打印机待删除" },
            { 8,"卡纸" },
            { 16,"没纸了" },
            { 32,"手动进纸" },
            { 64,"纸张问题" },
            { 128,"打印机离线" },
            { 256,"IO active" },
            { 512,"打印机忙" },
            { 1024,"正在打印" },
            { 1025 ,"正在暂停打印"},
            { 2048,"打印机出纸槽已满" },
            { 4096,"无法使用。" },
            { 8192,"等候中" },
            { 16384,"处理中" },
            { 17408 ,"正在进行后台打印"},
            { 32768,"初始化中" },
            { 65536,"热身" },
            { 131072,"碳粉不足" },
            { 262144,"无碳粉" },
            { 524288,"Page punt" },
            { 1048576,"用户干预" },
            { 2097152,"内存不足" },
            { 4194304,"	通过开放" },
            { 8388608,"服务器未知" },
            { 6777216,"节能" },
        };

        public static string PrintStatusCheck(string strDeviceIP)
        {
            DeviceManager d = new DeviceManager(strDeviceIP.Trim(), "HP", "", false, "");
            string strDeviceState = d.GetDeviceState();
            if (strDeviceState == "idle")
            {
                System.Threading.Thread.Sleep(500);
                strDeviceState = d.GetDeviceState();              

            }
            return strDeviceState;
        }
       
    }
    public enum SnmpStatus : int
    {
        NoError = 0, //正确
        SocketError = 12, //连接超时
        NoSuchName = 2 //没有该Oid
    }


    public class DeviceManager
    {
        private string _strDeviceIP;
        private string _strDeviceManu;
        private string _strDeviceModel;

        private string SamsungXmlPath = "samsung.xml";
        private string HpXmlPath = "hp.xml";
        private string RicohXmlPath = "ricoh.xml";

        private bool _PublicCounter;




        private delegate string StatusDelegate(ArrayList alValue);//判断设备状态

        //新增
        private bool isMonitorCounter;
        public bool IsMonitorCounter
        {
            get { return isMonitorCounter; }
            set { isMonitorCounter = value; }
        }
        /// <summary>
        /// 打印机IP
        /// </summary>
        public string DeviceIP
        {
            get { return this._strDeviceIP; }
            set { this._strDeviceIP = value; }
        }

        /// <summary>
        /// 打印机厂家
        /// </summary>
        public string DeviceManu
        {
            get { return this._strDeviceManu; }
            set { this._strDeviceManu = value; }
        }

        /// <summary>
        /// 打印机型号
        /// </summary>
        public string DeviceModel
        {
            get { return this._strDeviceModel; }
            set { this._strDeviceModel = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strDeviceIP"></param>
        /// <param name="strDeviceManu"></param>
        /// <param name="strDeviceModel"></param>
        public DeviceManager(string strDeviceIP, string strDeviceManu, string strDeviceModel, bool publicCounter, string AppPath)
        {
            this._strDeviceIP = strDeviceIP;
            this._strDeviceManu = strDeviceManu;
            this._strDeviceModel = strDeviceModel;
            this._PublicCounter = publicCounter;

            RicohXmlPath = AppPath + "\\" + RicohXmlPath;
            HpXmlPath = AppPath + "\\" + HpXmlPath;
            SamsungXmlPath = AppPath + "\\" + SamsungXmlPath;

        }

        /// <summary>
        ///获取打印机状态
        /// </summary>
        /// <returns></returns>
        public string GetDeviceState()//打印机状态
        {
            try
            {
                string sDeviceStatus = "unknown";

                switch (_strDeviceManu)
                {
                    case "RICOH":
                    case "HP":
                    case "LEXMARK":
                        {
                            ArrayList alValue = new ArrayList();
                            int iSuccess = GetSnmpValue(alValue);
                            sDeviceStatus = JudgeState(iSuccess, alValue, JudgeRicohHPDeviceState);
                        }
                        break;

                    case "TOSHIBA":
                        sDeviceStatus = GetToshibaDeviceState();
                        break;

                    case "SAMSUNG":
                        {
                            string ConsoleText = string.Empty;
                            GetSamsungState(ref sDeviceStatus, ref ConsoleText);
                            //ArrayList alValue = new ArrayList();
                            //int iSuccess = GetSnmpValue(alValue);
                            //sDeviceStatus = JudgeState(iSuccess, alValue, JudgeSamsungDeviceState);

                        }
                        break;
                    default:
                        {
                            //EventData eventData = new EventData(1);
                            //eventData.Add("Manu:", this._strDeviceManu);
                            //ExecuteEventLog.InsertEventLog("DeviceManager.GetDeviceState", EventLogItem.ID_100004,
                            //        EventLevel.Information, EventLogItem.Summary_100004, eventData);
                        }

                        break;
                }

                return sDeviceStatus;
            }
            catch (System.Exception e)
            {

                return "unknown";
            }

        }

        /// <summary>
        /// 判断设备状态
        /// </summary>
        /// <param name="iSuccess"></param>
        /// <param name="alValue"></param>
        /// <param name="statusDelegate"></param>
        /// <returns></returns>
        private string JudgeState(int iSuccess, ArrayList alValue, StatusDelegate statusDelegate)
        {
            string sDeviceStatus = "";

            switch ((SnmpStatus)iSuccess)
            {
                case SnmpStatus.NoError:
                    sDeviceStatus = statusDelegate(alValue);
                    break;

                case SnmpStatus.SocketError://     SnmpException.RequestTimedOut
                    sDeviceStatus = "connectFail";
                    break;

                default: //具体含义见SnmpException
                    sDeviceStatus = "unknown";
                    break;
            }

            return sDeviceStatus;
        }

        /// <summary>
        /// 通过snmp获取设备状态
        /// </summary>
        /// <param name="alValue">返回值，标识设备状态</param>
        /// <returns>成功返回0，失败为其他</returns>
        private int GetSnmpValue(ArrayList alValue)
        {
            ArrayList alOid = new ArrayList();
            SNMP snmp = new SNMP(this._strDeviceIP);
            SetStateOid(alOid);
            snmp.SetPduType(PduType.Get);
            snmp.SetPduContent(alOid);

            int iSuccess = snmp.GetValue(alOid, alValue);

            return iSuccess;
        }

        /// <summary>
        /// 设置状态Oid
        /// </summary>
        /// <param name="alOid"></param>
        private void SetStateOid(ArrayList alOid)
        {
            alOid.Add(".1.3.6.1.2.1.25.3.5.1.1.1"); //状态
            alOid.Add(".1.3.6.1.2.1.25.3.5.1.2.1"); //错误
        }

        //获取东芝设备状态
        private string GetToshibaDeviceState()
        {
            try
            {
                //FileInfo file = new FileInfo("TestFile.txt");  
                //StreamWriter sw= file.AppendText();      
                //sw.WriteLine("开始---------");
                //sw.Flush();

                ArrayList alOid = new ArrayList();
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.7.1.3.1.1.6.1.2");
                ArrayList alValue = new ArrayList();

                string sDeviceState = "idle";
                string sOid;
                string sValue;
                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.SetPduType(PduType.GetNext);


                while (true)
                {
                    snmp.SetPduContent(alOid);
                    //sw.WriteLine(DateTime.Now.ToString("yyyy-mm-dd hh:MM:ss:fff"));
                    //string ss = (string)alOid[0];
                    //sw.WriteLine("上一个Oid:" + ss);
                    //sw.Flush();
                    int state = snmp.GetNextValue(ref alOid, alValue);

                    switch ((SnmpStatus)state)
                    {
                        case SnmpStatus.NoError:
                            break;

                        case SnmpStatus.SocketError:
                            return "connectFail";

                        default:
                            return "unknown";
                    }

                    sOid = (string)alOid[0]; //下一个节点的Oid值
                    sValue = (string)alValue[0]; //下一个节点的value
                                                 //sw.WriteLine(DateTime.Now.ToString("yyyy-mm-dd hh:MM:ss:fff"));
                                                 //sw.WriteLine("本次Oid:"+sOid);
                                                 //sw.WriteLine("Value:" + sValue);
                                                 //sw.WriteLine("**************************");
                                                 //sw.Flush();
                    if (sOid.Contains("1.3.6.1.4.1.1129.2.3.50.1.7.1.3.1.1.6.1.2")) //是状态节点
                    {
                        sDeviceState = JudgeToshibaJobState(int.Parse(sValue));

                        if (sDeviceState != "idle")
                        {
                            break;
                        }
                    }
                    else //不是状态节点
                    {
                        break;
                    }
                }

                // sw.Close();  
                return sDeviceState;

            }
            catch (System.Exception e)
            {
                throw new Exception("获取东芝设备状态异常", e);
                //return "unknown";
            }
        }

        private string JudgeToshibaJobState(int iJobState)
        {
            string strReturn;

            //注释后加*的为常规流程中内容,数字为相应顺序
            switch (iJobState)
            {
                case 4097://缺纸
                    //strReturn = "noPaper";
                    strReturn = "打印机缺纸";
                    break;

                case 12547://卡纸
                case 258://2830C界面上显示
                    //strReturn = "jammed";
                    strReturn = "打印机卡纸";
                    break;

                case 42://门开
                    //strReturn = "doorOpen";
                    strReturn = "打印机门被打开";
                    break;

                case 196609://针对6.1.3与实际不符，可看作预处理，因为66539(真正正在打印）和196613（真正完成）(RIP Completed)时一直存在
                            //这部分目前只在取设备状态时使用，因为从6.1.2开始，所有65539时，不需要再到196609，可以看做空闲；196613时，看做空闲没问题
                case 196613: // 完成               *3
                case 196614: // 删除(RIP Deleted)
                case 196615: // 取消(RIP Canceled)
                case 196616: // 失败(RIP Failed)
                    strReturn = "idle";
                    break;

                case 16387://缺墨
                    //strReturn = "noToner";
                    strReturn = "打印机缺墨";
                    break;

                case 65537://Spooling
                case 65538://正在处理(RIP Analyzing)          *0
                case 65543: //等待                 *1
                case 65539: //正在打印             *2
                case 15://复印正在等待,东芝资料中没有，实际测试得到
                    strReturn = "printing";
                    break;

                default:
                    strReturn = "other";//作业中止时，还有其他错误值，未经测试，临时以other表示
                                        //EventData eventData = new EventData(4);
                                        //eventData.Add("IP:", this._strDeviceIP);
                                        //eventData.Add("Manu:", this._strDeviceManu);
                                        //eventData.Add("Model:", this._strDeviceModel);
                                        //eventData.Add("strReturn", iJobState);
                                        //ExecuteEventLog.InsertEventLog("DeviceManager.JudgeToshibaJobState", EventLogItem.ID_100003,
                                        //        EventLevel.Information, EventLogItem.Summary_100003, eventData);
                    break;
            }

            return strReturn;
        }

        public int GetToshibJobID(ref int iJobID)
        {
            try
            {
                ArrayList alOid = new ArrayList();
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.7.1.3.1.1.6.1.2");//作业状态
                ArrayList alValue = new ArrayList();
                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.SetPduType(PduType.GetNext);

                string sDeviceState = "idle";
                string sOid;
                string sValue;
                while (true)
                {
                    snmp.SetPduContent(alOid);
                    int state = snmp.GetNextValue(ref alOid, alValue);
                    if ((SnmpStatus)state != SnmpStatus.NoError)
                    {
                        return -1;
                    }

                    sOid = (string)alOid[0];
                    sValue = (string)alValue[0];
                    if (sOid.Contains("1.3.6.1.4.1.1129.2.3.50.1.7.1.3.1.1.6.1.2")) //是状态节点
                    {
                        sDeviceState = JudgeToshibaJobState(int.Parse(sValue));

                        if (sDeviceState != "idle")
                        {
                            iJobID = int.Parse(sOid.Substring(sOid.LastIndexOf(".") + 1));
                            return 1;
                        }
                    }
                    else //不是状态节点
                    {
                        break;
                    }
                }

                return 0;
            }
            catch (System.Exception e)
            {

                return -1;
            }

        }

        private string JudgeSamsungDeviceState(ArrayList alValue)
        {
            try
            {
                string strDeviceState = "idle";
                int iDeviceState = int.Parse(alValue[0].ToString());
                string strErrorCode = (string)alValue[1];
                int iErrorCode = 0;

                switch (iDeviceState)
                {
                    case 1:
                    case 3:
                        if (strErrorCode == "00 00")
                        {
                            return "idle";
                        }
                        break;

                    case 2:
                        return "unknown";

                    case 4:
                        return "printing";

                    case 5:
                        return "warmup";

                    default:
                        //EventData eventData = new EventData(1);
                        //eventData.Add("iDeviceState", iDeviceState);
                        //ExecuteEventLog.InsertEventLog("JudgeSamsungDeviceState", EventLogItem.ID_100003,
                        //        EventLevel.Information, EventLogItem.Summary_100003, eventData);
                        return "unknown";
                }

                if (strErrorCode.Length == 5) //2个16进制数“xx xx”
                {
                    iErrorCode = Convert.ToInt32(strErrorCode.Substring(3, 2) + strErrorCode.Substring(0, 2), 16);
                }
                else
                {
                    //此段代码是未修改snmpsharpnet时出现的bug，即原来在ocetstring的IsHex属性中为 && (这是错误的）
                    if (strErrorCode.Length == 2) // "ke"为转换为16进制数
                    {
                        string high = Convert.ToString(strErrorCode[0], 16);
                        high = high.PadLeft(2, '0');

                        string low = Convert.ToString(strErrorCode[1], 16);
                        low = low.PadLeft(2, '0');

                        iErrorCode = Convert.ToInt32(low + high, 16);
                    }
                    else
                    {
                        //EventData eventData = new EventData(1);
                        //eventData.Add("strErrorCode", strErrorCode);
                        //ExecuteEventLog.InsertEventLog("JudgeSamsungDeviceState", EventLogItem.ID_100003,
                        //        EventLevel.Information, EventLogItem.Summary_100003, eventData);
                        return "unknown";
                    }
                }

                strDeviceState = "idle";
                switch (iErrorCode)
                {
                    //对应着纸盒和墨盒等，因此为其他
                    case 0x0100: //serviceRequested
                    case 0x0100 | 0x0200:
                        if (JudgePrtAlertDesc())
                        {
                            strDeviceState = "other";
                        }
                        break;

                    //纸张
                    case 0x4004: //noPaper(0x4000)+InputTrayEmpty(0004);   snmpsharpnet未修改在OcetString类中IsHex时snmp的返回值是"@"；修改后返回"04 40"
                        if (JudgePrtAlertDesc())
                        {
                            //strDeviceState = "noPaper"; //
                            strDeviceState = "打印机缺纸";
                        }
                        break;

                    //卡纸
                    case 0x0400: //jammed
                        if (JudgePrtAlertDesc())
                        {
                            //strDeviceState = "jammed";
                            strDeviceState = "打印机卡纸";
                        }
                        break;

                    //门开
                    case 0x0080: //InputTrayMissing
                    case 0x0800: //doorOpen

                        if (JudgePrtAlertDesc())
                        {
                            //strDeviceState = "doorOpen";
                            strDeviceState = "打印机门被打开";
                        }
                        break;

                    //墨盒
                    case 0x0020://markerSupplyMissing
                    case 0x1000: //notorner
                    case 0x2000:
                        if (JudgePrtAlertDesc())
                        {
                            //strDeviceState = "noToner";
                            strDeviceState = "打印机缺墨";
                        }

                        break;

                    //防止遗漏项
                    default:
                        if (JudgePrtAlertDesc())
                        {
                            //strDeviceState = "other";
                            strDeviceState = "打印机发生故障，请检查";
                        }

                        //EventData eventData = new EventData(1);
                        //eventData.Add("strErrorCode", strErrorCode);
                        //ExecuteEventLog.InsertEventLog("JudgeSamsungDeviceState", EventLogItem.ID_100003,
                        //        EventLevel.Information, EventLogItem.Summary_100003, eventData);
                        break;
                }

                return strDeviceState;
            }
            catch (System.Exception e)
            {
                if (alValue != null && alValue.Count > 0)
                {


                }

                return "unknown";
            }
        }

        private bool JudgePrtAlertDesc()
        {
            try
            {
                ArrayList alValue = new ArrayList();
                ArrayList alOid = new ArrayList();
                alOid.Add("1.3.6.1.2.1.43.18.1.1.8.1");
                string strValue;
                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.SetPduType(PduType.GetNext);
                while (true)
                {
                    snmp.SetPduContent(alOid);
                    int state = snmp.GetNextValue(ref alOid, alValue);
                    if ((SnmpStatus)state != SnmpStatus.NoError)
                    {
                        break;
                    }

                    string strOid = (string)alOid[0];
                    if (strOid.Contains("1.3.6.1.2.1.43.18.1.1.8.1"))
                    {
                        strValue = (string)alValue[0];
                        if (strValue.Contains("Printing is not available") || strValue.Contains("Printing has stopped") || strValue.Contains("All machine services are currently disabled"))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (System.Exception e)
            {

                return false;
            }

        }

        private string JudgeRicohHPDeviceState(ArrayList alValue)
        {
            try
            {
                string strReturn = "";

                string strDeviceState = (string)alValue[0];//状态
                string strDeviceError = (string)alValue[1];//错误

                switch (strDeviceState)
                {
                    case "1":
                        strReturn = JudgeRicohHPDeviceError(strDeviceError);
                        break;

                    case "2":
                        strReturn = "unknown";
                        break;

                    case "3":
                        strReturn = "idle";
                        break;

                    case "4":
                        strReturn = "printing";
                        break;

                    case "5":
                        strReturn = "warmup";
                        break;
                }

                return strReturn;
            }

            catch (Exception e)
            {


                return "unknown";
            }

        }


        public bool GetSamsungState(ref string DeviceState, ref string ConsoleText)
        {
            bool bReturn = GetConsoleText(ref ConsoleText);
            if (!bReturn)
            {
                DeviceState = "connectFail";
                return true;
            }

            if (ConsoleText.Contains("睡眠中") || ConsoleText.Contains("就绪")
                || ConsoleText.Contains("Sleeping") || ConsoleText.Contains("Ready"))
            {
                DeviceState = "idle";
            }
            else
                if (ConsoleText.Contains("正在打印") || ConsoleText.Contains("Printing"))
            {
                DeviceState = "printing";
            }
            else
            {
                DeviceState = "other";
            }



            ////打印前
            //if (!IsPrinting)
            //{
            //    if (ConsoleText.Contains("睡眠中") || ConsoleText.Contains("就绪") || ConsoleText.Contains("墨粉不足")
            //     || ConsoleText.Contains("Sleeping") || ConsoleText.Contains("Ready") || ConsoleText.Contains("Toner Low"))
            //    {
            //        DeviceState = "idle";
            //    }
            //    else
            //    {
            //        DeviceState = "other";
            //    }


            //}
            //else//打印中
            //{
            //    if (ConsoleText.Contains("就绪") || ConsoleText.Contains("Ready"))
            //    {
            //        DeviceState = "idle";
            //    }
            //    else

            //        if (ConsoleText.Contains("正在打印") || ConsoleText.Contains("Printing"))
            //        {
            //            DeviceState = "printing";
            //        }
            //        else
            //        {
            //            DeviceState = "other";
            //        }
            //}

            return true;
        }

        public bool GetConsoleText(ref string ConsoleText)
        {
            try
            {
                ArrayList alOid = new ArrayList();
                ArrayList alValue = new ArrayList();
                alOid.Add(".1.3.6.1.2.1.43.16.5.1.2.1.1");
                alOid.Add(".1.3.6.1.2.1.43.16.5.1.2.1.2");

                //查询
                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.SetPduType(PduType.Get);
                snmp.SetPduContent(alOid);

                int iSuccess = snmp.GetValue(alOid, alValue);
                if ((SnmpStatus)iSuccess != SnmpStatus.NoError)
                {
                    return false;
                }

                ConsoleText = ConvertToGB((string)alValue[0]);
                ConsoleText += "|" + ConvertToGB((string)alValue[1]);

                return true;
            }
            catch (System.Exception)
            {
                return true;
            }

        }

        private string ConvertToGB(string utfString)
        {
            bool IsGB = true;
            if (utfString.Trim() == string.Empty)//判断是否为全是空格
            {
                return utfString;
            }

            //汉字用16位2进制数表示如E4 5A；还有纯英文Printing
            for (int i = 2; i < utfString.Length; i = i + 3)
            {
                if (utfString[i] != ' ')
                {
                    IsGB = false;
                    break;
                }
            }

            //不是汉字直接返回
            if (!IsGB)
            {
                return utfString;
            }

            ArrayList utfArray = new ArrayList();
            //try
            //{
            int len = utfString.Length == 2 ? 1 : (utfString.Length + 1) / 3;
            for (int i = 0; i < len; i++)
            {
                utfArray.Add(Convert.ToByte(utfString.Substring(3 * i, 2), 16));
            }
            //}
            //catch (System.Exception e)
            //{
            //    return "e" + utfString;
            //}

            byte[] utf = (byte[])utfArray.ToArray(typeof(Byte));
            byte[] gb = Encoding.Convert(Encoding.GetEncoding("utf-8"), Encoding.GetEncoding("gb2312"), utf);
            string s = Encoding.GetEncoding("gb2312").GetString(gb);

            return s;
        }


        private string JudgeRicohHPDeviceError(string strDeviceError)
        {
            try
            {
                int iDeviceError = 0;

                if (_strDeviceManu == "LEXMARK")
                {
                    strDeviceError = strDeviceError.Substring(0, 2);
                }
                //错误值的一个ascii，范围是0~255,由于包含不可输出字符返回16进制字符串，因此长度为1时直接转换，为2时是16进制数
                if (strDeviceError.Length == 1)
                {
                    iDeviceError = strDeviceError[0];
                }
                else
                {
                    if (strDeviceError.Length == 2)
                    {
                        iDeviceError = Convert.ToInt32(strDeviceError, 16);
                    }
                    else
                    {
                        //EventData eventData = new EventData(1);
                        //eventData.Add("strDeviceError", strDeviceError);
                        //ExecuteEventLog.InsertEventLog("JudgeRicohHPDeviceState", EventLogItem.ID_100003,
                        //        EventLevel.Information, EventLogItem.Summary_100003, eventData);

                        return "unknown";
                    }
                }

                //转换为2进制
                string strBinary = Convert.ToString(iDeviceError, 2);
                strBinary = strBinary.PadLeft(8, '0');

                if (strBinary[1] == '1') //缺纸
                {
                    //return "noPaper";
                    return "打印机缺纸";

                }

                if (strBinary[3] == '1') //缺墨
                {
                    //return "noToner";
                    return "打印机缺墨";
                }

                if (strBinary[4] == '1') //门开
                {
                    //return "doorOpen";
                    return "打印机门被打开";
                }

                if (strBinary[5] == '1') //卡纸
                {
                    //return "jammed";
                    return "打印机卡纸";
                }

                if (strBinary[6] == '1') //离线
                {
                    //return "offline";
                    return "打印机离线";
                }

                {
                    //EventData eventData = new EventData(2);
                    //eventData.Add("strDeviceError", strDeviceError);
                    //eventData.Add("strBinary", strBinary);
                    //ExecuteEventLog.InsertEventLog("JudgeRicohHPDeviceState", EventLogItem.ID_100003,
                    //        EventLevel.Information, EventLogItem.Summary_100003, eventData);
                }

                //return "other";
                return "打印机发生故障，请检查";
            }
            catch (System.Exception e)
            {




                //return "unknown";
                return "打印机发生未知故障，请检查";
            }

        }

        //////////////////////////获取作业状态///////////////////////////////////
        public string GetJobState()//打印机状态
        {
            string sDeviceStatus = "";
            switch (_strDeviceManu)
            {
                case "RICOH":
                case "HP":
                case "SAMSUNG":
                    {
                        sDeviceStatus = GetDeviceState();
                    }
                    break;
                default:
                    //EventData eventData = new EventData(1);
                    //eventData.Add("Manu:", this._strDeviceManu);
                    //ExecuteEventLog.InsertEventLog("DeviceManager.GetDeviceState", EventLogItem.ID_100004,
                    //        EventLevel.Information, EventLogItem.Summary_100004, eventData);
                    break;
            }

            return sDeviceStatus;
        }

        public string GetToshibaJobState(int iJobID)//作业状态
        {
            try
            {
                string strDeviceStatus = "";

                ArrayList alOid = new ArrayList();
                ArrayList alValue = new ArrayList();
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.7.1.3.1.1.6.1.2." + iJobID.ToString()); //可以通过该节点得到指定作业的状态

                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.SetPduType(PduType.Get);
                snmp.SetPduContent(alOid);
                int iSuccess = snmp.GetValue(alOid, alValue);

                switch ((SnmpStatus)iSuccess)
                {
                    case SnmpStatus.NoError:
                        strDeviceStatus = JudgeToshibaJobState(int.Parse(alValue[0].ToString()));
                        break;

                    case SnmpStatus.SocketError:
                        strDeviceStatus = "connectFail";
                        break;

                    case SnmpStatus.NoSuchName: //NoSuchName,意味着该节点已经消失
                                                //strDeviceStatus = GetToshibaDeviceState();
                        strDeviceStatus = "idle";
                        break;
                    default:
                        strDeviceStatus = "unknown";
                        //EventData eventData = new EventData(4);
                        //eventData.Add("IP:", this._strDeviceIP);
                        //eventData.Add("Manu:", this._strDeviceManu);
                        //eventData.Add("Model:", this._strDeviceModel);
                        //eventData.Add("iJobID", iJobID);
                        //ExecuteEventLog.InsertEventLog("DeviceManager.GetToshibaJobState", EventLogItem.ID_100003,
                        //        EventLevel.Information, EventLogItem.Summary_100003, eventData);
                        break;
                }

                return strDeviceStatus;
            }
            catch (System.Exception e)
            {

                return "unknown";
            }

        }

        //////////////////////////获取计数器///////////////////////////////////
        public bool GetDeviceCount(ref PrinterCounter printerCounter)//打印机状态
        {
            try
            {
                if (_PublicCounter)
                {
                    return GetPublicCounter(ref printerCounter);
                }

                bool bReturn = false;
                switch (_strDeviceManu)
                {
                    case "RICOH":
                        bReturn = GetRicohCount(ref printerCounter);
                        break;
                    case "HP":
                        bReturn = GetHPCount(ref printerCounter);
                        break;
                    case "TOSHIBA":
                        bReturn = GetToshibaCount(ref printerCounter);
                        break;
                    case "SAMSUNG":
                        bReturn = GetSamsungCount(ref printerCounter);
                        break;
                    case "LEXMARK":
                        bReturn = GetLexmarkCount(ref printerCounter);
                        break;
                    //case "C_":
                    //    bReturn = GetCommonCount(ref printerCounter);
                    //    break;

                    default:
                        //EventData eventData = new EventData(1);
                        //eventData.Add("_strDeviceManu", _strDeviceManu);
                        //ExecuteEventLog.InsertEventLog("DeviceManager.GetDeviceCount(", EventLogItem.ID_100004,
                        //        EventLevel.Information, EventLogItem.Summary_100004, eventData);
                        break;

                }
                return bReturn;
            }
            catch (System.Exception e)
            {

                return false;
            }
        }

        private bool GetRicohCount(ref PrinterCounter printerCounter)
        {
            try
            {
                printerCounter = new PrinterCounter();//格式化的计数器，返回值
                ArrayList alOid = new ArrayList();//计数器oid
                ArrayList alValue = new ArrayList();//未经格式化的计数器，由snmp返回

                //加载xml
                XML x = new XML();
                x.IsMonitorCounter = isMonitorCounter;
                if (!x.LoadXml(RicohXmlPath))
                {
                    return false;
                }

                //获取所有计数器oid
                if (!x.GetRicohOid(_strDeviceModel, alOid))
                {
                    return false;
                }
                SNMP snmp = new SNMP(this._strDeviceIP);
                //通过snmp获取计数器值
                snmp.SetPduType(PduType.Get);
                snmp.SetPduContent(alOid);
                int iSuccess = snmp.GetValue(alOid, alValue);

                if ((SnmpStatus)iSuccess != SnmpStatus.NoError)
                {
                    return false;
                }

                //转换计数器格式
                return x.GetRicohPrinterCounter(alValue, ref printerCounter);
            }
            catch (System.Exception e)
            {

                return false;
            }
        }

        private bool GetHPCount(ref PrinterCounter printerCounter)
        {
            try
            {
                printerCounter = new PrinterCounter();
                ArrayList alOid = new ArrayList();
                ArrayList alValue = new ArrayList();

                //加载xml
                XML x = new XML();
                if (!x.LoadXml(HpXmlPath))
                {
                    return false;
                }

                //获取所有计数器oid
                if (!x.GetHPOid(_strDeviceModel, alOid))
                {
                    return false;
                }

                //设置pdu的绑定Vb的最大数量
                int maxValue = x.GetHPMaxValue();
                if (maxValue == -1)
                {
                    return false;
                }
                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.MaxValue = maxValue; //依一次snmp获取数据数量

                //通过snmp获取计数器值
                snmp.SetPduType(PduType.Get);
                snmp.SetPduContent(alOid);
                int iSuccess = snmp.GetHPCounterBySNMP(alOid, alValue);
                if ((SnmpStatus)iSuccess != SnmpStatus.NoError || alValue.Count != alOid.Count)
                {
                    return false;
                }

                //转换计数器格式
                return x.GetHpPrinterCounter(alValue, ref printerCounter);
            }
            catch (System.Exception e)
            {


                return false;
            }
        }

        private bool GetToshibaCount(ref PrinterCounter printerCounter)
        {
            try
            {
                if (isMonitorCounter)
                {
                    return GetToshibaAllCount(ref printerCounter);
                }
                printerCounter = new PrinterCounter();
                ArrayList alOid = new ArrayList();
                ArrayList alValue = new ArrayList();

                //1、2、3、4 分别对应彩色、双色、黑白、总计数,打印计数（大纸张）A3及以上，已经折算A4
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.209.1.1");
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.209.1.2");
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.209.1.3");
                //alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.209.1.4");

                //1、2、3、4 分别对应彩色、双色、黑白、总计数,打印计数（小纸张）A4及以下 ，已经折算A4
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.210.1.1");
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.210.1.2");
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.210.1.3");
                // alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.210.1.4");


                //1、2、3、4 分别对应彩色、双色、黑白、总计数,打印计数（小纸张）A4及以下 ，已经折算A4
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.227.1.1");
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.227.1.2");
                alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.227.1.3");
                //alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.227.1.4");


                //查询
                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.SetPduType(PduType.Get);
                snmp.SetPduContent(alOid);

                int iSuccess = snmp.GetValue(alOid, alValue);
                if ((SnmpStatus)iSuccess != SnmpStatus.NoError)
                {
                    return false;
                }

                ArrayList alInt = new ArrayList();

                foreach (string strValue in alValue)
                {
                    int iValue;
                    if (int.Parse(strValue) == -1)
                    {
                        iValue = 0;
                    }
                    else
                    {
                        iValue = int.Parse(strValue);
                    }

                    alInt.Add(iValue);
                }

                printerCounter.Black = (int)alInt[2] + (int)alInt[5];
                printerCounter.Color = (int)alInt[0] + (int)alInt[1]
                                      + (int)alInt[3] + (int)alInt[4];
                printerCounter.A3 = ((int)alInt[0] + (int)alInt[1] + (int)alInt[2]) / 2;
                printerCounter.Duplex = (int)alInt[6] + (int)alInt[7] + (int)alInt[8];

                //if(printerCounter.Duplex == (printerCounter.Black + printerCounter.Color + printerCounter.Duplex))
                //{
                //    printerCounter.Duplex = printerCounter.Duplex / 2 + printerCounter.Duplex % 2;//东芝张双面计2，所以除2
                //}
                //else
                //{
                //    if((printerCounter.Black + printerCounter.Color)/2 >0 && (printerCounter.Black + printerCounter.Color)/2 >0)
                //}


                return true;
            }
            catch (System.Exception e)
            {

                return false;
            }

        }

        private bool GetSamsungCount(ref PrinterCounter printerCounter)
        {
            try
            {
                printerCounter = new PrinterCounter();
                ArrayList alOid = new ArrayList();
                ArrayList alValue = new ArrayList();

                //getnext后计数器才改变
                alOid.Add("1.3.6.1.4.1.236.11.5.11.53.11.2.1");
                SNMP snmp = new SNMP(this._strDeviceIP);
                snmp.SetPduType(PduType.GetNext);
                snmp.SetPduContent(alOid);
                int iSuccess = snmp.GetNextValue(ref alOid, alValue);
                if ((SnmpStatus)iSuccess != SnmpStatus.NoError)
                {
                    return false;
                }

                //下次使用前清空
                alOid.Clear();
                alValue.Clear();

                //加载xml
                XML x = new XML();
                if (!x.LoadXml(SamsungXmlPath))
                {
                    return false;
                }

                //获取所有计数器oid
                if (!x.GetSamsungOid(_strDeviceModel, alOid))
                {
                    return false;
                }

                //通过snmp获取计数器值
                snmp.SetPduType(PduType.Get);
                snmp.SetPduContent(alOid);
                alValue.Clear();
                iSuccess = snmp.GetValue(alOid, alValue);
                if ((SnmpStatus)iSuccess != SnmpStatus.NoError)
                {
                    return false;
                }

                //转换计数器格式
                return x.GetSamsungPrinterCounter(alValue, ref printerCounter);
            }
            catch (System.Exception e)
            {

                return false;
            }
        }

        private bool GetLexmarkCount(ref PrinterCounter printerCounter)
        {
            printerCounter = new PrinterCounter();
            ArrayList alOid = new ArrayList();
            ArrayList alValue = new ArrayList();
            alOid.Add("1.3.6.1.4.1.641.2.1.5.2.0");
            alOid.Add("1.3.6.1.4.1.641.2.1.5.3.0");
            //if (isMonitorCounter)
            //{
            //    alOid.Add("1.3.6.1.4.1.641.2.1.5.2.0");//实际打印复印页数
            //    alOid.Add("1.3.6.1.4.1.641.2.1.5.7.0");
            //}
            //alOid.Add("1.3.6.1.4.1.641.2.1.5.2.0");

            //查询
            SNMP snmp = new SNMP(this._strDeviceIP);
            snmp.SetPduType(PduType.Get);
            snmp.SetPduContent(alOid);

            int iSuccess = snmp.GetValue(alOid, alValue);
            if ((SnmpStatus)iSuccess != SnmpStatus.NoError)
            {
                return false;
            }

            printerCounter.Black = int.Parse((string)alValue[0]);
            printerCounter.Color = int.Parse((string)alValue[1]);
            printerCounter.A3 = 0;
            printerCounter.Duplex = 0;
            //if (isMonitorCounter)
            //{
            //    //printerCounter.Black = int.Parse((string)alValue[1]);// -int.Parse((string)alValue[2]);
            //    //printerCounter.CopyBlack = int.Parse((string)alValue[2]);
            //}

            return true;
        }


        private bool GetPublicCounter(ref PrinterCounter printerCounter)
        {
            printerCounter = new PrinterCounter();
            ArrayList alOid = new ArrayList();
            ArrayList alValue = new ArrayList();
            alOid.Add(".1.3.6.1.2.1.43.10.2.1.4.1.1");

            //查询
            SNMP snmp = new SNMP(this._strDeviceIP);
            snmp.SetPduType(PduType.Get);
            snmp.SetPduContent(alOid);

            int iSuccess = snmp.GetValue(alOid, alValue);
            if ((SnmpStatus)iSuccess != SnmpStatus.NoError)
            {
                return false;
            }

            printerCounter.Black = int.Parse((string)alValue[0]);
            printerCounter.Color = 0;
            printerCounter.A3 = 0;
            printerCounter.Duplex = 0;

            return true;

        }


        public bool FromatCounter(ref int black, ref int color, ref int A3, ref int duplex)
        {
            switch (this._strDeviceManu)
            {
                case "RICOH":
                    {
                        ////A3双计
                        //if ((black + color) == A3) //A3没有双计,只有A3
                        //{
                        //    black = 2 * black;
                        //    color = 2 * color;
                        //}
                        //else
                        //    if ((black + color) < 2 * A3) //A3没有双计,还有A4
                        //    {
                        //        if (black > 0)
                        //        {
                        //            black = black + A3;
                        //        }
                        //        else
                        //        {
                        //            color = color + A3;
                        //        }
                        //    }

                        //有双面
                        if (duplex > 0)
                        {
                            duplex = duplex + (A3 + 1) / 2;

                        }

                        //if(duplex > 0)
                        //{
                        //    duplex = (black + color - 2 * A3) / 2 <=duplex ?
                        //           (black + color - 2 * A3) / 2 + (duplex - (black + color - 2 * A3) / 2) * 2
                        //           :duplex;

                        //}
                    }
                    break;
                case "HP":
                    {

                    }
                    break;
                case "TOSHIBA":
                    {
                        if (duplex > 0)
                        {
                            duplex = (black + color - 2 * A3) / 2
                              + A3 - A3 % 2;
                        }

                    }
                    break;
                case "SAMSUNG":
                    {

                    }
                    break;
                default:
                    break;
            }

            return true;
        }
        private bool GetToshibaAllCount(ref PrinterCounter printerCounter)
        {
            ArrayList alOid = new ArrayList();
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,打印计数（大纸张）A3及以上，已经折算A4
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.209.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.209.1.4");
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,打印计数（小纸张）A4及以下 ，已经折算A4
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.210.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.210.1.4");
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,打印双面
            //打印1个双面，小纸张计2  
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.227.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.227.1.4");

            ArrayList alValue = new ArrayList();
            SNMP snmp = new SNMP(_strDeviceIP);
            snmp.SetPduType(PduType.Get);

            alValue.Clear();
            snmp.SetPduContent(alOid);
            int state = snmp.GetValue(alOid, alValue);
            if (state != 0)
            {
                return false;
            }
            printerCounter.Black = int.Parse(alValue[0].ToString()) + int.Parse(alValue[2].ToString());
            printerCounter.Color = int.Parse(alValue[1].ToString()) + int.Parse(alValue[3].ToString()) - printerCounter.Black;
            printerCounter.Duplex = (int.Parse(alValue[5].ToString())) / 2;
            printerCounter.A3 = (int.Parse(alValue[1].ToString())) / 2;

            alOid.Clear();
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,复印计数（大纸张）A3及以上，已经折算A4
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.211.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.211.1.4");
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,复印计数（小纸张）A4及以下 ，已经折算A4
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.212.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.212.1.4");
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,复印双面
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.229.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.229.1.4");
            alValue.Clear();
            snmp.SetPduContent(alOid);
            state = snmp.GetValue(alOid, alValue);
            if (state != 0)
            {
                return false;
            }

            printerCounter.Black += int.Parse(alValue[0].ToString()) + int.Parse(alValue[2].ToString());
            printerCounter.Color += int.Parse(alValue[1].ToString()) + int.Parse(alValue[3].ToString()) - int.Parse(alValue[0].ToString()) - int.Parse(alValue[2].ToString());
            printerCounter.Duplex += (int.Parse(alValue[5].ToString())) / 2;
            printerCounter.A3 += (int.Parse(alValue[1].ToString())) / 2;

            alOid.Clear();
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,list计数（大纸张）A3及以上，已经折算A4
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.225.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.225.1.4");
            //1、2、3、4 分别对应彩色、双色、黑白、总计数,list计数（小纸张）A4及以下 ，已经折算A4
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.226.1.3");
            alOid.Add("1.3.6.1.4.1.1129.2.3.50.1.3.21.6.1.226.1.4");
            alValue.Clear();
            snmp.SetPduContent(alOid);
            state = snmp.GetValue(alOid, alValue);
            if (state != 0)
            {
                return false;
            }
            printerCounter.Black += int.Parse(alValue[0].ToString()) + int.Parse(alValue[2].ToString());
            printerCounter.Color += int.Parse(alValue[1].ToString()) + int.Parse(alValue[3].ToString()) - int.Parse(alValue[0].ToString()) - int.Parse(alValue[2].ToString());
            printerCounter.A3 += (int.Parse(alValue[1].ToString())) / 2;

            return true;
        }

    }
    public class SNMP
    {
        private int port = 161;
        private int timeout = 400;
        //private int retry = 4;

        //每次通过snmp查询的最大数
        private int maxValue = 1; //测5035 ，此时配置文件的值都改为原来的2倍，发现为54时snmp错误 too big
        //调用get方法   测试只取一个节点，10次共156ms；取计数器3个节点，10次共203ms
        private AgentParameters param;
        private UdpTarget target;
        private string _deviceIP;
        private IpAddress agent;
        private PduType _pduType;
        private Pdu pdu;

        /// <summary>
        /// 设置一次通过snmp获取计数器的数量，一般超过50收不到返回值
        /// </summary>
        public int MaxValue
        {
            get { return maxValue; }
            set { this.maxValue = value; }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="deviceIP"></param>
        public SNMP(string deviceIP)
        {
            this._deviceIP = deviceIP;
            OctetString community = new OctetString("public");
            param = new AgentParameters(community);
            //param.Version = SnmpVersion.Ver1; //设备snmp版本
            agent = new IpAddress(deviceIP);
        }

        /// <summary>
        /// Get方法获取snmp值
        /// </summary>
        /// <param name="alOid"></param>
        /// <param name="alValue"></param>
        /// <returns></returns>
        public int GetValue(ArrayList alOid, ArrayList alValue)
        {
            ArrayList alPduOid = new ArrayList();
            alValue.Clear();

            int state = GetSnmpResult(alPduOid, alValue);
            return state;
        }

        /// <summary>
        /// Getnext获取snmp值
        /// </summary>
        /// <param name="alOid"></param>
        /// <param name="alValue"></param>
        /// <returns></returns>
        public int GetNextValue(ref ArrayList alOid, ArrayList alValue)
        {
            ArrayList alPduOid = new ArrayList();
            alValue.Clear();

            int state = GetSnmpResult(alPduOid, alValue);

            alOid = alPduOid;
            return state;
        }

        /// <summary>
        /// 获取HP计数器
        /// </summary>
        /// <param name="alOid"></param>
        /// <param name="alValue"></param>
        /// <returns></returns>
        public int GetHPCounterBySNMP(ArrayList alOid, ArrayList alValue)
        {
            try
            {
                int len = (alOid.Count % maxValue == 0 ? 0 : 1) + alOid.Count / maxValue; //每一次最多50个oid

                ArrayList alPduOid = new ArrayList();
                int state = 0;
                for (int k = 0; k < len; k++)
                {
                    pdu.VbList.Clear();

                    if ((k + 1) == len) //最后一次时需要判断等于max还是小于max
                    {
                        if (alOid.Count % maxValue == 0) //等于max
                        {
                            for (int i = 0; i < maxValue; i++)
                            {
                                pdu.VbList.Add((string)alOid[k * maxValue + i]); //sysDescr
                            }
                        }
                        else  //小于max
                        {
                            for (int i = 0; i < alOid.Count % maxValue; i++)
                            {
                                pdu.VbList.Add((string)alOid[k * maxValue + i]); //sysDescr
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < maxValue; i++)
                        {
                            pdu.VbList.Add((string)alOid[k * maxValue + i]); //sysDescr
                        }
                    }

                    state = GetSnmpResult(alPduOid, alValue);
                    if (state != 0)
                    {
                        return state;
                    }
                }

                return state;
            }
            catch (System.Exception e)
            {


                return -1;
            }

        }

        /// <summary>
        /// 设置Pdu的操作类型
        /// </summary>
        /// <param name="pduType"></param>
        public void SetPduType(PduType pduType)
        {
            try
            {
                pdu = new Pdu(pduType);
                this._pduType = pduType;
            }
            catch (System.Exception e)
            {
                e.Data.Add("函数SetPduType异常:", e.Message);
                throw e;
            }

        }

        /// <summary>
        /// 为PDU添加Vb对象
        /// </summary>
        /// <param name="alOid"></param>
        public void SetPduContent(ArrayList alOid)
        {
            try
            {
                pdu.VbList.Clear();
                for (int i = 0; i < alOid.Count; i++)
                {
                    pdu.VbList.Add((string)alOid[i]); //sysDescr
                }
            }
            catch (System.Exception e)
            {
                e.Data.Add("函数SetPduContent异常", e.Message);
                throw e;
            }

        }

        private int GetSnmpResult(ArrayList alPduOid, ArrayList alValue)
        {
            try
            {  //调用get方法   测试只取一个节点，10次共156ms；取计数器3个节点，10次共203ms
                target = new UdpTarget((IPAddress)agent, port, timeout, 4); //测试发现实际超时都加了0.6s

                SnmpV1Packet result = null;

                try
                {
                    result = (SnmpV1Packet)target.Request(pdu, param);
                }
                catch (SnmpException)
                {
                    // if (e.ErrorCode != 12)//打印机不在线，返回12；当试图连接一个在线的PC的IP时，返回0（实际是10054），特殊处理
                    {
                        //ExecuteEventLog.InsertEventLog("DeviceManager.GetDeviceState",
                        //  EventLogItem.ID_100002, EventLevel.Error, EventLogItem.Summary_100002, e);
                    }

                    target.Close();
                    return (int)SnmpStatus.SocketError;
                }

                if (result != null)
                {
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        target.Close();
                        return result.Pdu.ErrorStatus; //与SnmpException.ErrorCode值重复
                    }
                    else
                    {
                        foreach (Vb v in result.Pdu.VbList)
                        {
                            alPduOid.Add(v.Oid.ToString());
                            alValue.Add(v.Value.ToString());
                        }
                    }
                }

                target.Close();
            }
            catch (System.Exception e)
            {
                e.Data.Add("_deviceIP", _deviceIP);

                return -1;
            }

            return 0;
        }

    }
    public class PrinterCounter
    {
        private int _Black;

        /// <summary>
        /// 黑白：每张A3黑白双计，折算成Black
        /// </summary>
        public int Black
        {
            get { return _Black; }
            set { _Black = value; }
        }

        private int _Color;

        /// <summary>
        /// 彩色：每张A3黑白双计，折算成Color
        /// </summary>
        public int Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        private int _A3;

        /// <summary>
        /// A3
        /// </summary>
        public int A3
        {
            get { return _A3; }
            set { _A3 = value; }
        }

        private int _Duplex;

        /// <summary>
        /// 双面：1张A3双面计1
        /// </summary>
        public int Duplex
        {
            get { return _Duplex; }
            set { _Duplex = value; }
        }

    }
    public class XML
    {
        private XmlDocument doc = new XmlDocument();//读取XML中的OID
        private XmlElement xmlElement = null;
        private int[] counterNum = null; //保存八种纸型所对应的个数 hp samsung
        private int[] CounterValue = null;//ricoh 
        private string[] sXmlInfo = null;//black 、color、a3、duplex

        private bool bTotal = false;


        //新增
        private bool isMonitorCounter;
        public bool IsMonitorCounter
        {
            get { return isMonitorCounter; }
            set { isMonitorCounter = value; }
        }


        private Exception cException;
        public Exception CException
        {
            get { return this.cException; }
            set { this.cException = value; }
        }

        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="xmlPath"></param>
        public bool LoadXml(string xmlPath)
        {
            //Exception es = new Exception();
            //throw new Exception("LoadXml文件异常", es); 
            try
            {
                doc.Load(xmlPath);
                return true;
            }
            catch (System.Exception e)
            {
                throw new Exception("LoadXml文件异常", e);
            }
        }

        /// <summary>
        /// 获取Ricoh所有计数器的oid
        /// </summary>
        /// <param name="strDeviceModel"></param>
        /// <param name="alOid"></param>
        /// <returns></returns>
        public bool GetRicohOid(string strDeviceModel, ArrayList alOid)
        {
            string xpath = "model[text()='" + strDeviceModel + "']";

            if (!GetElement(xpath))
            {
                return false;
            }

            if (isMonitorCounter)
            {
                return GetRicohAllOid(alOid);
            }
            else
            {
                return GetRicohOid(alOid);
            }

            //return GetRicohOid(alOid);
        }
        /// <summary>
        /// 获取HP所有计数器的oid
        /// </summary>
        /// <param name="strDeviceModel"></param>
        /// <param name="alOid"></param>
        /// <returns></returns>
        public bool GetHPOid(string strDeviceModel, ArrayList alOid)
        {
            try
            {
                string xpath = "model[@name='" + strDeviceModel + "']";
                if (!GetElement(xpath))
                {
                    return false;
                }
                return GetHPOid(alOid);
            }
            catch (System.Exception e)
            {
                e.Data.Add("函数GetHPOid异常:", e.Message);
                throw e;
            }

        }

        /// <summary>
        ///  获取samsung所有计数器的oid
        /// </summary>
        /// <param name="strDeviceModel"></param>
        /// <param name="alOid"></param>
        /// <returns></returns>
        public bool GetSamsungOid(string strDeviceModel, ArrayList alOid)
        {
            string xpath = "model[@name='" + strDeviceModel + "']";
            if (!GetElement(xpath))
            {
                return false;
            }

            return GetSamsungOid(alOid);
        }

        /// <summary>
        /// 设置pdu的绑定Vb的最大数量，HP专用
        /// </summary>
        /// <returns></returns>
        public int GetHPMaxValue()
        {
            try
            {
                string maxValue = xmlElement.GetAttribute("max");
                return int.Parse(maxValue);
            }
            catch (System.Exception e)
            {
                e.Data.Add("函数GetHPMaxValue异常:", e.Message);
                throw e;
            }

        }

        //转换ricoh计数器格式
        public bool GetRicohPrinterCounter(ArrayList alValue, ref PrinterCounter printerCounter)
        {
            try
            {
                //构造计数器值
                int j = 0;
                for (int i = 0; i < sXmlInfo.Length; i++)
                {
                    if (sXmlInfo[i] != "")
                    {
                        CounterValue[i] = int.Parse((string)alValue[j]);
                        j++;
                    }
                    else
                    {
                        CounterValue[i] = 0;
                    }
                }

                printerCounter.Black = CounterValue[0];
                printerCounter.Color = CounterValue[1];
                printerCounter.A3 = CounterValue[2];
                printerCounter.Duplex = CounterValue[3];
                //新增
                if (isMonitorCounter)
                {
                    printerCounter.Black += CounterValue[4];
                    printerCounter.Color += CounterValue[5];
                }

                if (bTotal)
                {
                    printerCounter.Color = CounterValue[1] - CounterValue[0];
                }

                return true;
            }
            catch (System.Exception e)
            {
                throw new Exception("转换ricoh计数器格式异常", e);
            }

        }

        /// <summary>
        /// 转换HP计数器格式
        /// </summary>
        /// <param name="alValue"></param>
        /// <param name="printerCounter"></param>
        /// <returns></returns>
        public bool GetHpPrinterCounter(ArrayList alValue, ref PrinterCounter printerCounter)
        {
            try
            {
                int[] counter = new int[8];
                //生成数据
                int iOidNum = 0;
                for (int i = 0; i < 8; i++)
                {
                    counter[i] = 0;

                    for (int j = 0; j < counterNum[i]; j++)
                    {
                        counter[i] += int.Parse((string)alValue[iOidNum]);
                        iOidNum++;
                    }
                }

                //构造计数器值
                //A4黑白单面、A4黑白双面、A3黑白单面、A3黑白双面
                //A4彩色单面、A4彩色双面、A3彩色单面、A3彩色双面
                printerCounter.Black = counter[0] + counter[1] * 2 + counter[2] * 2 + counter[3] * 4;//黑白
                printerCounter.Color = counter[4] + counter[5] * 2 + counter[6] * 2 + counter[7] * 4;//彩色
                printerCounter.A3 = counter[2] + counter[3] * 2 + counter[6] + counter[7] * 2;//A3
                printerCounter.Duplex = counter[1] + 2 * counter[3] + counter[5] + 2 * counter[7];
                //printerCounter.Duplex = counter[1] + counter[3] + counter[5] + counter[7];//双面  之前1张A3双面计1
                return true;

            }
            catch (System.Exception e)
            {
                throw new Exception("转换HP计数器格式异常", e);
            }
        }

        /// <summary>
        /// 转换samsung计数器格式
        /// </summary>
        /// <param name="alValue"></param>
        /// <param name="printerCounter"></param>
        /// <returns></returns>
        public bool GetSamsungPrinterCounter(ArrayList alValue, ref PrinterCounter printerCounter)
        {
            try
            {
                return GetHpPrinterCounter(alValue, ref printerCounter);
            }
            catch (System.Exception e)
            {
                throw new Exception("转换Samsung计数器格式异常", e);
            }

        }

        private bool GetElement(string xpath)
        {
            //Exception ee = new Exception();
            //throw new Exception("ceshi", ee);
            try
            {
                //从xml文件中得到计数器节点
                XmlNode root = doc.DocumentElement;
                xmlElement = (XmlElement)root.SelectSingleNode(xpath);
                return true;
            }
            catch (System.Exception e)
            {
                e.Data.Add("函数GetElement异常:", e.Message);
                throw e;
            }
        }

        private bool GetRicohOid(ArrayList alOid)
        {
            try
            {
                CounterValue = new int[4];//ricoh 
                sXmlInfo = new string[4];//black 、color、a3、duplex

                //string[] sXmlInfo = new string[4];//black 、color、a3、duplex



                sXmlInfo[0] = xmlElement.GetAttribute("b");
                sXmlInfo[1] = xmlElement.GetAttribute("c");
                sXmlInfo[2] = xmlElement.GetAttribute("a");
                sXmlInfo[3] = xmlElement.GetAttribute("d");


                string total = xmlElement.GetAttribute("t");
                if (total != string.Empty)
                {
                    sXmlInfo[1] = total;
                    bTotal = true;
                }


                foreach (string s in sXmlInfo)
                {
                    if (s != "")
                    {
                        alOid.Add("1.3.6.1.4.1.367.3.2.1.2.19.5.1.9." + s);
                    }
                }

                return true;
            }
            catch (System.Exception e)
            {
                throw new Exception("GetRicohOid异常", e);
            }
        }

        private bool GetHPOid(ArrayList alOid)
        {
            try
            {
                counterNum = new int[8];
                XmlNodeList nodelist = xmlElement.ChildNodes;
                string[] sBeginOid = new string[] {"1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.1.1.1.","1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.1.1.3.",  //A4
                                               "1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.1.1.1.","1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.1.1.3.",  //A3,区别主要是后面的值不同
                                               "1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.3.1.1.","1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.3.1.3.",
                                               "1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.3.1.1.","1.3.6.1.4.1.11.2.3.9.4.2.1.1.16.3.1.3."
                                              };

                //（A4黑白单面、A4黑白双面、A3黑白单面、A3黑白双面、A4彩色单面、A4彩色双面、A3彩色单面、A3彩色双面) 
                for (int i = 0; i < 8; i++)
                {
                    XmlElement xe1 = (XmlElement)nodelist.Item(i);

                    if (xe1.InnerText != "") //为空时标识没有这种纸型
                    {
                        string[] sXmlInfo = xe1.InnerText.Split('|');
                        counterNum[i] = sXmlInfo.Length;
                        for (int j = 0; j < sXmlInfo.Length; j++)
                        {
                            alOid.Add(sBeginOid[i] + sXmlInfo[j] + ".0");
                        }
                    }
                    else
                    {
                        counterNum[i] = 0;
                    }
                }

                return true;
            }
            catch (System.Exception e)
            {
                e.Data.Add("重载函数GetHPOid异常:", e.Message);
                throw e;
            }
        }

        private bool GetSamsungOid(ArrayList alOid)
        {
            try
            {
                counterNum = new int[8];
                XmlNodeList nodelist = xmlElement.ChildNodes;

                for (int i = 0; i < 8; i++)
                {
                    XmlElement xe1 = (XmlElement)nodelist.Item(i);
                    if (xe1.InnerText != "") //为空时标识没有这种纸型
                    {
                        string[] sXmlInfo = xe1.InnerText.Split('|');
                        counterNum[i] = sXmlInfo.Length;
                        for (int j = 0; j < sXmlInfo.Length; j++)
                        {
                            alOid.Add("1.3.6.1.4.1.236.11.5.11.53.11.2.1.7.1." + sXmlInfo[j]);
                        }
                    }
                    else
                    {
                        counterNum[i] = 0;
                    }
                }

                return true;
            }
            catch (System.Exception e)
            {
                throw new Exception("GetSamsungOid异常", e);
            }
        }

        private bool GetRicohAllOid(ArrayList alOid)
        {
            try
            {
                CounterValue = new int[6];//ricoh 
                sXmlInfo = new string[6];//black 、color、a3、duplex、copy_black、copy_color

                sXmlInfo[0] = xmlElement.GetAttribute("b");
                sXmlInfo[1] = xmlElement.GetAttribute("c");
                sXmlInfo[2] = xmlElement.GetAttribute("a");
                sXmlInfo[3] = xmlElement.GetAttribute("d");
                sXmlInfo[4] = xmlElement.GetAttribute("cb");
                sXmlInfo[5] = xmlElement.GetAttribute("cc");


                string total = xmlElement.GetAttribute("t");
                if (total != string.Empty)
                {
                    sXmlInfo[1] = total;
                    bTotal = true;
                }


                foreach (string s in sXmlInfo)
                {
                    if (s != "")
                    {
                        alOid.Add("1.3.6.1.4.1.367.3.2.1.2.19.5.1.9." + s);
                    }
                }

                return true;
            }
            catch (System.Exception e)
            {
                throw new Exception("GetRicohOid异常", e);
            }
        }




    }
}
