using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuokratoimisto_projekti.Classes
{
    public class Toimipiste
    {
        public int ID { get; set; }
        public string Nimi { get; set; }
        public string Lahiosoite { get; set; }
        public string Postitoimipaikka { get; set; }
        public string Postinro { get; set; }
        public string Email { get; set; }
        public string Puhelinnro { get; set; }

        public ObservableCollection<Huone> Huoneet {  get; set; }

        public Toimipiste()
        {
            Nimi = string.Empty;
            Lahiosoite = string.Empty;
            Postitoimipaikka= string.Empty;
            Postinro = string.Empty;
            Email = string.Empty;
            Puhelinnro = string.Empty;

            Huoneet= new ObservableCollection<Huone>();

        }
        public string ToimipisteenTiedot => $"{Nimi}, {Lahiosoite}, {Postinro}, {Postitoimipaikka} ";

        public override string ToString()
        {
            return $"{Nimi}, {Lahiosoite}, {Postinro}, {Postitoimipaikka}";
        }

    }
}
