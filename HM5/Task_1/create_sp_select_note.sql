create or replace function SelectNote( 
	temp_id uuid
	)
	returns table (note_id uuid, header varchar, body varchar, is_deleted boolean, user_id integer,
				  modified_at timestamp with time zone, first_name varchar, last_name varchar)
	language sql
as $$
	select notes.id, notes.header, notes.body, notes.is_deleted, 
	notes.user_id, notes.modified_at, users.first_name, users.last_name from notes
	join users on notes.user_id = users.id 
	where notes.id = temp_id
$$;

--select * from SelectNote('6ecd8c99-4036-403d-bf84-cf8400f67836');