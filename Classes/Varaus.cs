using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuokratoimisto_projekti
{
    public class Varaus
    {
        public int ID { get; set; }
        public DateTime Varauspvm { get; set; }
        public DateTime Vahvistuspvm { get; set; }
        public DateTime AloitusPvm { get; set; }
        public DateTime Paattymispvm { get; set; }
        public int asiakas_id { get; set; }
        public int huone_id { get; set; }

        public Varaus()
        {
            Vahvistuspvm = DateTime.Now;
            Varauspvm = DateTime.Now;   
            ID = GenerateId();
        }
        public override string ToString()
        {
            return $"{AloitusPvm} {Paattymispvm}";
        }

        static int GenerateId() //ID:n generoiva metodi
        {
            List<int> generatedIds = new List<int>();
            Random random = new Random();

            // Silmukka ID luomista varten
            int id;
            do
            {
                id = random.Next(100, 9999);
                // Katsotaan onko ID olemassa
            } while (generatedIds.Contains(id));

            generatedIds.Add(id);  // Lisätään ID listaan. 
            //Console.WriteLine("Luotu lasku ID:llä " + id);
            return id;
        }

    }
}
