-- Create a procedure circle_area which has an IN parameter for radius 
-- and an OUT parameter for the area of a circle
DELIMITER //
CREATE OR REPLACE PROCEDURE circle_area (IN radius float, OUT area float)
DETERMINISTIC
BEGIN
SELECT (PI()*radius*radius) INTO area;
END //
DELIMITER ;

-- Create another procedure which uses a single session variable 
-- as an INOUT parameter to calculate the area of a circle
DELIMITER //
CREATE OR REPLACE PROCEDURE circle_area_inout (INOUT radius float)
DETERMINISTIC
BEGIN
SELECT (PI()*radius*radius) INTO radius;
END //
DELIMITER ;

-- Create a function to do the same job (area of circle)
DELIMITER //
CREATE OR REPLACE FUNCTION circle_area_func (radius FLOAT)
RETURNS FLOAT
DETERMINISTIC
BEGIN
RETURN (PI()*radius*radius);
END //
DELIMITER ;

-- Create a function age(birthDate DATE) which returns 
-- the number of years between now() and birthDate
DELIMITER //
CREATE OR REPLACE FUNCTION age(birthDate DATE)
RETURNS INT
NOT DETERMINISTIC
BEGIN
RETURN DATEDIFF(NOW(), birthDate) / 365.25;
END //
DELIMITER ;

--select * from information_schema.routines where routine_schema='boyeje2_pets' \G