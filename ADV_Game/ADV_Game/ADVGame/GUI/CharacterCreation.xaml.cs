using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using POProjekt;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy CharacterCreation.xaml
    /// </summary>
    public partial class CharacterCreation : Window
    {
        public Player player;
        string pattern = @"^[A-Za-z]+$";
        public CharacterCreation()
        {
            InitializeComponent();
            Player w = new Player("w",HeroType.Warrior);
            Player m = new Player("m",HeroType.Mage);
            Player r = new Player("r",HeroType.Rogue);

            string customText1 = $"HP: {w.Hp}\nDMG:  {w.Dmg}\nDEF: {w.Def}\nPojemność EQ: {w.Eq_limit}\n";
            WarriorStats.Text = customText1;

            string customText2 = $"HP: {m.Hp}\nDMG: {m.Dmg}\nDEF: {m.Def}\nPojemność EQ: {m.Eq_limit}\n";
            MageStats.Text = customText2;

            string customText3 = $"HP: {r.Hp}\nDMG: {r.Dmg}\nDEF: {r.Def}\nPojemność EQ: {r.Eq_limit}\n";
            RogueStats.Text = customText3;
        }

        private void BWarrior_Click(object sender, RoutedEventArgs e)
        {
            bool res = false;
            if (Regex.IsMatch(PlayerName.Text, pattern) && PlayerName.Text.Length < 12)
            {
                string nick = PlayerName.Text;
                res = true;
                Player p = new Player(nick, HeroType.Warrior);
                player = p;
            }
            DialogResult = res;
        }

        private void BMage_Click(object sender, RoutedEventArgs e)
        {
            bool res = false;
            if (Regex.IsMatch(PlayerName.Text, pattern) && PlayerName.Text.Length < 12)
            {
                string nick = PlayerName.Text;
                res = true;
                Player p = new Player(nick, HeroType.Mage);
                player = p;
            }
            DialogResult = res;
        }

        private void BRogue_Click(object sender, RoutedEventArgs e)
        {
            bool res = false;
            if (Regex.IsMatch(PlayerName.Text, pattern) && PlayerName.Text.Length < 12)
            {
                string nick = PlayerName.Text;
                res = true;
                Player p = new Player(nick, HeroType.Rogue);
                player = p;
            }
            DialogResult = res;
        }
    }
}
