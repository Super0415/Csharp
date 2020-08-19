using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class UserAgree
    {
        #region 数据结构
        private USBAgree usbAgree = null;

        /// <summary>
        /// USB 连接状态
        /// </summary>
        public bool USBConnectState { get; set; } = false;

        /// <summary>
        /// 硬件连接状态
        /// </summary>
        public bool HardConnectState { get; set; } = false;

        /// <summary>
        /// NFC连接状态
        /// </summary>
        public bool NFCConnectState { get; set; } = false;
        #endregion

        #region 单例
        /// <summary>
        /// 单例对象
        /// </summary>
        private static UserAgree userAgree = null;

        UserAgree()
        {
            usbAgree = new USBAgree();
            USBConnectState = usbAgree.OpenUSBkeySuccess();
        }
        public static UserAgree Instance
        {
            get
            {
                if (userAgree == null)
                {
                    userAgree = new UserAgree();
                }
                return userAgree;
            }
        }
        #endregion

        #region USB接口
        /// <summary>
        /// 手动打开USBkey
        /// </summary>
        public void HandOpenUSBkey()
        {
            USBConnectState = usbAgree.OpenUSBkeySuccess();
        }
        /// <summary>
        /// 获取USB中的总记录数
        /// </summary>
        /// <returns></returns>
        public int GetUSBTipSum()
        {
            int sum = -1;
            if (usbAgree.GetRecordTipSum(out sum) == true)
            {
                if (sum <= 320 && sum >= 0)
                {
                    return sum;
                }
                else sum = -1;
            }
            else sum = -1;
            return sum;
        }

        /// <summary>
        /// 清零USB中的总记录数
        /// </summary>
        /// <returns></returns>
        public bool ClearUSBTipSum()
        {
            return usbAgree.LetRecordTipSumClear();
        }

        public bool SetUSBTipSum(int sum)
        {
            return usbAgree.SetRecordTipSum(sum);
        }

        public bool SaveUSBTip(MyPublic.TipClass tip)
        {
            int addr = GetUSBTipSum();
            if (addr < 0)
            {
                return false;
            }
            if (!SetUSBTipSum(addr+1))
            {
                return false;
            }

            string data = tip.Name + "\n"
                        + tip.Sex + "\n"
                        + tip.Age + "\n"
                        + tip.Branch + "\n"
                        + tip.Position + "\n"
                        + tip.Level + "\n"
                        + tip.JopNum + "\n"
                        + tip.EquipNum + "\n"
                        + tip.DataT + "\n";

            return usbAgree.SaveTip(addr+1,data);
            
        }

        public bool GetPointTip(int addr,out MyPublic.TipClass tip)
        {
            tip = new MyPublic.TipClass();
            try
            {  
                string[] temp = usbAgree.GetTip(addr).Trim().Split('\n');
                tip.Name = temp[0];
                tip.Sex = temp[1];
                tip.Age = temp[2];
                tip.Branch = temp[3];
                tip.Position = temp[4];
                tip.Level = temp[5];
                tip.JopNum = temp[6];
                tip.EquipNum = temp[7];
                tip.DataT = temp[8];
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
