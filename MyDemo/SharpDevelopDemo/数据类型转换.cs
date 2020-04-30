/*
 * 目的：学习研究用
 * 用户： 枫中眸
 * 日期: 2020/4/22
 * 时间: 16:46
 * 
 */

//数据类型转换
//#define Test1
#define Test2

using System;


#if Test1
class Test
{
	static void Main()
	{
		int A = 50;
		long B = 700000000000;
		long C = 100;
		
		A = checked((int)B);			// checked 抛出溢出异常  unchecked 不必抛出异常
		Console.WriteLine("转换3");
		Console.WriteLine(A+" - "+B);
		
		B = A;
		Console.WriteLine("转换1");
		Console.WriteLine(A+" - "+B);
		
		checked							//搭配try..catch使用
		{
			A = (int)C;
			Console.WriteLine("转换2");
			Console.WriteLine(A+" - "+C);	
		}
		

		
		Console.ReadLine();
	}

}

#elif Test2

class father
{
	public father()
	{
		Console.WriteLine("我是父类构造函数");
	}
}

class son:father
{
	public int a = 99;
	public son()
	{
		Console.WriteLine("我是子类构造函数");
	}
	

}


class Test
{
	static void Main()
	{
		father f = new son();
		son s = (son)f;
		Console.WriteLine(s.a);
		Console.WriteLine(((son)f).a);
		
		
		Console.WriteLine(f is father);
		Console.WriteLine(f is son);
		Console.WriteLine(s is father);
		Console.WriteLine(s is son);
		
		father t1 = new father();
		son t2 = new son();
		Console.WriteLine(t1 is father);
		Console.WriteLine(t1 is son);
		Console.WriteLine(t2 is father);
		Console.WriteLine(t2 is son);
		
		Console.ReadLine();
	}
}






#endif


