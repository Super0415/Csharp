using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 特殊运算符
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("sizeof 检测数据类型的大小");
            Console.WriteLine("int 的大小为{0}",sizeof(int));
            Console.WriteLine("long 的大小为{0}", sizeof(long));
            Console.WriteLine("char 的大小为{0}", sizeof(char));

            int[] a = new int[5];
            double b = new double();
            float f = new float();
            Console.WriteLine("typeof 返回 class 的类型");
            Console.WriteLine("a 的类型为{0}", typeof(a));
            Console.WriteLine("b 的类型为{0}", typeof(b));
            Console.WriteLine("f 的类型为{0}", typeof(f));

            Console.ReadKey();
        }
    }
}
