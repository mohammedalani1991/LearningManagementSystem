if not exists (select * from [SystemSetting] where [Name]='HomePage_OurStudents')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'HomePage_OurStudents','1,500')
end

if not exists (select * from [SystemSetting] where [Name]='HomePage_StudentsNationalities')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'HomePage_StudentsNationalities','1,300')
end

if not exists (select * from [SystemSetting] where [Name]='HomePage_OurCourses')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'HomePage_OurCourses','1,666')
end

if not exists (select * from [SystemSetting] where [Name]='HomePage_TeachersNationalities')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'HomePage_TeachersNationalities','5,000')
end


if not exists (select * from [SystemSetting] where [Name]='HomePage_GraduateStudents')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'HomePage_GraduateStudents','6,500')
end


if not exists (select * from [SystemSetting] where [Name]='ContactUs_Youtube')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_Youtube','https://www.youtube.com/embed/aL8Mmft8F2M')
end

if not exists (select * from [SystemSetting] where [Name]='ContactUs_facebook')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_facebook','https://www.youtube.com/embed/aL8Mmft8F2M')
end

if not exists (select * from [SystemSetting] where [Name]='ContactUs_instagram')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_instagram','https://www.youtube.com/embed/aL8Mmft8F2M')
end

if not exists (select * from [SystemSetting] where [Name]='ContactUs_tiktok')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_tiktok','https://www.youtube.com/embed/aL8Mmft8F2M')
end


