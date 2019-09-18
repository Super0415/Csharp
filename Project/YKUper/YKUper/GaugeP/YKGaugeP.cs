using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;   //测试响应时间

namespace Yungku.Common.GaugeP
{
	public class YKGaugeP
    {
        Byte[] Crc16TabHi =
        {   0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
        };
        Byte[] Crc16TabLo =
        {   0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2,
            0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04,
            0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
            0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8,
            0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A,
            0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
            0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6,
            0xD2, 0x12, 0x13, 0xD3, 0x11, 0xD1, 0xD0, 0x10,
            0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
            0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4,
            0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE,
            0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
            0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA,
            0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C,
            0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0,
            0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62,
            0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
            0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE,
            0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68,
            0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
            0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C,
            0xB4, 0x74, 0x75, 0xB5, 0x77, 0xB7, 0xB6, 0x76,
            0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
            0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92,
            0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54,
            0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
            0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98,
            0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A,
            0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86,
            0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80, 0x40
        };
        enum CMD
        {
            ReadReg  = 0x03,        //读保持寄存器
            WriteReg = 0x10,        //写保持寄存器
            SPECIAL = 0xFE,         //特殊协议功能
        };
        enum CMD_FUN
        {
            SaveData = 0x01,        //保存数据
            Reset = 0x02,           //恢复出厂设置
        };
        enum CMD_SPE                //特殊协议功能
        {
            Heart = 0x01,           //心跳机制
            Verb  = 0x02,           //查询版本
        };
        enum MOD_STATE                //通讯状态
        {
            MOD_ERR = 0,           //通讯故障
            MOD_OK = 1,            //通讯正常
        };
        enum Protocol
        {
            COMMON_CMD,
            COMMON_STATE0,
            COMMON_STATE1,
            LED_RamDB,
            DO_RamDB,
            RamDB_PRESS01_Volt,
            RamDB_PRESS01_Value,
            RamDB_PRESS02_Volt,
            RamDB_PRESS02_Value,
            RamDB_PRESS03_Volt,
            RamDB_PRESS03_Value,
            RamDB_PRESS04_Volt,
            RamDB_PRESS04_Value,

            NumberBurn = 30,
            Sen1Selec,
            Sen2Selec,
            Sen3Selec,
            Sen4Selec,
            ABLE_APress01,
            DELAY_APress01,
            FlashDB_PRESS01_L,
            FlashDB_PRESS01_H,
            ABLE_APress02,
            DELAY_APress02,
            FlashDB_PRESS02_L,
            FlashDB_PRESS02_H,
            ABLE_APress03,
            DELAY_APress03,
            FlashDB_PRESS03_L,
            FlashDB_PRESS03_H,
            ABLE_APress04,
            DELAY_APress04,
            FlashDB_PRESS04_L,
            FlashDB_PRESS04_H,
            DO_Polarity,
            Sen1Calib,
            Sen2Calib,
            Sen3Calib,
            Sen4Calib,

            Map1_PRESS_Volt = (60 + 10 * 0),
            Map1_PRESS_Show = (60 + 10 * 1),
            Map2_PRESS_Volt = (60 + 10 * 2),
            Map2_PRESS_Show = (60 + 10 * 3),
            Map3_PRESS_Volt = (60 + 10 * 4),
            Map3_PRESS_Show = (60 + 10 * 5),
            Map4_PRESS_Volt = (60 + 10 * 6),
            Map4_PRESS_Show = (60 + 10 * 7),
        };

        private SerialPort sport = new SerialPort();
		private object syncRoot = new object();

