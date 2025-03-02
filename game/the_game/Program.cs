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
            public static int roundCount = 0; //pocet kol (pro zajimavost)
            public static int[] optionValue = { 30, 70, 80, 40, 50, 25, 2, 30 }; //1. nebezpeci, 2. jidlo, 3. penize, 4. doprava, 5. zbran, 6. nic, 7. exit, 8. munice
            //hodnota jednotlivych moznosti, co potka hrac na rozcesti -> z toho potom pravdepodobnost vyskytu
            public static bool isWinner = false;
        }
        static void Print(Player player) //tiskne stav avatara, hracovy postavicky
        {
            Console.WriteLine("Stav vaseho avatara: \nzdravi: " + player.GetHealth() + "\npenize: "
                + player.GetMoney() + "\nhlad: " + player.GetHunger()
                + "\nzbran: " + player.GetWeapon() + "\nvozidlo: " + player.GetVehicle());
        }
        static void Crossroads(Player player) //jednotliva rozcesti - hrac se rozhoduje, kudy chce jit
        {
            if (Global.roundCount % 2 == 0 && Global.roundCount != 0)
            { player.Starving(); } //jednou za 2 kola se zvetsi hlad o 5
            Print(player);
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
            if (whereNext == "p")
            {
                Console.WriteLine("Zahybate tedy doprava");
            }
            else if (whereNext == "l")
            {
                Console.WriteLine("Zahybate tedy doleva");
            }
            else
            {
                Console.WriteLine("Pokracujete tedy rovne");
            }
            Global.roundCount++; //pricita se kolo = 1 krizovatka, 1 kolo
            if (player.GetHealth() != 100)
            {
                player.Healing(); //pokud je hrac zraneny, cestou se postupne uzdravuje
            }

        }
        static void FoundEnemy(Player player, Gun gun, MachineGun machineGun) //potkava nebezpeci
        {
            Console.WriteLine("Pozor! Tamhle v krovi se neco pohnulo!");
            Random rng = new Random();
            int enemy = rng.Next(1, 3); //nahodne bud vlka nebo domorodce
            if (enemy == 1)
            {
                Wolf wolf = new Wolf(50, 5);
                Console.WriteLine("Vlk!");
                Print(player);
                while (!player.IsDead() && !wolf.IsDead()) //dokud 1 z nich nezemre, tak bojuji
                {
                    Fighting(wolf, player, gun, machineGun);
                }
            }
            else if (enemy == 2) //stejne tak u domorodce
            {
                int weapon = rng.Next(1, 5);
                string weaponType; //muze mit ruzne zbrane, ktere maji jinou uroven damage, kterou muzou hraci zpusobit
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
                    Fighting(native, player, gun, machineGun);
                }
            }
        }

        static void Fighting(Dangers enemy, Player player, Gun gun, MachineGun machineGun)
        {
            Random hitOrMiss = new Random();
            int playerHit = hitOrMiss.Next(1, 3); //nahodne se vybira, jestli hrac a nepritel zasahnou/utoku druheho se vyhnou
            if (playerHit == 1)
            { player.Hurt(enemy.GetDamage()); }
            else
            {
                Console.WriteLine("Odrazili jste  utok.");
            }
            Console.WriteLine("Jste na rade. Zautocte. \nStisknete 'u' pro utok.");
            if (player.GetWeapon() == "puska")
            {
                Console.WriteLine("Pocet naboju: " + gun.GetBullets());
            }
            else if (player.GetWeapon() == "samopal")
            {
                Console.WriteLine("Pocet naboju: " + machineGun.GetBullets());
            }

            string attac = Console.ReadLine();
            while (attac != "u")
            {
                Console.WriteLine("Stisknete 'u'"); //pokud hrac stiskne u, utoci
                attac = Console.ReadLine();
            }
            if (!player.IsDead())
            {
                Random hitOrMissEnemy = new Random();
                int enemyHit = hitOrMiss.Next(1, 3);
                if (enemyHit == 1)
                {
                    enemy.Hurt(player.GetDamage());
                    if (player.GetWeapon() == "granat") //pokud hrac pouzije granat, ztraci ho, nemuze pouzit po 2.
                    {
                        player.SetWeapon("");
                        player.SetDamage(10);
                    }
                    if (player.GetWeapon() == "puska") //pokud ma hrac pusku, odecitaji se naboje, kdyz dojdou, snizi se damage, kterou hrac pusobi nepriteli
                    {
                        gun.Firing();
                        if (gun.GetBullets() == 0)
                        {
                            Console.WriteLine("Nemate dostatek naboju.");
                            player.SetDamage(10);
                        }
                    }
                    else if (player.GetWeapon() == "samopal") //to same u samopalu
                    {
                        machineGun.Firing();
                        if (machineGun.GetBullets() == 0)
                        {
                            Console.WriteLine("Nemate dostatek naboju.");
                            player.SetDamage(10);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Netrefili jste se!");
                    if (player.GetWeapon() == "granat") //pokud hrac pouzije granat, ztraci ho, nemuze pouzit po 2.
                    {
                        player.SetWeapon("");
                        player.SetDamage(10);
                    }
                    if (player.GetWeapon() == "puska") //pokud ma hrac pusku, odecitaji se naboje, kdyz dojdou, snizi se damage, kterou hrac pusobi nepriteli
                    {
                        gun.Firing();
                        if (gun.GetBullets() == 0)
                        {
                            Console.WriteLine("Nemate dostatek naboju.");
                            player.SetDamage(10);
                        }
                    }
                    else if (player.GetWeapon() == "samopal") //to same u samopalu
                    {
                        machineGun.Firing();
                        if (machineGun.GetBullets() == 0)
                        {
                            Console.WriteLine("Nemate dostatek naboju.");
                            player.SetDamage(10);
                        }
                    }
                }
            }
        }
        static void FoundVehicle(Player player)
        {
            Random rng = new Random();
            int vehicle = rng.Next(1, 4); //stejny princip, nahodne vybrani prostredku
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
                        int currentGasAmount = car.GetGas();
                        player.SetVehicle("auto");
                        for (int i = 0; i < 4; i++) //kazdy dopr. prostr. jede urcity pocet rozcesti/kol, potom mu dojde palivo
                        {
                            Crossroads(player);
                            currentGasAmount -= 25;
                            Console.WriteLine("Autu dochazi benzin. Stav benzinu: " + currentGasAmount);
                        }
                        Console.WriteLine("Autu dosel benzin. Dal uz musite po svych.");
                        player.SetVehicle("");
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
                        player.SetVehicle("motorka");
                        for (int i = 0; i < 2; i++)
                        {
                            Crossroads(player);
                            currentGasAmount -= 50;
                            Console.WriteLine("Motorce dochazi benzin. Stav benzinu: " + currentGasAmount);
                        }
                        Console.WriteLine("Motorce dosel benzin. Dal uz musite po svych.");
                        player.SetVehicle("");
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
                        player.SetVehicle("letadlo");
                        for (int i = 0; i < 5; i++)
                        {
                            Crossroads(player);
                            currentGasAmount -= 20;
                            Console.WriteLine("Letadlu dochazi palivo. Stav paliva: " + currentGasAmount);
                        }
                        Console.WriteLine("Letadlu doslo palivo a museli jste nouzove pristat. Dal uz musite po svych.");
                        player.SetVehicle("");
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
            int weapon = rng.Next(1, 5); //stejny princip, pokud hrac nejde zbran, muze si ji vzit
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
                            //Gun gun = new Gun(5, 20);
                            player.FoundWeapon("puska");
                            player.SetDamage(20);
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Uz mate " + player.GetWeapon() + ". Muzete mit pouze 1 zbran"); //nemuze mit ale 2 zbrane
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
                            //Gun gun = new Gun(5, 20);
                            player.FoundWeapon("puska");
                            player.SetDamage(20);
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
                            //MachineGun machinegun = new MachineGun(5, 50);
                            player.FoundWeapon("samopal");
                            player.SetDamage(50);
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
                            //Gun gun = new Gun(5, 50);
                            player.FoundWeapon("samopal");
                            player.SetDamage(50);
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
                            //Knife knife = new Knife(15);
                            player.FoundWeapon("nuz");
                            player.SetDamage(15);
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
                            //Knife knife = new Knife(15);
                            player.FoundWeapon("nuz");
                            player.SetDamage(15);
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
                            //Grenade grenade = new Grenade(100);
                            player.FoundWeapon("granat");
                            player.SetDamage(100);
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
                            //Grenade grenade = new Grenade(100);
                            player.FoundWeapon("granat");
                            player.SetDamage(100);
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static void FoundAmmunition(Player player, Gun gun, MachineGun machineGun)
        {
            Console.WriteLine("Nasli jste naboje!");
            if (player.GetWeapon() == "puska")
            {
                Console.WriteLine("Stav vaseho avatara: \nzbran: " + player.GetWeapon() + "\nnaboje: " + gun.GetBullets());
            }
            else if (player.GetWeapon() == "samopal")
            {
                Console.WriteLine("Stav vaseho avatara: \nzbran: " + player.GetWeapon() + "\nnaboje: " + machineGun.GetBullets());
            }
            if (player.GetWeapon() == "puska" || player.GetWeapon() == "samopal")
            {
                Console.WriteLine("Sada 10 naboju stoji 15");
                if (player.GetMoney() < 15)
                {
                    Console.WriteLine("Nemate dost penez a naboje si koupit nemuzete");
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
                            player.Buying(15);
                            if (player.GetWeapon() == "puska")
                            {
                                gun.Loading();
                            }
                            else if (player.GetWeapon() == "samopal")
                            {
                                machineGun.Loading();
                            }
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Momentalne nevlastnite strelnou zbran a naboje nemate kam dat, takze musite pokracovat.");
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
        static void Exit(Player player)
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
                    Console.WriteLine("VYHRALI JSTE! \nPocet projitych rozcesti: " + Global.roundCount + "\nUspesne jste ukoncili hru a vyvedli sveho avatara z nebezpecne zeme.");
                    Global.isWinner = true;
                    break;
                case "p":
                    break;
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            Player player = new Player(100, 0, 0, 0, 0, 10);
            Grenade grenade = new Grenade(100);
            Knife knife = new Knife(15);
            Gun gun = new Gun(5, 20);
            MachineGun machineGun = new MachineGun(5, 50);


            Console.WriteLine("Vitejte!");
            Console.WriteLine();
            Console.WriteLine("Ocitli jste se na pude nehostinne zeme, kterou se jeste nepodarilo prozkoumat a civilizovat.\n" +
                "Vsude kolem vas je dzungle, jedine, co vidite je husty prales, kterym ovsem vede vyslapana cesticka.\n" +
                "Vydejte se po ni a s trochou stesti vas vyvede ven, pryc z teto zeme.\n" +
                "Musite byt obezretni, cestou vas muze sezrat vlk nebo skalpovat nektery domorodec.\n" +
                "Take muzete vyhladovet, pokud hlad presahne uroven 50 bodu." +
                "\nTak tedy stastnou cestu!");
            Console.WriteLine();
            Console.WriteLine("Pro start cesty stisknete 's'");
            string start = Console.ReadLine();
            while (start != "s")
            {
                Console.WriteLine("Pro start cesty stisknete 's'");
                start = Console.ReadLine();
            }
            while (!player.IsDead() && !Global.isWinner) //dokud hrac nezemre nebo neopusti hru exitem (vyhraje), opakuje se sada moznosti, co hrac najde na rozcesti
            {
                Crossroads(player);

                Random rng = new Random();
                int next = 0;
                int allValues = 0;
                for (int i = 0; i < 8; i++) //sectou se vsechny hodnoty moznosti (definovane na zacatku)
                {
                    allValues += Global.optionValue[i];
                }
                int randomThrow = rng.Next(1, allValues); //vygeneruje se nahodne cislo
                allValues = 0;
                for (int i = 0; i < 8; i++) //pokud je vygenerovane cislo v intervalu nektere moznosti, je tato moznost vybrana jako dalsi
                {
                    if (randomThrow > allValues && randomThrow < allValues + Global.optionValue[i])
                        next = i;
                    allValues += Global.optionValue[i];
                }
                switch (next)
                {
                    case 0:
                        FoundEnemy(player, gun, machineGun);
                        break;
                    case 1:
                        FoundFood(player);
                        break;
                    case 2:
                        FoundMoney(player);
                        break;
                    case 3:
                        FoundVehicle(player);
                        break;
                    case 4:
                        FoundWeapon(player);
                        break;
                    case 5:
                        Console.WriteLine("Na tomto rozcesti vas nic neceka. Pokracujte dal");
                        break;
                    case 6:
                        Exit(player);
                        break;
                    case 7:
                        FoundAmmunition(player, gun, machineGun);
                        break;
                    default:
                        break;
                }
                Global.optionValue[6] += 2; //s kazdym kolem se zvysuje pravdepodobnost, ze se objevi exit
            }
            Console.ReadKey();
        }
    }

}
