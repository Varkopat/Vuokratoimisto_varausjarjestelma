using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuokratoimisto_projekti.Classes
{
    public class Palvelu
    {
        public int PalveluID { get; set; }
        public int HuoneID { get; set; }
        public string Nimi { get; set; }
        public int Tyyppi { get; set; }
        public string Kuvaus { get; set; }
        public double Hinta { get; set; }
        public double ALV { get; set; }
        public string Yksikko { get; set; }
        public Palvelu()
        {
            HuoneID = 0;
            Nimi = string.Empty;
            Tyyppi = 0;
            Kuvaus = string.Empty;
            Hinta = 0;
            ALV = 0;

        }

        public override string ToString()
        {
            return $"{Nimi}";
        }
    }

}
