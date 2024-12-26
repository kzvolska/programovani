using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dodelani_poli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] myArray = new int[100];
            for (int i = 0; i < myArray.Length; i++)
            {
                myArray[i] = rnd.Next(0,10);
                Console.Write(myArray[i] + " ");
            }
            Console.WriteLine();

            //TODO 9
            int[] counts = new int[10];

            foreach (int number in myArray)
            {
                counts[number]++;
            }
            for (int i = 0; i < counts.Length; i++) 
            {
                Console.WriteLine("cetnost cisla " + i + " je " + counts[i]);
            }
            

            //todo 10
            int[] reversedArray = new int[100];

            for(int i = 0;i < reversedArray.Length;i++)
            {
                reversedArray[i] = myArray[myArray.Length - 1 - i]; //indexuje se od nulz, mus9me se posunout o 1 dolu
                Console.Write(reversedArray[i] + " ");
            }
           Console.WriteLine();
           Console.ReadKey();
        }
        
    }
}
