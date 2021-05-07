using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BaseUtil
{
    /// <summary>
    /// 检测类
    /// </summary>
    public class Check
    {
        /// <summary>
        /// 字符串是否为手机号码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^1(3[0-9]|5[0-9]|7[6-8]|8[0-9])[0-9]{8}$");
        }
        /// <summary>
        /// 身份证号是否正确
        /// </summary>
        /// <param name="IDcardNo"></param>
        /// <returns></returns>
        public static bool CheckIDCardNo(string IDcardNo)
        {
            bool result;
            try
            {
                if (IDcardNo.Length != 18)
                {
                    return false;
                }

                long n = 0;
                if (long.TryParse(IDcardNo.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(IDcardNo.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证  
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(IDcardNo.Remove(2)) == -1)
                {
                    return false;//省份验证  
                }

                string birth = IDcardNo.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证  
                }

                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = IDcardNo.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != IDcardNo.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证  
                }
                return true;//符合GB11643-1999标准 

            }
            catch 
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 文件是否未PDF
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static bool CheckPDF(string FilePath)
        {
            var pdfString = "%PDF-";
            var pdfBytes = Encoding.ASCII.GetBytes(pdfString);
            var len = pdfBytes.Length;
            var buf = new byte[len];
            var remaining = len;
            var pos = 0;
            using (var f = File.OpenRead(FilePath))
            {
                while (remaining > 0)
                {
                    var amtRead = f.Read(buf, pos, remaining);
                    if (amtRead == 0) return false;
                    remaining -= amtRead;
                    pos += amtRead;
                }
            }
            return pdfBytes.SequenceEqual(buf);
        }


        /// <summary>
        /// 检测是否未纯数字
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool CheckIsHasZH(string inputData)
        {
            Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }
    }
}
