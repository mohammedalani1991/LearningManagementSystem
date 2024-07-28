Drop Table EnrollStudentAlert

CREATE TABLE [dbo].[EnrollStudentAlert](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[AlertTypeId] [int]  NULL,
	[EnrollStudentCourseId] [int] NULL,
	[EnrollTeacherCourseId] [int] NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_EnrollStudentAlert] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[EnrollStudentAlert]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentAlert_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])
ALTER TABLE [dbo].[EnrollStudentAlert] CHECK CONSTRAINT [FK_EnrollStudentAlert_EnrollStudentCourse]

ALTER TABLE [dbo].[EnrollStudentAlert]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentAlert_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
ALTER TABLE [dbo].[EnrollStudentAlert] CHECK CONSTRAINT [FK_EnrollStudentAlert_EnrollTeacherCourse]