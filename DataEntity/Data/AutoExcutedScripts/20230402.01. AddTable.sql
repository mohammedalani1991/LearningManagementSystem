
CREATE TABLE [dbo].[SectionOfCourseQuiz](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [CreatedBy] [nvarchar](256) NOT NULL,
    [Status] [int] NOT NULL,
    [DeletedOn] [datetime] NULL,
    [SectionOfCourseId] [int] NULL,
    [LectureId] [int] NULL,
    [QuestionOne] [bit] NULL,
    [QuestionTwo] [bit] NULL,
    [QuestionThree] [bit] NULL,
 CONSTRAINT [PK_SectionOfCourseQuiz] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[SectionOfCourseQuiz]  WITH CHECK ADD  CONSTRAINT [FK_SectionOfCourseQuiz_SectionOfCourse] FOREIGN KEY([SectionOfCourseId])
REFERENCES [dbo].[SectionOfCourse] ([Id])

ALTER TABLE [dbo].[SectionOfCourseQuiz] CHECK CONSTRAINT [FK_SectionOfCourseQuiz_SectionOfCourse]

ALTER TABLE [dbo].[SectionOfCourseQuiz]  WITH CHECK ADD  CONSTRAINT [FK_SectionOfCourseQuiz_Lecture] FOREIGN KEY([LectureId])
REFERENCES [dbo].[Lecture] ([Id])

ALTER TABLE [dbo].[SectionOfCourseQuiz] CHECK CONSTRAINT [FK_SectionOfCourseQuiz_Lecture]

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CoursesContent', N'AddAndEditQuiz', N'AddAndEditQuiz', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
