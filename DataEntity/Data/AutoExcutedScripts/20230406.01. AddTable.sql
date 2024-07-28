
CREATE TABLE [dbo].[EnrollCourseQuizPointes](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [CreatedBy] [nvarchar](256) NOT NULL,
    [Status] [int] NOT NULL,
    [DeletedOn] [datetime] NULL,
    [EnrollStudentCourseId] [int] NULL,
    [EnrollCourseQuizId] [int] NULL,
    [QuestionOne] [decimal](8, 2) NULL,
    [QuestionTwo] [decimal](8, 2) NULL,
    [QuestionThree] [decimal](8, 2) NULL,
 CONSTRAINT [PK_EnrollCourseQuizPointes] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EnrollCourseQuizPointes]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseQuizPointes_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])

ALTER TABLE [dbo].[EnrollCourseQuizPointes] CHECK CONSTRAINT [FK_EnrollCourseQuizPointes_EnrollStudentCourse]

ALTER TABLE [dbo].[EnrollCourseQuizPointes]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseQuizPointes_EnrollCourseQuiz] FOREIGN KEY([EnrollCourseQuizId])
REFERENCES [dbo].[EnrollCourseQuiz] ([Id])

ALTER TABLE [dbo].[EnrollCourseQuizPointes] CHECK CONSTRAINT [FK_EnrollCourseQuizPointes_EnrollCourseQuiz]

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'EnrollCourseQuiz', N'AddAndEditQuizPointes', N'AddAndEditQuizPointes', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID)