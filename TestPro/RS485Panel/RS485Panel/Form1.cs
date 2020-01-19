﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;

namespace RS485Panel
{
    public partial class Form1 : Form
    {
        #region  接口定义

        /// <summary>
        /// 串口项内容
        /// </summary>
        int ComPort = 0;
        /// <summary>
        /// 串口波特率
        /// </summary>
        int ComRate = 9600;
        /// <summary>
        /// 串口状态：false-未连接通讯 true-连接通讯
        /// </summary>
        bool ComState = false;
        /// <summary>
        /// 波特率项
        /// </summary>
        int[] BaudItem = { 9600, 115200 };
        /// <summary>
        /// 自定义485协议对象
        /// </summary>
        CustomRS485 Com = new CustomRS485();

        /// <summary>
        /// 处理串口通讯，需要自动发送部分
        /// </summary>
        private Thread thread = null;
        #endregion

        #region 数据定义

        /// <summary>
        /// 自定义颜色
        /// </summary>
        Color MyGray = Color.FromArgb(255, 105, 105, 105);
        /// <summary>
        /// 从机地址
        /// </summary>
        int ComAddr = 1;


        /// <summary>
        /// 不同地址下从机状态数据存储类型
        /// </summary>
        private struct DataStruct
        {
            /// <summary>
            /// 从机地址
            /// </summary>
            public int ComAddr;

            /// <summary>
            /// 通讯状态
            /// </summary>
            public bool CommState;
            /// <summary>
            /// 通讯状态计数
            /// </summary>
            public byte CommStateCount;

            /// <summary>
            /// 固定输入口状态
            /// </summary>
            public byte State_FixedInput;
            /// <summary>
            /// 固定输出口状态
            /// </summary>
            public byte State_FixedOutput;

            /// <summary>
            /// 可调口类型
            /// </summary>
            public byte Type_IO;
            public byte Type_IOState;

        };

        /// <summary>
        /// 所以信息集Data
        /// </summary>
        DataStruct[] Data = new DataStruct[0x100];
        /// <summary>
        /// 活动中的信息索引号
        /// </summary>
        int[] DataNum = new int[0x100];
        /// <summary>
        /// 活动中的信息总数
        /// </summary>
        int DataTotal = 0;


        byte[] TESTCMDinfo = new byte[7];
        int TESTCMDCode = 0xFFFF;
        long TESTCMDSendNum = 0;
        long TESTCMDReceNum = 0;

        #endregion



        /// <summary>
        /// 串口下拉列表
        /// </summary>
        /// <param name="box"></param>
        private void comConf_ReflashPort(ComboBox box)
        {
            box.Items.Clear();
            box.Items.Add("刷新");  //配置选项
            box.SelectedIndex = 0;    //配置索引序号
            string[] ports = SerialPort.GetPortNames();
            for (int i = 1; i <= ports.Length; i++)
            {
                box.Items.Add(ports[i - 1]);  //配置选项
                box.SelectedIndex = i;    //配置索引序号
            }
        }

        /// <summary>
        /// 波特率下拉列表
        /// </summary>
        /// <param name="box"></param>
        private void comConf_BaudRate(ComboBox box)
        {
            for (int i = 0; i < BaudItem.Length; i++)
            {
                box.Items.Add(BaudItem[i]);  //配置选项
                box.SelectedIndex = i;    //配置索引序号
            }
        }

        /// <summary>
        /// 串口选择，关闭下拉列表时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int PortChoose = 0;
        private void cbbCom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbCom.SelectedIndex == 0)
            {
                comConf_ReflashPort(cbbCom);
                cbbCom.SelectedIndex = PortChoose;
            }
            else
            {
                PortChoose = cbbCom.SelectedIndex;
            }

