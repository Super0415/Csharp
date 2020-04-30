/*
 * 目的：学习研究用-构造器
 * 用户： 枫中眸
 * 日期: 2020/4/22
 * 时间: 14:33
 * 
 */
//#define Test1	//构造器传参
#define Test2	//构造器父类-子类

using System;


class Test
{
#if Test1
	
	class TestA
	{
		public int A;
		public string B;
		public TestA()
		{
		
		}
		public TestA(int n)
		{
			this.A = n;
		}
		public TestA(int n,string s)
		{
			this.A = n;
			this.B = s;
		}
		public void showA()
		{
			Console.WriteLine("A的数值为：{0}",A);
		}
		public void showB()
		{
			Console.WriteLine("字符串B为：{0}",B);
		}
		
	}
	
	static void Main()
	{
		TestA T1 = new TestA();		
		T1.showA();
		T1.showB();
		
		TestA T2 = new TestA(99);		
		T2.showA();
		T2.showB();
		
		TestA T3 = new TestA(9,"Feng");		
		T3.showA();
		T3.showB();
		
		Console.ReadLine();
	}

#elif Test2

	class TestA
	{
		public TestA()
		{
			Console.WriteLine("父类无参构造器");
		}
		public TestA(int a)
		{
			Console.WriteLine("父类有1参构造器");
		}
		public TestA(int a1, int a2)
		{
			Console.WriteLine("父类有2参构造器");
		}
	}
	
	class TestB:TestA
	{
		public TestB()
		{
			Console.WriteLine("子类无参构造器");
		}
		public TestB(int b)
		{
			Console.WriteLine("子类有1参构造器");
		}
		public TestB(int b1,int b2):base(b1,b2)
		{
			Console.WriteLine("子类有2参构造器");
		}
	}

	static void Main()
	{
		TestB N = new TestB(1);		//经过 父无参，子1参
		TestB M = new TestB(1,2);	//经过 父2参，子2参	base 传参给父类
		
		Console.ReadLine();
	}
	
#endif	
	
}


