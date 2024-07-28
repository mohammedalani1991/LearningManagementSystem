IF COL_LENGTH('Course','AssignmentMark') IS NULL
BEGIN
ALTER TABLE  [dbo].[Course] add  [AssignmentMark] [decimal](8, 2) NULL;
END

IF COL_LENGTH('Course','ListeningExamMark') IS NULL
BEGIN
ALTER TABLE  [dbo].[Course] add  [ListeningExamMark] [decimal](8, 2) NULL;
END

IF COL_LENGTH('CourseMarks','ValueTo') IS NULL
BEGIN
ALTER TABLE  [dbo].[CourseMarks] add  [ValueTo] [decimal](8, 2) NULL;
END

IF COL_LENGTH('EnrollTeacherCourse','CalculationTypeId') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollTeacherCourse] add  [CalculationTypeId] [int] NULL;
END

IF COL_LENGTH('EnrollTeacherCourse','IsCourseDone') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollTeacherCourse] add  [IsCourseDone] [bit] NULL;
END