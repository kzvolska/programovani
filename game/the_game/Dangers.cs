using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_game
{
    internal class Dangers
    {
        public int health;
        public int damage;


        public virtual void SetHealth(int value)
        {
            health = value;
        }
        public virtual void SetDamage(int value)
        {
            damage = value;
        }
        public virtual int GetHealth()
        {
            return health;
        }
        public virtual int GetDamage()
        { return damage; }

        public virtual bool IsDead()
        {
            return health <= 0;
        }

        public virtual void Hurt(int amount)
        { }
    }

    class Wolf : Dangers
    {
        public Wolf(int health, int damage)
        {
            SetHealth(health);
            SetDamage(damage);
        }
        public override int GetHealth()
        {
            return base.GetHealth();
        }
        public override int GetDamage()
        {
            return base.GetDamage();
        }
        public override void Hurt(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Console.WriteLine("Zabili jste vlka! Muzete pokracovat v ceste");
            }
            else
            {
                Console.WriteLine("Zasahli jste vlka! Z jeho zivota zbyva: " + health);
            }
        }
        public override bool IsDead()
        {
            return base.IsDead();
        }
    }

    class Native : Dangers
    {
        private string weapon;
        public Native(int health, string weapon)
        {
            SetHealth(health);
            SetWeapon(weapon);
        }
        public override void Hurt(int amount)
        {
            health -= amount;

            if (health <= 0)
            {
                Console.WriteLine("Zabili jste domorodce! Muzete pokracovat v ceste");
            }
            else
            {
                Console.WriteLine("Zasahli jste domorodce! Z jeho zivota zbyva: " + health);
            }

        }

        public void SetWeapon(string type)
        {
            weapon = type;
            if (weapon == "nuz")
            {
                damage = 10;
            }
            else if (weapon == "sekera")
            {
                damage = 15;
            }
            else if (weapon == "luk a sipy")
            {
                damage = 20;
            }
            else if (weapon == "puska")
            {
                damage = 25;
            }
        }
        public override int GetHealth()
        {
            return base.GetHealth();
        }
        public override int GetDamage()
        {
            return base.GetDamage();
        }
        public override bool IsDead()
        {
            return base.IsDead();
        }
    }


}
