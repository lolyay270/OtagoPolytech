-- 1. Get a list of all film titles and their inventory number – even those we don’t have.
SELECT f.title, i.inventory_id
FROM inventory i LEFT JOIN film f
ON i.film_id = f.film_id
LIMIT 100;

-- 2. Which films do we not have in stock?
SELECT title
FROM film f LEFT JOIN inventory i
ON f.film_id = i.film_id
WHERE i.film_id IS NULL;

-- 3. List the number of films in which each actor has featured 
-- (sort the output in descending order of the number of films)
SELECT 
    first_name,
    last_name, 
    COUNT(fa.actor_id) as film_count
FROM actor a INNER JOIN film_actor fa
ON a.actor_id = fa.actor_id
GROUP BY a.actor_id --each actor individually
ORDER BY film_count DESC
LIMIT 100;

-- 4. The store uses a formula to calculate the return‐on‐investment (or ROI) which is (rental_rate / replacement_cost * 100).
-- List the films, rental replacement cost and ROI which have an ROI more than 10. Order by ROI. Only have the formula once in the query
SELECT title, replacement_cost, roi
FROM 
    (SELECT title, replacement_cost, (rental_rate / replacement_cost * 100) AS roi
    FROM film) as roi_calc
WHERE roi > 10
ORDER BY roi;

-- 5. List the maximum, minimum and average film replacement cost using subselects in the select clause only 
-- (do not use a FROM clause in the main query) – yes this is silly.
SELECT 
    (SELECT MAX(replacement_cost) FROM film) as max,
    (SELECT MIN(replacement_cost) FROM film) as min,
    (SELECT AVG(replacement_cost) FROM film) as avg;

-- 6. List the students as pairs who come from the same sized high school. Order by school size.
-- Only list one pair of each student e.g. if you have Alice and Bob in a record don’t also list Bob and Alice (unless they are different students – we have two different AMY’s).
-- Work through this in stages – removing redundant pairs is the last step. You might like to display more information while developing the query (e.g. sid)
SELECT s1.sID, s1.sName, s2.sID, s2.sName, s1.sizeHS
FROM student as s1 INNER JOIN student as s2
ON s1.sID < s2.sID
WHERE s1.sizeHS = s2.sizeHS
ORDER BY s1.sizeHS;

-- 7. List each student that has made an application and the number of ITP’s they have applied to.
SELECT s.sID, sName, count(DISTINCT itpName) 'ITPs applied to'
FROM student s
JOIN apply a ON s.sID = a.sID
GROUP BY s.sID;

-- 8. Which students have not applied anywhere?
SELECT sID, sName
FROM student
WHERE sID NOT IN
  (SELECT sID FROM apply)
GROUP BY sID;

-- 9. List a count of the number of applications made by each student
SELECT s.sID, sName, count(a.itpName) 'applications'
FROM student s
LEFT JOIN apply a ON s.sID = a.sID
GROUP BY s.sID;

-- 10. List the number of institutions that each student has applied to:
SELECT s.sID, sName, count(DISTINCT a.itpName) 'institutions'
FROM student s
LEFT JOIN apply a ON s.sID = a.sID
GROUP BY s.sID;

-- 11. How many students have applied to each institution?
SELECT itpName, count(DISTINCT sID)
FROM apply
GROUP BY itpName;
