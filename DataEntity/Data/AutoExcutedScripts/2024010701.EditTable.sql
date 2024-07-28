IF COL_LENGTH('EnrollCourseQuiz','Order') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollCourseQuiz] add  [Order] [int] NULL;
END

IF COL_LENGTH('SectionOfCourseQuiz','Order') IS NULL
BEGIN
ALTER TABLE  [dbo].[SectionOfCourseQuiz] add  [Order] [int] NULL;
END
