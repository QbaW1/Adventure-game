using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using POProjekt;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy PlayerEQ.xaml
    /// </summary>
    public partial class PlayerEQWindow : Window
    {
        private Player player;
        public PlayerEQWindow(Player player)
        {
            InitializeComponent();

            this.player = player;  

            List<Item> sortedList = new List<Item>(player.bagpack.Bagpack);
            sortedList.Sort();

            player.bagpack.Bagpack = new LinkedList<Item>(sortedList);

            foreach (Item x in player.bagpack.Bagpack)
            {
                ItemList.Items.Add(x.Type);
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ItemList.SelectedIndex > -1)
            {
                Item selectedItem = player.bagpack.Bagpack.ElementAt(ItemList.SelectedIndex);

                player.HeroLostEq(selectedItem);
                player.bagpack.RemoveItem(selectedItem);

                ItemList.Items.RemoveAt(ItemList.SelectedIndex);

                MessageBox.Show($"Usunięto przedmiot: {selectedItem.Type}", "Usunięto przedmiot");
            }
            else
            {
                MessageBox.Show("Proszę zaznacz przedmiot do usunięcia.", "Nie zaznaczono przedmiotu");
            }
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (ItemList.SelectedIndex > -1)
            {
                Item selectedItem = player.bagpack.Bagpack.ElementAt(ItemList.SelectedIndex);
                MessageBox.Show($"{selectedItem.description}\nWaga: {selectedItem.Weight}[kg]");
            }
            else { MessageBox.Show("Proszę zaznacz przedmiot do wglądu.", "Nie zaznaczono przedmiotu"); }
        }

        private void BtnEscEQ_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}
