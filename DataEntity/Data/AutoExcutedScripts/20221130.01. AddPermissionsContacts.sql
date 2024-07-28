
Declare @PermissionAddedID int =0;
SET IDENTITY_INSERT [dbo].[Permission] ON ;

IF NOT EXISTS(select [Id] from  [dbo].[Permission] where [PageName]='UserProfiles' and [PermissionKey]='VerifyEmail' and [Status]=1)
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (@PermissionAddedID, N'', N'UserProfiles', N'VerifyEmail', N'VerifyEmail', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
end


SET IDENTITY_INSERT [dbo].[Permission] OFF ;

SET IDENTITY_INSERT [dbo].[RolePermissions] ON ;


set @PermissionAddedID =(select [Id] from  [dbo].[Permission] where [PageName]='UserProfiles' and [PermissionKey]='VerifyEmail')
IF NOT EXISTS(select [Id] from  [dbo].[RolePermissions] where PermissionId=@PermissionAddedID)
begin
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PermissionId]) VALUES ((select max(id) + 1 from  [dbo].[RolePermissions]), N'1', @PermissionAddedID)
end


SET IDENTITY_INSERT [dbo].[RolePermissions] OFF ;