        //private int Testnum = 0;
        //private double TestTBuf = new double();
        //private double TestTSum = new double();


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
        private int modaddr = 1;
        /// <summary>
        /// 设置获取通讯地址
        /// </summary>
        public int MODAddr
        {
            get { return modaddr; }
            set { modaddr = value; }
        }
        private int modstate = 0;
        /// <summary>
        /// 设置获取通讯地址
        /// </summary>
        public int MODState
        {
            get { return modstate; }
            set { modstate = value; }
        }

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
                Clear();
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
        /// 执行一个命令并返回一个结果
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        
        /*protected*/
        public Byte[] ExecuteCommand(Byte[] data)
		{
			lock (syncRoot)
			{
                Byte[] Rdata = new byte[0xFF];
                Stopwatch stopwatch = new Stopwatch();
                try
                {
                    sport.Write(data, 0, data.Length);
               //     stopwatch.Start(); //  开始监视代码运行时间
                    for (int i = 0; i < 0xFF; i++)
                    {
                        Rdata[i] = (byte)sport.ReadByte();

                        if (i < 5)
                            continue;
                        else if (i >= 5)
                        {
                            UInt16 crc16 = GetCrc16Code(Rdata, (Byte)(i - 1));
                            if (crc16 == FUN_CREAT_16U(Rdata[i - 1], Rdata[i]))
                            {

                                //stopwatch.Stop(); //  停止监视
                                //TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
                                //double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数

                                //TestTBuf += milliseconds;
                                //Testnum++;
                                //TestTSum = TestTBuf / Testnum;
                                Clear();
                                return Rdata;                                               //通讯 1027 次，单次通讯时间为 14.246ms                1318 - 14.319       //此为modbus协议，无法一次读取判断，每次读取判断均需要计算校验吗，耗费大量时间
                            }
                        }
                    }
                    Clear();
                    return Rdata;
                }
                catch/*(Exception ex)*/
                {
                    Clear();
                    return Rdata;
                }

            }
		}

