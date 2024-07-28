IF COL_LENGTH('SuperAdminSettings','ImageUrlAr') IS NULL
BEGIN
ALTER TABLE  [dbo].[SuperAdminSettings] add  [ImageUrlAr] [nvarchar](512) NULL;
END
