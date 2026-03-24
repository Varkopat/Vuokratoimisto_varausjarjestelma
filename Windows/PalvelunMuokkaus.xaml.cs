using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for PalvelunMuokkaus.xaml
    /// </summary>
    public partial class PalvelunMuokkaus : Window
    {
        private PalveluViewModel viewModel;
        private TietokantaToiminnot tietokantaToiminnot;

        public PalvelunMuokkaus()
        {
            InitializeComponent();
            tietokantaToiminnot = new TietokantaToiminnot();
            viewModel = new PalveluViewModel(tietokantaToiminnot);
            DataContext = viewModel;
        }

        private void Tallenna_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TallennaMuutokset();
        }

        private void LisaaUusiPalvelu_Click(object sender, RoutedEventArgs e)
        {
            UusiPalveluSivu uusiPalveluSivu = new UusiPalveluSivu();
            uusiPalveluSivu.ShowDialog();
        }

        private void Sulje_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Muokkaa_Click(object sender, RoutedEventArgs e)
        {
            if (PalvelutDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Virhe");
                return;
            }
            var palvelu = (Palvelu)PalvelutDataGrid.SelectedItem;
            UusiPalveluSivu uusiPalveluSivu = new UusiPalveluSivu(palvelu);
            uusiPalveluSivu.ShowDialog();
        }

        private void Poista_Click(object sender, RoutedEventArgs e)
        {
            if(PalvelutDataGrid.SelectedItem == null) {
                MessageBox.Show("Virhe");
                return;
            }
            var tulos = MessageBox.Show("Haluatko poistaa valitun palvelun?", "Varoitus", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (tulos == MessageBoxResult.No)
            {
                return;
            }
            var palvelu = (Palvelu)PalvelutDataGrid.SelectedItem;

            tietokantaToiminnot.PoistaPalvelu(palvelu);
            MessageBox.Show("Poistettu");
        }
    }


    public class PalveluViewModel : INotifyPropertyChanged
    {
        private TietokantaToiminnot tietokantaToiminnot;
        private ObservableCollection<Palvelu> palvelut;
        public ObservableCollection<Palvelu> Palvelut
        {
            get { return palvelut; }
            set
            {
                palvelut = value;
                OnPropertyChanged(nameof(Palvelut));
            }
        }

        public PalveluViewModel(TietokantaToiminnot tietokantaToiminnot)
        {
            this.tietokantaToiminnot = tietokantaToiminnot;
            Palvelut = tietokantaToiminnot.HaeKaikkiPalvelut();
        }

        public void TallennaMuutokset()
        {
            foreach (var palvelu in Palvelut)
            {
                tietokantaToiminnot.PaivitaPalvelu(palvelu);
            }
            MessageBox.Show("Muutokset tallennettu onnistuneesti!");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
