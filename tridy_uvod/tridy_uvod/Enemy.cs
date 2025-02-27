using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tridy_uvod
{
    internal class Enemy
    {
        int healthBase;
        int health;
        int damageBase;
        int damage;
        int level;
        Random rng = new Random();

        public Enemy (int healthBase, int damageBase, int level)
        {
            this.healthBase = healthBase;
            health = this.healthBase * level;
            this.damageBase = damageBase;
            damage = this.damageBase * level;
            this.level = level;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetDamage() 
        { 
            return damage; 
        }

        public void Hurt(int amount)
        {
            health -= amount;
            Console.WriteLine("Enemy got hit for " + amount + " damage");
            if (health <= 0)
            {
                Console.WriteLine("Enemy is dead");
            }
        }

        public int GetRandomDamage()
        {
            return rng.Next(damage / 2, damage + 1);

        }

        public bool IsDead()
        {
            return health <= 0;
        }
    }
}
