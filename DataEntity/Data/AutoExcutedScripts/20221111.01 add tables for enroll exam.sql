
CREATE TABLE [dbo].[EnrollStudentExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[FinalMark] [float] NULL,
	[MarkHeGot] [float] NULL,
	[MarkAfterConversion] [float] NULL,
	[ExamId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollStudentExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[EnrollStudentExamAnswerOption](
	[Id] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[QusetionId] [int] NOT NULL,
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EnrollStudentExamAnswerOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[EnrollStudentExamAnswer](
	[Id] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[QusetionId] [int] NOT NULL,
	[Answer] [nvarchar](500) NULL,
	[Mark] [float] NOT NULL,
	[ExamId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollStudentExamQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[EnrollStudentExamAnswerOption]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamAnswerOption_EnrollStudentExamQuestion] FOREIGN KEY([QusetionId])
REFERENCES [dbo].[EnrollStudentExamAnswer] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswerOption] CHECK CONSTRAINT [FK_EnrollStudentExamAnswerOption_EnrollStudentExamQuestion]

ALTER TABLE [dbo].[EnrollStudentExamAnswerOption]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamAnswerOption_QuestionOption] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[QuestionOption] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswerOption] CHECK CONSTRAINT [FK_EnrollStudentExamAnswerOption_QuestionOption]

ALTER TABLE [dbo].[EnrollStudentExamAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamQuestion_EnrollStudentExam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[EnrollStudentExam] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswer] CHECK CONSTRAINT [FK_EnrollStudentExamQuestion_EnrollStudentExam]

ALTER TABLE [dbo].[EnrollStudentExamAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentExamQuestion_ExamQuestion] FOREIGN KEY([QusetionId])
REFERENCES [dbo].[ExamQuestion] ([Id])

ALTER TABLE [dbo].[EnrollStudentExamAnswer] CHECK CONSTRAINT [FK_EnrollStudentExamQuestion_ExamQuestion]




ALTER TABLE EnrollCourseExam  DROP COLUMN CategoryId

ALTER TABLE EnrollCourseExam  add ExamTemplateId int


ALTER TABLE [dbo].[EnrollCourseExam]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseExam_ExamTemplate] FOREIGN KEY([ExamTemplateId])
REFERENCES [dbo].[ExamTemplate] ([Id])

ALTER TABLE [dbo].[EnrollCourseExam] CHECK CONSTRAINT [FK_EnrollCourseExam_ExamTemplate]




ALTER TABLE [dbo].[EnrollCourseExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseExamQuestion_ExamQuestion] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[ExamQuestion] ([Id])

ALTER TABLE [dbo].[EnrollCourseExamQuestion] CHECK CONSTRAINT [FK_EnrollCourseExamQuestion_ExamQuestion]



ALTER TABLE ExamTemplate  add FinalMark [float]

ALTER TABLE ExamTemplate  add MarkAfterConversion [float]


