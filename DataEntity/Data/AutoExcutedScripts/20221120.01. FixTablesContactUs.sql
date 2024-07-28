

delete from [dbo].[ContactUs];

ALTER TABLE  [dbo].[ContactUs] ALTER COLUMN [Email]  nvarchar(max) NOT NULL;
ALTER TABLE  [dbo].[ContactUs] ALTER COLUMN [Message]  nvarchar(max) NOT NULL;
ALTER TABLE  [dbo].[ContactUs] ALTER COLUMN [Name]  nvarchar(max) NOT NULL;


IF COL_LENGTH('ContactUs','Subject') IS NULL
BEGIN
ALTER TABLE  [dbo].[ContactUs] ADD  [Subject]  nvarchar(max) NOT NULL;
END


IF COL_LENGTH('ContactUs','CreatedBy') IS not NULL
BEGIN
ALTER TABLE  [dbo].[ContactUs] DROP COLUMN [CreatedBy];
END

IF COL_LENGTH('ContactUs','Mobile') IS not NULL
BEGIN
ALTER TABLE  [dbo].[ContactUs] DROP COLUMN [Mobile];
END


IF COL_LENGTH('ContactUs','Address') IS not NULL
BEGIN
ALTER TABLE  [dbo].[ContactUs] DROP COLUMN [Address];
END

IF COL_LENGTH('ContactUs','InsertDate') IS not NULL
BEGIN
ALTER TABLE  [dbo].[ContactUs] DROP COLUMN [InsertDate];
END










  
   
