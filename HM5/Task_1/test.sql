call insertuser('1vladyslav', 'dovhal');
call insertuser('2dovhalslav', 'vladal');
call insertuser('3vladyslav', 'vlad');

select * from users;

call insertnote('123e4567-e89b-12d3-a456-426655440000', 'note 1', '1111', 3);
call insertnote('777e7777-e77b-12d3-a456-426655440000', 'note 2', '2222', 2);
call insertnote('888e7777-e77b-12d3-a456-426655440000', '222222', '2222', 2);
call insertnote('999e7777-e77b-12d3-a456-426655440000', 'note 3', '3333', 1);

select * from notes;

select * from selectnote('123e4567-e89b-12d3-a456-426655440000');
select * from selectnote('777e7777-e77b-12d3-a456-426655440000');

call updatenotemark('123e4567-e89b-12d3-a456-426655440000');
call updatenotemark('777e7777-e77b-12d3-a456-426655440000');

select * from usersnotescount();
select * from usersnotescount();

select * from selectusernotes(1);
select * from selectusernotes(2);