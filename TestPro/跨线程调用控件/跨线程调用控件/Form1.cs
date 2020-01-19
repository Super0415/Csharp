using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 跨线程调用控件
{
    //代码示例1
    /*
    public partial class Form1 : Form
    {
        /// <summary>
        /// 任务线程
        /// </summary>
        List<Thread> Thread_List = new List<Thread>();

        public Form1()
        {
            InitializeComponent();
            #region method-1
            Control.CheckForIllegalCrossThreadCalls = false;
            #endregion
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        private void RecordInfo(string info)
        {
            textBox1.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff \r\n") + info +"\r\n");
        }
             
        private void 开启线程_Click(object sender, EventArgs e)
        {
            RecordInfo("开启线程");
            Thread thread1 = new Thread(Task_Thread);
            thread1.Start();
            if (Thread_List.IndexOf(thread1) == -1)
            {
                Thread_List.Add(thread1);
            }
        }

        /// <summary>
        /// 任务：打印当前线程ID
        /// </summary>
        private void Task_Thread()
        {
            while (Thread.CurrentThread.ThreadState != ThreadState.Aborted)
            {
                RecordInfo("当前线程ID：" + Thread.CurrentThread.ManagedThreadId.ToString()+ "状态："+ Thread.CurrentThread.IsAlive.ToString());
                Thread.Sleep(1000);
            }        
        }      
        private void 关闭线程_Click(object sender, EventArgs e)
        {
            foreach (Thread item in Thread_List)
            {
                RecordInfo("当前线程ID：" + item.ManagedThreadId.ToString() + " 关闭！");
                item.Abort();
                Thread_List.Remove(item);
            }
        }

        /// <summary>
        /// 强制关闭多线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
    */


    //代码示例2
    ///*
    public partial class Form1 : Form
    {
        /// <summary>
        /// 任务线程
        /// </summary>
        List<Thread> Thread_List = new List<Thread>();
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        private void RecordInfo(object info)
        {
            if (textBox1.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => { this.textBox1.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff \r\n") + x.ToString() + "\r\n"); };

                this.textBox1.Invoke(actionDelegate, info);
            }
            else
            {
                this.textBox1.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff \r\n") + info.ToString() + "\r\n");
            }
        }
        private void 开启线程_Click(object sender, EventArgs e)
        {
            RecordInfo("开启线程");
            Thread thread1 = new Thread(Task_Thread);
            thread1.Start();
            if (Thread_List.IndexOf(thread1) == -1)
            {
                Thread_List.Add(thread1);
            }
        }

        /// <summary>
        /// 任务：打印当前线程ID
        /// </summary>
        private void Task_Thread()
        {
            while (Thread.CurrentThread.ThreadState != ThreadState.Aborted)
            {
                RecordInfo("当前线程ID：" + Thread.CurrentThread.ManagedThreadId.ToString() + "状态：" + Thread.CurrentThread.IsAlive.ToString());

                Thread.Sleep(1000);
            }
        }
        private void 关闭线程_Click(object sender, EventArgs e)
        {
            for (int i = 0;i<Thread_List.Count;i++)
            {
                Thread item = Thread_List[i];
                RecordInfo("当前线程ID：" + item.ManagedThreadId.ToString() + " 关闭！");
                item.Abort();
            }
            Thread_List.ForEach(c=> c.Abort());
        }

        /// <summary>
        /// 强制关闭多线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }

    //*/








    #region method-1
    /*
    public Form1()
    {
        InitializeComponent();
        #region method-1
        Control.CheckForIllegalCrossThreadCalls = false;
        #endregion
    }
    */
    #endregion

}
