using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Made by Jan Borecky for PRG seminar at Gymnazium Voderadska, year 2024-2025.
 * Extended by students.
 */

namespace ArrayPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TODO 1: Vytvoř integerové pole a naplň ho pěti libovolnými čísly.
            int[] myArray = { 10, 20, 30, 40, 50 };

            //TODO 2: Vypiš do konzole všechny prvky pole, zkus jak klasický for, kde i využiješ jako index v poli, tak foreach.
            Console.WriteLine("vypisovani for cyklem");
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.WriteLine(myArray[i]);
            }

            Console.WriteLine("vypisovani foreachem");
            foreach (int num in myArray) //misto int muzu napsat var
            {
                Console.WriteLine(num);
            }

            //TODO 3: Spočti sumu všech prvků v poli a vypiš ji uživateli.
            int sum = 0;
            for (int i = 0; i < myArray.Length; i++)
            {
                sum += myArray[i];
            }
            Console.WriteLine("Suma for cyklem " + sum);

            //NEBO

            foreach (int num in myArray)
            {
                sum += num;
            }
            Console.WriteLine("Suma foreachem " + sum);

            //NEBO

            sum = myArray.Sum();
            Console.WriteLine("Suma funkci " + sum);

            //TODO 4: Spočti průměr prvků v poli a vypiš ho do konzole.
            int average = sum / myArray.Length;
            Console.WriteLine("prumer vypoctem je " + average);

            //NEBO

            double averageDouble = myArray.Average();
            Console.WriteLine("prumer funkci je " + averageDouble);

            //NEBO

            foreach (int num in myArray)
            {
                average = sum / myArray.Length;
            }
            Console.WriteLine("prumer foreachem je " + average);

            //TODO 5: Najdi maximum v poli a vypiš ho do konzole.
            int max;
            max = myArray.Max();
            Console.WriteLine("maximum funkci " + max);

            //NEBO

            foreach (int num in myArray)
            {
                if (num > max)
                {
                    max = num;
                }
            }
            Console.WriteLine("maximum foreachem " + max);

            //TODO 6: Najdi minimum v poli a vypiš ho do konzole.
            int min;
            min = myArray.Min();
            Console.WriteLine(min);

            //TODO 7: Vyhledej v poli číslo, které zadá uživatel, a vypiš index nalezeného prvku do konzole.
            Console.WriteLine("Napis cislo, ktere chces najit");
            int numberToFind = int.Parse(Console.ReadLine());
            int foundIndex = -1;
            //bool foundNumber = false;
            for (int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i] == numberToFind)
                {
                    //Console.WriteLine("index cisla " + numberToFind + " je " + i);
                    foundIndex = i;
                    break; //prerusi for cyklus, kdyz najde cislo, nepokracuje v prohledavani pole
                    //foundNumber = true;
                }
            }

            if (foundIndex == -1)
            {
                Console.WriteLine("cislo v poli neni");
            }
            else
            {
                Console.WriteLine("index cisla " + numberToFind + " je " + foundIndex);
            }
                /* if (!foundNumber)
                {
                    Console.WriteLine("cislo v poli neni");
                }
                */

                //TODO 8: Přepiš pole na úplně nové tak, že bude obsahovat 100 náhodně vygenerovaných čísel od 0 do 9.
                Random rng = new Random();
                int[] myArray2 = new int[100];
                for (int i = 0; i < myArray2.Length; i++)
                {
                    myArray2[i] = rng.Next(0, 10);
                    Console.WriteLine(myArray2[i]);
                }

                //TODO 9: Spočítej kolikrát se každé číslo v poli vyskytuje a spočítané četnosti vypiš do konzole.
                int[] counts = new int[10];

                //TODO 10: Vytvoř druhé pole, do kterého zkopíruješ prvky z prvního pole v opačném pořadí.


                //Zkus is dál hrát s polem dle své libosti. Můžeš třeba prohodit dva prvky, ukládat do pole prvky nějaké posloupnosti (a pak si je vyhledávat) nebo cokoliv dalšího tě napadne

                Console.ReadKey();
            
        }
    }
}
