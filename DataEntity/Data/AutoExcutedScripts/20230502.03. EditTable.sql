IF COL_LENGTH('Student','Balance') IS NULL
BEGIN
ALTER TABLE  [dbo].[Student] add  [Balance] [decimal](8, 2) NULL;
END