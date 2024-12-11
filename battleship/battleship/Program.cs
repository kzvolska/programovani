using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
   
    internal class Program
    {
       
        public static class Global
        {
            public static List<char> rows = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            public static List<char> yRows = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            public static string[,] pcArray = new string[10, 10];
            public static string[,] playerArray = new string[10, 10];
            public static int[] yColumns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            public static int[] columns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            public static int p = 0;
            public static int l = 0;
            public static int k = 0;
            public static int b = 0;
            public static int t = 0;

        }
        static void ComputerArray ()
        {
            Console.WriteLine("Pole pocitace");
            Console.WriteLine("  ");
            foreach (int name in Global.columns)
            {
                Console.Write(name + " ");
            }
            Console.WriteLine(" ");
            for (int i = 0; i < Global.pcArray.GetLength(0); i++)
            {
                Console.Write(Global.rows[i] + " ");
                for (int j = 0; j < Global.pcArray.GetLength(1); j++)
                {
                    Console.Write(Global.pcArray[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        
        static void PlayerArray ()
        {
            Console.WriteLine("Vase pole");
            Console.WriteLine("  ");
            foreach (int name in Global.yColumns)
            {
                Console.Write(name + " ");
            }
            Console.WriteLine(" "); 
            for (int i = 0; i < Global.playerArray.GetLength(0); i++)
            {
                Console.Write(Global.yRows[i] + " ");
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    Console.Write(Global.playerArray[i, j]);
                }
                Console.WriteLine();
            }
        }


        static void ShipLayout ()
        {
            bool shipInField = false;
            
            Console.WriteLine("Zadejte typ lode: L-letadlova, K-kriznik, T-torpedoborec, B-bitevni, P-ponorka");
            string type = Console.ReadLine();
            while (type != "L" && type != "K" && type != "T" && type != "B" && type != "P")
            {
                Console.WriteLine("zadavejte velka pocatecni pismena lodi: L, K, T, B nebo P");
                type = Console.ReadLine();
            }
            while (type == "L" && Global.l == 1)
            {
                Console.WriteLine("Kazdou lod muzete zadat jen jednou");
                type = Console.ReadLine();
            }
            while (type == "K" && Global.k == 1)
            {
                Console.WriteLine("Kazdou lod muzete zadat jen jednou");
                type = Console.ReadLine();
            }
            while (type == "T" && Global.t == 1)
            {
                Console.WriteLine("Kazdou lod muzete zadat jen jednou");
                type = Console.ReadLine();
            }
            while (type == "B" && Global.b == 1)
            {
                Console.WriteLine("Kazdou lod muzete zadat jen jednou");
                type = Console.ReadLine();
            }
            while (type == "P" && Global.p == 1)
            {
                Console.WriteLine("Kazdou lod muzete zadat jen jednou");
                type = Console.ReadLine();
            }

            Console.WriteLine("Zadejte, jak chcete lod umistit: H-horizontalne nebo V-vertikalne");
            string orientation = Console.ReadLine();
            while (orientation != "H" && orientation != "V")
            {
                Console.WriteLine("zadavejte velka pocatecni pismena orientaci: H nebo V");
                orientation = Console.ReadLine();
            }
            while (!shipInField)
            {
                Console.WriteLine("Rozmistete si lode: zadejte ciselne souradnice leveho horniho rohu lode" +
                    "\n souradnice radku (A-J)");

                char a = char.Parse(Console.ReadLine());
                while (!Global.yRows.Contains(a))
                {
                    Console.WriteLine("uvadejte velka pismena od A do J");
                    a = char.Parse(Console.ReadLine());
                }

                int index = Global.yRows.IndexOf(a);
                Console.WriteLine(" souradnice sloupce (1-10)");

                int b;
                while (!int.TryParse(Console.ReadLine(), out b))
                {
                    Console.WriteLine("uvadejte cisla od 1 do 10");
                }
                while (!Global.yColumns.Contains(b))
                {
                    Console.WriteLine("uvadejte cisla od 1 do 10");
                    b = int.Parse(Console.ReadLine());
                }

                if (type == "T")
                {
                    if (orientation == "V")
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 2; i++)
                                Global.playerArray[index + i, b - 1] = "T ";
                            Global.t = 1;
                            shipInField = true;
                        }

                    }
                    else
                    {
                        if (b + 1 > Global.playerArray.GetLength(0) || b + 2 > Global.playerArray.GetLength(0))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 2; i++)
                                Global.playerArray[index, b - 1 + i] = "T ";
                            Global.t = 1;
                            shipInField = true;
                        }

                    }
                }
                if (type == "B")
                {
                    if (orientation == "V")
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1) || index + 4 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                                Global.playerArray[index + i, b - 1] = "B ";
                            Global.b = 1;
                            shipInField = true;
                        }
                    }
                    else
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1) || index + 4 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                                Global.playerArray[index, b - 1 + i] = "B ";
                            Global.b = 1;
                            shipInField = true;
                        }
                    }
                }
                if (type == "L")
                {
                    if (orientation == "V")
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1) || index + 4 > Global.playerArray.GetLength(1) || index + 5 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                                Global.playerArray[index + i, b - 1] = "L ";
                            Global.l = 1;
                            shipInField = true;
                        }

                    }
                    else
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1) || index + 4 > Global.playerArray.GetLength(1) || index + 5 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                                Global.playerArray[index, b - 1 + i] = "L ";
                            Global.l = 1;
                            shipInField = true;
                        }
                    }
                }
                if (type == "K")
                {
                    if (orientation == "V")
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                                Global.playerArray[index + i, b - 1] = "K ";
                            Global.k = 1;
                            shipInField = true;
                        }
                    }
                    else
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                                Global.playerArray[index, b - 1 + i] = "K ";
                            Global.k = 1;
                            shipInField = true;
                        }
                    }
                }
                else
                {
                    if (orientation == "V")
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                                Global.playerArray[index + i, b - 1] = "P ";
                            Global.p = 1;
                            shipInField = true;
                        }
                    }
                    else
                    {
                        if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1))
                        {
                            Console.WriteLine("Lod se nevejde do pole");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                                Global.playerArray[index, b - 1 + i] = "P ";
                            Global.p = 1;
                            shipInField = true;
                        }
                    }
                }
            }    
            Console.Clear();
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < Global.pcArray.GetLength(0); i++)
            {
                for (int j = 0; j < Global.pcArray.GetLength(1); j++)
                {
                    Global.pcArray[i, j] = "* ";
                }
            }
            ComputerArray();

            for (int i = 0; i < Global.playerArray.GetLength(0); i++)
            {
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    Global.playerArray[i, j] = "* ";
                }
            }
            PlayerArray();

            while (Global.p != 1 || Global.t != 1 || Global.b != 1 || Global.l !=1 || Global.k != 1 )
            {
                ShipLayout();
                ComputerArray();
                PlayerArray();
            }
            

            Console.ReadKey();
        }
    }
}
