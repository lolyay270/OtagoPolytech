/* Delete the tables if they already exist */
drop table if exists Book;
drop table if exists Reviewer;
drop table if exists Rating; 

/* Create the schema for our tables */
create table Book(bID int, title text, published int, author text, genre text);
create table Reviewer(rID int, name text);
create table Rating(rID int, bID int, ratings int, ratingDate date);

/* Populate the tables with our data */
insert into Book values(101, 'To Kill a Mocking Bird', 1960, 'Harper Lee', 'Classic');
insert into Book values(102, 'Animal Farm', 1945, 'George Orwell', 'Classic');
insert into Book values(103, 'Dune', 1965, 'Frank Herbet', 'Science Fiction');
insert into Book values(104, 'The Diary of a Young Girl', 1947, 'Anne Frank', 'History');
insert into Book values(105, 'The Princess Bride', 1973, 'William Goldman', 'Fantasy');
insert into Book values(106, 'The Neverending Story', 1979, 'Michael Ende', null);
insert into Book values(107, '1776', 2005, 'David McCullough', 'History');
insert into Book values(108, 'I Robot', 1950, 'Isaac Asimov', 'Science Fiction');

insert into Reviewer values(201, 'Sarah Martinez');
insert into Reviewer values(202, 'Daniel Lewis');
insert into Reviewer values(203, 'Brittany Harris');
insert into Reviewer values(204, 'Mike Anderson');
insert into Reviewer values(205, 'Chris Jackson');
insert into Reviewer values(206, 'Elizabeth Thomas');
insert into Reviewer values(207, 'Isaac Asimov');
insert into Reviewer values(208, 'Ashley White');

insert into Rating values(201, 101, 2, '2011-01-22');
insert into Rating values(201, 101, 4, '2011-01-27');
insert into Rating values(202, 106, 4, null);
insert into Rating values(203, 103, 2, '2011-01-20');
insert into Rating values(203, 108, 4, '2011-01-12');
insert into Rating values(203, 108, 2, '2011-01-30');
insert into Rating values(204, 101, 3, '2011-01-09');
insert into Rating values(205, 103, 3, '2011-01-27');
insert into Rating values(205, 104, 2, '2011-01-22');
insert into Rating values(205, 108, 4, null);
insert into Rating values(206, 107, 3, '2011-01-15');
insert into Rating values(206, 106, 5, '2011-01-19');
insert into Rating values(207, 108, 5, '2011-01-20');
insert into Rating values(208, 104, 3, '2011-01-02');

