using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDLL
{
    //Designed For 身份证读卡 2020.6.30 Mr.Xin 
    public static class IDcard
    {
        private static string DLLpath = @"BaseDLL\IDCard\SynIDCardAPI.dll";
        public static int iPort;

        /// <summary>
        ///身份证初始化
        /// </summary>
        /// <param name="Prompt"> </param>
        /// <returns></returns>
        public static int IDcardSet(bool Prompt = false)
        {
            if (!File.Exists(DLLpath))
            {
                return 0;
            }
            int iPort;
            iPort = ReadCardAPI.FindUSBDriverDevice();
            //2020.6.10 重写这个方法，将其他读卡也写进去
            if (iPort == 0)
            {
                iPort = ReadCardAPI.FindUSBHIDDevice();
                if (iPort == 0)
                {
                    if (Prompt)
                        return 0;
                }
            }

            byte[] cPath = new byte[255];
            ReadCardAPI.Syn_SetPhotoPath(1, ref cPath);
            ReadCardAPI.Syn_SetPhotoName(3);
            ReadCardAPI.Syn_SetNationType(1);
            ReadCardAPI.Syn_SetSexType(1);
            ReadCardAPI.Syn_SetBornType(1);
            ReadCardAPI.Syn_SetUserLifeBType(1);
            ReadCardAPI.Syn_SetUserLifeEType(1, 0);
            return iPort;
        }

        //读取身份证的部分
        public static int IDcardRead(int iPort, ref IDCardData IDCardData)
        {
            int nRet;
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            //byte[] ctmp = new byte[255];
            byte[] szPathP = new byte[260];

            //做一下反转，让失败可以准确识别
            nRet = -ReadCardAPI.Syn_OpenPort(iPort);

            if (nRet == 0)
            {
                //寻卡是否成功 //一般没问题
                nRet = ReadCardAPI.Syn_StartFindIDCard(iPort, ref pucIIN, 0);
                if (nRet != 0)
                    return nRet;
                //选卡是否成功 //一般没有问题
                nRet = ReadCardAPI.Syn_SelectIDCard(iPort, ref pucSN, 0);
                if (nRet != 0)
                    return nRet;

                nRet = ReadCardAPI.Syn_ReadFPMsg(iPort, 0, ref IDCardData, szPathP);
                IDCardData.szPath = Encoding.Default.GetString(szPathP).Replace("\0", string.Empty);

            }
            return nRet;
        }
        public static bool DeleteIDcardImages(IDCardData idcardData)
        {
            try
            {
                File.Delete(idcardData.PhotoFileName);
                File.Delete(idcardData.szPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static Dictionary<int, object> Code = new Dictionary<int, object>()
        {
            {0,"读卡成功" },
            {1,"读取身份证信息成功，指纹读取成功" },
            {-1,"失败/不合法" },
            {2,"超时，设置不成功" },
            {5,"无法获得该SAM的波特率，该SAM串口不可用" },
            {33,"uiCurrBaud 、uiSetBaud输入参数数值错误" },
            {96,"自检失败，不能接收命令，其他，命令失败" },
            {128,"寻卡失败" },
            {129,"选卡失败" },
        };

    }
}
