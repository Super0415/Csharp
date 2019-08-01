using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Yungku.Common.IOCard.Net;
using Yungku.Common.IOCardS2;

namespace Yungku.Common.IOCard.DataDeal
{
    public class DataDeal
    {
        public class ConfInfo
        {

            /// <summary>
            /// 网络ip地址
            /// </summary>
            public string NetIP;             
            /// <summary>
            /// 网络端口
            /// </summary>
            public int Netport;             
            /// <summary>
            /// 网络连接超时
            /// </summary>
            public int Netimeout;           
            /// <summary>
            /// 串口波特率
            /// </summary>
            public int Baudrate;           
            /// <summary>
            /// 串口端口
            /// </summary>
            public int Comport;         
            /// <summary>
            /// 串口连接超时
            /// </summary>
            public int Comtimeout;
            /// <summary>
            /// 上位机版本 0-自动识别 1-S1 2-S2
            /// </summary>
            public int VerUper;

        }
        public class AxisData
        {
            /// <summary>
            /// 移动距离
            /// </summary>
            public int Distence;
            public int StartSpeed;           //起始速度
            public int RunSpeed;             //运行速度
            public double Acceleration;         //加速度
            public double Deceleration;         //减速度
            /// <summary>
            /// 当前位置
            /// </summary>
            public int Location;
            /// <summary>
            /// 目标位置
            /// </summary>
            public int Targetlocation;
            public int Direction;            //运动方向

            //回原点参数
            public int SecondSpeed;          //第二速度
            public int ReturnDirection;            //回原点运动方向
            //运动模式
            /// <summary>
            /// 运动模式  0-点对点 1-连续 2-原点
            /// </summary>
            public int RunMode;
            /// <summary>
            /// 停止运动模式 0-立即停 1-减速停
            /// </summary>
            public int StopRunMode;
            //轴IO
            public bool SignEnLimit;          //极限使能信号
            public bool SignEnOrigin;         //原点使能信号
            public bool SignReLimit;          //反转极限信号
            public bool SignReOrigin;         //反转原点信号
            /// <summary>
            /// bit0-报警状态
            /// bit1:忙状态
            /// bit2:错误状态
            /// bit3:正极限信号
            /// bit4:原点信号
            /// bit5:负极限信号
            /// bit6:回原点状态中
            /// </summary>
            public int PWMState;           //轴状态
            /// <summary>
            /// PWM控制输入输出
            /// </summary>
            public int PWMIOState;
            //演示模式
            public int ShowMode;         //演示模式
        }

        //结构体 电机控制状态
        public class EngineData
        {
            public int      FirstNum;       //固件信息读取次数
            /// <summary>
            /// 主板名称
            /// </summary>
            public string   Name;         //主板名称
            public int      SN;              //编号
            public int      switchID; //编码开关状态

            public string   Ver_Info;    //版本信息-公司

            /// <summary>
            /// 轴号
            /// </summary>
            public int Axis;

            /// <summary>
            /// 主板或者扩展卡
            /// </summary>
            public int CardID;
    
            /// <summary>
            /// 主板-IO控制-输入
            /// </summary>
            public int MInput; 

            /// <summary>
            /// 主板-IO控制-输出
            /// </summary>
            public int MOutput;       

            //通讯状态
            public int HeartCount;       //心跳计数
            public int ComHeartCount;       //心跳计数
            public int NetHeartCount;       //心跳计数


        }
        //结构体 通讯状态
        public class CommState
        {
            /// <summary>
            /// 记录硬件端口连接状态 0-未连接 1-连接
            /// </summary>
            public int NetHardCon;
            /// <summary>
            /// 记录socket连接状态 0-未连接 1-连接
            /// </summary>
            public int NetSoftCon;
            /// <summary>
            /// 记录串口硬连接状态 0-未连接 1-连接
            /// </summary>
            public int COMHardCon;
            /// <summary>
            ///记录串口通讯连接状态 0-未连接 1-连接
            /// </summary>
            public int COMSoftCon;
        }

        private ConfInfo Info = new ConfInfo();
        private EngineData enginedata = new EngineData();
        private AxisData[] axisdata = new AxisData[3];
        

