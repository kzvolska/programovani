using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tridy_uvod
{
    internal class Program
    {
        static void Duel(Player player, Enemy enemy)
        {
            while (!player.IsDead() && !enemy.IsDead())
            {
                //utok hrace na enemaka
                enemy.Hurt(player.GetRandomDamage());

                if (!enemy.IsDead())
                {
                    //utok enemaka na hrace
                    player.Hurt(enemy.GetRandomDamage());
                }
                //vypis
                Console.WriteLine("Player health " + player.GetHealth());
                Console.WriteLine("Enemy health " + enemy.GetHealth());
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Player player = new Player(100, 10, "Ignac");  //kazda trida ma prazdny konstruktor, instance tridy
            //player.SetHealth(100);
            //player.damage = 10;
            //player.name = "Ignac";
            Enemy enemy = new Enemy(20, 2, 1);

            Duel(player, enemy);

            Enemy enemy2 = new Enemy(20, 5, 2);

            Duel(player, enemy2);

            Console.ReadKey();
        }
    }
}
