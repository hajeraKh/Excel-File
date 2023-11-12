using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Method_Overriding
{

   
    public class BClass
    {
        public virtual void GetInfo()
        {
            Console.WriteLine("Welcome to our Website.");
        }
    }
  
    public class DClass : BClass
    {
        public override void GetInfo()
        {
            Console.WriteLine("Welcome to Excel Technologies Ltd.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DClass d = new DClass();
            d.GetInfo();
            BClass b = new BClass();
            b.GetInfo();
            Console.WriteLine("\nPress Enter Key to Exit..");
            Console.ReadLine();

            Console.ReadKey();
        }
    }

}
