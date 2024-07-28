drop table EnrollStudentExamAnswerOption
drop table EnrollStudentExamAnswer
drop table EnrollStudentExam

CREATE TABLE [dbo].[EnrollStudentExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[FinalMark] [float] NULL,
	[MarkHeGot] [float] NULL,
	[MarkAfterConversion] [float] NULL,
	[EnrollCourseExamId] [int] NOT NULL,
	[EnrollStudentCourseId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollStudentExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[EnrollStudentExamAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollCourseExamQuestionId] [int] NOT NULL,
	[Answer] [nvarchar](500) NULL,
	[Mark] [float] NOT NULL,
	[EnrollStudentExamId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollStudentExamQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[EnrollStudentExamAnswerOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollStudentExamAnswerId] [int] NOT NULL,
	[QusetionOptionId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollStudentExamAnswerOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EnrollStudentExam]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExam_EnrollCourseExam] FOREIGN KEY([EnrollCourseExamId])
REFERENCES [dbo].[EnrollCourseExam] ([Id])

ALTER TABLE [dbo].[EnrollStudentExam] CHECK CONSTRAINT [FK_EnrollStudentExam_EnrollCourseExam]

ALTER TABLE [dbo].[EnrollStudentExam]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExam_Student] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])

ALTER TABLE [dbo].[EnrollStudentExam] CHECK CONSTRAINT [FK_EnrollStudentExam_Student]

ALTER TABLE [dbo].[EnrollStudentExamAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamQuestion_EnrollStudentExam] FOREIGN KEY([EnrollStudentExamId])
REFERENCES [dbo].[EnrollStudentExam] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswer] CHECK CONSTRAINT [FK_EnrollStudentExamQuestion_EnrollStudentExam]

ALTER TABLE [dbo].[EnrollStudentExamAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamQuestion_ExamQuestion] FOREIGN KEY([EnrollCourseExamQuestionId])
REFERENCES [dbo].[EnrollCourseExamQuestion] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswer] CHECK CONSTRAINT [FK_EnrollStudentExamQuestion_ExamQuestion]

ALTER TABLE [dbo].[EnrollStudentExamAnswerOption]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamAnswerOption_EnrollStudentExamQuestion] FOREIGN KEY([EnrollStudentExamAnswerId])
REFERENCES [dbo].[EnrollStudentExamAnswer] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswerOption] CHECK CONSTRAINT [FK_EnrollStudentExamAnswerOption_EnrollStudentExamQuestion]

ALTER TABLE [dbo].[EnrollStudentExamAnswerOption]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamAnswerOption_QuestionOption] FOREIGN KEY([QusetionOptionId])
REFERENCES [dbo].[QuestionOption] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswerOption] CHECK CONSTRAINT [FK_EnrollStudentExamAnswerOption_QuestionOption]
