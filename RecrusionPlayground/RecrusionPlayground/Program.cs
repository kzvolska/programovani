using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrusionPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int factorial = Factorial(n);
            int fibonacci = Fibonacci(n);
            Console.WriteLine($"Pro cislo {n} je faktorial {factorial} a fibonacci je {fibonacci}.");
            Console.ReadKey();
        }
        static int Factorial (int n)
        {
            int result;
            if (n == 1)
            {
                result = 1;
            }
            else 
            {
                result = n* Factorial (n-1);
            }
            return result;
        }
        static int Fibonacci (int n)
        {
            if (n <= 1) return n;
            return Fibonacci(n-1) * Fibonacci (n-2);

        }
    }
}
