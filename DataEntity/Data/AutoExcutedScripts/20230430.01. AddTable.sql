
CREATE TABLE [dbo].[Expulsion](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [ExpelledFrom] [datetime] NOT NULL,
    [ExpelledTo] [datetime] NOT NULL,
    [ExpulsionStart] [datetime] NOT NULL,
    [ExpulsionEnd] [datetime] NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [CreatedBy] [nvarchar](256) NOT NULL,
    [Status] [int] NOT NULL,
    [DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Expulsion] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Expulsion', N'View', N'View', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Expulsion', N'Create', N'Create', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Expulsion', N'Edit', N'Edit', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'Expulsion', N'Delete', N'Delete', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'EnrollmentCourse', N'ExpelStudent', N'ExpelStudent', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID)