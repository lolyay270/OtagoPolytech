-- ------- SAKILA, MARIA ------- --
-- 1. List the names of all of the cities in Indonesia
SELECT city 'Cities in Indonesia'
FROM city
WHERE country_id =
  ( SELECT country_id
	FROM country 
	WHERE country = 'Indonesia');

-- 2. How many customers made a payment of $6 or more
SELECT COUNT(customer_id) 'Number of Payments $6 or More'
FROM customer
WHERE customer_id IN
    (SELECT customer_id
    FROM payment
    WHERE amount >= 6);

-- 3. How many films are not in the inventory?
SELECT COUNT(film_id) 'Number of Films Not In Inventory'
FROM film
WHERE film_id NOT IN
    (SELECT film_id 
    FROM inventory);

-- 4. List longest films?
SELECT title 'Longest Movies'
FROM film
WHERE length =
    (SELECT max(length)
    FROM film);

-- ------- STUDENT, SQLITE ------- --
-- 5. Who are the students who share the highest GPA?
SELECT sName 'Students with Highest GPA'
FROM student
WHERE GPA = 
    (SELECT max(GPA)
    FROM student);

-- 6. How many people have a GPA higher than the average?
SELECT sName 'Name', GPA
FROM student
WHERE GPA >
    (SELECT avg(GPA)
    FROM student);

-- 7. List the names of the students who were accepted somewhere 
SELECT sName 'Students Accepted'
FROM student
WHERE sID IN 
    (SELECT DISTINCT sID
    FROM apply
    WHERE decision = 'Y');

-- 8. List the names of the students who applied to an institution but were not accepted anywhere
SELECT sName 'Students Never Accepted'
FROM student
WHERE sID NOT IN
    (SELECT DISTINCT sID
    FROM apply
    WHERE decision = 'Y') -- never got accepted
AND sID IN
    (SELECT DISTINCT sID 
    FROM apply); -- have ever applied


-- ------- SAKILA, MARIA ------- --
-- 9. List all of the cities in Indonesia - using a join rather than sub-query
SELECT c.city 'City in Indonesia'
FROM city c
JOIN country C ON c.country_id = C.country_id
WHERE C.country = 'Indonesia';

-- 10. Make a list showing the number of payments for each customer (hint - needs a join and a group by). 
-- Order this list by the number of payments to identify the most frequent customers
SELECT first_name 'First Name', last_name 'Last Name', count(p.payment_id) `Payments Count`
FROM customer c 
JOIN payment p ON c.customer_id = p.customer_id
GROUP BY c.customer_id
ORDER BY `Payments Count` desc;

-- 11. There are two actors with the same name - what is the name of those actors? 
-- note to self: answer is susan davis
SELECT a1.first_name 'First Name', a1.last_name 'Last Name', a1.actor_id 'ID'
FROM actor a1
JOIN actor a2 ON a1.first_name = a2.first_name
WHERE 
    a1.last_name = a2.last_name
    AND a1.actor_id != a2.actor_id;

-- 12. List the title of the films along with their category
SELECT f.title 'Film Title', c.name 'Category'
FROM film f 
JOIN film_category fc ON f.film_id = fc.film_id
JOIN category c ON fc.category_id = c.category_id
LIMIT 100;

-- 13. List the title of the films in which THORA TEMPLE appears.
SELECT title 'Movies with Thora Temple'
FROM actor a 
JOIN film_actor fa ON a.actor_id = fa.actor_id
JOIN film f ON fa.film_id = f.film_id
WHERE first_name = 'THORA' AND last_name = 'TEMPLE';

-- 14. List the names and addresses (including city and country) of all of the staff
SELECT first_name, last_name, address, address2, district, postal_code, city, country
FROM staff s
JOIN address a ON s.address_id = a.address_id
JOIN city cit ON a.city_id = cit.city_id
JOIN country cou ON cit.country_id = cou.country_id;

-- 15. List the films each customer has rented, ordered by customer name
SELECT first_name, last_name, title
FROM customer c 
JOIN rental r ON c.customer_id = r.customer_id
JOIN inventory i ON r.inventory_id = i.inventory_id
JOIN film f ON i.film_id = f.film_id
ORDER BY first_name, last_name
LIMIT 100;

-- 16. List the number of actors who have been seen in films rented by each customer
SELECT c.first_name, c.last_name, title, count(a.actor_id)
FROM customer c 
JOIN rental r ON c.customer_id = r.customer_id
JOIN inventory i ON r.inventory_id = i.inventory_id
JOIN film f ON i.film_id = f.film_id
JOIN film_actor fa ON f.film_id = fa.film_id
JOIN actor a ON fa.actor_id = a.actor_id
GROUP BY c.customer_id, title
ORDER BY c.first_name, c.last_name
LIMIT 100;
