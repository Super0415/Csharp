using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 赋值_运算符
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = new int();
            int Tempb = new int();
            int Resultb = new int();
            int Temp = new int();       
            int Result = new int();
            TEST:
            Console.Write("请输入一个整数：");
            string str = Console.ReadLine();
            try
            {
                a = Convert.ToInt32(str);
            }
            catch
            {
                Console.WriteLine("格式错误，请重新输入。。。");
                goto TEST;
            }
            Temp = Result;
            Tempb = Result;
            Result = a;
            Resultb = a;
            Console.WriteLine("Result = a 时，            运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);
            Console.WriteLine("Resultb = a 时，           运算前Resulbt = {0}，a = {1},运算后Resultb = {2}", Tempb, a, Resultb);

            Temp = Result;
            Tempb = Result;
            Result += a;
            Resultb = Resultb + a;
            Console.WriteLine("Result += a 时，           运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);
            Console.WriteLine("Resultb = Resultb + a 时， 运算前Resulbt = {0}，a = {1},运算后Resultb = {2}", Tempb, a, Resultb);

            Temp = Result;
            Tempb = Result;
            Result -= a;
            Resultb = Resultb - a;
            Console.WriteLine("Result -= a 时，           运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);
            Console.WriteLine("Resultb = Resultb - a 时， 运算前Resulbt = {0}，a = {1},运算后Resultb = {2}", Tempb, a, Resultb);

            Temp = Result;
            Tempb = Result;
            Result *= a;
            Resultb = Resultb * a;
            Console.WriteLine("Result *= a 时，           运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);
            Console.WriteLine("Resultb = Resultb * a 时， 运算前Resulbt = {0}，a = {1},运算后Resultb = {2}", Tempb, a, Resultb);

            Temp = Result;
            Result /= a;
            Console.WriteLine("Result /= a 时，运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);

            Temp = Result;
            Result %= a;
            Console.WriteLine("Result %= a 时，运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);

            Temp = Result;
            Result <<= a;
            Console.WriteLine("Result <<= a 时，运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);

            Temp = Result;
            Result >>= a;
            Console.WriteLine("Result >>= a 时，运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);

            Temp = Result;
            Result &= a;
            Console.WriteLine("Result &= a 时，运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);

            Temp = Result;
            Result ^= a;
            Console.WriteLine("Result ^= a 时，运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);

            Temp = Result;
            Result |= a;
            Console.WriteLine("Result |= a 时，运算前Result = {0}，a = {1},运算后Result = {2}", Temp, a, Result);

            Console.ReadKey();
        }
    }
}




