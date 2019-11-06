using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 位运算符
{
    class Program
    {
        static void Main(string[] args)
        {
            //位运算符： " & "、" | "、" ^ "、" ~ "、" << "、" >> "

            Console.WriteLine("(规定内部标准a=2、b=4)\n");
            while (true)
            {
                Console.Write("请输出两个数据，以空格隔开：");
                string str = Console.ReadLine();
                int a = Convert.ToInt32(str.Split(' ')[0]);
                int b = Convert.ToInt32(str.Split(' ')[1]);
                int c = 0;

                Console.WriteLine("第一位为操作数据，第二位为移位数");
                Console.WriteLine("a二进制为：{0}", Convert.ToString(a, 2));
                Console.WriteLine("b二进制为：{0}", Convert.ToString(b, 2));
                c = a << b;
                Console.WriteLine("a << b = {0}", c);
                Console.WriteLine("a << b二进制为：{0}", Convert.ToString(c, 2));

                c = a >> b;
                Console.WriteLine("a >> b = {0}", c);
                Console.WriteLine("a >> b二进制为：{0}", Convert.ToString(c, 2));






                //Console.WriteLine("注意&的真值表");
                //Console.WriteLine("a二进制为：{0}", Convert.ToString(a, 2));
                //Console.WriteLine("b二进制为：{0}", Convert.ToString(b, 2));
                //Console.WriteLine("a & b = {0}",a&b);
                //Console.WriteLine("a&b二进制为：{0}", Convert.ToString(a & b, 2));

                //Console.WriteLine("注意|的真值表");
                //Console.WriteLine("a | b = {0}", a | b);
                //Console.WriteLine("a|b二进制为：{0}", Convert.ToString(a | b, 2));

                //Console.WriteLine("注意^的真值表");
                //Console.WriteLine("a ^ b = {0}", a ^ b);
                //Console.WriteLine("a^b二进制为：{0}", Convert.ToString(a ^ b, 2));

                //Console.WriteLine("注意~的真值表");
                //Console.WriteLine("~a = {0}", ~a);
                //Console.WriteLine("~a二进制为：{0}", Convert.ToString(~a, 2));

            }
        }
    }
}
