using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 运算符
{
    class Program
    {
        static void Main(string[] args)
        {
            //算数运算符：+ - * / % ++ --
            int a = 10;
            int b = 6;
            int c = 0;
            int d = b;
            int i = 0, j = 0;
            Console.WriteLine("Line00：a = " + a);
            c = a++;  //自增（+1）
            Console.WriteLine("Line01：a++ = " + c);
            c = a--;  //自减（-1）
            Console.WriteLine("Line02：a-- = " + c);
            Console.WriteLine("Line03：c = " + c);
            Console.WriteLine("Line04：c++ = " + c++);
            Console.WriteLine("Line05：c = " + c);
            Console.WriteLine("Line06：++c = " + ++c);
            Console.WriteLine("Line07：c = " + c);
            for (i = 0; i < 3; i++)
            {
                c = b++;  //自增（+1）
                Console.WriteLine("“后自增”循环序号i = {0},自增值为：{1}", i, c);
            }
            Console.WriteLine("“后自增”循环外确认数据：循环序号i = {0},自增值为：{1}", i, c);

            for (j = 0; j < 3; ++j)
            {
                c = ++d;  //自增（+1）\
                Console.WriteLine("“前自增”循环序号j = {0},自增值为：{1}", j, c);
            }
            Console.WriteLine("“前自增”循环外确认数据：循环序号i = {0},自增值为：{1}", j, c);

            Console.ReadLine();
        }



        //static void Main(string[] args)
        //{
        //    //算数运算符： ++ --
        //    int a = 10;
        //    int b = 10;
        //    int c = 0;
        //    int d = b;
        //    Console.WriteLine("假定a = 10，b = 10，则算数运算符如下：");

        //    c = a--;  //自减（后）
        //    Console.WriteLine("c = a-- = " + c);
        //    Console.WriteLine("c = " + c);
        //    c = --b;  //自减（前）
        //    Console.WriteLine("c = --b = " + c);
        //    Console.WriteLine("c = " + c);

        //    Console.ReadLine();
        //}





        //static void Main(string[] args)
        //{
        //    //算数运算符：  --
        //    int a = 10;
        //    int b = 10;
        //    int c = 0;
        //    int d = b;
        //    Console.WriteLine("假定a = 10，b = 10，则算数运算符如下：");

        //    c = a++;  //自增（后）
        //    Console.WriteLine("c = a++ = " + c);
        //    Console.WriteLine("c = " + c);
        //    c = ++b;  //自增（前）
        //    Console.WriteLine("c = ++b = " + c);
        //    Console.WriteLine("c = " + c);

        //    Console.ReadLine();
        //}


        //static void Main(string[] args)
        //{
        //    //算数运算符：+ - * / % ++ --
        //    int a = 10;
        //    int b = 6;
        //    int c = 0;
        //    int d = b;
        //    int i = 0, j = 0;

        //    Console.WriteLine("假定a = 10，b = 6，则算数运算符如下：");
        //    c = a + b;
        //    Console.WriteLine("a + b = {0}", c);
        //    c = a - b;
        //    Console.WriteLine("a - b = " + c);
        //    c = a * b;
        //    Console.WriteLine("a * b = " + c);
        //    c = a / b;  //取商数
        //    Console.WriteLine("a / b = " + c);
        //    c = a % b;  //取余数
        //    Console.WriteLine("a % b = " + c);
        //    c = a++;  //自增（+1）
        //    Console.WriteLine("a++ = " + c);
        //    c = a--;  //自减（-1）
        //    Console.WriteLine("a-- = " + c);

        //    for (i = 0; i < 3; i++)
        //    {
        //        c = b++;  //自增（+1）
        //        Console.WriteLine("“后自增”循环序号i = {0},自增值为：{1}", i, c);
        //    }
        //    Console.WriteLine("“后自增”循环外确认数据：循环序号i = {0},自增值为：{1}", i, c);

        //    for (j = 0; j < 3; ++j)
        //    {
        //        c = ++d;  //自增（+1）\
        //        Console.WriteLine("“前自增”循环序号j = {0},自增值为：{1}", j, c);
        //    }
        //    Console.WriteLine("“前自增”循环外确认数据：循环序号i = {0},自增值为：{1}", j, c);

        //    Console.ReadLine();
        //}
    }
}













