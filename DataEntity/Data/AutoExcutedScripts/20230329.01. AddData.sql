Declare @masterId int =0;

IF EXISTS(select [Id] from  [dbo].[MasterLookup] where [Code]='LearningMethod')
begin 
	SET @masterId = (select [Id] from  [dbo].[MasterLookup] where [Code]='LearningMethod')

	INSERT INTO [dbo].[DetailsLookup]([MasterId],[CreatedOn],[CreatedBy],[Status],[Code],[Value])
	VALUES (@masterId,'2021-11-30 13:57:59.933','salam-cs@hotmail.com',1,N'ElectronicOnce',N'Electronic Once')

	INSERT INTO [dbo].[DetailsLookupTranslation]([DetailsLookupId],[LanguageId],[Value] ,[IsDefault])
	VALUES (@@IDENTITY ,7,N'ألكتروني مرة واحدة' , 0)
END
