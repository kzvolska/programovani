using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace listy_slovniky
{
    internal class Program
    {
        static void PrintList(List<string> list)
        {
            foreach (string name in list)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            List<string> myStringList = new List<string>();
            myStringList.Add("Karel");
            myStringList.Add("Lojza");
            myStringList.Add("Xaver");
            myStringList.Add("Xenie");
            myStringList.Add("Andromeda");
            myStringList.Add("Cecilie");

            PrintList(myStringList);

            myStringList.RemoveAt(2);

            PrintList(myStringList);

            myStringList.Remove("Karel");

            PrintList(myStringList);

            if (myStringList.Exists(name => name.StartsWith("X")))
            {
                Console.WriteLine("v listu existuje jmeno zacinajici na X");
            }
            else
            {
                Console.WriteLine("v listu neexistuje jmeno zacinajici na X");
            }

            Dictionary<string, string> favoriteFoods = new Dictionary<string, string>();
            favoriteFoods["Karel"] = "Buchticky se sodo";
            favoriteFoods["Lojza"] = "Vypecky se zelim";
            favoriteFoods["Xaver"] = "Sisky s makem";
            favoriteFoods["Xenie"] = "Jitrnice s horcici";
            favoriteFoods["Andromeda"] = "Kabanos";
            favoriteFoods["Cecilie"] = "Durum kebeb se syrem, cesnek bylinky";

            foreach (KeyValuePair<string, string> studentFood in favoriteFoods)
            {
                string name = studentFood.Key;
                string food = studentFood.Value;
                Console.WriteLine("Oblibene jidlo studenta " + name + " je " + food);
            }

            if (favoriteFoods.ContainsKey("Martin"))
            {
                Console.WriteLine("Martin je v seznamu a ma oblibene jidlo");
            }
            else 
            {
                Console.WriteLine("Martin neni v seznamu a zije z energie vesmiru");
            }

            Console.ReadKey();  
        }
    }
}
