using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace EXCResources
{
    /// <summary>
    /// 许可证检测
    /// </summaryd>

    public class License
    {
        private static readonly string publicKeyXml = "<RSAKeyValue><Modulus>5sf/agoe+/hryIfvt7v6o9aNldWSkUoPkW6se8VbEo7B4JBT0vIUQqku635RU+0vhaF/IJ7TQw6pYerHacA83XYBy90KEN4twOBs1Gy3XfEBcjYheQO919Hif1gENzqzQEg47G36VdmWzmhjreq2YQQQN+p/ezIbYtrPXGNU4fE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static readonly string privateKeyXml = "<RSAKeyValue><Modulus>5sf/agoe+/hryIfvt7v6o9aNldWSkUoPkW6se8VbEo7B4JBT0vIUQqku635RU+0vhaF/IJ7TQw6pYerHacA83XYBy90KEN4twOBs1Gy3XfEBcjYheQO919Hif1gENzqzQEg47G36VdmWzmhjreq2YQQQN+p/ezIbYtrPXGNU4fE=</Modulus><Exponent>AQAB</Exponent><P>/OfgYc6H7sSiFUrwkTVtQEyuSm309+Whwuvuul/3zLkNJlvorGC2D5ksTz3Q0XFehHWgWNc0jQ3MRyKp2EHxgw==</P><Q>6ZrTQbe25FVr92pxAlBeO1iONdbLRM+/VmuwrZVgeHvu++8ChAidQT13rcVfqvLDuGq5/q2bgQgmraqdgRNIew==</Q><DP>0sEQ1bDcyncGcyQOMZQKRSkhnVjgaaztDpi6Sooq4GndsXep/+xgC8Ojjy1+VOtazpuPUjmUy28SKr2SOGtLrQ==</DP><DQ>b7mMsDGdVzdDm+Fciy7E4r1HxpgkP5TcfgijR2HZ8cXUVsnI+jzkeP9c7c8oIipZUSo6KoP9i4jKduTSz5jZYQ==</DQ><InverseQ>2kXWXpMpHplGwG/eHR17tVNyfaxjl2Hu2QWnlg5Jf/vLDMcA9MspGS5mS5uCNTTPh34T9PEtmCdA5L5i8kakwg==</InverseQ><D>EmVOzr0PyzX6IXn0ecjaKcUodBEaJcqpgwY3aYZJxCjs+2GFzQLO6qFhxBPFl9MIPrao04jVfjrk9ZEpZByWvUmq79tlzpBjeZW2wcjeUrZYK0/b0D7NRelf6InSJaOb9QKw/hhSPsl3x+nXPyhUFfz6q8bThGDSriC/eb3aSyE=</D></RSAKeyValue>";

        public static string Create(string MachineCode)
        {
            return Sign(MachineCode, privateKeyXml);
        }

        public static bool Check(string lisense)
        {
            Global.configData.SN = lisense;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //导入公钥，准备验证签名
            rsa.FromXmlString(publicKeyXml);
            return lisense.Length==172 && rsa.VerifyData(Encoding.UTF8.GetBytes(GetMachineCode()), "MD5", Convert.FromBase64String(lisense));
        }

        public static string Sign(string Text, string PrivateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(PrivateKey);

            return Convert.ToBase64String(rsa.SignData(Encoding.UTF8.GetBytes(Text), "MD5"));
        }

        public static string GetMachineCode()
        {
            byte[] code = Encoding.UTF8.GetBytes(Environment.MachineName);
            string Reg="";
            
            foreach (byte a in code)
            {
                Reg += a.ToString("x2");
            }
            return Reg;
        }
    }
   

    class VersionSettingsManager
    {

        /// <summary>
        /// 用于存储证书的基础路径
        /// </summary>
        public string StorePath
        {
            get
            {
                string temp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Store");

                if (Directory.Exists(temp) == false)
                    Directory.CreateDirectory(temp);

                return temp;
            }
        }

        private VersionSettingsManager()
        { }

        private static VersionSettingsManager _instance = null;
        public static VersionSettingsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new VersionSettingsManager();
                }
                return _instance;
            }
        }
        public bool Delete(string Name)
        {
            var dir = Path.Combine(this.StorePath, Name);

            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(string Name)
        {
            var dir = Path.Combine(this.StorePath, Name);

            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                using (StreamWriter writer = new StreamWriter(Path.Combine(dir, "私钥[" + Name + "].dat")))  //这个文件要保密...
                {
                    writer.WriteLine(rsa.ToXmlString(true));
                }

                using (StreamWriter writer = new StreamWriter(Path.Combine(dir, "公钥[" + Name + "].dat")))
                {
                    writer.WriteLine(rsa.ToXmlString(false));
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public string[] List()
        {
            List<string> result = new List<string>();

            var items = Directory.GetDirectories(this.StorePath);
            foreach (string item in items)
            {
                DirectoryInfo di = new DirectoryInfo(item);
                result.Add(di.Name);
            }

            return result.ToArray();
        }

        /// <summary>
        /// 读取公钥信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string ReadPublicKey(string Name)
        {
            var dir = Path.Combine(this.StorePath, Name);

            var path = Path.Combine(dir, "公钥[" + Name + "].dat");

            return File.ReadAllText(path);
        }

        /// <summary>
        /// 读取私钥信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string ReadPrivateKey(string Name)
        {
            var dir = Path.Combine(this.StorePath, Name);

            var path = Path.Combine(dir, "私钥[" + Name + "].dat");

            return File.ReadAllText(path);
        }

    }
}
