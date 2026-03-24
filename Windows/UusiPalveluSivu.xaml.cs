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
    /// Interaction logic for UusiPalveluSivu.xaml
    /// </summary>
    public partial class UusiPalveluSivu : Window
    {
        private TietokantaToiminnot tietokantaToiminnot;
        private bool isNew = true; 

        public UusiPalveluSivu()
        {
            InitializeComponent();

            tietokantaToiminnot=new TietokantaToiminnot();
            var huoneet = tietokantaToiminnot.HaeToimipisteetJaHuoneet();
            comHuoneet.ItemsSource = huoneet;
        }

        public UusiPalveluSivu(Palvelu palvelu): this()
        {
            Title = "Muokkaa palvelu";
            isNew = false;
            this.DataContext = palvelu;
        }

        private void TallennaUusiPalvelu_Click(object sender, RoutedEventArgs e)
        {
            if(comHuoneet.SelectedItem == null)
            {
                MessageBox.Show("virhe");
                return;
            }
            var palvelu=(Palvelu)this.DataContext;
            if (isNew)
            {
                tietokantaToiminnot.LisaaPalvelu(palvelu);
            }
            else
            {
                tietokantaToiminnot.PaivitaPalvelu(palvelu);
            }

            MessageBox.Show("Uusi palvelu tallennettu!");

            DialogResult = true;
        }

        private void Peruuta_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


    }
}
