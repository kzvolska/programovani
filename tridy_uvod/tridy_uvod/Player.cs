using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tridy_uvod
{
    internal class Player   //pro zalozeni tridy: Zobrazit (View) -> Průzkumník řešení -> pravým tlačítkem na Csharp projekt v pravo -> přidat -> třída
    {
        private int health;
        public int damage;
        public string name;
        private Random rng = new Random();

        public Player(int health, int damage, string name)      //konstruktor
        {
            SetHealth(health);   
            this.damage = damage;       //this ... promenna teto tridy
            this.name = name;
        }

        public void SetHealth(int value)
        {
            health = value;
            if (health < 0)
            {
                health = 0;
            }
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetDamage()
        {
            return damage;
        }

        public int GetRandomDamage()
        {
            //float randomDamage = damage * rng.Next(5, 15) / 10f;
            //return (int) randomDamage; //zaokrouhli se to
            return rng.Next(damage / 2, damage + (int)(damage * 1.5f));
        }

        public void Hurt(int amount)
        {
            health -= amount;
            Console.WriteLine("Payer got hit for " + amount + " damage");
            if (health <= 0)
            {
                Console.WriteLine("Player is dead");
            }
        }

        public bool IsDead()
        {
            /*if (health <= 0)
                return true;
            else
                return false;*/
            return health <= 0;
        }
    }
}
