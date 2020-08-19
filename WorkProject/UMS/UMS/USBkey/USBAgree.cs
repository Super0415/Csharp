using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class USBAgree
    {
        /// <summary>
        /// USBkey驱动
        /// </summary>
       private USBDrive usbDrive = null;

        public USBAgree()
        {
            usbDrive = new USBDrive();
        }

        /// <summary>
        /// 是否打开USB
        /// </summary>
        /// <returns></returns>
        public bool OpenUSBkeySuccess()
        {
            return usbDrive.AutoOpenUSBkey();
        }

        /// <summary>
        /// 获取记录总数量
        /// </summary>
        /// <returns></returns>
        public bool GetRecordTipSum(out int sum)
        {
            sum = 0;
            try
            {
                string temp = usbDrive.GetPointCluster(0).Split('\0')[0];
                sum = Convert.ToInt32(temp);
                if (sum >= 0 && sum <= 320)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 将记录总数量清零
        /// </summary>
        /// <returns></returns>
        public bool LetRecordTipSumClear()
        {
               return usbDrive.SetPointCluster(0,"0");
        }
        /// <summary>
        /// 设置总记录数
        /// </summary>
        /// <returns></returns>
        public bool SetRecordTipSum(int sum)
        {
            return usbDrive.SetPointCluster(0, sum.ToString());
        }


        /// <summary>
        /// 保存指定地址的记录
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SaveTip(int addr,string data)
        {
            return usbDrive.SetPointCluster(addr,data);
        }

        public string GetTip(int addr)
        {
            return usbDrive.GetPointCluster(addr);
        }

    }
}
