# Useful info per week
1.1. DB fundamentals
- layers
- types of DBs 

2.1 Data Modelling
- DB design fundamentals
- crows-feet & UML
- directions of ^
- verbs on ^

2.2. Use Cases
- DB creation reasons
- 7 question setup
- ensure all entities do CRUD
- what already exists?

3.1. Developing the Model
- finding issues in design
- finalising design
- many to many
- pick list / lookup table

3.2. Working with Objects
- OO thinking
- inheritance

4.1. Normalisation
- terms for each stage of DB (concept, logical, physical)
- lots of examples to understand it

4.2. SQL Intro
- DB languages (DDL, DML, etc)
- functions (min, max, etc)
- order by, as, where between, like, where and/or

5.1. Sub Selects and Joins
- sqlite setup pg2
- sub selects
  - order of sql processes
  - in, not in
  - where =, <, >
  - where exists, where not exists
- joins
  - cartesian joins (bad)
  - basic setup pg19
  - compacted 3 join pg22
  - self join pg24

5.2. Sub Select and Outer Joins
- subselect in select pg4
- limit pg8
- distinct pg9
- different types of joins
- joining using non pk/fk values
- is null, is not null

6.1. Nulls and Data Modifications
- nulls in logic (and, or, not)
- insert
- delete
- update

6.2. Physical Design
- convert logical to physical
- storage engine comparison (also see pdf and word)
- using engine with commands
- drop db pg11
- PK, auto increment, varchar(length), int
- show create table (and sqlite ver)
- timestamp, current time
- data types (look at word doc)
- inheritance into physical 
- naming conventions
- constraints in DBs and what we can use (FK, PK, UNIQUE, etc)
- on delete, on update
- foreign key in physical (pg33 and readme)

7.1. Stored Routines
- what can use routines?
- advantages
- transferring file into mysql
- procedure vs function
- change delimiter pg8
- using \G delimiter pg10
- session variables pg16
- procedures
  - basic procedure pg8
  - show procedures pg10,11
  - parameters(in, out, inout)
- functions
  - parameters(all are in)
  - returns value
  - datetime formatting
- characteristics (deterministic, contains sql, comment, etc)
- sql security (definer, invoker)

8.1. Constraints, Views and Indexes
- composite/multi-field PK
- more on FK on update/delete
- unique, not null
- show create table and \G
- triggers
  - uses
  - syntax for making
- views
  - show views
  - pros cons pg17
- index
  - benefits
  - issues
  - making pg20

9.1. Program Control
- complex coding in stored routines
- local variables
- if then, case
- loops (while do, repeat until, loop)

10.1. Concurrency
- Transactions
  - ACID (atomic, consistent, isolated, durable)
  - code pg8
- types of bad reads (dirty, non-repeatable, phantom)
- types of locks to stop bad reads

11.1. Controlling Transactions
- autocommit
- isolation levels
- binlog

12.1. Security
- physical
- network
- OS, patch Tues, exploit Wed
- DB client and app
- web apps that test security
- permissions (admin, user, object)
- connections
- authentication
- important tables in mysql (user, db, host, table_priv, column_priv)
- combining permissions (top down, additive)
- grant, revoke
- granting specific commands

12.2. Authorisation
(look at pdf with notations)
- views for specific users
- grant/revoke 
- revoke cascade/restrict

13.1. DateTime
- data types
- functions (now, curdate)
- errors/illegal -> all zeros
- how to write each data type
- date_format function
- unix time (1-1-1970), conversion
- timestamp vs unixtime
- all functions and formatters are in word doc

<br/><br/><br/>

# code snippet examples
## order by function renamed
```sql
SELECT first_name 'First Name', last_name 'Last Name', count(p.payment_id) `Payments Count`
FROM customer c 
JOIN payment p ON c.customer_id = p.customer_id
GROUP BY c.customer_id
ORDER BY `Payments Count` desc;
```

## referencing composite PK as FK
```sql
CREATE TABLE ab (
  a VARCHAR(1) DEFAULT "a",
  b VARCHAR(1) DEFAULT "b",
  PRIMARY KEY (a, b)
);

CREATE or replace TABLE cd (
  a VARCHAR(1),
  b VARCHAR(1),
  c INT PRIMARY KEY,
  d VARCHAR(2),
  FOREIGN KEY (a,b) REFERENCES ab(a,b) ON DELETE CASCADE ON UPDATE CASCADE
);
