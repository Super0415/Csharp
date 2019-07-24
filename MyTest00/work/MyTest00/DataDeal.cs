using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Yungku.Common.IOCard.Net;

namespace Yungku.Common.IOCard.DataDeal
{
    class DataDeal
    {
        //结构体 电机控制状态
        public class EngineData
        {
            public int timecount;                     //计时计数
            public string Name;                     //主板名称
            public int SN;                     //串口信息
            public ushort boardIP;              //扩展卡编号
            /// <summary>
            /// 轴号
            /// </summary>
            public int Axle;
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
            public int Reserve0;             //预留参数0
            public int Reserve1;             //预留参数1
            public int Reserve2;             //预留参数2
            public int Reserve3;             //预留参数3
            public int Reserve4;             //预留参数4

            public int StateCodingswitch;    //编码开关状态

            public int SetDirection;    //设置运动方向

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

            //主板或者扩展卡
            public int CardID;           //卡号 0-主板 1-扩展S1卡
            //主板-IO控制-输入
            public int MInput;         //主板输入

            //主板-IO控制-输出
            public int MOutput;        //主板输出





            //演示模式
            public int ShowMode;         //演示模式


            //通讯状态
            public int StatePOP;         //通讯状态 0-OK
            public int HeartCount;       //心跳计数
            public int CMDID;       //发送命令ID

        }
        //结构体 通讯状态
        public struct CommState
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
        private YKS2CardNet YKS2net = new YKS2CardNet();

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

        public int GetCMDID()
        {
            return enginedata.CMDID;
        }
        public int GetHeartCount()
        {
            return enginedata.HeartCount;
        }
        public int GetLocation()
        {
            return enginedata.Location;
        }
        public int Gettimecount()
        {
            return enginedata.timecount;
        }
        /// <summary>
        /// 获取轴状态
        /// </summary>
        /// <returns></returns>
        public int GetPWMState()
        {
            return enginedata.PWMState;
        }
        /// <summary>
        /// 获取轴IO
        /// </summary>
        /// <returns></returns>
        public int GetPWMIOState()
        {
            return enginedata.PWMIOState;
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
            enginedata.SignEnLimit = (EnLimit == 0 ? true : false);
        }
        /// <summary>
        /// 获取极限使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnLimit()
        {
            return enginedata.SignEnLimit;
        }
        /// <summary>
        /// 设置原点使能信号
        /// </summary>
        /// <param name="EnOrigin"></param>
        public void SetSignEnOrigin(int EnOrigin)
        {
            enginedata.SignEnOrigin = (EnOrigin == 0 ? true : false);
        }
        /// <summary>
        /// 获取原点使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnOrigin()
        {
            return enginedata.SignEnOrigin;
        }
        /// <summary>
        /// 设置反转极限信号
        /// </summary>
        /// <param name="ReLimit"></param>
        public void SetSignReLimit(int ReLimit)
        {
            enginedata.SignReLimit = (ReLimit == 0 ? true : false);
        }
        /// <summary>
        /// 获取反转极限信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReLimit()
        {
            return enginedata.SignReLimit;
        }
        /// <summary>
        /// 设置反转原点信号
        /// </summary>
        /// <param name="ReOrigin"></param>
        public void SetSignReOrigin(int ReOrigin)
        {
            enginedata.SignReOrigin = (ReOrigin == 0 ? true : false);
        }
        /// <summary>
        /// 获取反转原点信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReOrigin()
        {
            return enginedata.SignReOrigin;
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
            enginedata.Distence = distence;
        }
        /// <summary>
        /// 获取移动距离
        /// </summary>
        /// <returns></returns>
        public int GetDistence()
        {
            return enginedata.Distence;
        }
        /// <summary>
        /// 设置目标位置
        /// </summary>
        /// <param name="Targetloca"></param>
        public void SetTargetlocation(int Targetloca)
        {
            enginedata.Targetlocation = Targetloca;
        }
        /// <summary>
        /// 获取目标位置
        /// </summary>
        /// <returns></returns>
        public int GetTargetlocation()
        {
            return enginedata.Targetlocation;
        }
        /// <summary>
        /// 设置起始速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetStartSpeed(int speed)
        {
            enginedata.StartSpeed = speed;
        }
        /// <summary>
        /// 获取起始速度
        /// </summary>
        /// <returns></returns>
        public int GetStartSpeed()
        {
            return enginedata.StartSpeed;
        }
        /// <summary>
        /// 设置运行速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetRunSpeed(int speed)
        {
            enginedata.RunSpeed = speed;
        }
        /// <summary>
        /// 获取运行速度
        /// </summary>
        /// <returns></returns>
        public int GetRunSpeed()
        {
            return enginedata.RunSpeed;
        }
        /// <summary>
        /// 设置第二速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetSecondSpeed(int speed)
        {
            enginedata.SecondSpeed = speed;
        }
        /// <summary>
        /// 获取第二速度
        /// </summary>
        /// <returns></returns>
        public int GetSecondSpeed()
        {
            return enginedata.SecondSpeed;
        }
        /// <summary>
        /// 设置加速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetAcceleration(double aspd)
        {
            enginedata.Acceleration = aspd;
        }
        /// <summary>
        /// 获取加速度
        /// </summary>
        /// <returns></returns>
        public double GetAcceleration()
        {
            return enginedata.Acceleration;
        }
        /// <summary>
        /// 设置减速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetDeceleration(double aspd)
        {
            enginedata.Deceleration = aspd;
        }
        /// <summary>
        /// 获取减速度
        /// </summary>
        /// <returns></returns>
        public double GetDeceleration()
        {
            return enginedata.Deceleration;
        }
        /// <summary>
        /// 设置回原点方向
        /// </summary>
        /// <param name="Dir"></param>
        public void SetReturnDirection(int Dir)
        {
            enginedata.ReturnDirection = Dir;
        }
        /// <summary>
        /// 获取回原点方向
        /// </summary>
        /// <returns></returns>
        public int GetReturnDirection()
        {
            return enginedata.ReturnDirection;
        }
        /// <summary>
        /// 设置运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetRunMode(int Mode)
        {
            enginedata.RunMode = Mode;
        }
        /// <summary>
        /// 获取运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetRunMode()
        {
            return enginedata.RunMode;
        }
        /// <summary>
        /// 设置运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetDirection(int Dir)
        {
            enginedata.Direction = Dir;
        }
        /// <summary>
        /// 获取运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetDirection()
        {
            return enginedata.Direction;
        }
        /// <summary>
        /// 设置停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetStopRunMode(int StopMode)
        {
            enginedata.StopRunMode = StopMode;
        }
        /// <summary>
        /// 获取停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetStopRunMode()
        {
            return enginedata.StopRunMode;
        }
        /// <summary>
        /// 设置演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public void SetShowMode(int StopMode)
        {
            enginedata.ShowMode = StopMode;
        }
        /// <summary>
        /// 获取演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public int GetShowMode()
        {
            return enginedata.ShowMode;
        }


        

