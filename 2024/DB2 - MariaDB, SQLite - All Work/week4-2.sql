-- SQL Exercise
-- Sakila
--1. What is the average replacement cost of a film?
    SELECT 
    AVG(replacement_cost) AS 'Average Replacement Cost'
    FROM film;
    The query above returns 19.984

--2. Design a query to list the titles of each film and its language_id.
    SELECT
    title AS Title,
    language_id AS 'Language ID'
    FROM film;

--3. List the staff at each store (First name, last name, storenumber)
    SELECT
    first_name AS 'First Name',
    last_name AS Surname,
    store_id AS 'Store ID'
    FROM staff;

--4. Design a query to show the number of films in each language.
    SELECT 
    COUNT(film.title) AS 'Film Count',
    language.name AS Language
    FROM film 
    INNER JOIN language 
    ON language.language_id=film.language_id
    GROUP BY film.language_id;