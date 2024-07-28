IF COL_LENGTH('EnrollTeacherCourse','AgeGroupTo') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollTeacherCourse] add  [AgeGroupTo] [int] NULL;
END
