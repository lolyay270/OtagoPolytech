-- ----------reset db---------- --
DROP DATABASE IF EXISTS boyeje2_Hockey;
CREATE DATABASE boyeje2_Hockey;
USE boyeje2_Hockey;

-- SOURCE boyeje2_schema.sql;
-- SOURCE boyeje2_data.sql;
-- select * from information_schema.routines where routine_schema='boyeje2_Hockey' \G

-- ----------create each table that is required---------- --
CREATE OR REPLACE TABLE Person (
  personId INT AUTO_INCREMENT PRIMARY KEY,
  firstName VARCHAR(100) NOT NULL,
  lastName VARCHAR(100) NOT NULL,
  address VARCHAR(100) NOT NULL,
  phone VARCHAR(100) NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  photo BLOB
) ENGINE=INNODB;

CREATE OR REPLACE TABLE School (
  name VARCHAR(100) PRIMARY KEY,
  address VARCHAR(100) NOT NULL,
  phone VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL
) ENGINE=INNODB;

CREATE OR REPLACE TABLE Player (
  playerId INT PRIMARY KEY,
  DoB DATE NOT NULL,
  schoolName VARCHAR(100),
  FOREIGN KEY (playerId) REFERENCES Person(personId) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (schoolName) REFERENCES School(name) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=INNODB;

-- pick list
CREATE OR REPLACE TABLE Qualification (
  name VARCHAR(100) PRIMARY KEY
) ENGINE=INNODB;

CREATE OR REPLACE TABLE Coach (
  coachId INT,
  roleStart DATE NOT NULL,
  FOREIGN KEY (coachId) REFERENCES Person(personId) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=INNODB;

CREATE OR REPLACE TABLE Team (
  name VARCHAR(100) PRIMARY KEY,
  year YEAR NOT NULL,
  ageGroup VARCHAR(100) NOT NULL
) ENGINE=INNODB;

CREATE OR REPLACE TABLE Member (
  shirtNum INT NOT NULL,
  teamName VARCHAR(100) NOT NULL,
  PRIMARY KEY (teamName, shirtNum),
  playerId INT NOT NULL,
  FOREIGN KEY (teamName) REFERENCES Team(name) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (playerId) REFERENCES Player(playerId) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=INNODB;


-- ----------joining tables---------- --
CREATE OR REPLACE TABLE CoachQualifications (
  coachId INT NOT NULL,
  qualName VARCHAR(100) NOT NULL,
  FOREIGN KEY (coachId) REFERENCES Coach(coachId) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (qualName) REFERENCES Qualification(name) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=INNODB;

CREATE OR REPLACE TABLE TeamCoaches (
  teamName VARCHAR(100) NOT NULL,
  coachId INT NOT NULL,
  FOREIGN KEY (teamName) REFERENCES Team(name) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (coachId) REFERENCES Coach(coachId) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=INNODB;

CREATE OR REPLACE TABLE GuardianPersons (
  childId INT NOT NULL,
  guardianId INT NOT NULL,
  FOREIGN KEY (childId) REFERENCES Person(personId) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (guardianId) REFERENCES Person(personId) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=INNODB;

-- ----------create function---------- --
DELIMITER //
CREATE OR REPLACE FUNCTION teamsize (team varchar(100))
  RETURNS INT
  DETERMINISTIC
  READS SQL DATA
  SQL SECURITY INVOKER
  COMMENT 'This function takes in a team name and will return the number of players in that team'
  BEGIN
  RETURN (SELECT count(shirtNum) FROM Member WHERE teamName = team);
  END //
DELIMITER ;
/*
SELECT teamsize("not a team"); -- returns 0
SELECT teamsize("Southern Icebreakers"); -- returns 3
*/


-- ----------create view---------- --

-- function to run inside of view
DELIMITER //
CREATE OR REPLACE FUNCTION bestPhone(childId INT, guardianId INT)
  RETURNS VARCHAR(100)
  DETERMINISTIC
  READS SQL DATA
  SQL SECURITY INVOKER
  COMMENT 'This function returns the guardians phone if it exists, else returns players phone'
  BEGIN
  DECLARE gPhone VARCHAR(100) DEFAULT (SELECT phone FROM Person WHERE personId = guardianId);
  IF gPhone IS NOT NULL THEN RETURN gPhone;
  ELSE RETURN (SELECT phone FROM Person WHERE personId = childId);
  END IF;
  END //
DELIMITER ;
/*
SELECT bestPhone(1, 10); -- returns guardians num
SELECT bestPhone(2, NULL); -- returns childs num
*/

-- playerName, (if guardian), best phone, teamName
CREATE OR REPLACE VIEW phonelist AS
SELECT 
  CONCAT(c.firstName, ' ', c.lastName) `Child's Name`,
  CONCAT(g.firstName, ' ', g.lastName) `Guardian's Name`,
  bestPhone(c.personId, g.personId) `Phone`,
  m.teamName `Team Name`
FROM Player p
LEFT JOIN Person c ON p.playerId = c.personId -- c for child
LEFT JOIN GuardianPersons gp ON c.personId = gp.childId
LEFT JOIN Person g ON gp.guardianId = g.personId -- g for guardian
JOIN Member m ON p.playerId = m.playerId;
-- SELECT * FROM phonelist;
