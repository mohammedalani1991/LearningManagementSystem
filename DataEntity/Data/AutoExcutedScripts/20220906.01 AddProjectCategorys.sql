CREATE TABLE [dbo].[CmsProjectCategory](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Name] [nvarchar](max) NOT NULL,
[Description] [nvarchar](max) NOT NULL,
[CreatedOn] [datetime] NOT NULL,
[CreatedBy] [nvarchar](256) NOT NULL,
[Status] [int] NOT NULL,
[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_CmsProjectCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[CmsProjectCategoryTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectCategoryId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsProjectCategoryTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]




ALTER TABLE [dbo].[CmsProjectCategoryTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CmsProjectCategoryTranslation_CmsProjectCategory] FOREIGN KEY([ProjectCategoryId])
REFERENCES [dbo].[CmsProjectCategory] ([Id])

ALTER TABLE [dbo].[CmsProjectCategoryTranslation] CHECK CONSTRAINT [FK_CmsProjectCategoryTranslation_CmsProjectCategory]



Alter Table [dbo].[CmsProject] Add  [ProjectCategoryId] int null 

ALTER TABLE [dbo].[CmsProject]  WITH CHECK ADD  CONSTRAINT [FK_CmsProject_CmsProjectCategory] FOREIGN KEY([ProjectCategoryId])
REFERENCES [dbo].[CmsProjectCategory] ([Id])

ALTER TABLE [dbo].[CmsProject] CHECK CONSTRAINT [FK_CmsProject_CmsProjectCategory]



Alter Table [dbo].[CmsProjectTranslation] Add  [ShortDescription] nvarchar(max) null 


Alter Table [dbo].[CmsProject] Add  [BranchId] int null 

ALTER TABLE [CmsProjectCategory] ALTER COLUMN [Description] nvarchar(MAX) NULL
Alter Table [dbo].[CmsProjectCategoryTranslation] Add  [Description] nvarchar(max) null 
