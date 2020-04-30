#define MyTest 
#undef MySign 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS0168
namespace 预处理
{ 
    class Program
    {
        [Obsolete("过期了", false)]
        public static void aa()
        {

        }

        static void Main(string[] args)
        {
            aa();
            #line 20 "Special"
            int i;
            int j;
            Console.WriteLine("#Line 测试 #1");
            #region 折叠#line default
#line default
            int k;
            Console.WriteLine("#Line 测试 #2");
            int m;
            Console.WriteLine("#Line 测试 #3");
            #endregion
            Console.WriteLine("#Line 测试 #4");

            Console.ReadKey();
            Console.WriteLine("#Line 测试 按键1");

            Console.ReadKey();
            Console.WriteLine("#Line 测试 按键2");

            Console.ReadKey();

            //#warning Test My Warning1. 
            //#warning Test My Warning2. 
            //#error Test My Error

            //            //第一次预定义测试
            //#if (!MyTest)
            //            Console.WriteLine("MyTest未定义");
            //#endif

            //#if (MyTest)
            //            Console.WriteLine("MyTest定义成功");
            //#endif

            //            //第二次预定义测试
            //#if (MyTest && MySign)
            //                        Console.WriteLine("MyTest定义成功,MySign定义成功");
            //#elif (MyTest && !MySign)
            //                        Console.WriteLine("MyTest定义成功,MySign定义失败");
            //#elif (!MyTest && !MySign)
            //            Console.WriteLine("MyTest定义失败,MySign定义失败");
            //#else
            //                        Console.WriteLine("MyTest定义失败,MySign定义失败");
            //#endif

            //            //第三次预定义测试
            //#if (!MyTest || MySign)
            //            Console.WriteLine("MyTest定义失败,MySign定义成功");
            //#elif (!MyTest && !MySign)
            //            Console.WriteLine("MyTest定义成功,MySign定义失败");
            //#endif






            //            //第一次预定义测试
            //#if (MyTest)
            //            Console.WriteLine("MyTest定义成功");
            //#endif
            //#if (MySign)
            //            Console.WriteLine("MyTest定义成功");
            //#endif
            //#if (!MySign)
            //            Console.WriteLine("MySign未定义");
            //#endif

            //            //第二次预定义测试
            //#if (MyTest && MySign)
            //                        Console.WriteLine("MyTest定义成功,MySign定义成功");
            //#elif (MyTest && !MySign)
            //                        Console.WriteLine("MyTest定义成功,MySign定义失败");
            //#elif (!MyTest && MySign)
            //                        Console.WriteLine("MyTest定义失败,MySign定义成功");
            //#else
            //            Console.WriteLine("MyTest定义失败,MySign定义失败");
            //#endif
            //            //第三次预定义测试
            //#if (MyTest || MySign)
            //                        Console.WriteLine("MyTest定义成功,MySign定义成功");
            //#elif (MyTest && !MySign)
            //                        Console.WriteLine("MyTest定义成功,MySign定义失败");
            //#endif
        }
    }
}
