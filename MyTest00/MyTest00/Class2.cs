using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest00
{
    public partial class CMD : Form1
    {
        /// <summary>
        /// 1-相对移动
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_RelativeMovement(EngineData Data)
        {
            ushort Axle             = Data.Axle;                        //轴编号
            int Distence            = 0;                                //移动距离
            int StartSpeed          = (int)Data.StartSpeed;                  //起始速度
            int RunSpeed            = (int)Data.RunSpeed;                    //运行速度
            int Acceleration        = (int)((RunSpeed- StartSpeed)/Data.Acceleration/1000);                //加速度 ms
            int Deceleration        = Acceleration;                //减速度 -同加速度

            if (Data.Direction == 1) Distence = Data.Distence;          //确定移动方向
            else Distence = -Data.Distence;                    

            string SendData = string.Format("move {0},{1},{2},{3},{4},{5}", Axle, Distence, StartSpeed, RunSpeed, Acceleration, Deceleration);
            return SendData;
        }

        /// <summary>
        /// 2-绝对移动
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_AbsoluteMovement(EngineData Data)
        {
            ushort Axle             = Data.Axle;                        //轴编号
            int Targetlocation   = Data.Targetlocation;              //目标位置
            int StartSpeed       = (int)Data.StartSpeed;                  //起始速度
            int RunSpeed         = (int)Data.RunSpeed;                    //运行速度
            int Acceleration     = (int)((RunSpeed - StartSpeed) / Data.Acceleration /1000);                //加速度
            int Deceleration     = Acceleration;                //减速度 -同加速度

            string SendData = string.Format("moveto {0},{1},{2},{3},{4},{5}", Axle, Targetlocation, StartSpeed, RunSpeed, Acceleration, Deceleration);
            return SendData;
        }

        /// <summary>
        /// 3-停止
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_StopRun(EngineData Data)
        {
            ushort Axle             = Data.Axle;                        //轴编号
            double StopRunMode      = Data.StopRunMode;                 //停止模式

            string SendData = string.Format("stop {0},{1}", Axle, StopRunMode);
            return SendData;
        }
        /// <summary>
        /// 4-回原点
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GoHome(EngineData Data)
        {
            ushort Axle             = Data.Axle;                        //轴编号
            int StartSpeed       = (int)Data.StartSpeed;                  //起始速度
            int RunSpeed         = (int)Data.RunSpeed;                    //运行速度
            int Acceleration     = (int)((RunSpeed - StartSpeed) / Data.Acceleration / 1000);                //加速度
            int Deceleration     = Acceleration;                //减速度 -同加速度
            int homeMode         = Data.Reserve0;                    //预留参数
            int homeDir          = Data.Direction;                   //回原点方向
            int SecondSpeed      = Data.SecondSpeed;                 //回原点第二速度
            int offset           = Data.Reserve0;                    //预留参数

            string SendData = string.Format("home {0},{1},{2},{3},{4},{5},{6},{7},{8}", Axle, StartSpeed, RunSpeed, Acceleration, Deceleration, homeMode, homeDir, SecondSpeed, offset);
            return SendData;
        }

        /// <summary>
        /// 5-获取轴状态
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetAxleState(EngineData Data)
        {
            ushort Axle = Data.Axle;                                    //轴编号
            string SendData = string.Format("getst {0}", Axle);
            return SendData;
        }
        /// <summary>
        /// 6-获取轴位置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetAxlePos(EngineData Data)
        {
            ushort Axle = Data.Axle;                                    //轴编号
            string SendData = string.Format("getpos {0}", Axle);
            return SendData;
        }
        /// <summary>
        /// 7-设置主板输出端口
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetMOutput(EngineData Data)
        {
            int Output = Data.MOutput;                                  //主板输出                       
            string SendData = string.Format("setout {0}", Output);
            return SendData;
        }
        /// <summary>
        /// 8-获取主板输出端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMOutput()
        {
            string SendData = string.Format("getout");
            return SendData;
        }
        /// <summary>
        /// 9-获取主板输入端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMInput()
        {
            string SendData = string.Format("getin");
            return SendData;
        }
        /// <summary>
        /// 10-设置目标位置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetTargetlocation(EngineData Data)
        {
            ushort Axle = Data.Axle;                                //轴编号
            int Targetlocation = Data.Targetlocation;            //目标位置
            string SendData = string.Format("setpos {0},{1}", Axle, Targetlocation);
            return SendData;
        }
        /// <summary>
        /// 11-获取编码开关状态
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_StateCodingswitch()
        {
            string SendData = string.Format("getds");
            return SendData;
        }
        /// <summary>
        /// 12-心跳测试
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_Heart()
        {
            string SendData = string.Format("H");
            return SendData;
        }

        /// <summary>
        /// 13-获取版本
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetVersion()
        {
            string SendData = string.Format("getver");
            return SendData;
        }
        /// <summary>
        /// 14-获取串口号
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetSerial()
        {
            string SendData = string.Format("getsn");
            return SendData;
        }
        /// <summary>
        /// 15-获取轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetIOIndex(int chose)
        {
            string SendData = string.Format("getaio {0}", chose);
            return SendData;
        }

        /// <summary>
        /// 16-设置轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetIOIndex(int chose)
        {
            string SendData = string.Format("setaio {0},ioState", chose);
            return SendData;
        }

        /// <summary>
        /// 17-权限设置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetGrade(EngineData Data)
        {
            ushort Axle = Data.Axle;                                //轴编号
            string SendData = string.Format("setlmt {0},{1},{2},{3},{4}", Axle);
            return SendData;
        }
        /// <summary>
        /// 18-获取卡名称
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetName()
        {
            string SendData = string.Format("getname");
            return SendData;
        }

        //**************************** 扩展卡 **********************************************
        /// <summary>
        /// 扩展卡1-相对移动
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_RelativeMovementExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            ushort Axle = Data.Axle;                                //轴编号
            int Distence = Data.Distence;                        //移动距离
            int StartSpeed = (int)Data.StartSpeed;                    //起始速度
            int RunSpeed = (int)Data.RunSpeed;                        //运行速度
            int Acceleration = (int)((RunSpeed - StartSpeed) / Data.Acceleration / 1000);                //加速度
            int Deceleration = Acceleration;                //减速度 -同加速度

            string SendData = string.Format("nmove {0},{1},{2},{3},{4},{5},{6}", boardIP, Axle, Distence, StartSpeed, RunSpeed, Acceleration, Deceleration);
            return SendData;
        }

        /// <summary>
        /// 扩展卡2-绝对移动
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_AbsoluteMovementExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            ushort Axle = Data.Axle;                                //轴编号
            int Targetlocation = Data.Targetlocation;            //目标位置
            int StartSpeed = (int)Data.StartSpeed;                    //起始速度
            int RunSpeed = (int)Data.RunSpeed;                        //运行速度
            int Acceleration = (int)((RunSpeed - StartSpeed) / Data.Acceleration / 1000);                //加速度
            int Deceleration = Acceleration;                //减速度 -同加速度

            string SendData = string.Format("nmoveto {0},{1},{2},{3},{4},{5},{6}", boardIP, Axle, Targetlocation, StartSpeed, RunSpeed, Acceleration, Deceleration);
            return SendData;
        }

        /// <summary>
        /// 扩展卡3-停止
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_StopRunExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            ushort Axle = Data.Axle;                                //轴编号
            int StopRunMode = Data.StopRunMode;                  //停止模式

            string SendData = string.Format("nstop {0},{1},{2}", boardIP, Axle, StopRunMode);
            return SendData;
        }
        /// <summary>
        /// 扩展卡4-回原点
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GoHomeExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            ushort Axle = Data.Axle;                        //轴编号
            int StartSpeed = (int)Data.StartSpeed;                  //起始速度
            int RunSpeed = (int)Data.RunSpeed;                    //运行速度
            int Acceleration = (int)((RunSpeed - StartSpeed) / Data.Acceleration / 1000);                //加速度
            int Deceleration = Acceleration;                //减速度 -同加速度
            int homeMode = Data.Reserve0;                    //预留参数
            int homeDir = Data.Direction;                   //回原点方向
            int SecondSpeed = Data.SecondSpeed;                 //回原点第二速度
            int offset = Data.Reserve0;                    //预留参数

            string SendData = string.Format("nhome {0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", boardIP, Axle, StartSpeed, RunSpeed, Acceleration, Deceleration, homeMode, homeDir, SecondSpeed, offset);
            return SendData;
        }

        /// <summary>
        /// 扩展卡5-获取轴状态
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetAxleStateExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            ushort Axle = Data.Axle;                                    //轴编号
            string SendData = string.Format("ngetst {0},{1}", boardIP, Axle);
            return SendData;
        }
        /// <summary>
        /// 扩展卡6-获取轴位置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetAxlePosExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            ushort Axle = Data.Axle;                                    //轴编号
            string SendData = string.Format("ngetpos {0},{1}", boardIP, Axle);
            return SendData;
        }
        /// <summary>
        /// 扩展卡7-设置主板输出端口
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetMOutputExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            int Output = Data.MOutput;                               //位号0~8
            string SendData = string.Format("nsetout {0},{1}", boardIP, Output);
            return SendData;
        }
        /// <summary>
        /// 扩展卡8-获取主板输出端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMOutputExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("ngetout {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡9-获取主板输入端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMInputExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("ngetin {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡10-设置目标位置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetTargetlocationExtern(EngineData Data)
        {
            ushort Axle = Data.Axle;                                //轴编号
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            int Targetlocation = Data.Targetlocation;            //目标位置
            string SendData = string.Format("nsetpos {0},{1},{2}", boardIP ,Axle, Targetlocation);
            return SendData;
        }
        /// <summary>
        /// 扩展卡11-获取编码开关状态
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_StateCodingswitchExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("ngetds {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡12-心跳测试
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_HeartExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("H");
            return SendData;
        }

        /// <summary>
        /// 扩展卡13-获取版本
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetVersionExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("ngetver {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡14-获取串口号
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetSerialExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("ngetsn {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡15-获取轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetIOIndexExtern(EngineData Data, int chose)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("ngetaio {0},{1}", boardIP, chose);
            return SendData;
        }

        /// <summary>
        /// 扩展卡16-设置轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetIOIndexExtern(EngineData Data, int chose)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("nsetaio {0},{1},ioState", boardIP, chose);
            return SendData;
        }

        /// <summary>
        /// 扩展卡17-权限设置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetGradeExtern(EngineData Data)
        {
            ushort Axle = Data.Axle;                                //轴编号
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("nsetlmt {0},{1},{2},{3},{4},{5}", boardIP,Axle);
            return SendData;
        }
        /// <summary>
        /// 扩展卡18-获取卡名称
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetNameExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format("ngetname {0}", boardIP);
            return SendData;
        }

    }
}
