
CREATE TABLE [dbo].[CertificateAdoption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NULL,
	[SemesterId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_CertificateAdoption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[CertificateAdoption]  WITH CHECK ADD  CONSTRAINT [FK_CertificateAdoption_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ALTER TABLE [dbo].[CertificateAdoption] CHECK CONSTRAINT [FK_CertificateAdoption_Course]

ALTER TABLE [dbo].[CertificateAdoption]  WITH CHECK ADD  CONSTRAINT [FK_CertificateAdoption_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
ALTER TABLE [dbo].[CertificateAdoption] CHECK CONSTRAINT [FK_CertificateAdoption_Semester]

IF COL_LENGTH('EnrollTeacherCourse','CertificateAdoption') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollTeacherCourse] add  [CertificateAdoption] [bit] NULL;
END


INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CertificateAdoption', N'View', N'View', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CertificateAdoption', N'Create', N'Create', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID1 int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID1)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'EnrollmentCourse', N'EditCertificateAdoption', N'EditCertificateAdoption', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID2 int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID2)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'5', @ID2)

INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'CertificateAdoption', N'Delete', N'Delete', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
Declare @ID3 int =@@IDENTITY;
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @ID3)
