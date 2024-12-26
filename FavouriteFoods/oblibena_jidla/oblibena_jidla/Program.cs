using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oblibena_jidla
{
    internal class Program
    {
        static void PrintList(List<string> list)
        {
            foreach (string name in list) 
            { 
                Console.WriteLine(name);
            }
        }
        static void Main(string[] args)
        {
            List<string> availableFoods = new List<string>();
            availableFoods.Add("bolonske spagety");
            availableFoods.Add("rizoto");
            availableFoods.Add("rizek s kasi");
            availableFoods.Add("bramboraky");
            availableFoods.Add("vepro, knedlo, zelo");

            Console.WriteLine("seznam oblibenych jidel");
            PrintList(availableFoods);

            Console.WriteLine("zadejte cislo polozky, kterou chcete odstranit");
            int p = int.Parse(Console.ReadLine());
            
            

            Console.ReadKey();
        }
    }
}
