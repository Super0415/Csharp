using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Yungku.Common.IOCard.Net;
using Yungku.Common.IOCard;

namespace Yungku.Common.IOCard.DataDeal
{
    class DataDeal
    {

        public struct AxleData
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

            public ushort boardIP;        //扩展卡编号
            /// <summary>
            /// 轴号
            /// </summary>
            public int Axle;

            ///// <summary>
            ///// 移动距离
            ///// </summary>
            //public int Distence;             
            //public int StartSpeed;           //起始速度
            //public int RunSpeed;             //运行速度
            //public double Acceleration;         //加速度
            //public double Deceleration;         //减速度
            ///// <summary>
            ///// 当前位置
            ///// </summary>
            //public int Location;            
            ///// <summary>
            ///// 目标位置
            ///// </summary>
            //public int Targetlocation;      
            //public int Direction;            //运动方向
            ////回原点参数
            //public int SecondSpeed;          //第二速度
            //public int ReturnDirection;            //回原点运动方向
            ////运动模式
            ///// <summary>
            ///// 运动模式  0-点对点 1-连续 2-原点
            ///// </summary>
            //public int RunMode;
            ///// <summary>
            ///// 停止运动模式 0-立即停 1-减速停
            ///// </summary>
            //public int StopRunMode;             
            ////轴IO
            //public bool SignEnLimit;          //极限使能信号
            //public bool SignEnOrigin;         //原点使能信号
            //public bool SignReLimit;          //反转极限信号
            //public bool SignReOrigin;         //反转原点信号

            ///// <summary>
            ///// bit0-报警状态
            ///// bit1:忙状态
            ///// bit2:错误状态
            ///// bit3:正极限信号
            ///// bit4:原点信号
            ///// bit5:负极限信号
            ///// bit6:回原点状态中
            ///// </summary>
            //public int PWMState;           //轴状态
            ///// <summary>
            ///// PWM控制输入输出
            ///// </summary>
            //public int PWMIOState;
            ////演示模式
            //public int ShowMode;         //演示模式



            //主板或者扩展卡
            public int CardID;           //卡号 0-主板 1-扩展S1卡
            //主板-IO控制-输入
            public int MInput;         //主板输入

            //主板-IO控制-输出
            public int MOutput;        //主板输出

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

        private EngineData enginedata = new EngineData();
        private AxleData[] axledata = new AxleData[3];
        private AxleData axleXdata = new AxleData();

        private YKS2CardNet YKS2net = new YKS2CardNet();
        private YKS2Card YKS2Com = new YKS2Card();

        public CommState Comm = new CommState();

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
        /// 记录心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetComHeartCount(int Count)
        {
            enginedata.ComHeartCount = Count;
        }
        /// <summary>
        /// 获取心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetComHeartCount()
        {
            return enginedata.ComHeartCount;
        }
        /// <summary>
        /// 记录心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetNetHeartCount(int Count)
        {
            enginedata.NetHeartCount = Count;
        }
        /// <summary>
        /// 获取心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetNetHeartCount()
        {
            return enginedata.NetHeartCount;
        }
        public void SetLocation(int loc)
        {
            axledata[enginedata.Axle].Location = loc;
        }
        public int GetLocation()
        {
            return axledata[enginedata.Axle].Location;
        }

