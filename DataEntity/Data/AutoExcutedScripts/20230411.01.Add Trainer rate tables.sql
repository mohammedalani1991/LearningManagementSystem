CREATE TABLE [dbo].[EnrollCourseAllowUserRates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[ContactId] [int] NOT NULL,
	[RateTypeId] [int]  NULL,
	[EnrollTeacherCourseId] [int] NULL,
 CONSTRAINT [PK_EnrollCourseAllowUserRates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[EnrollCourseAllowUserRates]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAllowUserRates_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ALTER TABLE [dbo].[EnrollCourseAllowUserRates] CHECK CONSTRAINT [FK_EnrollCourseAllowUserRates_Contact]

ALTER TABLE [dbo].[EnrollCourseAllowUserRates]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseAllowUserRates_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
ALTER TABLE [dbo].[EnrollCourseAllowUserRates] CHECK CONSTRAINT [FK_EnrollCourseAllowUserRates_EnrollTeacherCourse]
---------------------------
CREATE TABLE [dbo].[TrainerRateMeasure](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[FromRange] [decimal]  NULL,
	[ToRange] [decimal]  NULL,
	[Measure] [nvarchar](200) NULL,
 CONSTRAINT [PK_TrainerRateMeasure] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 


CREATE TABLE [dbo].[TrainerRateMeasureTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrainerRateMeasureId] [int] NOT NULL,
	[Measure] [nvarchar](200) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_TrainerRateMeasureTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TrainerRateMeasureTranslation]  WITH CHECK ADD  CONSTRAINT [FK_TrainerRateMeasureTranslation_TrainerRateMeasure] FOREIGN KEY([TrainerRateMeasureId])
REFERENCES [dbo].[TrainerRateMeasure] ([Id])
ALTER TABLE [dbo].[TrainerRateMeasureTranslation] CHECK CONSTRAINT [FK_TrainerRateMeasureTranslation_TrainerRateMeasure]

---------------------------------
CREATE TABLE [dbo].[AcademicSupervisionStandard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Standard] [nvarchar](MAX) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[SortOrder] [int] NULL,
	[DeletedOn] [datetime] NULL
 CONSTRAINT [PK_AcademicSupervisionStandard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
----------------
CREATE TABLE [dbo].[AcademicSupervisionStandardTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AcademicSupervisionStandardId] [int] NOT NULL,
	[Standard] [nvarchar](MAX) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_AcademicSupervisionStandardTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[AcademicSupervisionStandardTranslation]  WITH CHECK ADD  CONSTRAINT [FK_AcademicSupervisionStandardTranslation_AcademicSupervisionStandard] FOREIGN KEY([AcademicSupervisionStandardId])
REFERENCES [dbo].[AcademicSupervisionStandard] ([Id])
ALTER TABLE [dbo].[AcademicSupervisionStandardTranslation] CHECK CONSTRAINT [FK_AcademicSupervisionStandardTranslation_AcademicSupervisionStandard]

----------------------------------
CREATE TABLE [dbo].[AcademicSupervisionRate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollTeacherCourseId] [int] NULL,
	[StandardId] int NULL,
    [Rate] [int] NULL,
	[Note] [nvarchar](MAX) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL
 CONSTRAINT [PK_AcademicSupervisionRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[AcademicSupervisionRate]  WITH CHECK ADD  CONSTRAINT [FK_AcademicSupervisionRate_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
ALTER TABLE [dbo].[AcademicSupervisionRate] CHECK CONSTRAINT [FK_AcademicSupervisionRate_EnrollTeacherCourse]

ALTER TABLE [dbo].[AcademicSupervisionRate]  WITH CHECK ADD  CONSTRAINT [FK_AcademicSupervisionRate_AcademicSupervisionStandard] FOREIGN KEY([StandardId])
REFERENCES [dbo].[AcademicSupervisionStandard] ([Id])
ALTER TABLE [dbo].[AcademicSupervisionRate] CHECK CONSTRAINT [FK_AcademicSupervisionRate_AcademicSupervisionStandard]
---------------------------------------


CREATE TABLE [dbo].[ManagementStandard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Standard] [nvarchar](MAX) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[SortOrder] [int] NULL,
	[Type] [int] NULL,
	[DeletedOn] [datetime] NULL
 CONSTRAINT [PK_ManagementStandard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
----------------
CREATE TABLE [dbo].[ManagementStandardTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ManagementStandardId] [int] NOT NULL,
	[Standard] [nvarchar](MAX) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_ManagementStandardTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ManagementStandardTranslation]  WITH CHECK ADD  CONSTRAINT [FK_ManagementStandardTranslation_ManagementStandard] FOREIGN KEY([ManagementStandardId])
REFERENCES [dbo].[ManagementStandard] ([Id])
ALTER TABLE [dbo].[ManagementStandardTranslation] CHECK CONSTRAINT [FK_ManagementStandardTranslation_ManagementStandard]
--------------------------

CREATE TABLE [dbo].[ManagementRate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollTeacherCourseId] [int] NULL,
	[Percent] decimal(18,2) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL
 CONSTRAINT [PK_ManagementRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[ManagementRate]  WITH CHECK ADD  CONSTRAINT [FK_ManagementRate_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
ALTER TABLE [dbo].[ManagementRate] CHECK CONSTRAINT [FK_ManagementRate_EnrollTeacherCourse]

---------------------------

CREATE TABLE [dbo].[ManagementRateLine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ManagementRateId] [int] NULL,
	[StandardId] int NULL,
    [Value] [nvarchar](MAX) NULL,
 CONSTRAINT [PK_ManagementRateLine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 


ALTER TABLE [dbo].[ManagementRateLine]  WITH CHECK ADD  CONSTRAINT [FK_ManagementRateLine_ManagementRate] FOREIGN KEY([ManagementRateId])
REFERENCES [dbo].[ManagementRate] ([Id])
ALTER TABLE [dbo].[ManagementRateLine] CHECK CONSTRAINT [FK_ManagementRateLine_ManagementRate]

ALTER TABLE [dbo].[ManagementRateLine]  WITH CHECK ADD  CONSTRAINT [FK_ManagementRateLine_Standard] FOREIGN KEY([StandardId])
REFERENCES [dbo].[ManagementStandard] ([Id])
ALTER TABLE [dbo].[ManagementRateLine] CHECK CONSTRAINT [FK_ManagementRateLine_Standard]
