using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using POProjekt;
using static System.Net.Mime.MediaTypeNames;

namespace GUI
{
    
    public partial class MainWindow : Window
    {
        Player player;

        POProjekt.Localization currentLocalization = new StartingLocation();

        public static int valley_counter = 1;
        public static int port_counter = 1;
        public static int graveyard_counter = 1;

        Difficulty difficulty;

        Enemy v1, v2, v3;
        Boss v4;

        Enemy p1, p2, p3;
        Boss p4;

        Enemy g1, g2, g3;



        private void Btn_save_click(object sender, RoutedEventArgs e) 
        {
            try
            {
                player.ZapisJson(player.Nickname + ".json");
                MessageBox.Show($"Pomyślnie zapisano postać do pliku JSON.\nNazwa zapisu to {player.Nickname}.json", "Sukces");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas zapisu do pliku JSON: " + ex.Message, "Błąd zapisu");
            }
        }

        private void Btn_load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string save_name = HeroNameBox.Text;
                save_name += ".json";

            
                player = Player.OdczytJson(save_name) ?? player;
                MessageBox.Show("Załadowano postać.", "Sukces");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas wczytywania postaci: " + ex.Message, "Błąd");
            }
        }

        private void Btn_Go(object sender, RoutedEventArgs e)
        {
            if (player == null)
            {
                MessageBox.Show("Najpierw stwórz postać!", "Brak postaci");
                return; 
            }
            if (player.bagpack.Bagpack_weight > player.Eq_limit) 
            {
                MessageBox.Show("Twój plecak jest zbyt ciężki aby kontynuować podróż!\nUsuń jakiś przedmiot z plecaka przed wyruszeniem w drogę.", "Zbyt ciężki plecak");
                PlayerEQWindow playerEQ = new PlayerEQWindow(player);
                playerEQ.Show();
                return; 
            }
            LocationSelectionWindow locationSelectionWindow = new LocationSelectionWindow();
           
                if (locationSelectionWindow.ShowDialog() == true)
                {
                    // Pobieranie wybranej lokalizacji
                    POProjekt.Localization selectedLocalization = locationSelectionWindow.SelectedLocalization;
                    if (currentLocalization.GetType() == selectedLocalization.GetType()) { MessageBox.Show("Zostałeś w tym samym miejscu!",""); return; }  
                    
                    // Aktualizacja bieżącej lokalizacji
                    currentLocalization = selectedLocalization;

                  
                    MessageBox.Show($"Zmieniono lokalizację na {selectedLocalization.Localization_name}.", "Zmiana lokalizacji");
                }
            
        }

        

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            if (player == null)
            {
                MessageBox.Show("Najpierw stwórz postać!", "Brak postaci");
                return;
            }
            if (currentLocalization.GetType() == typeof(StartingLocation)) { MessageBox.Show("Jesteś na spokojnej zielonej polanie.\nNie ma tutaj nic do znalezienia poza pasącymi się krowami."); return; }
            if (currentLocalization.amount_of_items == 0) { MessageBox.Show("W lokalizacji nie ma przedmiotów do znalezienia!", "Pusta lokalizacja"); }
            else 
            {
                MessageBox.Show($"Znaleziono {currentLocalization.item.Type}!\nPrzedmiot został dodany do ekwipunku.", "Znaleziono przedmiot");
                player.HeroBuffEq(currentLocalization.Item);
                currentLocalization.TakeItem(currentLocalization.Item, player.bagpack);
                
            }
            
            
        }

        Boss g4;


        private void BtnCheckStats_Click(object sender, RoutedEventArgs e)
        {
            if (player == null)
            {
                MessageBox.Show("Najpierw stwórz postać!", "Brak postaci");
                return;
            }
            MessageBox.Show($"Nazwa postaci: {player.Nickname}\nHP: {player.Hp}\n" +
                $"DMG: {player.Dmg}\nDEF: {player.Def}\nLimit eq: {player.Eq_limit}","Statystki postaci");
        }

        private void Btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCheckBagPack_Click(object sender, RoutedEventArgs e)
        {
            if (player != null && player.bagpack != null && player.bagpack.Bagpack.Count != 0)
            {
                PlayerEQWindow playerEQWindow = new PlayerEQWindow(player);
                playerEQWindow.Show();
            }
            else if (player != null && player.bagpack != null && player.bagpack.Bagpack.Count == 0) 
            {
                    MessageBox.Show("Twój plecak jest pusty!", "Pusty plecak");
                    return;
            }
            else
            {
                MessageBox.Show("Najpierw stwórz postać!", "Brak postaci");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            v1 = new Enemy("Zając", 41, 21, 5, difficulty);
            v2 = new Enemy("Wilk", 47, 31, 7, difficulty);
            v3 = new Enemy("Skrzat", 51, 31, 11, difficulty, new Item(ItemType.HealthPotion));
            v4 = new Boss("Leśny golem", 57, 37, 13, difficulty, new Item(ItemType.Key));

            p1 = new Enemy("Wsciekła mewa", 57, 41, 17, difficulty);
            p2 = new Enemy("Pirat", 61, 47, 19, difficulty, new Item(ItemType.HealthPotion));
            p3 = p2.Clone() as Enemy;
            p3.ChangeName("Korsarz");
            p4 = new Boss("Kraken", 67, 53, 23, difficulty, new Item(ItemType.Key));

            g1 = new Enemy("Szkielet", 67, 59, 29, difficulty, new Item(ItemType.HealthPotion));
            g2 = new Enemy("Zombie", 71, 61, 29, difficulty);
            g3 = new Enemy("Duch złodzieja", 89, 67, 31, difficulty);
            g4 = new Boss("Nekromanta", 101, 73, 43, difficulty, new Item(ItemType.Key));

        }

        private void BtnDifficulty_Click(object sender, RoutedEventArgs e)
        {
            DifficultyWindow difficultyWindow = new DifficultyWindow(difficulty);

            if (difficultyWindow.ShowDialog() == true)
            {
                difficulty = difficultyWindow.Difficulty;

                UpdateEnemyDifficulty(v1);
                UpdateEnemyDifficulty(v2);
                UpdateEnemyDifficulty(v3);
                UpdateEnemyDifficulty(v4);
                UpdateEnemyDifficulty(p1);
                UpdateEnemyDifficulty(p2);
                UpdateEnemyDifficulty(p3);
                UpdateEnemyDifficulty(p4);
                UpdateEnemyDifficulty(g1);
                UpdateEnemyDifficulty(g2);
                UpdateEnemyDifficulty(g3);
                UpdateEnemyDifficulty(g4);
            }
        }
        private void UpdateEnemyDifficulty(Enemy enemy)
        {
            if (enemy != null)
            {
                enemy.Difficulty = difficulty;
            }
        }

        private void BtnFight_Click(object sender, RoutedEventArgs e)
        {
            if(currentLocalization.GetType() == typeof(StartingLocation)) { MessageBox.Show("Jesteś w lokalizacji startowej.\nNic ci tutaj nie grozi :)");return; }
            if (player != null && player.Hp != 0)
            {
                bool won = false;
                if (currentLocalization is Valley)
                {
                    if (valley_counter == 1) { FightWindow fightWindow = new FightWindow(player, v1, won); fightWindow.ShowDialog(); won = fightWindow.Won; valley_counter += won ? 1 : 0; return; }
                    if (valley_counter == 2) { FightWindow fightWindow = new FightWindow(player, v2, won); fightWindow.ShowDialog(); won = fightWindow.Won; valley_counter += won ? 1 : 0; return; }
                    if (valley_counter == 3) { FightWindow fightWindow = new FightWindow(player, v3, won); fightWindow.ShowDialog(); won = fightWindow.Won; valley_counter += won ? 1 : 0; return; }
                    if (valley_counter == 4) { FightWindow fightWindow = new FightWindow(player, v4, won); fightWindow.ShowDialog(); won = fightWindow.Won; valley_counter += won ? 1 : 0; return; }
                    if (valley_counter == 5) { MessageBox.Show("Pokonałeś już wszystkich przeciwników w tej lokalizacji!", "Okolica bezpieczna");
                        player.key_count++;
                    }
                    if (player.key_count == 3) { MessageBox.Show("Gratulacje!\nPokonałeś wszystkich wrogów, a tym samym przeszedłeś grę!"); System.Windows.Application.Current.Shutdown(); }
                }
                if (currentLocalization is Port)
                {
                    if (port_counter == 1) { FightWindow fightWindow = new FightWindow(player, p1, won); fightWindow.ShowDialog(); won = fightWindow.Won; port_counter += won ? 1 : 0; }
                    if (port_counter == 2) { FightWindow fightWindow = new FightWindow(player, p2, won); fightWindow.ShowDialog(); won = fightWindow.Won; port_counter += won ? 1 : 0; }
                    if (port_counter == 3) { FightWindow fightWindow = new FightWindow(player, p3, won); fightWindow.ShowDialog(); won = fightWindow.Won; port_counter += won ? 1 : 0; }
                    if (port_counter == 4) { FightWindow fightWindow = new FightWindow(player, p4, won); fightWindow.ShowDialog(); won = fightWindow.Won; port_counter += won ? 1 : 0; }
                    if (port_counter == 5) { MessageBox.Show("Pokonałeś już wszystkich przeciwników w tej lokalizacji!", "Okolica bezpieczna");
                        player.key_count++;
                    }
                    if (player.key_count == 3) { MessageBox.Show("Gratulacje!\nPokonałeś wszystkich wrogów, a tym samym przeszedłeś grę!"); System.Windows.Application.Current.Shutdown(); }
                }
                if (currentLocalization is Graveyard)
                {
                    if (graveyard_counter == 1) { FightWindow fightWindow = new FightWindow(player, g1, won); fightWindow.ShowDialog(); won = fightWindow.Won; graveyard_counter += won ? 1 : 0; }
                    if (graveyard_counter == 2) { FightWindow fightWindow = new FightWindow(player, g2, won); fightWindow.ShowDialog(); won = fightWindow.Won; graveyard_counter += won ? 1 : 0; }
                    if (graveyard_counter == 3) { FightWindow fightWindow = new FightWindow(player, g3, won); fightWindow.ShowDialog(); won = fightWindow.Won; graveyard_counter += won ? 1 : 0; }
                    if (graveyard_counter == 4) { FightWindow fightWindow = new FightWindow(player, g4, won); fightWindow.ShowDialog(); won = fightWindow.Won; graveyard_counter += won ? 1 : 0; }
                    if (graveyard_counter == 5) { MessageBox.Show("Pokonałeś już wszystkich przeciwników w tej lokalizacji!", "Okolica bezpieczna");
                        player.key_count++;
                    }
                    if (player.key_count == 3) { MessageBox.Show("Gratulacje!\nPokonałeś wszystkich wrogów, a tym samym przeszedłeś grę!"); System.Windows.Application.Current.Shutdown(); }
                }
            }
            else MessageBox.Show("Najpierw stwórz postać!", "Brak postaci");
            
        }

        private void BtnCreation_Click(object sender, RoutedEventArgs e)
        {
            bool res = false;

                CharacterCreation characterCreationWindow = new CharacterCreation();
                characterCreationWindow.ShowDialog();

                player = characterCreationWindow.player;

                if (player != null)
                {
                    res = true;
                    MessageBox.Show($"Utworzono nowego gracza.", "Nowy gracz");
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas tworzenia gracza.", "Błąd");
                }
        }

    }

}