        /// <summary>
        /// 设置轴状态
        /// </summary>
        /// <param name="state"></param>
        public void SetPWMState(int state)
        {
            axledata[enginedata.Axle].PWMState = state;
        }
        /// <summary>
        /// 获取轴状态
        /// </summary>
        /// <returns></returns>
        public int GetPWMState()
        {
            return axledata[enginedata.Axle].PWMState;
        }
        /// <summary>
        /// 设置轴IO
        /// </summary>
        /// <param name="state"></param>
        public void SetPWMIOState(int state)
        {
            axledata[enginedata.Axle].PWMIOState = state;
        }
        /// <summary>
        /// 获取轴IO
        /// </summary>
        /// <returns></returns>
        public int GetPWMIOState()
        {
            return axledata[enginedata.Axle].PWMIOState;
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
            axledata[enginedata.Axle].SignEnLimit = (EnLimit == 1 ? true : false);
        }
        /// <summary>
        /// 获取极限使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnLimit()
        {
            return axledata[enginedata.Axle].SignEnLimit;
        }
        /// <summary>
        /// 设置原点使能信号
        /// </summary>
        /// <param name="EnOrigin"></param>
        public void SetSignEnOrigin(int EnOrigin)
        {
            axledata[enginedata.Axle].SignEnOrigin = (EnOrigin == 1 ? true : false);
        }
        /// <summary>
        /// 获取原点使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnOrigin()
        {
            return axledata[enginedata.Axle].SignEnOrigin;
        }
        /// <summary>
        /// 设置反转极限信号
        /// </summary>
        /// <param name="ReLimit"></param>
        public void SetSignReLimit(int ReLimit)
        {
            axledata[enginedata.Axle].SignReLimit = (ReLimit == 1 ? true : false);
        }
        /// <summary>
        /// 获取反转极限信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReLimit()
        {
            return axledata[enginedata.Axle].SignReLimit;
        }
        /// <summary>
        /// 设置反转原点信号
        /// </summary>
        /// <param name="ReOrigin"></param>
        public void SetSignReOrigin(int ReOrigin)
        {
            axledata[enginedata.Axle].SignReOrigin = (ReOrigin == 1 ? true : false);
        }
        /// <summary>
        /// 获取反转原点信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReOrigin()
        {
            return axledata[enginedata.Axle].SignReOrigin;
        }
        /// <summary>
        /// 设置轴号
        /// </summary>
        /// <param name="axle"></param>
        public void SetAxle(int axle)
        {
            enginedata.Axle = axle;
        }
        /// <summary>
        /// 获取轴号
        /// </summary>
        /// <returns></returns>
        public int GetAxle()
        {
            return enginedata.Axle;
        }
        /// <summary>
        /// 设置移动距离
        /// </summary>
        /// <param name="distence"></param>
        public void SetDistence(int distence)
        {
            axledata[enginedata.Axle].Distence = distence;
        }
        /// <summary>
        /// 获取移动距离
        /// </summary>
        /// <returns></returns>
        public int GetDistence()
        {
            return axledata[enginedata.Axle].Distence;
        }
        /// <summary>
        /// 设置目标位置
        /// </summary>
        /// <param name="Targetloca"></param>
        public void SetTargetlocation(int Targetloca)
        {
            axledata[enginedata.Axle].Targetlocation = Targetloca;
        }
        /// <summary>
        /// 获取目标位置
        /// </summary>
        /// <returns></returns>
        public int GetTargetlocation()
        {
            return axledata[enginedata.Axle].Targetlocation;
        }
        /// <summary>
        /// 设置起始速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetStartSpeed(int speed)
        {
            axledata[enginedata.Axle].StartSpeed = speed;
        }
        /// <summary>
        /// 获取起始速度
        /// </summary>
        /// <returns></returns>
        public int GetStartSpeed()
        {
            return axledata[enginedata.Axle].StartSpeed;
        }
        /// <summary>
        /// 设置运行速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetRunSpeed(int speed)
        {
            axledata[enginedata.Axle].RunSpeed = speed;
        }
        /// <summary>
        /// 获取运行速度
        /// </summary>
        /// <returns></returns>
        public int GetRunSpeed()
        {
            return axledata[enginedata.Axle].RunSpeed;
        }
        /// <summary>
        /// 设置第二速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetSecondSpeed(int speed)
        {
            axledata[enginedata.Axle].SecondSpeed = speed;
        }
        /// <summary>
        /// 获取第二速度
        /// </summary>
        /// <returns></returns>
        public int GetSecondSpeed()
        {
            return axledata[enginedata.Axle].SecondSpeed;
        }
        /// <summary>
        /// 设置加速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetAcceleration(double aspd)
        {
            axledata[enginedata.Axle].Acceleration = aspd;
        }
        /// <summary>
        /// 获取加速度
        /// </summary>
        /// <returns></returns>
        public double GetAcceleration()
        {
            return axledata[enginedata.Axle].Acceleration;
        }
        /// <summary>
        /// 设置减速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetDeceleration(double aspd)
        {
            axledata[enginedata.Axle].Deceleration = aspd;
        }
        /// <summary>
        /// 获取减速度
        /// </summary>
        /// <returns></returns>
        public double GetDeceleration()
        {
            return axledata[enginedata.Axle].Deceleration;
        }
        /// <summary>
        /// 设置回原点方向
        /// </summary>
        /// <param name="Dir"></param>
        public void SetReturnDirection(int Dir)
        {
            axledata[enginedata.Axle].ReturnDirection = Dir;
        }
        /// <summary>
        /// 获取回原点方向
        /// </summary>
        /// <returns></returns>
        public int GetReturnDirection()
        {
            return axledata[enginedata.Axle].ReturnDirection;
        }
        /// <summary>
        /// 设置运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetRunMode(int Mode)
        {
            axledata[enginedata.Axle].RunMode = Mode;
        }
        /// <summary>
        /// 获取运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetRunMode()
        {
            return axledata[enginedata.Axle].RunMode;
        }
        /// <summary>
        /// 设置运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetDirection(int Dir)
        {
            axledata[enginedata.Axle].Direction = Dir;
        }
        /// <summary>
        /// 获取运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetDirection()
        {
            return axledata[enginedata.Axle].Direction;
        }
        /// <summary>
        /// 设置停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetStopRunMode(int StopMode)
        {
            axledata[enginedata.Axle].StopRunMode = StopMode;
        }
        /// <summary>
        /// 获取停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetStopRunMode()
        {
            return axledata[enginedata.Axle].StopRunMode;
        }
        /// <summary>
        /// 设置演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public void SetShowMode(int StopMode)
        {
            axledata[enginedata.Axle].ShowMode = StopMode;
        }
        /// <summary>
        /// 获取演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public int GetShowMode()
        {
            return axledata[enginedata.Axle].ShowMode;
        }
        /// <summary>
        /// 设置演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public void SetCardID(int ID)
        {
            enginedata.CardID = ID;
        }
        /// <summary>
        /// 获取演习模式 0-停止 1-开始
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
                        enginedata.HeartCount++;
                        if (enginedata.HeartCount > 3)
                        {
                            enginedata.HeartCount = 0;
                            Comm.NetSoftCon = 0;

                        }
                        continue;
                    }
                    else
                    {
                        enginedata.HeartCount = 0;
                        enginedata.NetHeartCount = 0;

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
                    axledata[enginedata.Axle].Location = YKS2net.GetPosition(enginedata.Axle);

                    //获取轴IO状态
                    axledata[enginedata.Axle].PWMIOState = YKS2net.GetAIO(); //PWM控制输入输出状态

                    //获取主板输入端口值
                    enginedata.MInput = YKS2net.GetInputs();

                    //获取主板输出端口值
                    enginedata.MOutput = YKS2net.GetOutputs();

                    //获取轴状态
                    axledata[enginedata.Axle].PWMState = YKS2net.GetAxisStatus(enginedata.Axle);

                    if (axledata[enginedata.Axle].ShowMode == 1) 
                    {
                        if ((axledata[enginedata.Axle].PWMState >> 1 & 0x1) == 0)
                        {
                            int Dis = axledata[enginedata.Axle].Direction > 0 ? axledata[enginedata.Axle].Distence : -axledata[enginedata.Axle].Distence;
                            YKS2net.RltMove(enginedata.Axle, Dis, axledata[enginedata.Axle].StartSpeed, axledata[enginedata.Axle].RunSpeed, axledata[enginedata.Axle].Acceleration, axledata[enginedata.Axle].Deceleration);
                            if (axledata[enginedata.Axle].Direction == 0)
                            {
                                axledata[enginedata.Axle].Direction = 1;
                            }
                            else
                            {
                                axledata[enginedata.Axle].Direction = 0;
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
