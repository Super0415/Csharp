using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS
{
    class MyData
    {

        enum Item
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
            public string actionstate = string.Empty;

            /// <summary>
            /// 读取用户信息
            /// </summary>
            public string[] userInfoR = new string[20];
            /// <summary>
            /// 写入用户信息
            /// </summary>
            public string[] userInfoW = new string[20];
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
        public void Add(string key)
        {
            DetailInfo d = new DetailInfo();
            lock (padlock)
            {
                info.Add(key, d);
            }
        }

        /// <summary>
        /// 更新卡记录的校验状态
        /// </summary>
        /// <param name="key"></param>
        public void ReflashCardState(string key,string state)
        {            
            lock (padlock)
            {
                if (IsRepeat(key))
                {
                    info[key].checkstate = state;
                }
            }
        }


        /// <summary>
        /// 更新卡记录的通讯动作状态
        /// </summary>
        /// <param name="key"></param>
        public void ReflashCardActionState(string key, string state)
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
        public void ReadUserInfo(string key, int n,string ui)
        {
            lock (padlock)
            {
                if (IsRepeat(key))
                {
                    info[key].userInfoR[n] = ui;
                }
            }
        }

    }
}
