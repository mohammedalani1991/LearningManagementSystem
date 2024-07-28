
CREATE TABLE [dbo].[CmsWhatPeopleSay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonName] [nvarchar](max) NOT NULL,
	[PersonDetails] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[ShowInHomePage] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_CmsWhatPeopleSay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[CmsWhatPeopleSayTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PeopleId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[PersonName] [nvarchar](max) NOT NULL,
	[PersonDetails] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsWhatPeopleSayTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[CmsWhatPeopleSayTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CmsWhatPeopleSayTranslation_CmsWhatPeopleSay] FOREIGN KEY([PeopleId])
REFERENCES [dbo].[CmsWhatPeopleSay] ([Id])
ALTER TABLE [dbo].[CmsWhatPeopleSayTranslation] CHECK CONSTRAINT [FK_CmsWhatPeopleSayTranslation_CmsWhatPeopleSay]
