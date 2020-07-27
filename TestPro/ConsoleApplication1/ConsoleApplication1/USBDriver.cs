using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApplication1
{
	class USBDriver
	{
		#region 结构
		internal struct HID_ATTRIBUTES
		{
			internal UInt32 Size;
			internal ushort VendorID;
			internal ushort ProductID;
			internal ushort VersionNumber;
		}

		internal struct SP_DEVICE_INTERFACE_DATA
		{
			internal UInt32 cbSize;
			internal Guid interfaceClassGuid;
			internal UInt32 flags;
			internal UInt64 reserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct SP_DEVINFO_DATA
		{
			internal UInt32 cbSize;
			internal Guid classGuid;
			internal UInt32 devInst;
			internal UInt64 reserved;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
		{
			internal UInt32 cbSize;
			internal UInt32 devicePath;
		}

		/// <summary>
		/// HID设备类
		/// </summary>
		public class HIDINFO
		{
			public IntPtr handle;       //设备指针
			public string path;         //设备路径
			public ushort vid;          //厂商ID
			public ushort pid;          //产品ID
			public string pname;        //产品名称
			public string vname;        //厂商名称
			public FileStream fs;       //文件流
		}

		#endregion

		#region  dll引用函数
		//获得HID设备的GUID
		[DllImport("hid.dll")]
		private static extern void HidD_GetHidGuid(ref Guid HidGuid);
		//获取相关HID属性
		[DllImport("hid.dll")]
		private static extern Boolean HidD_GetAttributes(IntPtr hidHandle, out HID_ATTRIBUTES attributes);
		//获取HID产品名称
		[DllImport("hid.dll")]
		private static extern Boolean HidD_GetProductString(IntPtr hidHandle, byte[] ProductInfo, Int32 BufferLength);
		//获取HID厂家名称
		[DllImport("hid.dll")]
		private static extern Boolean HidD_GetManufacturerString(IntPtr hidHandle, byte[] ManufacturerInfo, Int32 BufferLength);
		//获取设备文件
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateFile(
			String lpFileName,                            // 文件名 
			UInt32 dwDesiredAccess,                         // 访问模式（写 / 读） 
			UInt32 dwShareMode,                             // 共享模式 
			IntPtr lpSecurityAttributes,                    // 指向安全属性的指针
			UInt32 dwCreationDisposition,                   // 如何创建 
			UInt32 dwFlagsAndAttributes,                    // 文件属性 
			IntPtr hTemplateFile                            // 用于复制文件句柄 
			);
		//获取需要的设备指针
		[DllImport("setupapi.dll", SetLastError = true)]
		private static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, uint Enumerator, IntPtr HwndParent, DIGCF Flags);
		//获取设备，true获取到
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInfo, ref Guid interfaceClassGuid, UInt32 memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);
		// 获取接口的详细信息 必须调用两次 第1次返回长度 第2次获取数据 
		[DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData,
			int deviceInterfaceDetailDataSize, ref int requiredSize, SP_DEVINFO_DATA deviceInfoData);
		#endregion

		#region  常量

		public enum DIGCF
		{
			DIGCF_DEFAULT = 0x1,
			DIGCF_PRESENT = 0x2,
			DIGCF_ALLCLASSES = 0x4,
			DIGCF_PROFILE = 0x8,
			DIGCF_DEVICEINTERFACE = 0x10
		}

		private const uint GENERIC_READ = 0x80000000;
		private const uint GENERIC_WRITE = 0x40000000;
		private const uint FILE_SHARE_READ = 0x00000001;
		private const uint FILE_SHARE_WRITE = 0x00000002;
		private const uint FILE_FLAG_OVERLAPPED = 0x40000000;
		private const int OPEN_EXISTING = 3;
		private const int NAME_LENS = 64;

		#endregion

		#region  方法
		private List<HIDINFO> HIDInfoList;
		private int HIDSearch(int max_try)
		{
			HIDInfoList.Clear();
			//指定HID类的GUID
			Guid guidHID = Guid.Empty;
			HidD_GetHidGuid(ref guidHID);

			//获取GUID类型设备信息指针 hDevInfo
			IntPtr hDevInfo = IntPtr.Zero;
			hDevInfo = SetupDiGetClassDevs(ref guidHID, 0, IntPtr.Zero, DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_DEVICEINTERFACE);

			////设备信息数据 devInfoData
			SP_DEVICE_INTERFACE_DATA DeviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
			DeviceInterfaceData.cbSize = (UInt32)Marshal.SizeOf(DeviceInterfaceData);

			SP_DEVINFO_DATA strtInterfaceData = new SP_DEVINFO_DATA();
			strtInterfaceData.cbSize = (UInt32)Marshal.SizeOf(strtInterfaceData);

			SP_DEVICE_INTERFACE_DETAIL_DATA detailData = new SP_DEVICE_INTERFACE_DETAIL_DATA();
			detailData.cbSize = (UInt32)Marshal.SizeOf(detailData);

			//循环内所需数据
			int device_found = 0;
			UInt32 device_index = 0;
			int device_max_try = max_try;
			int bufferSize = 0;
			IntPtr hidHandle = IntPtr.Zero;

			while (device_max_try > 0)
			{
				device_max_try--;
				if (SetupDiEnumDeviceInterfaces(hDevInfo, IntPtr.Zero, ref guidHID, device_index, ref DeviceInterfaceData))
				{
					SetupDiGetDeviceInterfaceDetail(hDevInfo, ref DeviceInterfaceData, IntPtr.Zero, 0, ref bufferSize, strtInterfaceData);
					IntPtr detailDataBuffer = Marshal.AllocHGlobal(bufferSize);
					Marshal.StructureToPtr(detailData, detailDataBuffer, false);
					if (SetupDiGetDeviceInterfaceDetail(hDevInfo, ref DeviceInterfaceData, detailDataBuffer, bufferSize, ref bufferSize, strtInterfaceData))
					{
						string devicePathName = Marshal.PtrToStringAuto(detailDataBuffer + sizeof(UInt32));
						hidHandle = CreateFile(devicePathName, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
						if (hidHandle != IntPtr.Zero && hidHandle.ToInt32() != -1)
						{
							HID_ATTRIBUTES hidAttributes = new HID_ATTRIBUTES();
							HidD_GetAttributes(hidHandle, out hidAttributes);
							byte[] pname = new byte[NAME_LENS];
							HidD_GetProductString(hidHandle, pname, NAME_LENS);
							byte[] vname = new byte[NAME_LENS];
							HidD_GetManufacturerString(hidHandle, vname, NAME_LENS);
							string str_pname = Encoding.Default.GetString(pname);
							string str_vname = Encoding.Default.GetString(vname);
							HIDINFO hidInfo = new HIDINFO();
							hidInfo.handle = hidHandle;
							hidInfo.path = devicePathName;
							hidInfo.pid = hidAttributes.ProductID;
							hidInfo.vid = hidAttributes.VendorID;
							hidInfo.pname = str_pname.Replace("\0", "");
							hidInfo.vname = str_vname.Replace("\0", "");
							HIDInfoList.Add(hidInfo);
							device_found++;
						}
					}
				}
				device_index++;
			}
			return device_found;
		}

		public USBDriver()
		{
			HIDInfoList = new List<HIDINFO>();
		}


		/// <summary>
		/// 获取HID设备数量
		/// </summary>
		/// <returns></returns>
		public int Count
		{
			get { return HIDSearch(255); }
		}

		/// <summary>
		/// 获取HID设备列表
		/// </summary>
		public List<HIDINFO> List
		{
			get { return HIDInfoList; }
		}

		/// <summary>
		/// 刷新HID设备列表
		/// </summary>
		public void Refresh()
		{
			HIDSearch(255);
		}

		/// <summary>
		/// 可写
		/// </summary>
		public bool CanWrite { get { return _CanWrite; } }
		private bool _CanWrite = false;
		/// <summary>
		/// 可读
		/// </summary>
		public bool CanRead { get { return _CanRead; } }
		private bool _CanRead = false;
		/// <summary>
		/// 接收长度
		/// </summary>
		public ulong RecvLen { get { return _RecvLen; } set { _RecvLen = value; } }
		private ulong _RecvLen = 0;


		/// <summary>
		/// 连接到指定设备
		/// </summary>
		/// <param name="n">设备号</param>
		/// <returns>是否连接成功</returns>
		public bool Connect(int n)
		{
			IntPtr handle = IntPtr.Zero;
			handle = CreateFile(HIDInfoList[n].path, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, IntPtr.Zero);
			if (handle != IntPtr.Zero && handle.ToInt32() != -1)
			{
				try
				{
					HIDInfoList[n].fs = new FileStream(new SafeFileHandle(handle, true), FileAccess.ReadWrite, 64, true);
					_CanRead = true;
					_CanWrite = true;
					return true;
				}
				catch (Exception)
				{
					_CanRead = false;
					_CanWrite = false;
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 连接到指定pid,vid设备
		/// </summary>
		/// <param name="vid">厂商ID</param>
		/// <param name="pid">产品ID</param>
		/// <returns>设备号</returns>
		public int Connect(int vid, int pid)
		{
			int n = Count;
			for (int i = 0; i < n; i++)
			{
				if (List[i].vid == vid && List[i].pid == pid)
				{
					if (Connect(i))
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>
		/// 异步发送
		/// </summary>
		/// <param name="n">设备号</param>
		/// <param name="s">要发送的64位byte[]</param>
		public async void Send(int n, byte[] s)
		{
			_CanWrite = false;
			byte[] SendBuffer = new byte[65];
			SendBuffer[0] = 0x00;
			for (int i = 0; i < 64; i++) { SendBuffer[i + 1] = s[i]; }
			try
			{
				await List[n].fs.WriteAsync(SendBuffer, 0, 65);
			}
			catch (Exception)
			{
				return;
			}
			_CanWrite = true;
		}
		/// <summary>
		/// 异步接收
		/// </summary>
		public async void Recv(int n, byte[] r, List<byte> l)
		{
			_CanRead = false;
			byte[] RecvBuffer = new byte[65];
			int count = 0;
			try
			{
				count = await List[n].fs.ReadAsync(RecvBuffer, 0, 65);
			}
			catch (Exception)
			{
				return;
			}

			if (count >= 63)
			{
				for (int i = 0; i < 64; i++) { r[i] = RecvBuffer[i + 1]; l.Add(r[i]); }
				_RecvLen++;
				if (_RecvLen > 1844674407370955160)
				{
					_RecvLen = 0;
				}
			}
			_CanRead = true;
		}
	#endregion
	}
}

	


