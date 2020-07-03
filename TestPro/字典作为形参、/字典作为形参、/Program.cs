using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace 字典作为形参_
{
    class Test
    {
        public static void ChangeDic()
        {
            Dictionary<int, int> d = Program.dic;
            if (!d.ContainsKey(99))
            {
                d.Add(99, 299);
                Console.WriteLine("新增特殊元素：[99,299]");
            }
            else
            {
                d.Add(100, 300);
                Console.WriteLine("新增特殊元素：[100,300]！");
            }

            Console.WriteLine("字典：{2} - 长度：{0} - 字典内容：{1}", d.Count, (d.Count > 0) ? "" : "空", "d");
            if (d.Count > 0)
            {
                foreach (int itemKey in d.Keys)
                {
                    Console.Write(" [{0},{1}] ", itemKey, d[itemKey]);
                }
            }
        }
    }

    class Program
    {

        static void IncreaseDic(Dictionary<int, int> d)
        {
            if (!d.ContainsKey(99))
            {
                d.Add(99, 299);
                Console.WriteLine("新增特殊元素：[99,299]");
            }
            else
            {
                Console.WriteLine("未增加新元素！");
            }
        }

        public static Dictionary<int, int> dic = new Dictionary<int, int>();
        static void Main(string[] args)
        {

            dic[0] = 100;
            dic[1] = 101;
            dic[2] = 102;

            //Dictionary<int, int> dic1 = new Dictionary<int, int>();
            //Dictionary<int, int> dic1 = dic;
            Dictionary<int, int> dic1 = new Dictionary<int, int>(dic);

            dic1[1] = 201;

            dic1[3] = 203;
            dic1[4] = 204;

            Dictionary<int, int> dic2 = new Dictionary<int, int>();
            foreach (int itemKey in dic.Keys)
            {
                dic2.Add(itemKey, dic[itemKey]);
            }

            ConcurrentDictionary<int, int> test = new ConcurrentDictionary<int, int>();

            Console.WriteLine("字典：{2} - 长度：{0} - 字典内容：{1}", dic.Count, (dic.Count > 0) ? "" : "空", "dic");
            if (dic.Count > 0)
            {
                foreach (int itemKey in dic.Keys)
                {
                    Console.Write(" [{0},{1}] ", itemKey, dic[itemKey]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("字典：{2} - 长度：{0} - 字典内容：{1}", dic1.Count, (dic1.Count > 0) ? "" : "空", "dic1");
            if (dic1.Count > 0)
            {
                foreach (int itemKey in dic1.Keys)
                {
                    Console.Write(" [{0},{1}] ", itemKey, dic1[itemKey]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("字典：{2} - 长度：{0} - 字典内容：{1}", dic2.Count, (dic1.Count > 0) ? "" : "空", "dic2");
            if (dic2.Count > 0)
            {
                foreach (int itemKey in dic2.Keys)
                {
                    Console.Write(" [{0},{1}] ", itemKey, dic2[itemKey]);
                }
            }


            Console.ReadKey();
        }
    }
}










//static void DealADic(Dictionary<int, int> d)
//{
//    d.Add(1,10);
//    d.Add(2, 10);
//}

//static void DealBDic(Dictionary<int, int> d)
//{
//    d.Add(3, 10);
//    d.Add(4, 10);
//}

//static void Main(string[] args)
//{
//    Dictionary<int, int> dic = new Dictionary<int, int>();
//    DealADic(dic);

//    Dictionary<int, int> dic1 = dic;
//    DealBDic(dic1);

//    Console.WriteLine("dic字典长度:" + dic.Count);
//    if (dic.Count > 0)
//    {
//        int temp = 0;
//        foreach (var item in dic)
//        {
//            Console.WriteLine("Key{0}:{1} - Value{0}:{2}" , temp++, item.Key,item.Value);

//        }
//    }

//    Console.WriteLine("dic1字典长度:" + dic1.Count);
//    if (dic1.Count > 0)
//    {
//        int temp = 0;
//        foreach (var item in dic1)
//        {
//            Console.WriteLine("Key{0}:{1} - Value{0}:{2}", temp++, item.Key, item.Value);

//        }
//    }

//    Console.ReadKey();
//}
