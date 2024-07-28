
Declare @PermissionAddedID int =0;
SET IDENTITY_INSERT [dbo].[Permission] ON ;

IF NOT EXISTS(select [Id] from  [dbo].[Permission] where [PageName]='TrainerCourse' and [PermissionKey]='View' and [Status]=1)
begin

 set @PermissionAddedID =(select max(id) + 1 from  [dbo].[Permission])
INSERT [dbo].[Permission] ([Id], [PageUrl], [PageName], [PermissionKey], [Description], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES (@PermissionAddedID, N'', N'TrainerCourse', N'View', N'View', CAST(N'2022-08-16T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
end


SET IDENTITY_INSERT [dbo].[Permission] OFF ;
