IF COL_LENGTH('EnrollStudentCourse','ExpelledDate') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollStudentCourse] add  [ExpelledDate] [datetime] NULL;
END