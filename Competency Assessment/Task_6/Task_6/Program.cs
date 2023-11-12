using System;

class Program
{
    static void Main()
    {
     
        Console.Write("Enter the value for n1: ");
        int n1 = int.Parse(Console.ReadLine());

        Console.Write("Enter the value for n2: ");
        int n2 = int.Parse(Console.ReadLine());

        Console.Write("Enter the value for n3: ");
        int n3 = int.Parse(Console.ReadLine());

      
        int min;

        if (n1 < n2)
        {
            min = n1;
        }
        else
        {
            min = n2;
        }

  
        if (n3 < min)
        {
            min = n3;
        }

     
        Console.WriteLine($"The minimum value is: {min}");
        Console.ReadKey();
    }
}
