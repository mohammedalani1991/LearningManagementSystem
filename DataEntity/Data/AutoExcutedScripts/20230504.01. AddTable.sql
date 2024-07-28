
CREATE TABLE [dbo].[StudentBalanceHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[EnrollStudentCourseId] [int] NULL,
	[Amount] [decimal](8, 2) NULL,
	[Balance] [decimal](8, 2) NULL,
	[Title] [nvarchar](256) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_StudentBalanceHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[StudentBalanceHistory]  WITH CHECK ADD  CONSTRAINT [FK_StudentBalanceHistory_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
ALTER TABLE [dbo].[StudentBalanceHistory] CHECK CONSTRAINT [FK_StudentBalanceHistory_Student]

ALTER TABLE [dbo].[StudentBalanceHistory]  WITH CHECK ADD  CONSTRAINT [FK_StudentBalanceHistory_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])
ALTER TABLE [dbo].[StudentBalanceHistory] CHECK CONSTRAINT [FK_StudentBalanceHistory_EnrollStudentCourse]


INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'EnrollmentCourse', N'DelayStudent', N'DelayStudent', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)