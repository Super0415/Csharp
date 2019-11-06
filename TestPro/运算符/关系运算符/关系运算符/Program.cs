using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关系运算符
{
    class Program
    {
        static void Main(string[] args)
        {
            //关系运算符有六种 “==”、“！=”、“>”、“<”、“>=”、“<=”

            TESTCODE:
            Console.WriteLine("输入两个数据，以空格隔开(输入T，退出):");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            try
            {
                while (true)
                {
                    Console.Write("输入两个数据a、b：");
                    string str = Console.ReadLine();
                    if (str.Equals("T")) break;

                    int a = Convert.ToInt32(str.Split(' ')[0]);
                    int b = Convert.ToInt32(str.Split(' ')[1]);
                    Console.WriteLine("演示：“<”、“>”、“<=”、“>=”、“==”、“!=”");
                    if (a < b) Console.WriteLine("a < b");
                    else Console.WriteLine("a 不小于 b");

                    if (a > b) Console.WriteLine("a > b");
                    else Console.WriteLine("a 不大于 b");

                    if (a <= b) Console.WriteLine("a <= b");
                    else Console.WriteLine("a 不小于等于 b");

                    if (a >= b) Console.WriteLine("a >= b");
                    else Console.WriteLine("a 不大于等于 b");

                    if (a == b) Console.WriteLine("a == b");
                    else Console.WriteLine("a 不等于 b");

                    if (a != b) Console.WriteLine("a != b");
                    else Console.WriteLine("a 等于 b");

                    Console.WriteLine();
                }
            }
            catch
            {
                Console.WriteLine("格式错误，请重新输入！");
                goto TESTCODE;
            }

        }





        //static void Main(string[] args)
        //{
        //    //关系运算符有六种 “==”、“！=”、“>”、“<”、“>=”、“<=”

        //    TESTCODE:
        //    Console.WriteLine("输入两个数据，以空格隔开(输入T，退出):");
        //    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
        //    try
        //    {
        //        while (true)
        //        {
        //            Console.Write("输入两个数据a、b：");
        //            string str = Console.ReadLine();
        //            if (str.Equals("T")) break;

        //            int a = Convert.ToInt32(str.Split(' ')[0]);
        //            int b = Convert.ToInt32(str.Split(' ')[1]);
        //            Console.WriteLine("演示：“<”、“>=”");
        //            if (a < b) Console.WriteLine("a < b");
        //            else Console.WriteLine("a 不小于 b");

        //            if (a >= b) Console.WriteLine("a >= b");
        //            else Console.WriteLine("a 不大于等于 b");

        //            Console.WriteLine();
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("格式错误，请重新输入！");
        //        goto TESTCODE;
        //    }

        //}




        //static void Main(string[] args)
        //{
        //    //关系运算符有六种 “==”、“！=”、“>”、“<”、“>=”、“<=”

        //    TESTCODE:
        //    Console.WriteLine("输入两个数据，以空格隔开(输入T，退出):");
        //    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
        //    try
        //    {
        //        while (true)
        //        {
        //            Console.Write("输入两个数据a、b：");
        //            string str = Console.ReadLine();
        //            if (str.Equals("T")) break;

        //            int a = Convert.ToInt32(str.Split(' ')[0]);
        //            int b = Convert.ToInt32(str.Split(' ')[1]);
        //            Console.WriteLine("演示：“==”、“!=”");
        //            if (a == b) Console.WriteLine("a 等于 b");
        //            else Console.WriteLine("a 不等于 b");
        //            Console.WriteLine();

        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("格式错误，请重新输入！");
        //        goto TESTCODE;
        //    }

        //}
    }
}
