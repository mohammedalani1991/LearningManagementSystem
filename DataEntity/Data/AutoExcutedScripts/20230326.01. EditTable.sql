IF COL_LENGTH('PracticalExamCourse','SubjectMark') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalExamCourse] add  [SubjectMark]  [int] NULL;
END

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Courses', N'EditSubjectMark', N'EditSubjectMark', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)