        private YKS2CardNet YKS2net = new YKS2CardNet();
        private YKS2Card YKS2Com = new YKS2Card();

        public CommState Comm = new CommState();


        /// <summary>
        /// 配置-网络IP
        /// </summary>
        public string NetIP
        {
            set { Info.NetIP = value;}
            get { return Info.NetIP; }
        }

        /// <summary>
        /// 配置-网络端口
        /// </summary>
        public int Netport
        {
            set { Info.Netport = value; }
            get { return Info.Netport; }
        }

        /// <summary>
        /// 配置-网络超时
        /// </summary>
        public int Netimeout
        {
            set { Info.Netimeout = value; }
            get { return Info.Netimeout; }
        }

        /// <summary>
        /// 配置-串口端口
        /// </summary>
        public int Comport
        {
            set { Info.Comport = value; }
            get { return Info.Comport; }
        }

        /// <summary>
        /// 配置-串口波特率
        /// </summary>
        public int Baudrate
        {
            set { Info.Baudrate = value; }
            get { return Info.Baudrate; }
        }

        /// <summary>
        /// 配置-串口超时
        /// </summary>
        /// <param name="ip"></param>
        public int Comtimeout
        {
            set { Info.Comtimeout = value; }
            get { return Info.Comtimeout; }
        }

        /// <summary>
        /// 配置-上位机版本
        /// </summary>
        /// <param name="ip"></param>
        public int VerUper
        {
            set { Info.VerUper = value; }
            get { return Info.VerUper; }
        }


        

        /// <summary>
        /// 设置串口硬链接状态
        /// </summary>
        /// <param name="COMHard"></param>
        public void SetCOMHard(int COMHard)
        {
            Comm.COMHardCon = COMHard;
        }
        /// <summary>
        /// 获取串口硬链接状态
        /// </summary>
        /// <returns></returns>
        public int GetCOMHard()
        {
            return Comm.COMHardCon;
        }
        public void SetCOMSoft(int COMSoft)
        {
            Comm.COMSoftCon = COMSoft;
        }
        public int GetCOMSoft()
        {
            return Comm.COMSoftCon;
        }
        public void SetNetHard(int NetHard)
        {
            Comm.NetHardCon = NetHard;
        }
        public int GetNetHard()
        {
            return Comm.NetHardCon;
        }
        public void SetNetSoft(int NetSoft)
        {
            Comm.NetSoftCon = NetSoft;
        }
        public int GetNetSoft()
        {
            return Comm.NetSoftCon;
        }

        /// <summary>
        /// 记录心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetHeartCount(int Count)
        {
            enginedata.HeartCount = Count;
        }
        /// <summary>
        /// 获取心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetHeartCount()
        {
            return enginedata.HeartCount;
        }
        /// <summary>
        /// 记录串口心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetComHeartCount(int Count)
        {
            enginedata.ComHeartCount = Count;
        }
        /// <summary>
        /// 获取串口心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetComHeartCount()
        {
            return enginedata.ComHeartCount;
        }
        /// <summary>
        /// 记录网口心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetNetHeartCount(int Count)
        {
            enginedata.NetHeartCount = Count;
        }
        /// <summary>
        /// 获取网口心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetNetHeartCount()
        {
            return enginedata.NetHeartCount;
        }
        public void SetLocation(int loc)
        {
            axisdata[enginedata.Axis].Location = loc; 
        }
        public int GetLocation()
        {
            return axisdata[enginedata.Axis].Location;
        }

