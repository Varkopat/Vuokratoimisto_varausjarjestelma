-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               11.2.2-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.3.0.6589
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for varausjarjestelma
CREATE DATABASE IF NOT EXISTS `varausjarjestelma` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci */;
USE `varausjarjestelma`;

-- Dumping structure for table varausjarjestelma.asiakas
CREATE TABLE IF NOT EXISTS `asiakas` (
  `asiakas_id` int(11) NOT NULL AUTO_INCREMENT,
  `nimi` varchar(40) NOT NULL,
  `lahiosoite` varchar(40) NOT NULL,
  `postitoimipaikka` varchar(30) NOT NULL,
  `postinro` char(5) NOT NULL,
  `email` varchar(50) NOT NULL,
  `puhelinnro` varchar(15) NOT NULL,
  PRIMARY KEY (`asiakas_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table varausjarjestelma.asiakas: ~7 rows (approximately)
INSERT INTO `asiakas` (`asiakas_id`, `nimi`, `lahiosoite`, `postitoimipaikka`, `postinro`, `email`, `puhelinnro`) VALUES
	(1, 'Liisa Virtanen', 'Mäkikatu 2', 'Turku', '20100', 'liisa.virtanen@.com', '0501234567'),
	(2, 'Antti Järvinen', 'Sarankatu 3', 'Tampere', '33100', 'antti.jarvinen@.com', '0401112233'),
	(3, 'Anna Korhonen', 'Ellintie 4', 'Oulu', '90100', 'anna.korhonen@.com', '0407654321'),
	(4, 'Jari Kinnunen', 'Tiirantie 5', 'Jyväskylä', '40100', 'jari.kinnunen@.com', '0407654321'),
	(5, 'Kalle Mäkinen', 'Selmankatu 1', 'Helsinki', '00100', 'kalle.makinen@.com', '0501234567'),
	(6, 'Delfi Oy', 'Liikekatu 2', 'Tampere', '33100', 'info@delfi.fi', '0409876543'),
	(7, 'VALO ry', 'Keskuskatu 3', 'Turku', '20100', 'info@valo.fi', '0452468135');

	-- Dumping structure for table varausjarjestelma.toimipiste
CREATE TABLE IF NOT EXISTS `toimipiste` (
  `toimipiste_id` int(11) NOT NULL AUTO_INCREMENT,
  `nimi` varchar(40) NOT NULL,
  `lahiosoite` varchar(40) NOT NULL,
  `postitoimipaikka` varchar(30) NOT NULL,
  `postinro` char(5) NOT NULL,
  `email` varchar(50) NOT NULL,
  `puhelinnro` varchar(15) NOT NULL,
  PRIMARY KEY (`toimipiste_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table varausjarjestelma.toimipiste: ~8 rows (approximately)
INSERT INTO `toimipiste` (`toimipiste_id`, `nimi`, `lahiosoite`, `postitoimipaikka`, `postinro`, `email`, `puhelinnro`) VALUES
	(1, 'Anyspace Pasila', 'Uutiskatu 2', 'Helsinki', '00240', 'info@anyspace.fi', '+358 12 345 678'),
	(2, 'Business Park Plaza', 'Äyritie 16', 'Vantaa', '01510', 'info@businessparkplaza.fi', '+358 23 456 789'),
	(3, 'United Spaces', 'Siltasaarenkatu 16', 'Jyväskylä', '40100', 'info@unitedspaces.fi', '+358 45 678 901'),
	(4, 'Studio42', 'Kauppakatu 23', 'Joensuu', '80100', 'info@studio42.fi', '+358 67 890 123'),
	(5, 'SmartOffice', 'Kauppakatu 5', 'Lappeenranta', '53100', 'info@smartoffice.fi', '+358 89 012 345'),
	(6, 'Business Tilat Oy', 'Henrikinkatu 2', 'Turku', '20100', ' businesstilat @.fi', '+358 87 654 321'),
	(7, 'Workspaces Oy', 'Ilmarinkatu 3', 'Tampere', '33100', 'workspaces @.fi', '+358 56 789 123'),
	(8, 'FirstOffice', 'Keilaniementie 1', 'Espoo', '02150', 'info@firstoffice.fi', '+358 34 567 890');


-- Dumping structure for table varausjarjestelma.huone
CREATE TABLE IF NOT EXISTS `huone` (
  `huone_id` int(11) NOT NULL AUTO_INCREMENT,
  `nimi` varchar(100) NOT NULL,
  `toimipiste_id` int(11) NOT NULL,
  `kapasiteetti` varchar(15) NOT NULL,
  `hinta` double(8,2) NOT NULL,
  PRIMARY KEY (`huone_id`),
  KEY `toimipiste_id` (`toimipiste_id`),
  CONSTRAINT `huone_ibfk_1` FOREIGN KEY (`toimipiste_id`) REFERENCES `toimipiste` (`toimipiste_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table varausjarjestelma.huone: ~14 rows (approximately)
INSERT INTO `huone` (`huone_id`, `nimi`, `toimipiste_id`, `kapasiteetti`, `hinta`) VALUES
	(1, 'Huone 1', 1, '4', 50.00),
	(2, 'Huone 2', 1, '8', 90.00),
	(3, 'Huone A/4', 2, '4', 45.00),
	(4, 'Huone B/8', 2, '8', 85.00),
	(5, 'Huone 1/4', 3, '4', 40.00),
	(6, 'Huone 1/4', 4, '4', 35.00),
	(7, 'Huone 2/8', 4, '8', 60.00),
	(8, 'Huone 1/4', 5, '4', 45.00),
	(9, 'Huone 1/4', 6, '4', 45.00),
	(10, 'Huone 1/2', 7, '2', 30.00),
	(11, 'Huone 2/4', 7, '4', 45.00),
	(12, 'Huone 1/2', 8, '2', 30.00),
	(13, 'Huone 2/4', 8, '4', 60.00),
	(14, 'Huone 3/8', 8, '8', 90.00);

-- View
CREATE VIEW `toimipisteetjahuoneet` AS SELECT h.huone_id,
       t.postitoimipaikka AS postitoimipaikka,
		 t.nimi AS toimipisteen_nimi, 
       h.nimi AS huoneen_nimi, 
       h.kapasiteetti
FROM Toimipiste t 
JOIN Huone h ON t.toimipiste_id = h.toimipiste_id;

-- Dumping structure for table varausjarjestelma.palvelu
CREATE TABLE IF NOT EXISTS `palvelu` (
  `palvelu_id` int(11) NOT NULL AUTO_INCREMENT,
  `huone_id` int(11) NOT NULL,
  `nimi` varchar(40) NOT NULL,
  `tyyppi` int(11) NOT NULL,
  `kuvaus` varchar(255) NOT NULL,
  `hinta` double(8,2) NOT NULL,
  `alv` double(8,2) NOT NULL,
  PRIMARY KEY (`palvelu_id`),
  KEY `huone_id` (`huone_id`),
  CONSTRAINT `palvelu_ibfk_1` FOREIGN KEY (`huone_id`) REFERENCES `huone` (`huone_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table varausjarjestelma.palvelu: ~27 rows (approximately)
INSERT INTO `palvelu` (`palvelu_id`, `huone_id`, `nimi`, `tyyppi`, `kuvaus`, `hinta`, `alv`) VALUES
	(1, 2, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 20.00, 24.00),
	(2, 4, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 20.00, 24.00),
	(3, 7, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 20.00, 24.00),
	(4, 5, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 10.00, 24.00),
	(5, 8, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 10.00, 24.00),
	(6, 9, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 10.00, 24.00),
	(7, 10, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 10.00, 24.00),
	(8, 11, 'Siivouspalvelut', 1, 'Ammattimaisen siivouspalvelun tarjoaminen toimistolle', 10.00, 24.00),
	(9, 2, 'Kokoustilapalvelut', 1, 'Kokousten ja konferenssien järjestämiseen tarvittavien palveluiden tarjoaminen', 40.00, 24.00),
	(10, 4, 'Kokoustilapalvelut', 1, 'Kokousten ja konferenssien järjestämiseen tarvittavien palveluiden tarjoaminen', 30.00, 24.00),
	(11, 7, 'Kokoustilapalvelut', 1, 'Kokousten ja konferenssien järjestämiseen tarvittavien palveluiden tarjoaminen', 30.00, 24.00),
	(12, 2, 'Tietoliikenneyhteydet', 1, 'Pääsy nopeisiin verkkoyhteyksiin', 10.00, 24.00),
	(13, 4, 'Tietoliikenneyhteydet', 1, 'Pääsy nopeisiin verkkoyhteyksiin', 10.00, 24.00),
	(14, 6, 'Tietoliikenneyhteydet', 1, 'Pääsy nopeisiin verkkoyhteyksiin', 10.00, 24.00),
	(15, 8, 'Tietoliikenneyhteydet', 1, 'Pääsy nopeisiin verkkoyhteyksiin', 10.00, 24.00),
	(16, 10, 'Tietoliikenneyhteydet', 1, 'Pääsy nopeisiin verkkoyhteyksiin', 10.00, 24.00),
	(17, 1, 'Kannetava tietokone', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(18, 3, 'Kannetava tietokone', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(19, 5, 'Kannetava tietokone', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(20, 7, 'Kannetava tietokone', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(21, 9, 'Kannetava tietokone', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(22, 11, 'Kannetava tietokone', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(23, 2, 'Dataprojektori', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(24, 4, 'Dataprojektori', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(25, 7, 'Dataprojektori', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00),
	(26, 12, 'Kannetava tietokone', 2, 'Toimistolaitteiden vuokraus', 5.00, 24.00);

-- Dumping structure for table varausjarjestelma.varaus
CREATE TABLE IF NOT EXISTS `varaus` (
  `varaus_id` int(11) NOT NULL AUTO_INCREMENT,
  `asiakas_id` int(11) NOT NULL,
  `huone_id` int(11) NOT NULL,
  `vahvistus_pvm` datetime NOT NULL,
  `varattu_pvm` datetime NOT NULL,
  `aloitus_pvm` datetime NOT NULL,
  `loppu_pvm` datetime NOT NULL,
  PRIMARY KEY (`varaus_id`),
  KEY `asiakas_id` (`asiakas_id`),
  KEY `huone_id` (`huone_id`),
  CONSTRAINT `varaus_ibfk_1` FOREIGN KEY (`asiakas_id`) REFERENCES `asiakas` (`asiakas_id`) ON DELETE CASCADE,
  CONSTRAINT `varaus_ibfk_2` FOREIGN KEY (`huone_id`) REFERENCES `huone` (`huone_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table varausjarjestelma.varaus: ~6 rows (approximately)
INSERT INTO `varaus` (`varaus_id`, `asiakas_id`, `huone_id`, `vahvistus_pvm`, `varattu_pvm`, `aloitus_pvm`, `loppu_pvm`) VALUES
	(1, 1, 1, '2024-05-01 08:00:00', '2024-04-25 08:00:00', '2024-05-01 08:00:00', '2024-05-10 17:00:00'),
	(2, 2, 2, '2024-05-02 09:00:00', '2024-04-26 09:00:00', '2024-05-02 09:00:00', '2024-05-11 18:00:00'),
	(3, 3, 3, '2024-05-03 10:00:00', '2024-04-27 10:00:00', '2024-05-03 10:00:00', '2024-05-12 19:00:00'),
	(4, 4, 4, '2024-05-04 11:00:00', '2024-04-28 11:00:00', '2024-05-04 11:00:00', '2024-05-13 20:00:00'),
	(5, 5, 5, '2024-05-05 12:00:00', '2024-04-29 12:00:00', '2024-05-05 12:00:00', '2024-05-14 21:00:00'),
	(6, 6, 6, '2024-05-06 13:00:00', '2024-04-30 13:00:00', '2024-05-06 13:00:00', '2024-05-15 22:00:00');

-- Dumping structure for table varausjarjestelma.varauksen_palvelut
CREATE TABLE IF NOT EXISTS `varauksen_palvelut` (
  `varaus_id` int(11) NOT NULL,
  `palvelu_id` int(11) NOT NULL,
  `palvelun_lkm` int(11) NOT NULL,
  PRIMARY KEY (`palvelu_id`,`varaus_id`),
  KEY `varaus_id` (`varaus_id`),
  CONSTRAINT `varauksen_palvelut_ibfk_1` FOREIGN KEY (`palvelu_id`) REFERENCES `palvelu` (`palvelu_id`) ON DELETE CASCADE,
  CONSTRAINT `varauksen_palvelut_ibfk_2` FOREIGN KEY (`varaus_id`) REFERENCES `varaus` (`varaus_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table varausjarjestelma.varauksen_palvelut: ~6 rows (approximately)
INSERT INTO `varauksen_palvelut` (`varaus_id`, `palvelu_id`, `palvelun_lkm`) VALUES
	(2, 1, 1),
	(2, 9, 2),
	(2, 12, 1),
	(1, 17, 1),
	(3, 18, 1),
	(2, 23, 1);



-- Dumping structure for table varausjarjestelma.lasku
CREATE TABLE IF NOT EXISTS `lasku` (
  `lasku_id` int(11) NOT NULL AUTO_INCREMENT,
  `varaus_id` int(11) NOT NULL,
  `asiakas_id` int(11) NOT NULL,
  `nimi` varchar(60) NOT NULL,
  `lahiosoite` varchar(40) NOT NULL,
  `postitoimipaikka` varchar(30) NOT NULL,
  `postinro` char(5) NOT NULL,
  `summa` double(8,2) NOT NULL,
  `alv` double(8,2) NOT NULL,
  `sahkoposti` tinyint(1) NOT NULL DEFAULT 1,
  `paperi` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`lasku_id`),
  KEY `asiakas_id` (`asiakas_id`),
  KEY `varaus_id` (`varaus_id`),
  CONSTRAINT `lasku_ibfk_1` FOREIGN KEY (`asiakas_id`) REFERENCES `asiakas` (`asiakas_id`) ON DELETE CASCADE,
  CONSTRAINT `lasku_ibfk_2` FOREIGN KEY (`varaus_id`) REFERENCES `varaus` (`varaus_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table varausjarjestelma.lasku: ~6 rows (approximately)
INSERT INTO `lasku` (`lasku_id`, `varaus_id`, `asiakas_id`, `nimi`, `lahiosoite`, `postitoimipaikka`, `postinro`, `summa`, `alv`, `sahkoposti`, `paperi`) VALUES
	(1, 1, 1, 'Liisa Virtanen', 'Mäkikatu 2', 'Turku', '20100', 354.19, 24.00, 1, 1),
	(2, 2, 2, 'Antti Järvinen', 'Sarankatu 3', 'Tampere', '33100', 1041.74, 24.00, 1, 1),
	(3, 3, 3, 'Anna Korhonen', 'Ellintie 4', 'Oulu', '90100', 1046.13, 24.00, 1, 1),
	(4, 4, 4, 'Jari Kinnunen', 'Tiirantie 5', 'Jyväskylä', '40100', 1005.43, 24.00, 1, 1),
	(5, 5, 5, 'Kalle Mäkinen', 'Selmankatu 1', 'Helsinki', '00100', 788.77, 24.00, 1, 1),
	(6, 6, 6, 'Delfi Oy', 'Liikekatu 2', 'Tampere', '33100', 827.56, 24.00, 1, 1);


/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
