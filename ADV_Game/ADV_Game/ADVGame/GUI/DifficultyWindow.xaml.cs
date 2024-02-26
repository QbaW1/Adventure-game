using POProjekt;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for DifficultyWindow.xaml
    /// </summary>
    public partial class DifficultyWindow : Window
    {
        public Difficulty Difficulty;
       

        public DifficultyWindow(Difficulty difficulty)
        {
            InitializeComponent();
            Difficulty = difficulty;
        }

        private void BtnEasy_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = Difficulty.easy;
            MessageBox.Show("Zmieniono poziom trudnosci na łatwy!", "Poziom trudności");
            
            Close();
        }

        private void BtnHard_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = Difficulty.hard;
            MessageBox.Show("Zmieniono poziom trudnosci na trudny!", "Poziom trudności");
           
            Close();
        }

        private void BtnMaster_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = Difficulty.master;
            MessageBox.Show("Zmieniono poziom trudnosci na mistrzowski!", "Poziom trudności");
          
            Close();
        }

        
    }
}
