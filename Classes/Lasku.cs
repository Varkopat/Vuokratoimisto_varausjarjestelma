using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuokratoimisto_projekti.Classes
{
    public class Lasku
    {
        // Perustiedot
        public int ID { get; set; }
        public Asiakas Asiakas { get; set; }
        public string AsiakasNimi { get; set; }
        public int AsiakasId { get; set; }
        public string Lahiosoite { get; set; }
        public string Postitoimipaikka { get; set; }
        public string Postinro { get; set; }
        public DateTime VarattuPvm { get; set; }
        public double Summa { get; set; }
        public double ALV { get; set; }
        public DateTime DueDate => VarattuPvm.AddDays(7);
        public double ToimipisteenPaivahinta { get; set; }
        public string LaskutusTapa { get; set; }

        // Varaustiedot
        public Varaus Varaus { get; set; }
        public int VarausId { get; set; }
        public DateTime VarauksenAlkuPvm { get; set; }
        public DateTime VarauksenLoppuPvm { get; set; }
        public string VarauksenNimi { get; set; }
        public string VarauksenPostitoimipaikka { get; set; }
        public string ToimipisteenNimi { get; set; }


        // Palvelutiedot
        public string PalvelunNimi { get; set; }
        public List<Palvelu> Palvelut { get; set; } = new List<Palvelu>();
        public Palvelu Palvelu { get; set; }
        public Toimipiste Toimipiste { get; set; }
        /*public override string ToString()
        {
            string message = $"{ID} {AsiakasNimi} {Lahiosoite} {Postitoimipaikka} {Varaus.ToString()} " +
                 $"{VarauksenPostitoimipaikka} {ToimipisteenNimi} {LaskutusTapa}";

            foreach (var item in Palvelut)
            {
                message += item.Nimi.ToString();
            }

            message += $"{Summa}, {ALV}";
            return message;
        }*/
    }
    

    public class LaskuNakyma
    {
        public ObservableCollection<Lasku> OsittaisetLaskut { get; set; }
        public Lasku SelectedLasku { get; set; }

        public DateTime DueDate => SelectedLasku.VarattuPvm.AddDays(7);
        public string DueDateString => DueDate.ToString("dd.MM.yyyy");

        public LaskuNakyma()
        {
            OsittaisetLaskut = new ObservableCollection<Lasku>();
        }

        
    }
}
