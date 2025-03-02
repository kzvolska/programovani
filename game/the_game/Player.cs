using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_game
{
    internal class Player
    {
        private int health;
        private int money;
        private int hunger;
        private int damage;
        private int vehicleNumber;
        private int weaponNumber;
        private string weapon;
        private string vehicle;

        public Player (int health, int money, int hunger, int vehicleNumber, int weaponNumber)
        {
            SetHealth (health);
            SetMoney (money);
            SetHunger (hunger);
            SetVehicleNumber (vehicleNumber);
            SetWeaponNumber (weaponNumber);
        }
        public void SetVehicleNumber(int number)
        {
            vehicleNumber = number;
        }
        public int GetVehicleNumber()
        {
            return vehicleNumber;
        }
        public void SetVehicle(string vehicleType)
        {
            vehicle = vehicleType;
        }
        public string GetVehicle()
        {
            return vehicle;
        }
        public void FoundVehicle(string vehicleType)
        {
            vehicleNumber++;
            vehicle = vehicleType;
        }
        public void SetWeaponNumber(int number)
        {
            weaponNumber = number;
        }
        public int GetWeaponNumber()
        {
            return weaponNumber;
        }
        public void FoundWeapon(string weaponType)
        {
            weaponNumber++;
            weapon = weaponType;
        }
        public void LeaveWeapon()
        {
            weaponNumber--;
        }
        public void SetHealth(int value)
        {
            health = value;
        }
        public int GetHealth()
        {
            return health;
        }
        public void SetMoney(int value)
        {
            money = value;
        }
        public int GetMoney() { return money; }
        public void FoundMoney ()
        {
            money += 10;
            Console.WriteLine("Stav vaseho avatara: \npenize: " +  money);
        }
        public void Buying(int amount)
        {
            money -= amount;
        }
        public void SetHunger(int value)
        {
            hunger = value;
        }
        public int GetHunger() { return hunger; }
        public void Feeding()
        {
            hunger = 0;
        }
        public void Starving()
        {
            hunger += 5;
            if (hunger >= 50)
            {
                Console.WriteLine("Vas avatar vyhladovel! \nGAME OVER!");
            }
        }

        public void Healing()
        {
            health += 5;
        }
        public void SetWeapon(string weaponType)
        {
            weapon = weaponType;
        }
        public string GetWeapon()
        { return weapon; }

        public void SetDamage()
        {
            if (weapon == "pistole")
            {
                damage = 20;
            }
            else if (weapon == "nuz")
            {
                damage = 15;
            }
            else if (weapon == "samopal")
            {
                damage = 50;
            }
            else if (weapon == "granat")
            {
                damage = 100;
            }
            else damage = 10;
        }
        public int GetDamage()
        {
            SetDamage();
            return damage;
        }
        public void Hurt(int amount)
        {
            health -= amount;
            Console.WriteLine("Byli jste zasazeni! Z vaseho zivota zbyva: " + health);
            if (health <= 0)
            {
                Console.WriteLine("Vas avatar byl zabit! \nGAME OVER!");
            }
        }
        public bool IsDead ()
        {
            if (health <= 0 || hunger == 50)
                return true;
            else
                return false;
        }
    }
}
