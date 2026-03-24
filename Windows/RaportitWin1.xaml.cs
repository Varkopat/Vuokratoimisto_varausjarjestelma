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
    /// Interaction logic for RaportitWin1.xaml
    /// </summary>
    public partial class RaportitWin1 : Window
    {
        private ObservableCollection<string> osittaisetLaskut;
        private readonly LaskuTiedotRepo repo;
        private Lasku selectedLasku;
        private Raportti Raportti;
        public RaportitWin1()
        {
            InitializeComponent();
            /*
            repo = new LaskuTiedotRepo();
            repo.HaeOsittaisetLaskut();*/

            Raportti = new Raportti();
            DataContext = this;

            // Example data
            Raportti.Add(new Palvelu { Nimi = "Palvelu1" });
            Raportti.Add(new Huone { Nimi = "Huone1" });

            ObservableCollection<string> osittaisetLaskut = new ObservableCollection<string> { Raportti.ToString() };
            LaskutListBox.ItemsSource = osittaisetLaskut;


        }
        public ObservableCollection<string> OsittaisetLaskut
        {
            get { return osittaisetLaskut; }
        }


        private void RaporttiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                        RaportitWin2 raportitWin2 = new RaportitWin2(kokoLasku);
                        raportitWin2.Show();                        
                    }
                    else
                    {
                        MessageBox.Show("Tietoja ei löytynyt.");
                    }
                }

            }
            else
            {
                MessageBox.Show("Valitse ensin asiakas listalta.");
            }
        }

        private void TakaisinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
