using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloworld
{

 //Access Specifier：访问修饰符，这个决定了变量或方法对于另一个类的可见性。
 //Return type：返回类型，一个方法可以返回一个值。返回类型是方法返回的值的数据类型。如果方法不返回任何值，则返回类型为 void。
 //Method name：方法名称，是一个唯一的标识符，且是大小写敏感的。它不能与类中声明的其他标识符相同。
 //Parameter list：参数列表，使用圆括号括起来，该参数是用来传递和接收方法的数据。参数列表是指方法的参数类型、顺序和数量。参数是可选的，也就是说，一个方法可能不包含参数。
 //Method body：方法主体，包含了完成任务所需的指令集。      
    class Program
    {
        public static int Func()
        {
            Console.WriteLine("Hello Second World!");
            return 1;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello First World!");
            var num = Func();
            Console.WriteLine("num is "+num.GetType());

            Console.ReadKey();
        }
    }
}