        /// <summary>
        /// 设置轴状态
        /// </summary>
        /// <param name="state"></param>
        public void SetPWMState(int state)
        {
            axisdata[enginedata.Axis].PWMState = state;
        }
        /// <summary>
        /// 获取轴状态
        /// </summary>
        /// <returns></returns>
        public int GetPWMState()
        {
            return axisdata[enginedata.Axis].PWMState;
        }
        /// <summary>
        /// 设置轴IO
        /// </summary>
        /// <param name="state"></param>
        public void SetPWMIOState(int state)
        {
            axisdata[enginedata.Axis].PWMIOState = state;
        }
        /// <summary>
        /// 获取轴IO
        /// </summary>
        /// <returns></returns>
        public int GetPWMIOState()
        {
            return axisdata[enginedata.Axis].PWMIOState;
        }
        /// <summary>
        /// 设置开关量输入口
        /// </summary>
        /// <param name="state"></param>
        public void SetMInput(int Input)
        {
            enginedata.MInput = Input;
        }
        /// <summary>
        /// 获取开关量输入口
        /// </summary>
        /// <returns></returns>
        public int GetMInput()
        {
            return enginedata.MInput;
        }
        /// <summary>
        /// 设置开关量输出口
        /// </summary>
        /// <returns></returns>
        public void SetMOutput(int Output)
        {
            enginedata.MOutput = Output;
        }
        /// <summary>
        /// 获取开关量输出口
        /// </summary>
        /// <returns></returns>
        public int GetMOutput()
        {
            return enginedata.MOutput;
        }

