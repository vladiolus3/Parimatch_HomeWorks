create or replace procedure InsertUser( 
	temp_first_name varchar(128), 
	temp_last_name varchar(128)
	)
	language sql
as $$
	insert into users(first_name, last_name)
	values(temp_first_name, temp_last_name)
$$;

--call InsertUser('andrei', 'romanenko');