
CREATE TABLE [dbo].[EnrollCourseQuiz](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [CreatedBy] [nvarchar](256) NOT NULL,
    [Status] [int] NOT NULL,
    [DeletedOn] [datetime] NULL,
    [EnrollTeacherCourseId] [int] NULL,
    [LectureId] [int] NULL,
    [QuestionOne] [bit] NULL,
    [QuestionTwo] [bit] NULL,
    [QuestionThree] [bit] NULL,
 CONSTRAINT [PK_EnrollCourseQuiz] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EnrollCourseQuiz]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseQuiz_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollCourseQuiz] CHECK CONSTRAINT [FK_EnrollCourseQuiz_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollCourseQuiz]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseQuiz_Lecture] FOREIGN KEY([LectureId])
REFERENCES [dbo].[Lecture] ([Id])

ALTER TABLE [dbo].[EnrollCourseQuiz] CHECK CONSTRAINT [FK_EnrollCourseQuiz_Lecture]

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'EnrollCourseQuiz', N'AddAndEditQuiz', N'AddAndEditQuiz', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID)