create or replace procedure InsertNote(
	temp_id uuid,
	temp_header varchar(128), 
	temp_body varchar(1024),
	temp_user_id integer
	)
	language sql
as $$
	insert into notes(id, header, body, user_id, modified_at)
	values(temp_id, temp_header, temp_body, temp_user_id, current_timestamp)
$$;

--call InsertNote('6ecd8c99-4036-403d-bf84-cf8400f67836', 'book', 'very interesting book', 2);