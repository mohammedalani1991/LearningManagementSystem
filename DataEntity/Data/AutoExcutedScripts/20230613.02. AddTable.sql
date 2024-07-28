CREATE TABLE [dbo].[StudentExpulsionHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int]  NULL,
	[EnrollStudentCourseId] [int]  NULL,
	[TypeId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	CONSTRAINT [PK_StudentExpulsionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[StudentExpulsionHistory]  WITH CHECK ADD  CONSTRAINT [FK_StudentExpulsionHistory_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
ALTER TABLE [dbo].[StudentExpulsionHistory] CHECK CONSTRAINT [FK_StudentExpulsionHistory_Student]

ALTER TABLE [dbo].[StudentExpulsionHistory]  WITH CHECK ADD  CONSTRAINT [FK_StudentExpulsionHistory_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])
ALTER TABLE [dbo].[StudentExpulsionHistory] CHECK CONSTRAINT [FK_StudentExpulsionHistory_EnrollStudentCourse]