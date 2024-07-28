
CREATE TABLE [dbo].[EnrollStudentCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Price] [nchar](10) NULL,
	[IsPass] [bit] NULL,
	[Mark] [float] NULL,
	[NeedApproval] [bit] NULL,
 CONSTRAINT [PK_EnrollStudentCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EnrollStudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentCourse_EnrollTeacherCourse] FOREIGN KEY([CourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollStudentCourse] CHECK CONSTRAINT [FK_EnrollStudentCourse_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollStudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentCourse_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])

ALTER TABLE [dbo].[EnrollStudentCourse] CHECK CONSTRAINT [FK_EnrollStudentCourse_Student]

