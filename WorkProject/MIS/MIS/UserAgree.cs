using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MIS
{
    class UserAgree
    {

        SerialDrivers com = null;
        /// <summary>
        /// 用户数据
        /// </summary>
        MyData mydata = null;
        /// <summary>
        /// 数据库字典信息
        /// </summary>
        Dictionary<string, MyData.DetailInfo> dicdata = null;

        #region 设置单例
        private static UserAgree instance = null;
        private static readonly object padlock = new object();

        UserAgree()
        {
            com = new SerialDrivers();
            mydata = MyData.Instance;
            dicdata = MyData.GetDictionary;
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
        public string GetNowNFCCard()
        {
            return com.NowCard;
        }

        /// <summary>
        /// NFC发送写入信息
        /// </summary>
        /// <returns></returns>
        public bool SendWriteNFCInfo(string cardId)
        {
            if (!dicdata.ContainsKey(cardId))
            {
                MessageBox.Show("数据库中无此数据");
                return false;
            }

            SendWriteNFCCmd((int)MyData.Item.name,cardId);
            SendWriteNFCCmd((int)MyData.Item.sex, cardId);
            SendWriteNFCCmd((int)MyData.Item.age, cardId);
            SendWriteNFCCmd((int)MyData.Item.branch, cardId);
            SendWriteNFCCmd((int)MyData.Item.position, cardId);
            SendWriteNFCCmd((int)MyData.Item.level, cardId);
            SendWriteNFCCmd((int)MyData.Item.jobNum, cardId);
            SendWriteNFCCmd((int)MyData.Item.equipNum, cardId);
            SendWriteNFCCmd((int)MyData.Item.date, cardId);
            dicdata[cardId].actionstate = MyData.ActionStr.写卡完成;

            return true;
        }

        /// <summary>
        /// 发送指定写入指令
        /// </summary>
        /// <param name="select"></param>
        private void SendWriteNFCCmd(int select,string cardId)
        {
            Thread.Sleep(50);
            MyData.DetailInfo buff = dicdata[cardId];
            string str = buff.userInfoW[(MyData.Item)select];
            byte[] temp = System.Text.Encoding.Default.GetBytes(str);
            com.SendWriteCmd(select, temp);
            mydata.SendWriteAdd(cardId);
            //buff.cmdState[select] = false;
            buff.actionstate = MyData.ActionStr.写卡中;
            mydata.ReflashComState(cardId, select,false);
            
        }

        /// <summary>
        /// NFC发送读取指令
        /// </summary>
        /// <returns></returns>
        public bool SendReadNFCInfo(string cardId)
        {
            if (!dicdata.ContainsKey(cardId))
            {
                MessageBox.Show("数据库中无此数据");
                return false;
            }
            SendReadNFCCmd((int)MyData.Item.name, cardId);
            SendReadNFCCmd((int)MyData.Item.sex, cardId);
            SendReadNFCCmd((int)MyData.Item.age, cardId);
            SendReadNFCCmd((int)MyData.Item.branch, cardId);
            SendReadNFCCmd((int)MyData.Item.position, cardId);
            SendReadNFCCmd((int)MyData.Item.level, cardId);
            SendReadNFCCmd((int)MyData.Item.jobNum, cardId);
            SendReadNFCCmd((int)MyData.Item.equipNum, cardId);
            SendReadNFCCmd((int)MyData.Item.date, cardId);
            dicdata[cardId].actionstate = MyData.ActionStr.读卡完成;

            return true;
        }

        /// <summary>
        /// 发送指定读取指令
        /// </summary>
        /// <param name="select"></param>
        private void SendReadNFCCmd(int select, string cardId)
        {
            Thread.Sleep(50);
            MyData.DetailInfo buff = dicdata[cardId];
            //string str = buff.userInfoR[(MyData.Item)select];
            //byte[] temp = System.Text.Encoding.UTF8.GetBytes(str);
            com.SendReadCmd(select);
            //buff.cmdState[select] = false;
            buff.actionstate = MyData.ActionStr.读卡中;
            mydata.ReflashComState(cardId, select, false);
            
        }




    }
}
