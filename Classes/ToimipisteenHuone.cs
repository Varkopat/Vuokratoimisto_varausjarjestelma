using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuokratoimisto_projekti.Classes
{
    public class ToimipisteenHuone
    {
        public int HuoneID { get; set; }
        public string Postitoimipaikka { get; set; }
        public string ToimipisteenNimi { get; set; }
        public string HuoneenNimi { get; set; }
        public string Kapasiteetti { get; set; }
        public string Nimi => $"{Postitoimipaikka}, {ToimipisteenNimi}, {HuoneenNimi} ({Kapasiteetti}) ";

    }
}
