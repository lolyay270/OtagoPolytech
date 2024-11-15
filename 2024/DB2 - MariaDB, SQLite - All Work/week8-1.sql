-- On your bookRating database in SQLite.
-- Create a view that joins all three tables together, select bID, title, published, genre, reviewers name, ratings and ratingdate

CREATE VIEW v_mega_table as
    SELECT b.bID, title, published, genre, name, ratings, ratingDate
    FROM reviewer rev
    JOIN rating rat ON rev.rID = rat.rID
    JOIN book b ON rat.bID = b.bID;
