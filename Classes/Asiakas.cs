using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuokratoimisto_projekti.Classes
{
    public class Asiakas
    {
        public int ID { get; set; }
        public string Nimi { get; set; }
        public string Lahiosoite { get; set; }
        public string Postitoimipaikka { get; set; }
        public string Postinro { get; set; }
        public string Email {  get; set; } 
        public string Puhelinnro { get; set; }


    }
}
