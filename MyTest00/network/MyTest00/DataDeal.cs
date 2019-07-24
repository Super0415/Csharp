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
            public ushort Axle;
            public int Distence;             //距离
            public double StartSpeed;           //起始速度
            public double RunSpeed;             //运行速度
            public double Acceleration;         //加速度
            public double Deceleration;         //减速度
            /// <summary>
            /// 当前位置
            /// </summary>
            public int Location;             //当前位置
            public int Targetlocation;       //目标位置
            public int Direction;            //运动方向
            public int Reserve0;             //预留参数0
            public int Reserve1;             //预留参数1
            public int Reserve2;             //预留参数2
            public int Reserve3;             //预留参数3
            public int Reserve4;             //预留参数4

            public ushort StateCodingswitch;    //编码开关状态

            public ushort SetDirection;    //设置运动方向

            //回原点参数
            public int SecondSpeed;          //第二速度
            public int ReturnDirection;            //回原点运动方向
            //运动模式
            public int RunMode;                 //运动模式  0-点对点 1-连续 2-原点
            public int StopRunMode;             //停止运动模式 0-立即停 1-减速停
            //轴IO
            public ushort SignEnLimit;          //极限使能信号
            public ushort SignEnOrigin;         //原点使能信号
            public ushort SignReversalLimit;    //反转极限信号
            public ushort SignReversalOrigin;   //反转原点信号

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
            public ushort CardID;           //卡号 0-主板 1-扩展S1卡
            //主板-IO控制-输入
            public int MInput;         //主板输入

            //主板-IO控制-输出
            public int MOutput;        //主板输出





            //演示模式
            public ushort ShowMode;         //演示模式


            //通讯状态
            public ushort StatePOP;         //通讯状态 0-OK
            public ushort HeartCount;       //心跳计数
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

        public void SetCommState(int COMHard, int COMSoft, int NetHard, int NetSoft)
        {
            Comm.COMHardCon = COMHard;
            Comm.COMSoftCon = COMSoft;
            Comm.NetHardCon = NetHard;
            Comm.NetSoftCon = NetSoft;
        }
        public void SetCOMHard(int COMHard)
        {
            Comm.COMHardCon = COMHard;
        }
        public void SetCOMSoft(int COMSoft)
        {
            Comm.COMSoftCon = COMSoft;
        }
        public void SetNetHard(int NetHard)
        {
            Comm.NetHardCon = NetHard;
        }
        public void SetNetSoft(int NetSoft)
        {
            Comm.NetSoftCon = NetSoft;
        }

        public int GetCOMHard()
        {
            return Comm.COMHardCon;
        }
        public int GetCOMSoft()
        {
            return Comm.COMSoftCon;
        }
        public int GetNetHard()
        {
            return Comm.NetHardCon;
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
        public int GetMOutput()
        {
            return enginedata.MOutput;
        }

        /// <summary>
        /// 线程处理
        /// </summary>
        public void ThreadMain()
        {
            
            while (true)
            {
                //YKS2net.NetWorkInit();
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
                    Thread.Sleep(30);
                    //获取轴当前位置
                    enginedata.Location = YKS2net.GetPosition(enginedata.Axle);
                    Thread.Sleep(30);
                    ////获取轴状态
                    enginedata.PWMState = YKS2net.GetAxisStatus(enginedata.Axle);
                    Thread.Sleep(30);
                    ////获取轴IO状态
                    enginedata.PWMIOState = YKS2net.GetAIO(); //PWM控制输入输出状态
                    Thread.Sleep(30);
                    ////获取主板输入端口值
                    enginedata.MInput = YKS2net.GetInputs();
                    Thread.Sleep(30);
                    ////获取主板输出端口值
                    enginedata.MOutput = YKS2net.GetOutputs();
                    Thread.Sleep(30);
                }
                else if (Comm.NetHardCon == 0 && Comm.NetSoftCon == 0)//断开网络
                {
                    enginedata.CMDID = 99;
                }


            }
        }


    }
}
