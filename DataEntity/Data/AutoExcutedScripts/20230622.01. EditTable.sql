IF COL_LENGTH('Semester','Default') IS NULL
BEGIN
ALTER TABLE  [dbo].[Semester] add  [Default] [bit] NULL;
END
