CREATE TABLE [dbo].[SubjectTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SubjectTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[SubjectTranslation]  WITH CHECK ADD  CONSTRAINT [FK_SubjectTranslation_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])

ALTER TABLE [dbo].[SubjectTranslation] CHECK CONSTRAINT [FK_SubjectTranslation_Subject]