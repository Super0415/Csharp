/*
 * 由SharpDevelop创建。
 * 用户： 62536
 * 日期: 2020/4/22
 * 时间: 9:04
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
//#define test1	//同一个类中新增数组，static
//#define test2	//不同类中新增数组 static
//#define test3	//使用 System.Collections 中的 arraylist 实现增删改
//#define test4	//多维数组
#define test5	//不规则数组/交叉数组
using System;
using System.Collections;

namespace Sharp1
{
#if test1	//同一个类中新增数组，static
	class Program
	{
		public static void CreateArr(int num)
		{
			int[] arr = new int[num];
				for (int i = 0; i < num; i++) {
				arr[i] = i;
				Console.WriteLine(arr[i]);
				}
				
		}
		public static void Main(string[] args)
		{
			
			int i = 1;
			while(i>0)
			{
				Console.Write("Please enter the array's length:");
				i = int.Parse(Console.ReadLine());
				CreateArr(i);
			}
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}


#elif test2	//不同类中新增数组 static

	class MySelfTest
	{	
			public void CreateArr(int num)
			{
				int[] arr = new int[num];
					for (int i = 0; i < num; i++) {
					arr[i] = i;
					Console.WriteLine(arr[i]);
					}
					
				Console.WriteLine("我在新类！");
			}
	
	}
	class Program
	{

		public static void Main(string[] args)
		{
			MySelfTest arr = new MySelfTest();
			int i = 1;
			while(i>0)
			{
				Console.Write("Please enter the array's length:");
				i = int.Parse(Console.ReadLine());
				arr.CreateArr(i);
			}
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}

#elif test3	//使用 System.Collections 中的 arraylist 实现增删改

	class Program
	{

		public static void Main(string[] args)
		{
			ArrayList arr = new ArrayList();
			while(true)
			{
				Console.WriteLine("Please input string:");
				string str = Console.ReadLine();
				if(str == "end")
				{
					Console.WriteLine("数组中存储信息如下：");
					foreach (string s in arr) {
					Console.WriteLine(s);
					}
					break;
				}			
				else if(str == "del")
				{
					Console.WriteLine("删除信息:");
					string info = Console.ReadLine();
					arr.Remove(info);	//删
					continue;
				}
				else if(str == "exchange")
				{
					Console.WriteLine("数组中存储信息如下：");
					foreach (string s in arr) {
					Console.WriteLine(s);
					}
					Console.WriteLine("请填写目标信息:");
					string infoS = Console.ReadLine();
					Console.WriteLine("请填写修改信息:");
					string infoO = Console.ReadLine();
					arr[arr.IndexOf(infoS)] = infoO;		//查 改
					continue;
				}
					
				arr.Add(str);		//增
			}
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}

#elif test4	//多维数组

	class Program
	{

		public static void Main(string[] args)
		{
			int X = 0, Y = 0;
			while(true)
			{
				Console.WriteLine("请输入多维数组的行列数（例如：2,3）");
				string str = Console.ReadLine();
				if(str == "end") break;
				try
				{
					string[] s = str.Split(',');
					X = int.Parse(s[0]);
					Y = int.Parse(s[1]);
				}
				catch
				{
					Console.WriteLine("输入格式不正确，请重新输入...");
					continue;
				}
				int[,] arr = new int[X,Y];
				for (int i = 0; i < X; i++) {
					for (int j = 0; j < Y; j++) {
						arr[i,j] = i+j;
					}
				}			
				for (int i = 0; i < X; i++) {
					for (int j = 0; j < Y; j++) {
						Console.Write("arr[{0},{1}] = {2}  ",i,j,arr[i,j]);
					}
					Console.WriteLine();
				}
			}

			

			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
#elif test5	//不规则数组/交叉数组

	class Program
	{

		public static void Main(string[] args)
		{
			int[][] arr = new int[3][];			
			arr[0] = new int[]{1,2,3,4,5};
			arr[1] = new int[]{11,12,};
			arr[2] = new int[]{21,22,23,24,25,26,27};
			
			foreach (int[] element in arr) {
				foreach (int e in element) {
					Console.Write(e+" ");
				}
				Console.WriteLine();
			}
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
#endif


}