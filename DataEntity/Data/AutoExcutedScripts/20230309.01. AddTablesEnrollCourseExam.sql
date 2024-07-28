
IF COL_LENGTH('EnrollCourseExam','TestTypeID') IS NULL
BEGIN
ALTER TABLE  [dbo].EnrollCourseExam ADD  TestTypeID  int  NULL;
END



IF COL_LENGTH('EnrollCourseExam','EnrollLectureID') IS NULL
BEGIN
ALTER TABLE  [dbo].EnrollCourseExam ADD  EnrollLectureID  int  NULL;
END
