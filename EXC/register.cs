using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace EXC
{
    class registerNumber
    {
        // 取得设备硬盘的卷标号
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        public static string GetDiskID()
        {
            try
            {
                //获取硬盘ID     
                String HDid = " ";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        //获得CPU的序列号
        public static string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        public static string GetMacAddress()
        {
            try
            {
                // 获取网卡硬件地址     
                string mac = " ";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow ";
            }
            finally
            {
            }
        }

        //生成机器码
        public static string getMNum()
        {
            string strNum = getCpu() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号
            string strMNum = strNum.Substring(0, 24);//从生成的字符串中取出前24个字符做为机器码
            return strMNum;
        }

        public static int[] intCode = new int[127];//存储密钥
        public static int[] intNumber = new int[25];//存机器码的Ascii值
        public static char[] Charcode = new char[25];//存储机器码字
        public static void setIntCode()//给数组赋值小于10的数
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        //生成注册码  
        public static string getRNum()
        {
            string nm = getMNum();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(nm));
            return Encoding.Default.GetString(result);

            //return nm;
            /*
            setIntCode();//初始化127位数组
            for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
            {
                Charcode[i] = Convert.ToChar(getMNum().Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储注册码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }
                }
            }
            return strAsciiName;
            */
        }
    }


    public class LicenseChecker
    {


        /// <summary>
        /// 版本识别码（来自授权文件制作工具，这个很重要，比如和版本对应）
        /// </summary>
        public string VersionNo{ get{ return "4a57107a"; }}

        /// <summary>
        /// 机器识别码
        /// </summary>
        /// <returns></returns>
        public string GetMachineCode()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            String strHardDiskID = null;//存储磁盘序列号
            foreach (ManagementObject mo in searcher.Get())
            {
                strHardDiskID = mo["SerialNumber"].ToString().Trim();//记录获得的磁盘序列号
                break;
            }

            return MD5Helper.GetMD5HashString(strHardDiskID, 8);
        }


        /// <summary>
        /// 校验授权文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Check(string content)
        {
            try
            {
                string message = "授权文件格式无法识别";

                string[] items = content.Split(new string[] { "+++++=====+++++" }, StringSplitOptions.RemoveEmptyEntries);

                if (items.Length != 3)
                {
                    message = "授权文件格式无法识别";
                    return false;
                }

                string json = items[0];
                string sign = items[1];
                string publicKeyXml = items[2];

                var fileVersionNo = MD5Helper.GetMD5HashString(publicKeyXml).Substring(0, 8);
                //if (fileVersionNo.Equals(this.VersionNo) == false)
                //{
                //    message = "版本识别号有误";
                //    return false;
                //}

                var isVerify = Verify(json, sign, publicKeyXml);

                if (isVerify == false)
                {
                    message = "授权文件格式校验未通过";
                    return false;
                }

                AuthorizeFile af = AuthorizeFile.FromJson(json);

                if (af.Check(this.GetMachineCode()) == false)
                {
                    message = "应用授权失败，当前授权类型为：" + af.AuthorizeType;
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }


        }

        bool Verify(byte[] data, byte[] Signature, string PublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //导入公钥，准备验证签名
            rsa.FromXmlString(PublicKey);
            //返回数据验证结果
            return rsa.VerifyData(data, "MD5", Signature);
        }

        public bool Verify(String Text, string SignatureBase64, string PublicKey)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(Text);
            byte[] Signature = Convert.FromBase64String(SignatureBase64);

            return Verify(data, Signature, PublicKey);

        }


    }

    /// <summary>
    /// 授权文件数据格式
    /// </summary>
    class AuthorizeFile
    {

        public string Version { get; set; }

        public string AuthorizeType { get; set; }

        public string AuthorizeContent { get; set; }


        public DateTime SignDate { get; set; }

        public bool Check(string machineCode)
        {
            string[] splits = new string[] { ",", "\r\n" };
            string[] items = this.AuthorizeContent.Split(splits, StringSplitOptions.RemoveEmptyEntries);


            foreach (string item in items)
            {
                if (item.Equals(machineCode))
                    return true;
            }

            return false;
        }

        public string ToJson()
        {
            this.SignDate = DateTime.Now;

            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static AuthorizeFile FromJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizeFile>(json);
        }


    }


    public class MD5Helper
    {
        private static MD5 md5 = MD5.Create();

        //使用utf8编码将字符串散列
        public static string GetMD5HashString(string sourceStr)
        {
            return GetMD5HashString(Encoding.UTF8, sourceStr);
        }
        //使用utf8编码将字符串散列
        public static string GetMD5HashString(string sourceStr, int length)
        {
            string code = GetMD5HashString(Encoding.UTF8, sourceStr);

            if (code.Length > length)
                code = code.Substring(0, length);

            code = code.Replace("o", "a");
            code = code.Replace("O", "a");
            code = code.Replace("0", "a");

            return code;
        }
        //使用指定编码将字符串散列
        public static string GetMD5HashString(Encoding encode, string sourceStr)
        {
            StringBuilder sb = new StringBuilder();

            byte[] source = md5.ComputeHash(encode.GetBytes(sourceStr));
            for (int i = 0; i < source.Length; i++)
            {
                sb.Append(source[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }

}
