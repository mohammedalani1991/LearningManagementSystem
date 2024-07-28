if not exists (select * from [SystemSetting] where [Name]='TimeZone')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'TimeZone','UTC') 
end

