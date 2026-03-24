using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for VarauksetWin1.xaml
    /// </summary>
    public partial class VarauksetWin1 : Window
    {
        private TietokantaToiminnot TietokantaToiminnot;
        private Varaus varaus = new Varaus();
        private Asiakas asiakas = new Asiakas();

        public VarauksetWin1()
        {
            InitializeComponent();

            // Luodaan tietokantatoiminnot olio
            TietokantaToiminnot = new TietokantaToiminnot();
        }

        private void SeuraavaBtn_Click(object sender, RoutedEventArgs e)
        {
            //Katsotaan onko kaikki tiedot täytetty
            bool check = CheckIfEmpty();
            // Tallennetaan asiakastiedot
            if (!check)
            {
                asiakas.Nimi = TextBoxNimi.Text;
                asiakas.Puhelinnro = TextBoxPuhelin.Text;
                asiakas.Postitoimipaikka = TextBoxKaupunki.Text;
                asiakas.Email = TextBoxEmail.Text;
                asiakas.Lahiosoite = TextBoxLahiosoite.Text;
                asiakas.Postinro = TextBoxPostinumero.Text;

            }
            else
            {
                MessageBox.Show("Täytä asiakastiedot ensin");
                return;
            }

            if (TextBoxPostinumero.Text.Length > 5 || TextBoxPostinumero.Text.Length <= 0)
            {
                MessageBox.Show("Postinumero ei kelpaa");
                return;
            }

            //Tallennetaan varaustiedot 
            if (dpAloituspvm.SelectedDate.HasValue)
            {
                varaus.AloitusPvm = dpAloituspvm.SelectedDate.Value;
                varaus.Paattymispvm = dpLopetuspvm.SelectedDate.Value;
                varaus.Vahvistuspvm = DateTime.Now;
                varaus.Varauspvm = DateTime.Now; 
            };

            string kaupunki = comPaikkakunta.Text;
            // Avataan seuraava ikkuna ja siirretään asiakas ja varaus tiedot talteen seuraavaan ikkunaan
            VarauksetWin2 varauksetWin2 = new VarauksetWin2(asiakas, varaus, kaupunki);
            varauksetWin2.Show();
            this.Close();
        }

        private void TakaisinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //-------- KÄYTTÖMUKAVUUS TOIMINNOT + BUGIEN ESTOT------
        //Postinumeron voi syöttää vain numeroina
        private void TextBoxPostinumero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }
        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$"); 
        }

        private void MoveFocusOnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Move focus to the next control
                var uie = e.OriginalSource as UIElement;
                uie?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private bool CheckIfEmpty()
        {
            bool ifEmpty = false;

            if (string.IsNullOrEmpty(TextBoxNimi.Text))
            {
                ifEmpty = true;
            }
            if (string.IsNullOrEmpty(TextBoxEmail.Text))
            {
                ifEmpty = true;
            }
            if (string.IsNullOrEmpty(TextBoxKaupunki.Text))
            {
                ifEmpty = true;
            }
            if (string.IsNullOrEmpty(TextBoxLahiosoite.Text))
            {
                ifEmpty = true;
            }
            if (!dpAloituspvm.SelectedDate.HasValue)
            {
                ifEmpty = true;
            }
            if (!dpLopetuspvm.SelectedDate.HasValue)
            {
                ifEmpty = true;
            }


            return ifEmpty;
        }
    }
};

