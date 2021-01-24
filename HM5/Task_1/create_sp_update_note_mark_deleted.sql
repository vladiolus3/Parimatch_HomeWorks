create or replace procedure UpdateNoteMark( 
	temp_id uuid
	)
	language sql
as $$
	update notes
	set is_deleted = true,
		modified_at = current_timestamp
	where id = temp_id;
$$;

--call UpdateNoteMark('6ecd8c99-4036-403d-bf84-cf8400f67836');
--select * from SelectNote('6ecd8c99-4036-403d-bf84-cf8400f67836');