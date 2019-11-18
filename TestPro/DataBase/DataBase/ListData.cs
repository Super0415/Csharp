﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDataBase
{
    class ListData
    {
        private static ListData _instance = null;
        private static readonly object syncRoot = new object();

        /// <summary>
        /// 私有构造函数，保证唯一性
        /// </summary>
        private ListData()
        {
        }

        /// <summary>
        /// 公有静态方法，返回一个唯一的实例
        /// </summary>
        /// <returns></returns>
        public static ListData GetInstance()        //“If - Lock - If”结构模式，即双重检查锁定的双重判断机制：
        {
            if (_instance == null)                  //第一重判断，先判断实例是否存在，不存在再加锁处理
            {
                lock (syncRoot)                     //加锁，在某一时刻只允许一个线程访问
                {
                    if (_instance == null)          //第二重判断: 第一个线程进入Lock中执行创建代码，第二个线程处于排队等待状态，当第二个线程进入Lock后并不知道实例已创建，将会继续创建新的实例
                    {
                        _instance = new ListData();
                    }
                }
            }
            return _instance;
        }

        //创建全局变量，用来存放已show出的窗体对象
        private List<Form> AllForms = new List<Form>();

        /// <summary>
        /// 显示唯一的窗体
        /// </summary>
        /// <param name="Form"></param>
        public void OnlySubformShow(Form Form)
        {
            //判断窗体是否已经弹出，默认false
            bool hasform = false;
            //遍历所有窗体对象
            foreach (Form f in AllForms)
            {
                //判断弹出的窗体是否重复
                if (f.Name == Form.Name)
                {
                    //重复，修改为true
                    hasform = true;
                    f.WindowState = FormWindowState.Normal;
                    //获取焦点
                    f.Focus();
                }
            }
            if (!hasform)
            { 
                //添加到所有窗体中
                AllForms.Add(Form);
                //并打开该窗体
                Form.Show();
            }
        }

        /// <summary>
        /// 删除窗口列表中隐藏的窗体
        /// </summary>
        /// <param name="Form"></param>
        public void OnlySubformHide(Form Form)
        {
            //遍历所有窗体对象
            foreach (Form f in AllForms)
            {
                //判断弹出的窗体是否重复
                if (f.Name == Form.Name)
                {
                    AllForms.Remove(f);
                    f.Hide();
                    break;
                }
            }
        }




        private string line;
        /// <summary>
        /// 读取设置
        /// </summary>
        public string Getline     
        {
            get { return line; }
            set { line = value; }
        }



    }
}
