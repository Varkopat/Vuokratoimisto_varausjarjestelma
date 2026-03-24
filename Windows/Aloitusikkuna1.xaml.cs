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

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for Aloitusikkuna1.xaml
    /// </summary>
    public partial class Aloitusikkuna1 : Window
    {
        public Aloitusikkuna1()
        {
            InitializeComponent();
        }

        private void Varaukset_MouseEnter(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "Varaukset: Voit luoda uusia varauksia ja tarkastella olemassa olevia varauksia.";
        }

        private void Laskut_MouseEnter(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "Laskut: Tarkastele ja hallitse laskuja.";
        }

        private void Raportit_MouseEnter(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "Raportit: Tarkastele raportteja toiminnasta ja tuloksista.";
        }

        private void Intro_MouseLeave(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "Liikuta hiiri painikkeiden päälle nähdäksesi lisätietoja.";
        }

        private void Asiakashallinta_MouseEnter(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "Asiakashallinta järjestelmä: Hallitse asiakastietoja ja asiakassuhteita.";
        }

        private void Toimipisteet_MouseEnter(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "Toimipisteiden hallintajärjestelmä: Ylläpidä ja hallinnoi toimipisteen tietoja.";
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Varmistusdialogi
            MessageBoxResult result = MessageBox.Show("Haluatko varmasti sulkea ohjelman?", "Vahvista sulkeminen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Tarkista käyttäjän vastaus
            if (result == MessageBoxResult.Yes)
            {
                this.Close(); // Sulkee ikkunan vain, jos käyttäjä valitsee 'Kyllä'
            }
        }
        private void PalveluidenHallinta_Click(object sender, RoutedEventArgs e)
        {
            // Luo uusi instanssi PalvelunMuokkaus-ikkunasta
            PalvelunMuokkaus palvelunMuokkausIkkuna = new PalvelunMuokkaus();
            // Näytä uusi ikkuna
            palvelunMuokkausIkkuna.ShowDialog();
        }

        private void PalveluidenHallinta_MouseEnter(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "Voit muokata palveluja.";
        }

        private void PalveluidenHallinta_MouseLeave(object sender, MouseEventArgs e)
        {
            infoTextBlock.Text = "";
        }


        private void Varaukset_Click(object sender, RoutedEventArgs e)
        {
            // Luodaan uusi VarauksetWin-ikkuna
            VarauksetWin1 varauksetWindow = new VarauksetWin1();

            // Näytetään VarauksetWin-ikkuna
            varauksetWindow.Show();

            // Vaihtoehtoisesti, jos haluat että nykyinen ikkuna sulkeutuu kun uusi avataan:
            // this.Close();
        }

        private void Laskut_Click(object sender, RoutedEventArgs e)
        {
            
            LaskutiedotWin1 laskutiedotWin1 = new LaskutiedotWin1();
            laskutiedotWin1.Show();
            
        }
        private void Raportit_Click(object sender, RoutedEventArgs e)
        {

            RaportitWin1 varauksetWindow = new RaportitWin1();
            varauksetWindow.Show();

        }
        private void AsHallint_Click(object sender, RoutedEventArgs e)
        {

            AsHallintWin1 varauksetWindow = new AsHallintWin1();
            varauksetWindow.Show();

        }

        private void Toimipisteet_Click(object sender, RoutedEventArgs e)
        {
            // luodaan uusi toimipiste ikkuna ja avataan se
            var toimipisteet= new ToimipisteetWin1();

            toimipisteet.Show();

        }
    }

}
