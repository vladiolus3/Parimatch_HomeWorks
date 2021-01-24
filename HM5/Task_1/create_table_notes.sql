create table notes
(
	id uuid primary key,
	header varchar(128) not null, 
	body varchar(1024) not null,
	is_deleted boolean not null default false,
	user_id integer references users (id) not null,
	modified_at timestamp with time zone default current_timestamp not null
);
create index modified_at_id on notes(modified_at);