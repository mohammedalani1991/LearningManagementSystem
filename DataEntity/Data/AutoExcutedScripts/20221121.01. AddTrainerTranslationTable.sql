    alter table Trainer add [Description] nvarchar(max)


  
  alter table Employee drop column [Description]
  alter table EmployeeTranslation drop column [Description]


  CREATE TABLE [dbo].[TrainerTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrainerId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_TrainerTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[TrainerTranslation]  WITH CHECK ADD  CONSTRAINT [FK_TrainerTranslation_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainer] ([Id])


ALTER TABLE [dbo].[TrainerTranslation] CHECK CONSTRAINT [FK_TrainerTranslation_Trainer]
