using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Yungku.Common.IOCardS1;

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
        public struct AxisData
        {
            /// <summary>
            /// 移动距离
            /// </summary>
            public int Dist;
            public int StartSpeed;              //起始速度
            public int RunSpeed;                //运行速度
            public double Acce;                 //加速度
            public double Deceleration;         //减速度
            /// <summary>
            /// 当前位置
            /// </summary>
            public long Location;
            /// <summary>
            /// 目标位置
            /// </summary>
            public int Targetlocation;
            public int Direction;            //运动方向

            //回原点参数
            public int SecSpd;          //第二速度
            public int ReDire;            //回原点运动方向
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
            public int      DIP; //编码开关状态

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
        public class ComState
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
        private EngineData endata = new EngineData();
        private AxisData[] axisdata = new AxisData[3];

        //private YKS2CardNet YKS2net = new YKS2CardNet();
        private YKS1Card YKS1Com = new YKS1Card();

        public ComState Coms = new ComState();

        /// <summary>
        /// 配置-网络IP
        /// </summary>
        public string NetIP
        {
            set { Info.NetIP = value; }
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

        ///// <summary>
        ///// 设置串口硬链接状态
        ///// </summary>
        ///// <param name="COMHard"></param>
        //public void SetCOMHard(int COMHard)
        //{
        //    Comm.COMHardCon = COMHard;
        //}
        /// <summary>
        /// 获取串口硬链接状态
        /// </summary>
        public int COMHardCon
        {
            set { Coms.COMHardCon = value; }
            get { return Coms.COMHardCon; }
        }

        /// <summary>
        /// 获取串口软链接状态
        /// </summary>
        public int COMSoftCon
        {
            set { Coms.COMSoftCon = value; }
            get { return Coms.COMSoftCon; }
        }

        /// <summary>
        /// 获取网口硬链接状态
        /// </summary>
        public int NetHardCon
        {
            set { Coms.NetHardCon = value; }
            get { return Coms.NetHardCon; }
        }

        /// <summary>
        /// 获取网口软链接状态
        /// </summary>
        public int NetSoftCon
        {
            set { Coms.NetSoftCon = value; }
            get { return Coms.NetSoftCon; }
        }

        /// <summary>
        /// 记录心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetHeartCount(int Count)
        {
            endata.HeartCount = Count;
        }
        /// <summary>
        /// 获取心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetHeartCount()
        {
            return endata.HeartCount;
        }
        /// <summary>
        /// 记录串口心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetComHeartCount(int Count)
        {
            endata.ComHeartCount = Count;
        }
        /// <summary>
        /// 获取串口心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetComHeartCount()
        {
            return endata.ComHeartCount;
        }
        /// <summary>
        /// 记录网口心跳断开次数
        /// </summary>
        /// <param name="Count"></param>
        public void SetNetHeartCount(int Count)
        {
            endata.NetHeartCount = Count;
        }
        /// <summary>
        /// 获取网口心跳断开次数
        /// </summary>
        /// <returns></returns>
        public int GetNetHeartCount()
        {
            return endata.NetHeartCount;
        }


        /// <summary>
        /// 设置当前位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="loc"></param>
        public void SetLocation(int axis,long loc)
        {
            if(YKS1Com.ErrNum != loc) axisdata[axis].Location = loc;
        }
        /// <summary>
        /// 获取当前位置
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public long GetLocation(int axis)
        {
            return axisdata[axis].Location;
        }

        /// <summary>
        /// 设置轴状态
        /// </summary>
        /// <param name="state"></param>
        public void SetPWMState(int axis,int state)
        {
            axisdata[axis].PWMState = state;
        }
        /// <summary>
        /// 获取轴状态
        /// </summary>
        /// <returns></returns>
        public int GetPWMState(int axis)
        {
            return axisdata[axis].PWMState;
        }
        /// <summary>
        /// 设置轴IO
        /// </summary>
        /// <param name="state"></param>
        public void SetPWMIOState(int axis,int state)
        {
            axisdata[axis].PWMIOState = state;
        }
        /// <summary>
        /// 获取轴IO
        /// </summary>
        /// <returns></returns>
        public int GetPWMIOState(int axis)
        {
            return axisdata[axis].PWMIOState;
        }
        /// <summary>
        /// 配置开关量输入口
        /// </summary>
        /// <param name="state"></param>
        public int MInput
        {
            set { endata.MInput = value; }
            get { return endata.MInput; }
        }

        /// <summary>
        /// 设置开关量输出口
        /// </summary>
        /// <returns></returns>
        public int MOutput
        {
            set { endata.MOutput = value; }
            get { return endata.MOutput; }
        }

        /// <summary>
        /// 设置极限使能信号
        /// </summary>
        /// <param name="EnLimit"></param>
        public void SetSignEnLimit(int EnLimit)
        {
            axisdata[endata.Axis].SignEnLimit = (EnLimit == 1 ? true : false);
        }
        /// <summary>
        /// 获取极限使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnLimit()
        {
            return axisdata[endata.Axis].SignEnLimit;
        }
        /// <summary>
        /// 设置原点使能信号
        /// </summary>
        /// <param name="EnOrigin"></param>
        public void SetSignEnOrigin(int EnOrigin)
        {
            axisdata[endata.Axis].SignEnOrigin = (EnOrigin == 1 ? true : false);
        }
        /// <summary>
        /// 获取原点使能信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignEnOrigin()
        {
            return axisdata[endata.Axis].SignEnOrigin;
        }
        /// <summary>
        /// 设置反转极限信号
        /// </summary>
        /// <param name="ReLimit"></param>
        public void SetSignReLimit(int ReLimit)
        {
            axisdata[endata.Axis].SignReLimit = (ReLimit == 1 ? true : false);
        }
        /// <summary>
        /// 获取反转极限信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReLimit()
        {
            return axisdata[endata.Axis].SignReLimit;
        }
        /// <summary>
        /// 设置反转原点信号
        /// </summary>
        /// <param name="ReOrigin"></param>
        public void SetSignReOrigin(int ReOrigin)
        {
            axisdata[endata.Axis].SignReOrigin = (ReOrigin == 1 ? true : false);
        }
        /// <summary>
        /// 获取反转原点信号
        /// </summary>
        /// <returns></returns>
        public bool GetSignReOrigin()
        {
            return axisdata[endata.Axis].SignReOrigin;
        }

        public int Axis
        {
            set { endata.Axis = value; }
            get { return endata.Axis; }
        }
        ///// <summary>
        ///// 设置轴号
        ///// </summary>
        ///// <param name="axle"></param>
        //public void SetAxle(int axle)
        //{
        //    enginedata.Axis = axle;
        //}
        ///// <summary>
        ///// 获取轴号
        ///// </summary>
        ///// <returns></returns>
        //public int GetAxle()
        //{
        //    return enginedata.Axis;
        //}

        /// <summary>
        /// 设置移动距离
        /// </summary>
        /// <param name="distence"></param>
        public void SetDistence(int axis,int distence)
        {
            axisdata[axis].Dist = distence;
        }
        /// <summary>
        /// 获取移动距离
        /// </summary>
        /// <returns></returns>
        public int GetDistence(int axis)
        {
            return axisdata[axis].Dist;
        }

        /// <summary>
        /// 设置目标位置
        /// </summary>
        /// <param name="Targetloca"></param>
        public void SetTargloca(int axis,int Targetloca)
        {
            axisdata[axis].Targetlocation = Targetloca;
        }
        /// <summary>
        /// 获取目标位置
        /// </summary>
        /// <returns></returns>
        public int GetTargloca(int axis)
        {
            return axisdata[axis].Targetlocation;
        }
        /// <summary>
        /// 设置起始速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetStartSpd(int axis,int speed)
        {
            axisdata[axis].StartSpeed = speed;
        }
        /// <summary>
        /// 获取起始速度
        /// </summary>
        /// <returns></returns>
        public int GetStartSpd(int axis)
        {
            return axisdata[axis].StartSpeed;
        }
        /// <summary>
        /// 设置运行速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetRunSpd(int axis,int speed)
        {
            axisdata[axis].RunSpeed = speed;
        }
        /// <summary>
        /// 获取运行速度
        /// </summary>
        /// <returns></returns>
        public int GetRunSpd(int axis)
        {
            return axisdata[axis].RunSpeed;
        }
        /// <summary>
        /// 设置第二速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetSecSpd(int axis,int speed)
        {
            axisdata[axis].SecSpd = speed;
        }
        /// <summary>
        /// 获取第二速度
        /// </summary>
        /// <returns></returns>
        public int GetSecSpd(int axis)
        {
            return axisdata[axis].SecSpd;
        }
        /// <summary>
        /// 设置加速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetAcce(int axis,double aspd)
        {
            axisdata[axis].Acce = aspd;
        }
        /// <summary>
        /// 获取加速度
        /// </summary>
        /// <returns></returns>
        public double GetAcce(int axis)
        {
            return axisdata[axis].Acce;
        }
        /// <summary>
        /// 设置减速度
        /// </summary>
        /// <param name="aspd"></param>
        public void SetDece(int axis,double aspd)
        {
            axisdata[axis].Deceleration = aspd;
        }
        /// <summary>
        /// 获取减速度
        /// </summary>
        /// <returns></returns>
        public double GetDece(int axis)
        {
            return axisdata[axis].Deceleration;
        }
        /// <summary>
        /// 设置回原点方向
        /// </summary>
        /// <param name="Dir"></param>
        public void SetReturnDire(int axis,int Dir)
        {
            axisdata[axis].ReDire = Dir;
        }
        /// <summary>
        /// 获取回原点方向
        /// </summary>
        /// <returns></returns>
        public int GetReturnDire(int axis)
        {
            return axisdata[axis].ReDire;
        }
        /// <summary>
        /// 设置运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetRunMode(int axis,int Mode)
        {
            axisdata[axis].RunMode = Mode;
        }
        /// <summary>
        /// 获取运动模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetRunMode(int axis)
        {
            return axisdata[axis].RunMode;
        }
        /// <summary>
        /// 设置运动方向
        /// </summary>
        /// <param name="Mode"></param>
        public void SetDire(int axis,int Dir)
        {
            axisdata[axis].Direction = Dir;
        }
        /// <summary>
        /// 获取运动方向
        /// </summary>
        /// <param name="Mode"></param>
        public int GetDire(int axis)
        {
            return axisdata[axis].Direction;
        }
        /// <summary>
        /// 设置停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public void SetStopRunMode(int StopMode)
        {
            axisdata[endata.Axis].StopRunMode = StopMode;
        }
        /// <summary>
        /// 获取停止模式
        /// </summary>
        /// <param name="Mode"></param>
        public int GetStopRunMode()
        {
            return axisdata[endata.Axis].StopRunMode;
        }
        /// <summary>
        /// 设置演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public void SetShowMode(int axis,int StopMode)
        {
            axisdata[axis].ShowMode = StopMode;
        }
        /// <summary>
        /// 获取演习模式 0-停止 1-开始
        /// </summary>
        /// <param name="Mode"></param>
        public int GetShowMode(int axis)
        {
            return axisdata[axis].ShowMode;
        }
        /// <summary>
        /// 设置主板或者扩展卡
        /// </summary>
        /// <param name="Mode"></param>
        public void SetCardID(int ID)
        {
            endata.CardID = ID;
        }
        /// <summary>
        /// 获取主板或者扩展卡
        /// </summary>
        /// <param name="Mode"></param>
        public int GetCardID()
        {
            return endata.CardID;
        }
        /// <summary>
        /// 设置固件信息读取次数
        /// </summary>
        /// <param name="num"></param>
        public void SetFirstNum(int num)
        {
            endata.FirstNum = num;
        }
        /// <summary>
        /// 获取固件信息读取次数
        /// </summary>
        /// <param></param>
        public int GetFirstNum()
        {
            return endata.FirstNum;
        }
        /// <summary>
        /// 设置主板名称
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            endata.Name = name;
        }

        /// <summary>
        /// 获取主板名称
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return endata.Name;
        }
        /// <summary>
        /// 设置固件编号
        /// </summary>
        /// <param name="name"></param>
        public void SetSN(int sn)
        {
            endata.SN = sn;
        }

        /// <summary>
        /// 获取固件编号
        /// </summary>
        /// <returns></returns>
        public int GetSN()
        {
            return endata.SN;
        }
        /// <summary>
        /// 配置编码开关状态
        /// </summary>
        /// <param name="name"></param>
        public int DIP
        {
            set { endata.DIP = value; }
            get { return endata.DIP; }
        }

        /// <summary>
        /// 设置版本信息
        /// </summary>
        /// <param name="name"></param>
        public void SetVer_Info(string info)
        {
            endata.Ver_Info = info;
        }

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        public string GetVer_Info()
        {
            return endata.Ver_Info;
        }



        /// <summary>
        /// 线程处理
        /// </summary>
        public void ThreadMain()
        {
            while (true)
            {

                    Thread.Sleep(30);


                if (Info.VerUper == 2)
                {
                    if (Coms.COMHardCon == 1)
                    {










                    }



                }










                //    if (Comm.NetHardCon == 1 && Comm.NetSoftCon == 1)  //连接网络
                //    {
                //        //心跳检测
                //        if (!YKS2net.IsExists())
                //        {
                //            //enginedata.HeartCount++;
                //            //if (enginedata.HeartCount > 3)
                //            //{
                //            //    enginedata.HeartCount = 0;
                //            //    Comm.NetSoftCon = 0;

                //            //}
                //            continue;
                //        }
                //        else
                //        {
                //            //enginedata.HeartCount = 0;
                //            //enginedata.NetHeartCount = 0;

                //        }

                //        if (enginedata.FirstNum == 0)  //读取固件信息
                //        {
                //            enginedata.FirstNum = 1;

                //            //获取主板名称
                //            enginedata.Name = YKS2net.GetCardName();
                //            //获取固件编号
                //            enginedata.SN = YKS2net.GetSN();
                //            //获取编码开关状态
                //            enginedata.switchID = YKS2net.GetDipSwitch();

                //            ////获取版本信息
                //            enginedata.Ver_Info = YKS2net.GetVerInfo();

                //        }


                //        //获取轴当前位置
                //        axledata[enginedata.Axle].Location = YKS2net.GetPosition(enginedata.Axle);

                //        //获取轴IO状态
                //        axledata[enginedata.Axle].PWMIOState = YKS2net.GetAIO(); //PWM控制输入输出状态


                //        if (enginedata.CardID == 1)
                //        {
                //            //获取扩展板输入端口值
                //            enginedata.MInput = YKS2net.GetInputsEx();
                //            //获取扩展板输出端口值
                //            enginedata.MOutput = YKS2net.GetOutputsEx();

                //        }
                //        else
                //        {
                //            //获取主板输入端口值
                //            enginedata.MInput = YKS2net.GetInputs();
                //            //获取主板输出端口值
                //            enginedata.MOutput = YKS2net.GetOutputs();
                //        }




                //       //获取轴状态
                //       axledata[enginedata.Axle].PWMState = YKS2net.GetAxisStatus(enginedata.Axle);


                //        if (axledata[enginedata.Axle].ShowMode == 1) 
                //        {
                //            if ((axledata[enginedata.Axle].PWMState >> 1 & 0x1) == 0)
                //            {
                //                int Dis = axledata[enginedata.Axle].Direction > 0 ? axledata[enginedata.Axle].Distence : -axledata[enginedata.Axle].Distence;
                //                YKS2net.RltMove(enginedata.Axle, Dis, axledata[enginedata.Axle].StartSpeed, axledata[enginedata.Axle].RunSpeed, axledata[enginedata.Axle].Acceleration, axledata[enginedata.Axle].Deceleration);
                //                if (axledata[enginedata.Axle].Direction == 0)
                //                {
                //                    axledata[enginedata.Axle].Direction = 1;
                //                }
                //                else
                //                {
                //                    axledata[enginedata.Axle].Direction = 0;
                //                }
                //            }
                //        }

                //    }
                //    else if (Comm.NetHardCon == 0 && Comm.NetSoftCon == 0)//断开网络
                //    {
                //    }
            }
        }

   }
}
