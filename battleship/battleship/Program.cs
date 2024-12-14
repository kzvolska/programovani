using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace battleship
{

    internal class Program
    {

        public static class Global
        {
            public static List<string> rows = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };   //zavedla jsem si globalni promenne, abych je mohla pouzivac napric funkcemi
            public static List<string> yRows = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            public static string[,] pcArray = new string[10, 10];
            public static string[,] playerArray = new string[10, 10];
            public static int[] yColumns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            public static int[] columns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //pocty lodi zadanych hracem do pole
            public static int ponorka = 0;      
            public static int letadlova = 0;
            public static int kriznik = 0;
            public static int battleship = 0;
            public static int torpedoborec = 0;
            //delky jednotlivych lodi
            public static int ponorkaLength = 3;
            public static int letadlovaLength = 5;      
            public static int kriznikLength = 3;
            public static int battleshipLength = 4;
            public static int torpedoborecLength = 2;
            //pocet zasahu
            public static int ponorkaHits = 0;
            public static int letadlovaHits = 0;      
            public static int kriznikHits = 0;
            public static int battleshipHits = 0;
            public static int torpedoborecHits = 0;

            public static int pcSunkenShips = 0;
            public static int playerSunkenShips = 0;

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

        static bool ShipInField(int x, int shipLength)
        {
            for (int i = 0; i < shipLength-1; i++)
            {
                
                if (x > Global.playerArray.GetLength(1))
                {
                    return false;
                }
                x++;
            }
            return true;
        }

        //kontrola, jestli se pri zadavani lodi zadane lode neprekryvaji
        static bool CheckPosition(int x, int y, string[,] yourArray, string verHor, int shipLength)    
        {
            int number = 0;
            bool shipInField = ShipInField(x, shipLength);
            if (verHor == "H")
            {
                while (number != shipLength && !shipInField)
                {
                    if (yourArray[x, y + number] != "* ")
                    {
                        return false;
                    }
                    number++;
                }
                return true;
            }
            else
            {
                while (number != shipLength && !shipInField)
                {
                    if (yourArray[x+ number, y] != "* ")
                    {
                        return false;
                    }
                    number++;
                }
                return true;
            }
        }


        static void ComputerShipLayout()       //nahodne rozmistuje lode pocitace + kontroluje, jestli se vejdou/neprekryvaji
        {
            Random rnd = new Random();
            Random orient = new Random();
            bool pcShipInField = false;

            //torpedoborec
            //int pcX = rnd.Next(0, 10);
            //int pcY = rnd.Next(0, 10);
            int pcX = 1;
            int pcY = 1;
            //int pcOrientation = orient.Next(1, 3);
            int pcOrientation = 2;
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
            while (!pcShipInField)
            {

                if (pcOrientation == 1)
                {
                    if (pcX + 1 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 2; i++)
                            Global.pcArray[pcX + i, pcY] = "T ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcX = rnd.Next(0, 10);
                    }
                }
                else
                {
                    if (pcY + 1 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 2; i++)
                            Global.pcArray[pcX, pcY + i] = "T ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcY = rnd.Next(0, 10);
                    }
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
            pcShipInField = false;
            while (!pcShipInField)
            {
                if (pcOrientation == 1)
                {
                    if (pcX + 1 < Global.pcArray.GetLength(1) - 1 && pcX + 2 < Global.pcArray.GetLength(1) - 1 && pcX + 3 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 4; i++)
                            Global.pcArray[pcX + i, pcY] = "B ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcX = rnd.Next(0, 10);
                    }
                }
                else
                {
                    if (pcY + 1 < Global.pcArray.GetLength(1) - 1 && pcY + 2 < Global.pcArray.GetLength(1) - 1 && pcY + 3 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 4; i++)
                            Global.pcArray[pcX, pcY + i] = "B ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcY = rnd.Next(0, 10);
                    }
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
            pcShipInField = false;
            while (!pcShipInField)
            {
                if (pcOrientation == 1)
                {
                    if (pcX + 1 < Global.pcArray.GetLength(1) - 1 && pcX + 2 < Global.pcArray.GetLength(1) - 1 && pcX + 3 < Global.pcArray.GetLength(1) - 1 && pcX + 4 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 5; i++)
                            Global.pcArray[pcX + i, pcY] = "L ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcX = rnd.Next(0, 10);
                    }
                }
                else
                {
                    if (pcY + 1 < Global.pcArray.GetLength(1) - 1 && pcY + 2 < Global.pcArray.GetLength(1) - 1 && pcY + 3 < Global.pcArray.GetLength(1) - 1 && pcY + 4 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 5; i++)
                            Global.pcArray[pcX, pcY + i] = "L ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcY = rnd.Next(0, 10);
                    }
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
            pcShipInField = false;
            while (!pcShipInField)
            {
                if (pcOrientation == 1)
                {
                    if (pcX + 1 < Global.pcArray.GetLength(1) - 1 && pcX + 2 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX + i, pcY] = "K ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcX = rnd.Next(0, 10);
                    }
                }
                else
                {
                    if (pcY + 1 < Global.pcArray.GetLength(1) - 1 && pcY + 2 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX, pcY + i] = "K ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcY = rnd.Next(0, 10);
                    }
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
            pcShipInField = false;
            while (!pcShipInField)
            {
                if (pcOrientation == 1)
                {
                    if (pcX + 1 < Global.pcArray.GetLength(1) - 1 && pcX + 2 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX + i, pcY] = "P ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcX = rnd.Next(0, 10);
                    }
                }
                else
                {
                    if (pcY + 1 < Global.pcArray.GetLength(1) - 1 && pcY + 2 < Global.pcArray.GetLength(1) - 1)
                    {
                        for (int i = 0; i < 3; i++)
                            Global.pcArray[pcX, pcY + i] = "P ";
                        pcShipInField = true;
                    }
                    else
                    {
                        pcShipInField = false;
                        pcY = rnd.Next(0, 10);
                    }
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
            }
        }

        static int WriteX()        //umoznuje uziveteli vypsat souradnice zadane lode
        {
            Console.WriteLine("zadejte souradnice radku (A-J)");

            string x = Convert.ToString(Console.ReadLine());
            while (!Global.yRows.Contains(x))
            {
                Console.WriteLine("uvadejte velka pismena od A do J");
                x = Convert.ToString(Console.ReadLine());
            }
            int index = Global.yRows.IndexOf(x);
            return index;
        }

        static int WriteY()            //umoznuje uziveteli vypsat souradnice zadane lode
        {
            Console.WriteLine("zadejte souradnice sloupce (1-10)");
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
                Console.WriteLine("Kazdou lod muzete zadat jen jednou, zadejte jinou lod");        //overeni, ze je kazda lod jen jednou
                type = Console.ReadLine();
                while (type != "L" && type != "K" && type != "T" && type != "B" && type != "P")         //overeni vstupu
                {
                    Console.WriteLine("zadavejte velka pocatecni pismena lodi: L, K, T, B nebo P");
                    type = Console.ReadLine();
                }
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
                        Console.WriteLine("Lod se sem nevejde");
                        index = WriteX();
                        y = WriteY();
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.torpedoborecLength);
                    }

                    if (orientation == "V")
                    {
                        shipInField = ShipInField(index, Global.torpedoborecLength);
                        if (!shipInField)
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
                        shipInField = ShipInField(y-1, Global.torpedoborecLength);
                        if (!shipInField)
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
                        Console.WriteLine("Lod se sem nevejde");
                        index = WriteX();
                        y = WriteY();
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.battleshipLength);
                    }
                    if (orientation == "V")
                    {
                        shipInField = ShipInField(index, Global.battleshipLength);
                        if (!shipInField)
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
                        shipInField = ShipInField(y-1, Global.battleshipLength);
                        if (!shipInField)
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
                        Console.WriteLine("Lod se sem nevejde");
                        index = WriteX();
                        y = WriteY();
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.letadlovaLength);
                    }
                    if (orientation == "V")
                    {
                        shipInField = ShipInField(index, Global.letadlovaLength);
                        if (shipInField)
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
                        shipInField = ShipInField(y - 1, Global.letadlovaLength);
                        if (!shipInField)
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
                        Console.WriteLine("Lod se sem nevejde");
                        index = WriteX();
                        y = WriteY();
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.kriznikLength);
                    }
                    if (orientation == "V")
                    {
                        shipInField = ShipInField(index, Global.kriznikLength);
                        if (!shipInField)
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
                        shipInField = ShipInField(y-1, Global.kriznikLength);
                        if (!shipInField)
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
                        Console.WriteLine("Lod se sem nevejde");
                        index = WriteX();
                        y = WriteY();
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.ponorkaLength);
                    }
                    if (orientation == "V")
                    {
                        shipInField = ShipInField(index, Global.ponorkaLength);
                        if (!shipInField)
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
                        shipInField = ShipInField(y - 1, Global.ponorkaLength);
                        if (!shipInField)
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

        static void CompArrayReveal(int xShow, int yShow, string[,] pcArrayReveal)
        {
            pcArrayReveal[xShow, yShow - 1] = Global.pcArray[xShow, yShow - 1];
            Console.WriteLine("Pole pocitace");
            Console.WriteLine("  ");
            Console.Write("  ");
            foreach (int name in Global.columns)
            {
                Console.Write(name + " ");
            }
            Console.WriteLine(" ");
            for (int i = 0; i < pcArrayReveal.GetLength(0); i++)
            {
                Console.Write(Global.rows[i] + " ");
                for (int j = 0; j < pcArrayReveal.GetLength(1); j++)
                {
                    Console.Write(pcArrayReveal[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void PlayerShooting(string[,] pcArrayReveal)
        {
            int xShooting = WriteX();
            int yShooting = WriteY();
            Console.Clear();
            if (Global.pcArray[xShooting, yShooting-1] == "* ")
            {
                Global.pcArray[xShooting, yShooting-1] = "- ";
                pcArrayReveal[xShooting, yShooting-1] = Global.pcArray[xShooting, yShooting-1];
                CompArrayReveal(xShooting, yShooting, pcArrayReveal);
                Console.WriteLine("nezasahli jste");
            }
            else 
            {
                CompArrayReveal(xShooting, yShooting, pcArrayReveal);
                Console.WriteLine("zasah!");
                if (pcArrayReveal[xShooting, yShooting-1] == "T ")
                {
                    Global.torpedoborecHits++;
                    if (Global.torpedoborecHits == Global.torpedoborecLength)
                    {
                        Console.WriteLine("potopili jste torpedoborec");
                        Global.pcSunkenShips++;
                    }
                }
                if (pcArrayReveal[xShooting, yShooting - 1] == "K ")
                {
                    Global.kriznikHits++;
                    if (Global.kriznikHits == Global.kriznikLength)
                    {
                        Console.WriteLine("potopili jste kriznik");
                        Global.pcSunkenShips++;
                    }
                }
                if (pcArrayReveal[xShooting, yShooting - 1] == "B ")
                {
                    Global.battleshipHits++;
                    if (Global.battleshipHits == Global.battleshipLength)
                    {
                        Console.WriteLine("potopili jste bitevni lod");
                        Global.pcSunkenShips++;
                    }
                }
                if (pcArrayReveal[xShooting, yShooting - 1] == "L ")
                {
                    Global.letadlovaHits++;
                    if (Global.letadlovaHits == Global.letadlovaLength)
                    {
                        Console.WriteLine("potopili jste letadlovou lod");
                        Global.pcSunkenShips++;
                    }
                }
                if (pcArrayReveal[xShooting, yShooting - 1] == "P ")
                {
                    Global.ponorkaHits++;
                    if (Global.ponorkaHits == Global.ponorkaLength)
                    {
                        Console.WriteLine("potopili jste ponorku");
                        Global.pcSunkenShips++;
                    }
                }
            }
        }

        static void ComputerShooting ()
        {
            Random rnd = new Random();
            int xShooting = rnd.Next(0, 10);
            int yShooting = rnd.Next(0, 10);
            Console.WriteLine();
            if (Global.playerArray[xShooting, yShooting] != "* ")
            {
                Console.WriteLine("pocitac zasahl vasi lod");
                Global.playerArray[xShooting, yShooting] = "/ ";
                PlayerArray();
            }
            else
            {
                Console.WriteLine("pocitac nezasahl vasi lod");
                Global.playerArray[xShooting, yShooting] = "- ";
                PlayerArray();
            }
            bool notSunken = false;
            for (int i = 0; i < Global.playerArray.GetLength(0); i++)
            {
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    if (Global.playerArray[i,j] == "T ")
                    {
                        notSunken = true;
                        break;
                    }
                    else
                    {
                        notSunken = false;
                    }
                }
                if (notSunken)
                {
                    break;
                }
            }
            if (!notSunken)
            {
                Console.WriteLine("pocitac vam potopil torpedoborec");
                Global.playerSunkenShips++;
            }

            //kriznik
            notSunken = false;
            for (int i = 0; i < Global.playerArray.GetLength(0); i++)
            {
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    if (Global.playerArray[i, j] == "K ")
                    {
                        notSunken = true;
                        break;
                    }
                    else
                    {
                        notSunken = false;
                    }
                }
                if (notSunken)
                {
                    break;
                }
            }
            if (!notSunken)
            {
                Console.WriteLine("pocitac vam potopil kriznik");
                Global.playerSunkenShips++;
            }
            //bitevni lod
            notSunken = false;
            for (int i = 0; i < Global.playerArray.GetLength(0); i++)
            {
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    if (Global.playerArray[i, j] == "B ")
                    {
                        notSunken = true;
                        break;
                    }
                    else
                    {
                        notSunken = false;
                    }
                }
                if (notSunken)
                {
                    break;
                }
            }
            if (!notSunken)
            {
                Console.WriteLine("pocitac vam potopil bitevni lod");
                Global.playerSunkenShips++;
            }
            //letadlova
            notSunken = false;
            for (int i = 0; i < Global.playerArray.GetLength(0); i++)
            {
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    if (Global.playerArray[i, j] == "L ")
                    {
                        notSunken = true;
                        break;
                    }
                    else
                    {
                        notSunken = false;
                    }
                }
                if (notSunken)
                {
                    break;
                }
            }
            if (!notSunken)
            {
                Console.WriteLine("pocitac vam potopil letadlovou lod");
                Global.playerSunkenShips++;
            }
            //ponorka
            notSunken = false;
            for (int i = 0; i < Global.playerArray.GetLength(0); i++)
            {
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    if (Global.playerArray[i, j] == "P ")
                    {
                        notSunken = true;
                        break;
                    }
                    else
                    {
                        notSunken = false;
                    }
                }
                if (notSunken)
                {
                    break;
                }
            }
            if (!notSunken)
            {
                Console.WriteLine("pocitac vam potopil ponorku");
                Global.playerSunkenShips++;
            }
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
            Console.WriteLine("Rozmistete si lode: zadavejte vzdy souradnice leveho horniho rohu lode");
                while (Global.ponorka != 1 || Global.torpedoborec != 1 || Global.battleship != 1 || Global.letadlova != 1 || Global.kriznik != 1)
                {
                    ShipLayout();                   //opakuje se zadavani lodi do pole, dokud tam neni vsech pet lodi
                    ComputerArray();
                    PlayerArray();
                }
                ComputerShipLayout();           //pocitac zada lode do pole

            string[,] pcArrayReveal = new string[10, 10];
            for (int i = 0; i < pcArrayReveal.GetLength(0); i++)
            {
                for (int j = 0; j < pcArrayReveal.GetLength(1); j++)
                {
                    pcArrayReveal[i, j] = "* ";
                }
            }
            while (Global.pcSunkenShips != 5 && Global.playerSunkenShips != 5)
            {
                Console.WriteLine("jste na tahu, zadejte souradnice, kam chcete vystrelit");
                PlayerShooting(pcArrayReveal);
                Console.WriteLine("na tahu je pocitac");
                ComputerShooting();
            }

            if (Global.pcSunkenShips == 5)
            {
                Console.WriteLine("vyhrali jste!");
            }
            else
            {
                Console.WriteLine("prohrali jste:(");
            }

            Console.ReadKey();
            }
    }
}
