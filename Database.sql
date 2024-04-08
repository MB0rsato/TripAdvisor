-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versione server:              10.4.32-MariaDB - mariadb.org binary distribution
-- S.O. server:                  Win64
-- HeidiSQL Versione:            12.5.0.6677
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dump della struttura del database tripadvisor
DROP DATABASE IF EXISTS `tripadvisor`;
CREATE DATABASE IF NOT EXISTS `tripadvisor` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `tripadvisor`;

-- Dump della struttura di tabella tripadvisor.classes
DROP TABLE IF EXISTS `classes`;
CREATE TABLE IF NOT EXISTS `classes` (
  `classid` varchar(200) NOT NULL DEFAULT 'NoClass',
  `specialization` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`classid`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.classes: ~0 rows (circa)
DELETE FROM `classes`;

-- Dump della struttura di tabella tripadvisor.comments
DROP TABLE IF EXISTS `comments`;
CREATE TABLE IF NOT EXISTS `comments` (
  `id` int(11) NOT NULL,
  `text` varchar(200) DEFAULT NULL,
  `state` varchar(200) DEFAULT NULL,
  `rating` int(11) DEFAULT NULL,
  `author` varchar(200) DEFAULT NULL,
  `deleted` char(2) DEFAULT NULL,
  `idTrip` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `FK_commenti_uscite` (`idTrip`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.comments: ~0 rows (circa)
DELETE FROM `comments`;

-- Dump della struttura di tabella tripadvisor.partecipations
DROP TABLE IF EXISTS `partecipations`;
CREATE TABLE IF NOT EXISTS `partecipations` (
  `idtrip` int(11) NOT NULL DEFAULT 0,
  `classid` varchar(200) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idtrip`,`classid`),
  KEY `FK_partecipations_classes` (`classid`),
  CONSTRAINT `FK_partecipations_classes` FOREIGN KEY (`classid`) REFERENCES `classes` (`classid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_partecipations_trips` FOREIGN KEY (`idtrip`) REFERENCES `trips` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.partecipations: ~0 rows (circa)
DELETE FROM `partecipations`;

-- Dump della struttura di tabella tripadvisor.trips
DROP TABLE IF EXISTS `trips`;
CREATE TABLE IF NOT EXISTS `trips` (
  `id` int(11) NOT NULL DEFAULT 0,
  `date` date DEFAULT NULL,
  `location` varchar(200) DEFAULT NULL,
  `classes` varchar(200) DEFAULT NULL,
  `duration` int(11) DEFAULT NULL,
  `type` varchar(200) DEFAULT NULL,
  `price` int(11) DEFAULT NULL,
  `pictures` varchar(200) DEFAULT NULL,
  `description` varchar(200) DEFAULT NULL,
  `averageRating` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.trips: ~0 rows (circa)
DELETE FROM `trips`;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
