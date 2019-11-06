using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 逻辑运算与位运算
{
    class Program
    {
        static void Main(string[] args)
        {
            //逻辑运算： &&(与) 、||(或)、 ！(非)

            Console.WriteLine("(规定内部标准a=10、b=20)\n");
            while (true)
            {
                Console.Write("请输出两个数据，以空格隔开：");
                string str = Console.ReadLine();
                int a = Convert.ToInt32(str.Split(' ')[0]);
                int b = Convert.ToInt32(str.Split(' ')[1]);

                if (a == 10 && b == 20)
                    Console.WriteLine("“a == 10 && b == 20”下，条件均为真");
                else
                    Console.WriteLine("“a == 10 && b == 20”下，条件不均为真");

                if (a == 10 || b == 20)
                    Console.WriteLine("“a == 10 || b == 20”下，条件至少有一个为真");
                else
                    Console.WriteLine("“a == 10 || b == 20”下，条件均不为真");

                if (a != 10)
                    Console.WriteLine("“a != 10”下，条件为真");
                else
                    Console.WriteLine("“a != 10”下，条件不为真");
            }

        }
    }
}
