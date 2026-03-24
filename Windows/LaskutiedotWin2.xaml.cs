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
using Vuokratoimisto_projekti.Repositories;
using static Vuokratoimisto_projekti.Repositories.LaskuTiedotRepo;

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for LaskutiedotWin2.xaml
    /// </summary>
    public partial class LaskutiedotWin2 : Window
    {
        private readonly LaskuTiedotRepo repo;
        public Lasku SelectedLasku { get; set; }
        public LaskutiedotWin2(Lasku selectedLasku)
        {
            InitializeComponent();

            SelectedLasku = selectedLasku;
            DataContext = this;

            repo = new LaskuTiedotRepo();

            // Laske varauksen kesto päivissä
            int varauksenKesto = (SelectedLasku.VarauksenLoppuPvm - SelectedLasku.VarauksenAlkuPvm).Days;

            // Laske huoneen hinta
            double huoneHinta = varauksenKesto * SelectedLasku.ToimipisteenPaivahinta;

            // Lisää huoneen tiedot ensimmäiselle riville
            var huonePalvelu = new Palvelu
            {
                Nimi = "Huone: " + SelectedLasku.ToimipisteenNimi,
                Hinta = huoneHinta,
                ALV = SelectedLasku.ALV
            };

            SelectedLasku.Palvelut.Insert(0, huonePalvelu);

            // Lisää yksikkö "pvä" kaikille palveluille
            foreach (var palvelu in SelectedLasku.Palvelut)
            {
                palvelu.Yksikko = "pvä";
            }

            // Laske palveluiden yhteissumma ja päivitä laskun summa
            var palvelutSumma = SelectedLasku.Palvelut.Sum(p => p.Hinta);
            SelectedLasku.Summa = palvelutSumma;
        }
    

        private void SuljeBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Haluatko varmasti sulkea ikkunan?", "Vahvista sulkeminen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void PoistaBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Oletko varma, että haluat poistaa laskun?", "Vahvista poisto", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Poista lasku tietokannasta
                repo.PoistaLasku(SelectedLasku.ID);

                // Viesti, että lasku on poistettu
                MessageBox.Show("Lasku poistettu onnistuneesti.", "Poisto vahvistettu", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
        }
    }

}
