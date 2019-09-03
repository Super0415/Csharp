using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

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
            WriteReg = 0x10,      //写保持寄存器
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

            NumberBurn = 50,
            Sen1Selec,
            Sen2Selec,
            Sen3Selec,
            Sen4Selec,
            RS485ConfAble,
            RS485_Baudrate,
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

            Map1_PRESS_ADC = 80,
            Map1_PRESS_Value = 100,
            Map2_PRESS_ADC = 120,
            Map2_PRESS_Value = 140,
            Map3_PRESS_ADC = 160,
            Map3_PRESS_Value = 180,
            Map4_PRESS_ADC = 200,
            Map4_PRESS_Value = 220,
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
        private int modaddr = 1;
        /// <summary>
        /// 设置获取通讯地址
        /// </summary>
        public int MODAddr
        {
            get { return modaddr; }
            set { modaddr = value; }
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
                try
                {
                    sport.Write(data, 0, data.Length);
                    for (int i = 0; i < 0xFF; i++)
                    {
                        Rdata[i] = (byte)sport.ReadByte();

                        if (i < 5) continue;
                        else if (i >= 5)
                        {
                            UInt16 crc16 = GetCrc16Code(Rdata, (Byte)(i - 1));
                            if (crc16 == FUN_CREAT_16U(Rdata[i - 1], Rdata[i]))
                            {
                                Clear();
                                return Rdata;
                            }
                        }
                    }
                    Clear();
                    return Rdata;
                }
                catch/*(Exception ex)*/
                {
                    return Rdata;
                }

            }
		}

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
            Byte[] Sdata = new byte[8];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.RamDB_PRESS01_Volt);
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.ReadReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];        //地址
            Sdata[4] = 0x00;
            Sdata[5] = 0x08;
            UInt16 crc16 = GetCrc16Code(Sdata, 6);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[6] = crc08[0];
            Sdata[7] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            data[0] = FUN_CREAT_16U(Rdata[5], Rdata[6]);
            data[1] = FUN_CREAT_16U(Rdata[9], Rdata[10]);
            data[2] = FUN_CREAT_16U(Rdata[13], Rdata[14]);
            data[3] = FUN_CREAT_16U(Rdata[17], Rdata[18]);
            return data;
        }

        /// <summary>
        /// 获取状态1
        /// </summary>
        /// <returns></returns>
        public int GetSTATE0(int addr)
        {
            Byte[] Sdata = new byte[8];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.COMMON_STATE0);
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.ReadReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];        //地址
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
        /// 获取LED状态
        /// </summary>
        /// <returns></returns>
        public int GetLeds(int addr)
        {
            Byte[] Sdata = new byte[8];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.LED_RamDB);
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.ReadReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];        //地址
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
        /// 获取输出口状态
        /// </summary>
        /// <returns></returns>
        public int GetOutputs(int addr)
        {
            Byte[] Sdata = new byte[8];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.DO_RamDB);
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.ReadReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];        //地址
            Sdata[4] = 0x00;
            Sdata[5] = 0x01;
            UInt16 crc16 = GetCrc16Code(Sdata,6);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[6] = crc08[0];
            Sdata[7] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);

            return FUN_CREAT_16U(Rdata[3], Rdata[4]);
        }
        /// <summary>
        /// 获取输出口极性、LED极性
        /// </summary>
        /// <returns></returns>
        public int GetOutputPol(int addr)
        {
            Byte[] Sdata = new byte[8];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.DO_Polarity);
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.ReadReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];        //地址
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
        /// 获取指定位置、长度的数据组
        /// 1-波特率；2-1动作配置；3-1动作延时；4-1低阈值动作；5-1高阈值动作
        /// 6-2动作配置；7-2动作延时；8-2低阈值动作；9-2高阈值动作；10-3动作配置；11-3动作延时；12-3低阈值动作；
        /// 13-3高阈值动作；14-4动作配置；15-4动作延时；16-4低阈值动作；17-4高阈值动作
        /// </summary>
        /// <returns>处理后的整形数组</returns>
        public int[] GetFlashDBP(int addr)
        {
            Byte[] Sdata = new byte[8];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.RS485_Baudrate);
            Byte Len = 17;
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.ReadReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];         //地址 - 从 FlashDB 的第0位开始
            Sdata[4] = 0x00;
            Sdata[5] = Len;            //读取24个数,超过255重新规划
            UInt16 crc16 = GetCrc16Code(Sdata, 6);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[6] = crc08[0];
            Sdata[7] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            int[] DData = new int[0xFF];
            for(int i = 0;i < Len;i++)
            {
                DData[i] = FUN_CREAT_16U(Rdata[3+i*2], Rdata[4+i*2]);
            }
            return DData;
        }
        /// <summary>
        /// 设置输出口极性、LED极性
        /// </summary>
        /// <returns></returns>
        public bool SetOutputPol(int addr,int Pol)
        {
            Byte[] Sdata = new byte[11];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.DO_Polarity);
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.WriteReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];        //地址
            Sdata[4] = 0x00;
            Sdata[5] = 0x01;
            Sdata[6] = 0x02;
            Byte[] temp = FUN_UC_HIGH_LOW((UInt16)Pol);
            Sdata[7] = temp[0];
            Sdata[8] = temp[1];
            UInt16 crc16 = GetCrc16Code(Sdata, 9);
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[9] = crc08[0];
            Sdata[10] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            if (Rdata[0] != 0) return true;

            return false;
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
            Byte[] Sdata = new byte[dlen*2+9];
            Byte[] Addr = FUN_UC_HIGH_LOW((UInt16)Protocol.RS485ConfAble);
            Byte Len = (Byte)dlen;
            Byte Sum = (Byte)(dlen*2);
            Sdata[0] = (Byte)addr;
            Sdata[1] = (Byte)CMD.WriteReg;
            Sdata[2] = Addr[0];
            Sdata[3] = Addr[1];          //地址 - 从 FlashDB 的第0位开始
            Sdata[4] = 0x00;
            Sdata[5] = Len;              //读取19个数,超过255重新规划
            Sdata[6] = Sum;            //读取19 *2 = 46个数,超过255重新规划

            for(int i = 0;i < Len;i++)
            {
                Byte[] temp = FUN_UC_HIGH_LOW((UInt16)data[i]);
                Sdata[7+i*2] = temp[0];
                Sdata[8+i*2] = temp[1];
            }          

            UInt16 crc16 = GetCrc16Code(Sdata, (Byte)(7+ Sum));
            Byte[] crc08 = FUN_UC_HIGH_LOW(crc16);
            Sdata[7 + Len * 2] = crc08[0];
            Sdata[8 + Len * 2] = crc08[1];
            Byte[] Rdata = GetIntegerValue(Sdata);
            if (Rdata[0] != 0) return true;

            return false;
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
        /// 将16位数据分成高8位、低8位
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
