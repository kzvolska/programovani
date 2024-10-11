using System;
using System.Diagnostics.Eventing.Reader;

namespace Calculator
{
    internal class Program
    {
        static double LoadNumber() //pracuji s typem double, protoze pozdeji vyuzivam mocniny a odmocniny
        {
            Console.WriteLine("zadejte libovolne cislo"); //funkce na nacteni vstupu 
            double num;
            while (!double.TryParse(Console.ReadLine(), out num)) //kontrola vstupu uzivatele, nepusti ho dal, dokud nebude spravny (nasla jsem si, jak se dela)
                Console.WriteLine("neplatny vstup, zadejte libovolne realne cislo");
            return num; //vrati nactene cislo
        }
        static double Plus(double first, double second) //nasleduji funkce pro jednotlive operace
        {
            double result = first + second;
            return result;
        }
        static double Minus(double first, double second)
        {
            double result = first - second;
            return result;
        }
        static double Multiply(double first, double second)
        {
            double result = first * second;
            return result;
        }
        static double Divide(double first, double second)
        {
            double result = first / second;
            return result;
        }
        static double Power(double first, double second)
        {
            double result = Math.Pow(first, second);
            return result;
        }
        static string Operation()
        {
            Console.WriteLine("zadejte velke zvyraznene pismeno operace, kterou chcete provest (Soucet, Rozdil, sOucin, Podil, druha Mocnina, druha oDmocnina, prEvod do jine ciselne soustavy)");
            string operace = Console.ReadLine();
            while (operace != "S" && operace != "R" && operace != "O" && operace != "P" && operace != "M" && operace != "D" && operace != "E") //dokud uzivatel nezada spravne nazev operace, nebude pusten dal
            {
                Console.WriteLine("pismeno operace musi byt bud S, R, O, P, M, D nebo E)");
                operace = Console.ReadLine();
            }
            return operace;
        }
        static void Calculation() //funkce s vypocty, abych nemusela porad opisovat
        {
            double first = LoadNumber(); //zavolam funkci pro nacteni cisel a nactu si dve cisla
            double second = LoadNumber();
            string operation = Operation(); //zavolam funkci pro nacteni vstupu pro typ operace
            double result = 0; //nadefinuji promennou result, do ktere se budou ukladat vysledky
            if (operation == "S") //pokud uzivatel zada S, cisla se sectou
            {
                result = Plus(first, second); //zavola se prislusna funkce
                Console.WriteLine("soucet vami zadanych cisel je " + result); //vypise se vysledek
            }
            else if (operation == "R") //pokud zada R, cisla se odectou
            {
                result = Minus(first, second);
                Console.WriteLine("rozdil vami zadanych cisel je " + result);
            }
            else if (operation == "O") //pokud zada O, cisla se vynasobi
            {
                result = Multiply(first, second);
                Console.WriteLine("soucin vami zadanych cisel je " + result);
            }
            else if (operation == "P") //pokud zada P, cisla se podeli
            {
                if (second == 0)
                {
                    Console.WriteLine("operaci neni mozne provest, delite nulou"); //osetrim deleni nulou
                }
                else
                {
                    result = Divide(first, second);
                    Console.WriteLine("podil vami zadanych cisel je " + result);
                }
            }
            else if (operation == "M") //pokud zada M, cisla se podeli
            {
                if (first == 0 && second == 0)
                {
                    Console.WriteLine("operaci neni mozne provest, mocnite nulu nulou"); //osetruji, ze se nemocni nula nulou
                }
                else if (first < 0 && second - (int)second > 0) //pokud bude prvni cislo zaporne a druhe desetinne (nasla jsem si, jak zapsat) - desetinna cast je vetsi nez nula
                {
                    Console.WriteLine("operaci neni mozne provest"); //osetruji, ze neodmocnocnuji zaporne cislo
                }
                else
                {
                    result = Power(first, second);
                    Console.WriteLine(second + ". mocnina cisla " + first + " je " + result);
                }
            }
            else if (operation == "D")//pokud zada D, prvni cislo se odmocni druhym
            {
                if (first < 0 && second % 2 == 0)
                {
                    Console.WriteLine("operaci neni mozne provest"); //zaporne cislo nejde odmocnit, pokud je odmocnina suda 
                }
                else if (second == 0)
                {
                    Console.WriteLine("operaci neni mozne provest"); //nemuzu odmocnit nulou
                }
                else
                {
                    result = Power(first, 1 / second); //
                    Console.WriteLine(second + ". odmocnina z " + first + " je " + result);
                }
            }
            else //pokud zada E, prevede se do soustavy druheho cisla (nasla jsem si, jak se dela)
            {
                if (first < 0)
                {
                    Console.WriteLine("operaci neni mozne provest, prevadene cislo nesmi byt zaporne"); //neprevadi zaporna cisla
                }
                else if (second < 2 && second - (int)second > 0)
                {
                    Console.WriteLine("operaci neni mozne provest, soustava musi byt dvojkova nebo vyssi a musi to byt cele cislo"); //neprevadi nizsi nez 2 a desetinne soustavy
                }
                else
                {
                    string resultString = Convert.ToString((int)first, (int)second); //musi byt ulozeno ve stringu a cisla v integeru
                    Console.WriteLine("reprezentace cisla " + first + " v " + second + ". soustave: " + resultString);
                }
            }
        }
        static void Main(string[] args)
        {
            string ending = "";
            while (ending != "k") //vypocty se budou opakovat, dokud nestiskne uzivatel pismeno k
            {
                Calculation();
                Console.WriteLine("pro ukonceni zadejte 'k', pro pokracovani stisknete enter");
                ending = Console.ReadLine();
            }
        }
    }
}
