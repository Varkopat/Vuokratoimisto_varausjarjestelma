using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Vuokratoimisto_projekti.Classes;

namespace Vuokratoimisto_projekti.Repositories
{
    public class LaskuTiedotRepo
    {
        private const string localWithDb = "Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=varausjarjestelma;";
        private readonly string _connectionString;

        public ObservableCollection<Lasku> OsittaisetLaskut { get; set; }

        public LaskuTiedotRepo()
        {
            OsittaisetLaskut = new ObservableCollection<Lasku>();
            _connectionString = localWithDb;
        }
        //Hae osittaisia laskutietoja LaskutiedotWin1 varteen
        public void HaeOsittaisetLaskut()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT lasku.lasku_id, asiakas.nimi AS asiakas_nimi, toimipiste.postitoimipaikka, varaus.aloitus_pvm, varaus.loppu_pvm, lasku.summa FROM lasku " +
                                                    "INNER JOIN asiakas ON lasku.asiakas_id = asiakas.asiakas_id " +
                                                    "INNER JOIN varaus ON lasku.varaus_id = varaus.varaus_id " +
                                                    "INNER JOIN huone ON varaus.huone_id = huone.huone_id " +
                                                    "INNER JOIN toimipiste ON huone.toimipiste_id = toimipiste.toimipiste_id", conn);

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    OsittaisetLaskut.Add(new Lasku
                    {
                        ID = dr.GetInt32("lasku_id"),
                        AsiakasNimi = dr.GetString("asiakas_nimi"),
                        Postitoimipaikka = dr.GetString("postitoimipaikka"),
                        VarauksenAlkuPvm = dr.GetDateTime("aloitus_pvm"),
                        VarauksenLoppuPvm = dr.GetDateTime("loppu_pvm"),
                        Summa = dr.GetDouble("summa")
                    });
                }
            }
        }
        //Hae koko laskun tietoja LasktiedotWin2 varteen
        public Lasku GetKokoLasku(int laskuId)
        {
            Lasku lasku = null;
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT lasku.lasku_id, asiakas.nimi AS asiakas_nimi, asiakas.lahiosoite, asiakas.postitoimipaikka, " +
                                                    "varaus.aloitus_pvm, varaus.loppu_pvm, varaus.varattu_pvm, " +
                                                    "toimipiste.nimi AS toimipisteen_nimi, huone.hinta AS huoneen_hinta, " +
                                                    "lasku.summa, lasku.alv " +
                                                    "FROM lasku " +
                                                    "INNER JOIN asiakas ON lasku.asiakas_id = asiakas.asiakas_id " +
                                                    "INNER JOIN varaus ON lasku.varaus_id = varaus.varaus_id " +
                                                    "INNER JOIN huone ON varaus.huone_id = huone.huone_id " +
                                                    "INNER JOIN toimipiste ON huone.toimipiste_id = toimipiste.toimipiste_id " +
                                                    "WHERE lasku.lasku_id = @LaskuId", conn);
                cmd.Parameters.AddWithValue("@LaskuId", laskuId);

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lasku = new Lasku
                    {
                        ID = dr.GetInt32("lasku_id"),
                        AsiakasNimi = dr.GetString("asiakas_nimi"),
                        Lahiosoite = dr.GetString("lahiosoite"),
                        Postitoimipaikka = dr.GetString("postitoimipaikka"),
                        VarattuPvm = dr.GetDateTime("varattu_pvm"),
                        ToimipisteenNimi = dr.GetString("toimipisteen_nimi"),
                        ToimipisteenPaivahinta = dr.GetDouble("huoneen_hinta"),
                        Summa = dr.GetDouble("summa"),
                        ALV = dr.GetDouble("alv"),
                        VarauksenAlkuPvm = dr.GetDateTime("aloitus_pvm"),
                        VarauksenLoppuPvm = dr.GetDateTime("loppu_pvm")
                    };

                    // Hae palvelut erillisellä komennolla
                    lasku.Palvelut = GetPalvelutForLasku(laskuId);
                }
            }
            return lasku;
        }
        //Hae palveluiden tietoja LaskutiedotWin2 varteen
        private List<Palvelu> GetPalvelutForLasku(int laskuId)
        {
            List<Palvelu> palvelut = new List<Palvelu>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT palvelu.nimi, palvelu.hinta, palvelu.alv " +
                                                    "FROM palvelu " +
                                                    "INNER JOIN varauksen_palvelut ON palvelu.palvelu_id = varauksen_palvelut.palvelu_id " +
                                                    "INNER JOIN varaus ON varauksen_palvelut.varaus_id = varaus.varaus_id " +
                                                    "INNER JOIN lasku ON varaus.varaus_id = lasku.varaus_id " +
                                                    "WHERE lasku.lasku_id = @LaskuId", conn);
                cmd.Parameters.AddWithValue("@LaskuId", laskuId);

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    palvelut.Add(new Palvelu
                    {
                        Nimi = dr.GetString("nimi"),
                        Hinta = dr.GetDouble("hinta"),
                        ALV = dr.GetDouble("alv")
                    });
                }
            }
            return palvelut;
        }
        //Poistaa laskun tietokannasta
        public void PoistaLasku(int laskuId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM lasku WHERE lasku_id = @LaskuId", conn);
                cmd.Parameters.AddWithValue("@LaskuId", laskuId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
