CREATE TABLE [dbo].[PracticalEnrollmentExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[PracticalExamId] [int] NULL,
	[EnrollTeacherCourseId] [int] NULL,
	[TypeId] [int] NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_PracticalEnrollmentExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[PracticalEnrollmentExam]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExam_PracticalExam] FOREIGN KEY([PracticalExamId])
REFERENCES [dbo].[PracticalExam] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExam] CHECK CONSTRAINT [FK_PracticalEnrollmentExam_PracticalExam]

ALTER TABLE [dbo].[PracticalEnrollmentExam]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExam_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExam] CHECK CONSTRAINT [FK_PracticalEnrollmentExam_EnrollTeacherCourse]


IF COL_LENGTH('PracticalExam','TypeId') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalExam] add  [TypeId]  [int]  NULL;
END