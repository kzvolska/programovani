using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace the_game
{
    internal class Program
    {
        public static class Global
        {
            public static int roundCount = 0;
            public static int[] optionValue = { 20, 90, 100, 40, 50, 25, 2 };

        }
        static void Crossroads()
        {
            Console.WriteLine("Pred sebou vidite rozcesti.\nKudy se vydate dal?");
            Console.WriteLine();
            Console.WriteLine("Stisknete \n'l' pro vlevo\n'p' pro vpravo\n'r' pro rovne");
            string whereNext = Console.ReadLine();
            while (whereNext != "p" && whereNext != "l" && whereNext != "r")
            {
                Console.WriteLine("Stisknete 'p', 'r' nebo 'l'");
                whereNext = Console.ReadLine();
            }
            Console.Clear();
            switch (whereNext)
            {
                case "p":
                    Console.WriteLine("Zahybate tedy doprava");
                    break;
                case "l":
                    Console.WriteLine("Zahybate tedy doleva");
                    break;
                case "r":
                    Console.WriteLine("Jdete tedy rovne");
                    break;
                default:
                    break;
            }
            Global.roundCount++;
        }
        static void FoundEnemy(Player player)
        {
            Console.WriteLine("Pozor! Tamhle v krovi se neco pohnulo!");
            Random rng = new Random();
            int enemy = rng.Next(1, 3);
            if (enemy == 1)
            {
                Wolf wolf = new Wolf(50, 5);
                Console.WriteLine("Vlk!");
                while (!player.IsDead() && !wolf.IsDead())
                {
                    player.Hurt(wolf.GetDamage());
                    if (!player.IsDead())
                        wolf.Hurt(player.GetDamage());
                }
            }
            else if (enemy == 2)
            {
                int weapon = rng.Next(1, 5);
                string weaponType;
                if (weapon == 1)
                {
                    weaponType = "nuz";
                }
                else if (weapon == 2)
                {
                    weaponType = "sekera";
                }
                else if (weapon == 3)
                {
                    weaponType = "luk a sipy";
                }
                else
                {
                    weaponType = "puska";
                }
                Native native = new Native(100, weaponType);
                Console.WriteLine("Domorodec!\nA je ozbrojen! \nZbran domorodce: " + weaponType);
                while (!player.IsDead() && !native.IsDead())
                {
                    player.Hurt(native.GetDamage());
                    if (!player.IsDead())
                        native.Hurt(player.GetDamage());
                }
            }
        }

        static void FoundVehicle()
        {
            Random rng = new Random();
            int vehicle = rng.Next(1, 4);
            if (vehicle == 1)
            {
                Console.WriteLine("Vedle cesty stoji zaparkovane auto");
                Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
                string takeLeave = Console.ReadLine();
                while (takeLeave != "n" && takeLeave != "v")
                {
                    Console.WriteLine("Stisknete 'v' nebo 'n'");
                    takeLeave = Console.ReadLine();
                }
                switch (takeLeave)
                {
                    case "v":
                        Car car = new Car(100);
                        int currentGasAmount = car.GetGas() ;
                        for (int i = 0; i < 4; i++)
                        {
                            Crossroads();
                            currentGasAmount -= 25;
                            Console.WriteLine("Autu dochazi benzin. Stav benzinu: " + currentGasAmount);
                        }
                        Console.WriteLine("Autu dosel benzin. Dal uz musite po svych.");
                        break;
                    case "n":
                        break;
                    default:
                        break;
                }
            }
            else if (vehicle == 2)
            {
                Console.WriteLine("Vedle cesty stoji zaparkovana motorka");
                Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
                string takeLeave = Console.ReadLine();
                while (takeLeave != "n" && takeLeave != "v")
                {
                    Console.WriteLine("Stisknete 'v' nebo 'n'");
                    takeLeave = Console.ReadLine();
                }
                switch (takeLeave)
                {
                    case "v":
                        Motorcycle motorcycle = new Motorcycle(100);
                        int currentGasAmount = motorcycle.GetGas();
                        for (int i = 0; i < 2; i++)
                        {
                            Crossroads();
                            currentGasAmount -= 50;
                            Console.WriteLine("Motorce dochazi benzin. Stav benzinu: " + currentGasAmount);
                        }
                        Console.WriteLine("Motorce dosel benzin. Dal uz musite po svych.");
                        break;
                    case "n":
                        break;
                    default:
                        break;
                }
            }
            else if (vehicle == 3)
            {
                Console.WriteLine("Vede cesty stoji zaparkovane letadlo");
                Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
                string takeLeave = Console.ReadLine();
                while (takeLeave != "n" && takeLeave != "v")
                {
                    Console.WriteLine("Stisknete 'v' nebo 'n'");
                    takeLeave = Console.ReadLine();
                }
                switch (takeLeave)
                {
                    case "v":
                        Plane plane = new Plane(100);
                        int currentGasAmount = plane.GetGas();
                        for (int i = 0; i < 6; i++)
                        {
                            Crossroads();
                            currentGasAmount -= 20;
                            Console.WriteLine("Letadlu dochazi palivo. Stav paliva: " + currentGasAmount);
                        }
                        Console.WriteLine("Letadlu doslo palivo a museli jste nouzove pristat. Dal uz musite po svych.");
                        break;
                    case "n":
                        break;
                    default:
                        break;
                }
            }
        }
        static void FoundWeapon(Player player)
        {
            Random rng = new Random();
            int weapon = rng.Next(1, 5);
            if (weapon == 1)
            {
                Console.WriteLine("Na ceste lezi puska");
                if (player.GetWeaponNumber() == 0)
                {
                    Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            Gun gun = new Gun(5, 20);
                            player.FoundWeapon("puska");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Jednu zbran uz mate. Muzete mit pouze 1 zbran");
                    Console.WriteLine("Stisknete 'v' pro vzit a zahodit stavajici nebo 'n' pro nebrat a nechat stavajici");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            player.LeaveWeapon();
                            Gun gun = new Gun(5, 20);
                            player.FoundWeapon("puska");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (weapon == 2)
            {
                Console.WriteLine("Na ceste lezi samopal");
                if (player.GetWeaponNumber() == 0)
                {
                    Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            MachineGun machinegun = new MachineGun(5, 50);
                            player.FoundWeapon("samopal");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Jednu zbran uz mate. Muzete mit pouze 1 zbran");
                    Console.WriteLine("Stisknete 'v' pro vzit a zahodit stavajici nebo 'n' pro nebrat a nechat stavajici");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            player.LeaveWeapon();
                            Gun gun = new Gun(5, 50);
                            player.FoundWeapon("samopal");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (weapon == 3)
            {
                Console.WriteLine("Na ceste lezi nuz");
                if (player.GetWeaponNumber() == 0)
                {
                    Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            Knife knife = new Knife(15);
                            player.FoundWeapon("nuz");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Jednu zbran uz mate. Muzete mit pouze 1 zbran");
                    Console.WriteLine("Stisknete 'v' pro vzit a zahodit stavajici nebo 'n' pro nebrat a nechat stavajici");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            player.LeaveWeapon();
                            Knife knife = new Knife(15);
                            player.FoundWeapon("nuz");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (weapon == 4)
            {
                Console.WriteLine("Na ceste lezi granat");
                if (player.GetWeaponNumber() == 0)
                {
                    Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            Grenade grenade = new Grenade(100);
                            player.FoundWeapon("granat");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Jednu zbran uz mate. Muzete mit pouze 1 zbran");
                    Console.WriteLine("Stisknete 'v' pro vzit a zahodit stavajici nebo 'n' pro nebrat a nechat stavajici");
                    string takeLeave = Console.ReadLine();
                    while (takeLeave != "n" && takeLeave != "v")
                    {
                        Console.WriteLine("Stisknete 'v' nebo 'n'");
                        takeLeave = Console.ReadLine();
                    }
                    switch (takeLeave)
                    {
                        case "v":
                            player.LeaveWeapon();
                            Grenade grenade = new Grenade(100);
                            player.FoundWeapon("granat");
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        static void FoundFood(Player player)
        {
            Console.WriteLine("Nasli jste jidlo!");
            Console.WriteLine("Stav vaseho avatara: \nhlad: " + player.GetHunger() + "\npenize: " + player.GetMoney());
            Console.WriteLine("Jidlo stoji 10");
            if (player.GetMoney() < 10)
            {
                Console.WriteLine("Nemate dost penez a jidlo si koupit nemuzete");
            }
            else
            {
                Console.WriteLine("Stisknete 'k' pro koupit nebo 'n' pro nekupovat");
                string takeLeave = Console.ReadLine();
                while (takeLeave != "n" && takeLeave != "k")
                {
                    Console.WriteLine("Stisknete 'k' nebo 'n'");
                    takeLeave = Console.ReadLine();
                }
                switch (takeLeave)
                {
                    case "k":
                        player.Buying(10);
                        player.Feeding();
                        break;
                    case "n":
                        break;
                    default:
                        break;
                }
            }
        }
        static void FoundMoney(Player player)
        {
            Console.WriteLine("Nasli jste penize!");
            Console.WriteLine("Stisknete 'v' pro vzit nebo 'n' pro nebrat");
            string takeLeave = Console.ReadLine();
            while (takeLeave != "n" && takeLeave != "v")
            {
                Console.WriteLine("Stisknete 'v' nebo 'n'");
                takeLeave = Console.ReadLine();
            }
            switch (takeLeave)
            {
                case "v":
                    player.FoundMoney();
                    break;
                case "n":
                    break;
                default:
                    break;
            }
        }
        static void Exit (Player player)
        {
            Console.WriteLine("Na tomto rozcesti je exit. \nPokud chcete hru opustit, stisknete 'e', pokud chcete pokracovat, stisknete 'p'.");
            string stayLeave = Console.ReadLine();
            while (stayLeave != "p" && stayLeave != "e")
            {
                Console.WriteLine("Stisknete 'e' nebo 'p'");
                stayLeave = Console.ReadLine();
            }
            switch (stayLeave)
            {
                case "e":
                    Console.WriteLine("VYHRALI JSTE! \nUspesne jste ukoncili hru a vyvedli sveho avatara z nebezpecne zeme.");
                    break;
                case "p":
                    break;
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            Player player = new Player(100, 0, 0, 0, 0);
            Console.WriteLine("Vitejte!");
            Console.WriteLine();
            Console.WriteLine("Ocitli jste se na pude nehostinne zeme, kterou se jeste nepodarilo prozkoumat a civilizovat.\n" +
                "Vsude kolem vas je dzungle, jedine, co vidite je husty prales, kterym ovsem vede vyslapana cesticka.\n" +
                "Vydejte se po ni a s trochou stesti vas vyvede ven, pryc z teto zeme.\n" +
                "Musite byt obezretni, cestou vas muze sezrat vlk nebo skalpovat nektery domorodec.\n" +
                "Tak tedy stastnou cestu!");
            Console.WriteLine();
            Console.WriteLine("Stav vaseho avatara: \nzdravi: " + player.GetHealth() + "\npenize: "
                + player.GetMoney() + "\nhlad: " + player.GetHunger()
                + "\nzbran: " + "\nvozidlo: ");
            Console.WriteLine();
            Console.WriteLine("Pro start cesty stisknete 's'");
            string start = Console.ReadLine();
            while (start != "s")
            {
                Console.WriteLine("Pro start cesty stisknete 's'");
                start = Console.ReadLine();
            }
            while (!player.IsDead())
            {
                //Console.Clear();
                player.Starvation();
                Crossroads();

                Random rng = new Random();
                int next = 0;
                int allValues = 0;
                for (int i = 0; i < 7; i++)
                {
                    allValues += Global.optionValue[i];
                }
                int randomThrow = rng.Next(1, allValues);
                allValues = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (randomThrow > allValues && randomThrow < allValues + Global.optionValue[i])
                        next = i;
                    allValues += Global.optionValue[i];
                }
                switch (next)
                {
                    case 1:
                        FoundEnemy(player);
                        break;
                    case 2:
                        FoundFood(player);
                        break;
                    case 3:
                        FoundMoney(player);
                        break;
                    case 4:
                        FoundVehicle();
                        break;
                    case 5:
                        FoundWeapon(player);
                        break;
                    case 6:
                        Console.WriteLine("Na tomto rozcesti vas nic neceka. Pokracujte dal");
                        break;
                    case 7:
                        Exit(player);
                        break;
                    default:
                        break;
                }
                Global.optionValue[6]++;
                player.IsDead();
            }
            Console.ReadKey();
        }
    }

}
