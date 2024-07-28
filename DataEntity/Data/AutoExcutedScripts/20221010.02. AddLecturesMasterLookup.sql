if not exists (select * from MasterLookup where Code='ResourceType')
begin
INSERT into MasterLookup (CreatedOn,CreatedBy,Status,Name,Code)
VALUES (getdate(),'salam-cs@hotmail.com',1,'ResourceType','ResourceType')
end
