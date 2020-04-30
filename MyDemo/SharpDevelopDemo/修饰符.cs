/*
 * 目的：学习研究用
 * 用户： 枫中眸
 * 日期: 2020/4/22
 * 时间: 16:18
 * 
 */
//修饰符

using System;


sealed class ForTest
{
	protected ForTest()
	{
		Console.WriteLine("父类构造函数！");
	}

}



class Test/*:ForTest*/    //由于 sealed 修饰父类，使父类无法被继承
{
	void defaultMod()
	{
		Console.WriteLine("修饰符1： default ");
	}
	public void PublicMod()
	{
		Console.WriteLine("修饰符2： public ");
	}
	private void PrivateMod()
	{
		Console.WriteLine("修饰符3： private ");
	}
	internal void InternalMod()
	{
		Console.WriteLine("修饰符4： internal ");
	}
	protected void ProtectedMod()
	{
		Console.WriteLine("修饰符5： protected ");
	}
	protected internal void ProtectedInternalMod()
	{
		Console.WriteLine("修饰符6： protected internal ");
	}
	
	static void Main()
	{
		Test T = new Test();
		T.defaultMod();
		T.PublicMod();
		T.PrivateMod();
		T.InternalMod();
		T.ProtectedMod();
		T.ProtectedInternalMod();
	}

}





