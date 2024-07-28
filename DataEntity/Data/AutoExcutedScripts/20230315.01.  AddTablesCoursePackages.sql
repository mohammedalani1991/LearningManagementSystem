
CREATE TABLE [dbo].[CoursePackages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[PackageName] [nvarchar](200) NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_CoursePackages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[CoursePackagesRelations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CoursePackagesId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_CoursePackagesRelations_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[CoursePakagesTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CoursePackagesId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[PackageName] [nvarchar](200) NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_CoursePakagesTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[CoursePackagesRelations]  WITH CHECK ADD  CONSTRAINT [FK_CoursePackagesRelations_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[CoursePackagesRelations] CHECK CONSTRAINT [FK_CoursePackagesRelations_Course]

ALTER TABLE [dbo].[CoursePackagesRelations]  WITH CHECK ADD  CONSTRAINT [FK_CoursePackagesRelations_CoursePackages] FOREIGN KEY([CoursePackagesId])
REFERENCES [dbo].[CoursePackages] ([Id])

ALTER TABLE [dbo].[CoursePackagesRelations] CHECK CONSTRAINT [FK_CoursePackagesRelations_CoursePackages]

ALTER TABLE [dbo].[CoursePakagesTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CoursePakagesTranslation_CoursePackages] FOREIGN KEY([CoursePackagesId])
REFERENCES [dbo].[CoursePackages] ([Id])

ALTER TABLE [dbo].[CoursePakagesTranslation] CHECK CONSTRAINT [FK_CoursePakagesTranslation_CoursePackages]

