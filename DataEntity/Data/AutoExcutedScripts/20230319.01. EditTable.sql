IF COL_LENGTH('PracticalEnrollmentExam','StartDate') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalEnrollmentExam] add  [StartDate]  [datetime] Not NULL;
END
IF COL_LENGTH('PracticalEnrollmentExam','EndDate') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalEnrollmentExam] add  [EndDate]  [datetime] Not NULL;
END
