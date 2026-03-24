using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Vuokratoimisto_projekti.Classes;
using System.Windows.Input;
using System.Diagnostics.Eventing.Reader;

namespace Vuokratoimisto_projekti.Windows
{
    public partial class VarauksetWin2 : Window
    {
        private ObservableCollection<Toimipiste> Toimipisteet;
        private TietokantaToiminnot TietokantaToiminnot;
        private Toimipiste selectedToimipiste;
        private List<Palvelu> Palvelut = new List<Palvelu>();
        private Palvelu selectedPalvelu;
        private Asiakas Asiakas;
        private Varaus Varaus;
        private string LaskutusTapa = "Email";

        //Valittu huone
        private Huone selectedHuone;
        //Laskun luonti
        private Lasku lasku = new Lasku();

        public VarauksetWin2()
        {
            InitializeComponent();

            // Create database operations object
            TietokantaToiminnot = new TietokantaToiminnot();

            // Load Toimipisteet
            comToimipisteet.ItemsSource = TietokantaToiminnot.HaeToimipisteet();

        }
        public VarauksetWin2(Asiakas asiakas, Varaus varaus, string valittuKaupunki)
        {
            InitializeComponent();

            // Create database operations object
            TietokantaToiminnot = new TietokantaToiminnot();

            // Load Toimipisteet
            comToimipisteet.ItemsSource = TietokantaToiminnot.HaeToimipisteetPerKaupunki(valittuKaupunki);
            Asiakas = asiakas;
            Varaus = varaus;
            lasku.VarauksenPostitoimipaikka = valittuKaupunki;
        }

        //Seuraavaan ikkunaan siirryttäessä tallennetaan Lasku
        private void SeuraavaBtn_Click(object sender, RoutedEventArgs e)
        {
            //Asiakkaan vienti tietokantaan
           

            TietokantaToiminnot.LisaaAsiakas(Asiakas);
            
            //Varauksen täydennys
            Varaus.asiakas_id = TietokantaToiminnot.GetCustomerId(Asiakas);
            Varaus.huone_id = selectedHuone.HuoneID;
            TietokantaToiminnot.LisaaVaraus(Varaus);

            //Asiakas tiedot
            lasku.Asiakas = Asiakas;
            lasku.AsiakasNimi = Asiakas.Nimi;
            lasku.Lahiosoite = Asiakas.Lahiosoite;
            lasku.Postitoimipaikka = Asiakas.Postitoimipaikka;
            lasku.VarattuPvm = Varaus.Varauspvm;
            lasku.ToimipisteenNimi = comToimipisteet.Text;
            

            //Varaustiedot
            lasku.Summa = LaskeLaskunSumma();
            lasku.ALV = 24;
            lasku.Varaus = Varaus;
            lasku.VarauksenAlkuPvm = Varaus.AloitusPvm;
            lasku.VarauksenLoppuPvm = Varaus.Paattymispvm;
            lasku.Palvelut = Palvelut;

            lasku.LaskutusTapa = LaskutusTapa;
            VarausWin3 varausWin3 = new VarausWin3(lasku, Asiakas, Varaus, selectedHuone); // Open next window
            varausWin3.Show();
            this.Close();
        }

        private void TakaisinBtn_Click(object sender, RoutedEventArgs e)
        {
            VarauksetWin1 varauksetWin1 = new VarauksetWin1(); // Open previous window
            varauksetWin1.Show();
            this.Close();
        }

        //Lasketaan lopullinen hinta laskulle
        private double LaskeLaskunSumma()
        {
            double summa = selectedHuone.Hinta;
            foreach (var palvelu in Palvelut)
            {
                summa += palvelu.Hinta;
            }

            return summa; 
        }

        private void comToimipisteet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comToimipisteet.SelectedItem != null)
            {
                selectedToimipiste = (Toimipiste)comToimipisteet.SelectedItem;

                // Fetch rooms of the selected toimipiste
                var huoneet = TietokantaToiminnot.HaeToimipisteenHuoneet(selectedToimipiste);

                // Clear existing items and set new ItemsSource
                comHuoneet.ItemsSource = null;
                comHuoneet.ItemsSource = huoneet;
                comHuoneet.SelectedIndex = 0;
            }
            //Tyhjennetään palvelut lista, jos huone/toimipiste valintaa muutetaan kesken laskun teon
            Palvelut.Clear();
            UpdatePalvelutList();
        }

        private void comHuoneet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Huoneen valinta
            if (comHuoneet.SelectedItem != null)
            {
                selectedHuone = (Huone)comHuoneet.SelectedItem;
                infoHuoneet.Text = selectedHuone.TulostaTiedot() + "\nToimipiste: " + selectedToimipiste.Nimi + "\nKaupunki " + selectedToimipiste.Postitoimipaikka;

                var palvelut = TietokantaToiminnot.HaeHuoneenPalvelut(selectedHuone);

                comPalvelut.ItemsSource = null;
                comPalvelut.ItemsSource = palvelut;
                comPalvelut.DisplayMemberPath = "Nimi";
                //Palvelut valikon default indeksi = 0 (miellyttävämpi käyttökokemus)
                comPalvelut.SelectedIndex = 0;
            }

            //Tyhjennetään palvelut lista, jos huone/toimipiste valintaa muutetaan kesken laskun teon
            Palvelut.Clear();
            UpdatePalvelutList();
        }

        //Palvelut listan UI päivitys
        private void UpdatePalvelutList()
        {
            listPalvelut.ItemsSource = Palvelut.ToList();
        }

        private void Lisaa_btn_Click(object sender, RoutedEventArgs e)
        {
            //Lisätään palvelut listalle valittu palvelu
            Palvelut.Add(selectedPalvelu);
            UpdatePalvelutList();
            
        }

        private void comPalvelut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Palvelun valinta
            if (comPalvelut.SelectedItem != null)
            {
                selectedPalvelu = (Palvelu)comPalvelut.SelectedItem;
            }
        }

        private void Poista_btn_Click(object sender, RoutedEventArgs e)
        {
            if (listPalvelut.SelectedItem != null)
            {
                Palvelut.Remove(selectedPalvelu);
                UpdatePalvelutList();
            }

        }

        private void listPalvelut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listPalvelut.SelectedItem != null)
            {
                selectedPalvelu = (Palvelu)listPalvelut.SelectedItem;
            }
        }

        //Näppäin painallukset
        private void comPalvelut_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                //Entterillä voi toteuttaa myös itemin lisäyksen
                Lisaa_btn_Click(sender, e);
            }

        }

        //Itemin poistaminen delete näppäimellä
        private void listPalvelut_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.Delete))
            {
                //Entterillä voi toteuttaa myös itemin lisäyksen
                Poista_btn_Click(sender, e);
            }

        }

        private void radPaperi_Checked(object sender, RoutedEventArgs e)
        {
            LaskutusTapa = radChecked();
        }

        private string radChecked()
        {
            string laskutusValinta;
            if (radPaperi.IsChecked == true)
            {
                laskutusValinta = "Paperi";
            }
            else
            {
                laskutusValinta = "Email";
            }
            return laskutusValinta;
        }
    }
}
