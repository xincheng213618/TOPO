using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtil
{
    public class Info
    {
        public static string NetVersion()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    int releaseKey = (int)ndpKey.GetValue("Release");
                    if (releaseKey >= 528040)
                        return "4.8 or later";
                    if (releaseKey >= 461808)
                        return "4.7.2";
                    if (releaseKey >= 461308)
                        return "4.7.1";
                    if (releaseKey >= 460798)
                        return "4.7";
                    if (releaseKey >= 394802)
                        return "4.6.2";
                    if (releaseKey >= 394254)
                        return "4.6.1";
                    if (releaseKey >= 393295)
                        return "4.6";
                    if (releaseKey >= 379893)
                        return "4.5.2";
                    if (releaseKey >= 378675)
                        return "4.5.1";
                    if (releaseKey >= 378389)
                        return "4.5";
                    // This code should never execute. A non-null release key should mean
                    // that 4.5 or later is installed.
                    return "No 4.5 or later version detected";
                }
                else
                {
                    return "Null";
                }
            }
        }


        public static string[] IPAdress()
        {
            IPAddress[] List = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            string[] IP = new string[List.Length];
            for (int i = 0; i < List.Length; i++)
            {
                IP[i] = List[i].ToString();
            }

            return IP;
        }
        public static string[] MACAdress()
        {
            string[] mac = new string[IPAdress().Length];
            int i = 0;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection Moc = mc.GetInstances();
            foreach (ManagementObject mo in Moc)
                if (mo["IPEnabled"].ToString() == "True")
                {
                    mac[i] = mo["MacAddress"].ToString().Trim().Replace(':', '-');
                    i++;
                }
                else
                {
                    mac[i] = "aa:bb:cc:dd";
                }
            return mac;

        }
    }
}
