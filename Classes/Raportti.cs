using System;
using System.Collections.Generic;

namespace Vuokratoimisto_projekti.Classes
{
    public class Raportti
    {
        public List<Palvelu> TilatutPalvelut { get; set; } = new List<Palvelu>();
        public List<Huone> VaratutHuoneet { get; set; } = new List<Huone>();
        public int PalveluidenMaara { get; private set; } = 0;
        public int HuoneMaara { get; private set; } = 0;
        public DateTime AlkuAika { get; private set; }

        public DateTime Aika { get; private set; } = DateTime.Now;

        public void Add(Palvelu palvelu)
        {
            TilatutPalvelut.Add(palvelu);
            PalveluidenMaara++;
        }

        public void Add(Huone huone)
        {
            VaratutHuoneet.Add(huone);
            HuoneMaara++;
        }

        public Raportti()
        {
            DateTime now = DateTime.Now;
            AlkuAika = new DateTime(now.Year, now.Month, 1);
        }

        public override string ToString()
        {
            string message = "";

            if (TilatutPalvelut.Count > 0)
            {
                message += "Tilatut Palvelut:\n";
                foreach (var item in TilatutPalvelut)
                {
                    message += item.Nimi + "\n";
                }
            }

            if (VaratutHuoneet.Count > 0)
            {
                message += "Varatut Huoneet:\n";
                foreach (var item in VaratutHuoneet)
                {
                    message += item.Nimi + "\n";
                }
            }

            message += $"Tilattujen palveluiden määrä: {PalveluidenMaara} \nVarattujen huoneiden määrä: {HuoneMaara}";
            message += $"\nRaportin luonti aikaväli {AlkuAika} - {Aika}";

            return message.Trim();
        }
    }
}
