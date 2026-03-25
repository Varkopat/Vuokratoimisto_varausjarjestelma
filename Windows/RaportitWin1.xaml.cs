using System;
using System.Windows;
using System.Windows.Controls;
using Vuokratoimisto_projekti.Classes;
using Vuokratoimisto_projekti.Repositories;

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for RaportitWin1.xaml
    /// </summary>
    public partial class RaportitWin1 : Window
    {
        private readonly LaskuTiedotRepo repo;

        public RaportitWin1()
        {
            InitializeComponent();
            
            repo = new LaskuTiedotRepo();
            DataContext = repo;
            
            // Hae laskut tietokannasta
            repo.HaeOsittaisetLaskut();
        }

        private void RaporttiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Event handler for future use
        }

        private void AvaaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LaskutListBox.SelectedItem is Lasku selectedLasku)
            {
                // Hae täydelliset tiedot tietokannasta
                Lasku kokoLasku = repo.GetKokoLasku(selectedLasku.ID);

                if (kokoLasku != null)
                {
                    RaportitWin2 raportitWin2 = new RaportitWin2(kokoLasku);
                    raportitWin2.Show();
                }
                else
                {
                    MessageBox.Show("Tietoja ei löytynyt.");
                }
            }
            else
            {
                MessageBox.Show("Valitse ensin lasku listalta.");
            }
        }

        private void TakaisinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
