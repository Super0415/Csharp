using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yungku.Common.IOCard
{
	public class YKS1Card
	{
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
				sport.BaudRate = 9600;
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
        /// 清空输入输出缓存区
        /// </summary>
        public void Clear()
        {
            lock (syncRoot)
            {
                if (sport.IsOpen)
                {
                    sport.DiscardInBuffer();//清理输入缓冲区
                    sport.DiscardOutBuffer();//清理输出缓冲区
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
        /// <summary>
        /// 执行一个命令并返回一个结果
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string ExecuteCommand(string cmd)
		{
			lock (syncRoot)
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
		}

		/// <summary>
        /// 获取整形数据
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected int GetIntegerValue(string cmd)
		{
			string ret = ExecuteCommand(cmd);
			return int.Parse(ret);
		}

		/// <summary>
        /// 检查执行结果是否正常
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected bool ExecuteAndCheckOk(string cmd)
		{
			string ret = ExecuteCommand(cmd);
			return ret.Equals("OK");
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

        /// <summary>
        /// 获取输入口状态
        /// </summary>
        /// <returns></returns>
        public int GetInputs()
		{
			return GetIntegerValue("X?");
		}

		/// <summary>
        /// 获取输出口状态
        /// </summary>
        /// <returns></returns>
        public int GetOutputs()
		{
			return GetIntegerValue("Y?");
		}
        /// <summary>
        /// 心跳测试
        /// </summary>
        /// <returns></returns>
        public bool IsExists()
        {
            if (!sport.IsOpen)
                return false;
            return ExecuteAndCheckOk("HI");
        }

        /// <summary>
        /// 停止电机
        /// </summary>
        /// <returns></returns>
        public bool EmgStop()
        {
            return ExecuteAndCheckOk(string.Format("MS"));
        }
        /// <summary>
        /// 开始相对移动
        /// </summary>
        /// <returns></returns>
        public bool RltMove()
        {
            return ExecuteAndCheckOk(string.Format("SM"));
        }
        /// <summary>
        /// 开始回原点
        /// </summary>
        /// <returns></returns>
        public bool Home()
        {
            return ExecuteAndCheckOk(string.Format("SH"));
        }
        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public bool Clean()
        {
            return ExecuteAndCheckOk(string.Format("RP"));
        }
        /// <summary>
        /// 查询电机位置
        /// </summary>
        /// <returns></returns>
        public int GetPosition()
        {
            return GetIntegerValue("POS?");
        }
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        public string GetVerInfo()
        {
            return GetVerString("VER?");
        }
        /// <summary>
        /// 查询电机状态
        /// </summary>
        /// <returns></returns>
        public int GetState()
        {
            return GetIntegerValue("MST?");
        }
        /// <summary>
        /// 查询电机DIP开关的值
        /// </summary>
        /// <returns></returns>
        public int GetState()
        {
            return GetIntegerValue("D?");
        }


        /// <summary>
        /// 查询DIP开关的值
        /// </summary>
        /// <returns></returns>
        public int GetDipSwitch()
		{
			return GetIntegerValue("D?");
		}

		/// <summary>
        /// 设置指定输出端口状态
        /// </summary>
        /// <param name="chanel"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetOutput(int chanel, bool val)
		{
			return ExecuteAndCheckOk(string.Format("SY {0}{1}", chanel, val ? 1:0));
		}
        /// <summary>
        /// 设置所有输出端口状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetOutputs(int state)
        {
            return ExecuteAndCheckOk(string.Format("SY {0}", state));
        }

        /// <summary>
        /// 带参数回原点
        /// </summary>
        /// <param name="dirc">方向</param>
        /// <param name="dist">移动距离</param>
        /// <param name="vel">移动速度</param>
        /// <returns></returns>
        public bool SetMMHome(int dirc,int dist,int vel)
        {
            return ExecuteAndCheckOk(string.Format("MM H{0}{1}{2}", dirc, dist.ToString().PadLeft(5,'0'), vel.ToString().PadLeft(4,'0')));
        }
        /// <summary>
        /// 带参数移动
        /// </summary>
        /// <param name="dirc">方向</param>
        /// <param name="dist">移动距离</param>
        /// <param name="vel">移动速度</param>
        /// <returns></returns>
        public bool SetMMmove(int dirc, int dist, int vel)
        {
            return ExecuteAndCheckOk(string.Format("MM M{0}{1}{2}", dirc, dist.ToString().PadLeft(5, '0'), vel.ToString().PadLeft(4, '0')));
        }
        /// <summary>
        /// 带参数停机
        /// </summary>
        /// <param name="dirc">方向</param>
        /// <param name="dist">移动距离</param>
        /// <param name="vel">移动速度</param>
        /// <returns></returns>
        public bool SetMMStop(int dirc, int dist, int vel)
        {
            return ExecuteAndCheckOk(string.Format("MM S{0}{1}{2}", dirc, dist.ToString().PadLeft(5, '0'), vel.ToString().PadLeft(4, '0')));
        }
        /// <summary>
        /// 配置参数
        /// </summary>
        /// <param name="dirc">方向</param>
        /// <param name="dist">移动距离</param>
        /// <param name="vel">移动速度</param>
        /// <returns></returns>
        public bool SetMMPara(int dirc, int dist, int vel)
        {
            return ExecuteAndCheckOk(string.Format("MM P{0}{1}{2}", dirc, dist.ToString().PadLeft(5, '0'), vel.ToString().PadLeft(4, '0')));
        }




    }
}
