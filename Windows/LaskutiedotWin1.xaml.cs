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
using Vuokratoimisto_projekti.Classes;
using Vuokratoimisto_projekti.Repositories;

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LaskutiedotWin1 : Window
    {

        private readonly LaskuTiedotRepo repo;
        private Lasku selectedLasku;

        public LaskutiedotWin1()
        {
            InitializeComponent();

            // Luodaan LaskuTiedotRepo-instanssi ja alustetaan se
            repo = new LaskuTiedotRepo();
            DataContext = repo;

            repo.HaeOsittaisetLaskut();

        }

        private void AvaaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LaskutListBox.SelectedItem != null)
            {
                Lasku selectedLasku = LaskutListBox.SelectedItem as Lasku;
         
                if (selectedLasku != null)
                {
                    // Hae täydelliset tiedot tietokannasta
                    Lasku kokoLasku = repo.GetKokoLasku(selectedLasku.ID);

                    if (kokoLasku != null)
                    {
                        LaskutiedotWin2 laskutiedotWin2 = new LaskutiedotWin2(kokoLasku);
                        laskutiedotWin2.Show();
                    }
                    else
                    {
                        MessageBox.Show("Laskun tietoja ei löytynyt.");
                    }
                }

            }
            else
            {
                MessageBox.Show("Valitse ensin lasku listasta.");
            }
        }
        private void TakaisinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LaskutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
