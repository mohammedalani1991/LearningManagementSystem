

IF COL_LENGTH('Currency','SortOrder') IS NULL
BEGIN
ALTER TABLE  [dbo].[Currency] ADD  [SortOrder]  int  NULL;
END

