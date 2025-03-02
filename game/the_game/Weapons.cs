using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_game
{
    internal class Weapons
    {
        private int bullets;
        private int usage;
        private int damage;

        public virtual void SetBullets (int number)
        {
            bullets = number;
        }
        public virtual int GetBullets () 
        {
            return bullets;
        }

        public virtual void SetUsage(int number)
        {
            usage = number;
        }
        public virtual int GetUsage ()
        {
            return usage;
        }

        public virtual void SetDamage(int number)
        {
            damage = number;
        }
        public virtual int GetDamage()
        {
            return damage;
        }
    }

    class Gun : Weapons 
    {
        public Gun (int bullets, int damage)
        {
            SetBullets (bullets);
            SetDamage (damage);
        }
    }
    class MachineGun : Weapons 
    {
        public MachineGun(int bullets, int damage)
        {
            SetBullets(bullets);
            SetDamage(damage);
        }
    }
    class Knife : Weapons 
    {
        public Knife(int damage)
        {
            SetDamage(damage);
        }
    }
    class Grenade : Weapons 
    {
        public Grenade(int damage)
        {
            SetDamage(damage);
        }
    }

}
