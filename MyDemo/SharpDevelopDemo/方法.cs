/*
 * 由SharpDevelop创建。
 * 用户： 62536
 * 日期: 2020/4/22
 * 时间: 13:06
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
 
//#define Test1		//共享变量
//#define Test2		//普通变量
//#define Test3		//普通参数 - ref参数 - out参数
//#define Test4		//多参数
#define Test5		//值参数与引用参数
using System;
class Method
{
#if Test1	
	class TestA
	{
		public static int i = 0;		//类中变量用 static 变为共享变量
		public void Addi()
		{
			i += 1;
		}
	}

	static void Main()
	{
		TestA A = new TestA();
		A.Addi();
		TestA B = new TestA();
		B.Addi();
		
		Console.WriteLine(TestA.i);		//直接从类中提取
		
		Console.ReadLine();
	}
#elif Test2
	class TestA
	{
		public int i = 0;				//普通变量
		public void Addi()
		{
			i += 1;
		}
	}

	static void Main()
	{
		TestA A = new TestA();
		A.Addi();
		TestA B = new TestA();
		B.Addi();
		
		Console.WriteLine(A.i);
		
		Console.ReadLine();
	}
#elif Test3		//普通参数 - ref参数 - out参数
	static void ComMethod(int i)
	{
		i++;
		Console.WriteLine("普通传参！");
	}
	static void RefMethod(ref int i)
	{
		i++;
		Console.WriteLine("ref传参！（注意使用此ref前，参数需要初始化）");
	}
	static void OutMethod(out int i)
	{
		i = 0;
		i++;
		Console.WriteLine("out传参！(注意需要初始化out变量)");
	}
	static void Main()
	{
		int n = 0;
		ComMethod(n);		
		Console.WriteLine(n);
		
		int m = 0;
		RefMethod(ref m);		
		Console.WriteLine(m);
		
		int p = 0;
		OutMethod(out p);		
		Console.WriteLine(p);
		
		Console.ReadLine();
	}
#elif Test4		//多参数
	static int ComMethod(params int[] i)			
	{		
		int tem = 0;
		foreach (int e in i) {
			tem += e;
		}
		return tem;
	}
	static void Main()
	{
		//举例：
		Console.WriteLine("数据总值为：{0}",ComMethod(1,2,3,4));
		Console.WriteLine("数据总值为：{0}",ComMethod(1,2,3,4,5));		//参数不定长，需要 params 作为标识
		
		Console.WriteLine("请输入多个数据(以空格隔开)：");
		string str = Console.ReadLine();
		string[] Arrstr = str.Split(' ');
		int[] temp = new int[Arrstr.Length];
		for (int i = 0; i < Arrstr.Length; i++) {
			temp[i] = int.Parse(Arrstr[i]);
		}
		Console.WriteLine("数据总值为：{0}",ComMethod(temp));
		Console.ReadLine();
	}	
#elif Test5		//值参数与引用参数
	static void ComMethod(int i)			
	{		
		i++;
	}
	static void ComMethod(int[] i)			
	{		
		for (int j = 0; j < i.Length; j++) {
			i[j] = j;
		}
	}
	static void ComMethod(string i)			
	{		
		i = "asdfghj";
	}
	static void Main()
	{
		int temp1 = 0;
		ComMethod(temp1);								//值传递类型
		Console.WriteLine("数据为：{0}",temp1);
		
		
		
		int[] temp2 = {12,23,34,45,56,67};
		ComMethod(temp2);								//数组名为应用类型
		Console.Write("数据为：");
		foreach (int e in temp2) {
			Console.Write(e+" ");
		}
		Console.WriteLine();
		
		
		
		string temp3 = "qazxsw";
		ComMethod(temp3);								//字符串为引用类型，但字符串无法修改
		Console.WriteLine(temp3);
		
		Console.ReadLine();
	}		
	
	
	
#endif

}
