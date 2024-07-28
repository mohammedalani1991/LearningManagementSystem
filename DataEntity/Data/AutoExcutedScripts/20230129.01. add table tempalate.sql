
CREATE TABLE [dbo].[TemplateHtml](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NOT NULL,
	[RanderHtml] [nvarchar](max) NULL,
	[Code] [nvarchar](100) NOT NULL,
	[TypeId] [int] NOT NULL,

 CONSTRAINT [PK_TemplateHtml] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[CourseCertification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[TemplateHtmlId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,

 CONSTRAINT [PK_CourseCertification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[TemplateHtml]  WITH CHECK ADD  CONSTRAINT [FK_TemplateHtml_DetailsLookup] FOREIGN KEY([TypeId])
REFERENCES [dbo].[DetailsLookup] ([Id])

ALTER TABLE [dbo].[TemplateHtml] CHECK CONSTRAINT [FK_TemplateHtml_DetailsLookup]




ALTER TABLE [dbo].[CourseCertification]  WITH CHECK ADD  CONSTRAINT [FK_CourseCertification_TemplateHtml] FOREIGN KEY([TemplateHtmlId])
REFERENCES [dbo].[TemplateHtml] ([Id])

ALTER TABLE [dbo].[CourseCertification] CHECK CONSTRAINT [FK_CourseCertification_TemplateHtml]


ALTER TABLE [dbo].[CourseCertification]  WITH CHECK ADD  CONSTRAINT [FK_CourseCertification_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[CourseCertification] CHECK CONSTRAINT [FK_CourseCertification_Course]