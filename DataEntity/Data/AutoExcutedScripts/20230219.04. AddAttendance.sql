CREATE TABLE [dbo].[CourseAttendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollStudentCourseId] [int] NULL,
	[Date] [datetime] NULL,
	[IsPresent] [bit] NULL,
	[AbsenceNote] [nvarchar](1000) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_CourseAttendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[CourseAttendance]  WITH CHECK ADD  CONSTRAINT [FK_CourseAttendance_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])

ALTER TABLE [dbo].[CourseAttendance] CHECK CONSTRAINT [FK_CourseAttendance_EnrollStudentCourse]