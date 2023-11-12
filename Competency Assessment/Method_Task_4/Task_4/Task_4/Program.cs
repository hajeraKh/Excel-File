using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    public class Program
    {
         static int add(int a, int b)
        {
            return a + b; 
        }

        static int add(int a, int b, int c)
        { 
            return a + b + c; 
        }
        static void Main(string[] args)
        {
            int Add1 = add(5, 5);
            int Add2 = add(5, 2, 7);
            Console.WriteLine("Method:" +Add1);
            Console.WriteLine("Overloaded Method:" +Add2);
            Console.ReadKey();
        }
    }
}
