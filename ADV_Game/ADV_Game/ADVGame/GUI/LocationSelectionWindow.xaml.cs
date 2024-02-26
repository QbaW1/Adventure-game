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
using System.Windows.Shapes;
using POProjekt;

namespace GUI
{
    /// <summary>
    /// Logika interakcji dla klasy LocationSelectionWindow.xaml
    /// </summary>
    public partial class LocationSelectionWindow : Window
    {
        public POProjekt.Localization SelectedLocalization { get; private set; }
        
        public LocationSelectionWindow()
        {
            InitializeComponent();
            Valley v  = new Valley();
            ValleyDescp.Text = v.localization_description;
            Port p = new Port();
            PortDescp.Text = p.localization_description;
            Graveyard g = new Graveyard();
            GraveyardDescp.Text = g.localization_description;
        }

        private void Valley_Click(object sender, RoutedEventArgs e)
        {
            SelectedLocalization = new Valley();
            DialogResult = true;
            Close();
        }

        private void Port_Click(object sender, RoutedEventArgs e)
        {
            SelectedLocalization = new Port();
            DialogResult = true;
            Close();
        }

        private void Cementary_Click(object sender, RoutedEventArgs e)
        {
            SelectedLocalization = new Graveyard();
            DialogResult = true;
            Close();
        }
    }
}
