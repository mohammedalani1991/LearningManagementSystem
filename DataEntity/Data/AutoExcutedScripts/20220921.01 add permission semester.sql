﻿
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Semester', N'View', N'View', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Semester', N'Create', N'Create', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Semester', N'Edit', N'Edit', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Semester', N'Delete', N'Delete', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Semester', N'Details', N'Details', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)



INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Area', N'ManageCourses', N'ManageCMS', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
