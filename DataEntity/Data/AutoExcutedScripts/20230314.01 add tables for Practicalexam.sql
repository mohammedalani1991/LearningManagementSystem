
CREATE TABLE [dbo].[PracticalExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NULL,
	[Mark] [decimal](8, 2) NULL,
	[MarkAfterConversion] [decimal](8, 2) NULL,
 CONSTRAINT [PK_PracticalExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[PracticalExamCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[PracticalExamId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_PracticalExamCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[PracticalExamQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[PracticalExamId] [int] NOT NULL,
	[PracticalQuestionId] [int] NOT NULL,
 CONSTRAINT [PK_PracticalExamQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[PracticalExamTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PracticalExamId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_PracticalExamTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[PracticalQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NULL,
	[Mark] [decimal](8, 2) NULL,
	[IsDiscountFromTotal] [bit] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_PracticalQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[PracticalQuestionTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PracticalQuestionId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_PracticalQuestionTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PracticalExamCourse]  WITH CHECK ADD  CONSTRAINT [FK_PracticalExamCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[PracticalExamCourse] CHECK CONSTRAINT [FK_PracticalExamCourse_Course]

ALTER TABLE [dbo].[PracticalExamCourse]  WITH CHECK ADD  CONSTRAINT [FK_PracticalExamCourse_PracticalExam] FOREIGN KEY([PracticalExamId])
REFERENCES [dbo].[PracticalExam] ([Id])

ALTER TABLE [dbo].[PracticalExamCourse] CHECK CONSTRAINT [FK_PracticalExamCourse_PracticalExam]

ALTER TABLE [dbo].[PracticalExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_PracticalExamQuestion_PracticalExam] FOREIGN KEY([PracticalExamId])
REFERENCES [dbo].[PracticalExam] ([Id])

ALTER TABLE [dbo].[PracticalExamQuestion] CHECK CONSTRAINT [FK_PracticalExamQuestion_PracticalExam]

ALTER TABLE [dbo].[PracticalExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_PracticalExamQuestion_PracticalQuestion] FOREIGN KEY([PracticalQuestionId])
REFERENCES [dbo].[PracticalQuestion] ([Id])

ALTER TABLE [dbo].[PracticalExamQuestion] CHECK CONSTRAINT [FK_PracticalExamQuestion_PracticalQuestion]

ALTER TABLE [dbo].[PracticalExamTranslation]  WITH CHECK ADD  CONSTRAINT [FK_PracticalExamTranslation_PracticalExam] FOREIGN KEY([PracticalExamId])
REFERENCES [dbo].[PracticalExam] ([Id])

ALTER TABLE [dbo].[PracticalExamTranslation] CHECK CONSTRAINT [FK_PracticalExamTranslation_PracticalExam]

ALTER TABLE [dbo].[PracticalQuestionTranslation]  WITH CHECK ADD  CONSTRAINT [FK_PracticalQuestionTranslation_PracticalQuestion] FOREIGN KEY([PracticalQuestionId])
REFERENCES [dbo].[PracticalQuestion] ([Id])

ALTER TABLE [dbo].[PracticalQuestionTranslation] CHECK CONSTRAINT [FK_PracticalQuestionTranslation_PracticalQuestion]

