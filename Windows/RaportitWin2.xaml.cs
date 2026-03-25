using System;
using System.Windows;
using System.Windows.Controls;
using Vuokratoimisto_projekti.Classes;

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for RaportitWin2.xaml
    /// </summary>
    public partial class RaportitWin2 : Window
    {
        private Lasku lasku;

        public RaportitWin2(Lasku kokoLasku)
        {
            InitializeComponent();
            
            if (kokoLasku == null)
            {
                MessageBox.Show("Laskun tiedot ovat puutteelliset.");
                this.Close();
                return;
            }

            lasku = kokoLasku;
            DataContext = lasku;
        }

        private void TakaisinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TulostaBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Lasku");
            }
        }

        private void VieAsiakasTietoihinBtn_Click(object sender, RoutedEventArgs e)
        {
            // Avaa asiakastietojen muokkaus-ikkuna
            MessageBox.Show($"Asiakkaan {lasku.AsiakasNimi} tietojen avaaminen.");
        }
    }
}
