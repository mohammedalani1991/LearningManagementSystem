
CREATE TABLE [dbo].[EnrollCourseAssigment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[EnrollTeacherCourseId] [int] NOT NULL,
	[PublishDate] [datetime2](7) NULL,
	[PublishEndDate] [datetime2](7) NULL,
 CONSTRAINT [PK_EnrollCourseAssigment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[EnrollCourseAssigmentQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollCourseAssigmentId] [int] NOT NULL,
	[QuestionName] [nvarchar](500) NOT NULL,
	[QuestionType] [int] NOT NULL,
 CONSTRAINT [PK_EnrollCourseAssigmentQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[EnrollCourseAssigmentQuestionOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[QuestionId] [int] NULL,
 CONSTRAINT [PK_EnrollCourseAssigmentQuestionOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[EnrollCourseAssigmentQuestionOptionTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[LanguageId] [int] NOT NULL,
	[OptionId] [int] NULL,
 CONSTRAINT [PK_EnrollCourseAssigmentQuestionOptionTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[EnrollCourseAssigmentQuestionTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL
) ON [PRIMARY]



CREATE TABLE [dbo].[EnrollCourseAssigmentTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollCourseAssigmentId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_EnrollCourseAssigmentTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[EnrollStudentAssigment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollCourseAssigmentId] [int] NOT NULL,
	[EnrollStudentCourseId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollStudentAssigment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[EnrollStudentAssigmentAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollCourseAssigmentQuestionId] [int] NOT NULL,
	[Answer] [nvarchar](500) NULL,
	[EnrollStudentAssigmentId] [int] NOT NULL,
	[IsCorrect] [bit] NULL,
 CONSTRAINT [PK_EnrollStudentAssigmentAnswer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[EnrollStudentAssigmentAnswerOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollStudentAssigmentAnswerId] [int] NOT NULL,
	[QusetionOptionId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollStudentAssigmentAnswerOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EnrollCourseAssigment]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAssigment_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollCourseAssigment] CHECK CONSTRAINT [FK_EnrollCourseAssigment_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestion]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAssigmentQuestion_EnrollCourseAssigment] FOREIGN KEY([EnrollCourseAssigmentId])
REFERENCES [dbo].[EnrollCourseAssigment] ([Id])

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestion] CHECK CONSTRAINT [FK_EnrollCourseAssigmentQuestion_EnrollCourseAssigment]

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestionOption]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAssigmentQuestionOption_EnrollCourseAssigmentQuestion] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[EnrollCourseAssigmentQuestion] ([Id])

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestionOption] CHECK CONSTRAINT [FK_EnrollCourseAssigmentQuestionOption_EnrollCourseAssigmentQuestion]

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestionOptionTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAssigmentQuestionOptionTranslation_EnrollCourseAssigmentQuestionOption] FOREIGN KEY([OptionId])
REFERENCES [dbo].[EnrollCourseAssigmentQuestionOption] ([Id])

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestionOptionTranslation] CHECK CONSTRAINT [FK_EnrollCourseAssigmentQuestionOptionTranslation_EnrollCourseAssigmentQuestionOption]

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestionTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAssigmentQuestionTranslation_EnrollCourseAssigmentQuestion] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[EnrollCourseAssigmentQuestion] ([Id])

ALTER TABLE [dbo].[EnrollCourseAssigmentQuestionTranslation] CHECK CONSTRAINT [FK_EnrollCourseAssigmentQuestionTranslation_EnrollCourseAssigmentQuestion]

ALTER TABLE [dbo].[EnrollCourseAssigmentTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAssigmentTranslation_EnrollCourseAssigment] FOREIGN KEY([EnrollCourseAssigmentId])
REFERENCES [dbo].[EnrollCourseAssigment] ([Id])

ALTER TABLE [dbo].[EnrollCourseAssigmentTranslation] CHECK CONSTRAINT [FK_EnrollCourseAssigmentTranslation_EnrollCourseAssigment]

ALTER TABLE [dbo].[EnrollStudentAssigment]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentAssigment_EnrollCourseAssigment] FOREIGN KEY([EnrollCourseAssigmentId])
REFERENCES [dbo].[EnrollCourseAssigment] ([Id])

ALTER TABLE [dbo].[EnrollStudentAssigment] CHECK CONSTRAINT [FK_EnrollStudentAssigment_EnrollCourseAssigment]

ALTER TABLE [dbo].[EnrollStudentAssigment]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentAssigment_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])

ALTER TABLE [dbo].[EnrollStudentAssigment] CHECK CONSTRAINT [FK_EnrollStudentAssigment_EnrollStudentCourse]

ALTER TABLE [dbo].[EnrollStudentAssigmentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentAssigmentAnswer_EnrollCourseAssigmentQuestion] FOREIGN KEY([EnrollCourseAssigmentQuestionId])
REFERENCES [dbo].[EnrollCourseAssigmentQuestion] ([Id])

ALTER TABLE [dbo].[EnrollStudentAssigmentAnswer] CHECK CONSTRAINT [FK_EnrollStudentAssigmentAnswer_EnrollCourseAssigmentQuestion]

ALTER TABLE [dbo].[EnrollStudentAssigmentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentAssigmentAnswer_EnrollStudentAssigment] FOREIGN KEY([EnrollStudentAssigmentId])
REFERENCES [dbo].[EnrollStudentAssigment] ([Id])

ALTER TABLE [dbo].[EnrollStudentAssigmentAnswer] CHECK CONSTRAINT [FK_EnrollStudentAssigmentAnswer_EnrollStudentAssigment]

ALTER TABLE [dbo].[EnrollStudentAssigmentAnswerOption]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentAssigmentAnswerOption_EnrollStudentAssigmentAnswer] FOREIGN KEY([EnrollStudentAssigmentAnswerId])
REFERENCES [dbo].[EnrollStudentAssigmentAnswer] ([Id])

ALTER TABLE [dbo].[EnrollStudentAssigmentAnswerOption] CHECK CONSTRAINT [FK_EnrollStudentAssigmentAnswerOption_EnrollStudentAssigmentAnswer]

