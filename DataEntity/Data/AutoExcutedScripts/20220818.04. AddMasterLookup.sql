if not exists (select * from MasterLookup where Code='CalendarType')
begin
INSERT into MasterLookup (CreatedOn,CreatedBy,Status,Name,Code)
VALUES (getdate(),'salam-cs@hotmail.com',1,'CalendarType','CalendarType')
end
