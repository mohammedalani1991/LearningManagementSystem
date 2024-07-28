  
  CREATE TABLE [dbo].[EnrollStudentCourseAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollStudentCourseId] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[FileAttached] [nvarchar](500) NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_EnrollStudentCourseAttachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EnrollStudentCourseAttachment]  WITH CHECK ADD  CONSTRAINT [FK_EnrollStudentCourseAttachment_EnrollStudentCourse] FOREIGN KEY([EnrollStudentCourseId])
REFERENCES [dbo].[EnrollStudentCourse] ([Id])

ALTER TABLE [dbo].[EnrollStudentCourseAttachment] CHECK CONSTRAINT [FK_EnrollStudentCourseAttachment_EnrollStudentCourse]



  
  
  
  alter table Employee add [Description] nvarchar(max)
  alter table EmployeeTranslation add [Description] nvarchar(max)



  alter table Course add NeedQuestion bit
  alter table Course add QuestionDescription nvarchar(max)
  alter table CourseTranslation add QuestionDescription nvarchar(max)



