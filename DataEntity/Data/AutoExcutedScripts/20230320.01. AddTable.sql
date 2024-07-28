CREATE TABLE [dbo].[PracticalEnrollmentExamStudent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[PracticalEnrollmentExamId] [int] NULL,
	[EnrollStudentCourseId] [int] NULL,
	[Mark] [decimal](8, 2) NULL,
	[MarkAfterConversion] [decimal](8, 2) NULL,
 CONSTRAINT [PK_PracticalEnrollmentExamStudent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[PracticalEnrollmentExamStudentSubject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[PracticalEnrollmentExamStudentId] [int] NULL,
	[SubjectId] [int] NULL,
	[Mark] [decimal](8, 2) NULL,
	[DisountMarkTotal] [decimal](8, 2) NULL,
 CONSTRAINT [PK_PracticalEnrollmentExamStudentSubject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[PracticalEnrollmentExamStudentSubjectRating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[PracticalEnrollmentExamStudentSubjectId] [int] NOT NULL,
	[PracticalQuestionId] [int] NOT NULL,
	[Mark] [decimal](18, 2) NULL,
	[NoOfErrors] [int] NULL,
 CONSTRAINT [PK_PracticalEnrollmentExamStudentSubjectRating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PracticalEnrollmentExamStudent]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamStudent_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExamStudent] CHECK CONSTRAINT [FK_PracticalEnrollmentExamStudent_EnrollStudentCourse]

ALTER TABLE [dbo].[PracticalEnrollmentExamStudent]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamStudent_PracticalEnrollmentExam] FOREIGN KEY([PracticalEnrollmentExamId])
REFERENCES [dbo].[PracticalEnrollmentExam] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExamStudent] CHECK CONSTRAINT [FK_PracticalEnrollmentExamStudent_PracticalEnrollmentExam]

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamStudentSubject_PracticalEnrollmentExamStudent] FOREIGN KEY([PracticalEnrollmentExamStudentId])
REFERENCES [dbo].[PracticalEnrollmentExamStudent] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubject] CHECK CONSTRAINT [FK_PracticalEnrollmentExamStudentSubject_PracticalEnrollmentExamStudent]

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamStudentSubject_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubject] CHECK CONSTRAINT [FK_PracticalEnrollmentExamStudentSubject_Subject]

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubjectRating]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamStudentSubjectRating_PracticalEnrollmentExamStudentSubject] FOREIGN KEY([PracticalEnrollmentExamStudentSubjectId])
REFERENCES [dbo].[PracticalEnrollmentExamStudentSubject] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubjectRating] CHECK CONSTRAINT [FK_PracticalEnrollmentExamStudentSubjectRating_PracticalEnrollmentExamStudentSubject]

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubjectRating]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamStudentSubjectRating_PracticalQuestion] FOREIGN KEY([PracticalQuestionId])
REFERENCES [dbo].[PracticalQuestion] ([Id])

ALTER TABLE [dbo].[PracticalEnrollmentExamStudentSubjectRating] CHECK CONSTRAINT [FK_PracticalEnrollmentExamStudentSubjectRating_PracticalQuestion]
