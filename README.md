# Toimistotilojen varausjärjestelmä

Toimistotilojen varausjärjestelmä fiktiiviselle yritykselle nimeltä Vuokratoimistot Oy.

## Ominaisuudet

- toimipisteiden hallinta
- palveluiden hallinta
- toimistotilavarausten hallinta
- asiakashallintajärjestelmä
- laskujen hallinta ja seuranta
- vuokrattujen tilojen raportointi aikajaksolla valituissa toimipisteissä
- ostettujen lisäpalvelujen ja vuokrattujen laitteiden raportointi aikajaksolla valituissa toimipisteissä
- laskujen lähettäminen sähköpostitse ja laskujen tulostus

## Käyttöohjeet

### 1. Tietokannan asennus

Luo ensin tietokanta esim. HeidiSQL-ohjelmalla. Tietokannan luomiseen tarvittavan SQL-skriptin löydät DB-kansiosta. Lisäksi sinulla tulee olla Visual Studiossa asennettuna MySqlConnector-niminen nuget-pakkaus, jotta voit sovelluksessa luoda tietokantayhteyden.

Anna seuraavaksi HeidiSQL:n asetuksissa täydet oikeudet opiskelija-nimiselle käyttäjälle. Tämä tapahtuu seuraavasti:

Työkalut -> Käyttäjähallinta -> Lisää -> Tee uusi käyttäjä nimeltä opiskelija -> Allow access to -kentässä annetaan kaikki oikeudet Global privileges -valinnassa.

### 2. Sähköpostilaskun ominaisuuden konfigurointi

Sähköpostilaskujen lähettämiseksi sinun tulee määrittää SMTP-asetukset `RaportitWin2.xaml.cs`-tiedostossa:

#### Vaihe 1: Gmail-tilin valmisteleminen
1. Avaa Gmail-tilisi
2. Siirry **Tietosuoja ja turvallisuus** -osioon
3. Ota käyttöön **Kahden vaiheen vahvistus** (jos ei vielä käytössä)
4. Luo **sovelluskohtainen salasana** (App password):
   - Valitse **Sovellukset ja laitteet** -> **Sovellukselle määritetyt salasanat**
   - Valitse sovellukseksi "Mail" ja laitteeksi "Windows-tietokone"
   - Kopioi luodut 16 merkkiä pitkä salasana

#### Vaihe 2: Koodin päivitys
Avaa tiedosto `Windows/RaportitWin2.xaml.cs` ja korvaa seuraavat vakiot:

```csharp
private const string SenderEmail = "your-email@gmail.com"; // ← Korvaa Gmail-osoitteella
private const string SenderPassword = "your-app-password"; // ← Korvaa sovelluskohtaisella salasanalla
```

#### Vaihe 3: Asiakkaiden sähköpostiosoitteiden varmistaminen
- Varmista, että kaikilla asiakkailla on **Email**-kenttään tallennettu sähköpostiosoite
- Ilman sähköpostiosoitetta laskua ei voi lähettää

### 3. Laskujen lähettäminen

1. Avaa lasku raportit-näkymästä
2. Napsauta **Sähköpostilasku**-nappia
3. Sovellus lähettää HTML-muotoisen laskun asiakkaan sähköpostiosoitteeseen
4. Virheilmoitus näkyy, jos sähköpostiosoite puuttuu tai lähettäminen epäonnistuu

## Turvallisuus

⚠️ **Huomio**: Älä jaa sovelluskohtaista salasanaasi tai käytä omaa Gmail-salasanaasi koodissa. Käytä aina Gmail-sovellukselle luotua erillistä salasanaa.
