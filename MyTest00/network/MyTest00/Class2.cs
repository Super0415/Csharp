using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest00
{
    public partial class CMD : Form1
    {

        public enum CMDTopNum
        {
           //主板指令头
            move = 0,
            moveto,
            stop,
            home,
            getst,
            getpos,
            setout,
            getout,
            getin,
            setpos,
            getds,
            H,
            getver,
            getsn,
            getaio,
            setaio,
            setlmt,
            getname,
            //扩展板指令头
            nmove,
            nmoveto,
            nstop,
            nhome,
            ngetst,
            ngetpos,
            nsetout,
            ngetout,
            ngetin,
            nsetpos,
            ngetds,
            nH,
            ngetver,
            ngetsn,
            ngetaio,
            nsetaio,
            nsetlmt,
            ngetname,
            TEST0,
            TEST1
        }
        public static string[] CMDTop = 
        {
            //主板指令头
            "move",
            "moveto",
            "stop",
            "home",
            "getst",
            "getpos",
            "setout",
            "getout",
            "getin",
            "setpos",
            "getds",
            "H",
            "getver",
            "getsn",
            "getaio",
            "setaio",
            "setlmt",
            "getname",
            //扩展板指令头
            "nmove",
            "nmoveto",
            "nstop",
            "nhome",
            "ngetst",
            "ngetpos",
            "nsetout",
            "ngetout",
            "ngetin",
            "nsetpos",
            "ngetds",
            "H",
            "ngetver",
            "ngetsn",
            "ngetaio",
            "nsetaio",
            "nsetlmt",
            "ngetname",
            "TEST0",
            "TEST1"
        };
        enum CMDStateNum
        {   
            OK = 0,
            BUSY,
            ERROR,
            Invalide,
        }
        public static string[] CMDStateEN =
        {
            "OK",
            "BUSY",
            "ERROR",
            "Invalide paramters!",
        };
        public static string[] CMDStateCN =
{
            "操作成功",
            "正忙",
            "操作失败",
            "无效参数",
        };

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

            if (Data.SetDirection == 1) Distence = Data.Distence;          //确定移动方向
            else Distence = -Data.Distence;                    

            string SendData = string.Format(CMDTop[(int)CMDTopNum.move] + " {0},{1},{2},{3},{4},{5}", Axle, Distence, StartSpeed, RunSpeed, Acceleration, Deceleration);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-1-相对移动
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns>数据状态</returns>
        public static int HandleCmd_RelativeMovement(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
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

            string SendData = string.Format(CMDTop[(int)CMDTopNum.moveto] + " {0},{1},{2},{3},{4},{5}", Axle, Targetlocation, StartSpeed, RunSpeed, Acceleration, Deceleration);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-2-绝对移动
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns>数据状态</returns>
        public static int HandleCmd_AbsoluteMovement(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
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

            string SendData = string.Format(CMDTop[(int)CMDTopNum.stop] + " {0},{1}", Axle, StopRunMode);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-3-停止
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns>数据状态</returns>
        public static int HandleCmd_StopRun(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
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
            int homeDir          = Data.ReturnDirection;                   //回原点方向
            int SecondSpeed      = Data.SecondSpeed;                 //回原点第二速度
            int offset           = Data.Reserve0;                    //预留参数

            string SendData = string.Format(CMDTop[(int)CMDTopNum.home] + " {0},{1},{2},{3},{4},{5},{6},{7},{8}", Axle, StartSpeed, RunSpeed, Acceleration, Deceleration, homeMode, homeDir, SecondSpeed, offset);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-4-回原点
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns>数据状态</returns>
        public static int HandleCmd_GoHome(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 5-获取轴状态
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetAxleState(EngineData Data)
        {
            ushort Axle = Data.Axle;                                    //轴编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getst] + " {0}", Axle);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-5-获取轴状态
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns>数据状态</returns>
        public static int HandleCmd_GetAxleState(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.LightState = int.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 6-获取轴位置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetAxlePos(EngineData Data)
        {
            ushort Axle = Data.Axle;                                    //轴编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getpos] + " {0}", Axle);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-6-获取轴位置
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_GetAxlePos(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^(\-|\+)?\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.Location = int.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 7-设置主板输出端口
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetMOutput(EngineData Data)
        {
            int Output = Data.MOutput;                                  //主板输出                       
            string SendData = string.Format(CMDTop[(int)CMDTopNum.setout] + " {0}", Output);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-7-设置主板输出端口
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_SetMOutput(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.MOutput = int.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 8-获取主板输出端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMOutput()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getout]);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-8-获取主板输出端口值
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_GetMOutput(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.MOutput = int.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 9-获取主板输入端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMInput()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getin]);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-9-获取主板输入端口值
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_GetMInput(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.MInput = int.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
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
            string SendData = string.Format(CMDTop[(int)CMDTopNum.setpos] + " {0},{1}", Axle, Targetlocation);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-10-设置目标位置
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_SetTargetlocation(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 11-获取编码开关状态
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_StateCodingswitch()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getds]);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-11-获取编码开关状态
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_StateCodingswitch(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.StateCodingswitch = ushort.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 12-心跳测试
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_Heart()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.H]);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-12-心跳测试
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_Heart(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    if (i == 0)
                    {
                        enginedata.HeartCount = 0;
                    }
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 13-获取版本
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetVersion()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getver]);
            return SendData;
        }
        /// <summary>
        /// 14-获取串口号
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetSerial()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getsn]);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-14-获取串口号
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_GetSerial(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.SN = int.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }

        /// <summary>
        /// 15-获取轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetIOIndex()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getaio] + " {0}", 15);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-15-获取轴IO
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_GetIOIndex(string cmddata)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(cmddata))
            {
                enginedata.PWMState = int.Parse(cmddata);
                return 0;
            }

            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 16-设置轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetIOIndex()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.setaio] + " {0},ioState");
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-16-设置轴IO
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_SetIOIndex(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 17-权限设置
        /// </summary>
        /// <param name="Data">数据集</param>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetGrade(EngineData Data)
        {
            ushort Axle = Data.Axle;                                //轴编号
            ushort EnLimit = Data.SignEnLimit;
            ushort EnOrigin = Data.SignEnOrigin;
            ushort ReversalLimit = Data.SignReversalLimit;
            ushort ReversalOrigin = Data.SignReversalOrigin;

            string SendData = string.Format(CMDTop[(int)CMDTopNum.setlmt] + " {0},{1},{2},{3},{4}", Axle, EnLimit, EnOrigin, ReversalLimit, ReversalOrigin);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-17-权限设置
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_SetGrade(string cmddata)
        {
            for (int i = 0; i < CMDStateEN.Length; i++)
            {
                if (string.Equals(cmddata, CMDStateEN[i]))
                {
                    return i;
                }
            }
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
        }
        /// <summary>
        /// 18-获取卡名称
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetName()
        {
            string SendData = string.Format(CMDTop[(int)CMDTopNum.getname]);
            return SendData;
        }
        /// <summary>
        /// 返回数据处理-18-获取卡名称
        /// </summary>
        /// <param name="cmddata">返回数据</param>
        /// <returns></returns>
        public static int HandleCmd_GetName(string cmddata)
        {
            if (cmddata != null)
            {
                enginedata.Name = cmddata;
                return (int)CMDStateNum.OK;       //返回数据正常
            }     
            return (int)CMDStateNum.Invalide;       //返回无效数据状态
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

            string SendData = string.Format(CMDTop[(int)CMDTopNum.nmove] + " {0},{1},{2},{3},{4},{5},{6}", boardIP, Axle, Distence, StartSpeed, RunSpeed, Acceleration, Deceleration);
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

            string SendData = string.Format(CMDTop[(int)CMDTopNum.nmoveto] + " {0},{1},{2},{3},{4},{5},{6}", boardIP, Axle, Targetlocation, StartSpeed, RunSpeed, Acceleration, Deceleration);
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

            string SendData = string.Format(CMDTop[(int)CMDTopNum.nstop] + " {0},{1},{2}", boardIP, Axle, StopRunMode);
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
            int homeDir = Data.ReturnDirection;                   //回原点方向
            int SecondSpeed = Data.SecondSpeed;                 //回原点第二速度
            int offset = Data.Reserve0;                    //预留参数

            string SendData = string.Format(CMDTop[(int)CMDTopNum.nhome] + " {0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", boardIP, Axle, StartSpeed, RunSpeed, Acceleration, Deceleration, homeMode, homeDir, SecondSpeed, offset);
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
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetst] + " {0},{1}", boardIP, Axle);
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
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetpos] + " {0},{1}", boardIP, Axle);
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
            string SendData = string.Format(CMDTop[(int)CMDTopNum.nsetout] + " {0},{1}", boardIP, Output);
            return SendData;
        }
        /// <summary>
        /// 扩展卡8-获取主板输出端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMOutputExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetout] + " {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡9-获取主板输入端口值
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetMInputExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetin] + " {0}", boardIP);
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
            string SendData = string.Format(CMDTop[(int)CMDTopNum.nsetpos] + " {0},{1},{2}", boardIP ,Axle, Targetlocation);
            return SendData;
        }
        /// <summary>
        /// 扩展卡11-获取编码开关状态
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_StateCodingswitchExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetds] + " {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡12-心跳测试
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_HeartExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.nH]);
            return SendData;
        }

        /// <summary>
        /// 扩展卡13-获取版本
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetVersionExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetver] + " {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡14-获取串口号
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetSerialExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetsn] + " {0}", boardIP);
            return SendData;
        }
        /// <summary>
        /// 扩展卡15-获取轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetIOIndexExtern(EngineData Data, int chose)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetaio] + " {0},{1}", boardIP, chose);
            return SendData;
        }

        /// <summary>
        /// 扩展卡16-设置轴IO
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_SetIOIndexExtern(EngineData Data, int chose)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.nsetaio] + " {0},{1},ioState", boardIP, chose);
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
            string SendData = string.Format(CMDTop[(int)CMDTopNum.nsetlmt] + " {0},{1},{2},{3},{4},{5}", boardIP,Axle);
            return SendData;
        }
        /// <summary>
        /// 扩展卡18-获取卡名称
        /// </summary>
        /// <returns>控制命令</returns>
        public static string ControlCmd_GetNameExtern(EngineData Data)
        {
            ushort boardIP = Data.boardIP;                          //扩展卡编号
            string SendData = string.Format(CMDTop[(int)CMDTopNum.ngetname] + " {0}", boardIP);
            return SendData;
        }

        public static int GetInfo_CmdNum(string cmdd)
        {

            for (int i = 0; i < CMDTop.Length; i++)
            {
                if (string.Equals(cmdd, CMDTop[i]))
                {
                    return i;
                }
            }
            return 0xFF;
        }

        public static int Group_CmdReceivedData(string cmddata, int cmdnum)
        {
            int result = 0;
            switch (cmdnum)
            {
                case 0: //相对移动
                    result = HandleCmd_RelativeMovement(cmddata);
                    break;
                case 1: //绝对移动
                    result = HandleCmd_AbsoluteMovement(cmddata);
                    break;
                case 2: //停止
                    result = HandleCmd_StopRun(cmddata);
                    break;
                case 3: //回原点
                    result = HandleCmd_GoHome(cmddata);
                    break;
                case 4://获取轴状态
                    result = HandleCmd_GetAxleState(cmddata);
                    break;
                case 5: //获取轴位置
                    result = HandleCmd_GetAxlePos(cmddata);
                    break;
                case 6: //设置主板输出端口
                    result = HandleCmd_SetMOutput(cmddata);
                    break;
                case 7: //获取主板输出端口值
                    result = HandleCmd_GetMOutput(cmddata);
                    break;
                case 8: //获取主板输入端口值
                    result = HandleCmd_GetMInput(cmddata);
                    break;
                case 9: //设置目标位置
                    result = HandleCmd_SetTargetlocation(cmddata);
                    break;
                case 10://获取编码开关状态
                    result = HandleCmd_StateCodingswitch(cmddata);
                    break;
                case 11://心跳测试
                    result = HandleCmd_Heart(cmddata);
                    break;
                case 12:
                    break;
                case 13://获取串口号
                    result = HandleCmd_GetSerial(cmddata);
                    break;
                case 14:
                    result = HandleCmd_GetIOIndex(cmddata);
                    break;
                case 15:
                    result = HandleCmd_SetIOIndex(cmddata);
                    break;
                case 16:
                    result = HandleCmd_SetGrade(cmddata);
                    break;
                case 17:
                    result = HandleCmd_GetName(cmddata);
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
                case 24:
                    break;
                case 25:
                    break;
                case 26:
                    break;
                case 27:
                    break;
                case 28:
                    break;
                case 29:
                    break;
                case 30:
                    break;
                case 31:
                    break;
                case 32:
                    break;
                case 33:
                    break;
                case 34:
                    break;
                case 35:
                    break;
                case 36:
                    break;
                case 37:
                    break;
                case 38:
                    break;
                default:
                    break;

            }
            return result;

        }

        public static string Group_CmdSendData(int cmdnum)
        {
            string result = null;
            switch (cmdnum)
            {
                case 0: //相对移动
                    result = ControlCmd_RelativeMovement(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 1: //绝对移动
                    result = ControlCmd_AbsoluteMovement(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 2: //停止
                    result = ControlCmd_StopRun(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 3: //回原点
                    result = ControlCmd_GoHome(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 4: //获取轴IO状态
                    result = ControlCmd_GetAxleState(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.getaio;   //获取轴IO
                    break;
                case 5: //获取轴位置
                    result = ControlCmd_GetAxlePos(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.getst;    //获取轴状态
                    break;
                case 6: //设置主板输出端口
                    result = ControlCmd_SetMOutput(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 7: //获取主板输出端口值
                    result = ControlCmd_GetMOutput();
                    if (enginedata.ShowMode == 1)
                    {
                        enginedata.CMDID = (int)CMDTopNum.move;
                        if (enginedata.SetDirection == 1)
                            enginedata.SetDirection = 0;
                        else
                            enginedata.SetDirection = 1; 
                    }
                    else enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 8: //获取主板输入端口值
                    result = ControlCmd_GetMInput();
                    enginedata.CMDID = (int)CMDTopNum.getout;   //获取主板输出端口值
                    break;
                case 9: //设置目标位置
                    result = ControlCmd_SetTargetlocation(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 10: //获取编码开关状态
                    result = ControlCmd_StateCodingswitch();
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 11: //心跳测试
                    result = ControlCmd_Heart();
                    enginedata.CMDID = (int)CMDTopNum.getpos;   //获取轴位置
                    break;
                case 12: //获取版本
                    result = ControlCmd_GetVersion();
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 13: //获取串口号
                    result = ControlCmd_GetSerial();
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 14: //获取轴IO
                    result = ControlCmd_GetIOIndex();
                    enginedata.CMDID = (int)CMDTopNum.getin;    //获取主板输入端口值
                    break;
                case 15: //设置轴IO       //暂不完善
                    result = ControlCmd_SetIOIndex();
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 16: //权限设置
                    result = ControlCmd_SetGrade(enginedata);
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                case 17: //获取卡名称
                    result = ControlCmd_GetName();
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;
                default:    //错误码
                    enginedata.CMDID = (int)CMDTopNum.H;
                    break;

            }
            return result;
        }

    }
}
