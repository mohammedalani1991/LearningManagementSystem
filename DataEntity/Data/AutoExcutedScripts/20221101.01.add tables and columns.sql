
  
  
  
  
  
  
 
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[CityTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_CityTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[CountryTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_CountryTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])

ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Country]

ALTER TABLE [dbo].[CityTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CityTranslation_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])

ALTER TABLE [dbo].[CityTranslation] CHECK CONSTRAINT [FK_CityTranslation_City]

ALTER TABLE [dbo].[CountryTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CountryTranslation_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])

ALTER TABLE [dbo].[CountryTranslation] CHECK CONSTRAINT [FK_CountryTranslation_Country]





  alter table Contact add CountryId int
  alter table Contact add CityId int
  alter table Contact add [Language] int
  alter table Contact add [NationalityId] int




  ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_City]




ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Country]