-------- SQLite --------
-- Insert a new customer Alice who is 23 years old
INSERT INTO Person 
VALUES ('Alice', 23, 'female');

-- Alice likes the same pizza’s as Amy. Insert records into Eats based on Amy’s food choice
INSERT INTO Eats
SELECT 'Alice', pizza 
FROM 
   (SELECT name, pizza
    FROM Eats 
    WHERE name = 'Amy');

-- Alice goes to all pizzerias which serves mushroom pizza’s – insert these records into Frequents
INSERT INTO Frequents
SELECT 'Alice', pizzeria
FROM
    (SELECT pizzeria, pizza
    FROM Serves
    WHERE pizza is 'mushroom');

-- It turns out that Cal is female – update her record
UPDATE Person
SET gender = 'female'
WHERE name = 'Cal';

-- New York Pizza has been purchased by ‘Kiwi Pizza Company’. 
-- Change the name of all references in both Serves and Frequents tables
UPDATE Serves
SET pizzeria = ''


-- The price of cheese increases by 10%. Increase the cost of all cheese pizza’s by 10%
-- (added challenge – make sure the price is rounded to two decimal places).