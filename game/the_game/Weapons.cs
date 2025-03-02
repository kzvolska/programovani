using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_game
{
    internal class Weapons //zbrane maji pocet naboju (puska a samopal) a damage, kterou pusobi
    {
        private int bullets;
        private int damage;

        public virtual void SetBullets (int number)
        {
            bullets = number;
        }
        public virtual int GetBullets () 
        {
            return bullets;
        }

        public virtual void SetDamage(int number)
        {
            damage = number;
        }
        public virtual int GetDamage()
        {
            return damage;
        }
        public virtual void Firing()
        {
            bullets--;
        }
        public virtual void Loading()
        {
            bullets += 15;
        }
    }

    class Gun : Weapons 
    {
        public Gun (int bullets, int damage)
        {
            SetBullets (bullets);
            SetDamage (damage);
        }
        public override int GetBullets()
        {
            return base.GetBullets ();
        }
    }
    class MachineGun : Weapons 
    {
        public MachineGun(int bullets, int damage)
        {
            SetBullets(bullets);
            SetDamage(damage);
        }
        public override int GetBullets()
        {
            return base.GetBullets();
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
