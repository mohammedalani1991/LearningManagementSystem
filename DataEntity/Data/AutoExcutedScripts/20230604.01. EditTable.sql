IF COL_LENGTH('ExamTemplate','Shuffle') IS NULL
BEGIN
ALTER TABLE  [dbo].[ExamTemplate] add  [Shuffle] [bit] NULL;
END

IF COL_LENGTH('EnrollCourseExam','Shuffle') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollCourseExam] add  [Shuffle] [bit] NULL;
END