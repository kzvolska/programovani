using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{

    internal class Program
    {

        public static class Global
        {
            public static List<char> rows = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };   //zavedla jsem si globalni promenne, abych je mohla pouzivac napric funkcemi
            public static List<char> yRows = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            public static string[,] pcArray = new string[10, 10];
            public static string[,] playerArray = new string[10, 10];
            public static int[] yColumns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            public static int[] columns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            public static int ponorka = 0;      //pocty lodi zadanych hracem do pole
            public static int letadlova = 0;
            public static int kriznik = 0;
            public static int battleship = 0;
            public static int torpedoborec = 0;
            public static int ponorkaLength = 3;
            public static int letadlovaLength = 5;      //delky jednotlivych lodi
            public static int kriznikLength = 3;
            public static int battleshipLength = 4;
            public static int torpedoborecLength = 2;

        }
        static void ComputerArray()
        {
            Console.WriteLine("Pole pocitace");         //vypisuje pole pocitace
            Console.WriteLine("  ");
            Console.Write("  ");
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


        static void PlayerArray()          //vypisuje pole uzivatele
        {
            Console.WriteLine("Vase pole");
            Console.WriteLine("  ");
            Console.Write("  ");
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

        static bool CheckPosition(int x, int y, string[,] yourArray, string verHor, int shipLength)    //kontrola, jestli se pri zadavani lodi zadane lode neprekryvaji
        {
            if (verHor == "H")
            {

                for (int i = 0; i < shipLength - 1; i++)
                {
                    if (yourArray[x + i, y] != "* ")
                    {
                        Console.WriteLine("lod se sem nevejde");
                        return false;
                    }
                    else
                        return true;
                }
            }
            else
            {
                for (int i = 0; i < shipLength - 1; i++)
                {
                    if (yourArray[x, y + i] != "* ")
                    {
                        Console.WriteLine("lod se sem nevejde");
                        return false;
                    }
                    else
                        return true;
                }
            }
            return false;
        }


        static void ComputerShipLayout()       //nahodne rozmistuje lode pocitace + kontroluje, jestli se vejdou/neprekryvaji
        {
            Random rnd = new Random();
            Random orient = new Random();
            bool pcShipInField = false;
            //torpedoborec
            int pcX = rnd.Next(0, 10);
            int pcY = rnd.Next(0, 10);
            int pcOrientation = orient.Next(1, 3);
            string horVer;
            bool pcCorrectPosition = true;
            if (pcOrientation == 1)
            {
                horVer = "V";
            }
            else
            {
                horVer = "H";
            }
            CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.torpedoborecLength);
            while (!pcCorrectPosition)
            {
                pcOrientation = orient.Next(1, 3);
                if (pcOrientation == 1)
                {
                    horVer = "V";
                }
                else
                {
                    horVer = "H";
                }
                CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.torpedoborecLength);
            }
            while (!pcShipInField)
            {

                if (pcOrientation == 1)
                {
                    if (pcX + 1 > Global.pcArray.GetLength(1) || pcX + 2 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                            Global.pcArray[pcX + i, pcY] = "T ";
                        pcShipInField = true;
                    }

                }
                else
                {
                    if (pcY + 1 > Global.pcArray.GetLength(1) || pcY + 2 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                            Global.pcArray[pcX, pcY + i] = "T ";
                        pcShipInField = true;
                    }
                }

                //bitevni lod
                pcX = rnd.Next(0, 10);
                pcY = rnd.Next(0, 10);
                pcOrientation = orient.Next(1, 3);
                pcCorrectPosition = true;
                if (pcOrientation == 1)
                {
                    horVer = "V";
                }
                else
                {
                    horVer = "H";
                }
                CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.battleshipLength);
                while (!pcCorrectPosition)
                {
                    pcOrientation = orient.Next(1, 3);
                    if (pcOrientation == 1)
                    {
                        horVer = "V";
                    }
                    else
                    {
                        horVer = "H";
                    }
                    CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.battleshipLength);
                }
                if (pcOrientation == 1)
                {
                    if (pcX + 1 > Global.pcArray.GetLength(1) || pcX + 2 > Global.pcArray.GetLength(1) || pcX + 3 > Global.pcArray.GetLength(1) || pcX + 4 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                            Global.pcArray[pcX + i, pcY] = "B ";
                        pcShipInField = true;
                    }
                }
                else
                {
                    if (pcY + 1 > Global.pcArray.GetLength(1) || pcY + 2 > Global.pcArray.GetLength(1) || pcY + 3 > Global.pcArray.GetLength(1) || pcY + 4 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                            Global.pcArray[pcX, pcY + i] = "B ";
                        pcShipInField = true;
                    }
                }

                //letadlova lod
                pcX = rnd.Next(0, 10);
                pcY = rnd.Next(0, 10);
                pcOrientation = orient.Next(1, 3);
                pcCorrectPosition = true;
                if (pcOrientation == 1)
                {
                    horVer = "V";
                }
                else
                {
                    horVer = "H";
                }
                CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.letadlovaLength);
                while (!pcCorrectPosition)
                {
                    pcOrientation = orient.Next(1, 3);
                    if (pcOrientation == 1)
                    {
                        horVer = "V";
                    }
                    else
                    {
                        horVer = "H";
                    }
                    CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.letadlovaLength);
                }
                if (pcOrientation == 1)
                {
                    if (pcX + 1 > Global.pcArray.GetLength(1) || pcX + 2 > Global.pcArray.GetLength(1) || pcX + 3 > Global.pcArray.GetLength(1) || pcX + 4 > Global.pcArray.GetLength(1) || pcX + 5 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                            Global.pcArray[pcX + i, pcY] = "L ";
                        pcShipInField = true;
                    }
                }
                else
                {
                    if (pcY + 1 > Global.pcArray.GetLength(1) || pcY + 2 > Global.pcArray.GetLength(1) || pcY + 3 > Global.pcArray.GetLength(1) || pcY + 4 > Global.pcArray.GetLength(1) || pcY + 5 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                            Global.pcArray[pcX, pcY + i] = "L ";
                        pcShipInField = true;
                    }
                }

                //kriznik
                pcX = rnd.Next(0, 10);
                pcY = rnd.Next(0, 10);
                pcOrientation = orient.Next(1, 3);
                pcCorrectPosition = true;
                if (pcOrientation == 1)
                {
                    horVer = "V";
                }
                else
                {
                    horVer = "H";
                }
                CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.kriznikLength);
                while (!pcCorrectPosition)
                {
                    pcOrientation = orient.Next(1, 3);
                    if (pcOrientation == 1)
                    {
                        horVer = "V";
                    }
                    else
                    {
                        horVer = "H";
                    }
                    CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.kriznikLength);
                }
                if (pcOrientation == 1)
                {
                    if (pcX + 1 > Global.pcArray.GetLength(1) || pcX + 2 > Global.pcArray.GetLength(1) || pcX + 3 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX + i, pcY] = "K ";
                        pcShipInField = true;
                    }
                }
                else
                {
                    if (pcY + 1 > Global.pcArray.GetLength(1) || pcY + 2 > Global.pcArray.GetLength(1) || pcY + 3 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX, pcY + i] = "K ";
                        pcShipInField = true;
                    }
                }

                //ponorka
                pcX = rnd.Next(0, 10);
                pcY = rnd.Next(0, 10);
                pcOrientation = orient.Next(1, 3);
                pcCorrectPosition = true;
                if (pcOrientation == 1)
                {
                    horVer = "V";
                }
                else
                {
                    horVer = "H";
                }
                CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.ponorkaLength);
                while (!pcCorrectPosition)
                {
                    pcOrientation = orient.Next(1, 3);
                    if (pcOrientation == 1)
                    {
                        horVer = "V";
                    }
                    else
                    {
                        horVer = "H";
                    }
                    CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.ponorkaLength);
                }
                if (pcOrientation == 1)
                {
                    if (pcX + 1 > Global.pcArray.GetLength(1) || pcX + 2 > Global.pcArray.GetLength(1) || pcX + 3 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX + i, pcY] = "P ";
                        pcShipInField = true;
                    }
                }
                else
                {
                    if (pcY + 1 > Global.pcArray.GetLength(1) || pcY + 2 > Global.pcArray.GetLength(1) || pcY + 3 > Global.pcArray.GetLength(1))
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX, pcY + i] = "P ";
                        pcShipInField = true;
                    }
                }
            }
        }

            static int WriteX()        //umoznuje uziveteli vypsat souradnice zadane lode
            {
                Console.WriteLine("Rozmistete si lode: zadejte ciselne souradnice leveho horniho rohu lode" +
                        "\n souradnice radku (A-J)");

                char x = char.Parse(Console.ReadLine());
                while (!Global.yRows.Contains(x))
                {
                    Console.WriteLine("uvadejte velka pismena od A do J");
                    x = char.Parse(Console.ReadLine());
                }
                int index = Global.yRows.IndexOf(x);
                return index;
            }

            static int WriteY()            //umoznuje uziveteli vypsat souradnice zadane lode
            {
                Console.WriteLine(" souradnice sloupce (1-10)");

                int y;
                while (!int.TryParse(Console.ReadLine(), out y))
                {
                    Console.WriteLine("uvadejte cisla od 1 do 10");
                }
                while (!Global.yColumns.Contains(y))
                {
                    Console.WriteLine("uvadejte cisla od 1 do 10");
                    y = int.Parse(Console.ReadLine());
                }
                return y;
            }
            static void ShipLayout()           //vypisovani lodi do pole
            {
                bool shipInField = false;

                Console.WriteLine("Zadejte typ lode: L-letadlova, K-kriznik, T-torpedoborec, B-bitevni, P-ponorka");
                string type = Console.ReadLine();
                while (type != "L" && type != "K" && type != "T" && type != "B" && type != "P")         //overeni vstupu
                {
                    Console.WriteLine("zadavejte velka pocatecni pismena lodi: L, K, T, B nebo P");
                    type = Console.ReadLine();
                }
                while (type == "L" && Global.letadlova == 1 || type == "K" && Global.kriznik == 1 || type == "T" && Global.torpedoborec == 1 || type == "B" && Global.battleship == 1 || type == "P" && Global.ponorka == 1)
                {
                    Console.WriteLine("Kazdou lod muzete zadat jen jednou");        //overeni, ze je kazda lod jen jednou
                    type = Console.ReadLine();
                }

                Console.WriteLine("Zadejte, jak chcete lod umistit: H-horizontalne nebo V-vertikalne");
                string orientation = Console.ReadLine();
                while (orientation != "H" && orientation != "V")        //overeni vstupu
                {
                    Console.WriteLine("zadavejte velka pocatecni pismena orientaci: H nebo V");
                    orientation = Console.ReadLine();
                }
                while (!shipInField)        //overeni, aby byla zadana lod v hranicich pole
                {
                    int index = WriteX();
                    int y = WriteY();
                    bool correctPosition = true;


                    if (type == "T")        //vypisuje jednotive typy lodi + vola funkci na kontrolovani prekryvani
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.torpedoborecLength);
                        while (!correctPosition)
                        {
                            index = WriteX();
                            y = WriteY();
                            correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.torpedoborecLength);
                        }

                        if (orientation == "V")
                        {
                            if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 2; i++)
                                    Global.playerArray[index + i, y - 1] = "T ";
                                Global.torpedoborec = 1;
                                shipInField = true;
                            }

                        }
                        else
                        {
                            if (y + 1 > Global.playerArray.GetLength(0) || y + 2 > Global.playerArray.GetLength(0))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 2; i++)
                                    Global.playerArray[index, y - 1 + i] = "T ";
                                Global.torpedoborec = 1;
                                shipInField = true;
                            }

                        }
                    }
                    else if (type == "B")
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.battleshipLength);
                        while (!correctPosition)
                        {
                            index = WriteX();
                            y = WriteY();
                            correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.battleshipLength);
                        }
                        if (orientation == "V")
                        {
                            if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1) || index + 4 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 4; i++)
                                    Global.playerArray[index + i, y - 1] = "B ";
                                Global.battleship = 1;
                                shipInField = true;
                            }
                        }
                        else
                        {
                            if (y + 1 > Global.playerArray.GetLength(1) || y + 2 > Global.playerArray.GetLength(1) || y + 3 > Global.playerArray.GetLength(1) || y + 4 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 4; i++)
                                    Global.playerArray[index, y - 1 + i] = "B ";
                                Global.battleship = 1;
                                shipInField = true;
                            }
                        }
                    }
                    else if (type == "L")
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.letadlovaLength);
                        while (!correctPosition)
                        {
                            index = WriteX();
                            y = WriteY();
                            correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.letadlovaLength);
                        }
                        if (orientation == "V")
                        {
                            if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1) || index + 4 > Global.playerArray.GetLength(1) || index + 5 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 5; i++)
                                    Global.playerArray[index + i, y - 1] = "L ";
                                Global.letadlova = 1;
                                shipInField = true;
                            }

                        }
                        else
                        {
                            if (y + 1 > Global.playerArray.GetLength(1) || y + 2 > Global.playerArray.GetLength(1) || y + 3 > Global.playerArray.GetLength(1) || y + 4 > Global.playerArray.GetLength(1) || y + 5 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 5; i++)
                                    Global.playerArray[index, y - 1 + i] = "L ";
                                Global.letadlova = 1;
                                shipInField = true;
                            }
                        }
                    }
                    else if (type == "K")
                    {

                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.kriznikLength);
                        while (!correctPosition)
                        {
                            index = WriteX();
                            y = WriteY();
                            correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.kriznikLength);
                        }
                        if (orientation == "V")
                        {
                            if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index + i, y - 1] = "K ";
                                Global.kriznik = 1;
                                shipInField = true;
                            }
                        }
                        else
                        {
                            if (y + 1 > Global.playerArray.GetLength(1) || y + 2 > Global.playerArray.GetLength(1) || y + 3 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index, y - 1 + i] = "K ";
                                Global.kriznik = 1;
                                shipInField = true;
                            }
                        }
                    }
                    else if (type == "P")
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.ponorkaLength);
                        while (!correctPosition)
                        {
                            index = WriteX();
                            y = WriteY();
                            correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.ponorkaLength);
                        }
                        if (orientation == "V")
                        {
                            if (index + 1 > Global.playerArray.GetLength(1) || index + 2 > Global.playerArray.GetLength(1) || index + 3 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index + i, y - 1] = "P ";
                                Global.ponorka = 1;
                                shipInField = true;
                            }
                        }
                        else
                        {
                            if (y + 1 > Global.playerArray.GetLength(1) || y + 2 > Global.playerArray.GetLength(1) || y + 3 > Global.playerArray.GetLength(1))
                            {
                                Console.WriteLine("Lod se nevejde do pole");
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index, y - 1 + i] = "P ";
                                Global.ponorka = 1;
                                shipInField = true;
                            }
                        }
                    }
                }
                Console.Clear();        //vymazani konzole
            }
            static void Main(string[] args)
            {
                for (int i = 0; i < Global.pcArray.GetLength(0); i++)           //naplneni poli hvezdickami
                {
                    for (int j = 0; j < Global.pcArray.GetLength(1); j++)
                    {
                        Global.pcArray[i, j] = "* ";
                    }
                }
                ComputerArray();        //vypsani poli

                for (int i = 0; i < Global.playerArray.GetLength(0); i++)
                {
                    for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                    {
                        Global.playerArray[i, j] = "* ";
                    }
                }
                PlayerArray();

                while (Global.ponorka != 1 || Global.torpedoborec != 1 || Global.battleship != 1 || Global.letadlova != 1 || Global.kriznik != 1)
                {
                    ShipLayout();                   //opakuje se zadavani lodi do pole, dokud tam neni vsech pet lodi
                    ComputerArray();
                    PlayerArray();
                }
                ComputerShipLayout();           //pocitac zada lode do pole




                Console.ReadKey();
            }
    }
}
