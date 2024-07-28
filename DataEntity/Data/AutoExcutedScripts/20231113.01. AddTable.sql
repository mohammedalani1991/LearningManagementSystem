
CREATE TABLE [dbo].[PracticalEnrollmentExamTrainer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PracticalEnrollmentExamId] [int] NULL,
	[TrainerId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_PracticalEnrollmentExamTrainer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[PracticalEnrollmentExamTrainer]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamTrainer_PracticalEnrollmentExam] FOREIGN KEY([PracticalEnrollmentExamId])
REFERENCES [dbo].[PracticalEnrollmentExam] ([Id])
ALTER TABLE [dbo].[PracticalEnrollmentExamTrainer] CHECK CONSTRAINT [FK_PracticalEnrollmentExamTrainer_PracticalEnrollmentExam]

ALTER TABLE [dbo].[PracticalEnrollmentExamTrainer]  WITH CHECK ADD  CONSTRAINT [FK_PracticalEnrollmentExamTrainer_Trainer] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainer] ([Id])
ALTER TABLE [dbo].[PracticalEnrollmentExamTrainer] CHECK CONSTRAINT [FK_PracticalEnrollmentExamTrainer_Trainer]



INSERT [dbo].[Permission] ( [PageUrl], [PageName], [PermissionKey], [Description], [ModuleId], [CreatedOn], [CreatedBy], [Status], [DeletedOn]) VALUES ( N'', N'PracticalEnrollmentExam', N'AddTrainer', N'Add Trainer', 1, CAST(N'2022-09-05T00:00:00.000' AS DateTime), N'salam-cs@hotmail.com', 1, NULL)
INSERT [dbo].[RolePermissions] ( [RoleId], [PermissionId]) VALUES ( N'1', @@IDENTITY)
