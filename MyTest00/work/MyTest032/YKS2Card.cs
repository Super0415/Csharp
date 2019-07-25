using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Yungku.Common.IOCard
{
	public class YKS2Card
	{
		enum AxisStatus
		{
			Alm = 0,	//报警状态
			Busy,		//忙状态
			Err,		//错误状态
			Lmp,		//正极限信号
			Org,		//原点信号
			Lmn,		//负极限信号
			Homing,		//回原点状态中
			LmtEn,		//是否使用极限信号
			OrgEn,		//是否使用原点信号
			LmtRvs,		//是否反转极限信号
			OrgRvs,		//是否反转原点信号
		};

		private SerialPort sport = new SerialPort();
		private object syncRoot = new object();

		private int port = 1;
		/// <summary>
		/// 设置或获取通讯端口
		/// </summary>
		public int Port
		{
			get { return port; }
			set { port = value; }
		}

        private int baudRate = 115200;
        /// <summary>
        /// 设置或获取波特率
        /// </summary>
        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

		private int timeout = 1000;
		/// <summary>
		/// 设置或获取读写超时时间
		/// 修改此值后必须重新调用Open才会生效
		/// </summary>
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		//private int boardId = 0;

		//public int BoardId
		//{
		//	get { return boardId; }
		//	set { boardId = value; }
		//}

 
        public bool IsOpen()
        {
            lock (syncRoot)
            {
                if (sport.IsOpen)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 打开控制卡
        /// </summary>
        public void Open()
		{
			lock (syncRoot)
			{
				if (sport.IsOpen)
					sport.Close();

				sport.PortName = "COM" + Port.ToString();
				sport.BaudRate = BaudRate;
				sport.StopBits = StopBits.One;
				sport.DataBits = 8;

				sport.ReadTimeout = Timeout;
				sport.WriteTimeout = Timeout;
				sport.Open();
                sport.ReadExisting();
                
			}
		}

		/// <summary>
		/// 关闭卡
		/// </summary>
		public void Close()
		{
			lock (syncRoot)
			{
				sport.Close();
			}
		}

		/// <summary>
		/// 执行一个命令并返回一个结果
		/// </summary>
		/// <param name="cmd"></param>
		/// <returns></returns>
		protected string ExecuteCommand(string cmd)
		{
			lock (syncRoot)
			{
                try
                {
                    sport.WriteLine(cmd);

                    string ret = sport.ReadLine();
                    if (ret.Equals(cmd))
                    {
                        ret = sport.ReadLine();
                        return ret.Trim().ToUpper();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch
                {
                return string.Empty;
                }

            }
		}
        /// <summary>
        /// 查询版本信息
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string ExecuteVer(string cmd)
        {
            lock (syncRoot)
            {
                try
                {
                    sport.WriteLine(cmd);

                    string ret = sport.ReadLine();
                    if (ret.Equals(cmd))
                    {
                        string Ver_Co = sport.ReadLine();
                        string Ver_Web = sport.ReadLine();
                        string Ver_Card = sport.ReadLine();
                        string Ver_UD = sport.ReadLine();
                        string Ver_SPT = sport.ReadLine();

                        return Ver_Co.Trim() + "\n" + Ver_Web.Trim() + "\n" + Ver_Card.Trim() + "\n" + Ver_UD.Trim() + "\n" + Ver_SPT.Trim();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch
                {
                    return string.Empty;
                }

            }
        }

        protected int GetIntegerValue(string cmd)
		{
			string ret = ExecuteCommand(cmd);
            if (ret == "") return 0;
            return int.Parse(ret);  
            
        }
        /// <summary>
        /// 获取返回字符串
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string GetString(string cmd)
        {
            return ExecuteCommand(cmd);
        }
        /// <summary>
        /// 获取返回字符串
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string GetVerString(string cmd)
        {
            return ExecuteVer(cmd);
        }

        protected bool ExecuteAndCheckOk(string cmd)
		{
			string ret = ExecuteCommand(cmd);
			return ret.Equals("OK");
		}

		public void EnumCard()
		{
			lock (syncRoot)
			{
				//sport.WriteLine("EnumIP");
				//string ret = sport.ReadLine();
			}
		}

		public int GetAIO()
		{
			return GetIntegerValue("getaio 15");
		}

		public bool SetAIO(int chanel, bool val)
		{
			return ExecuteAndCheckOk("setaio " + chanel.ToString() + "," + (val ? "1" : "0"));
		}

		public int GetInputs()
		{
			return GetIntegerValue("getin");
		}

		public int GetInputsEx()
		{
			return GetIntegerValue("getinex");
		}

		public int GetOutputs()
		{
			return GetIntegerValue("getout");
		}

		public int GetOutputsEx()
		{
			return GetIntegerValue("getoutex");
		}

		public int GetDipSwitch()
		{
			return GetIntegerValue("getds");
		}

		public int GetDipSwitchEx()
		{
			return GetIntegerValue("getdsex");
		}

		public bool SetOutputs(byte val)
		{
			return ExecuteAndCheckOk("setout " + val.ToString());
		}

		public bool SetOutput(int chanel, bool val)
		{
			return ExecuteAndCheckOk("setout " + chanel.ToString() + "," + (val ? "1" : "0"));
		}

		public bool SetOutputEx(int chanel, bool val)
		{
			return ExecuteAndCheckOk("setoutex " + chanel.ToString() + "," + (val ? "1" : "0"));
		}

		public int GetAxisStatus(int axisNo)
		{
			return GetIntegerValue("getst " + axisNo.ToString());
		}

		public bool IsBusy(int axisNo)
		{
			int status = GetAxisStatus(axisNo);

			return (status & (0x01 << (int)AxisStatus.Busy)) > 0
				|| (status & (0x01 << (int)AxisStatus.Homing)) > 0;
		}

		public int GetPosition(int axisNo)
		{
			return GetIntegerValue("getpos " + axisNo.ToString());
		}

        
        /// <summary>
        /// 获取主板名称
        /// </summary>
        /// <returns></returns>
        public string GetCardName()
        {
            return GetString("getname");
        }
        /// <summary>
        /// 获取主板编号
        /// </summary>
        /// <returns></returns>
        public int GetSN()
        {
            return GetIntegerValue("getsn");
        } 
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        public string GetVerInfo()
        {
            return GetVerString("getver");
        }


		public bool SetPosition(int axisNo, int pos)
		{
			return ExecuteAndCheckOk("setpos " + axisNo.ToString() + "," + pos.ToString());
		}

		public bool RltMove(int axisNo, int dist, int startVel = 100,
			int vel = 2000, double acc = 0.2, double dec = 0.2)
		{
			int accTimeToPules = (int)((vel - startVel) / (acc * 1000));
			string cmd = string.Format("move {0},{1},{2},{3},{4},{5}",
				axisNo, dist, startVel, vel, accTimeToPules, accTimeToPules);

			return ExecuteAndCheckOk(cmd);
		}

		public bool AbsMove(int axisNo, int pos, int startVel = 100,
			int vel = 2000, double acc = 0.2, double dec = 0.2)
		{
			int accTimeToPules = (int)((vel - startVel) / (acc * 1000));
			string cmd = string.Format("moveto {0},{1},{2},{3},{4},{5}",
				axisNo, pos, startVel, vel, accTimeToPules, accTimeToPules);

			return ExecuteAndCheckOk(cmd);
		}

		public bool Home(int axisNo, int startVel = 100, int homeDir = 0, int homeSVel = 200,
			int vel = 2000, double acc = 0.2, double dec = 0.2,
			int homeMode = 0, int offset = 0)
		{
			int accTimeToPules = (int)((vel - startVel) / (acc * 1000));
			string cmd = string.Format("home {0},{1},{2},{3},{4},{5},{6},{7},{8}",
				axisNo, startVel, vel, accTimeToPules, accTimeToPules,
				homeMode, homeDir, homeSVel, offset);

			return ExecuteAndCheckOk(cmd);
		}

		public bool Stop(int axisNo)
		{
			return ExecuteAndCheckOk("stop " + axisNo.ToString() + ",1");
		}

		public bool EmgStop(int axisNo)
		{
			return ExecuteAndCheckOk("stop " + axisNo.ToString() + ",0");
		}

		public bool SetLimits(int axisNo, bool lmtEn, bool orgEn,
			bool lmtReverse, bool orgReverse)
		{
			string cmd = "setlmt " + axisNo.ToString() + "," + (lmtEn ? "1" : "0")
				 + "," + (orgEn ? "1" : "0")
				 + "," + (lmtReverse ? "1" : "0")
				 + "," + (orgReverse ? "1" : "0");

			return ExecuteAndCheckOk(cmd);
		}

		public bool IsExists()
		{
			if (!sport.IsOpen)
				return false;
			return ExecuteAndCheckOk("H");
		}
	}
}
