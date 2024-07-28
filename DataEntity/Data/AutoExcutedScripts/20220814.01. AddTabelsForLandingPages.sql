CREATE TABLE [dbo].[Calendar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Name] [nvarchar](500) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Calendar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[CalendarTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CalendarId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CalendarTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[CmsCatery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[ShowInHomePage] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_CmsCatery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[CmsCateryTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CateryId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsCateryTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[CmsPage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[CateryId] [int] NULL,
	[Keyword] [nvarchar](max) NULL,
	[AllowComment] [bit] NULL,
	[PublishDate] [datetime] NULL,
	[EndDate] [datetime2](7) NULL,
	[SortOrder] [nchar](10) NULL,
	[ShowInHomePage] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[IsFeatured] [bit] NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_CmsPage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[CmsPageTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Keyword] [nvarchar](max) NULL,
 CONSTRAINT [PK_CmsPageTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[CmsProject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Keyword] [nvarchar](max) NULL,
	[PublishDate] [datetime] NULL,
	[EndDate] [datetime2](7) NULL,
	[SortOrder] [nchar](10) NULL,
	[ShowInHomePage] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[IsFeatured] [bit] NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[PaymentType] [int] NULL,
	[ProjectCost] [float] NULL,
	[OneObjectFees] [float] NULL,
	[ProjectStatus] [int] NULL,
 CONSTRAINT [PK_CmsProject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[CmsProjectResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Link] [nvarchar](max) NULL,
	[Type] [int] NULL,
	[ProjectId] [int] NULL,
 CONSTRAINT [PK_CmsProjectResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[CmsProjectTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Keyword] [nvarchar](max) NULL,
 CONSTRAINT [PK_CmsProjectTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[CmsSlider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[ReadMoreLink] [varchar](max) NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_CmsSlider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[CmsSliderTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SliderId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsSliderTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[CalendarTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CalendarTranslation_Calendar] FOREIGN KEY([CalendarId])
REFERENCES [dbo].[Calendar] ([Id])

ALTER TABLE [dbo].[CalendarTranslation] CHECK CONSTRAINT [FK_CalendarTranslation_Calendar]

ALTER TABLE [dbo].[CmsCatery]  WITH CHECK ADD  CONSTRAINT [FK_CmsCatery_CmsCatery] FOREIGN KEY([ParentId])
REFERENCES [dbo].[CmsCatery] ([Id])

ALTER TABLE [dbo].[CmsCatery] CHECK CONSTRAINT [FK_CmsCatery_CmsCatery]

ALTER TABLE [dbo].[CmsCateryTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CmsCateryTranslation_CmsCatery] FOREIGN KEY([CateryId])
REFERENCES [dbo].[CmsCatery] ([Id])

ALTER TABLE [dbo].[CmsCateryTranslation] CHECK CONSTRAINT [FK_CmsCateryTranslation_CmsCatery]

ALTER TABLE [dbo].[CmsPage]  WITH CHECK ADD  CONSTRAINT [FK_CmsPage_CmsCatery] FOREIGN KEY([CateryId])
REFERENCES [dbo].[CmsCatery] ([Id])

ALTER TABLE [dbo].[CmsPage] CHECK CONSTRAINT [FK_CmsPage_CmsCatery]

ALTER TABLE [dbo].[CmsPageTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CmsPageTranslation_CmsPage] FOREIGN KEY([PageId])
REFERENCES [dbo].[CmsPage] ([Id])

ALTER TABLE [dbo].[CmsPageTranslation] CHECK CONSTRAINT [FK_CmsPageTranslation_CmsPage]

ALTER TABLE [dbo].[CmsProjectResource]  WITH CHECK ADD  CONSTRAINT [FK_CmsProjectResource_CmsProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[CmsProject] ([Id])

ALTER TABLE [dbo].[CmsProjectResource] CHECK CONSTRAINT [FK_CmsProjectResource_CmsProject]

ALTER TABLE [dbo].[CmsProjectTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CmsProjectTranslation_CmsProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[CmsProject] ([Id])

ALTER TABLE [dbo].[CmsProjectTranslation] CHECK CONSTRAINT [FK_CmsProjectTranslation_CmsProject]

ALTER TABLE [dbo].[CmsSliderTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CmsSliderTranslation_CmsSlider] FOREIGN KEY([SliderId])
REFERENCES [dbo].[CmsSlider] ([Id])

ALTER TABLE [dbo].[CmsSliderTranslation] CHECK CONSTRAINT [FK_CmsSliderTranslation_CmsSlider]

