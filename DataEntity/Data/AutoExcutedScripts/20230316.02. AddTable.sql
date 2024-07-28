IF COL_LENGTH('Subject','ExamTypeId') IS NULL
BEGIN
ALTER TABLE  [dbo].[Subject] add  [ExamTypeId]  [int]  NULL;
END

CREATE TABLE [dbo].[PracticalExamCourseSubject](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [CreatedBy] [nvarchar](256) NOT NULL,
    [Status] [int] NOT NULL,
    [DeletedOn] [datetime] NULL,
    [PracticalExamCourseId] [int] NULL,
    [SubjectId] [int] NULL,
 CONSTRAINT [PK_PracticalExamCourseSubject] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PracticalExamCourseSubject]  WITH CHECK ADD  CONSTRAINT [FK_PracticalExamCourseSubject_PracticalExamCourse] FOREIGN KEY([PracticalExamCourseId])
REFERENCES [dbo].[PracticalExamCourse] ([Id])

ALTER TABLE [dbo].[PracticalExamCourseSubject] CHECK CONSTRAINT [FK_PracticalExamCourseSubject_PracticalExamCourse]

ALTER TABLE [dbo].[PracticalExamCourseSubject]  WITH CHECK ADD  CONSTRAINT [FK_PracticalExamCourseSubject_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])

ALTER TABLE [dbo].[PracticalExamCourseSubject] CHECK CONSTRAINT [FK_PracticalExamCourseSubject_Subject]

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'View', N'View', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'AddExam', N'AddExam', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID1 int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID1)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'AddStudent', N'AddStudent', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID2 int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID2)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID2)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'AddPoints', N'AddPoints', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID3 int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID3)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID3)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Courses', N'AddSubjects', N'AddSubjects', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'EditExam', N'EditExam', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'DeleteExam', N'DeleteExam', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)