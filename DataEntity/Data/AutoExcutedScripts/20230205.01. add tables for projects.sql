
CREATE TABLE [dbo].[CmsProjectCost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[ProjectId] [int] NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Cost] [float] NOT NULL,
	[IsOther] [bit] NULL,
 CONSTRAINT [PK_CmsProjectCost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[CmsProjectCostTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectCostId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CmsProjectCostTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[CmsProjectDonor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Mobile] [nvarchar](50) NULL,
	[Email] [nvarchar](250) NULL,
	[Cost] [float] NOT NULL,
	[ProjectId] [int] NULL,
	[ProjectCostId] [int] NULL,
 CONSTRAINT [PK_CmsProjectDonor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[CmsProjectCost]  WITH CHECK ADD  CONSTRAINT [FK_CmsProjectCost_CmsProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[CmsProject] ([Id])

ALTER TABLE [dbo].[CmsProjectCost] CHECK CONSTRAINT [FK_CmsProjectCost_CmsProject]

ALTER TABLE [dbo].[CmsProjectCostTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CmsProjectCostTranslation_CmsProjectCost] FOREIGN KEY([ProjectCostId])
REFERENCES [dbo].[CmsProjectCost] ([Id])

ALTER TABLE [dbo].[CmsProjectCostTranslation] CHECK CONSTRAINT [FK_CmsProjectCostTranslation_CmsProjectCost]

ALTER TABLE [dbo].[CmsProjectDonor]  WITH CHECK ADD  CONSTRAINT [FK_CmsProjectDonor_CmsProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[CmsProject] ([Id])

ALTER TABLE [dbo].[CmsProjectDonor] CHECK CONSTRAINT [FK_CmsProjectDonor_CmsProject]


alter table cmsproject drop column branchId


alter table cmsproject add TargetQty int

alter table cmsproject add SecondDescription	nvarchar(MAX)

alter table cmsprojecttranslation add SecondDescription	nvarchar(MAX)

