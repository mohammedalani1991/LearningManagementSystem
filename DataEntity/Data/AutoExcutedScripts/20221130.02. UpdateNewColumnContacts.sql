IF COL_LENGTH('Contact','IsEmailVerified') IS NULL
BEGIN
ALTER TABLE  [dbo].[Contact] ADD  [IsEmailVerified]  bit;
END