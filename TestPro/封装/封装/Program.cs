using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 封装
{
    class Score
    {
        protected float ChineseScore;
        internal float MathScore;
        private float ForeignScore;
        public void SetScore(float ch = 0, float ma = 0, float fo = 0)
        {
            ChineseScore = ch;
            MathScore = ma;
            ForeignScore = fo;
        }
        public float GetTotal()
        {
            return ChineseScore + MathScore + ForeignScore;
        }
        /// <summary>
        /// 评价课程成绩标准
        /// </summary>
        /// <returns>0-完美1-优2-良3-中4-差</returns>
        public string ResultCourse(float score = 0)
        {
            if (score < 60) return "差";             //差
            else if (score < 80) return "中";        //中
            else if (score < 90) return "良";        //良
            else if (score < 100) return "优";       //优
            else return "完美";                          //完美
        }
    }

    class Test: Score
    {
        public float ObjectChinese;
        public Score test = new Score();
        public void TestMain()
        {

            Program Object = new Program();

            Console.WriteLine("****** 欢迎进入课程评价系统！******");
            Console.Write("请输入语文成绩：");
            float Chinese = float.Parse(Console.ReadLine());
            Console.Write("请输入数学成绩：");
            float Math = float.Parse(Console.ReadLine());
            Console.Write("请输入外语成绩：");
            float Foreign = float.Parse(Console.ReadLine());

            ObjectChinese = test.ChineseScore;
            test.SetScore(Chinese, Math, Foreign);
            Console.WriteLine("总分数为：" + test.GetTotal());
            Console.WriteLine("各科评价如下：\n语文：{0}\n数学：{1}\n外语：{2}", test.ResultCourse(Chinese), test.ResultCourse(Math), test.ResultCourse(Foreign));
            //Console.ReadKey();
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.TestMain();
            //float ObjectChinese;
            //Score Test = new Score();
            //Program Object = new Program();

            //Console.WriteLine("****** 欢迎进入课程评价系统！******");
            //Console.Write("请输入语文成绩：");
            //float Chinese = float.Parse(Console.ReadLine());
            //Console.Write("请输入数学成绩：");
            //float Math = float.Parse(Console.ReadLine());
            //Console.Write("请输入外语成绩：");
            //float Foreign = float.Parse(Console.ReadLine());

            //ObjectChinese = Test.MathScore;
            //Test.SetScore(Chinese, Math, Foreign);
            //Console.WriteLine("总分数为：" + Test.GetTotal());
            //Console.WriteLine("各科评价如下：\n语文：{0}\n数学：{1}\n外语：{2}", Test.ResultCourse(Chinese), Test.ResultCourse(Math), Test.ResultCourse(Foreign));
            Console.ReadKey();
        }
    }
}
