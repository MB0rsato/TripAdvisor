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

-- Dump della struttura di tabella tripadvisor.commenti
DROP TABLE IF EXISTS `commenti`;
CREATE TABLE IF NOT EXISTS `commenti` (
  `id` int(11) NOT NULL,
  `testo` varchar(200) DEFAULT NULL,
  `stato` varchar(200) DEFAULT NULL,
  `voto` int(11) DEFAULT NULL,
  `autore` varchar(200) DEFAULT NULL,
  `eliminato` char(2) DEFAULT NULL,
  `idUscita` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `FK_commenti_uscite` (`idUscita`),
  CONSTRAINT `FK_commenti_uscite` FOREIGN KEY (`idUscita`) REFERENCES `uscite` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.commenti: ~0 rows (circa)
DELETE FROM `commenti`;

-- Dump della struttura di tabella tripadvisor.uscite
DROP TABLE IF EXISTS `uscite`;
CREATE TABLE IF NOT EXISTS `uscite` (
  `id` int(11) NOT NULL DEFAULT 0,
  `data` date DEFAULT NULL,
  `luogo` varchar(200) DEFAULT NULL,
  `classi` varchar(200) DEFAULT NULL,
  `durata` int(11) DEFAULT NULL,
  `tipologia` varchar(200) DEFAULT NULL,
  `prezzo` int(11) DEFAULT NULL,
  `foto` varchar(200) DEFAULT NULL,
  `descrizione` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.uscite: ~0 rows (circa)
DELETE FROM `uscite`;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
