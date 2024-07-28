

IF COL_LENGTH('EnrollCourseExam','ExamFinalMark') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollCourseExam] ADD  [ExamFinalMark]  float  NULL;
END



IF COL_LENGTH('EnrollCourseExamQuestion','Mark') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollCourseExamQuestion] ADD  [Mark]  float  NULL;
END