        /// <summary>
        /// 设置极限使能信号
        /// </summary>
        /// <param name="EnLimit"></param>
        public void SetSignEnLimit(int EnLimit)
        {
            axisdata[enginedata.Axis].SignEnLimit = (EnLimit == 1 ? true : false);
        }
        /// <summary>
        /// 获取极限使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnLimit()
        {
            return axisdata[enginedata.Axis].SignEnLimit;
        }
        /// <summary>
        /// 设置原点使能信号
        /// </summary>
        /// <param name="EnOrigin"></param>
        public void SetSignEnOrigin(int EnOrigin)
        {
            axisdata[enginedata.Axis].SignEnOrigin = (EnOrigin == 1 ? true : false);
        }
        /// <summary>
        /// 获取原点使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnOrigin()
        {
            return axisdata[enginedata.Axis].SignEnOrigin;
        }
        /// <summary>
        /// 设置反转极限信号
        /// </summary>
        /// <param name="ReLimit"></param>
        public void SetSignReLimit(int ReLimit)
        {
            axisdata[enginedata.Axis].SignReLimit = (ReLimit == 1 ? true : false);
        }
        /// <summary>
        /// 获取反转极限信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReLimit()
        {
            return axisdata[enginedata.Axis].SignReLimit;
        }
        /// <summary>
        /// 设置反转原点信号
        /// </summary>
        /// <param name="ReOrigin"></param>
        public void SetSignReOrigin(int ReOrigin)
        {
            axisdata[enginedata.Axis].SignReOrigin = (ReOrigin == 1 ? true : false);
        }
        /// <summary>
        /// 获取反转原点信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReOrigin()
        {
            return axisdata[enginedata.Axis].SignReOrigin;
        }
        /// <summary>
        /// 设置轴号
        /// </summary>
        /// <param name="axle"></param>
        public void SetAxle(int axle)
        {
            enginedata.Axis = axle;
        }
        /// <summary>
        /// 获取轴号
        /// </summary>
        /// <returns></returns>
        public int GetAxle()
        {
            return enginedata.Axis;
        }
        /// <summary>
        /// 设置移动距离
        /// </summary>
        /// <param name="distence"></param>
        public void SetDistence(int distence)
        {
            axisdata[enginedata.Axis].Distence = distence;
        }
        /// <summary>
        /// 获取移动距离
        /// </summary>
        /// <returns></returns>
        public int GetDistence()
        {
            return axisdata[enginedata.Axis].Distence;
        }
        /// <summary>
        /// 设置目标位置
        /// </summary>
        /// <param name="Targetloca"></param>
        public void SetTargetlocation(int Targetloca)
        {
            axisdata[enginedata.Axis].Targetlocation = Targetloca;
        }
        /// <summary>
        /// 获取目标位置
        /// </summary>
        /// <returns></returns>
        public int GetTargetlocation()
        {
            return axisdata[enginedata.Axis].Targetlocation;
        }
        /// <summary>
        /// 设置起始速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetStartSpeed(int speed)
        {
            axisdata[enginedata.Axis].StartSpeed = speed;
        }
        /// <summary>
        /// 获取起始速度
        /// </summary>
        /// <returns></returns>
        public int GetStartSpeed()
        {
            return axisdata[enginedata.Axis].StartSpeed;
        }
        /// <summary>
        /// 设置运行速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetRunSpeed(int speed)
        {
            axisdata[enginedata.Axis].RunSpeed = speed;
        }
        /// <summary>
        /// 获取运行速度
        /// </summary>
        /// <returns></returns>
        public int GetRunSpeed()
        {
            return axisdata[enginedata.Axis].RunSpeed;
        }
        /// <summary>
        /// 设置第二速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetSecondSpeed(int speed)
        {
            axisdata[enginedata.Axis].SecondSpeed = speed;
        }
        /// <summary>
        /// 获取第二速度
        /// </summary>
        /// <returns></returns>
        public int GetSecondSpeed()
        {
            return axisdata[enginedata.Axis].SecondSpeed;
        }
        /// <summary>
        /// 设置加速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetAcceleration(double aspd)
        {
            axisdata[enginedata.Axis].Acceleration = aspd;
        }
        /// <summary>
        /// 获取加速度
        /// </summary>
        /// <returns></returns>
        public double GetAcceleration()
        {
            return axisdata[enginedata.Axis].Acceleration;
        }
        /// <summary>
        /// 设置减速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetDeceleration(double aspd)
        {
            axisdata[enginedata.Axis].Deceleration = aspd;
        }
        /// <summary>
        /// 获取减速度
        /// </summary>
        /// <returns></returns>
        public double GetDeceleration()
        {
            return axisdata[enginedata.Axis].Deceleration;
        }
        /// <summary>
        /// 设置回原点方向
        /// </summary>
        /// <param name="Dir"></param>
        public void SetReturnDirection(int Dir)
        {
            axisdata[enginedata.Axis].ReturnDirection = Dir;
        }
        /// <summary>
        /// 获取回原点方向
        /// </summary>
        /// <returns></returns>
        public int GetReturnDirection()
        {
            return axisdata[enginedata.Axis].ReturnDirection;
        }
        /// <summary>
        /// 设置运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetRunMode(int Mode)
        {
            axisdata[enginedata.Axis].RunMode = Mode;
        }
        /// <summary>
        /// 获取运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetRunMode()
        {
            return axisdata[enginedata.Axis].RunMode;
        }
        /// <summary>
        /// 设置运动方向
        /// </summary>
        /// <param name="Mode"></param>
        public void SetDirection(int Dir)
        {
            axisdata[enginedata.Axis].Direction = Dir;
        }
        /// <summary>
        /// 获取运动方向
        /// </summary>
        /// <param name="Mode"></param>
        public int GetDirection()
        {
            return axisdata[enginedata.Axis].Direction;
        }
        /// <summary>
        /// 设置停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetStopRunMode(int StopMode)
        {
            axisdata[enginedata.Axis].StopRunMode = StopMode;
        }
        /// <summary>
        /// 获取停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetStopRunMode()
        {
            return axisdata[enginedata.Axis].StopRunMode;
        }
        /// <summary>
        /// 设置演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public void SetShowMode(int StopMode)
        {
            axisdata[enginedata.Axis].ShowMode = StopMode;
        }
        /// <summary>
        /// 获取演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public int GetShowMode()
        {
            return axisdata[enginedata.Axis].ShowMode;
        }
        /// <summary>
        /// 设置主板或者扩展卡
        /// </summary>
        /// <param name="Mode"></param>
        public void SetCardID(int ID)
        {
            enginedata.CardID = ID;
        }
        /// <summary>
        /// 获取主板或者扩展卡
        /// </summary>
        /// <param name="Mode"></param>
        public int GetCardID()
        {
            return enginedata.CardID;
        }
        /// <summary>
        /// 设置固件信息读取次数
        /// </summary>
        /// <param name="num"></param>
        public void SetFirstNum(int num)
        {
            enginedata.FirstNum = num;
        }
        /// <summary>
        /// 获取固件信息读取次数
        /// </summary>
        /// <param></param>
        public int GetFirstNum()
        {
            return enginedata.FirstNum;
        }
        /// <summary>
        /// 设置主板名称
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            enginedata.Name = name;
        }

