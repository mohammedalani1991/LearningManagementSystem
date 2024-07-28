IF OBJECT_ID(N'dbo.CalenderTranslation', N'U') IS NOT NULL  
   DROP TABLE [dbo].[CalenderTranslation];  


IF OBJECT_ID(N'dbo.Calender', N'U') IS NOT NULL  
   DROP TABLE [dbo].[Calender];  


IF OBJECT_ID(N'dbo.Calendar', N'U') IS NULL  and   OBJECT_ID(N'dbo.CalendarTranslation', N'U') IS NULL 
begin
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


ALTER TABLE [dbo].[CalendarTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CalendarTranslation_Calendar] FOREIGN KEY([CalendarId])
REFERENCES [dbo].[Calendar] ([Id])

ALTER TABLE [dbo].[CalendarTranslation] CHECK CONSTRAINT [FK_CalendarTranslation_Calendar]

end