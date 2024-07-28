Declare @PermissionAddedID int =0;


INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CourseAttendances', N'View', N'View', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)

set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='CourseAttendances' and [PermissionKey]='View')

INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @PermissionAddedID)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @PermissionAddedID)


INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CourseAttendances', N'Edit', N'Edit', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)

set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='CourseAttendances' and [PermissionKey]='Edit')

INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @PermissionAddedID)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @PermissionAddedID)





