delete from DetailsLookup where MasterId = (select Id from MasterLookup where Code = 'LearningMethod') 
and DetailsLookup.Id not in (3133,3134,3135)

SET IDENTITY_INSERT DetailsLookup ON

if not exists (select * from DetailsLookup where Id =3133 )
begin
insert into DetailsLookup ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [Code], [Value])
values (3133,(select Id from MasterLookup where Code = 'LearningMethod') ,getdate(),'salam-cs@hotmail.com',1,'Online','Online')
end

if not exists (select * from DetailsLookupTranslation where DetailsLookupId =3133 )
begin
insert into DetailsLookupTranslation ([DetailsLookupId], [Value], [LanguageId], [IsDefault])
values (3133 ,N'الالكتروني',7,0)
end


if not exists (select * from DetailsLookup where Id =3134 )
begin
insert into DetailsLookup ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [Code], [Value])
values (3134,(select Id from MasterLookup where Code = 'LearningMethod') ,getdate(),'salam-cs@hotmail.com',1,'Offline','Offline')
end


if not exists (select * from DetailsLookupTranslation where DetailsLookupId =3134 )
begin
insert into DetailsLookupTranslation ([DetailsLookupId], [Value], [LanguageId], [IsDefault])
values (3134 ,N'الوجاهي',7,0)
end



if not exists (select * from DetailsLookup where Id =3135 )
begin
insert into DetailsLookup ([Id], [MasterId], [CreatedOn], [CreatedBy], [Status], [Code], [Value])
values (3135,(select Id from MasterLookup where Code = 'LearningMethod') ,getdate(),'salam-cs@hotmail.com',1,'Mix','Mix')
end

if not exists (select * from DetailsLookupTranslation where DetailsLookupId =3135 )
begin
insert into DetailsLookupTranslation ([DetailsLookupId], [Value], [LanguageId], [IsDefault])
values (3135 ,N'مدمج',7,0)
end

SET IDENTITY_INSERT DetailsLookup OFF





