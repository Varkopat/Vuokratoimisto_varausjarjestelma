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

### TODO:
Mahdollisuus tehdä paperi- ja sähköpostilaskut.

## Käyttöohjeet

1. Luo ensin tietokanta esim. HeidiSQL-ohjelmalla. Tietokannan luomiseen tarvittavan SQL-skriptin löydät DB-kansiosta. Lisäksi sinulla tulee olla Visual Studiossa asennettuna MySqlConnector-niminen nuget-pakkaus, jotta voit sovelluksessa luoda tietokantayhteyden.

2. Anna seuraavaksi HeidiSQL:n asetuksissa täydet oikeudet opiskelija-nimiselle käyttäjälle. Tämä tapahtuu seuraavasti:

Työkalut -> Käyttäjähallinta -> Lisää -> Allow access to -kentässä annetaan kaikki oikeudet Global privileges -valinnassa.
