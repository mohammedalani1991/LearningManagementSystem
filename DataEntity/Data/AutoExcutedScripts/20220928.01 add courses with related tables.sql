
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CourseName] [nvarchar](200) NOT NULL,
	[CourseDuration] [int] NULL,
	[CoursePrice] [int] NULL,
	[AcquiredSkills] [nvarchar](max) NULL,
	[TargetGroup] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[Requirements] [nvarchar](max) NULL,
	[CategoryId] [int] NULL,
	[LearningMethodId] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[CourseCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[ShowInHomePage] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_CourseCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[CourseCategoryTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CourseCategoryTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[CoursePrerequisite](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CourseId] [int] NULL,
	[PrerequisiteCourseId] [int] NULL,
 CONSTRAINT [PK_PrerequisiteCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[CourseResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NULL,
	[Type] [int] NULL,
	[Link] [nvarchar](500) NULL,
	[LectureId] [int] NULL,
	[CourseId] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CourseResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[CourseResourceTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[LanguageId] [int] NULL,
	[CourseResourceId] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CourseResourceTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[CourseTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[AcquiredSkills] [nvarchar](max) NULL,
	[TargetGroup] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[LanguageId] [int] NOT NULL,
	[CourseName] [nvarchar](200) NULL,
	[Requirements] [nvarchar](max) NULL,
 CONSTRAINT [PK_CourseTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[EnrollCourseExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Duration] [int] NULL,
	[CategoryId] [int] NULL,
	[EnrollTeacherCourseId] [int] NULL,
	[PublishDate] [datetime2](7) NOT NULL,
	[PublishEndDate] [datetime2](7) NULL,
	[IsPrerequisite] [bit] NULL,
 CONSTRAINT [PK_EnrollCourseExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollCourseExamQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[EnrollCourseExamId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollCourseExamQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollCourseExamTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollCourseExamId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_EnrollCourseExamTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollCourseResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](500) NULL,
	[Type] [int] NULL,
	[Link] [nvarchar](500) NULL,
	[EnrollLectureId] [int] NULL,
	[EnrollCourseId] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EnrollCourseResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[EnrollCourseResourceTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[LanguageId] [int] NULL,
	[EnrollCourseResourceId] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EnrollCourseResourceTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]





CREATE TABLE [dbo].[EnrollCourseTime](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollCourseId] [int] NOT NULL,
	[DayId] [int] NOT NULL,
	[FromTime] [time](7) NULL,
	[ToTime] [time](7) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[LearningMethodId] [int] NULL,
 CONSTRAINT [PK_EnrollCourseTime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollCourseTimeCustomization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollCourseId] [int] NOT NULL,
	[DayId] [int] NOT NULL,
	[Date] [date] NULL,
	[FromTime] [time](7) NULL,
	[ToTime] [time](7) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[LearningMethodId] [int] NULL,
 CONSTRAINT [PK_EnrollCourseTimeCustomization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollLecture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[LectureName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[EnrollSectionId] [int] NULL,
 CONSTRAINT [PK_EnrollLecture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollLectureTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LectureName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[EnrollLectureId] [int] NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollLectureTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollSectionOfCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[SectionName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[EnrollCourseId] [int] NULL,
 CONSTRAINT [PK_EnrollSectionOfCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollSectionOfCourseTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[EnrollSectionId] [int] NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_EnrollSectionOfCourseTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollTeacherCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CourseName] [nvarchar](200) NOT NULL,
	[LearningMethodId] [int] NULL,
	[TeacherId] [int] NULL,
	[SemesterId] [int] NULL,
	[SectionName] [nvarchar](200) NULL,
	[PublicationDate] [date] NOT NULL,
	[PublicationEndDate] [date] NULL,
	[WorkStartDate] [date] NOT NULL,
	[WorkEndDate] [date] NULL,
	[CourseId] [int] NOT NULL,
	[BranchId] [int] NULL,
	[CountOfStudent] [int] NULL,
 CONSTRAINT [PK_EnrollTeacherCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[EnrollTeacherCourseTranlation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[CourseName] [nvarchar](200) NOT NULL,
	[SectionName] [nvarchar](200) NULL,
	[LanguageId] [int] NOT NULL,
	[EnrollCourseId] [int] NULL,
 CONSTRAINT [PK_EnrollTeacherCourseTranlation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[ExamQuestion](
	[Id] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[TemplateId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL
) ON [PRIMARY]





CREATE TABLE [dbo].[ExamTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Duration] [int] NULL,
	[CategoryId] [int] NULL,
	[CourseId] [int] NULL,
 CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[ExamTemplateTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[LanguageId] [int] NOT NULL,
	[ExamId] [int] NULL,
 CONSTRAINT [PK_TemplateTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[Lecture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[LectureName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[SectionId] [int] NULL,
 CONSTRAINT [PK_Lecture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[LectureTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LectureName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[LectureId] [int] NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_LectureTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Mark] [int] NULL,
	[CategoryId] [int] NULL,
	[CourseId] [int] NULL,
	[Type] [int] NOT NULL,
	[CertifiedBankQuestion] [bit] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[QuestionOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[QuestionId] [int] NULL,
 CONSTRAINT [PK_QuestionOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[QuestionOptionTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[LanguageId] [int] NOT NULL,
	[OptionId] [int] NULL,
 CONSTRAINT [PK_QuestionOptionTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[QuestionTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[LanguageId] [int] NOT NULL,
	[QuestionId] [int] NULL,
 CONSTRAINT [PK_QuestionTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[SectionOfCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[SectionName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[CourseId] [int] NULL,
 CONSTRAINT [PK_SectionOfCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]





CREATE TABLE [dbo].[SectionOfCourseTranslation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionName] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[SectionId] [int] NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_SectionOfCourseTranslation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]















CREATE TABLE [dbo].[StudentSubscription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[EnrollTeacherCourseId] [int] NOT NULL,
	[SubscriptionDate] [datetime] NULL,
	[PaymentWayId] [int] NOT NULL,
	[Price] [decimal](9, 2) NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[CurrencyRate] [float] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[FinalPrice] [decimal](9, 2) NULL,
 CONSTRAINT [PK_StudentSubscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_CourseCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CourseCategory] ([Id])

ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_CourseCategory]

ALTER TABLE [dbo].[CourseCategoryTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CourseCategoryTranslation_CourseCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CourseCategory] ([Id])

ALTER TABLE [dbo].[CourseCategoryTranslation] CHECK CONSTRAINT [FK_CourseCategoryTranslation_CourseCategory]

ALTER TABLE [dbo].[CoursePrerequisite]  WITH CHECK ADD  CONSTRAINT [FK_PrerequisiteCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[CoursePrerequisite] CHECK CONSTRAINT [FK_PrerequisiteCourse_Course]

ALTER TABLE [dbo].[CoursePrerequisite]  WITH CHECK ADD  CONSTRAINT [FK_PrerequisiteCourse_Course1] FOREIGN KEY([PrerequisiteCourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[CoursePrerequisite] CHECK CONSTRAINT [FK_PrerequisiteCourse_Course1]

ALTER TABLE [dbo].[CourseResource]  WITH CHECK ADD  CONSTRAINT [FK_CourseResource_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[CourseResource] CHECK CONSTRAINT [FK_CourseResource_Course]

ALTER TABLE [dbo].[CourseResource]  WITH CHECK ADD  CONSTRAINT [FK_CourseResource_Lecture] FOREIGN KEY([LectureId])
REFERENCES [dbo].[Lecture] ([Id])

ALTER TABLE [dbo].[CourseResource] CHECK CONSTRAINT [FK_CourseResource_Lecture]

ALTER TABLE [dbo].[CourseResourceTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CourseResourceTranslation_CourseResource] FOREIGN KEY([CourseResourceId])
REFERENCES [dbo].[CourseResource] ([Id])

ALTER TABLE [dbo].[CourseResourceTranslation] CHECK CONSTRAINT [FK_CourseResourceTranslation_CourseResource]

ALTER TABLE [dbo].[CourseTranslation]  WITH CHECK ADD  CONSTRAINT [FK_CourseTranslation_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[CourseTranslation] CHECK CONSTRAINT [FK_CourseTranslation_Course]

ALTER TABLE [dbo].[EnrollCourseExam]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseExam_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollCourseExam] CHECK CONSTRAINT [FK_EnrollCourseExam_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollCourseExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseExamQuestion_EnrollCourseExam] FOREIGN KEY([EnrollCourseExamId])
REFERENCES [dbo].[EnrollCourseExam] ([Id])

ALTER TABLE [dbo].[EnrollCourseExamQuestion] CHECK CONSTRAINT [FK_EnrollCourseExamQuestion_EnrollCourseExam]

ALTER TABLE [dbo].[EnrollCourseExamTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseExamTranslation_EnrollCourseExam] FOREIGN KEY([EnrollCourseExamId])
REFERENCES [dbo].[EnrollCourseExam] ([Id])

ALTER TABLE [dbo].[EnrollCourseExamTranslation] CHECK CONSTRAINT [FK_EnrollCourseExamTranslation_EnrollCourseExam]

ALTER TABLE [dbo].[EnrollCourseResource]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseResource_EnrollLecture] FOREIGN KEY([EnrollLectureId])
REFERENCES [dbo].[EnrollLecture] ([Id])

ALTER TABLE [dbo].[EnrollCourseResource] CHECK CONSTRAINT [FK_EnrollCourseResource_EnrollLecture]

ALTER TABLE [dbo].[EnrollCourseResource]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseResource_EnrollTeacherCourse] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollCourseResource] CHECK CONSTRAINT [FK_EnrollCourseResource_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollCourseResourceTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseResourceTranslation_EnrollCourseResource] FOREIGN KEY([EnrollCourseResourceId])
REFERENCES [dbo].[EnrollCourseResource] ([Id])

ALTER TABLE [dbo].[EnrollCourseResourceTranslation] CHECK CONSTRAINT [FK_EnrollCourseResourceTranslation_EnrollCourseResource]

ALTER TABLE [dbo].[EnrollCourseTime]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseTime_EnrollTeacherCourse] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollCourseTime] CHECK CONSTRAINT [FK_EnrollCourseTime_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollCourseTimeCustomization]  WITH CHECK ADD  CONSTRAINT [FK_EnrollCourseTimeCustomization_EnrollTeacherCourse] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollCourseTimeCustomization] CHECK CONSTRAINT [FK_EnrollCourseTimeCustomization_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollLecture]  WITH CHECK ADD  CONSTRAINT [FK_EnrollLecture_EnrollSectionOfCourse] FOREIGN KEY([EnrollSectionId])
REFERENCES [dbo].[EnrollSectionOfCourse] ([Id])

ALTER TABLE [dbo].[EnrollLecture] CHECK CONSTRAINT [FK_EnrollLecture_EnrollSectionOfCourse]

ALTER TABLE [dbo].[EnrollLectureTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollLectureTranslation_EnrollLecture] FOREIGN KEY([EnrollLectureId])
REFERENCES [dbo].[EnrollLecture] ([Id])

ALTER TABLE [dbo].[EnrollLectureTranslation] CHECK CONSTRAINT [FK_EnrollLectureTranslation_EnrollLecture]

ALTER TABLE [dbo].[EnrollSectionOfCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollSectionOfCourse_EnrollTeacherCourse] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollSectionOfCourse] CHECK CONSTRAINT [FK_EnrollSectionOfCourse_EnrollTeacherCourse]

ALTER TABLE [dbo].[EnrollSectionOfCourseTranslation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollSectionOfCourseTranslation_EnrollSectionOfCourse] FOREIGN KEY([EnrollSectionId])
REFERENCES [dbo].[EnrollSectionOfCourse] ([Id])

ALTER TABLE [dbo].[EnrollSectionOfCourseTranslation] CHECK CONSTRAINT [FK_EnrollSectionOfCourseTranslation_EnrollSectionOfCourse]

ALTER TABLE [dbo].[EnrollTeacherCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollTeacherCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[EnrollTeacherCourse] CHECK CONSTRAINT [FK_EnrollTeacherCourse_Course]

ALTER TABLE [dbo].[EnrollTeacherCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollTeacherCourse_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])

ALTER TABLE [dbo].[EnrollTeacherCourse] CHECK CONSTRAINT [FK_EnrollTeacherCourse_Semester]

ALTER TABLE [dbo].[EnrollTeacherCourseTranlation]  WITH CHECK ADD  CONSTRAINT [FK_EnrollTeacherCourseTranlation_EnrollTeacherCourse] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[EnrollTeacherCourseTranlation] CHECK CONSTRAINT [FK_EnrollTeacherCourseTranlation_EnrollTeacherCourse]

ALTER TABLE [dbo].[ExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_ExamQuestion_ExamTemplate] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[ExamTemplate] ([Id])

ALTER TABLE [dbo].[ExamQuestion] CHECK CONSTRAINT [FK_ExamQuestion_ExamTemplate]

ALTER TABLE [dbo].[ExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_ExamQuestion_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])

ALTER TABLE [dbo].[ExamQuestion] CHECK CONSTRAINT [FK_ExamQuestion_Question]

ALTER TABLE [dbo].[ExamTemplate]  WITH CHECK ADD  CONSTRAINT [FK_ExamTemplate_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[ExamTemplate] CHECK CONSTRAINT [FK_ExamTemplate_Course]

ALTER TABLE [dbo].[ExamTemplate]  WITH CHECK ADD  CONSTRAINT [FK_ExamTemplate_CourseCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CourseCategory] ([Id])

ALTER TABLE [dbo].[ExamTemplate] CHECK CONSTRAINT [FK_ExamTemplate_CourseCategory]

ALTER TABLE [dbo].[ExamTemplateTranslation]  WITH CHECK ADD  CONSTRAINT [FK_ExamTemplateTranslation_ExamTemplate] FOREIGN KEY([ExamId])
REFERENCES [dbo].[ExamTemplate] ([Id])

ALTER TABLE [dbo].[ExamTemplateTranslation] CHECK CONSTRAINT [FK_ExamTemplateTranslation_ExamTemplate]

ALTER TABLE [dbo].[Lecture]  WITH CHECK ADD  CONSTRAINT [FK_Lecture_SectionOfCourse] FOREIGN KEY([SectionId])
REFERENCES [dbo].[SectionOfCourse] ([Id])

ALTER TABLE [dbo].[Lecture] CHECK CONSTRAINT [FK_Lecture_SectionOfCourse]

ALTER TABLE [dbo].[LectureTranslation]  WITH CHECK ADD  CONSTRAINT [FK_LectureTranslation_Lecture] FOREIGN KEY([LectureId])
REFERENCES [dbo].[Lecture] ([Id])

ALTER TABLE [dbo].[LectureTranslation] CHECK CONSTRAINT [FK_LectureTranslation_Lecture]

ALTER TABLE [dbo].[QuestionOption]  WITH CHECK ADD  CONSTRAINT [FK_QuestionOption_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])

ALTER TABLE [dbo].[QuestionOption] CHECK CONSTRAINT [FK_QuestionOption_Question]

ALTER TABLE [dbo].[QuestionOptionTranslation]  WITH CHECK ADD  CONSTRAINT [FK_QuestionOptionTranslation_QuestionOption] FOREIGN KEY([OptionId])
REFERENCES [dbo].[QuestionOption] ([Id])

ALTER TABLE [dbo].[QuestionOptionTranslation] CHECK CONSTRAINT [FK_QuestionOptionTranslation_QuestionOption]

ALTER TABLE [dbo].[QuestionTranslation]  WITH CHECK ADD  CONSTRAINT [FK_QuestionTranslation_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])

ALTER TABLE [dbo].[QuestionTranslation] CHECK CONSTRAINT [FK_QuestionTranslation_Question]

ALTER TABLE [dbo].[SectionOfCourse]  WITH CHECK ADD  CONSTRAINT [FK_SectionOfCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])

ALTER TABLE [dbo].[SectionOfCourse] CHECK CONSTRAINT [FK_SectionOfCourse_Course]

ALTER TABLE [dbo].[SectionOfCourseTranslation]  WITH CHECK ADD  CONSTRAINT [FK_SectionOfCourseTranslation_SectionOfCourse] FOREIGN KEY([SectionId])
REFERENCES [dbo].[SectionOfCourse] ([Id])

ALTER TABLE [dbo].[SectionOfCourseTranslation] CHECK CONSTRAINT [FK_SectionOfCourseTranslation_SectionOfCourse]



ALTER TABLE [dbo].[StudentSubscription]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubscription_EnrollTeacherCourse] FOREIGN KEY([EnrollTeacherCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])

ALTER TABLE [dbo].[StudentSubscription] CHECK CONSTRAINT [FK_StudentSubscription_EnrollTeacherCourse]

