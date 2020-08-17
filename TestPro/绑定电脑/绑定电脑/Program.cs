using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace 绑定电脑
{
    class Program
    {
        static string GetMAC()
        {
            string mac = "本机的MAC地址:";
            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"])
                    {
                        string[] tmpMac = mo["MacAddress"].ToString().Split(':');
                        for (int i = 0; i < tmpMac.Length; i++)
                        {
                            mac += tmpMac[i];
                        }
                    }
                }
            }
            return mac + "\r\n";
        }

        static string GetCPUID()
        {
            string CPUID = "本机的CPU序列号:";
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject item in moc)
                {
                    CPUID += item["ProcessorId"].ToString().Trim();
                }
            }
            return CPUID + "\r\n";
        }

        static string GetHardID()
        {
            string HardID = "本机的硬盘序列号:";
            using (ManagementClass mc = new ManagementClass("Win32_DiskDrive"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject item in moc)
                {
                    HardID += item["Model"].ToString().Trim();
                }
            }
            return HardID + "\r\n";
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GetMAC());
            Console.WriteLine(GetCPUID());
            Console.WriteLine(GetHardID());

            Console.ReadKey();

        }
    }
}
