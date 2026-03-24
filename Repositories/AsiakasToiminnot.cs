using MySqlConnector;
using System.Collections.ObjectModel;

namespace Vuokratoimisto_projekti.Classes
{
    public class AsiakasToiminnot
    {
        private const string localWithDb = "Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=varausjarjestelma;";

        /// <summary>
        /// Hakee asiakkaat tietokannasta
        /// </summary>
        /// <returns>asiakas kokoelma</returns>
        public ObservableCollection<Asiakas> HaeAsiakkaat()
        {
            var asiakkaat = new ObservableCollection<Asiakas>();
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM asiakas", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    asiakkaat.Add(new Asiakas
                    {
                        ID = dr.GetInt32("asiakas_id"),
                        Nimi = dr.GetString("nimi"),
                        Lahiosoite = dr.GetString("lahiosoite"),
                        Postitoimipaikka = dr.GetString("postitoimipaikka"),
                        Postinro=dr.GetString("postinro"),
                        Email=dr.GetString("email"),
                        Puhelinnro=dr.GetString("puhelinnro")
                    });
                }
            }
            return asiakkaat;
        }

        public void PoistaAsiakas(Asiakas asiakas)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM asiakas WHERE asiakas_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", asiakas.ID);

                cmd.ExecuteNonQuery();
            }
        }

        public void LisaaAsiakas(Asiakas asiakas)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO asiakas(nimi, lahiosoite, postitoimipaikka, postinro, email, puhelinnro) " +
                    "VALUES(@nimi,@osoite, @postitoimip, @postinro, @email, @nro)", conn);

                cmd.Parameters.AddWithValue("@nimi", asiakas.Nimi);
                cmd.Parameters.AddWithValue("@osoite", asiakas.Lahiosoite);
                cmd.Parameters.AddWithValue("@postitoimip", asiakas.Postitoimipaikka);
                cmd.Parameters.AddWithValue("@postinro", asiakas.Postinro);
                cmd.Parameters.AddWithValue("@email", asiakas.Email);
                cmd.Parameters.AddWithValue("@nro", asiakas.Puhelinnro);
                cmd.ExecuteNonQuery();

            }

        }
        public void MuutaAsiakas(Asiakas asiakas)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE asiakas SET nimi=@nimi, lahiosoite=@osoite, postitoimipaikka=@postitoimip, " +
                    "postinro=@postinro, email=@email, puhelinnro=@nro WHERE asiakas_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", asiakas.ID);
                cmd.Parameters.AddWithValue("@nimi", asiakas.Nimi);
                cmd.Parameters.AddWithValue("@osoite", asiakas.Lahiosoite);
                cmd.Parameters.AddWithValue("@postitoimip", asiakas.Postitoimipaikka);
                cmd.Parameters.AddWithValue("@postinro", asiakas.Postinro);
                cmd.Parameters.AddWithValue("@email", asiakas.Email);
                cmd.Parameters.AddWithValue("@nro", asiakas.Puhelinnro);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
