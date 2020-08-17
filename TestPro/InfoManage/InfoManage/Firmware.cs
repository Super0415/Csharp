using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace InfoManage
{
    class Firmware
    {
        private SerialPort sport = new SerialPort();
        private object syncRoot = new object();

        /// <summary>
        /// 设置或获取通讯端口
        /// </summary>
        public string Port { get; set; } = "COM3";

        /// <summary>
        /// 设置或获取波特率
        /// </summary>
        public int BaudRate { get; set; } = 115200;

        /// <summary>
        /// 设置或获取读写超时时间
        /// 修改此值后必须重新调用Open才会生效
        /// </summary>
        public int Timeout { get; set; } = 1000;

        /// <summary>
        /// 检查当前串口是否被占用
        /// </summary>
        /// <returns></returns>
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
        /// 打开指定串口
        /// </summary>
        public void Open()
        {
            lock (syncRoot)
            {
                if (sport.IsOpen)
                    sport.Close();
                sport.PortName = Port;
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
        /// 关闭串口
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
        private void Clear()
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

        private void ReadData()
        {
            byte[] buff = new byte[23];
            int offset = 23;
            int count = 23;
            sport.Read(buff, offset, count);
        }

    }
}