            string str = (string)cbbCom.SelectedItem;
            string[] sArray = Regex.Split(str, "COM", RegexOptions.IgnoreCase);
            ComPort = Convert.ToInt32(sArray[1]);
        }

        /// <summary>
        /// 波特率更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComRate = BaudItem[cbbRate.SelectedIndex];
        }

        /// <summary>
        /// 从机地址更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAddr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComAddr = Convert.ToInt32(tbAddr.Text);
            }
            catch
            {
                tbAddr.Text = ComAddr.ToString();
            }

            if (ComAddr > 255)
            {
                ComAddr = 255;
                tbAddr.Text = ComAddr.ToString();
            }
            else if (ComAddr < 0)
            {
                ComAddr = 0;
                tbAddr.Text = ComAddr.ToString();
            }
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        public void RecodeInfo(string info)
        {
            tbRecode.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff  ") + info + "\r\n");
        }
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="TimeAble">是否显示时间戳</param>
        public void RecodeInfo(string info, bool TimeAble)
        {
            tbRecode.AppendText((TimeAble ? DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff  ") : "") + info + "\r\n");
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="TimeAble">是否显示时间戳</param>
        /// <param name="LineAble">是否换行</param>
        public void RecodeInfo(string info, bool TimeAble, bool LineAble)
        {
            tbRecode.AppendText((TimeAble ? DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff  ") : "") + info + (LineAble ? "\r\n" : ""));
        }


        public Form1()
        {
            InitializeComponent();
            comConf_ReflashPort(cbbCom);
            cbbCom.SelectedIndex = 0;
            comConf_BaudRate(cbbRate);
            cbbRate.SelectedIndex = 0;
            cbCheckMode.Checked = false;
            //tbAddr.Text = "0";

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 点击串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbbCom.Text.Equals("刷新"))
                {
                    RecodeInfo("请选择串口！");
                    return;
                }
                if (!ComState)
                {
                    RecodeInfo("打开串口");
                    btnOpen.Text = "关闭";

                    thread_MatchLower();

                    if (Data.Count() != 0)
                    {
                        Com.Port = ComPort;
                        Com.BaudRate = ComRate;
                        Com.Timeout = 100;
                        Com.Open();         //打开串口
                        ComState = true;

                        thread = new Thread(thread_AutoDeal);
                        thread.Start();
                        timer1.Enabled = true;
                        panel1.Enabled = true;

                        ComAddr = Data[DataNum[0]].ComAddr;
                        tbAddr.Text = ComAddr.ToString();
                        if (cbCheck.Checked)
                        {
                            RecodeInfo("匹配总线从机成功!");
                            RecodeInfo("匹配总线地址：",false,false);
                            for (int i = 0; i < DataTotal; i++)
                            {
                                RecodeInfo(DataNum[i].ToString() + " " ,false, false);
                            }
                            RecodeInfo("", false);  //换行
                        }
                    }
                    else
                    {
                        if (cbCheck.Checked) RecodeInfo("当前总线无挂载从机！关闭串口！");
                        btnOpen.Text = "打开";
                    }
                }
                else
                {
                    RecodeInfo("关闭串口");
                    Com.Close();         //关闭串口
                    timer1.Enabled = false;
                    panel1.Enabled = false;
                    if (thread != null) thread.Abort();
                    ComState = false;
                    btnOpen.Text = "打开";
                }
            }
            catch (Exception ex)
            {
                RecodeInfo("串口异常：" + ex.Message);
                btnOpen.Text = "打开";
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取总线上从机地址
        /// </summary>
        private void thread_MatchLower()
        {
            //初始化
            DataTotal = 0;
            for (int i = 0; i < 0x100; i++)
                DataNum[i] = 0x100;


            if (!cbCheck.Checked)   //不查总线
            {
                for (int i = 0; i < 0x100; i++)
                {
                    DataNum[DataTotal] = i;
                    Data[DataNum[DataTotal]].ComAddr = i;
                    Data[DataNum[DataTotal]].CommState = true;
                    DataTotal++;
                }
            }
            else      //查总线
            {
                try
                {
                    Com.Port = ComPort;
                    Com.BaudRate = ComRate;
                    Com.Timeout = 30;
                    Com.Open();         //打开串口
                    if (cbCheck.Checked) RecodeInfo("匹配总线从机中，预计5s，请稍等...");
                    for (int i = 0; i <= 0xFF; i++)
                    {

                        if (Com.Radio_FindAddr(i))
                        {
                            DataNum[DataTotal] = i;
                            Data[DataNum[DataTotal]].ComAddr = i;
                            Data[DataNum[DataTotal]].CommState = true;
                            DataTotal++;
                        }
                        else
                        {
                            for (int j = 0; j < DataNum.Length; j++)
                            {
                                if (DataTotal != 0 && DataNum[j] == i)
                                {
                                    for (int k = j; k < DataNum.Length; k++)
                                    {
                                        DataNum[k] = DataNum[k + 1];
                                    }
                                    DataTotal--;
                                }
                            }
                        }
                        if (cbCheck.Checked)
                        {
                            if (i == 30) RecodeInfo("匹配总线从机中，预计4s，请稍等...");
                            else if (i == 60) RecodeInfo("匹配总线从机中，预计3s，请稍等...");
                            else if (i == 90) RecodeInfo("匹配总线从机中，预计2s，请稍等...");
                            else if (i == 120) RecodeInfo("匹配总线从机中，预计1s，请稍等...");
                        }
                    }
                    Array.Sort(DataNum); //从小到大排序。
                    Com.Close();         //关闭串口
                }
                catch (Exception)
                {
                    RecodeInfo("总线匹配失败！");
                }
            }
        }

        /// <summary>
        /// 线程-处理自动检测任务
        /// </summary>
        private void thread_AutoDeal()
        {
            Byte[] Rmsg = new byte[7];

            while (true)
            {
                
                if (TestCMDModel)
                {
                    if (TESTCMDCode == 0xFFFF) Com.SetCheckNum(TESTCMDSendNum);
                    if (TESTCMDSendNum == 0)
                    {
                        TESTCMDCode = Com.Test_SendCMD(TESTCMDinfo);
                    }
                    else
                    {
                        long[] info = Com.GetCheckInfo();
                        TESTCMDReceNum = info[1];
                        if (TESTCMDReceNum < TESTCMDSendNum)
                        {
                            TESTCMDCode = Com.Test_SendCMD(TESTCMDinfo);
                        }
                    }              
                }

                if (cbCheckSlave.Checked == true)
                {
                    int index = Array.IndexOf(DataNum, ComAddr);
                    if (index != (-1))
                    {
                        int indexD = DataNum[index];    //通讯地址

                        Rmsg = Com.Query_AllFixdInput(indexD, 0xFF);    //查固定输入
                        if (Rmsg != null && Rmsg[5] == 0)
                        {
                            Data[indexD].State_FixedInput = Rmsg[4];
                            Data[indexD].CommState = true;
                            if (Data[indexD].CommStateCount != 0)
                            {
                                Data[indexD].CommStateCount = 0;
                                RecodeInfo("从机：" + Data[indexD].ComAddr.ToString() + "上线！");
                            }
                        }
                        else
                        {
                            if (Rmsg == null && Data[indexD].CommState == true)
                            {
                                if (Data[indexD].CommStateCount < 0x03)
                                {
                                    Data[indexD].CommStateCount++;
                                }
                                else
                                {
                                    Data[indexD].CommState = false;
                                }
                            }
                        }

                        Rmsg = Com.Query_AllFixdOutput(indexD, 0xFF);   //查固定输出???
                        if (Rmsg != null && Rmsg[5] == 0)
                        {
                            Data[indexD].State_FixedOutput = Rmsg[4];
                            Data[indexD].CommState = true;
                           
                        }

                        Rmsg = Com.Query_TypeIO(indexD, 0xFF);         //查可调类型
                        if (Rmsg != null && Rmsg[5] == 0)
                        {
                            Data[indexD].Type_IO = Rmsg[4];
                            Data[indexD].CommState = true;
                        }

                        Rmsg = Com.Query_TypeIO_State(indexD, 0xFF);   //查可调输入输出状态
                        if (Rmsg != null && Rmsg[5] == 0)
                        {
                            Data[indexD].Type_IOState = Rmsg[4];
                            Data[indexD].CommState = true;
                        }
                    }    
                }
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// 退出关闭所有线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0); //关闭主窗体时，关闭所有线程
        }


        /// <summary>
        /// 刷新显示数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Data[ComAddr].CommState)
            {
                #region 通讯显示
                lbLedFixedX0.BackColor = (Data[ComAddr].State_FixedInput & 1 << 0) == 0 ? MyGray : Color.Green;
                lbLedFixedX1.BackColor = (Data[ComAddr].State_FixedInput & 1 << 1) == 0 ? MyGray : Color.Green;
                lbLedFixedX2.BackColor = (Data[ComAddr].State_FixedInput & 1 << 2) == 0 ? MyGray : Color.Green;
                lbLedFixedX3.BackColor = (Data[ComAddr].State_FixedInput & 1 << 3) == 0 ? MyGray : Color.Green;
                lbLedFixedX4.BackColor = (Data[ComAddr].State_FixedInput & 1 << 4) == 0 ? MyGray : Color.Green;
                lbLedFixedX5.BackColor = (Data[ComAddr].State_FixedInput & 1 << 5) == 0 ? MyGray : Color.Green;
                lbLedFixedX6.BackColor = (Data[ComAddr].State_FixedInput & 1 << 6) == 0 ? MyGray : Color.Green;
                lbLedFixedX7.BackColor = (Data[ComAddr].State_FixedInput & 1 << 7) == 0 ? MyGray : Color.Green;

                lbLedFixedY0.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 0) == 0 ? MyGray : Color.Green;
                lbLedFixedY1.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 1) == 0 ? MyGray : Color.Green;
                lbLedFixedY2.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 2) == 0 ? MyGray : Color.Green;
                lbLedFixedY3.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 3) == 0 ? MyGray : Color.Green;
                lbLedFixedY4.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 4) == 0 ? MyGray : Color.Green;
                lbLedFixedY5.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 5) == 0 ? MyGray : Color.Green;
                lbLedFixedY6.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 6) == 0 ? MyGray : Color.Green;
                lbLedFixedY7.BackColor = (Data[ComAddr].State_FixedOutput & 1 << 7) == 0 ? MyGray : Color.Green;

                btnTypeSet0.Text = (Data[ComAddr].Type_IO & 1 << 0) == 0 ? "X0" : "Y0";
                btnTypeSet1.Text = (Data[ComAddr].Type_IO & 1 << 1) == 0 ? "X1" : "Y1";
                btnTypeSet2.Text = (Data[ComAddr].Type_IO & 1 << 2) == 0 ? "X2" : "Y2";
                btnTypeSet3.Text = (Data[ComAddr].Type_IO & 1 << 3) == 0 ? "X3" : "Y3";
                btnTypeSet4.Text = (Data[ComAddr].Type_IO & 1 << 4) == 0 ? "X4" : "Y4";
                btnTypeSet5.Text = (Data[ComAddr].Type_IO & 1 << 5) == 0 ? "X5" : "Y5";
                btnTypeSet6.Text = (Data[ComAddr].Type_IO & 1 << 6) == 0 ? "X6" : "Y6";
                btnTypeSet7.Text = (Data[ComAddr].Type_IO & 1 << 7) == 0 ? "X7" : "Y7";

                lbTypeSet0.BackColor = (Data[ComAddr].Type_IOState >> 0 & 0x01) == 0 ? Color.Green : MyGray;
                lbTypeSet1.BackColor = (Data[ComAddr].Type_IOState >> 1 & 0x01) == 0 ? Color.Green : MyGray;
                lbTypeSet2.BackColor = (Data[ComAddr].Type_IOState >> 2 & 0x01) == 0 ? Color.Green : MyGray;
                lbTypeSet3.BackColor = (Data[ComAddr].Type_IOState >> 3 & 0x01) == 0 ? Color.Green : MyGray;
                lbTypeSet4.BackColor = (Data[ComAddr].Type_IOState >> 4 & 0x01) == 0 ? Color.Green : MyGray;
                lbTypeSet5.BackColor = (Data[ComAddr].Type_IOState >> 5 & 0x01) == 0 ? Color.Green : MyGray;
                lbTypeSet6.BackColor = (Data[ComAddr].Type_IOState >> 6 & 0x01) == 0 ? Color.Green : MyGray;
                lbTypeSet7.BackColor = (Data[ComAddr].Type_IOState >> 7 & 0x01) == 0 ? Color.Green : MyGray;
                #endregion
            }
            else
            {
                #region   当前从机掉线
                if (Data[ComAddr].CommState == false && Data[ComAddr].CommStateCount != 0x00)
                {
                    Data[ComAddr].CommStateCount = 0;
                    RecodeInfo("当前从机：" + Data[ComAddr].ComAddr.ToString() + "掉线！");
                }
                #endregion
                #region 默认灯光效果
                lbLedFixedX0.BackColor = MyGray;
                lbLedFixedX1.BackColor = MyGray;
                lbLedFixedX2.BackColor = MyGray;
                lbLedFixedX3.BackColor = MyGray;
                lbLedFixedX4.BackColor = MyGray;
                lbLedFixedX5.BackColor = MyGray;
                lbLedFixedX6.BackColor = MyGray;
                lbLedFixedX7.BackColor = MyGray;

                lbLedFixedY0.BackColor = MyGray;
                lbLedFixedY1.BackColor = MyGray;
                lbLedFixedY2.BackColor = MyGray;
                lbLedFixedY3.BackColor = MyGray;
                lbLedFixedY4.BackColor = MyGray;
                lbLedFixedY5.BackColor = MyGray;
                lbLedFixedY6.BackColor = MyGray;
                lbLedFixedY7.BackColor = MyGray;

                btnTypeSet0.Text = "X0";
                btnTypeSet1.Text = "X1";
                btnTypeSet2.Text = "X2";
                btnTypeSet3.Text = "X3";
                btnTypeSet4.Text = "X4";
                btnTypeSet5.Text = "X5";
                btnTypeSet6.Text = "X6";
                btnTypeSet7.Text = "X7";

                lbTypeSet0.BackColor = MyGray;
                lbTypeSet1.BackColor = MyGray;
                lbTypeSet2.BackColor = MyGray;
                lbTypeSet3.BackColor = MyGray;
                lbTypeSet4.BackColor = MyGray;
                lbTypeSet5.BackColor = MyGray;
                lbTypeSet6.BackColor = MyGray;
                lbTypeSet7.BackColor = MyGray;
                #endregion
            }

            #region 测试通讯性能
            if (TestTimeModel)
            {
                long[] info = Com.GetCheckInfo();
                lbTestTTime.Text = info[0].ToString();
                lbTestNum.Text = info[1].ToString();
                if (info[1] != 0) lbTestSTime.Text = ((double)((double)info[0]/(double)info[1])).ToString("0.00");
                lbErrnum.Text = info[2].ToString();
            }

            //0 - 通讯正常1 - 校验错误2 - 通讯异常
            if (TestCMDModel)
            {
                if (TESTCMDCode != 0 && TESTCMDCode != 0xFFFF)
                {
                    if (TESTCMDCode == 1) RecodeInfo("指令校验错误！");
                    else if (TESTCMDCode == 2) RecodeInfo("通讯异常！");
                    TestCMDModel = false;
                    tbCMDInfo.ReadOnly = false;
                    tbCMDNum.ReadOnly = false;
                    btnCMD.Text = "开始";
                }
                else
                {
                    long[] info = Com.GetCheckInfo();
                    lbCMDTTime.Text = info[0].ToString();
                    lbCMDSum.Text = info[1].ToString();
                    if (info[1] != 0) lbCMDSTime.Text = ((double)((double)info[0] / (double)info[1])).ToString("0.00");
                    lbCMDErrNum.Text = info[2].ToString();
                }
            }
            #endregion

        }

        /// <summary>
        /// 设置固定输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbLedFixedYn_Click(object sender, EventArgs e)
        {
            DataStruct model = Data.Where(c => c.ComAddr == ComAddr).FirstOrDefault();
            int index = int.Parse((sender as Control).Tag.ToString());
            Com.Set_AllFixdOutput(ComAddr, 1 << index, ((model.State_FixedOutput & 1 << index) == 0 ? 1 : 0) << index);
            RecodeInfo("设置固定输出口" + index.ToString());
        }

        /// <summary>
        /// 设置可调输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbLedSetYn_Click(object sender, EventArgs e)
        {
            DataStruct model = Data.Where(c => c.ComAddr == ComAddr).FirstOrDefault();
            int index = int.Parse((sender as Control).Tag.ToString());
            if (((model.Type_IO >> index) & 0x01) == 1)
            {
                Com.Set_AllSetOutput(ComAddr, 1 << index, ((model.Type_IOState & 1 << index) == 0 ? 1 : 0) << index);
                RecodeInfo("设置可调输出口"+ index.ToString());
            }
            else
            {
                RecodeInfo("无法改变输入口状态！");
            }
           

            
        }

        //private void tbRecode_DoubleClick(object sender, EventArgs e)
        //{
        //    long[] info = Com.GetCheckInfo();
        //    RecodeInfo("总时间(ms)：" + info[0] + " 总次数：" + info[1] + " 单次耗时(ms)："+info[2]);
        //}

        /// <summary>
        /// 地址减法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddrSub_Click(object sender, EventArgs e)
        {

            //int index = Array.IndexOf(DataNum, ComAddr);
            //if (index != (-1))
            //{
            //    int indexD = DataNum[index];


                //int index = Data.FindIndex(a => a.ComAddr == ComAddr);
            int index = Array.IndexOf(DataNum, ComAddr);

            int max = DataTotal-1;

            if (index > 0) index--;
            else index = max;

            tbAddr.Text = DataNum[index].ToString();
            //CheckNowAddr();
        }

        /// <summary>
        /// 地址加法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddrAdd_Click(object sender, EventArgs e)
        {
            //int index = Data.FindIndex(a => a.ComAddr == ComAddr);
            int index = Array.IndexOf(DataNum, ComAddr);
            int max = DataTotal-1;

            if (index < max) index++;
            else index = 0;

            tbAddr.Text = DataNum[index].ToString();
            //CheckNowAddr();
        }


        bool TestTimeModel = false;
        /// <summary>
        /// 开始测试时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest1_Click(object sender, EventArgs e)
        {
            if (!TestTimeModel)
            {
                
                try
                {
                    Com.SetCheckNum(Convert.ToInt64(tbTestNum.Text));
                    TestTimeModel = true;
                    tbTestNum.ReadOnly = true;
                    btnTest1.Text = "停止";
                    panel3.Enabled = false;
                    cbCheckSlave.Checked = true;
                }
                catch
                {
                    MessageBox.Show("配置错误！");
                }
                
            }
            else
            {
                TestTimeModel = false;
                tbTestNum.ReadOnly = false;
                btnTest1.Text = "开始";

                panel3.Enabled = true;
            }
            
        }

        /// <summary>
        /// 报文测试模式
        /// </summary>
        bool TestCMDModel = false;
        /// <summary>
        /// 测试报文
        /// </summary>

        /// <summary>
        /// 报文测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCMD_Click(object sender, EventArgs e)
        {
            //int code = Com.Test_SendCMD();

            if (!TestCMDModel)
            {
                try
                {
                    TESTCMDSendNum = Convert.ToInt64(tbCMDNum.Text);
                    string[] CMDInfoS = tbCMDInfo.Text.Trim().Split(' ');
                    if (CMDInfoS.Length > 7)
                    {
                        RecodeInfo("测试指令格式错误！");
                        return;
                    }
                    for (int i = 0; i < CMDInfoS.Length; i++)
                    {
                        int Temp = Convert.ToInt32(CMDInfoS[i], 16);
                        TESTCMDinfo[i] = (byte)Temp;
                    }
                    TESTCMDCode = 0xFFFF;
                    //Com.SetCheckNum(TESTCMDSendNum);                  
                }
                catch
                {
                    RecodeInfo("测试指令格式错误！");
                    return;
                }               
                TestCMDModel = true;
                tbCMDInfo.ReadOnly = true;
                tbCMDNum.ReadOnly = true;
                btnCMD.Text = "停止";

                panel2.Enabled = false;
            }
            else
            {
                TestCMDModel = false;               
                tbCMDInfo.ReadOnly = false;
                tbCMDNum.ReadOnly = false;
                btnCMD.Text = "开始";

                panel2.Enabled = true;
            }
        }

        private void cbCheckMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckMode.Checked == true)
            {
                groupBox6.Enabled = true;
                cbCheckSlave.Checked = false;
                this.Size = new Size(697, 552);
            }
            else
            {
                groupBox6.Enabled = false;
                this.Size = new Size(498, 552);
            }
        }

        private void cbbRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComRate = Convert.ToInt32(cbbRate.Text);
            }
            catch
            {
                cbbRate.Text = ComRate.ToString();
            }

        }

        
        /// <summary>
        /// 面板数据变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_EnabledChanged(object sender, EventArgs e)
        {
            #region 默认灯光效果
            lbLedFixedX0.BackColor = MyGray;
            lbLedFixedX1.BackColor = MyGray;
            lbLedFixedX2.BackColor = MyGray;
            lbLedFixedX3.BackColor = MyGray;
            lbLedFixedX4.BackColor = MyGray;
            lbLedFixedX5.BackColor = MyGray;
            lbLedFixedX6.BackColor = MyGray;
            lbLedFixedX7.BackColor = MyGray;

            lbLedFixedY0.BackColor = MyGray;
            lbLedFixedY1.BackColor = MyGray;
            lbLedFixedY2.BackColor = MyGray;
            lbLedFixedY3.BackColor = MyGray;
            lbLedFixedY4.BackColor = MyGray;
            lbLedFixedY5.BackColor = MyGray;
            lbLedFixedY6.BackColor = MyGray;
            lbLedFixedY7.BackColor = MyGray;

            btnTypeSet0.Text = "X0";
            btnTypeSet1.Text = "X1";
            btnTypeSet2.Text = "X2";
            btnTypeSet3.Text = "X3";
            btnTypeSet4.Text = "X4";
            btnTypeSet5.Text = "X5";
            btnTypeSet6.Text = "X6";
            btnTypeSet7.Text = "X7";

            lbTypeSet0.BackColor = MyGray;
            lbTypeSet1.BackColor = MyGray;
            lbTypeSet2.BackColor = MyGray;
            lbTypeSet3.BackColor = MyGray;
            lbTypeSet4.BackColor = MyGray;
            lbTypeSet5.BackColor = MyGray;
            lbTypeSet6.BackColor = MyGray;
            lbTypeSet7.BackColor = MyGray;
            #endregion
        }
    }
}
