/*
 * 由SharpDevelop创建。
 * 用户： 62536
 * 日期: 2020/4/22
 * 时间: 11:38
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
 //抽象类
 
 using System;
 namespace A
 {

	 abstract class father	//抽象类只能作为基类，不能实现方法
	 {
	 	protected int _x;
	 	public abstract void FunA();  	//抽象方法需要 abstract，可以被继承需要 public，抽象方法必须声明在抽象类中
	 
	 	public abstract int X
	 	{
	 		get;
	 		set;
	 	}
	 }
	 
	 class Son:father	//抽象类只能作为基类，不能实现方法
	 {
	 	public override void FunA()		//继承父类抽象类，需要 override
	 	{
	 		Console.WriteLine("I am you father!");
	 	}
	 	
	 	public override int X
	 	{
	 		get
	 		{
	 			return _x;
	 		}
	 		set
	 		{
	 			_x = value;
	 		}
	 	}
	 } 
	 
	 	
	 class Test
	 {
	 	static void Main()
	 	{
	 		Son s = new Son();
	 		s.FunA();
	 		s.X = 10;
	 		Console.WriteLine(s.X);
	 		Console.WriteLine("please press any key...");
	 		Console.ReadKey();
	 	}
	 }
 
 }

 
 
 
 
 
 
 
 

