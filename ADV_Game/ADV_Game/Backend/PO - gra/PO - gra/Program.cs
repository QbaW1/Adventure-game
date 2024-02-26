using System;
using System.Collections.Concurrent;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using PO___gra;
using POProjekt;


public class Program
{
    public static void Main(string[] args)
    {
        Start();
     
    }
    
 

    public static int valley_counter = 1;
    public static int port_counter = 1;
    public static int graveyard_counter = 1;

    public static void Start() 
    {
        Console.WriteLine("Wybierz poziom trudności");
        Console.WriteLine("1 - Łatwy");
        Console.WriteLine("2 - Trudny");
        Console.WriteLine("3 - Mistrz");

        int difficulty_level = 0;
        Difficulty enum_difficulty_level = Difficulty.easy;

        bool valid_input_difficulty = false;
        do 
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out difficulty_level))
                {
                    Console.WriteLine("Musisz podać liczbę od 1-3!");
                }

                if (difficulty_level != 1 && difficulty_level != 2 && difficulty_level != 3)
                {
                    throw new Exception("Niepoprawna liczba!");
                }

                switch (difficulty_level)
                {
                    case 1:
                        enum_difficulty_level = Difficulty.easy;
                        valid_input_difficulty = true;
                        break;
                    case 2:
                        enum_difficulty_level = Difficulty.hard;
                        valid_input_difficulty = true;
                        break;
                    case 3:
                        enum_difficulty_level = Difficulty.master;
                        valid_input_difficulty = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
            }
        } while (!valid_input_difficulty);
        Console.Clear();

        Player player = new Player();
        Player new_player = player.character_creation();
        if(new_player == null) 
        { 
            Console.WriteLine("Coś poszło nie tak przy tworzeniu postaci!"); 
            return;
        }


        Console.ReadKey();
        Console.Clear();


        Localization currentLocalization = new StartingLocation();
        
        
        static Localization ChooseNewLocalization(Localization currentLocalization) //Pozwala nam przejść do nowej lokalizacji, przesłaniamy do tego zmienną obecnaLokalizacja
        {
            Console.WriteLine("Wybierz nową lokalizację:");
            Console.WriteLine("1 - Dolina Magów [Zagrożenie - niskie]");
            Console.WriteLine("2 - Port [Zagrożenie - średnie]");
            Console.WriteLine("3 - Cmentarz [Zagrożenie - wysokie]");
            Console.WriteLine("4 - Wrota [???]");

            int choice = 0;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Wyruszyłeś do Doliny Magów!");
                        return new Valley();
                    case 2:
                        Console.WriteLine("Wyruszyłeś do Portu!");
                        return new Port();
                    case 3:
                        Console.WriteLine("Wyruszyłeś w kierunku cmentarza..");
                        return new Graveyard();
                    case 4:
                        Console.WriteLine("Wyruszyłeś w kiernku gigantycznych wrót..");
                        return new Gates();
                    default:
                        Console.WriteLine("Niepoprawny wybór. Pozostajesz w obecnej lokalizacji.");
                        return currentLocalization;
                }
            }
            else
            {
                Console.WriteLine("Niepoprawny wybór. Pozostajesz w obecnej lokalizacji.");
                return currentLocalization;
            }

        }

        bool winCondition = true;
        while (winCondition)
        {

            Console.WriteLine($"Twoja loklizacja: {currentLocalization.Localization_name}\nCo chciałbyś zrobić?\n");
            Console.WriteLine("1 - Rozglądnij się\n");
            Console.WriteLine("2 - Szukaj\n");
            Console.WriteLine("3 - Idź dalej\n");
            Console.WriteLine("4 - Szukaj guza\n");
            Console.WriteLine("5 - Sprawdź plecak\n");
            Console.WriteLine("6 - Sprawdź staty\n");
            Console.WriteLine("7 - Zapisz Postać\n");
            Console.WriteLine("8 - Wczytaj Postać\n");

            int intTemp = 0;
            try
            {
                if (!int.TryParse(Console.ReadLine(), out intTemp)) { throw new MyError("Możesz tylko podać liczby całkowite!\n", "Musisz podać liczbę od 1-8!\n"); }

                if (intTemp > 8 || intTemp < 1)
                {
                    throw new MyError("Wprowadź liczbę całkowitą!", "Musisz podać liczbę od 1-8!");
                }
            }
            catch (MyError ex)
            {
                Console.WriteLine("Błąd: " + ex.ExtraInfo);
                Console.WriteLine("Błąd: " + ex.Message);
            }
            string save_name = string.Empty;

            switch (intTemp) 
            {
                case 1:
                    currentLocalization.LookAround();
                    break;
                case 2:
                    if (currentLocalization.amount_of_items != 0 && currentLocalization.item != null)
                    {
                        Console.WriteLine("Znalazłeś: " + currentLocalization.item.type);
                        new_player.HeroBuffEq(currentLocalization.item);
                        currentLocalization.TakeItem(currentLocalization.item,new_player.bagpack);
                    }
                    else { Console.WriteLine("Nic nie znalazłeś."); }
                    break;
                case 3:
                    new_player.WeightTest();
                    currentLocalization = ChooseNewLocalization(currentLocalization);
                    break;
                case 4:
                    if (currentLocalization is StartingLocation)
                    {
                        Console.WriteLine("Jesteś na beztroskeij polanie.\nNic ci tutaj nie grozi.\n");
                    }
                    else
                    {
                        Encounter(new_player, currentLocalization, enum_difficulty_level, ref valley_counter, ref port_counter, ref graveyard_counter);
                    }
                    break;
                case 5:
                    new_player.bagpack.BackpackContains();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine($"Nazwa gracza: {new_player.Nickname}\nLimit ekwipunku: {new_player.Eq_limit}\nDMG: {new_player.Dmg}\nDEF: {new_player.Def}\nHP: {new_player.Hp}");
                    break;
                case 7:
                    save_name = new_player.Nickname;
                    save_name = save_name + ".json";
                    new_player.ZapisJson(save_name);
                    break;
                case 8:
                    Console.WriteLine("Podaj nazwe postaci, którą chcesz wczytać:\n");
                    save_name = Console.ReadLine();
                    Console.WriteLine("Wczytywanie postaci..\n");
                    save_name += ".json";
                    new_player = Player.OdczytJson(save_name) ?? new_player;
                    break;
            }
            if (currentLocalization is Gates) 
            {
                Gates end = new Gates();
                winCondition = end.OpenTheGates(new_player);
                
            }

            Console.WriteLine("\n*Wciśnij dowolny przycisk aby kontynuować*\n"); Console.ReadKey(); Console.Clear();
        }
        Console.WriteLine("Wkładasz wszystkie trzy znalezione klucze.\nNagle słyszysz głośne *klik* i wrota zaczynają się rozsuwać.\n");
        Thread.Sleep(3000);
        Console.WriteLine("Przed tobą rozpościera się tylko ciemność.\n");
        Thread.Sleep(1000);
        Console.WriteLine("Postanawiasz przekroczyc próg.\nIdziesz..idziesz..idziesz.\nMasz wrażenie, że upłynęły godziny.\nPrawda jest taka, że już dawno temu straciłeś poczucie czasu.\n");
        Thread.Sleep(3000);
        Console.WriteLine("Kiedy zaczynasz myśleć, że popełniłeś błąd nagle dostrzegasz odległe światło.\nBiegniesz w jego kiernku.\nPrzed tobą otwierają się kolejne drzwi, przez które przeskakujesz z impetem.\n");
        Thread.Sleep(5000);
        Console.WriteLine("Nagle budzisz się w swoim łóżku.\nTo był tylko sen.\n");
        Console.WriteLine($"Gratulacje {new_player.Nickname}, wygrałeś!");
    }

    public static bool Fight(Player Character1, Enemy Enemy1)
    {
        int Character_hp = Character1.Hp;
        int Enemy_hp = Enemy1.Hp;
        
        while (Character_hp > 0 && Enemy_hp > 0)
        {
            Console.WriteLine("Co chcesz zrobić");
            Console.WriteLine("1 - Atakuj");
            Console.WriteLine("2 - Ulecz się");
            Console.WriteLine("3 - Uciekaj");

            int choice = 0;
            try
            {
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Musisz podać liczbę od 1-3!");
                }

                if (choice != 1 && choice != 2 && choice != 3)
                {
                    throw new Exception("Niepoprawna liczba!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
                continue;
            }

            switch (choice)
            {
                case 1:
                    int Character_att = Character1.HeroAttack(Character1.Dmg);
                    Enemy_hp -= (1 - (Enemy1.Def / 100)) * Character_att;
                    Console.WriteLine($"Przeciwnikowi zostało {Enemy_hp} życia\n");
                    if (Enemy_hp <= 0)
                    {
                        if (Enemy1.EnemyItem is not null)
                        {
                            Console.WriteLine($"Zdobyłeś {Enemy1.EnemyItem.type}\n");
                            Character1.bagpack.AddItem(Enemy1.EnemyItem);
                        }
                        Console.WriteLine("Wygrałeś\n");
                        return true;
                    }
                    break;
                case 2:
                    Item healthPotion = Character1.bagpack.FindItem(item => item.Type == ItemType.HealthPotion);
                    if (healthPotion != null)
                    {
                        Character_hp = Character1.Hp;
                        Character1.bagpack.RemoveItem(healthPotion);
                        Console.WriteLine($"Wróciłeś do pełni zdrowia!\nTwoje HP: {Character1.Hp}\n");
                    }
                    else { Console.WriteLine("W twoim ekwipunku nie ma mikstur zdrowia!\n"); }
                        break;
                case 3:
                    Console.WriteLine("Uciekasz...\n");
                    return false;
            }

            Console.WriteLine($"{Enemy1.Name} atakuje!\n");
            int Enemy_att = Enemy1.EnemyAttack(Enemy1.Dmg);
            Character_hp -= (1 - (Character1.Def / 100)) * Enemy_att;
            Console.WriteLine($"Zostało ci {Character_hp} życia\n");
            if (Character_hp <= 0)
            {
                Console.WriteLine("Przegrałeś\n");
                return false;
            }
        }

        return false;
    }

    public static void Encounter(Player player, Localization currentLocalization, Difficulty enum_difficulty_level, ref int valley_counter, ref int port_counter, ref int graveyard_counter)
    {
        Enemy v1 = new("Zając", 41, 51, 5, enum_difficulty_level);
        Enemy v2 = new("Wilk", 47, 31, 7, enum_difficulty_level);
        Enemy v3 = new("Skrzat", 51, 31, 11, enum_difficulty_level, new Item(ItemType.HealthPotion));
        Boss v4 = new("Leśny golem", 57, 37, 13, enum_difficulty_level, new Item(ItemType.Key));

        Enemy p1 = new("Wsciekła mewa", 57, 41, 17, enum_difficulty_level);
        Enemy p2 = new("Pirat", 61, 47, 19, enum_difficulty_level, new Item(ItemType.HealthPotion));
        Enemy p3 = p2.Clone() as Enemy;
        p3.ChangeName("Korsarz");
        Boss p4 = new("Kraken", 67, 53, 23, enum_difficulty_level, new Item(ItemType.Key));

        Enemy g1 = new("Szkielet", 67, 59, 29, enum_difficulty_level, new Item(ItemType.HealthPotion));
        Enemy g2 = new("Zombie", 71, 61, 29, enum_difficulty_level);
        Enemy g3 = new("Duch złodzieja", 89, 67, 31, enum_difficulty_level);
        Boss g4 = new("Nekromanta", 101, 73, 43, enum_difficulty_level, new Item(ItemType.Key));

        bool won = false;
        Console.Clear();
        if (currentLocalization is Valley)
        {
            if (valley_counter == 1) { Console.WriteLine($"Walczysz z {v1.Name}\n"); won = Fight(player, v1); valley_counter += won ? 1 : 0; return; }
            if (valley_counter == 2) { Console.WriteLine($"Walczysz z {v2.Name}\n"); won = Fight(player, v2); valley_counter += won ? 1 : 0; return; }
            if (valley_counter == 3) { Console.WriteLine($"Walczysz z {v3.Name}\n"); won = Fight(player, v3); valley_counter += won ? 1 : 0; return; }
            if (valley_counter == 4)
            {
                Console.WriteLine($"Walczysz z {v4.Name}\n"); won = Fight(player, v4); valley_counter += won ? 1 : 0;
                player.Key_count += won ? 1 : 0; if (won == true) Console.WriteLine("Znalazłeś klucz!\n"); return;
            }
            if (valley_counter == 5) { Console.WriteLine("Pokonałeś już wszystkich przeciwników w tej lokalizacji\n"); return; }
        }
        if (currentLocalization is Port)
        {
            if (port_counter == 1) { Console.WriteLine($"Walczysz z {p1.Name}\n"); won = Fight(player, p1); port_counter += won ? 1 : 0; return; }
            if (port_counter == 2) { Console.WriteLine($"Walczysz z {p2.Name}\n"); won = Fight(player, p2); port_counter += won ? 1 : 0; return; }
            if (port_counter == 3) { Console.WriteLine($"Walczysz z {p3.Name}\n"); won = Fight(player, p3); port_counter += won ? 1 : 0; return; }
            if (port_counter == 4)
            {
                Console.WriteLine($"Walczysz z {p4.Name}\n"); won = Fight(player, p4); port_counter += won ? 1 : 0;
                player.Key_count += won ? 1 : 0; if (won == true) Console.WriteLine("Znalazłeś klucz!\n"); return;
            }
            if (port_counter == 5) { Console.WriteLine("Pokonałeś już wszystkich przeciwników w tej lokalizacji\n"); return; }
        }
        if (currentLocalization is Graveyard)
        {
            if (graveyard_counter == 1) { Console.WriteLine($"Walczysz z {g1.Name}\n"); won = Fight(player, g1); graveyard_counter += won ? 1 : 0; return; }
            if (graveyard_counter == 2) { Console.WriteLine($"Walczysz z {g2.Name}\n"); won = Fight(player, g2); graveyard_counter += won ? 1 : 0; return; }
            if (graveyard_counter == 3) { Console.WriteLine($"Walczysz z {g3.Name}\n"); won = Fight(player, g3); graveyard_counter += won ? 1 : 0; return; }
            if (graveyard_counter == 4)
            {
                Console.WriteLine($"Walczysz z {g4.Name}\n"); won = Fight(player, g4); graveyard_counter += won ? 1 : 0;
                player.Key_count += won ? 1 : 0; if (won == true) Console.WriteLine("Znalazłeś klucz!\n"); return;
            }
            if (graveyard_counter == 5) { Console.WriteLine("Pokonałeś już wszystkich przeciwników w tej lokalizacji\n"); return; }
        }
    }
}