using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Vuokratoimisto_projekti.Classes;

namespace Vuokratoimisto_projekti.Windows
{
    /// <summary>
    /// Interaction logic for RaportitWin2.xaml
    /// </summary>
    public partial class RaportitWin2 : Window
    {
        private Lasku lasku;
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SenderEmail = "your-email@gmail.com"; // Vaihda oikeaksi
        private const string SenderPassword = "your-app-password"; // Vaihda oikeaksi

        public RaportitWin2(Lasku kokoLasku)
        {
            InitializeComponent();
            
            if (kokoLasku == null)
            {
                MessageBox.Show("Laskun tiedot ovat puutteelliset.");
                this.Close();
                return;
            }

            lasku = kokoLasku;
            DataContext = lasku;
        }

        private void TakaisinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TulostaBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Lasku");
            }
        }

        private void VieAsiakasTietoihinBtn_Click(object sender, RoutedEventArgs e)
        {
            // Avaa asiakastietojen muokkaus-ikkuna
            MessageBox.Show($"Asiakkaan {lasku.AsiakasNimi} tietojen avaaminen.");
        }

        private void LaheLaskunSahkopostilla_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lasku.Asiakas?.Email))
            {
                MessageBox.Show("Asiakkaan sähköpostiosoite ei ole tallennettu.", "Virhe", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LaheSahkopostiLasku();
        }

        private void LaheSahkopostiLasku()
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(SmtpServer, SmtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                    smtpClient.Timeout = 10000;

                    using (MailMessage mailMessage = new MailMessage(SenderEmail, lasku.Asiakas.Email))
                    {
                        mailMessage.Subject = $"Lasku {lasku.ID} - {lasku.AsiakasNimi}";
                        mailMessage.Body = GenerateLaskunSisalto();
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                        MessageBox.Show("Lasku lähetetty sähköpostitse onnistuneesti!", "Onnistui", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (SmtpException ex)
            {
                MessageBox.Show($"Sähköpostin lähettäminen epäonnistui: {ex.Message}", "Virhe", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Virhe tapahtui: {ex.Message}", "Virhe", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateLaskunSisalto()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset=\"UTF-8\">");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: Arial, sans-serif; color: #333; }");
            html.AppendLine("table { border-collapse: collapse; width: 100%; margin: 15px 0; }");
            html.AppendLine("th, td { border: 1px solid #ddd; padding: 12px; text-align: left; }");
            html.AppendLine("th { background-color: #4CAF50; color: white; }");
            html.AppendLine(".section-header { background-color: #f9f9f9; font-weight: bold; margin-top: 20px; }");
            html.AppendLine(".total-row { background-color: #e8f5e9; font-weight: bold; }");
            html.AppendLine("h1 { color: #2c3e50; }");
            html.AppendLine(".footer { margin-top: 30px; padding-top: 15px; border-top: 1px solid #ddd; font-size: 12px; color: #666; }");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");

            html.AppendLine($"<h1>LASKU {lasku.ID}</h1>");

            // Laskun perustiedot
            html.AppendLine("<div class='section-header'>Laskun Perustiedot</div>");
            html.AppendLine("<table>");
            html.AppendLine($"<tr><th>Laskun ID</th><td>{lasku.ID}</td></tr>");
            html.AppendLine($"<tr><th>Luontipäivä</th><td>{lasku.VarattuPvm:dd.MM.yyyy}</td></tr>");
            html.AppendLine($"<tr><th>Eräpäivä</th><td>{lasku.DueDate:dd.MM.yyyy}</td></tr>");
            html.AppendLine($"<tr><th>Laskutustapa</th><td>{lasku.LaskutusTapa}</td></tr>");
            html.AppendLine("</table>");

            // Asiakastiedot
            html.AppendLine("<div class='section-header'>Asiakastiedot</div>");
            html.AppendLine("<table>");
            html.AppendLine($"<tr><th>Nimi</th><td>{lasku.AsiakasNimi}</td></tr>");
            html.AppendLine($"<tr><th>Osoite</th><td>{lasku.Lahiosoite}</td></tr>");
            html.AppendLine($"<tr><th>Postinumero</th><td>{lasku.Postinro}</td></tr>");
            html.AppendLine($"<tr><th>Postitoimipaikka</th><td>{lasku.Postitoimipaikka}</td></tr>");
            html.AppendLine("</table>");

            // Varauksen tiedot
            html.AppendLine("<div class='section-header'>Varauksen Tiedot</div>");
            html.AppendLine("<table>");
            html.AppendLine($"<tr><th>Toimipiste</th><td>{lasku.ToimipisteenNimi}</td></tr>");
            html.AppendLine($"<tr><th>Varauksen alku</th><td>{lasku.VarauksenAlkuPvm:dd.MM.yyyy}</td></tr>");
            html.AppendLine($"<tr><th>Varauksen loppu</th><td>{lasku.VarauksenLoppuPvm:dd.MM.yyyy}</td></tr>");
            html.AppendLine($"<tr><th>Päivähinta</th><td>{lasku.ToimipisteenPaivahinta:F2} €</td></tr>");
            html.AppendLine("</table>");

            // Palvelut
            if (lasku.Palvelut != null && lasku.Palvelut.Count > 0)
            {
                html.AppendLine("<div class='section-header'>Tilatut Palvelut</div>");
                html.AppendLine("<table>");
                html.AppendLine("<tr><th>Palvelu</th><th>Hinta</th></tr>");
                foreach (var palvelu in lasku.Palvelut)
                {
                    html.AppendLine($"<tr><td>{palvelu.Nimi}</td><td>{palvelu.Hinta:F2} €</td></tr>");
                }
                html.AppendLine("</table>");
            }

            // Yhteenveto
            html.AppendLine("<div class='section-header'>Yhteenveto</div>");
            html.AppendLine("<table>");
            html.AppendLine($"<tr><th>Summa (ilman ALV)</th><td>{lasku.Summa:F2} €</td></tr>");
            html.AppendLine($"<tr><th>ALV ({lasku.ALV:F1}%)</th><td>{(lasku.Summa * lasku.ALV / 100):F2} €</td></tr>");
            html.AppendLine($"<tr class='total-row'><th>Yhteensä</th><td>{(lasku.Summa + lasku.Summa * lasku.ALV / 100):F2} €</td></tr>");
            html.AppendLine("</table>");

            // Alateksti
            html.AppendLine("<div class='footer'>");
            html.AppendLine("<p>Tämä on automaattisesti luotu lasku. Mikäli sinulla on kysymyksiä, ole hyvä ja ota yhteyttä asiakaspalveluumme.</p>");
            html.AppendLine($"<p>Lähetetty: {DateTime.Now:dd.MM.yyyy HH:mm:ss}</p>");
            html.AppendLine("</div>");

            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }
    }
}
