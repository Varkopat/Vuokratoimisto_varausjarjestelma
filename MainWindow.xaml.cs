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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vuokratoimisto_projekti.Windows;

namespace Vuokratoimisto_projekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        // Luodaan sanakirja käyttäjätunnuksille ja salasanoille
        private Dictionary<string, string> credentials = new Dictionary<string, string>()
    {
        {"Helsinki", "Helsinki"},
        {"Vantaa", "Vantaa"},
        {"Jyväskylä", "Jyväskylä"},
        {"Joensuu", "Joensuu"},
        {"Lappeenranta", "Lappeenranta"},
        {"Turku", "Turku"},
        {"Tampere", "Tampere"},
        {"Espoo", "Espoo"}
    };

     

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            // Tarkistetaan, onko TextBoxin teksti oletusteksti "Käyttäjätunnus"
            if (txtUsername.Text == "Käyttäjätunnus")
            {
                txtUsername.Text = ""; // Tyhjennetään TextBox
                txtUsername.Foreground = Brushes.Black; // Asetetaan tekstin väri mustaksi
            }
        }

        // Tapahtumankäsittelijä, joka aktivoituu, kun käyttäjätunnuksen TextBox menettää fokuksen
        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            // Tarkistetaan, onko TextBox tyhjä tai sisältää vain tyhjiä merkkejä
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Käyttäjätunnus"; // Asetetaan oletusteksti takaisin
                txtUsername.Foreground = Brushes.Gray; // Asetetaan tekstin väri harmaaksi
            }
        }

        // Tapahtumankäsittelijä, joka aktivoituu, kun salasanan näkyvän TextBoxin saa fokuksen
        private void txtVisiblePassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtVisiblePassword.Visibility = Visibility.Collapsed; // Piilotetaan näkyvä TextBox
            txtPassword.Visibility = Visibility.Visible; // Tehdään PasswordBox näkyväksi
            txtPassword.Focus(); // Asetetaan fokus PasswordBoxiin
        }

        // Tapahtumankäsittelijä, joka aktivoituu, kun PasswordBox menettää fokuksen
        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            // Tarkistetaan, onko PasswordBox tyhjä
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                txtPassword.Visibility = Visibility.Collapsed; // Piilotetaan PasswordBox
                txtVisiblePassword.Visibility = Visibility.Visible; // Tehdään näkyvä TextBox näkyväksi
                txtVisiblePassword.Text = "Salasana"; // Asetetaan oletusteksti "Salasana" näkyvään TextBoxiin
                txtVisiblePassword.Foreground = Brushes.Gray; // Asetetaan tekstin väri harmaaksi
            }
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Tarkistetaan, että käyttäjätunnus ja salasana eivät ole oletusarvoja eivätkä tyhjiä
            if (username != "Käyttäjätunnus" && !string.IsNullOrWhiteSpace(username) &&
                password != "Salasana" && !string.IsNullOrWhiteSpace(password))
            {
                // Tarkistetaan, löytyykö käyttäjätunnus sanakirjasta ja vastaako salasana
                if (credentials.ContainsKey(username) && credentials[username] == password)
                {
                    // Kirjautumistiedot ovat oikein, siirrytään Aloitusikkuna1:een
                    Aloitusikkuna1 aloitusikkuna = new Aloitusikkuna1();
                    aloitusikkuna.Show();
                    this.Close(); // Suljetaan kirjautumisikkuna
                }
                else
                {
                    MessageBox.Show("Virheellinen käyttäjätunnus tai salasana.");
                }
            }
            else
            {
                MessageBox.Show("Anna kelvollinen käyttäjätunnus ja salasana.");
            }
        }

        // Kirjautuminen ENTER näppäimellä
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}

