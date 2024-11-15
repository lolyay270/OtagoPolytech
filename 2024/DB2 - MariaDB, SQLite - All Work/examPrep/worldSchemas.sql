 CREATE TABLE `country` (
  `Code` char(3) NOT NULL DEFAULT '',
  `Name` char(52) NOT NULL DEFAULT '',
  `Continent` enum('Asia','Europe','North America','Africa','Oceania','Antarctica','South America') NOT NULL DEFAULT 'Asia',
  `Region` char(26) NOT NULL DEFAULT '',
  `SurfaceArea` float(10,2) NOT NULL DEFAULT 0.00,
  `IndepYear` smallint(6) DEFAULT NULL,
  `Population` int(11) NOT NULL DEFAULT 0,
  `LifeExpectancy` float(3,1) DEFAULT NULL,
  `GNP` float(10,2) DEFAULT NULL,
  `GNPOld` float(10,2) DEFAULT NULL,
  `LocalName` char(45) NOT NULL DEFAULT '',
  `GovernmentForm` char(45) NOT NULL DEFAULT '',
  `HeadOfState` char(60) DEFAULT NULL,
  `Capital` int(11) DEFAULT NULL,
  `Code2` char(2) NOT NULL DEFAULT '',
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci
/*
+------+-------------+---------------+---------------------------+-------------+-----------+------------+----------------+---------+---------+-----------------------+----------------------------------------------+--------------------------+---------+-------+
| Code | Name        | Continent     | Region                    | SurfaceArea | IndepYear | Population | LifeExpectancy | GNP     | GNPOld  | LocalName             | GovernmentForm                               | HeadOfState              | Capital | Code2 |
+------+-------------+---------------+---------------------------+-------------+-----------+------------+----------------+---------+---------+-----------------------+----------------------------------------------+--------------------------+---------+-------+
| ABW  | Aruba       | North America | Caribbean                 |      193.00 |      NULL |     103000 |           78.4 |  828.00 |  793.00 | Aruba                 | Nonmetropolitan Territory of The Netherlands | Beatrix                  |     129 | AW    |
| AFG  | Afghanistan | Asia          | Southern and Central Asia |   652090.00 |      1919 |   22720000 |           45.9 | 5976.00 |    NULL | Afganistan/Afqanestan | Islamic Emirate                              | Mohammad Omar            |       1 | AF    |
| AGO  | Angola      | Africa        | Central Africa            |  1246700.00 |      1975 |   12878000 |           38.3 | 6648.00 | 7984.00 | Angola                | Republic                                     | José Eduardo dos Santos  |      56 | AO    |
+------+-------------+---------------+---------------------------+-------------+-----------+------------+----------------+---------+---------+-----------------------+----------------------------------------------+--------------------------+---------+-------+
*/


select * from country where Continent = "Oceania" \G


CREATE TABLE `city` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` char(35) NOT NULL DEFAULT '',
  `CountryCode` char(3) NOT NULL DEFAULT '',
  `District` char(20) NOT NULL DEFAULT '',
  `Population` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`),
  KEY `CountryCode` (`CountryCode`),
  CONSTRAINT `city_ibfk_1` FOREIGN KEY (`CountryCode`) REFERENCES `country` (`de`)
) ENGINE=InnoDB AUTO_INCREMENT=4080 DEFAULT CHARSET=latin1 COLLATE=latin1_swedh_ci
/*
+----+----------+-------------+----------+------------+
| ID | Name     | CountryCode | District | Population |
+----+----------+-------------+----------+------------+
|  1 | Kabul    | AFG         | Kabol    |    1780000 |
|  2 | Qandahar | AFG         | Qandahar |     237500 |
|  3 | Herat    | AFG         | Herat    |     186800 |
+----+----------+-------------+----------+------------+
*/





 CREATE TABLE `countrylanguage` (
  `CountryCode` char(3) NOT NULL DEFAULT '',
  `Language` char(30) NOT NULL DEFAULT '',
  `IsOfficial` enum('T','F') NOT NULL DEFAULT 'F',
  `Percentage` float(4,1) NOT NULL DEFAULT 0.0,
  PRIMARY KEY (`CountryCode`,`Language`),
  KEY `CountryCode` (`CountryCode`),
  CONSTRAINT `countryLanguage_ibfk_1` FOREIGN KEY (`CountryCode`) REFERENCES `country` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci
/*
+-------------+------------+------------+------------+
| CountryCode | Language   | IsOfficial | Percentage |
+-------------+------------+------------+------------+
| ABW         | Dutch      | T          |        5.3 |
| ABW         | English    | F          |        9.5 |
| ABW         | Papiamento | F          |       76.7 |
+-------------+------------+------------+------------+
*/

--db is data from 2001, due to US and Palau presidents only overlapping in 2001