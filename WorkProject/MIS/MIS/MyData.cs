using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MIS
{
    class MyData
    {
        public enum ProductType
        {
            USBkey,
            NFC,
        }

        public enum Item
        {
            /// <summary>
            /// 通讯类型
            /// </summary>
            comType = 0,
            /// <summary>
            /// 员工姓名
            /// </summary>
            name,
            /// <summary>
            /// 性别
            /// </summary>
            sex,
            /// <summary>
            /// 年龄
            /// </summary>
            age,
            /// <summary>
            /// 部门
            /// </summary>
            branch,
            /// <summary>
            /// 职位
            /// </summary>
            position,
            /// <summary>
            /// 权限
            /// </summary>
            level,
            /// <summary>
            /// 工号
            /// </summary>
            jobNum,
            /// <summary>
            /// 登记日期
            /// </summary>
            date,
            /// <summary>
            /// 设备类型
            /// </summary>
            equipType,
            /// <summary>
            /// 设备编号
            /// </summary>
            equipNum,

            END,
            CMDNUM = 9,
        }

        public enum ActionStr
        {
            无卡,

            写卡中,
            写卡完成,

            写卡成功,
            写卡失败,

            读卡中,
            读卡完成,

            读卡成功,
            读卡失败,

            非法操作,
            初始化失败,

        }


        /// <summary>
        /// 信息数据结构
        /// </summary>
        public class DetailInfo
        {
            /// <summary>
            /// 通讯状态
            /// </summary>
            public string checkstate = string.Empty;

            /// <summary>
            /// 动作状态
            /// </summary>
            public ActionStr actionstate = ActionStr.无卡;

            /// <summary>
            /// 读取用户信息
            /// </summary>
            public Dictionary<Item, string> userInfoR = new Dictionary<Item, string>();
            /// <summary>
            /// 写入用户信息
            /// </summary>
            public Dictionary<Item, string> userInfoW = new Dictionary<Item, string>();

            /// <summary>
            /// 单条指令通讯状态
            /// </summary>
            public BitArray cmdState = new BitArray((int)Item.END);

            /// <summary>
            /// 写成功计数
            /// </summary>
            public int WriteOKCount = 0;
        }


        /// <summary>
        /// string - 卡号 DetailInfo - 卡内信息
        /// </summary>
        private static Dictionary<string, DetailInfo> info = null;
        public static Dictionary<string, DetailInfo> GetDictionary
        {
            get
            {
                return info;
            }
        }


        #region 设置单例
        private static MyData instance = null;
        private static readonly object padlock = new object();

        MyData()
        {
            info = new Dictionary<string, DetailInfo>();
        }
        /// <summary>
        /// 获取单例对象
        /// </summary>
        public static MyData Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MyData();
                    }
                    return instance;
                }
            }
        }
        #endregion

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key"></param>
        public void NFCAdd(string key)
        {
            DetailInfo d = new DetailInfo();
            d.userInfoR[Item.comType] = ProductType.NFC.ToString();
            d.userInfoR[Item.equipType] = ProductType.NFC.ToString();
            d.userInfoR[Item.name] = "";
            d.userInfoR[Item.sex] = "";
            d.userInfoR[Item.age] = "";
            d.userInfoR[Item.branch] = "";
            d.userInfoR[Item.position] = "";
            d.userInfoR[Item.level] = "";
            d.userInfoR[Item.jobNum] = "";
            d.userInfoR[Item.date] = "";
            d.userInfoR[Item.equipNum] = "";

            lock (padlock)
            {
                info.Add(key, d);
            }
        }

        ///// <summary>
        ///// 更新卡记录的校验状态
        ///// </summary>
        ///// <param name="key"></param>
        //public void ReflashCardState(string key,string state)
        //{            
        //    lock (padlock)
        //    {
        //        if (IsRepeat(key))
        //        {
        //            info[key].checkstate = state;
        //        }
        //    }
        //}

        /// <summary>
        /// 更新数据库单条通讯指令状态
        /// </summary>
        /// <param name="key"></param>
        public void ReflashComState(string key, int cmdNum, bool state)
        {
            lock (padlock)
            {
                if (IsRepeat(key))
                {
                    info[key].cmdState[cmdNum] = state;

                    foreach (bool item in info[key].cmdState)
                    {
                        if (item == true)
                        {
                            info[key].actionstate = ActionStr.读卡完成;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 发送一条写指令，计数自增1
        /// </summary>
        /// <param name="key"></param>
        public void SendWriteAdd(string key)
        {
            lock (padlock)
            {
                if (IsRepeat(key))
                {
                    info[key].WriteOKCount += 1;
                }
            }
        }

        /// <summary>
        /// 接收到一条写指令，计数自减1
        /// </summary>
        /// <param name="key"></param>
        public void ReceWriteDel(string key)
        {
            lock (padlock)
            {
                if (IsRepeat(key))
                {
                    info[key].WriteOKCount -= 1;
                    if (info[key].WriteOKCount == 0)
                    {
                        info[key].actionstate = ActionStr.写卡成功;
                    }
                }
            }
        }



        /// <summary>
        /// 更新卡记录的通讯动作状态
        /// </summary>
        /// <param name="key"></param>
        public void ReflashCardActionState(string key, MyData.ActionStr state)
        {
            lock (padlock)
            {
                if (IsRepeat(key))
                {
                    info[key].actionstate = state;
                }
            }
        }

        /// <summary>
        /// 检验有无重复数据
        /// </summary>
        /// <param name="key"></param>
        public bool IsRepeat(string key)
        {
            return info.Keys.Contains(key);
        }

        /// <summary>
        /// 记录用户数据
        /// </summary>
        /// <param name="n"></param>
        /// <param name="ui"></param>
        public void ReadUserInfo(string key, int n, string ui)
        {
            lock (padlock)
            {
                if (IsRepeat(key))
                {
                    info[key].userInfoR[(Item)n] = ui;
                }
            }
        }

        List<int> Test = new List<int>();

        /// <summary>
        /// 处理NFC数据
        /// </summary>
        /// <returns></returns>
        public void DealRecData(string cardId, int blockNum, byte[] data)
        {
            if (!info.ContainsKey(cardId))
            {
                MessageBox.Show("数据库中无此数据");
                return;
            }
            ReflashComState(cardId, blockNum, true);

            Test.Add(blockNum);

            string buff = System.Text.Encoding.Default.GetString(data);
            string bu = System.Text.RegularExpressions.Regex.Replace(buff, "[\0]", "");

            lock (padlock)
            {
                info[cardId].userInfoR[(Item)blockNum] = bu;
            }
                

        }

    }
}
