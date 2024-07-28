if not exists (select * from [SystemSetting] where [Name]='ContactUs_Map_Lat_Long')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_Map_Lat_Long','2.939802,101.323592')
end

if not exists (select * from [SystemSetting] where [Name]='ContactUs_Address')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_Address','Al-Safa Academy Training Center. Kuala Lumpur, Malaysia')
end

if not exists (select * from [SystemSetting] where [Name]='ContactUs_Email')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_Email','info@assafa-academy.com')
end

if not exists (select * from [SystemSetting] where [Name]='ContactUs_Phone')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'ContactUs_Phone','+60108442628')
end


if not exists (select * from [SystemSetting] where [Name]='GoogleReCaptcha_Site_key')
begin
INSERT INTO [SystemSetting] ([CreatedOn], [CreatedBy], [Status], [Name], [Value])
values(getdate(),'System',1,'GoogleReCaptcha_Site_key','6LcuFiAjAAAAAJ-HpGr64cYC3Hb-fRnRSpKdG3c7')
end