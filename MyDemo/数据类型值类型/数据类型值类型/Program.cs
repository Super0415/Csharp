using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据类型值类型
{
    class Program
    {
        static void MyFunc_AutoPlus(int factor, ref int target)
        {
            target += factor;
        }

        static bool MyFunc_AutoCompare(int data1,int data2, out int max,out string Evaluate)
        {
            max = 0;
            Evaluate = null;
            if (data1 > data2)
            {
                max = data1;
                Evaluate = "第1个数大";
                return true;
            }
            else if (data1 < data2)
            {
                max = data2;
                Evaluate = "第2个数大";
                return true;
            }
            else
            {
                Evaluate = "没有数大";
                return false;
            }
        }

        enum MyStudentNum
        {
            xiao_yi,
            xiao_er,
            xiao_san=0,
            xiao_si,
            xiao_wu,
            xiao_liu=1,
            xiao_qi,
        }

        public struct Student
        {
            public string name;
            public int Student_ID;
            public float chinese;
            public float math;
            public float english;

            public double MyFun_Assessment()
            {
                return (chinese * 0.4 + math * 0.4 + english * 0.2);
            }
        }
        static void Main(string[] args)
        {
            Student sdt1 = new Student();
            sdt1.name = "Lily";
            sdt1.Student_ID = 1;
            sdt1.chinese = 87;
            sdt1.math = 98;
            sdt1.english = 100;

            Console.WriteLine("{0}\t的综合成绩为：{1}", sdt1.name, sdt1.MyFun_Assessment());

            Student sdt2 = sdt1;
            sdt2.name = "Lucy";
            Console.WriteLine("{0}\t的综合成绩为：{1}", sdt2.name, sdt2.MyFun_Assessment());


            for(int z = 0; z < 30; z++)
                Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("学生 {0} \t的学号是：{1}" , MyStudentNum.xiao_yi.ToString(),(int)MyStudentNum.xiao_yi);
            Console.WriteLine("学生 {0} \t的学号是：{1}", MyStudentNum.xiao_er.ToString(), (int)MyStudentNum.xiao_er);
            Console.WriteLine("学生 {0} \t的学号是：{1}", MyStudentNum.xiao_san.ToString(), (int)MyStudentNum.xiao_san);
            Console.WriteLine("学生 {0} \t的学号是：{1}", MyStudentNum.xiao_si.ToString(), (int)MyStudentNum.xiao_si);
            Console.WriteLine("学生 {0} \t的学号是：{1}", MyStudentNum.xiao_wu.ToString(), (int)MyStudentNum.xiao_wu);
            Console.WriteLine("学生 {0} \t的学号是：{1}", MyStudentNum.xiao_liu.ToString(), (int)MyStudentNum.xiao_liu);
            Console.WriteLine("学生 {0} \t的学号是：{1}", MyStudentNum.xiao_qi.ToString(), (int)MyStudentNum.xiao_qi);

            Console.WriteLine("*********************");
            int a = 1;
            int b = a;           
            a += 1;
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            Console.WriteLine("*********************");
            int c = 0;
            MyFunc_AutoPlus(2, ref c);
            Console.WriteLine("c = " + c);
            Console.WriteLine("*********************");

            int max = 0;
            string info = null;
            MyFunc_AutoCompare(a,b,out max,out info);
            Console.WriteLine("第一个数据：" + a);
            Console.WriteLine("第二个数据：" + b);
            Console.WriteLine("max = " + max);
            Console.WriteLine("评价：" + info);
            Console.WriteLine("*********************");

            bool e = false;
            Console.WriteLine("bool e\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , e.ToString(), sizeof(bool).ToString(), e, !e);

            byte f = 1;
            Console.WriteLine("byte f\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , f.ToString(),sizeof(byte).ToString(), byte.MinValue, byte.MaxValue);

            char g = 'a';
            Console.WriteLine("char g\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , g.ToString(),sizeof(char).ToString(), char.MinValue, char.MaxValue);

            decimal h = 1;
            Console.WriteLine("decimal h\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , h.ToString(),sizeof(decimal).ToString(), decimal.MinValue, decimal.MaxValue);

            double i = 1;
            Console.WriteLine("double i\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , i.ToString(),sizeof(double).ToString(), double.MinValue, double.MaxValue);

            float j = 1;
            Console.WriteLine("float j\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , j.ToString(),sizeof(float).ToString(), float.MinValue, float.MaxValue);

            int k = 1;
            Console.WriteLine("int k\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , k.ToString(),sizeof(int).ToString(), int.MinValue, int.MaxValue);

            long l = 1;
            Console.WriteLine("long l\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , l.ToString(),sizeof(long).ToString(), long.MinValue, long.MaxValue);

            sbyte m = 1;
            Console.WriteLine("sbyte m\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , m.ToString(),sizeof(sbyte).ToString(), sbyte.MinValue, sbyte.MaxValue);

            short n = 1;
            Console.WriteLine("short n\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , n.ToString(),sizeof(short).ToString(), short.MinValue, short.MaxValue);

            uint o = 1;
            Console.WriteLine("uint o\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , o.ToString(),sizeof(uint).ToString(), uint.MinValue, uint.MaxValue);

            ulong p = 1;
            Console.WriteLine("ulong p\t\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , p.ToString(),sizeof(ulong).ToString(), ulong.MinValue, ulong.MaxValue);

            ushort q = 1;
            Console.WriteLine("ushort q\t为：{0}，其大小为：{1}，其范围为：{2} ~ {3}" , q.ToString(),sizeof(ushort).ToString(), ushort.MinValue, ushort.MaxValue);

            Console.ReadKey();
        }
    }
}
