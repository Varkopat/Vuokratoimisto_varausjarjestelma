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
    /// Interaction logic for VarausWin3.xaml
    /// </summary>
    public partial class VarausWin3 : Window
    {
        private TietokantaToiminnot TietokantaToiminnot;
        private Asiakas Asiakas;
        private Varaus Varaus;
        private Lasku Lasku;
        private Huone Huone;
        public VarausWin3(Lasku lasku, Asiakas asiakas, Varaus varaus, Huone huone)
        {
            InitializeComponent();

            Huone = huone;
            TietokantaToiminnot = new TietokantaToiminnot();
            Lasku = lasku;
            //VarausTiedot.Text = Lasku.ToString();
            Asiakas = asiakas;
            Varaus = varaus;
            Lasku.Varaus = Varaus;
            Lasku.VarausId = Varaus.ID;
            Lasku.Postinro = Asiakas.Postinro;
            Lasku.AsiakasId = TietokantaToiminnot.GetCustomerId(Asiakas);

            this.DataContext = Lasku;
            palvelut_tulostus.Text = PrintPalvelut();
        }

        private void ValmisBtn_Click(object sender, RoutedEventArgs e) //Valmis painike
        {
            
            TietokantaToiminnot.LisaaLasku(Lasku);
            LisaaRaporttiin(Lasku);
            this.Close();
        }

        private void TakaisinBtn_Click(object sender, RoutedEventArgs e) //Takaisin näppäin
        {
            VarauksetWin2 varauksetWin2 = new VarauksetWin2(); //Avataan aikaisempi ikkuna
            varauksetWin2.Show();
            this.Close();
        }

        private string PrintPalvelut()
        {
            string text = "";
            foreach (var item in Lasku.Palvelut) 
            {
                text += item.ToString();
                text += "\n";
            }
            return text;
        }

        private void LisaaRaporttiin(Lasku lasku)
        {
            Raportti raportti = new Raportti();
            
            
            foreach (var item in lasku.Palvelut)
            {
                raportti.Add(item);
            }
            raportti.Add(Huone);

        }
    }
}
