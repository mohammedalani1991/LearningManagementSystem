delete from EnrollStudentExamAnswerOption
delete from EnrollStudentExamAnswer
delete from EnrollStudentExam

IF COL_LENGTH('EnrollStudentExamAnswer','IsCorrect') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollStudentExamAnswer] ADD  [IsCorrect]  bit  NULL;
END

