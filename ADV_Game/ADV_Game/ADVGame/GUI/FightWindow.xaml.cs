using POProjekt;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for FightWindow.xaml
    /// </summary>
    public partial class FightWindow : Window
    {
        Player player;
        Enemy enemy;
        public bool Won;

        int characterHp;
        int enemyHp;

        public FightWindow() : base()
        {
            InitializeComponent();
        }
        public FightWindow(Player player, Enemy enemy, bool won)
        {
            InitializeComponent();
            this.player = player;
            this.enemy = enemy;
            this.Won = won;

            characterHp = player.Hp;
            enemyHp = enemy.Hp;
            UpdateUI();
        }

        private void UpdateUI()
        {
            TxtEnemy.Text = enemy.Name;
            TxtPlayerHP.Text = characterHp.ToString();
            TxtEnemyHP.Text = enemyHp.ToString();
        }

        private void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            int characterAtt = player.HeroAttack(player.Dmg);
            enemyHp -= (int)((1 - (enemy.Def / 100.0)) * characterAtt);
            MessageBox.Show($"Zaatakowałeś przeciwnika! Zostało mu {enemyHp} życia!", "Twój atak");

            if (enemyHp <= 0)
            {
                if (enemy.EnemyItem != null)
                {
                    player.bagpack.AddItem(enemy.EnemyItem);
                    MessageBox.Show($"Znaleziono {enemy.EnemyItem.Type}","Pozyskano przedmiot");
                }
                Won = true;
                MessageBox.Show("Wygrałeś!", "Koniec walki");

                Close();
            }
            else
            {
                int enemyAtt = enemy.EnemyAttack(enemy.Dmg);
                characterHp -= (int)((1 - (player.Def / 100.0)) * enemyAtt);
                MessageBox.Show($"Przeciwnik zaatakował! Zostało ci {characterHp} życia!", "Atak przeciwnika");

                if (characterHp <= 0)
                {
                    Won = false;
                    MessageBox.Show("Przegrałeś!", "Koniec walki");

                    Close();
                }
                else
                {
                    UpdateUI();
                }
            }
        }

        private void BtnHeal_Click(object sender, RoutedEventArgs e)
        {
            Item healthPotion = player.bagpack.FindItem(item => item.Type == ItemType.HealthPotion);

            if (healthPotion != null)
            {
                characterHp = player.Hp;
                player.bagpack.RemoveItem(healthPotion);
                MessageBox.Show($"Wróciłeś do pełni zdrowia!\nTwoje HP: {player.Hp}", "Ulecz się");
                UpdateUI();
            }
            else
            {
                MessageBox.Show("W twoim ekwipunku nie ma mikstur zdrowia!", "Brak mikstur");
            }
        }

        private void BtnEscape_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Uciekłeś...", "Uciekasz");
            Won = false;
            Close();
        }
    }
}