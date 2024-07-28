
Declare @PermissionAddedID int =0;
SET IDENTITY_INSERT [dbo].[Permission] ON ;


IF NOT EXISTS(select [Id] from  [dbo].[Permission] where [PageName]='EnrollCoursesContent' and [PermissionKey]='View')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (@PermissionAddedID, N'', N'EnrollCoursesContent', N'View', N'View', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='EnrollCoursesContent' and [PermissionKey]='Create')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (@PermissionAddedID, N'', N'EnrollCoursesContent', N'Create', N'Create', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='EnrollCoursesContent' and [PermissionKey]='Edit')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (@PermissionAddedID, N'', N'EnrollCoursesContent', N'Edit', N'Edit', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='EnrollCoursesContent' and [PermissionKey]='Delete')
begin

set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (@PermissionAddedID, N'', N'EnrollCoursesContent', N'Delete', N'Delete', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='EnrollCoursesContent' and [PermissionKey]='Select')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (@PermissionAddedID, N'', N'EnrollCoursesContent', N'Select', N'Select Calendar', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)

end


SET IDENTITY_INSERT [dbo].[Permission] OFF ;









