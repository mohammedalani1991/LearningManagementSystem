delete from CoursePakagesTranslation
delete from CoursePackagesRelations
delete from CoursePackages

ALTER TABLE [CoursePackagesRelations]
DROP CONSTRAINT [FK_CoursePackagesRelations_Course];

ALTER TABLE [dbo].[CoursePackagesRelations]  WITH CHECK ADD  CONSTRAINT [FK_CoursePackagesRelations_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
