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
            //zavedla jsem si globalni promenne, abych je mohla pouzivac napric funkcemi
            //zavedeni listu na oznaceni radku 
            public static List<string> rows = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            public static List<string> yRows = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            //zavedeni pole pocitace a hrace
            public static string[,] pcArray = new string[10, 10];
            public static string[,] playerArray = new string[10, 10];
            //zavedeni poli na oznaceni sloupcu
            public static int[] yColumns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            public static int[] columns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //pocty lodi zadanych hracem do pole - jsou ke kontrole, aby hrac nezadal jednu lod 2x
            public static int ponorka = 0;
            public static int letadlova = 0;
            public static int kriznik = 0;      //vsechno jsem se snazila pojmenovavat v aj, akorat nazvy lodi mam cesky, protoze slovicka neznam a nevyznala bych se v tom
            public static int battleship = 0;
            public static int torpedoborec = 0;

            //delky jednotlivych lodi
            public static int ponorkaLength = 3;
            public static int letadlovaLength = 5;
            public static int kriznikLength = 3;
            public static int battleshipLength = 4;
            public static int torpedoborecLength = 2;

            //pocet zasahu hrace
            public static int ponorkaHits = 0;
            public static int letadlovaHits = 0;
            public static int kriznikHits = 0;
            public static int battleshipHits = 0;
            public static int torpedoborecHits = 0;

            //pocet zasahu pocitace
            public static int pcPonorkaHits = 0;
            public static int pcLetadlovaHits = 0;
            public static int pcKriznikHits = 0;
            public static int pcBattleshipHits = 0;
            public static int pcTorpedoborecHits = 0;

            public static int pcSunkenShips = 0;    //pocet potopenych lodi pocitace
            public static int playerSunkenShips = 0;    //pocet potopenych lodi hrace

            //promenna, ktera hlida, jestli hrac bombu pouzil jen 1x
            public static int bombUsed = 0;
            //promenna, ktera hlida, jestli pocitac bombu pouzil jen 1x
            public static int pcBombUsed = 0;

            //promenne na pocitani strel 
            public static int numberOfBullets = 5;
            public static int pcNumberOfBullets = 5;

        }
        static void ComputerArray()         //vypisuje pole pocitace
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.WriteLine("Pole pocitace");
            Console.WriteLine("  ");
            Console.Write("  ");
            foreach (int name in Global.columns)        //vypisuje oznaceni sloupcu
            {
                Console.Write(name + " ");
            }
            Console.WriteLine(" ");
            for (int i = 0; i < Global.pcArray.GetLength(0); i++)       //vypisuje oznaceni radku
            {
                Console.Write(Global.rows[i] + " ");
                for (int j = 0; j < Global.pcArray.GetLength(1); j++)
                {
                    Console.Write(Global.pcArray[i, j]);        //vypisuje hodnoty
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        static void PlayerArray()          //vypisuje pole uzivatele (stejny system jako pole pocitace)
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

        static bool ShipInField(int x, int y, string verHor, int shipLength)        //kontroluje, jestli se lod vejde do pole
        {
            if (verHor == "H")
            {
                for (int i = 0; i < shipLength; i++)        //pokud horizontalne, opakuje tolikrat, jak je dlouha dana lod, jestli je souradnice sloupce v poli
                {

                    if (y > Global.playerArray.GetLength(1) - 1)
                    {
                        return false;
                    }
                    y++;
                }
                return true;
            }
            else
            {
                for (int i = 0; i < shipLength; i++)        //pokud vertikalne, jestli je souradnice radku v poli
                {

                    if (x > Global.playerArray.GetLength(1) - 1)
                    {
                        return false;
                    }
                    x++;
                }
                return true;            //vraci false, kdyz se nevejde, true, kdyz vejde
            }

        }

        //kontrola, jestli se pri zadavani lodi zadane lode neprekryvaji
        static bool CheckPosition(int x, int y, string[,] yourArray, string verHor, int shipLength)
        {
            int number = 0;
            if (verHor == "H")      //pokud horizontalne - kontroluje, jestli s posouvanim doprava je vedlejsi hodnota * nebo pismeno lodi
            {
                while (number != shipLength)
                {
                    if (yourArray[x, y + number] != "* ")
                    {
                        return false;       //pokud je vedle lod, vrati false
                    }
                    number++;
                }
                return true;    //jinak vrati true
            }
            else
            {
                while (number != shipLength)    //vertikalne stejny princip, akorat pricitani smerem dolu
                {
                    if (yourArray[x + number, y] != "* ")
                    {
                        return false;
                    }
                    number++;
                }
                return true;
            }
        }
        static void ComputerShipLayout()    //rozlozeni lodi pocitace
        {
            Random rnd = new Random();
            Random orient = new Random();
            bool pcShipInField = false;
            bool goodPosition = false;
            int pcX;
            int pcY;
            string horVer;
            int pcOrientation;
            bool pcCorrectPosition;

            while (!goodPosition)       //probiha dokola, dokud se lod nevejde do pole a neprekryva se s jinou
            {
                //torpedoborec
                pcX = rnd.Next(0, 10);      //zadava nahodne souradnice
                pcY = rnd.Next(0, 10);
                pcOrientation = orient.Next(1, 3);  //zadava nahodne, jestli chce lod horizontalne nebo vertikalne
                pcCorrectPosition = true;
                if (pcOrientation == 1)
                {
                    horVer = "V";
                }
                else
                {
                    horVer = "H";
                }
                pcShipInField = ShipInField(pcX, pcY, horVer, Global.torpedoborecLength);
                if (pcShipInField)      //kontrola, jestli je lod v poli
                {
                    pcCorrectPosition = CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.torpedoborecLength);
                    if (pcCorrectPosition)      //kontrola, jestli se neprekryva
                    {
                        // ukonceni while
                        goodPosition = true;

                        //zapsani lode do pcArray
                        if (pcOrientation == 1)
                        {
                            for (int i = 0; i < 2; i++)
                                Global.pcArray[pcX + i, pcY] = "T ";
                        }
                        else
                        {
                            for (int i = 0; i < 2; i++)
                                Global.pcArray[pcX, pcY + i] = "T ";
                        }
                    }
                }
            }
            //to same pro kazdou dalsi lod - vse stejne, akorat pokazde pise jine pismeno
            //bitevni lod
            goodPosition = false;
            while (!goodPosition)
            {
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
                pcShipInField = ShipInField(pcX, pcY, horVer, Global.battleshipLength);
                if (pcShipInField)
                {
                    pcCorrectPosition = CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.battleshipLength);
                    if (pcCorrectPosition)
                    {
                        goodPosition = true;
                        if (pcOrientation == 1)
                        {
                            for (int i = 0; i < 4; i++)
                                Global.pcArray[pcX + i, pcY] = "B ";
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                                Global.pcArray[pcX, pcY + i] = "B ";
                        }
                    }
                }
            }

            //letadlova lod
            goodPosition = false;
            while (!goodPosition)
            {
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
                pcShipInField = ShipInField(pcX, pcY, horVer, Global.letadlovaLength);
                if (pcShipInField)
                {
                    pcCorrectPosition = CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.letadlovaLength);
                    if (pcCorrectPosition)
                    {
                        goodPosition = true;
                        if (pcOrientation == 1)
                        {
                            for (int i = 0; i < 5; i++)
                                Global.pcArray[pcX + i, pcY] = "L ";
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                                Global.pcArray[pcX, pcY + i] = "L ";
                        }
                    }
                }
            }

            //kriznik
            goodPosition = false;
            while (!goodPosition)
            {
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
                pcShipInField = ShipInField(pcX, pcY, horVer, Global.kriznikLength);
                if (pcShipInField)
                {
                    pcCorrectPosition = CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.kriznikLength);
                    if (pcCorrectPosition)
                    {
                        goodPosition = true;
                        if (pcOrientation == 1)
                        {
                            for (int i = 0; i < 3; i++)
                                Global.pcArray[pcX + i, pcY] = "K ";
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                                Global.pcArray[pcX, pcY + i] = "K ";
                        }
                    }
                }
            }

            //ponorka
            goodPosition = false;
            while (!goodPosition)
            {
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
                pcShipInField = ShipInField(pcX, pcY, horVer, Global.ponorkaLength);
                if (pcShipInField)
                {
                    pcCorrectPosition = CheckPosition(pcX, pcY, Global.pcArray, horVer, Global.ponorkaLength);
                    if (pcCorrectPosition)
                    {
                        goodPosition = true;
                        if (pcOrientation == 1)
                        {
                            for (int i = 0; i < 3; i++)
                                Global.pcArray[pcX + i, pcY] = "P ";
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                                Global.pcArray[pcX, pcY + i] = "P ";
                        }
                    }
                }
            }
        }

        static int WriteX()        //umoznuje uziveteli vypsat souradnice radku zadavane lode + osetreni vstupu
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

        static int WriteY()            //umoznuje uziveteli vypsat souradnice sloupce zadavane lode + osetreni vstupu
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
        static void ShipLayout()           //vypisovani lodi do pole uzivatele
        {
            bool shipInField = false;
            bool goodPosition = false;
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
            int index;
            int y;
            bool correctPosition = true;
            if (type == "T")        //vypisuje jednotive typy lodi + vola funkci na kontrolovani prekryvani
            {
                while (!goodPosition)
                {
                    index = WriteX();   //napise souradnici radku
                    y = WriteY();       //souradnici sloupce
                    shipInField = ShipInField(index, y - 1, orientation, Global.torpedoborecLength);
                    if (shipInField)    //vola funkci na kontrolu, jestli se lod vejde do pole - kdyz ano, pokracuje se dal
                    {
                        Console.WriteLine("Lod se nevejde do pole");
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.torpedoborecLength);
                        if (correctPosition)    // kontrola prekryvani lodi - kdyz se neprekryvaji, pokracuje se k tisteni lodi
                        {
                            goodPosition = true;
                            if (orientation == "V")
                            {
                                for (int i = 0; i < 2; i++)
                                    Global.playerArray[index + i, y - 1] = "T ";
                                Global.torpedoborec = 1;

                            }
                            else
                            {
                                for (int i = 0; i < 2; i++)
                                    Global.playerArray[index, y - 1 + i] = "T ";
                                Global.torpedoborec = 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("lod se sem nevejde"); //kdyz neni splneno neprekryvani, napise to uzivateli a vrati se na zacatek
                        }
                    }
                    else
                    {
                        Console.WriteLine("lod se nevejde do pole");    //kdyz neni splneno, ze lod je v poli, napise to uzivateli a vrati se na zacatek
                    }
                }
            }
            else if (type == "B")
            {
                while (!goodPosition)
                {
                    index = WriteX();
                    y = WriteY();
                    shipInField = ShipInField(index, y - 1, orientation, Global.battleshipLength);
                    if (shipInField)
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.battleshipLength);
                        if (correctPosition)
                        {
                            goodPosition = true;
                            if (orientation == "V")
                            {
                                for (int i = 0; i < 4; i++)
                                    Global.playerArray[index + i, y - 1] = "B ";
                                Global.battleship = 1;
                            }
                            else
                            {
                                for (int i = 0; i < 4; i++)
                                    Global.playerArray[index, y - 1 + i] = "B ";
                                Global.battleship = 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("lod se sem nevejde");
                        }
                    }
                    else
                    {
                        Console.WriteLine("lod se nevejde do pole");
                    }
                }
            }
            else if (type == "L")
            {
                while (!goodPosition)
                {
                    index = WriteX();
                    y = WriteY();
                    shipInField = ShipInField(index, y - 1, orientation, Global.letadlovaLength);
                    if (shipInField)
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.letadlovaLength);
                        if (correctPosition)
                        {
                            goodPosition = true;
                            if (orientation == "V")
                            {
                                for (int i = 0; i < 5; i++)
                                    Global.playerArray[index + i, y - 1] = "L ";
                                Global.letadlova = 1;
                            }
                            else
                            {
                                for (int i = 0; i < 5; i++)
                                    Global.playerArray[index, y - 1 + i] = "L ";
                                Global.letadlova = 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("lod se sem nevejde");
                        }
                    }
                    else
                    {
                        Console.WriteLine("lod se nevejde do pole");
                    }
                }

            }
            else if (type == "K")
            {
                while (!goodPosition)
                {
                    index = WriteX();
                    y = WriteY();
                    shipInField = ShipInField(index, y - 1, orientation, Global.kriznikLength);
                    if (shipInField)
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.kriznikLength);
                        if (correctPosition)
                        {
                            goodPosition = true;
                            if (orientation == "V")
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index + i, y - 1] = "K ";
                                Global.kriznik = 1;
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index, y - 1 + i] = "K ";
                                Global.kriznik = 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("lod se sem nevejde");
                        }
                    }
                    else
                    {
                        Console.WriteLine("lod se nevejde do pole");
                    }
                }
            }
            else if (type == "P")
            {
                while (!goodPosition)
                {
                    index = WriteX();
                    y = WriteY();
                    shipInField = ShipInField(index, y - 1, orientation, Global.ponorkaLength);
                    if (shipInField)
                    {
                        correctPosition = CheckPosition(index, y - 1, Global.playerArray, orientation, Global.ponorkaLength);
                        if (correctPosition)
                        {
                            goodPosition = true;
                            if (orientation == "V")
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index + i, y - 1] = "P ";
                                Global.ponorka = 1;
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                    Global.playerArray[index, y - 1 + i] = "P ";
                                Global.ponorka = 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("lod se sem nevejde");
                        }
                    }
                    else
                    {
                        Console.WriteLine("lod se nevejde do pole");
                    }
                }
            }
        }

        static void Hit(int xShooting, int yShooting, string[,] pcArrayReveal)      //co se deje, kdyz se uzivatel trefi do lode
        {
            //kontrola, jestli sem uz uzivatel nestrilel
            while (pcArrayReveal[xShooting, yShooting - 1] == "- " || pcArrayReveal[xShooting, yShooting - 1] == "T " ||
                    pcArrayReveal[xShooting, yShooting - 1] == "B " || pcArrayReveal[xShooting, yShooting - 1] == "P " ||
                    pcArrayReveal[xShooting, yShooting - 1] == "L " || pcArrayReveal[xShooting, yShooting - 1] == "K ")
            {
                CompArrayReveal(pcArrayReveal);
                Console.WriteLine("sem uz jste strileli, strilejte na jine souradnice");
                xShooting = WriteX();   //pokud ano, musi zadat nove souradnice
                yShooting = WriteY();
            }
            if (Global.pcArray[xShooting, yShooting - 1] == "* ")
            {
                pcArrayReveal[xShooting, yShooting - 1] = "- ";
                CompArrayReveal(pcArrayReveal);
            }
            else
            {
                pcArrayReveal[xShooting, yShooting - 1] = Global.pcArray[xShooting, yShooting - 1]; //prepsani hodnoty z pcArray do pole, ktere uzivatel vidi
                CompArrayReveal(pcArrayReveal);       //pokud ne, zobrazi se uzivateli, co na teto souradnicic mel pocitac 
                if (pcArrayReveal[xShooting, yShooting - 1] == "T ")        //pokud trefil napr. torpedoborec a uz ma 1 trefeny -> torpedoborec je potopeny, zvysuje se pocet potopenych lodi uzivatelem
                {
                    Global.torpedoborecHits++;
                    if (Global.torpedoborecHits == Global.torpedoborecLength)
                    {
                        Console.WriteLine("potopili jste torpedoborec");
                        Global.pcSunkenShips++;
                    }
                }
                if (pcArrayReveal[xShooting, yShooting - 1] == "K ")        //atd.
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

        static void Bomb(int xShooting, int yShooting, string verHor, string[,] pcArrayReveal)
        {
            if (verHor == "H")      //kdyz bomba horizontalne, prepise tri po sobe jdouci vedle sebe lezici hvezdicky, podle toho, jestli tam je lod nebo ne
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Global.pcArray[xShooting, yShooting - 1 + i] == "* ")
                    {
                        Global.pcArray[xShooting, yShooting - 1 + i] = "- ";
                        pcArrayReveal[xShooting, yShooting - 1 + i] = Global.pcArray[xShooting, yShooting - 1 + i];
                        CompArrayReveal(pcArrayReveal);
                    }
                    else
                    {
                        Console.WriteLine("zasah!");
                        Hit(xShooting, yShooting + i, pcArrayReveal);

                    }
                }
            }
            else         //kdyz bomba vertikalne, prepise tri po sobe jdouci pod sebou lezici hvezdicky, podle toho, jestli tam je lod nebo ne
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Global.pcArray[xShooting + i, yShooting - 1] == "* ")
                    {
                        Global.pcArray[xShooting + i, yShooting - 1] = "- ";
                        pcArrayReveal[xShooting + i, yShooting - 1] = Global.pcArray[xShooting + i, yShooting - 1];
                        CompArrayReveal(pcArrayReveal);
                    }
                    else
                    {
                        Console.WriteLine("zasah!");
                        Hit(xShooting + i, yShooting, pcArrayReveal);

                    }
                }
            }
        }

        static void NormalShot(int xShooting, int yShooting, string[,] pcArrayReveal)
        {
            if (Global.pcArray[xShooting, yShooting - 1] == "* ")       //kdyz se netrefi, prepise hvezdicku na - a napise, ze se netrefil
            {
                Global.pcArray[xShooting, yShooting - 1] = "- ";
                pcArrayReveal[xShooting, yShooting - 1] = Global.pcArray[xShooting, yShooting - 1];
                CompArrayReveal(pcArrayReveal);
                Console.WriteLine("nezasahli jste");
            }
            else   //kdyz se trefi, funkce
            {
                Console.WriteLine("zasah!");
                Hit(xShooting, yShooting, pcArrayReveal);

            }
        }

        static void CompArrayReveal(string[,] pcArrayReveal) //zobrazeni uzivateli, co ma pocitac na dane souradnici
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");       //maze konzoli - vsechno predchozi
            Console.WriteLine("Pole pocitace");     //+vypsani tohoto pole
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
        static void PlayerShooting(string[,] pcArrayReveal)     //strili uzivatel
        {
            int xShooting;
            int yShooting;
            string selectWeaponInfo;
            if (Global.numberOfBullets == 0)
            {
                selectWeaponInfo = "nemate zadne naboje, pro doplneni musite jedno kolo vynechat (stisknete S)";
            }
            else
            {
                selectWeaponInfo = "mate " + Global.numberOfBullets + " naboju, pro strelbu stisknete N";
            }
            if (Global.bombUsed == 0)
            {
                selectWeaponInfo = selectWeaponInfo + " pro pouziti bomby stisknete B";
            }
            else
            {
                selectWeaponInfo = selectWeaponInfo + " bomba jiz byla pouzita";
            }
            Console.WriteLine(selectWeaponInfo);
            string weapon = Console.ReadLine();
            bool successfulAction = false;
            while (!successfulAction)
            {
                switch (weapon)
                {
                    case "B":
                        if (Global.bombUsed == 0) //osetreni, ze nepouzije bombu znovu
                        {
                            Console.WriteLine("zadejte, zda chcete bombu spustit V vertikalne nebo H horizontalne");
                            string verHor = Console.ReadLine();
                            while (verHor != "V" && verHor != "H")      //kontrola vstupu
                            {
                                Console.WriteLine("zadejte pouze V nebo H");
                                verHor = Console.ReadLine();
                            }
                            Console.WriteLine("zadejte souradnice pro levy horni roh strely");  //zadava souradnice leveho horniho rohu strely
                            xShooting = WriteX();
                            yShooting = WriteY();
                            bool bombInField = ShipInField(xShooting, yShooting - 1, verHor, 3);
                            while (!bombInField)        //vsechny souradnice bomby musi byt v poli - kontrola
                            {
                                Console.WriteLine("bomba se nevejde do pole");
                                xShooting = WriteX();
                                yShooting = WriteY();
                                bombInField = ShipInField(xShooting, yShooting - 1, verHor, 3);
                            }
                            Bomb(xShooting, yShooting, verHor, pcArrayReveal);
                            Global.bombUsed = 1;        //dale uz bombu nemuze do konce hry pouzit
                            successfulAction = true;
                        }
                        else
                        {
                            Console.WriteLine("Stiskli jste spatnou klavesu");
                            weapon = Console.ReadLine();
                        }
                        break;
                    case "N":
                        if (Global.numberOfBullets != 0)        //osetreni, ze strili jen kdyz ma naboje
                        {
                            xShooting = WriteX();
                            yShooting = WriteY();
                            NormalShot(xShooting, yShooting, pcArrayReveal);
                            Global.numberOfBullets--;       //po kazdem vystrelu se odecitaji naboje
                            successfulAction = true;
                        }
                        else
                        {
                            Console.WriteLine("Stiskli jste spatnou klavesu");
                            weapon = Console.ReadLine();
                        }
                        break;
                    case "S":
                        if (Global.numberOfBullets == 0)
                        {
                            Global.numberOfBullets += 5;        //pricteni 5 naboju
                            CompArrayReveal(pcArrayReveal);
                            successfulAction = true;
                        }
                        else
                        {
                            Console.WriteLine("Stiskli jste spatnou klavesu");
                            weapon = Console.ReadLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Stiskli jste spatnou klavesu");
                        weapon = Console.ReadLine();
                        break;
                }
            }
        }

        static (int, int) XYShooting()      //funkce na urceni souradnice + strategie pocitace
        {
            Random rnd = new Random();
            int xShooting = rnd.Next(0, 10);    //vygeneruje nahodna cisla
            int yShooting = rnd.Next(0, 10);
            for (int i = 0; i < Global.playerArray.GetLength(0); i++)   //prochazi pole a hleda, jestli uz ma nejaky zasah
            {
                for (int j = 0; j < Global.playerArray.GetLength(1); j++)
                {
                    if (Global.playerArray[i, j] == "/ ")       //pokud uz ma zasah, strili na policka okolo
                    {
                        if (i > 0)      //zaroven kontrola, za nevystreli mimo pole
                        {
                            if (Global.playerArray[i - 1, j] != "/ " && Global.playerArray[i - 1, j] != "- ")
                            {
                                return (i - 1, j);  //pokud zjisti, ze na vedlejsim policku jeste nestrilel, vystreli tam
                            }
                        }
                        if (i < 9)
                        {
                            if (Global.playerArray[i + 1, j] != "/ " && Global.playerArray[i + 1, j] != "- ")
                            {
                                return (i + 1, j);      //postupne takto zkontroluje vsechny vedlejsi policka
                            }
                        }
                        if (j > 0)
                        {
                            if (Global.playerArray[i, j - 1] != "/ " && Global.playerArray[i, j - 1] != "- ")
                            {
                                return (i, j - 1);
                            }
                        }
                        if (j < 0)
                        {
                            if (Global.playerArray[i, j + 1] != "/ " && Global.playerArray[i, j + 1] != "- ")
                            {
                                return (i, j + 1);
                            }
                        }
                    }
                }
            }
            return (xShooting, yShooting);  //pokud jeste nema zasah, nebo uz vedle strilel, vrati random cisla
        }

        static void PcHit(int xShooting, int yShooting)
        {
            while (Global.playerArray[xShooting, yShooting] == "- " || Global.playerArray[xShooting, yShooting] == "/ ")
            {
                (xShooting, yShooting) = XYShooting();      //kontrola, ze nestrili 2x na stejne misto
            }
            Console.WriteLine("pocitac zasahl vasi lod");       //pokud zasahne , oznaci pole /, podle toho, o jakou jde lod, zvysi pocet trefeni
            Global.playerArray[xShooting, yShooting] = "/ ";
            if (Global.playerArray[xShooting, yShooting] == "T ")
            {
                Global.pcTorpedoborecHits++;
            }
            else if (Global.playerArray[xShooting, yShooting] == "L ")
            {
                Global.pcLetadlovaHits++;
            }
            else if (Global.playerArray[xShooting, yShooting] == "B ")
            {
                Global.pcBattleshipHits++;
            }
            else if (Global.playerArray[xShooting, yShooting] == "K ")
            {
                Global.pcKriznikHits++;
            }
            else if (Global.playerArray[xShooting, yShooting] == "P ")
            {
                Global.pcPonorkaHits++;
            }
        }
        static void PcBomb(int xShooting, int yShooting, string verHor)
        {
            if (verHor == "H")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Global.playerArray[xShooting, yShooting + i] == "* ")
                    {
                        Global.playerArray[xShooting, yShooting + i] = "- ";
                        Console.WriteLine("pocitac nezasahl vasi lod");
                    }
                    else
                    {
                        PcHit(xShooting, yShooting + i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Global.playerArray[xShooting + i, yShooting] == "* ")
                    {
                        Global.playerArray[xShooting + i, yShooting] = "- ";
                        Console.WriteLine("pocitac nezasahl vasi lod");
                    }
                    else
                    {
                        PcHit(xShooting + i, yShooting);
                    }
                }
            }
        }
        static void ComputerShooting()      //strili pocitac
        {
            int xShooting;
            int yShooting;
            Random rnd = new Random();
            int weapon = rnd.Next(1, 4);
            bool correctAction = false;
            while (!correctAction)
            {
                switch (weapon)
                {
                    case 1:     //bomba
                        if (Global.pcBombUsed == 0)
                        {
                            int bombOrientation = rnd.Next(1, 3);
                            string verHor;
                            if (bombOrientation == 1)
                            {
                                verHor = "H ";
                            }
                            else
                            {
                                verHor = "V ";
                            }
                            (xShooting, yShooting) = XYShooting();
                            bool bombInField = ShipInField(xShooting, yShooting, verHor, 3);
                            while (!bombInField)
                            {
                                (xShooting, yShooting) = XYShooting();
                                bombInField = ShipInField(xShooting, yShooting, verHor, 3);
                            }
                            PcBomb(xShooting, yShooting, verHor);
                            Global.pcBombUsed = 1;
                            correctAction = true;
                        }
                        else
                        {
                            weapon = rnd.Next(1, 4);
                        }
                        break;
                    case 2:     //strileni
                        if (Global.pcNumberOfBullets != 0)
                        {
                            (xShooting, yShooting) = XYShooting();
                            if (Global.playerArray[xShooting, yShooting] != "* ")
                            {
                                PcHit(xShooting, yShooting);
                            }
                            else
                            {
                                Console.WriteLine("pocitac nezasahl vasi lod");     //pokud nezasahne lod, napise -
                                Global.playerArray[xShooting, yShooting] = "- ";
                            }
                            Global.pcNumberOfBullets--;
                            correctAction = true;
                        }
                        else
                        {
                            weapon = rnd.Next(1, 4);
                        }
                        break;
                    case 3:     //preskoceni kola
                        if (Global.pcNumberOfBullets == 0)
                        {
                            Global.pcNumberOfBullets += 5;
                            Console.WriteLine("pocitac preskocil kolo, dobiji si naboje");
                        }
                        else
                        {
                            weapon = rnd.Next(1, 4);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (Global.pcTorpedoborecHits == 2)     //pokud se pocet trefeni rovna delce lodi, je potopena, zvysuje se cislo potopenych lodi pocitacem
            {
                Console.WriteLine("pocitac vam potopil torpedoborec");
                Global.playerSunkenShips++;
            }
            if (Global.pcLetadlovaHits == 5)
            {
                Console.WriteLine("pocitac vam potopil letadlovou lod");
                Global.playerSunkenShips++;
            }
            if (Global.pcBattleshipHits == 4)
            {
                Console.WriteLine("pocitac vam potopil bitevni lod");
                Global.playerSunkenShips++;
            }
            if (Global.pcKriznikHits == 3)
            {
                Console.WriteLine("pocitac vam potopil kriznik");
                Global.playerSunkenShips++;
            }
            if (Global.pcPonorkaHits == 3)
            {
                Console.WriteLine("pocitac vam potopil ponorku");
                Global.playerSunkenShips++;
            }
            PlayerArray();
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
                ShipLayout();                   //opakuje se zadavani lodi do pole a vypisovani poli dokud tam neni vsech pet lodi
                ComputerArray();
                PlayerArray();
            }
            ComputerShipLayout();           //pocitac zada lode do pole
            //ComputerArray(); //TESTOVANI - SMAZAT
            string[,] pcArrayReveal = new string[10, 10];       //zavedeni noveho pole, ktere se ukazuje uzivateli misto pole pocitace, do ktereho si ulozil lode
            for (int i = 0; i < pcArrayReveal.GetLength(0); i++)
            {
                for (int j = 0; j < pcArrayReveal.GetLength(1); j++)
                {
                    pcArrayReveal[i, j] = "* ";     //do pole se ulozi *
                }
            }
            while (Global.pcSunkenShips != 5 && Global.playerSunkenShips != 5)      //dokud nema hrac nebo pocitac vsech 5 lodi potopenych, opakuje se:
            {
                Console.WriteLine("jste na tahu, zadejte souradnice, kam chcete vystrelit"); //informace, kdo je na tahu 
                PlayerShooting(pcArrayReveal);  //strileni hrace
                Console.WriteLine("na tahu je pocitac");
                ComputerShooting();     //strileni pocitace
            }
            if (Global.pcSunkenShips == 5)
            {
                Console.WriteLine("VYHRALI JSTE!");     //pokud vsechny lode drive potopil uzivatel, vyhral
            }
            else
            {
                Console.WriteLine("PROHRALI JSTE:(" +       //pokud vsechny lode drive potopil pocitac, uzivatel prohral
                    "\n GAME OVER");
            }

            Console.ReadKey();
        }
    }
}
