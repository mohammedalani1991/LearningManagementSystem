
IF COL_LENGTH('CalendarTranslation','Description') IS NULL
BEGIN
ALTER TABLE  [dbo].[CalendarTranslation] ADD  [Description]  nvarchar(max)  NULL;
END


IF COL_LENGTH('Calendar','Description') IS NULL
BEGIN
ALTER TABLE  [dbo].[Calendar] ADD  [Description]  nvarchar(max)  NULL;
END


