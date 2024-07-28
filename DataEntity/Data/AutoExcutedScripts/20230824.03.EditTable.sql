IF COL_LENGTH('SuperAdminSettings','SecondarySiteColor') IS NULL
BEGIN
ALTER TABLE  [dbo].[SuperAdminSettings] add  [SecondarySiteColor] [nvarchar](256) NULL;
END
