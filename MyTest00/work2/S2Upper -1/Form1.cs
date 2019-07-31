using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Yungku.Common.IOCard.Net;
using Yungku.Common.IOCard.DataDeal;
using Yungku.Common.IOCard;


namespace S2Upper
{

    public partial class Form1 : Form
    {     
        YKS2CardNet YKS2net = new YKS2CardNet();
        YKS2Card YKS2Com = new YKS2Card();
        public DataDeal datadeal = new DataDeal();
        Thread thread = null;                   //线程句柄-处理通讯过程
        Thread thread2 = null;                  //创建线程
        

        //****************************************** Myself Code ***********************************************
        // ************************* 网络通讯 *************************
        void NetWorkEventToConnect()
        {
            if (YKS2net.IP == string.Empty)     //确保不会误操作导致IP为空
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " IP为空\r\n");
                return;
            }

            if (!YKS2net.NetWorkPing())
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口硬连接异常\r\n");
                button1.Text = "网口未连接";
                button1.BackColor = Color.White;
                if (datadeal.GetCOMHard() == 0)
                {
                    FromNature(false);
                }

                datadeal.SetNetHard(0);
                datadeal.SetNetSoft(0);
                return;
            }


            if (datadeal.GetNetHard() == 0)
            {
                button1.Text = "网口已连接";
                button1.BackColor = Color.FromArgb(128, 255, 128);
                FromNature(true);
                datadeal.SetNetHard(1);
                if (YKS2net.IsExists())
                {
                    datadeal.SetNetSoft(1);
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口软连接正常" + "\r\n");
                }
                else
                {
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口软连接异常" + "\r\n");
                }
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口断开软链接" + "\r\n");
                button1.Text = "网口未连接";
                button1.BackColor = Color.White;
                if (datadeal.GetCOMHard() == 0)
                {
                    FromNature(false);
                }

                datadeal.SetNetHard(0);
                datadeal.SetNetSoft(0);
            }
        }

        // ************************* 串口通讯 *************************
        //串口通讯 - 配置-端口号、波特率  toolStripComboBox1
        private void COMInit()
        {
            CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到
            int[] item = { 9600, 115200 };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                toolStripComboBox2.Items.Add(item[i]);  //配置选项
                toolStripComboBox2.SelectedIndex = i;    //配置索引序号      
            }
            toolStripComboBox2.SelectedItem = toolStripComboBox2.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                toolStripComboBox1.Items.Add(ports[i]);  //配置选项
                toolStripComboBox1.SelectedIndex = i;    //配置索引序号
            }
            toolStripComboBox1.SelectedItem = toolStripComboBox1.Items[0];    //默认为列表第1个变量

        }
        /// <summary>
        /// 串口运行线程
        /// </summary>
        private void SerialSendReceived()
        {
            while (true)
            {
                Thread.Sleep(30);
                if (datadeal.GetNetSoft() == 1)
                    continue;
                //心跳检测
                if (!YKS2Com.IsExists())
                {
                    datadeal.SetComHeartCount(datadeal.GetComHeartCount() + 1);

                    if(datadeal.GetComHeartCount() > 1)
                    {
                        datadeal.SetCOMSoft(0);
   
                        textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 串口心跳异常！" + "\r\n");
                        YKS2Com.Close();
                        datadeal.SetCOMHard(0);
                        button2.Text = "串口未连接";
                        button2.BackColor = Color.White;
                        if (datadeal.GetNetHard() == 0)
                        {
                            FromNature(false);
                        }
                        thread2.Abort();


                    }
                    continue;
                }
                else
                {
                    datadeal.SetCOMSoft(1);
                }
                if (datadeal.GetFirstNum() == 0)    //读取固件信息
                {
                    datadeal.SetFirstNum(1);

                    //获取主板名称
                    datadeal.SetName(YKS2Com.GetCardName());
                    //获取固件编号
                    datadeal.SetSN(YKS2Com.GetSN());
                    //获取编码开关状态
                    datadeal.SetswitchID(YKS2Com.GetDipSwitch());
                    //获取版本信息
                    datadeal.SetVer_Info(YKS2Com.GetVerInfo());
                }

                //获取轴位置
                datadeal.SetLocation(YKS2Com.GetPosition(datadeal.GetAxle()));
                //获取轴IO
                datadeal.SetPWMIOState(YKS2Com.GetAIO());

                if (datadeal.GetCardID() == 1)
                {
                    //获取主板输入端口值 
                    datadeal.SetMInput(YKS2Com.GetInputsEx());
                    //获取主板输出端口值
                    datadeal.SetMOutput(YKS2Com.GetOutputsEx());
                }
                else
                {
                    //获取主板输入端口值 
                    datadeal.SetMInput(YKS2Com.GetInputs());
                    //获取主板输出端口值
                    datadeal.SetMOutput(YKS2Com.GetOutputs());
                }

                //获取轴状态
                datadeal.SetPWMState(YKS2Com.GetAxisStatus(datadeal.GetAxle()));
                if (datadeal.GetShowMode() == 1)
                {
                    if ((datadeal.GetPWMState() >> 1 & 0x1) == 0)
                    {
                        int Dis = datadeal.GetDirection() > 0 ? datadeal.GetDistence() : -datadeal.GetDistence();
                        YKS2Com.RltMove(datadeal.GetAxle(), Dis, datadeal.GetStartSpeed(), datadeal.GetRunSpeed(), datadeal.GetAcceleration(), datadeal.GetDeceleration());
                        if (datadeal.GetDirection() == 0)
                        {
                            datadeal.SetDirection(1);
                        }
                        else
                        {
                            datadeal.SetDirection(0);
                        }
                    }
                }
            }

        } 
        /// <summary>
        /// 配置属性
        /// </summary>
        /// <param name="able"></param>
        public void FromNature(bool able)
        {
            if (able)
            {
                timer2.Enabled = true;
                button15.Enabled = true; //Y0
                button17.Enabled = true; //Y1
                button21.Enabled = true; //Y2
                button19.Enabled = true; //Y3
                button23.Enabled = true; //Y4
                button25.Enabled = true; //Y5
                button27.Enabled = true; //Y6
                button29.Enabled = true; //Y7
                //X轴
                button9.Enabled = true;  //极限设置
                button10.Enabled = true; //向左键
                button11.Enabled = true; //向右键
                button12.Enabled = true; //减速停
                button13.Enabled = true; //立即停
                button14.Enabled = true; //演习键

                //Y轴
                button18.Enabled = true;  //极限设置
                button22.Enabled = true; //向左键
                button3.Enabled = true; //向右键
                button20.Enabled = true; //减速停
                button4.Enabled = true; //立即停
                button16.Enabled = true; //演习键

                //Z轴
                button30.Enabled = true;  //极限设置
                button32.Enabled = true; //向左键
                button24.Enabled = true; //向右键
                button31.Enabled = true; //减速停
                button26.Enabled = true; //立即停
                button28.Enabled = true; //演习键

            }
            else
            {
                timer2.Enabled = false;
                button15.Enabled = false; //Y0
                button17.Enabled = false; //Y1
                button21.Enabled = false; //Y2
                button19.Enabled = false; //Y3
                button23.Enabled = false; //Y4
                button25.Enabled = false; //Y5
                button27.Enabled = false; //Y6
                button29.Enabled = false; //Y7

                //X轴
                button9.Enabled = false;  //极限设置
                button10.Enabled = false; //向左键
                button11.Enabled = false; //向右键
                button12.Enabled = false; //减速停
                button13.Enabled = false; //立即停
                button14.Enabled = false; //演习键

                //Y轴
                button18.Enabled = false;  //极限设置
                button22.Enabled = false; //向左键
                button3.Enabled = false; //向右键
                button20.Enabled = false; //减速停
                button4.Enabled = false; //立即停
                button16.Enabled = false; //演习键

                //Z轴
                button30.Enabled = false;  //极限设置
                button32.Enabled = false; //向左键
                button24.Enabled = false; //向右键
                button31.Enabled = false; //减速停
                button26.Enabled = false; //立即停
                button28.Enabled = false; //演习键


            }

        }
        //事件API - 点击按钮，连接串口 (comboBox1,comboBox2, button2)
        public void COMEventConnect()
        {
            try
            {
                if (datadeal.GetCOMHard() == 0)    //判断串口是否打开
                {
                    string port = (string)toolStripComboBox1.SelectedItem;
                    string[] port1 = port.Split('M');
                    YKS2Com.Port = Convert.ToInt32(port1[1]);
                    YKS2Com.BaudRate = Convert.ToInt32(toolStripComboBox2.SelectedItem.ToString());
                    if (toolStripTextBox4.Text != "")               //记录串口通讯-超时
                        YKS2Com.Timeout = int.Parse(toolStripTextBox4.Text);
                    YKS2Com.Open();

                    button2.Text = "串口已连接";
                    button2.BackColor = Color.FromArgb(128, 255, 128);
                    if (datadeal.GetNetHard() == 0)
                    {
                        FromNature(true);
                    }

                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "打开串口" + "\r\n");
                    datadeal.SetCOMHard(1);

                    thread2 = new Thread(SerialSendReceived);
                    thread2.Start();
                }
                else
                {
                    if (datadeal.GetNetHard() == 0)
                    {
                        FromNature(false);
                    }
                    YKS2Com.Close();
                    button2.Text = "串口未连接";
                    button2.BackColor = Color.White;


                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "关闭串口" + "\r\n");
                    datadeal.SetCOMHard(0);
                    thread2.Abort();

                }
            }
            catch(Exception ex)
            {
                YKS2Com.Close(); 
                MessageBox.Show(ex.Message);
            }
        }

        //********************** 组件配置 *************************************
        //配置运动模式
        public void Conf_ComboboxN()
        {
            Tool_Comboboxn(comboBox1);
            Tool_Comboboxn(comboBox2);
            Tool_Comboboxn(comboBox3);
        }
        public void Tool_Comboboxn(ComboBox Box)
        {
            string[] item = { "点到点", "连续", "原点" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                Box.Items.Add(item[i]);
                Box.SelectedIndex = i;    //配置索引序号

            }
            Box.SelectedItem = Box.Items[1];    //默认为列表第二个变量 
        }
        void Tool_ChangeCardIDFont()
        {
            if (datadeal.GetCardID() == 0)
            {
                datadeal.SetCardID(1);  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Regular);
                this.label57.Font = new Font("宋体", 9, FontStyle.Bold);

                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "切换到S1卡：" + datadeal.GetCardID() + "\r\n");

            }
            else
            {
                datadeal.SetCardID(0);  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Bold);
                this.label57.Font = new Font("宋体", 9, FontStyle.Regular);
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "切换到主板：" + datadeal.GetCardID() + "\r\n");
            }
            datadeal.SetMInput(0);
            datadeal.SetMOutput(0);
        }
        /// <summary>
        /// 根据轴号改变字体粗体，区分显示，以及记录轴号
        /// </summary>
        void Tool_ChangeAxleFont(ushort num)
        {


        }
        //获取输出口的值
        int GetterValue(int Value, ushort bit)
        {
            return Value >> bit & 1;
        }
        //配置输出口的值
        int SetterValue(int Value, ushort bit, ushort State)
        {
            if (State == 1) { Value &= (1 << bit) ^ (0xFFFF); }
            else { Value |= 1 << bit; }
            return Value;
        }

        //为窗口2准备的函数接口 SignEnLimit;         //极限使能信号
        //public string SetSignEnLimit{set{datadeal.SignEnLimit = ushort.Parse(value);}}
        public int SetSignEnLimit { set { datadeal.SetSignEnLimit(value); } get { return datadeal.GetSignEnLimit() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignEnOrigin;        //原点使能信号
        //public string SetSignEnOrigin { set { engineData.SignEnOrigin = ushort.Parse(value); } }
        public int SetSignEnOrigin { set { datadeal.SetSignEnOrigin(value); } get { return datadeal.GetSignEnOrigin() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignReversalLimit;   //反转极限信号
        //public string SetSignReversalLimit { set { engineData.SignReversalLimit = ushort.Parse(value); } }
        public int SetSignReversalLimit { set { datadeal.SetSignReLimit(value); } get { return datadeal.GetSignReLimit() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //反转原点信号
        //public string SetSignReversalOrigin { set { engineData.SignReversalOrigin = ushort.Parse(value); } }
        public int SetSignReversalOrigin { set { datadeal.SetSignReOrigin(value); } get { return datadeal.GetSignReOrigin() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //发送极限设置
        public void SetSignSendFirm()
        {
            if ((datadeal.GetNetSoft() == 1))
                YKS2net.SetLimits(datadeal.GetAxle(), datadeal.GetSignEnLimit(), datadeal.GetSignEnOrigin(), datadeal.GetSignReLimit(), datadeal.GetSignReOrigin());
            else if ((datadeal.GetCOMSoft() == 1))
                YKS2Com.SetLimits(datadeal.GetAxle(), datadeal.GetSignEnLimit(), datadeal.GetSignEnOrigin(), datadeal.GetSignReLimit(), datadeal.GetSignReOrigin());
        }
        //public DataDeal Dealdata { get { return datadeal; } }
        //****************************************** Myself Code ***********************************************


        /// <summary>
        /// 窗体程序
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            ThreadStart threadStart = new ThreadStart(datadeal.ThreadMain);//通过ThreadStart委托告诉子线程执行什么方法　　
            thread = new Thread(threadStart);
            thread.Start();//启动新线程      //关闭窗体，自动关闭线程


            textBox3.Height = 230;                  //数据接收区的高度
            textBox3.Width = 700;                   //数据接收区的宽度
            toolStripTextBox1.Text = "192.168.1.100";   //网络通讯-IP
            toolStripTextBox2.Text = "4000";            //网络通讯-port
            toolStripTextBox3.Text = "300";             //网络通讯-timeout
            toolStripTextBox4.Text = "300";             //串口通讯-timeout
            
            //X轴数据初始化
            textBox4.Text = "0";                    //当前位置
            textBox5.Text = "1000";                 //距离
            textBox6.Text = "0.2";                  //加速度
            textBox7.Text = "100";                  //起始速度
            textBox8.Text = "10000";                //运行速度
            textBox9.Text = "1000";                 //第二速度
            textBox10.Text= "0";                    //目标位置

            //Y轴数据初始化
            textBox15.Text = "0";                    //当前位置
            textBox13.Text = "1000";                 //距离
            textBox11.Text = "0.2";                  //加速度
            textBox14.Text = "100";                  //起始速度
            textBox12.Text = "10000";                //运行速度
            textBox2.Text = "1000";                 //第二速度
            textBox1.Text = "0";                    //目标位置

            //Z轴数据初始化
            textBox22.Text = "0";                    //当前位置
            textBox20.Text = "1000";                 //距离
            textBox18.Text = "0.2";                  //加速度
            textBox21.Text = "100";                  //起始速度
            textBox19.Text = "10000";                //运行速度
            textBox17.Text = "1000";                 //第二速度
            textBox16.Text = "0";                    //目标位置

            //engineData.CMDID = (int)CMD.CMDTopNum.H;
            //端口显示控件 波特率显示控件
            COMInit();
            //控件配置-下拉列表3
            Conf_ComboboxN();
            FromNature(false);

        }
        //点击事件 - 网口连接状态判断
        private void button1_Click(object sender, EventArgs e)
        {
            string ip = "192.168.1.100";
            int port = 4000;
            int time = 300;
            if (toolStripTextBox1.Text != "")               //记录网络通讯-IP
                ip = toolStripTextBox1.Text.Trim();
            if (toolStripTextBox2.Text != "")               //记录网络通讯-port
                port = int.Parse(toolStripTextBox2.Text);
            if (toolStripTextBox3.Text != "")               //记录网络通讯-超时
                time = int.Parse(toolStripTextBox3.Text);
            //网络通讯 - 配置IP与端口号
            YKS2net.NetWorkInit(ip, port, time);
            NetWorkEventToConnect();
        }
        //点击事件 - 串口连接
        private void button2_Click(object sender, EventArgs e)
        {
            COMEventConnect();
        }
        //测试模式配置定时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if(commu.NetWorkHardConnectstate == 1) NetWorkGetTcpPOPnnections();        //若物理连接初次导通，则之后开始检测

        }

        /// <summary>
        /// 主板/扩展板切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
            
        }
        /// <summary>
        /// 主板/扩展板切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label28_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }
        /// <summary>
        /// 主板/扩展板切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label57_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }
        /// <summary>
        /// 极限设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            Form3 IForm = new Form3();
            IForm.Owner = this;
            IForm.Show();
        }

        /// <summary>
        /// 事件- 鼠标离开控件，用于保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1MouseLeaveTool(object sender, EventArgs e)
        {

        }
        private void DataTool()
        {
            datadeal.SetAxle(tabControl1.SelectedIndex);        //记录选项卡索引

            if (datadeal.GetAxle() == 0) //X轴
            {
                datadeal.SetDistence(int.Parse(textBox5.Text.Trim()));              //距离
                datadeal.SetAcceleration(double.Parse(textBox6.Text.Trim()));              //加速度
                datadeal.SetStartSpeed(int.Parse(textBox7.Text.Trim()));              //起始速度
                datadeal.SetRunSpeed(int.Parse(textBox8.Text.Trim()));              //运行速度
                datadeal.SetSecondSpeed(int.Parse(textBox9.Text.Trim()));              //第二速度
                datadeal.SetTargetlocation(int.Parse(textBox10.Text.Trim()));              //目标位置

                datadeal.SetRunMode(comboBox3.SelectedIndex);                       //设置运动模式
            }
            else if (datadeal.GetAxle() == 1)//Y轴
            {
                datadeal.SetDistence(int.Parse(textBox13.Text.Trim()));              //距离
                datadeal.SetAcceleration(double.Parse(textBox11.Text.Trim()));              //加速度
                datadeal.SetStartSpeed(int.Parse(textBox14.Text.Trim()));              //起始速度
                datadeal.SetRunSpeed(int.Parse(textBox12.Text.Trim()));              //运行速度
                datadeal.SetSecondSpeed(int.Parse(textBox2.Text.Trim()));              //第二速度
                datadeal.SetTargetlocation(int.Parse(textBox1.Text.Trim()));              //目标位置

                datadeal.SetRunMode(comboBox1.SelectedIndex);                       //设置运动模式
            }
            else//Z轴
            {
                datadeal.SetDistence(int.Parse(textBox20.Text.Trim()));              //距离
                datadeal.SetAcceleration(double.Parse(textBox18.Text.Trim()));              //加速度
                datadeal.SetStartSpeed(int.Parse(textBox21.Text.Trim()));              //起始速度
                datadeal.SetRunSpeed(int.Parse(textBox19.Text.Trim()));              //运行速度
                datadeal.SetSecondSpeed(int.Parse(textBox17.Text.Trim()));              //第二速度
                datadeal.SetTargetlocation(int.Parse(textBox16.Text.Trim()));              //目标位置

                datadeal.SetRunMode(comboBox2.SelectedIndex);                       //设置运动模式
            }

            if (datadeal.GetLocation() >= 0) datadeal.SetReturnDirection(0);    //设置回原点方向
            else datadeal.SetReturnDirection(1);

       
            
            
            

        }
        /// <summary>
        /// 选择X轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //Tool_ChangeAxleFont(0);
            //datadeal.SetAxle(0);
        }
        /// <summary>
        /// 选择Y轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            //Tool_ChangeAxleFont(1);
            //datadeal.SetAxle(1);
        }

        /// <summary>
        /// 选择Z轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            //Tool_ChangeAxleFont(2);
            //datadeal.SetAxle(2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// 配置开关量输出
        /// </summary>
        /// <param name="outnum"></param>
        private void Config_Out(int outnum)
        {
            int MOutput = datadeal.GetMOutput();
            int id = datadeal.GetCardID();

            if ((MOutput >> outnum & 0x1) == 0) MOutput |= 1 << outnum;
            else MOutput &= (1 << outnum) ^ (0xFFFF);

            if ((datadeal.GetNetSoft() == 1))
            {
                if (datadeal.GetCardID() == 1)
                {
                    YKS2net.SetOutputEx(outnum, (MOutput >> outnum & 0x1) == 0 ? false : true);
                }
                else YKS2net.SetOutputs((byte)MOutput);

            }
            else if ((datadeal.GetCOMSoft() == 1))
            {
                if (datadeal.GetCardID() == 1)
                {
                    YKS2Com.SetOutputEx(outnum, (MOutput >> outnum & 0x1) == 0 ? false : true);
                }
                else YKS2Com.SetOutputs((byte)MOutput);
            }

        }
        /// <summary>
        /// 开关量-输出口0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            Config_Out(0);
        }
        /// <summary>
        /// 开关量-输出口1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            Config_Out(1);
        }

        /// <summary>
        /// 开关量-输出口2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            Config_Out(2);
        }

        /// <summary>
        /// 开关量-输出口3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            Config_Out(3);
        }

        /// <summary>
        /// 开关量-输出口4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            Config_Out(4);
        }

        /// <summary>
        /// 开关量-输出口5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            Config_Out(5);
        }

        /// <summary>
        /// 开关量-输出口6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            Config_Out(6);
        }

        /// <summary>
        /// 开关量-输出口7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            Config_Out(7);
        }

        /// <summary>
        /// 向左运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            button_ToLeft_Click();
        }
        private void button_ToLeft_Click()
        {
            DataTool();
            datadeal.SetDirection(0);

            int axisNo = datadeal.GetAxle();
            int pos = datadeal.GetTargetlocation();
            int startVel = datadeal.GetStartSpeed();
            int vel = datadeal.GetRunSpeed();
            double acc = datadeal.GetAcceleration();
            double dec = datadeal.GetDeceleration();
            int dist = datadeal.GetDirection() > 0 ? datadeal.GetDistence() : (-datadeal.GetDistence());
            int homeDir = datadeal.GetReturnDirection();
            int homeSVel = datadeal.GetSecondSpeed();
            int homeMode = 0;   //预留参数
            int offset = 0;     //预留参数
            if ((datadeal.GetNetSoft() == 1) && !YKS2net.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2net.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2net.RltMove(axisNo, -2147483647, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2net.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else if ((datadeal.GetCOMSoft() == 1) && !YKS2Com.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2Com.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2Com.RltMove(axisNo, -2147483647, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2Com.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 执行错误！\r\n");
            }

        }

        /// <summary>
        /// 向右运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            button_ToRight_Click();
        }
        private void button_ToRight_Click()
        {
            DataTool();

            datadeal.SetDirection(1);

            int axisNo = datadeal.GetAxle();
            int pos = datadeal.GetTargetlocation();
            int startVel = datadeal.GetStartSpeed();
            int vel = datadeal.GetRunSpeed();
            double acc = datadeal.GetAcceleration();
            double dec = datadeal.GetDeceleration();
            int dist = datadeal.GetDistence();
            int homeDir = datadeal.GetReturnDirection();
            int homeSVel = datadeal.GetSecondSpeed();
            int homeMode = 0;   //预留参数
            int offset = 0;     //预留参数
            if ((datadeal.GetNetHard() == 1) &&!YKS2net.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2net.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2net.RltMove(axisNo, 2147483647, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2net.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else if ((datadeal.GetCOMSoft() == 1) && !YKS2Com.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2Com.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2Com.RltMove(axisNo, 2147483647, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2Com.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 执行错误！\r\n");
            }

        }

        /// <summary>
        /// 窗口灯光变化
        /// </summary>
        void WindosShowLight()
        {
            int MInput = datadeal.GetMInput();
            int MOutput = datadeal.GetMOutput();
            //开关量输出
            if ((MOutput >> 0 & 0x1) == 0) button15.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y0
            else button15.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 1 & 0x1) == 0) button17.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y1
            else button17.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 2 & 0x1) == 0) button21.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y2
            else button21.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 3 & 0x1) == 0) button19.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y3
            else button19.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 4 & 0x1) == 0) button23.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y4
            else button23.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 5 & 0x1) == 0) button25.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y5
            else button25.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 6 & 0x1) == 0) button27.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y6
            else button27.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 7 & 0x1) == 0) button29.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y7
            else button29.BackColor = Color.FromArgb(44, 255, 44);
            //开关量输入
            if ((MInput >> 0 & 0x1) == 0) label5.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X0
            else label5.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 1 & 0x1) == 0) label6.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X1
            else label6.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 2 & 0x1) == 0) label7.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X2
            else label7.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 3 & 0x1) == 0) label8.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X3
            else label8.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 4 & 0x1) == 0) label9.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X4
            else label9.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 5 & 0x1) == 0) label10.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X5
            else label10.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 6 & 0x1) == 0) label11.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X6
            else label11.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 7 & 0x1) == 0) label12.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X7
            else label12.BackColor = Color.FromArgb(44, 255, 44);

            AxleShowLight(datadeal.GetPWMState(), datadeal.GetPWMIOState());
        }
        void ShowLight(int State, Label Lab)
        {
            if (State == 0) Lab.BackColor = Color.FromArgb(28, 66, 28);    //灯光-暗
            else Lab.BackColor = Color.FromArgb(44, 255, 44);
        }
        void AxleShowLight(int PWMSt, int IOSt)
        {
            datadeal.SetSignEnLimit((PWMSt>>7)&0x1);
            datadeal.SetSignEnOrigin((PWMSt >> 8) & 0x1);
            datadeal.SetSignReLimit((PWMSt >> 9) & 0x1);
            datadeal.SetSignReOrigin((PWMSt >> 10) & 0x1);


            if (datadeal.GetAxle() == 0)
            {
                ShowLight((PWMSt >> 1 & 0x1), label17);    //灯光-忙
                ShowLight((PWMSt >> 3 & 0x1), label13);    //灯光-正极限
                ShowLight((PWMSt >> 4 & 0x1), label15);    //灯光-原点
                ShowLight((PWMSt >> 5 & 0x1), label14);    //灯光-负极限
                ShowLight((PWMSt >> 6 & 0x1), label16);    //灯光-回原点
                ShowLight((IOSt & 0x1), label18);           //灯光-脉冲
                ShowLight((IOSt & 0x2), label26);           //灯光-方向

                ShowLight((PWMSt >> 7 & 0x1), label90);    //灯光-极限使能
                ShowLight((PWMSt >> 8 & 0x1), label91);    //灯光-原点使能
                ShowLight((PWMSt >> 9 & 0x1), label92);    //灯光-反极限使能
                ShowLight((PWMSt >> 10 & 0x1), label93);    //灯光-反原点使能


            }
            else if (datadeal.GetAxle() == 1)
            {
                ShowLight((PWMSt >> 1 & 0x1), label56);    //灯光-忙
                ShowLight((PWMSt >> 3 & 0x1), label61);    //灯光-正极限
                ShowLight((PWMSt >> 4 & 0x1), label59);    //灯光-原点
                ShowLight((PWMSt >> 5 & 0x1), label60);    //灯光-负极限
                ShowLight((PWMSt >> 6 & 0x1), label58);    //灯光-回原点
                ShowLight((IOSt & 0x4), label54);           //灯光-脉冲
                ShowLight((IOSt & 0x8), label53);           //灯光-方向

                ShowLight((PWMSt >> 7 & 0x1), label105);    //灯光-极限使能
                ShowLight((PWMSt >> 8 & 0x1), label104);    //灯光-原点使能
                ShowLight((PWMSt >> 9 & 0x1), label103);    //灯光-反极限使能
                ShowLight((PWMSt >> 10 & 0x1), label102);    //灯光-反原点使能
            }
            else
            {
                ShowLight((PWMSt >> 1 & 0x1), label78);    //灯光-忙
                ShowLight((PWMSt >> 3 & 0x1), label82);    //灯光-正极限
                ShowLight((PWMSt >> 4 & 0x1), label80);    //灯光-原点
                ShowLight((PWMSt >> 5 & 0x1), label81);    //灯光-负极限
                ShowLight((PWMSt >> 6 & 0x1), label79);    //灯光-回原点
                ShowLight((IOSt & 0x10), label77);           //灯光-脉冲
                ShowLight((IOSt & 0x20), label76);           //灯光-方向

                ShowLight((PWMSt >> 7 & 0x1), label113);    //灯光-极限使能
                ShowLight((PWMSt >> 8 & 0x1), label112);    //灯光-原点使能
                ShowLight((PWMSt >> 9 & 0x1), label111);    //灯光-反极限使能
                ShowLight((PWMSt >> 10 & 0x1), label110);    //灯光-反原点使能
            }



        }
        /// <summary>
        /// 窗口数据变化
        /// </summary>
        void WindosShowData()
        {
            if (datadeal.GetAxle() == 0)
                textBox4.Text = datadeal.GetLocation().ToString();
            else if (datadeal.GetAxle() == 1)
                textBox15.Text = datadeal.GetLocation().ToString();
            else
                textBox22.Text = datadeal.GetLocation().ToString();

                
            
            

        }
        /// <summary>
        /// 定时器：每10ms发送一次查询命令，查询常规数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            WindosShowLight();
            WindosShowData();

            datadeal.SetNetHeartCount(YKS2net.Gettestnum());
        }

        /// <summary>
        /// 减速停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            button_Stop_Click();
        }
        private void button_Stop_Click()
        {
            datadeal.SetStopRunMode(1);
            int axisNo = datadeal.GetAxle();

            if ((datadeal.GetNetSoft() == 1)/* && !YKS2net.IsBusy(axisNo)*/)
            {
                YKS2net.Stop(axisNo);
            }
            else if ((datadeal.GetCOMSoft() == 1)/* && !YKS2Com.IsBusy(axisNo)*/)
            {
                YKS2Com.Stop(axisNo);
            }
        }

        /// <summary>
        /// 立即停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            button1_EmgStop_Click();
        }
        private void button1_EmgStop_Click()
        {
            datadeal.SetStopRunMode(0);
            int axisNo = datadeal.GetAxle();
            if ((datadeal.GetNetSoft() == 1) /*&& !YKS2net.IsBusy(axisNo)*/)
            {
                YKS2net.EmgStop(axisNo);
            }
            else if ((datadeal.GetCOMSoft() == 1) /*&& !YKS2Com.IsBusy(axisNo)*/)
            {
                YKS2Com.EmgStop(axisNo);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DataTool();
            if (this.radioButton1.Checked == true)
                datadeal.SetDirection(1);        //演示模式正方向
            else
                datadeal.SetDirection(0);        //演示模式负方向

            if (datadeal.GetShowMode() == 0)
            {
                datadeal.SetShowMode(1);    //开始演示
                button14.Text = "停止";
                button9.Enabled = false;  //极限设置
                button10.Enabled = false; //向左键
                button11.Enabled = false; //向右键
                button12.Enabled = false; //减速停
                button13.Enabled = false; //立即停
            }
            else
            {
                datadeal.SetShowMode(0);   //停止演示
                button14.Text = "开始";
                button9.Enabled = true;  //极限设置
                button10.Enabled = true; //向左键
                button11.Enabled = true; //向右键
                button12.Enabled = true; //减速停
                button13.Enabled = true; //立即停
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            datadeal.SetShowMode(0);
            button14.Text = "开始";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            //panel2.Controls.Clear();//移除所有控件
            //Form3 form = new Form3();
            //form.Owner = this;
            //form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            //form.TopLevel = false; //指示子窗体非顶级窗体
            //this.panel2.Controls.Add(form);//将子窗体载入panel
            //form.Show();
        }

        /// <summary>
        /// Y轴向左键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            button_ToLeft_Click();
        }
        /// <summary>
        /// Z轴向左键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button32_Click(object sender, EventArgs e)
        {
            button_ToLeft_Click();
        }
        /// <summary>
        /// Y轴向右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            button_ToRight_Click();
        }

        /// <summary>
        /// Z轴向右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button24_Click(object sender, EventArgs e)
        {
            button_ToRight_Click();
        }

        /// <summary>
        /// Y轴减速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            button_Stop_Click();
        }

        /// <summary>
        /// Z轴减速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button31_Click(object sender, EventArgs e)
        {
            button_Stop_Click();
        }

        /// <summary>
        /// Y轴立即停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            button1_EmgStop_Click();
        }

        /// <summary>
        /// Z轴立即停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            button1_EmgStop_Click();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form3 IForm = new Form3();
            IForm.Owner = this;
            IForm.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Form3 IForm = new Form3();
            IForm.Owner = this;
            IForm.Show();
        }
        private void tabControl1_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            datadeal.SetAxle(tabControl1.SelectedIndex);        //记录选项卡索引
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DataTool();
            if (this.radioButton4.Checked == true)
                datadeal.SetDirection(1);        //演示模式正方向
            else
                datadeal.SetDirection(0);        //演示模式负方向

            if (datadeal.GetShowMode() == 0)
            {
                datadeal.SetShowMode(1);    //开始演示
                button16.Text = "停止";
                button18.Enabled = false;  //极限设置
                button22.Enabled = false; //向左键
                button3.Enabled = false; //向右键
                button20.Enabled = false; //减速停
                button4.Enabled = false; //立即停
            }
            else
            {
                datadeal.SetShowMode(0);   //停止演示
                button16.Text = "开始";
                button18.Enabled = true;  //极限设置
                button22.Enabled = true; //向左键
                button3.Enabled = true; //向右键
                button20.Enabled = true; //减速停
                button4.Enabled = true; //立即停
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            DataTool();
            if (this.radioButton6.Checked == true)
                datadeal.SetDirection(1);        //演示模式正方向
            else
                datadeal.SetDirection(0);        //演示模式负方向

            if (datadeal.GetShowMode() == 0)
            {
                datadeal.SetShowMode(1);    //开始演示
                button28.Text = "停止";
                button30.Enabled = false;  //极限设置
                button32.Enabled = false; //向左键
                button24.Enabled = false; //向右键
                button31.Enabled = false; //减速停
                button26.Enabled = false; //立即停
            }
            else
            {
                datadeal.SetShowMode(0);   //停止演示
                button28.Text = "开始";
                button30.Enabled = true;  //极限设置
                button32.Enabled = true; //向左键
                button24.Enabled = true; //向右键
                button31.Enabled = true; //减速停
                button26.Enabled = true; //立即停
            }
        }

        private void tsmiClearPos_Click(object sender, EventArgs e)
        {
            int axisNo = datadeal.GetAxle();
            if ((datadeal.GetNetHard() == 1) && !YKS2net.IsBusy(axisNo))
            {
                YKS2net.SetPosition(axisNo,0);
            }
            else if ((datadeal.GetCOMSoft() == 1) && !YKS2Com.IsBusy(axisNo))
            {
                YKS2Com.SetPosition(axisNo, 0);
            }
        }

        /// <summary>
        /// Z轴运动控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                textBox20.Enabled = false;
            }
            else textBox20.Enabled = true;
        }
        /// <summary>
        /// Y轴运动控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                textBox13.Enabled = false;
            }
            else textBox13.Enabled = true;
        }
        /// <summary>
        /// X轴运动选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 1)
            {
                textBox5.Enabled = false;
            }
            else textBox5.Enabled = true;
        }

        private void TsmiVers_Click(object sender, EventArgs e)
        {
            string Ver_Co = "Shenzhen Yungku Precision Fisture Co.,Ltd.";
            string Ver_Web = "www.yungku.com";
            string Ver_Card = "S2 Smart Card.";
            string Ver_UD = "xxxx";
            string Ver_SPT = "15221283134@163.com";
            try
            {
                string[] info = datadeal.GetVer_Info().Split('\n');    //版本信息-公司
                Ver_Co = info[0];    //版本信息-公司
                Ver_Web = info[1];    //版本信息-网址
                Ver_Card = info[2];    //版本信息-主板名
                Ver_UD = info[3];       //版本信息-版本号
                Ver_SPT = info[4];      //版本信息-技术支持
            }
            catch { }
            string Name = datadeal.GetName();
            int sn = datadeal.GetSN();

            MessageBox.Show("公司名称：" + Ver_Co + "\n公司网址：" + Ver_Web + "\n产品名称：" + Ver_Card + "\n固件版本：" + Ver_UD + "\n硬件版本：" + Name + "\n产品编号：" + sn + "\n技术支持：" + Ver_SPT, "产品信息", 0);
        }

        private void 网口响应测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = YKS2net.Gettestnum();
            MessageBox.Show("累计数：" + YKS2net.Gettestnum() + "\n累计时：" + YKS2net.GettestTBuf() + "\n单次时长："+ YKS2net.GettestTSum() , "产品信息", 0);
        }
    }
}


