
Declare @PermissionAddedID int =0;
SET IDENTITY_INSERT [dbo].[Permission] ON ;

IF NOT EXISTS(select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='View')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (@PermissionAddedID, N'', N'CmsSliders', N'View', N'View', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Create')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (@PermissionAddedID, N'', N'CmsSliders', N'Create', N'Create', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Edit')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (@PermissionAddedID, N'', N'CmsSliders', N'Edit', N'Edit', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Delete')
begin

set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (@PermissionAddedID, N'', N'CmsSliders', N'Delete', N'Delete', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)
end

IF NOT EXISTS (select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Select')
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn], [ModuleId]) VALUES (@PermissionAddedID, N'', N'CmsSliders', N'Select', N'Select Slider', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL, 1)

end


SET IDENTITY_INSERT [dbo].[Permission] OFF ;


SET IDENTITY_INSERT [dbo].[RolePermissions] ON ;


set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='View')
IF NOT EXISTS(select [Id] from  [dbo].[RolePermissions] where PermissionId=@PermissionAddedID)
begin
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES ((select max(id) + 1 from  [dbo].[RolePermissions]), N'1', @PermissionAddedID)
end

set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Create')
IF NOT EXISTS(select [Id] from  [dbo].[RolePermissions] where PermissionId=@PermissionAddedID)
begin
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES ((select max(id) + 1 from  [dbo].[RolePermissions]), N'1', @PermissionAddedID)
end

set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Edit')
IF NOT EXISTS(select [Id] from  [dbo].[RolePermissions] where PermissionId=@PermissionAddedID)
begin
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES ((select max(id) + 1 from  [dbo].[RolePermissions]), N'1', @PermissionAddedID)
end

set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Delete')
IF NOT EXISTS(select [Id] from  [dbo].[RolePermissions] where PermissionId=@PermissionAddedID)
begin
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES ((select max(id) + 1 from  [dbo].[RolePermissions]), N'1', @PermissionAddedID)
end

set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='CmsSliders' and [PermissionKey]='Select')
IF NOT EXISTS(select [Id] from  [dbo].[RolePermissions] where PermissionId=@PermissionAddedID)
begin
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES ((select max(id) + 1 from  [dbo].[RolePermissions]), N'1', @PermissionAddedID)
end


SET IDENTITY_INSERT [dbo].[RolePermissions] OFF ;








