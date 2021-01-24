create or replace function SelectUserNotes(
	temp_user_id integer
	)
	returns table(f_id uuid, f_header varchar, f_body varchar, 
				  f_is_deleted boolean, f_user_id integer, f_modified_at timestamp with time zone) 
	language sql
as $$
	select * from notes
	where user_id = temp_user_id and is_deleted = false;
$$;

--select * from SelectUserNotes(1);