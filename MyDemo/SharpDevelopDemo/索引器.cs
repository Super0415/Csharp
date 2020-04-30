/*
 * 目的：学习研究用 - 索引器
 * 用户： 枫中眸
 * 日期: 2020/4/22
 * 时间: 18:13
 * 
 */
using System;
using System.Collections;
class Name										//普通类
{
	private string m_name;
	public Name(string n)
	{
		m_name = n;
	}

	public string mname							//属性
	{
		get
		{
			return m_name;
		}
		set
		{
			m_name = value;
		}
	}
}


class IndexClass
{												//数组作为存储的索引
	private string[] name = new string[10];
	public string this[int index]				//注意：index 存在范围 0-9
	{
		set{name[index] = value;m_lengh++;}
		get{return name[index];}
	}
	private int m_lengh;
	public int Lengh
	{
		get
		{
			return m_lengh;
		}
	}


}


class IndexInfo1								// Hashtable 作为存储的索引
{
	private Hashtable name = new Hashtable();	//混合类型键值,但是不好检索
	public string this[int key]				
	{
		set{name.Add(key,value);n_lengh++;}
		get{return name[key].ToString(); }
	}
	public string this[string key]				
	{
		set{name.Add(key,value);s_lengh++;}
		get{return name[key].ToString(); }
	}

	private int n_lengh;
	public int nLengh
	{
		get
		{
			return n_lengh;
		}
	}
	private int s_lengh;
	public int sLengh
	{
		get
		{
			return s_lengh;
		}
	}
}


class IndexInfo2								// Hashtable 作为存储的索引
{
	private Hashtable name = new Hashtable();	//单一类型键值，便于检索	
	public string this[int key]					//由 键 读写 值
	{
		set{name.Add(key,value);n_lengh++;}
		get{return name[key].ToString(); }
	}
	public int this[string aname]						//由 值 读写 键
	{
		set{name.Add(value,aname);s_lengh++;}
		get
		{
			foreach (DictionaryEntry e in name) {
				if(e.Value.ToString() == aname)
				{
					return Convert.ToInt32(e.Key);
				}
			}
			return -1;
		}
		
	}

	private int n_lengh;
	public int nLengh
	{
		get
		{
			return n_lengh;
		}
	}
	private int s_lengh;
	public int sLengh
	{
		get
		{
			return s_lengh;
		}
	}
}


//索引器不可以声明为 static ，属性可以为 static 索引永远属于实例成员，不能为static


class Test
{
	static void Main()
	{
		Name[] n = new Name[3];
		n[0] = new Name("零");
		n[1] = new Name("壹");
		n[2] = new Name("贰");
		foreach (Name e in n) {
			Console.WriteLine(e.mname+" ");
		}
		
		IndexClass ind = new IndexClass();
		ind[0] = "零零";
		ind[1] = "壹壹";
		ind[2] = "贰贰";
		for (int i = 0; i < ind.Lengh; i++) {
			Console.WriteLine(ind[i]);
		}
		
		IndexInfo1 info = new IndexInfo1();
		info[0] = "男";
		info[1] = "女";
		info[2] = "男";
		info[3] = "无";
		info["wewe"] = "无";
		
		Console.WriteLine(info[0]);
		Console.WriteLine(info[1]);
		Console.WriteLine(info[2]);
		Console.WriteLine(info[3]);
		Console.WriteLine(info["wewe"]);	//检索时不太好转换 int 和 string 型的键值 
		
		
		IndexInfo2 info2 = new IndexInfo2();
		info2[0] = "张";
		info2[1] = "李";
		info2[2] = "王";
		
		Console.WriteLine(info2[0]+"    "+info2["张"]);
		Console.WriteLine(info2[1]+"    "+info2["李"]);
		Console.WriteLine(info2[2]+"    "+info2["王"]);
	}

}


