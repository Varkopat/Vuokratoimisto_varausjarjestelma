using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Vuokratoimisto_projekti.Classes
{
    public class TietokantaToiminnot
    {
		private readonly string connectionString;

        public TietokantaToiminnot()
        {
			// Haetaan connectionString asetus App.config tiedostosta
			connectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			// Salataan app.config
			if (!config.ConnectionStrings.SectionInformation.IsProtected)
			{
				config.ConnectionStrings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
				config.Save();
			}
		}

		/// <summary>
		/// Hakee toimipisteet tietokannasta
		/// </summary>
		/// <returns>Toimipiste-tyyppinen observablecollection</returns>
		public ObservableCollection<Toimipiste> HaeToimipisteet()
        {
            var toimipisteet = new ObservableCollection<Toimipiste>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM toimipiste", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    toimipisteet.Add(new Toimipiste
                    {
                        ID = dr.GetInt32("toimipiste_id"),
                        Nimi = dr.GetString("nimi"),
                        Lahiosoite = dr.GetString("lahiosoite"),
                        Postitoimipaikka = dr.GetString("postitoimipaikka"),
                        Postinro = dr.GetString("postinro"),
                        Email = dr.GetString("email"),
                        Puhelinnro = dr.GetString("puhelinnro")
                    });
                }
            }
            return toimipisteet;
        }


        public ObservableCollection<Toimipiste> HaeToimipisteetPerKaupunki(string kaupunki)
        {
            var toimipisteet = new ObservableCollection<Toimipiste>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM toimipiste WHERE postitoimipaikka = @kaupunki", conn);
                cmd.Parameters.AddWithValue("@kaupunki", kaupunki);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    toimipisteet.Add(new Toimipiste
                    {
                        ID = dr.GetInt32("toimipiste_id"),
                        Nimi = dr.GetString("nimi"),
                        Lahiosoite = dr.GetString("lahiosoite"),
                        Postitoimipaikka = dr.GetString("postitoimipaikka"),
                        Postinro = dr.GetString("postinro"),
                        Email = dr.GetString("email"),
                        Puhelinnro = dr.GetString("puhelinnro")
                    });
                }
            }
            return toimipisteet;
        }
        /// <summary>
        /// Toimipisteen poistaminen tietokannasta
        /// </summary>
        /// <param name="toimipiste"></param>
        public void PoistaToimipiste(Toimipiste toimipiste)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM toimipiste WHERE toimipiste_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", toimipiste.ID);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Uuden toimipisteen lisääminen tietokantaan
        /// </summary>
        /// <param name="toimipiste"></param>
        public void LisaaToimipiste(Toimipiste toimipiste)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO toimipiste(nimi, lahiosoite, postitoimipaikka, postinro, email, puhelinnro) " +
                    "VALUES(@nimi,@osoite, @postitoimip, @postinro, @email, @nro)", conn);

                cmd.Parameters.AddWithValue("@nimi", toimipiste.Nimi);
                cmd.Parameters.AddWithValue("@osoite", toimipiste.Lahiosoite);
                cmd.Parameters.AddWithValue("@postitoimip", toimipiste.Postitoimipaikka);
                cmd.Parameters.AddWithValue("@postinro", toimipiste.Postinro);
                cmd.Parameters.AddWithValue("@email", toimipiste.Email);
                cmd.Parameters.AddWithValue("@nro", toimipiste.Puhelinnro);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Toimipisteen tietojen päivitys tietokantaan
        /// </summary>
        /// <param name="toimipiste"></param>
        public void PaivitaToimipiste(Toimipiste toimipiste, ObservableCollection<Huone> huoneet)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE toimipiste SET nimi=@nimi, lahiosoite=@osoite, postitoimipaikka=@postitoimip, " +
                    "postinro=@postinro, email=@email, puhelinnro=@nro WHERE toimipiste_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", toimipiste.ID);
                cmd.Parameters.AddWithValue("@nimi", toimipiste.Nimi);
                cmd.Parameters.AddWithValue("@osoite", toimipiste.Lahiosoite);
                cmd.Parameters.AddWithValue("@postitoimip", toimipiste.Postitoimipaikka);
                cmd.Parameters.AddWithValue("@postinro", toimipiste.Postinro);
                cmd.Parameters.AddWithValue("@email", toimipiste.Email);
                cmd.Parameters.AddWithValue("@nro", toimipiste.Puhelinnro);

                cmd.ExecuteNonQuery();


                foreach (var huone in huoneet)
                {

                    if (huone.HuoneID == 0)
                    {//lisätään uusi huone
                        MySqlCommand cmdIns = new MySqlCommand("INSERT INTO huone (huone_id, nimi, toimipiste_id, kapasiteetti, hinta) " +
                            "VALUES (@id, @nimi, @toimipid, @kapasit, @hinta)", conn);
                        cmdIns.Parameters.AddWithValue("@id", huone.HuoneID);
                        cmdIns.Parameters.AddWithValue("@nimi", huone.Nimi);
                        cmdIns.Parameters.AddWithValue("@toimipid", huone.ToimipisteID);
                        cmdIns.Parameters.AddWithValue("@kapasit", huone.Kapasiteetti);
                        cmdIns.Parameters.AddWithValue("@hinta", huone.Hinta);
                        cmdIns.ExecuteNonQuery();

                    }
                    else
                    {//päivitetään olemassa olevaa huonetta
                        MySqlCommand cmdUpd = new MySqlCommand("UPDATE huone SET nimi=@nimi, toimipiste_id=@toimipid, " +
                            "kapasiteetti=@kapasit, hinta=@hinta WHERE huone_id=@id", conn);
                        cmdUpd.Parameters.AddWithValue("@nimi", huone.Nimi);
                        cmdUpd.Parameters.AddWithValue("@toimipid", huone.ToimipisteID);
                        cmdUpd.Parameters.AddWithValue("@kapasit", huone.Kapasiteetti);
                        cmdUpd.Parameters.AddWithValue("@hinta", huone.Hinta);
                        cmdUpd.Parameters.AddWithValue("@id", huone.HuoneID);
                        cmdUpd.ExecuteNonQuery();
                    }
                }
            }
        }
        /// <summary>
        /// Hakee toimipisteen huoneet tietokannasta
        /// </summary>
        /// <param name="toimipiste"></param>
        /// <returns>huone- tyyppinen ObservableCollection</returns>
        public ObservableCollection<Huone> HaeToimipisteenHuoneet(Toimipiste toimipiste)
        {
            var huoneet = new ObservableCollection<Huone>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM huone WHERE toimipiste_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", toimipiste.ID);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    huoneet.Add(new Huone
                    {
                        HuoneID = dr.GetInt32("huone_id"),
                        Nimi = dr.GetString("nimi"),
                        Hinta = dr.GetDouble("hinta"),
                        Kapasiteetti = dr.GetString("kapasiteetti"),
                        ToimipisteID = dr.GetInt32("toimipiste_id")
                    });
                }
            }
            return huoneet;
        }



        public ObservableCollection<Palvelu> HaeHuoneenPalvelut(Huone huone)
        {
            var palvelut = new ObservableCollection<Palvelu>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM varausjarjestelma.palvelu JOIN varausjarjestelma.huone ON varausjarjestelma.palvelu.huone_id = varausjarjestelma.huone.huone_id WHERE varausjarjestelma.huone.huone_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", huone.HuoneID);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    palvelut.Add(new Palvelu
                    {
                        PalveluID = dr.GetInt32("palvelu_id"),
                        HuoneID = dr.GetInt32("huone_id"),
                        Nimi = dr.GetString("nimi"),
                        Tyyppi = dr.GetInt32("tyyppi"),
                        Kuvaus = dr.GetString("kuvaus"),
                        Hinta = dr.GetDouble("hinta"),
                        ALV = dr.GetDouble("alv"),
                    });
                }
            }
            return palvelut;
        }

        public void LisaaAsiakas(Asiakas asiakas)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO asiakas (Nimi, Lahiosoite, Postitoimipaikka, Postinro, Email, Puhelinnro) " +
                    "VALUES (@Nimi, @Lahiosoite, @Postitoimipaikka, @Postinro, @Email, @Puhelinnro)", conn);

                cmd.Parameters.AddWithValue("@Nimi", asiakas.Nimi);
                cmd.Parameters.AddWithValue("@Lahiosoite", asiakas.Lahiosoite);
                cmd.Parameters.AddWithValue("@Postitoimipaikka", asiakas.Postitoimipaikka);
                cmd.Parameters.AddWithValue("@Postinro", asiakas.Postinro);
                cmd.Parameters.AddWithValue("@Email", asiakas.Email);
                cmd.Parameters.AddWithValue("@Puhelinnro", asiakas.Puhelinnro);

                cmd.ExecuteNonQuery();
            }
        }

        //Metodi asiakkaan ID:n noutamista varten tietokannasta
        public int GetCustomerId(Asiakas asiakas)
        {
            int id = 0;
            string query = "SELECT asiakas_id FROM asiakas WHERE nimi = @Nimi";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nimi", asiakas.Nimi);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    id = Convert.ToInt32(result);
                }
            }

            return id;
        }

        public void LisaaVaraus(Varaus varaus)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO varaus (varaus_id, asiakas_id, huone_id, vahvistus_pvm, varattu_pvm, aloitus_pvm, loppu_pvm) " +
                    "VALUES (@ID, @asiakas_id, @huone_id, @Vahvistuspvm, @Varauspvm, @Aloituspvm, @Paattymispvm)", conn);

                cmd.Parameters.AddWithValue("@ID", varaus.ID);
                cmd.Parameters.AddWithValue("@asiakas_id", varaus.asiakas_id);
                cmd.Parameters.AddWithValue("@huone_id", varaus.huone_id);
                cmd.Parameters.AddWithValue("@Vahvistuspvm", varaus.Vahvistuspvm);
                cmd.Parameters.AddWithValue("@Varauspvm", varaus.Varauspvm);
                cmd.Parameters.AddWithValue("@Aloituspvm", varaus.AloitusPvm);
                cmd.Parameters.AddWithValue("@Paattymispvm", varaus.Paattymispvm);

                cmd.ExecuteNonQuery();
            }
        }



        public ObservableCollection<Palvelu> HaeKaikkiPalvelut()
        {
            var palvelut = new ObservableCollection<Palvelu>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM palvelu", conn);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    palvelut.Add(new Palvelu
                    {
                        PalveluID = dr.GetInt32("palvelu_id"),
                        HuoneID = dr.GetInt32("huone_id"),
                        Nimi = dr.GetString("nimi"),
                        Tyyppi = dr.GetInt32("tyyppi"),
                        Kuvaus = dr.GetString("kuvaus"),
                        Hinta = dr.GetDouble("hinta"),
                        ALV = dr.GetDouble("alv")
                    });
                }
            }
            return palvelut;
        }


        public void PaivitaPalvelu(Palvelu palvelu)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE palvelu SET huone_id=@huoneID, nimi=@nimi, tyyppi=@tyyppi, kuvaus=@kuvaus, hinta=@hinta, alv=@alv WHERE palvelu_id=@palveluID", conn);
                cmd.Parameters.AddWithValue("@palveluID", palvelu.PalveluID);
                cmd.Parameters.AddWithValue("@huoneID", palvelu.HuoneID);
                cmd.Parameters.AddWithValue("@nimi", palvelu.Nimi);
                cmd.Parameters.AddWithValue("@tyyppi", palvelu.Tyyppi);
                cmd.Parameters.AddWithValue("@kuvaus", palvelu.Kuvaus);
                cmd.Parameters.AddWithValue("@hinta", palvelu.Hinta);
                cmd.Parameters.AddWithValue("@alv", palvelu.ALV);
                cmd.ExecuteNonQuery();
            }
        }

        public void LisaaPalvelu(Palvelu palvelu)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO palvelu (huone_id, nimi, tyyppi, kuvaus, hinta, alv) VALUES (@huoneID, @nimi, @tyyppi, @kuvaus, @hinta, @alv)", conn);
                cmd.Parameters.AddWithValue("@huoneID", palvelu.HuoneID);
                cmd.Parameters.AddWithValue("@nimi", palvelu.Nimi);
                cmd.Parameters.AddWithValue("@tyyppi", palvelu.Tyyppi);
                cmd.Parameters.AddWithValue("@kuvaus", palvelu.Kuvaus);
                cmd.Parameters.AddWithValue("@hinta", palvelu.Hinta);
                cmd.Parameters.AddWithValue("@alv", palvelu.ALV);
                cmd.ExecuteNonQuery();
            }
        }


        public ObservableCollection<Huone> HaeKaikkiHuoneet()
        {
            var huoneet = new ObservableCollection<Huone>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM huone", conn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    huoneet.Add(new Huone
                    {
                        HuoneID = dr.GetInt32("huone_id"),
                        Nimi = dr.GetString("nimi"),
                        Hinta = dr.GetDouble("hinta"),
                        Kapasiteetti = dr.GetString("kapasiteetti"),
                        ToimipisteID = dr.GetInt32("toimipiste_id")
                    });
                }
            }
            return huoneet;
        }

        public ObservableCollection<ToimipisteenHuone> HaeToimipisteetJaHuoneet()
        {
            var toimipisteetJaHuoneet = new ObservableCollection<ToimipisteenHuone>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT huone_id, postitoimipaikka, toimipisteen_nimi, huoneen_nimi, kapasiteetti FROM ToimipisteetJaHuoneet", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    toimipisteetJaHuoneet.Add(new ToimipisteenHuone
                    {
                        HuoneID = dr.GetInt32("huone_id"),
                        Postitoimipaikka = dr.GetString("postitoimipaikka"),
                        ToimipisteenNimi = dr.GetString("toimipisteen_nimi"),
                        HuoneenNimi = dr.GetString("huoneen_nimi"),
                        Kapasiteetti = dr.GetString("kapasiteetti")
                    });
                }

            }
            return toimipisteetJaHuoneet;

        }

        public void PoistaPalvelu(Palvelu palvelu)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM palvelu WHERE palvelu_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", palvelu.PalveluID);
                cmd.ExecuteNonQuery();  
            }
        }

        public void LisaaLasku(Lasku lasku)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `lasku` (`lasku_id`, `varaus_id`, " +
                    "`asiakas_id`, `nimi`, `lahiosoite`, `postitoimipaikka`, `postinro`, `summa`, `alv`) " +
                    "VALUES (@ID, @VarausId, @AsiakasId, @AsiakasNimi, @Lahiosoite, " +
                    "@Postitoimipaikka, @Postinro, @Lasku.Summa, @Lasku.ALV)", conn);

                cmd.Parameters.AddWithValue("@ID", lasku.ID);
                cmd.Parameters.AddWithValue("@VarausId", lasku.VarausId);
                cmd.Parameters.AddWithValue("@AsiakasId", lasku.AsiakasId);
                cmd.Parameters.AddWithValue("@AsiakasNimi", lasku.AsiakasNimi);
                cmd.Parameters.AddWithValue("@Lahiosoite", lasku.Lahiosoite);
                cmd.Parameters.AddWithValue("@Postitoimipaikka", lasku.Postitoimipaikka);
                cmd.Parameters.AddWithValue("@Postinro", lasku.Postinro);
                cmd.Parameters.AddWithValue("@Lasku.Summa", lasku.Summa);
                cmd.Parameters.AddWithValue("@Lasku.ALV", lasku.ALV);
                cmd.ExecuteNonQuery();


            }
        }
    }
}