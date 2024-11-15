-- origonally from stanford http://www.db-class.org/course/resources/index adjusted to give local NZ flavour
drop table if exists ITP;
drop table if exists Student;
drop table if exists Apply;

create table ITP(itpName text, region text, enrollment int);
create table Student(sID int, sName text, GPA real, sizeHS int);
create table Apply(sID int, itpName text, major text, decision text);
