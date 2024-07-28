
CREATE TABLE [dbo].[CourseMarkTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseMarkId] [int]  NULL,
	[Title] [nvarchar](256) NULL,
	[LanguageId] [int] NOT NULL,
	CONSTRAINT [PK_CourseMarkTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[CourseMarkTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CourseMarkTranslation_CourseMarks] FOREIGN KEY([CourseMarkId])
REFERENCES [dbo].[CourseMarks] ([Id])
ALTER TABLE [dbo].[CourseMarkTranslation] CHECK CONSTRAINT [FK_CourseMarkTranslation_CourseMarks]