update EnrollCourseExam set  TestTypeID = 1

ALTER TABLE [dbo].EnrollCourseExam  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseExam_EnrollLecture] FOREIGN KEY(EnrollLectureID)
REFERENCES [dbo].EnrollLecture ([Id])

ALTER TABLE [dbo].EnrollCourseExam CHECK CONSTRAINT [FK_EnrollCourseExam_EnrollLecture]