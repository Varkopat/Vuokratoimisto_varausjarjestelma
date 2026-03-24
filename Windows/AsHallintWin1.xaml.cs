using System.Windows;
using Vuokratoimisto_projekti.Classes;

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for AsHallintWin1.xaml
    /// </summary>
    public partial class AsHallintWin1 : Window
    {
        private AsiakasToiminnot asiakasToiminnot = new AsiakasToiminnot();

        public AsHallintWin1()
        {
            InitializeComponent();
            PaivitaAsiakasLista();
        }

        private void PaivitaAsiakasLista()
        {
            var asiakkaat = asiakasToiminnot.HaeAsiakkaat();
            comAsiakkaat.ItemsSource = asiakkaat;
        }

        private void BtnMuuta_Click(object sender, RoutedEventArgs e)
        {
            if (comAsiakkaat.SelectedItem == null)
            {
                MessageBox.Show(this, "Virhe");

                return;
            }
            var asiakas = (Asiakas)comAsiakkaat.SelectedItem;
            var am = new AsiakasMuuttaminen(asiakas);
            if(am.ShowDialog() == true)
            {
                asiakas = (Asiakas)am.DataContext;
                asiakasToiminnot.MuutaAsiakas(asiakas);
                MessageBox.Show(this, "Asiakkaan muuttaminen on valmis");
            }   
        }

        private void BtnPoista_Click(object sender, RoutedEventArgs e)
        {
            
            if (comAsiakkaat.SelectedItem == null)
            {
                MessageBox.Show(this, "Virhe");

                return;
            }
            var tulos = MessageBox.Show("Haluatko poistaa valitun asiakkaan?", "Varoitus", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (tulos == MessageBoxResult.No)
            {
                return;
            }
            var asiakas = (Asiakas)comAsiakkaat.SelectedItem;
            asiakasToiminnot.PoistaAsiakas(asiakas);
            PaivitaAsiakasLista();
            MessageBox.Show(this, "Asiakkaan poistaminen on valmis");
        }

        private void BtnLisaa_Click(object sender, RoutedEventArgs e)
        {
            var asiakas = (Asiakas)DataContext;
            asiakasToiminnot.LisaaAsiakas(asiakas);
            PaivitaAsiakasLista();
            MessageBox.Show(this, "Asiakkaan lisääminen on valmis");
        }
    }
}
