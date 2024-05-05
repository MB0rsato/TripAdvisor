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

-- Dump dei dati della tabella tripadvisor.classes: ~4 rows (circa)
DELETE FROM `classes`;
INSERT INTO `classes` (`classid`, `specialization`) VALUES
	('1AI', 'Informatica'),
	('2AE', 'Elettrotecnica'),
	('3AI', 'Informatica'),
	('3BI', 'Informatica'),
	('4AMM', 'Meccanica'),
	('5AAU', 'Automazione');

-- Dump della struttura di tabella tripadvisor.comments
DROP TABLE IF EXISTS `comments`;
CREATE TABLE IF NOT EXISTS `comments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `text` varchar(200) DEFAULT NULL,
  `state` varchar(200) DEFAULT NULL,
  `rating` int(11) DEFAULT NULL,
  `authorid` varchar(200) DEFAULT NULL,
  `deleted` char(1) DEFAULT NULL,
  `idTrip` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `FK_comments_trips` (`idTrip`),
  KEY `FK_comments_users` (`authorid`),
  CONSTRAINT `FK_comments_trips` FOREIGN KEY (`idTrip`) REFERENCES `trips` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_comments_users` FOREIGN KEY (`authorid`) REFERENCES `users` (`uid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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

-- Dump dei dati della tabella tripadvisor.partecipations: ~1 rows (circa)
DELETE FROM `partecipations`;
INSERT INTO `partecipations` (`idtrip`, `classid`) VALUES
	(0, '3AI'),
	(0, '3BI');

-- Dump della struttura di tabella tripadvisor.trips
DROP TABLE IF EXISTS `trips`;
CREATE TABLE IF NOT EXISTS `trips` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `date` date DEFAULT NULL,
  `location` varchar(200) DEFAULT NULL,
  `duration` int(11) DEFAULT NULL,
  `type` varchar(200) DEFAULT NULL,
  `price` int(11) DEFAULT NULL,
  `picture` varchar(200) DEFAULT NULL,
  `description` varchar(200) DEFAULT NULL,
  `averageRating` int(11) DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.trips: ~3 rows (circa)
DELETE FROM `trips`;
INSERT INTO `trips` (`id`, `date`, `location`, `duration`, `type`, `price`, `picture`, `description`, `averageRating`) VALUES
	(1, '2024-04-12', 'Steelco', 5, 'Uscita aziendale', 10, NULL, 'Uscita aziendale presso Steelco a Riese', 3),
	(2, '2024-05-05', 'Test', 3, 'Uscita Sportiva', 20, NULL, 'Ciao', 0),
	(3, '2024-05-02', 'Test2', 1, 'Uscita Culturale', 10, NULL, 'CAIOOOO', 0);

-- Dump della struttura di tabella tripadvisor.users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `uid` varchar(200) NOT NULL DEFAULT '',
  `name` varchar(50) DEFAULT NULL,
  `classid` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`uid`),
  KEY `FK_users_classes` (`classid`),
  CONSTRAINT `FK_users_classes` FOREIGN KEY (`classid`) REFERENCES `classes` (`classid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dump dei dati della tabella tripadvisor.users: ~3 rows (circa)
DELETE FROM `users`;
INSERT INTO `users` (`uid`, `name`, `classid`) VALUES
	('axLxI2rDazekCgkQlTUHWiZoU563', 'Aldo Baglio', '1AI'),
	('Hgdp5n6dVZPEpjej6axi6ZspEFG2', 'Aldo', '1AI'),
	('wifjf1bdN7ftrTM5aOPc8EDzOhR2', 'Thomas Garbui', '3BI');

-- Dump della struttura di trigger tripadvisor.AvgRating
DROP TRIGGER IF EXISTS `AvgRating`;
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ZERO_IN_DATE,NO_ZERO_DATE,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `AvgRating` AFTER INSERT ON `comments` FOR EACH ROW BEGIN

update trips
inner join (
  SELECT comments.idtrip AS id, avg(comments.rating) AS averageRating
  from comments
  group BY comments.idtrip
) as p on p.id = trips.id
set trips.averageRating = p.averageRating;

END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
