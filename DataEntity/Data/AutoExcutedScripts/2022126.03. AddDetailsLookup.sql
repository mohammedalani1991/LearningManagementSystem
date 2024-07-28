
if not exists (select Id from DetailsLookup where MasterId=(select Id from MasterLookup where Code='CourseAgeGroup') and Status=1)
BEGIN
 INSERT into DetailsLookup ([MasterId], [CreatedOn], [CreatedBy], [Status], [Code], [Value])
 values ((select Id from MasterLookup where Code='CourseAgeGroup'),getdate(),'salam-cs@hotmail.com',1,'Males','Males')


  INSERT into DetailsLookup ([MasterId], [CreatedOn], [CreatedBy], [Status], [Code], [Value])
 values ((select Id from MasterLookup where Code='CourseAgeGroup'),getdate(),'salam-cs@hotmail.com',1,'Females','Females')


  INSERT into DetailsLookup ([MasterId], [CreatedOn], [CreatedBy], [Status], [Code], [Value])
 values ((select Id from MasterLookup where Code='CourseAgeGroup'),getdate(),'salam-cs@hotmail.com',1,'Mixed','Mixed')

end

