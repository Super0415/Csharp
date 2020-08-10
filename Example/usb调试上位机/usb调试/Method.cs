using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace usbtest
{
	class Method
	{
		private static int bmp_m_size = 153600;

		/// <summary>
		/// 字符串转字节数组
		/// </summary>
		/// <param name="s"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool Str2Buf(string s, out byte[] b)
		{
			b = new byte[64];
			int len = s.Length;
			if (len > 128) len = 128;
			for (int i = 0; i < len / 2; i++)
			{
				try
				{
					b[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
				}
				catch (Exception)
				{
					return false;
				}
			}
			return true;
		}



		/// <summary>
		/// 图片转数组
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public static List<byte> Load(Image img)
		{
			byte[] arr_bmp = new byte[bmp_m_size];
			Bitmap bmp = new Bitmap(240, 320, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(bmp);
			g.DrawImage(img, 0, 0, 240, 320);
			img.Dispose();
			g.Dispose();

			int n;
			ushort v = 0x0000;
			byte R, G, B;
			int h = 320;
			int w = 240;
			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					n = (i * w + j) * 2;
					R = bmp.GetPixel(j, i).R;
					G = bmp.GetPixel(j, i).G;
					B = bmp.GetPixel(j, i).B;
					v = 0;
					v |= (ushort)(R >> 3 << 11);
					v |= (ushort)(G >> 2 << 5);
					v |= (ushort)(B >> 3);
					arr_bmp[n] = (byte)(v >> 8);
					arr_bmp[n + 1] = (byte)v;
				}
			}
			List<byte> B1 = arr_bmp.ToList();
			bmp.Dispose();
			return B1;
		}

		/// <summary>
		/// 二进制转数组
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static List<byte> LoadBin(string path)
		{
			FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
			BinaryReader br = new BinaryReader(fs);
			byte[] bSend = new byte[(int)fs.Length];
			bSend = br.ReadBytes((int)fs.Length);
			List<byte> B = bSend.ToList();
			br.Close();
			fs.Close();
			return B;
		}
	}
}
