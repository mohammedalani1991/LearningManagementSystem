




ALTER TABLE [dbo].[EnrollStudentExam]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExam_EnrollCourseExam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[EnrollCourseExam] ([Id])

ALTER TABLE [dbo].[EnrollStudentExam] CHECK CONSTRAINT [FK_EnrollStudentExam_EnrollCourseExam]


alter table ContactUs drop column IF EXISTS Mobile