using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace usbtest
{
	public partial class Form1 : Form
	{

		USBDriver usb;
		private bool bRun;
		private int nSelect = -1;
		byte[] SendBuffer;
		byte[] RecvBuffer;
		StringBuilder sbInfo;
		long sendNum = 0;
		displayMode dm;
		List<byte> bSend;
		string Debug;
		string[] strDevice;
		List<byte> l_recvbuffer;
		private bool noDisplay;

		

		enum displayMode
		{
			refresh,//刷新显示
			append //追加显示
		}

		public Form1()
		{
			InitializeComponent();
			usb = new USBDriver();
			SendBuffer = new byte[64];
			RecvBuffer = new byte[64];
			LastBuffer = new byte[64];
			sbInfo = new StringBuilder();
			dm = displayMode.append;
			bSend = new List<byte>();
			l_recvbuffer = new List<byte>();
		}

		private void RefreshList()
		{
			int n = usb.Count;              // 若工程设置中 优选 32位 平台，可能导致 SetupDiGetClassDevs 找到的设备信息不准确
            if (n == 0)
			{
				lbl_status.Text = "未找到设备";
				lbl_status.ForeColor = Color.Black;
				cb_devices.Enabled = false;
			}
			else
			{
				lbl_status.Enabled = true;
				cb_devices.Items.Clear();
				for (int i = 0; i < n; i++)
				{
					string strDisplay = i.ToString() + "  " + usb.List[i].pname;
					cb_devices.Items.Add(strDisplay);
				}
				lbl_status.Text = "找到" + n + "个设备";
				lbl_status.ForeColor = Color.Black;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			RefreshList();
		}

		private void btn_clearRecv_Click(object sender, EventArgs e)
		{
			tb_recv.Text = "";
			Array.Clear(RecvBuffer, 0, 64);
		}

		private void btn_clearSend_Click(object sender, EventArgs e)
		{
			tb_send.Text = "";
			Array.Clear(SendBuffer, 0, 64);
		}

		private void cb_devices_SelectedIndexChanged(object sender, EventArgs e)
		{
			bRun = false;
			timerSends.Enabled = bRun;
			btn_sends.ForeColor = (bRun) ? Color.DarkGreen : Color.DarkRed;
			nSelect = -1;
			strDevice = cb_devices.Text.Split(' ');
			Int32.TryParse(strDevice[0], out nSelect);
			if (nSelect >= 0)
			{
				if (usb.Connect(nSelect))
				{
					tb_pid.Text = usb.List[nSelect].pid.ToString("X4");
					tb_vid.Text = usb.List[nSelect].vid.ToString("X4");
					return;
				}
			}

		}

		private void btn_search_Click(object sender, EventArgs e)
		{
			usb.Refresh();
			RefreshList();
		}

		private void btn_send_Click(object sender, EventArgs e)
		{
			if (bConnected == false) return;
            int n;
			int.TryParse(tb_ms.Text, out n);

			if (bfile )
			{
				tb_send.AppendText("正在发送文件...每包延时" + n + "ms");
				SetDataWait(ref bSend, n);
				bfile = false;
			}
			else
			{
				DataSend();
			}

		}


		private void DataSend()
		{
			if (nSelect < 0)
			{
				lbl_status.Text = "未连接设备！";
				lbl_status.ForeColor = Color.Red;
				return;
			}
			string s = tb_send.Text.Replace(" ", "");
			if (Method.Str2Buf(s, out SendBuffer) == false)
			{
				timerSends.Stop();
				timerRecv.Stop();
				tb_info.AppendText("输入字符串格式不正确！\r\n");
				return;
			}

			if (usb.CanWrite)
			{
				usb.Send(nSelect, SendBuffer);
			}
			else if (usb.CanWrite == false)
			{
				timerSends.Stop();
				timerRecv.Stop();
				lbl_status.Text = "异常！连接失败！";
				lbl_status.ForeColor = Color.Red;
				tb_info.AppendText("设备已断开连接!\r\n");
				return;
			}			
			
			if (!noDisplay)
			{
				sendNum += 64;
				tb_info.AppendText("已发送" + sendNum + "字节\r\n");
				tb_info.ScrollToCaret();
			}			
		}

		private void btn_clearInfo_Click(object sender, EventArgs e)
		{
			tb_info.Text = "";
		}

		private void btn_clearCount_Click(object sender, EventArgs e)
		{
			sendNum = 0;
			recvLen = 0;
			usb.RecvLen = 0;
		}

		ulong recvLen = 0;
		byte[] LastBuffer;
		private void timerRecv_Tick(object sender, EventArgs e)
		{
			usb.Recv(nSelect, RecvBuffer, l_recvbuffer);
			if (recvLen == usb.RecvLen) return;
			recvLen = usb.RecvLen;
			if (!noDisplay)
			{
				tb_info.AppendText("已接收" + recvLen * 64 + "个字节\r\n");
			}
			
			if (dm == displayMode.refresh)
			{
				tb_recv.Text = "";
				foreach (var item in l_recvbuffer)
				{
					tb_recv.Text += (item.ToString("X2") + " ");
				}

			}
			else
			{
				foreach (var item in l_recvbuffer)
				{
					tb_recv.AppendText(item.ToString("X2") + " ");
				}
			}
			Thread.Sleep(100);
			l_recvbuffer.Clear();
		}

		

		bool recvb = true;      //是否开始接收
		private void btn_stopRecv_Click(object sender, EventArgs e)
		{
			if (recvb)
			{
				btn_stopRecv.Text = "开始接收";
				timerRecv.Stop();
				recvb = false;
			}
			else
			{
				btn_stopRecv.Text = "停止接收";
				timerRecv.Start();
				recvb = true;
			}
		}

		bool cycleSend = false;     //是否周期发送
		private void btn_sends_Click(object sender, EventArgs e)
		{
			if (bConnected == false) return;
			if (cycleSend == false)
			{
				btn_sends.Text = "停止";
				btn_sends.ForeColor = Color.Red;
				timerSends.Start();
				cycleSend = true;
			}
			else
			{
				btn_sends.Text = "周期发送";
				btn_sends.ForeColor = Color.Black;
				timerSends.Stop();
				cycleSend = false;
			}
		}

		int r = 1;
		private void timerSends_Tick(object sender, EventArgs e)
		{
            int interval;

            int.TryParse(tb_ms.Text, out interval);
			timerSends.Interval = interval;
			if(r==1)
			{
				SendBuffer[0] = 0xff;
				SendBuffer[1] = 0x00;
				SendBuffer[2] = 0xff;
				SendBuffer[3] = 0x00;
				usb.Send(nSelect, SendBuffer);
				r = 2;
				return;
			}
			if (r == 2)
			{
				SendBuffer[0] = 0x00;
				SendBuffer[1] = 0xff;
				SendBuffer[2] = 0xff;
				SendBuffer[3] = 0x00;
				usb.Send(nSelect, SendBuffer);
				r = 3;
				return;
			}
			if (r == 3)
			{
				SendBuffer[0] = 0x00;
				SendBuffer[1] = 0xff;
				SendBuffer[2] = 0x00;
				SendBuffer[3] = 0xff;
				usb.Send(nSelect, SendBuffer);
				r = 4;
				return;
			}
			if (r == 4)
			{
				SendBuffer[0] = 0xff;
				SendBuffer[1] = 0x00;
				SendBuffer[2] = 0x00;
				SendBuffer[3] = 0xff;
				usb.Send(nSelect, SendBuffer);
				r = 1;
				return;
			}
			//DataSend();
		}

		private void tb_recv_DoubleClick(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "文本文件|*.txt|所有文件|*.*";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fName = saveFileDialog.FileName;
				File.WriteAllText(fName, tb_recv.Text);
				tb_info.AppendText("文件已成功保存到" + fName);
			}
		}

		bool bfile = false;     //是否发送文件
		private void tb_send_DoubleClick(object sender, EventArgs e)
		{
			bSend.Clear();
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "所有文件|*.*|图片|*.jpg;*.bmp;*.png|bin文件|*.bin";
			openFileDialog.Title = "请选择要发送的文件";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fName = openFileDialog.FileName;
				string x = Path.GetExtension(fName);
				if (x.Equals(".png") || x.Equals(".jpg") || x.Equals(".bmp") || x.Equals(".jpeg"))
				{
					bSend = Method.Load(Image.FromFile(fName));
				}
				else
				{
					bSend = Method.LoadBin(fName);
				}
				tb_info.AppendText("已选择文件" + fName + "\r\n" + "文件大小:" + bSend.Count + "字节\r\n");
				tb_send.Text = "成功读取文件\r\n" + fName + "\r\n";
				btn_sends.Enabled = false;
				bfile = true;
			}
		}

		/// <summary>
		/// 传图功能
		/// </summary>
		/// <param name="S">原图BIN</param>
		/// <param name="ms">每包延迟</param>
		/// <returns>最后成功</returns>
		private bool SetDataWait(ref List<byte> S, int ms)
		{
			List<byte> B = S;
			int i = 0, max = S.Count / 64;
			pBar1.Visible = true;
			pBar1.Minimum = 1;
			pBar1.Maximum = max;
			pBar1.Value = 1;
			pBar1.Step = 1;
			for (i = 0; i < max; i++)
			{
				for (int k = 0; k < 64; k++)
				{
					SendBuffer[k] = B[i * 64 + k];
				}
				usb.Send(nSelect, SendBuffer);
				Debug = (i + 1) + "/" + max;
				pBar1.PerformStep();
				Thread.Sleep((int)ms);
			}
			if (S.Count % 64 != 0)
			{
				SendBuffer = new byte[64];
				for (int j = 0; j < S.Count % 64; j++)
				{
					SendBuffer[j] = B[i * 64 + j];
				}
				usb.Send(nSelect, SendBuffer);
			}

			/*var task = new Task(() =>
			  {
				  for (i = 0; i < max; i++)
				  {
					  for (int k = 0; k < 64; k++)
					  {
						  SendBuffer[k] = B[i * 64 + k];
					  }
					  usb.Send(nSelect, SendBuffer);
					  Debug = (i + 1) + "/" + max;
					  Thread.Sleep((int)ms);
				  }
				  if (B.Count % 64 != 0)
				  {
					  SendBuffer = new byte[64];
					  for (int j = 0; j < B.Count % 64; j++)
					  {
						  SendBuffer[j] = B[i * 64 + j];
					  }
					  usb.Send(nSelect, SendBuffer);
				  }
			  });
			task.Start();
			task.Wait();*/
			pBar1.Visible = false;
			tb_info.AppendText("文件发送完毕！\r\n请重新选择要发送的图片！\r\n");
			tb_send.Text = "";
			bSend.Clear();
			btn_sends.Enabled = true;

			/*new Thread(			//用线程发送文件
				new ThreadStart(() =>
				{
					//Thread.Sleep(4000);
					for (i = 0; i < max; i++)
					{
						for (int k = 0; k < 64; k++)
						{
							SendBuffer[k] = B[i * 64 + k];
						}
						usb.Send(nSelect,SendBuffer);
						Debug= (i+1) + "/" + max;
						Thread.Sleep((int)ms);
					}
					if (B.Count % 64 != 0)
					{
						SendBuffer = new byte[64];
						for (int j = 0; j < B.Count % 64; j++)
						{
							SendBuffer[j] = B[i * 64 + j];
						}
						usb.Send(nSelect, SendBuffer);
					}
				})
				).Start();*/

			//MessageBox.Show("文件发送完毕！");			
			return true;
		}

		bool bConnected = false;
		private void btn_connect_Click(object sender, EventArgs e)
		{
			this.btn_connect.Text = bConnected ? "打开" : "关闭";
			this.lbl_status.Text = bConnected ? "" : "连接成功";
			this.pictureBox1.Image = bConnected ? Properties.Resources.close : Properties.Resources.accept;
			cb_devices.Enabled = bConnected;
			btn_search.Enabled = bConnected;
			if (bConnected == false)
			{
				strDevice = cb_devices.Text.Split(' ');
				Int32.TryParse(strDevice[0], out nSelect);
				if (nSelect >= 0)
				{
					lbl_status.Text = @"正在连接 " + strDevice[0] + " " + strDevice[1] + " " + strDevice[2];
					lbl_status.ForeColor = Color.Black;
					lbl_status.Text = @"连接成功！";
					lbl_status.ForeColor = Color.Green;
					tb_info.AppendText("连接设备" + usb.List[nSelect].pname + "成功\r\n" + "设备路径:\r\n" + usb.List[nSelect].path + "\r\n" + "厂商号:" + tb_pid.Text + "\r\n" + "设备号:" + tb_vid.Text + "\r\n");
					tb_send.Text = @"00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 ";
					timerRecv.Start();
				}
				else
				{
					lbl_status.Text = "异常！连接失败！";
					lbl_status.ForeColor = Color.Red;
				}

			}
			else
			{
				nSelect = -1;
				sendNum = 0;
				recvLen = 0;
				usb.RecvLen = 0;
				timerRecv.Stop();
				timerSends.Stop();
				tb_recv.Text = "";
				tb_send.Text = "";
				tb_info.Text = "";
			}
			bConnected = !bConnected;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			dm = checkBox1.Checked ? displayMode.refresh : displayMode.append;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			noDisplay = checkBox2.Checked;
		}
	}
}