        /// <summary>
        /// 查询单个数据指令
        /// </summary>
        /// <param name="addr">通讯地址</param>
        /// <param name="cmd">通讯命令</param>
        /// <param name="dpos">查询数据起始位置</param>
        /// <param name="leng">查询数据长度</param>
        /// <returns></returns>
        public int GetData(byte addr, byte cmd, UInt16 dpos)
        {
            byte[] Sdata = new byte[8];
            byte[] pos = FUN_UC_HIGH_LOW(dpos);
            Sdata[0] = addr;        //通讯地址
            Sdata[1] = cmd;         //通讯命令
            Sdata[2] = pos[0];      //查询数据起始位置高位
            Sdata[3] = pos[1];      //查询数据起始位置低位
            Sdata[4] = 0x00;
            Sdata[5] = 0x01;
            UInt16 crc16 = GetCrc16Code(Sdata, 6);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[6] = crc08[0];
            Sdata[7] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            return FUN_CREAT_16U(Rdata[3], Rdata[4]);
        }
        /// <summary>
        /// 查询多个数据指令
        /// </summary>
        /// <param name="addr">通讯地址</param>
        /// <param name="cmd">通讯命令</param>
        /// <param name="dpos">查询数据起始位置</param>
        /// <param name="leng">查询数据长度</param>
        /// <returns></returns>
        public int[] GetDatas(byte addr, byte cmd, UInt16 dpos, UInt16 leng)
        {
            byte[] Sdata = new byte[8];
            byte[] pos = FUN_UC_HIGH_LOW(dpos);
            byte[] len = FUN_UC_HIGH_LOW(leng);
            Sdata[0] = addr;        //通讯地址
            Sdata[1] = cmd;         //通讯命令
            Sdata[2] = pos[0];      //查询数据起始位置高位
            Sdata[3] = pos[1];      //查询数据起始位置低位
            Sdata[4] = len[0];      //查询数据长度高位
            Sdata[5] = len[1];      //查询数据长度低位
            UInt16 crc16 = GetCrc16Code(Sdata, 6);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[6] = crc08[0];
            Sdata[7] = crc08[1];

            Byte[] Rdata = GetIntegerValue(Sdata);
            int[] DData = new int[leng];
            for (int i = 0; i < leng; i++)
            {
                DData[i] = FUN_CREAT_16U(Rdata[3 + i * 2], Rdata[4 + i * 2]);
            }
            return DData;
        }
        /// <summary>
        /// 写入单个数据指令
        /// </summary>
        /// <returns></returns>
        public bool SetData(int addr, int cmd, int dpos,int data)
        {
            Byte N = 1;
            Byte[] Sdata = new byte[7+N*2+2];
            Byte[] pos = FUN_UC_HIGH_LOW((UInt16)dpos);
            Byte[] n = FUN_UC_HIGH_LOW(N);
            Sdata[0] = (byte)addr;            //通讯地址
            Sdata[1] = (byte)cmd;             //通讯命令
            Sdata[2] = pos[0];          //写入数据起始位置高位
            Sdata[3] = pos[1];          //写入数据起始位置低位
            Sdata[4] = n[0];            //写入数据数量高位
            Sdata[5] = n[1];            //写入数据数量低位
            Sdata[6] = (Byte)(N*2);     //写入数据字节数量
            Byte[] temp = FUN_UC_HIGH_LOW((UInt16)data);    //写入数据
            Sdata[7] = temp[0];
            Sdata[8] = temp[1];
            UInt16 crc16 = GetCrc16Code(Sdata, (byte)(7+N*2));
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[9] = crc08[0];
            Sdata[10] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            if (Rdata[0] != 0) return true;
            return false;
        }
        /// <summary>
        /// 写入多个数据指令
        /// </summary>
        /// <returns></returns>
        public bool SetDatas(int addr, int cmd, int dpos, int[] data,int len)
        {
            Byte N = (Byte)len;
            Byte[] Sdata = new byte[7 + N * 2 + 2];
            Byte[] pos = FUN_UC_HIGH_LOW((UInt16)dpos);
            Byte[] n = FUN_UC_HIGH_LOW(N);
            Sdata[0] = (Byte)addr;            //通讯地址
            Sdata[1] = (Byte)cmd;             //通讯命令
            Sdata[2] = pos[0];          //写入数据起始位置高位
            Sdata[3] = pos[1];          //写入数据起始位置低位
            Sdata[4] = n[0];            //写入数据数量高位
            Sdata[5] = n[1];            //写入数据数量低位
            Sdata[6] = (Byte)(N * 2);     //写入数据字节数量

            for (int i = 0; i < N; i++)
            {
                Byte[] temp = FUN_UC_HIGH_LOW((UInt16)data[i]);
                Sdata[7 + i * 2] = temp[0];
                Sdata[8 + i * 2] = temp[1];
            }

            UInt16 crc16 = GetCrc16Code(Sdata, (byte)(7 + N * 2));
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[7 + N * 2] = crc08[0];
            Sdata[8 + N * 2] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            if (Rdata[0] != 0) return true;
            return false;
        }
        /// <summary>
        /// 写入读取数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Byte[] GetIntegerValue(Byte[] data)
        {
            return ExecuteCommand(data);
        }
        /// <summary>
        /// 获取传感器数据 4 个
        /// </summary>
        /// <returns></returns>
        public int[] GetSensors(int addr)
        {
            int[] data = new int[4];
            int[] Temp = GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.RamDB_PRESS01_Volt, 8);
            data[0] = Temp[1];
            data[1] = Temp[3];
            data[2] = Temp[5];
            data[3] = Temp[7];
            return data;
        }
        /// <summary>
        /// 获取状态1
        /// </summary>
        /// <returns></returns>
        public int GetSTATE0(int addr)
        {
            return GetData((Byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.COMMON_STATE0);
        }
        /// <summary>
        /// 获取LED状态
        /// </summary>
        /// <returns></returns>
        public int GetLeds(int addr)
        {
            return GetData((Byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.LED_RamDB);
        }
        /// <summary>
        /// 获取输出口状态
        /// </summary>
        /// <returns></returns>
        public int GetOutputs(int addr)
        {
            return GetData((Byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.DO_RamDB);
        }
        /// <summary>
        /// 获取输出口极性、LED极性
        /// </summary>
        /// <returns></returns>
        public int GetOutputPol(int addr)
        {
            return GetData((Byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.DO_Polarity);
        }

        /// <summary>
        /// 设置输出口极性、LED极性
        /// </summary>
        /// <param name="addr">通讯地址</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetOutputPol(int addr, int data)
        {
            return SetData((Byte)addr, (Byte)CMD.WriteReg, (UInt16)Protocol.DO_Polarity, data);
        }
        /// <summary>
        /// 获取固件内部传感器选择项
        /// </summary>
        /// <returns></returns>
        public int[] GetSensorItems(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Sen1Selec, 4);
        }
        /// <summary>
        /// 设置固件内部传感器选择项
        /// </summary>
        /// <returns></returns>
        public bool SetSensorItems(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Sen1Selec, data, 4);
        }
        /// <summary>
        /// 获取固件内部传感器校准值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensorCalib(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Sen1Calib, 4);
        }
        /// <summary>
        /// 设置固件内部传感器校准值
        /// </summary>
        /// <returns></returns>
        public bool SetSensorCalib(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Sen1Calib, data, 4);
        }

