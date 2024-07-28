DECLARE @masterId AS int

INSERT INTO [dbo].[MasterLookup]([Name],[Code],[CreatedOn],[CreatedBy],[Status])VALUES
('TemplateType','TemplateType','2021-11-30 13:57:59.933','salam-cs@hotmail.com',1)
 
 SET @masterId = @@IDENTITY

INSERT INTO [dbo].[DetailsLookup]([MasterId],[CreatedOn],[CreatedBy],[Status],[Code],[Value])
VALUES (@masterId,'2021-11-30 13:57:59.933','salam-cs@hotmail.com',1,N'Certificate',N'Certificate')