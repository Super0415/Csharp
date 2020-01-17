using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 练习题1
{
    //数学计算题
    //  2/1 + 3/2 + 5/3 + 8/5 + 13/8 +... 的n位的值与和


    class Program
    {

        /// <summary>
        /// 求单项数据，可知：后一个数的分子为：前一个数的分子+分母  后一个数的分母为：前一个数的分母
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static float Fun_Seek(int n)
        {
            int Z1 = 2;
            int M1 = 1;
            int Zn = 0;
            int Mn = 0;
            float result = 0;
            if (n == 0) return 0;
            else if (n == 1) return Z1 / M1;

            for (int i = 2; i <= n; i++)
            {
                Zn = Z1 + M1;
                Mn = Z1;


            }

            return result;
        }


        static void Main(string[] args)
        {
        }
    }
}
