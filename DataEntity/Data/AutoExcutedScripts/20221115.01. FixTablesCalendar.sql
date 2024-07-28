
delete from [dbo].[CalendarTranslation];
delete from [dbo].[Calendar];

ALTER TABLE  [dbo].[Calendar] ALTER COLUMN [StartDate]  datetime NOT NULL;
ALTER TABLE  [dbo].[Calendar] ALTER COLUMN [EndDate]  datetime NOT NULL;


if not exists (select Id from DetailsLookup where MasterId=(select Id from MasterLookup where Code='CalendarType') and Status=1)
BEGIN
 INSERT into DetailsLookup ([MasterId], [CreatedOn], [CreatedBy], [Status], [Code], [Value])
 values ((select Id from MasterLookup where Code='CalendarType'),getdate(),'salam-cs@hotmail.com',1,'Type1','Type1')
end












  
   
