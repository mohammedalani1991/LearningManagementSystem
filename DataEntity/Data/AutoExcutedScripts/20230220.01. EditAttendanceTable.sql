IF COL_LENGTH('CourseAttendance','EnrollTeacherCourseId') IS NULL
BEGIN
ALTER TABLE  [dbo].[CourseAttendance] ADD  [EnrollTeacherCourseId] int NULL;

ALTER TABLE [dbo].[CourseAttendance]  WITH CHECK ADD  CONSTRAINT [FK_CourseAttendance_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[CourseAttendance] CHECK CONSTRAINT [FK_CourseAttendance_EnrollTeacherCourse]
END