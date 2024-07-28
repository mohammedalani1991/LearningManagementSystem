
INSERT [dbo].[SuperAdmin] ( [Name], [Show]) VALUES ( N'MarkAdoptionForPracticalExam', 1)

CREATE TABLE [dbo].[MarkAdoptionForPracticalExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PracticalExamId] [int] NULL,
	[EnrollTeacherCourseId] [int] NULL,
	[Value] [bit] NULL,
	CONSTRAINT [PK_MarkAdoptionForPracticalExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[MarkAdoptionForPracticalExam]  WITH CHECK ADD  CONSTRAINT [FK_MarkAdoptionForPracticalExam_PracticalExam] FOREIGN KEY([PracticalExamId])
REFERENCES [dbo].[PracticalExam] ([Id])
ALTER TABLE [dbo].[MarkAdoptionForPracticalExam] CHECK CONSTRAINT [FK_MarkAdoptionForPracticalExam_PracticalExam]

ALTER TABLE [dbo].[MarkAdoptionForPracticalExam]  WITH CHECK ADD  CONSTRAINT [FK_MarkAdoptionForPracticalExam_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
ALTER TABLE [dbo].[MarkAdoptionForPracticalExam] CHECK CONSTRAINT [FK_MarkAdoptionForPracticalExam_EnrollTeacherCourse]

IF COL_LENGTH('PracticalEnrollmentExam','MarkAdoption') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalEnrollmentExam] add  [MarkAdoption] [bit] NULL;
END

CREATE TABLE [dbo].[MarkAdoptionForExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamTemplateId] [int]  NULL,
	[EnrollTeacherCourseId] [int] NULL,
	[Value] [bit] NULL,
	CONSTRAINT [PK_MarkAdoptionForExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[MarkAdoptionForExam]  WITH CHECK ADD  CONSTRAINT [FK_MarkAdoptionForExam_ExamTemplate] FOREIGN KEY([ExamTemplateId])
REFERENCES [dbo].[ExamTemplate] ([Id])
ALTER TABLE [dbo].[MarkAdoptionForExam] CHECK CONSTRAINT [FK_MarkAdoptionForExam_ExamTemplate]

ALTER TABLE [dbo].[MarkAdoptionForExam]  WITH CHECK ADD  CONSTRAINT [FK_MarkAdoptionForExam_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
ALTER TABLE [dbo].[MarkAdoptionForExam] CHECK CONSTRAINT [FK_MarkAdoptionForExam_EnrollTeacherCourse]

IF COL_LENGTH('EnrollCourseExam','MarkAdoption') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollCourseExam] add  [MarkAdoption] [bit] NULL;
END


INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'MarkAdoptionForPracticalExam', N'View', N'View', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'MarkAdoptionForPracticalExam', N'Edit', N'Edit', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'MarkAdoptionForExam', N'View', N'View', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'MarkAdoptionForExam', N'Edit', N'Edit', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'MarkAdoption', N'MarkAdoption', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'EnrollCourseExam', N'MarkAdoption', N'MarkAdoption', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'EnrollmentCourse', N'ExpulsionsHistory', N'ExpulsionsHistory', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID1 int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID1)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID1)