        /// <summary>
        /// 获取固件内部传感器1的电压值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor1Volt(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map1_PRESS_Volt, 10);
        }
        /// <summary>
        /// 设置固件内部传感器1的电压值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor1Volt(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map1_PRESS_Volt, data, 10);
        }
        /// <summary>
        /// 获取固件内部传感器1的显示值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor1Show(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map1_PRESS_Show, 10);
        }
        /// <summary>
        /// 设置固件内部传感器1的显示值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor1Show(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map1_PRESS_Show, data, 10);
        }
        /// <summary>
        /// 获取固件内部传感器2的电压值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor2Volt(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map2_PRESS_Volt, 10);
        }
        /// <summary>
        /// 设置固件内部传感器2的电压值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor2Volt(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map2_PRESS_Volt, data, 10);
        }
        /// <summary>
        /// 获取固件内部传感器2的显示值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor2Show(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map2_PRESS_Show, 10);
        }
        /// <summary>
        /// 设置固件内部传感器2的显示值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor2Show(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map2_PRESS_Show, data, 10);
        }
        /// <summary>
        /// 获取固件内部传感器3的电压值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor3Volt(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map3_PRESS_Volt, 10);
        }
        /// <summary>
        /// 设置固件内部传感器3的电压值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor3Volt(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map3_PRESS_Volt, data, 10);
        }
        /// <summary>
        /// 获取固件内部传感器3的显示值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor3Show(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map3_PRESS_Show, 10);
        }
        /// <summary>
        /// 设置固件内部传感器3的显示值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor3Show(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map3_PRESS_Show, data, 10);
        }
        /// <summary>
        /// 获取固件内部传感器4的电压值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor4Volt(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map4_PRESS_Volt, 10);
        }
        /// <summary>
        /// 设置固件内部传感器4的电压值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor4Volt(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map4_PRESS_Volt, data, 10);
        }
        /// <summary>
        /// 获取固件内部传感器4的显示值
        /// </summary>
        /// <returns></returns>
        public int[] GetSensor4Show(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.Map4_PRESS_Show, 10);
        }
        /// <summary>
        /// 设置固件内部传感器4的显示值
        /// </summary>
        /// <returns></returns>
        public bool SetSensor4Show(int addr, int[] data)
        {
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.Map4_PRESS_Show, data, 10);
        }
        /// <summary>
        /// 获取特定位置、长度的参数配置
        ////* 1-1动作配置；2-1动作延时；3-1低阈值动作；4-1高阈值动作
        /// 5-2动作配置；6-2动作延时；7-2低阈值动作；8-2高阈值动作
        /// 9-3动作配置；10-3动作延时；11-3低阈值动作；12-3高阈值动作；
        /// 13-4动作配置；14-4动作延时；15-4低阈值动作；16-4高阈值动作
        /// </summary>
        /// <returns>处理后的整形数组</returns>
        public int[] GetFlashDBP(int addr)
        {
            return GetDatas((byte)addr, (Byte)CMD.ReadReg, (UInt16)Protocol.ABLE_APress01, 16);
        }
       
        /// <summary>
        /// 设置 FlashDB 中RS485ConfAble开始的数
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="data"></param>
        /// <param name="dlen"></param>
        /// <returns></returns>
        public bool SetFlashDBP(int addr, int[] data,int dlen)
        {           
            return SetDatas(addr, (int)CMD.WriteReg, (int)Protocol.ABLE_APress01, data, dlen);
        }
        /// <summary>
        /// 设置保存数据
        /// </summary>
        /// <returns></returns>
        public bool SetSaveData(int addr)
        {
            return SetData(addr, (int)CMD.WriteReg, (int)Protocol.COMMON_CMD, (int)CMD_FUN.SaveData);
        }
        /// <summary>
        /// 恢复出厂设置
        /// </summary>
        /// <returns></returns>
        public bool SetReset(int addr)
        {
            return SetData(addr, (int)CMD.WriteReg, (int)Protocol.COMMON_CMD, (int)CMD_FUN.Reset);
        }

        /// <summary>
        /// 心跳回应 01 FE 01 A1 A0  
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public bool IsExists(int addr)
        {
            if (!sport.IsOpen)
                return false;

            Byte[] Sdata = new byte[5];
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.SPECIAL;
            Sdata[2] = (Byte)CMD_SPE.Heart;
            UInt16 crc16 = GetCrc16Code(Sdata, 3);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[3] = crc08[0];
            Sdata[4] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            if ((Rdata[0] == Sdata[0]) && (Rdata[1] == Sdata[1]) && (Rdata[2] == Sdata[2]))
            {
                modstate = (int)MOD_STATE.MOD_OK;
                return true;
            }

            modstate = (int)MOD_STATE.MOD_ERR;
            return false;
        }
        public string GetVerInfo(int addr)
        {
            Byte[] Sdata = new byte[5];
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.SPECIAL;
            Sdata[2] = (Byte)CMD_SPE.Verb;
            UInt16 crc16 = GetCrc16Code(Sdata, 3);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[3] = crc08[0];
            Sdata[4] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            if (Rdata[0] != 0)
            {
                modstate = (int)MOD_STATE.MOD_OK;
                string str = Encoding.UTF8.GetString(Rdata);
                string info = str.Split('@')[1].Split('\0')[0];
                return info;
            }

            modstate = (int)MOD_STATE.MOD_ERR;
            return null;
        }

        /// <summary>
        /// 将两个8位的数据按规则合并为16位数
        /// </summary>
        /// <param name="h"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        int FUN_CREAT_16U(Byte h, Byte l)
        {
            return ((int)(l) + ((int)(h) << 8));
        }
        /// <summary>
        /// 将16位数据分成高8位（数组1位）、低8位（数组0位） 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Byte[] FUN_UC_HIGH_LOW(UInt16 data)
        {
            Byte[] pdata = new Byte[2];
            pdata[0] = (Byte)(data >> 8);
            pdata[1] = (Byte)(data);
            return pdata;
        }

        UInt16 GetCrc16Code(Byte[] pBuf, Byte ucSize)
        {
            Byte Index = 0;
            Byte CrcHi = 0xFF;
            Byte CrcLo = 0xFF;
            int i = 0;
            while ((ucSize--) > 0)
            {   
                Index = (Byte)(CrcHi ^ (pBuf[i++]));
                CrcHi = (Byte)(CrcLo ^ Crc16TabHi[Index]);
                CrcLo = Crc16TabLo[Index];
            }
            return (UInt16)(((UInt16)CrcHi << 8) + (UInt16)CrcLo);
        }
    }
}
