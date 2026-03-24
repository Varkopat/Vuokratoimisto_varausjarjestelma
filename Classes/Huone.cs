using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuokratoimisto_projekti.Classes
{
    public class Huone
    {
        public int HuoneID { get; set; }
        public string Nimi { get; set; }
        public double Hinta { get; set; }
        public string Kapasiteetti { get; set; }
        public int ToimipisteID { get; set; }

        public Huone()
        {
            Nimi = string.Empty;
            Hinta = 0;
            Kapasiteetti = string.Empty;
            ToimipisteID = 0;
        }

        public override string ToString()
        {
            return $"{Nimi}"; 
        }

        public string TulostaTiedot()
        {
            return $"Nimi: {Nimi}\nHinta: {Hinta} e\nKapasiteetti: {Kapasiteetti}";
        }

    }
}
