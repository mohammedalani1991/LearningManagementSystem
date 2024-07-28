

IF COL_LENGTH('Trainer','Signature') IS NULL
BEGIN
ALTER TABLE  [dbo].[Trainer] ADD  [Signature]  nvarchar(512)  NULL;
END
