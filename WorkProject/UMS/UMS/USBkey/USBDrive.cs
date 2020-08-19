using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsFormsApplication1
{
    class USBDrive
    {
        // 打开设备
        [DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern bool xjkOpenKey(byte[] cUserName, byte[] btPassWord, char cLetter);
        //// 打开设备
        //[DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        //static extern bool xjkOpenKeyEx(byte[] cUserName, byte[] btPassWord);
        // 读取一个扇区的数据
        [DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern bool xjkReadSector(byte[] cDataRead, int iSector);
        // 写入一个扇区的数据
        [DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern bool xjkWriteSector(byte[] cDataWrite, int iSector);
        // 读取一个簇的数据
        [DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern bool xjkReadCluster(byte[] cDataRead, int iCluster);
        // 写入一个簇的数据
        [DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern bool xjkWriteCluster(byte[] cDataWrite, int iCluster);
        // 修改密码
        [DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern bool xjkChangePwd(byte[] cOldName, byte[] cNewName, byte[] cOldPwd, byte[] cNewPwd);
        // 读取 Key 序列号
        [DllImport("xjkKeyApi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern bool xjkGetSerial(byte[] cSerial);
        // 获取扇区总数
        [DllImport("xjkKeyApi.dll")]
        static extern int xjkGetSectors();
        // 获取簇总数
        [DllImport("xjkKeyApi.dll")]
        static extern int xjkGetClusters();
        // 关闭设备
        [DllImport("xjkKeyApi.dll")]
        static extern void xjkCloseKey();


        /// <summary>
        /// 自动打开USBkey
        /// </summary>
        /// <returns></returns>
        public bool AutoOpenUSBkey()
        {
            DriveInfo[] allDirves = DriveInfo.GetDrives();
            //检索计算机上的所有逻辑驱动器名称
            foreach (DriveInfo item in allDirves)
            {
                // 打开设备
                Byte[] cUserName = new Byte[257];
                cUserName = System.Text.Encoding.Default.GetBytes("");
                Byte[] btPassWord = new Byte[32];
                btPassWord = System.Text.Encoding.Default.GetBytes("888888");
                System.Text.StringBuilder cLetter = new System.Text.StringBuilder(item.Name, 32);
                bool bRtn = xjkOpenKey(cUserName, btPassWord, cLetter[0]);
                if (bRtn)
                    return true;               
            }
            return false;
        }

        /// <summary>
        /// 获取指定簇内容（0-320）
        /// </summary>
        public string GetPointCluster(int point)
        {
            byte[] cDataRead = new Byte[4097];
            bool bRtn = xjkReadCluster(cDataRead, point);
            if (bRtn)
            {
                string temp = System.Text.Encoding.Default.GetString(cDataRead);               
                return temp;
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 设置指定簇的内容
        /// </summary>
        /// <param name="point">0-320</param>
        /// <param name="data">最大513字节</param>
        /// <returns></returns>
        public bool SetPointCluster(int point,string data)
        {
            byte[] cDataWrite = new byte[513];
            cDataWrite = System.Text.Encoding.Default.GetBytes(data);
            return xjkWriteCluster(cDataWrite, point); ;
        }
    }
}
