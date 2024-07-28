IF COL_LENGTH('Contact','PhoneNumberCode') IS NULL
BEGIN
ALTER TABLE  [dbo].[Contact] add  [PhoneNumberCode] [nvarchar](256) NULL;
END
