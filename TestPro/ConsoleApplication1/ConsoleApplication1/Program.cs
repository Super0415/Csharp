using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static USBDriver usb = new USBDriver();
        static void Main(string[] args)
        {
         
            int m = usb.Count;
            int n = usb.Count;
            Console.WriteLine(m);
            Console.WriteLine(n);
            Console.ReadKey();
        }
    }
}
