using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据测试
{
    class Program
    {
        static void Main(string[] args)
        {
            short temp = -5;
            ushort temp1 = (ushort)(0 - temp);
            
            string result = Convert.ToString(temp, 2);//转换为百2进制度形式
            string result1 = Convert.ToString(temp1, 2);//转换为百2进制度形式\
            Console.WriteLine(result);
            Console.WriteLine(result1);
            Console.ReadKey();
        }
    }
}
