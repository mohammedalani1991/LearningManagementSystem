INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsProject', N'View', N'View', 1, CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsProject', N'Create', N'Create', 1, CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsProject', N'Edit', N'Edit', 1, CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsProject', N'Delete', N'Delete', 1, CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)





INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsCategoryProject', N'View', N'View', 1, CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsCategoryProject', N'Create', N'Create', 1, CAST(N'2021-12-02T00:03:40.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsCategoryProject', N'Edit', N'Edit', 1, CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CmsCategoryProject', N'Delete', N'Delete', 1, CAST(N'2021-12-02T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)




if not exists (select * from MasterLookup where Code='PaymentTypeForProject')
begin
INSERT into MasterLookup (CreatedOn,CreatedBy,Status,Name,Code)
VALUES (getdate(),'salam-cs@hotmail.com',1,'Payment Type For Project','PaymentTypeForProject')
end


if not exists (select * from MasterLookup where Code='ProjectStatus')
begin
INSERT into MasterLookup (CreatedOn,CreatedBy,Status,Name,Code)
VALUES (getdate(),'salam-cs@hotmail.com',1,'Project Status','ProjectStatus')
end


 ALTER TABLE [CmsProject] ALTER COLUMN [SortOrder] int null

   ALTER TABLE [CmsProjectTranslation] ALTER COLUMN [Description] nvarchar(MAX) null
  ALTER TABLE [CmsProject] ALTER COLUMN [Description] nvarchar(MAX) null