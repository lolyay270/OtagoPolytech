--------TIME OF DAY--------
DELIMITER //
CREATE OR REPLACE FUNCTION timeOfDay(time TIME)
RETURNS VARCHAR(100)
DETERMINISTIC
BEGIN
  IF time < '12:00:00' THEN RETURN 'Good Morning!';
  ELSEIF time = '12:00:00' THEN RETURN 'Lunch Time!';
  ELSEIF time < '17:00:00' THEN RETURN 'Good Afternoon!';
  ELSE RETURN 'Good Evening!';
  END IF;
END //
DELIMITER ;
/*
SELECT timeOfDay('09:00:00'); --morning
SELECT timeOfDay('12:00:00'); --lunch (cause noon != afternooon)
SELECT timeOfDay('13:00:00'); --afternoon
SELECT timeOfDay('19:00:00'); --evening
*/


--------FIBONACCI SEQUENCE--------
DELIMITER //
CREATE OR REPLACE FUNCTION fib(repeats INT)
RETURNS VARCHAR(100)
DETERMINISTIC
BEGIN
  DECLARE step INT DEFAULT 2; -- step is default 2 cause 2 nums are preset
  DECLARE numsString VARCHAR(100) DEFAULT '0, 1'; 
  DECLARE num1 INT DEFAULT 0;
  DECLARE num2 INT DEFAULT 1;
  DECLARE sum INT;
  IF repeats <= 1 THEN RETURN 'This method requires a number larger than 1';
  ELSEIF repeats = 2 THEN RETURN numsString;
  END IF;
  WHILE step < repeats DO
    SET step = step + 1;
    -- swap nums 
    SET sum = num1 + num2;
    SET num1 = num2;
    SET num2 = sum;
    -- add sum to return string
    SET numsString = CONCAT(numsString, ', ', sum);
  END WHILE;
  RETURN numsString;
END //
DELIMITER ;
/*
SELECT fib(-2); --must be above 1
SELECT fib(2);  --0, 1
SELECT fib(10); --0, 1, 1, 2, 3, 5, 8, 13, 21, 34 
*/
