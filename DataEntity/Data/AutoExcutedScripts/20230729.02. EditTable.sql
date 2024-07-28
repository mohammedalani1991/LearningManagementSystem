IF COL_LENGTH('SectionOfCourse','Order') IS NULL
BEGIN
ALTER TABLE  [dbo].[SectionOfCourse] add  [Order] [int] NULL;
END

IF COL_LENGTH('Lecture','Order') IS NULL
BEGIN
ALTER TABLE  [dbo].[Lecture] add  [Order] [int] NULL;
END

IF COL_LENGTH('EnrollLecture','Order') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollLecture] add  [Order] [int] NULL;
END


INSERT [dbo].[SuperAdmin] ( [Name], [Show]) VALUES ( N'Ratings', 1)