        /// <summary>
        /// 线程处理
        /// </summary>
        public void ThreadMain()
        { 
            while (true)
            {
                enginedata.timecount++;
                Thread.Sleep(30);
                if (Comm.NetHardCon == 1 && Comm.NetSoftCon == 1)  //连接网络
                {
                    enginedata.CMDID = 100;

                    //心跳检测
                    if (YKS2net.IsExists())
                    {
                        enginedata.HeartCount++;
                    }
                    else enginedata.HeartCount = 0;
               
                    //获取轴当前位置
                    enginedata.Location = YKS2net.GetPosition(enginedata.Axle);

                    //获取轴IO状态
                    enginedata.PWMIOState = YKS2net.GetAIO(); //PWM控制输入输出状态

                    //获取主板输入端口值
                    enginedata.MInput = YKS2net.GetInputs();

                    //获取主板输出端口值
                    enginedata.MOutput = YKS2net.GetOutputs();

                    //获取轴状态
                    enginedata.PWMState = YKS2net.GetAxisStatus(enginedata.Axle);

                    if (enginedata.ShowMode == 1) 
                    {
                        if ((enginedata.PWMState >> 1 & 0x1) == 0)
                        {
                            int Dis = enginedata.Direction > 0 ? enginedata.Distence : -enginedata.Distence;
                            YKS2net.RltMove(enginedata.Axle, Dis, enginedata.StartSpeed, enginedata.RunSpeed, enginedata.Acceleration, enginedata.Deceleration);
                            if (enginedata.Direction == 0)
                            {
                                enginedata.Direction = 1;
                            }
                            else
                            {
                                enginedata.Direction = 0;
                            }
                        }
                    }
                  
                }
                else if (Comm.NetHardCon == 0 && Comm.NetSoftCon == 0)//断开网络
                {
                    enginedata.CMDID = 99;
                }


            }
        }


    }
}
