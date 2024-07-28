CREATE TABLE [dbo].[Semester](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[PublicationDate] [date] NOT NULL,
	[PublicationEndDate] [date] NULL,
	[WorkStartDate] [date] NOT NULL,
	[WorkEndDate] [date] NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SemesterTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SemesterId] [int] NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Description] [nvarchar](1000) NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_SemesterTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[SemesterTranslation]  WITH CHECK ADD  CONSTRAINT [FK_SemesterTranslation_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])

ALTER TABLE [dbo].[SemesterTranslation] CHECK CONSTRAINT [FK_SemesterTranslation_Semester]
