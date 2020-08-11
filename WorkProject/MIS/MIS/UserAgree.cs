using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS
{
    class UserAgree
    {

        SerialDrivers com = null;
        

        #region 设置单例
        private static UserAgree instance = null;
        private static readonly object padlock = new object();

        UserAgree()
        {
            com = new SerialDrivers();
        }
        /// <summary>
        /// 获取单例对象
        /// </summary>
        public static UserAgree Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserAgree();
                    }
                    return instance;
                }
            }
        }
        #endregion


        /// <summary>
        /// 获取硬件连接状态
        /// </summary>
        public bool GetStatePort()
        {
            return com.StatePort;
        }
        /// <summary>
        /// 获取NFC连接状态
        /// </summary>
        public bool GetStateNFC()
        {
            return com.StateNFC;
        }
        /// <summary>
        /// 获取当前连接卡号
        /// </summary>
        public string GetNowCard()
        {
            return com.NowCard;
        }

        
    }
}
