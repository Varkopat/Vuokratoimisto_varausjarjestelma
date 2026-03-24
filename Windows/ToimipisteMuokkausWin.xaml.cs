using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vuokratoimisto_projekti.Classes;

namespace Vuokratoimisto_projekti.Windows
{
    public partial class ToimipisteMuokkausWin : Window
    {
        private ObservableCollection<Huone> huoneet;
        private TietokantaToiminnot tietokantaToiminnot;
        private ObservableCollection<Palvelu> palvelu;

        public ToimipisteMuokkausWin(Toimipiste toimipiste)
        {
            InitializeComponent();
            this.DataContext = (toimipiste);

            //alustetaan huone kokoelm ja tietokantaluokka olio. 
            huoneet = new ObservableCollection<Huone>();
            tietokantaToiminnot = new TietokantaToiminnot();
            //haetaan toimipisteen tiedot ja huoneidet tiedot näkymään
            huoneet = tietokantaToiminnot.HaeToimipisteenHuoneet(toimipiste);
            dataGrid.ItemsSource = huoneet;
        }
        /// <summary>
        /// Välittää päivitettävät tiedot tietokanta luokkaan ja sulkee ikkunan päivityksen jälkeen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Paivita_Click(object sender, RoutedEventArgs e)
        {
            var toimipiste = (Toimipiste)this.DataContext;

            tietokantaToiminnot.PaivitaToimipiste(toimipiste, huoneet);

            this.Close();
        }

      
    }
}