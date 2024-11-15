-- Jenna Boyes
-- SQL Worksheet

-- 1. Find the titles and the genre of all books written by Harper Lee.
SELECT title 'Book Title'
FROM book
WHERE author = 'Harper Lee';

-- 2. Find all years that have a book that received a rating of 4 or 5, and sort them in increasing order.
SELECT ratings 'Rating', ratingDate 'Date'
FROM rating
WHERE ratings > 3
ORDER BY ratings;

-- 3. Find the names of all reviewers who rated To Kill a Mocking Bird.
SELECT name 'Reviewer Name'
FROM rating rat
JOIN book b ON rat.bID = b.bID
JOIN reviewer rev ON rat.rID = rev.rID
WHERE title = 'To Kill a Mocking Bird';

-- 4. Some reviewers didn't provide a date with their rating. Find the names of all reviewers who have ratings with a NULL value for the date.
SELECT name 'Reviewer Name'
FROM rating rat
JOIN reviewer rev ON rat.rID = rev.rID
WHERE ratingDate IS NULL;

-- 5. For any rating where the reviewer is the same as the author of any book, return the reviewer name, book title, and the ratings
SELECT name 'Reviewer/Author', title 'Book Title', ratings 'Rating'
FROM reviewer rev
JOIN rating rat ON rev.rID = rat.rID
JOIN book b ON rat.bID = b.bID
WHERE author = name;

-- 6. Write a query to return the rating data in a more readable format (usings titles): reviewer name, book title, ratings, and ratingDate. 
-- Also, sort the data, first by reviewer name, then by book title, and lastly by ratings.
SELECT name 'Reviewer Name', title 'Book Title', ratings 'Rating', ratingDate 'Rating Date'
FROM reviewer rev
JOIN rating rat ON rev.rID = rat.rID
JOIN book b ON rat.bID = b.bID
ORDER BY name, title, ratings;

-- 7. For all cases where the same reviewer rated the same book twice and gave it a higher rating the second time, 
-- return the reviewer's name and the title of the book.
SELECT name 'Reviewer', title 'Book Title' 
FROM rating r1
JOIN rating r2 ON r1.rID = r2.rID AND r1.bID = r2.bID
JOIN reviewer rev ON r1.rID = rev.rID
JOIN book b ON r1.bID = b.bID
WHERE r1.ratingDate < r2.ratingDate AND r1.ratings < r2.ratings;

-- 8. For each book that has at least one rating, find the highest rating that book received. Return the book title and the rating. Sort by book title.
SELECT title 'Book Title', max(ratings) 'Highest Rating'
FROM rating r
JOIN book b ON r.bID = b.bID
GROUP BY r.bID
ORDER BY title;

-- 9. For each book, return the title and the 'rating spread', that is, the difference between highest and lowest ratings given to that book. 
-- Sort by rating spread from highest to lowest, then by book title.
SELECT title 'Book Title', max - min `Rating Spread`
FROM
    (SELECT min(ratings) min, max(ratings) max, title
    FROM rating r
    JOIN book b ON r.bID = b.bID
    GROUP BY r.bID)
ORDER BY `Rating Spread` desc, title;

-- 10. Find the difference between the average rating of books released before 1970 and the average rating of books released after 1970.
-- (Make sure to calculate the average rating for each book, then the average of those averages for books before 1970 and books after. 
-- Don't just calculate the overall average rating before and after 1970.)
SELECT ABS( AVG(avgPre) - AVG(avgPost) ) 'Ratings Difference - Pre to Post 1970'
FROM
    -- avg of each books ratings before 1970
    (SELECT avg(ratings) avgPre
    FROM rating r
    JOIN book b ON r.bID = b.bID
    WHERE published < 1970
    GROUP BY r.bID)
JOIN
    -- avg of each books ratings after 1970
    (SELECT avg(ratings) avgPost
    FROM rating r
    JOIN book b ON r.bID = b.bID
    WHERE published > 1970
    GROUP BY r.bID);
