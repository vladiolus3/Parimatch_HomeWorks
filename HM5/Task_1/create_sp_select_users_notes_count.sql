create or replace function CountFalseNote(
	temp_user_id integer
	)
	returns integer 
	language sql
as $$
	select count(id) from notes
	where user_id = temp_user_id and is_deleted = false;
$$;

create or replace function UsersNotesCount( )
	returns table(id integer, f_name varchar, l_name varchar, count_note integer) 
	language sql
as $$
	select id, first_name, last_name, CountFalseNote(id) from users;
$$;

select * from UsersNotesCount();