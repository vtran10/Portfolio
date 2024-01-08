drop table output_data;

select * from output_data LIMIT 10;

create table test_table(
    sometext varchar(100)
);

insert into test_table values('test');
select sometext from test_table;
INSERT INTO test_table (sometext)
VALUES ('sometext:varchar(100)');