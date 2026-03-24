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
using Vuokratoimisto_projekti.Classes;

namespace Vuokratoimisto_projekti.Windows
{ 
    /// <summary>
    /// Interaction logic for ToimipisteetWin1.xaml
    /// </summary>
    public partial class ToimipisteetWin1 : Window
    {
        private TietokantaToiminnot TietokantaToiminnot;
        
        public ToimipisteetWin1()
        {
            InitializeComponent();
            //luodaan tiekantatoiminnot olio
            TietokantaToiminnot= new TietokantaToiminnot();

            //Täytetään comToimipisteet
            comToimipisteet.ItemsSource = TietokantaToiminnot.HaeToimipisteet();
        }

        /// <summary>
        /// Toimipisteen lisäys tapahtumankäsittelijä
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLisaa_Click(object sender, RoutedEventArgs e)
        {
            var toimipiste = (Toimipiste)this.DataContext;
            TietokantaToiminnot.LisaaToimipiste(toimipiste);

            //Lisätään huoneet uudessa ikkunassa ja palataan sen jälkeen tähän ikkunaan
            ToimipisteMuokkausWin lisaahuoneet = new ToimipisteMuokkausWin(toimipiste);
            var paluuarvo = lisaahuoneet.ShowDialog();

                if (paluuarvo == true)
            {
                comToimipisteet.ItemsSource = TietokantaToiminnot.HaeToimipisteet();
            }

        }

        /// <summary>
        /// Varmistetaan halutaanko varmasti poistaa. Jos kyllä, poistetaan tietokannasta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPoista_Click(object sender, RoutedEventArgs e)
        {
            //metodi toimipisteen poistoon ja varmistus halutaanko varmasti poistaa

            var valittuToimipiste = (Toimipiste)comToimipisteet.SelectedItem;
            if (valittuToimipiste != null)
            {
                var tulos = MessageBox.Show("Haluatko varmasti poistaa valitun tuotteen?", "Varoitus", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if(tulos == MessageBoxResult.Yes)
                {
                   
                    TietokantaToiminnot.PoistaToimipiste(valittuToimipiste);
                    comToimipisteet.ItemsSource=TietokantaToiminnot.HaeToimipisteet();
                }
            }
        }

        private void BtnMuokkaa_Click(object sender, RoutedEventArgs e)
        {
            //toimipisteen päivitys uuteen ikkunaan.Uuden ikkunan konstruktoriin välitetään päivitettävä toimipiste
            var valittuToimipiste = (Toimipiste)comToimipisteet.SelectedItem;

            if (valittuToimipiste != null)
            {
                ToimipisteMuokkausWin paivita = new ToimipisteMuokkausWin(valittuToimipiste);
                var paluuarvo = paivita.ShowDialog();

                if (paluuarvo== true)
                {
                    comToimipisteet.ItemsSource = TietokantaToiminnot.HaeToimipisteet();
                }
            }
        }
    }
}
