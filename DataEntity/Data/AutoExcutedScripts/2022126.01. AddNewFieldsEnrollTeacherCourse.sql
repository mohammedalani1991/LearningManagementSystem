IF COL_LENGTH('[dbo].[EnrollTeacherCourse]','AgeAllowedForRegistration') IS NULL
BEGIN
Alter table  [dbo].[EnrollTeacherCourse] Add AgeAllowedForRegistration int not null DEFAULT (0)
END

IF COL_LENGTH('[dbo].[EnrollTeacherCourse]','AgeGroup') IS NULL
BEGIN
Alter table  [dbo].[EnrollTeacherCourse] Add AgeGroup int not null DEFAULT (0)
END
