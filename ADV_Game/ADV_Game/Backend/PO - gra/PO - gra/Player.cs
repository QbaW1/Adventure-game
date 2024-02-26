using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POProjekt
{
    public class Player 
    {
        public string nickname;
        public int hp;
        public int dmg;
        public int def;
        public decimal eq_limit; 
        public HeroType playerClass;
        public int key_count;
        public Equipment bagpack = new Equipment();

        public string Nickname { get => nickname; set => nickname = value; }
        public int Hp {get => hp; set => hp = value; }
        public int Dmg { get => dmg; set => dmg = value; }
        public decimal Eq_limit { get => eq_limit; set => eq_limit = value; }
        public HeroType PlayerClass { get => playerClass; set => playerClass = value; }
        public int Key_count { get => key_count; set => key_count = value; }
        public int Def { get => def; set => def = value; }
        

        public Player()
        {
            this.nickname = string.Empty;
            this.def = 0;
            this.key_count = 0;
            this.hp = 0;
            this.dmg = 0;
            this.eq_limit = 0;
            this.playerClass = HeroType.Warrior;
        }

        public Player(string player_name, HeroType character) : this() 
        {
            this.nickname = player_name;
            switch (character) 
            {
                case HeroType.Warrior:
                    this.hp = 150;
                    this.dmg = 10;
                    this.eq_limit = 100;
                    this.playerClass = character;
                    Console.WriteLine("Witaj potężny wojowniku!");
                    break;
                case HeroType.Rogue:
                    this.hp = 90;
                    this.dmg = 15;
                    this.eq_limit = 70;
                    this.playerClass = character;
                    Console.WriteLine("Witaj przebiegły łotrze!");
                    break;
                case HeroType.Mage:
                    this.hp = 70;
                    this.dmg = 22;
                    this.eq_limit = 50;
                    this.playerClass = character;
                    Console.WriteLine("Witaj sędziwy magu!");
                    break;
            }
        }

        public Player character_creation()
        {
            Console.WriteLine("Budzisz się z okropnym bólem głowy." +
            "\nRozglądasz się i dociera do ciebie, że nie wiesz gdzie jesteś." +
            "\nMyślisz, starasz się sobie przypomnieć cokolwiek.\nJak się nazywasz?"); 

            string player_name = null;
            bool valid_input = false;

            do
            {
                try
                {
                    player_name = Console.ReadLine();
                    string pattern = @"^[A-Za-z]+$";
                    if (Regex.IsMatch(player_name, pattern) && player_name.Length < 12)
                    {
                        Console.WriteLine("Witaj " + player_name + "!\nJesteś sobie wstanie przypomnieć kim byłeś?\n1 - Wojownikiem?\n2 - Łotrem?\n3 - Magiem?");
                        valid_input = true;
                    }
                    else
                    {
                        throw new Exception("Niepoprawna nazwa gracza!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd: " + ex.Message);
                }
            } while (!valid_input);

            valid_input = false;
            int intTemp = 0;

            do
            {
                try
                {
                    intTemp = Convert.ToInt32(Console.ReadLine());
                    if (intTemp != 1 && intTemp != 2 && intTemp != 3)
                    {
                        throw new Exception("Wybrano niepoprawny numer!");
                    }
                    else
                    {
                        Player new_player = new Player(player_name, (HeroType)intTemp);
                        valid_input = true;
                        Console.WriteLine($"\nTwoja postać: {new_player.PlayerClass}\nhp: {new_player.Hp}\ndmg: {new_player.Dmg}\npojemność eq [kg]: {new_player.Eq_limit}\n");
                        return new_player; 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd: " + ex.Message); 
                }
            } while (!valid_input);

            return null; // Jeśli coś pójdzie nie tak, zwracamy null
        }

        public int HeroAttack(int dmg)
        {
            Random r = new();
            int attack = r.Next((int)(0.88 * dmg), (int)(1.12 * dmg));
            return attack;
        }

        public void WeightTest() 
        {
            if (bagpack.bagpack_weight > Eq_limit)
            {
                Console.WriteLine("Twój plecak zbyt ciężki!\nPrzed wyruszeniem w drogę musisz wyrzucić jakiś przedmiot.\n");
                Console.WriteLine("\n*Wciśnij dowolny przycisk aby kontynuować*\n"); Console.ReadKey(); Console.Clear();
                bagpack.BackpackContains();
                Console.WriteLine("\nKtóry przedmiot chcesz usunąć?");

                int index = 0;
                if (!int.TryParse(Console.ReadLine(), out index)) { Console.WriteLine("Musisz podać liczbę całkowitą z listy!"); }
                index -= 1;
                Item lostItem = bagpack.RemoveItemAt(index);
                HeroLostEq(lostItem);
                WeightTest();
            }
        }

        public void HeroBuffEq(Item item) 
        {
                switch (item.Type)
                {
                    case ItemType.Armor:
                        this.def += 40;
                     break;
                    case ItemType.Dagger:
                        if (playerClass == HeroType.Rogue)
                        {
                            this.dmg += 30;
                        }
                        else { this.dmg += 10; }
                        break;
                    case ItemType.MagicScepter:
                        if (playerClass == HeroType.Mage)
                        {
                            this.dmg += 55;
                        }
                        else { this.dmg += 15; }
                        break;
                    case ItemType.Sword:
                        if (playerClass == HeroType.Warrior)
                        {
                            this.dmg += 50;
                        }
                        else { this.dmg += 20; }
                        break;
                    case ItemType.HealthStone:
                        this.Hp += 50;
                        break;
                }
        }
        public void HeroLostEq(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Armor:
                    this.def -= 40;
                    break;
                case ItemType.Dagger:
                    if (playerClass == HeroType.Rogue)
                    {
                        this.dmg -= 30;
                    }
                    else { this.dmg -= 10; }
                    break;
                case ItemType.MagicScepter:
                    if (playerClass == HeroType.Mage)
                    {
                        this.dmg -= 55;
                    }
                    else { this.dmg -= 15; }
                    break;
                case ItemType.Sword:
                    if (playerClass == HeroType.Warrior)
                    {
                        this.dmg -= 50;
                    }
                    else { this.dmg -= 20; }
                    break;
                case ItemType.HealthStone:
                    this.Hp -= 50;
                    break;
            }
        }

        public void ZapisJson(string nazwaPliku)
        {
                string jsonstr = JsonSerializer.Serialize(this, typeof(Player));
                File.WriteAllText(nazwaPliku, jsonstr);
        }

        public static Player OdczytJson(string nazwaPliku)
        {
                string jsonstr = File.ReadAllText(nazwaPliku);
                return JsonSerializer.Deserialize<Player>(jsonstr);  
        }
    }

    public enum HeroType 
    {
        Warrior = 1,
        Rogue = 2,
        Mage = 3
    }
}