        /// <summary>
        /// 获取主板名称
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return enginedata.Name;
        }
        /// <summary>
        /// 设置固件编号
        /// </summary>
        /// <param name="name"></param>
        public void SetSN(int sn)
        {
            enginedata.SN = sn;
        }

        /// <summary>
        /// 获取固件编号
        /// </summary>
        /// <returns></returns>
        public int GetSN()
        {
            return enginedata.SN;
        }
        /// <summary>
        /// 设置编码开关状态
        /// </summary>
        /// <param name="name"></param>
        public void SetswitchID(int sn)
        {
            enginedata.switchID = sn;
        }

        /// <summary>
        /// 获取编码开关状态
        /// </summary>
        /// <returns></returns>
        public int GetswitchID()
        {
            return enginedata.switchID;
        }
        /// <summary>
        /// 设置版本信息
        /// </summary>
        /// <param name="name"></param>
        public void SetVer_Info(string info)
        {
            enginedata.Ver_Info = info;
        }

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        public string GetVer_Info()
        {
            return enginedata.Ver_Info;
        }



        /// <summary>
        /// 线程处理
        /// </summary>
        public void ThreadMain()
        { 
            while (true)
            {

                Thread.Sleep(30);
                if (Comm.NetHardCon == 1 && Comm.NetSoftCon == 1)  //连接网络
                {
                    //心跳检测
                    if (!YKS2net.IsExists())
                    {
                        //enginedata.HeartCount++;
                        //if (enginedata.HeartCount > 3)
                        //{
                        //    enginedata.HeartCount = 0;
                        //    Comm.NetSoftCon = 0;

                        //}
                        continue;
                    }
                    else
                    {
                        //enginedata.HeartCount = 0;
                        //enginedata.NetHeartCount = 0;

                    }

                    if (enginedata.FirstNum == 0)  //读取固件信息
                    {
                        enginedata.FirstNum = 1;

                        //获取主板名称
                        enginedata.Name = YKS2net.GetCardName();
                        //获取固件编号
                        enginedata.SN = YKS2net.GetSN();
                        //获取编码开关状态
                        enginedata.switchID = YKS2net.GetDipSwitch();
           
                        ////获取版本信息
                        enginedata.Ver_Info = YKS2net.GetVerInfo();

                    }


                    //获取轴当前位置
                    axisdata[enginedata.Axis].Location = YKS2net.GetPosition(enginedata.Axis);

                    //获取轴IO状态
                    axisdata[enginedata.Axis].PWMIOState = YKS2net.GetAIO(); //PWM控制输入输出状态


                    if (enginedata.CardID == 1)
                    {
                        //获取扩展板输入端口值
                        enginedata.MInput = YKS2net.GetInputsEx();
                        //获取扩展板输出端口值
                        enginedata.MOutput = YKS2net.GetOutputsEx();

                    }
                    else
                    {
                        //获取主板输入端口值
                        enginedata.MInput = YKS2net.GetInputs();
                        //获取主板输出端口值
                        enginedata.MOutput = YKS2net.GetOutputs();
                    }
                    

                     

                   //获取轴状态
                   axisdata[enginedata.Axis].PWMState = YKS2net.GetAxisStatus(enginedata.Axis);
        

                    if (axisdata[enginedata.Axis].ShowMode == 1) 
                    {
                        if ((axisdata[enginedata.Axis].PWMState >> 1 & 0x1) == 0)
                        {
                            int Dis = axisdata[enginedata.Axis].Direction > 0 ? axisdata[enginedata.Axis].Distence : -axisdata[enginedata.Axis].Distence;
                            YKS2net.RltMove(enginedata.Axis, Dis, axisdata[enginedata.Axis].StartSpeed, axisdata[enginedata.Axis].RunSpeed, axisdata[enginedata.Axis].Acceleration, axisdata[enginedata.Axis].Deceleration);
                            if (axisdata[enginedata.Axis].Direction == 0)
                            {
                                axisdata[enginedata.Axis].Direction = 1;
                            }
                            else
                            {
                                axisdata[enginedata.Axis].Direction = 0;
                            }
                        }
                    }
                  
                }
                else if (Comm.NetHardCon == 0 && Comm.NetSoftCon == 0)//断开网络
                {
                }
            }
        }

   }
}
