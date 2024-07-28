
CREATE TABLE [dbo].[Assignment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CourseId] [int] NOT NULL,
	[SubmissionDate] [datetime2](7) NOT NULL,
	[ExpiryDate] [datetime2](7) NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[AssignmentTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_AssignmentTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[EnrollAssignment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollCourseId] [int] NOT NULL,
	[SubmissionDate] [datetime2](7) NOT NULL,
	[ExpiryDate] [datetime2](7) NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EnrollAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[EnrollAssignmentTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollAssignmentId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EnrollAssignmentTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Assignment_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_Assignment_Course]

ALTER TABLE [dbo].[AssignmentTranslation]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentTranslation_Assignment] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([Id])

ALTER TABLE [dbo].[AssignmentTranslation] CHECK CONSTRAINT [FK_AssignmentTranslation_Assignment]

ALTER TABLE [dbo].[EnrollAssignment]  WITH CHECK ADD  CONSTRAINT [FK_EnrollAssignment_EnrollCourseExam] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollCourseExam] ([Id])

ALTER TABLE [dbo].[EnrollAssignment] CHECK CONSTRAINT [FK_EnrollAssignment_EnrollCourseExam]

ALTER TABLE [dbo].[EnrollAssignmentTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollAssignmentTranslation_EnrollAssignment] FOREIGN KEY([EnrollAssignmentId])
REFERENCES [dbo].[EnrollAssignment] ([Id])

ALTER TABLE [dbo].[EnrollAssignmentTranslation] CHECK CONSTRAINT [FK_EnrollAssignmentTranslation_EnrollAssignment]

