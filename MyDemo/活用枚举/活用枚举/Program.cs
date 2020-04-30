using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 活用枚举
{
    class Program
    {
        enum Student
        {
            Xiaoyi = 1,
            Xiaoer,
            Xiaosan,
        }
        static void Main(string[] args)
        {
            Student c = (Student)Enum.Parse(typeof(Student), "Xiaor", true);

            switch (c)
            {
                case Student.Xiaoyi:
                    Console.WriteLine("学员：" + Student.Xiaoyi + "\t序号为：" + ((int)Student.Xiaoyi).ToString());
                    break;
                case Student.Xiaoer:
                    Console.WriteLine("学员：" + Student.Xiaoer + "\t序号为：" + ((int)Student.Xiaoer).ToString());
                    break;
                case Student.Xiaosan:
                    Console.WriteLine("学员：" + Student.Xiaosan + "\t序号为：" + ((int)Student.Xiaosan).ToString());
                    break;
                default:
                    Console.WriteLine("查无此人！！！");

                    break;

            }


            Console.ReadKey();
        }
    }
}

          //for (int i = 0; i< 4; i++)
          //  {
          //      switch (i)
          //      {
          //          case (int)Student.Xiaoyi:
          //              Console.WriteLine("学员：" + Student.Xiaoyi + "\t序号为：" + ((int)Student.Xiaoyi).ToString());
          //              break;
          //          case (int)Student.Xiaoer:
          //              Console.WriteLine("学员：" + Student.Xiaoer + "\t序号为：" + ((int)Student.Xiaoer).ToString());
          //              break;
          //          case (int)Student.Xiaosan:
          //              Console.WriteLine("学员：" + Student.Xiaosan + "\t序号为：" + ((int)Student.Xiaosan).ToString());
          //              break;
          //          default:
          //              Console.WriteLine("查无此人！！！");
          //              break;

          //      }
          //  }
          //  string str = "" + Student.Xiaoer;
          //  switch (str)
          //  {
          //      case "Xiaoyi":
          //          Console.WriteLine("学员：" + Student.Xiaoyi + "\t序号为：" + ((int)Student.Xiaoyi).ToString());
          //          break;
          //      case "Xiaoer":
          //          Console.WriteLine("学员：" + Student.Xiaoer + "\t序号为：" + ((int)Student.Xiaoer).ToString());
          //          break;
          //      case "Xiaosan":
          //          Console.WriteLine("学员：" + Student.Xiaosan + "\t序号为：" + ((int)Student.Xiaosan).ToString());
          //          break;
          //      default:
          //          Console.WriteLine("查无此人！！！");
          //          break;

          //  }



