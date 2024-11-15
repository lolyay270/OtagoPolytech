-- origonally from stanford http://www.db-class.org/course/resources/index adjusted to give local NZ flavour
delete from Student;
delete from ITP;
delete from Apply;

insert into Student values (123, 'Amy', 3.9, 1000);
insert into Student values (234, 'Bob', 3.6, 1500);
insert into Student values (345, 'Craig', 3.5, 500);
insert into Student values (456, 'Doris', 3.9, 1000);
insert into Student values (567, 'Edward', 2.9, 2000);
insert into Student values (678, 'Fay', 3.8, 200);
insert into Student values (789, 'Gary', 3.4, 800);
insert into Student values (987, 'Helen', 3.7, 800);
insert into Student values (876, 'Irene', 3.9, 400);
insert into Student values (765, 'Jay', 2.9, 1500);
insert into Student values (654, 'Amy', 3.9, 1000);
insert into Student values (543, 'Craig', 3.4, 2000);


insert into ITP values ('Otago Polytechnic', 'Otago', 3337);
insert into ITP values ('Unitec', 'Auckland', 8378);
insert into ITP values ('CPIT', 'Canterbury', 5076);
insert into ITP values ('AUT', 'Auckland', 15484);

insert into Apply values (123, 'Otago Polytechnic', 'BIT', 'Y');
insert into Apply values (123, 'Otago Polytechnic', 'Engineering', 'N');
insert into Apply values (123, 'Unitec', 'BIT', 'Y');
insert into Apply values (123, 'AUT', 'Engineering', 'Y');
insert into Apply values (234, 'Unitec', 'Nursing', 'N');
insert into Apply values (345, 'CPIT', 'Business Administration', 'Y');
insert into Apply values (345, 'AUT', 'Business Administration', 'N');
insert into Apply values (345, 'AUT', 'BIT', 'Y');
insert into Apply values (345, 'AUT', 'Engineering', 'N');
insert into Apply values (678, 'Otago Polytechnic', 'Art', 'Y');
insert into Apply values (987, 'Otago Polytechnic', 'BIT', 'Y');
insert into Apply values (987, 'Unitec', 'BIT', 'Y');
insert into Apply values (876, 'Otago Polytechnic', 'BIT', 'N');
insert into Apply values (876, 'CPIT', 'Nursing', 'Y');
insert into Apply values (876, 'CPIT', 'Architecture', 'N');
insert into Apply values (765, 'Otago Polytechnic', 'Art', 'Y');
insert into Apply values (765, 'AUT', 'Art', 'N');
insert into Apply values (765, 'AUT', 'psychology', 'Y');
insert into Apply values (543, 'CPIT', 'BIT', 'N');
