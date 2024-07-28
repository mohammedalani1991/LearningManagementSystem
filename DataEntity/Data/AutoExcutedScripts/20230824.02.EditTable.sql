IF COL_LENGTH('SuperAdminSettings','SiteColor') IS NULL
BEGIN
ALTER TABLE  [dbo].[SuperAdminSettings] add  [SiteColor] [nvarchar](256) NULL;
END
