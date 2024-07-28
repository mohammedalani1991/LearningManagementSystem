
delete from [dbo].[EnrollCourseResourceTranslation];
delete from [dbo].[EnrollCourseResource];

delete from [dbo].[EnrollLectureTranslation];
delete from [dbo].[EnrollLecture];

delete from [dbo].[EnrollSectionOfCourseTranslation];
delete from [dbo].[EnrollSectionOfCourse];

delete from [dbo].[EnrollCourseExamQuestion];

delete from [dbo].[EnrollCourseExam];
delete from [dbo].[EnrollCourseExamTranslation];


delete from [dbo].[EnrollAssignmentTranslation];
delete from [dbo].[EnrollAssignment];

delete from [dbo].[EnrollCourseTime];

delete from [dbo].[EnrollTeacherCourseTranlation];
delete from [dbo].[EnrollTeacherCourse];


ALTER TABLE  [dbo].[EnrollTeacherCourse] ALTER COLUMN [TeacherId]  INT NOT NULL;


ALTER TABLE  [dbo].[EnrollTeacherCourseTranlation] ALTER COLUMN [EnrollCourseId]  INT NOT NULL;

ALTER TABLE  [dbo].[EnrollSectionOfCourseTranslation] ALTER COLUMN [EnrollSectionId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollSectionOfCourseTranslation] ALTER COLUMN [SectionName]  nvarchar(500) NOT NULL;


ALTER TABLE  [dbo].[EnrollSectionOfCourse] ALTER COLUMN [EnrollCourseId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollSectionOfCourse] ALTER COLUMN [SectionName]  nvarchar(500) NOT NULL;



ALTER TABLE  [dbo].[EnrollLecture] ALTER COLUMN [EnrollSectionId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollLecture] ALTER COLUMN [LectureName]  nvarchar(500) NOT NULL;



ALTER TABLE  [dbo].[EnrollLectureTranslation] ALTER COLUMN [EnrollLectureId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollLectureTranslation] ALTER COLUMN [LectureName]  nvarchar(500) NOT NULL;

IF COL_LENGTH('EnrollLecture','EnrollCourseId') IS NULL
BEGIN
ALTER TABLE  [dbo].[EnrollLecture] ADD  [EnrollCourseId]  INT NOT NULL;
END



ALTER TABLE  [dbo].[EnrollCourseResourceTranslation] ALTER COLUMN [EnrollCourseResourceId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollCourseResourceTranslation] ALTER COLUMN [LanguageId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollCourseResourceTranslation] ALTER COLUMN [Name]  nvarchar(500) NOT NULL;


ALTER TABLE  [dbo].[EnrollCourseResource] ALTER COLUMN [EnrollLectureId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollCourseResource] ALTER COLUMN [EnrollCourseId]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollCourseResource] ALTER COLUMN [Name]  nvarchar(500) NOT NULL;
ALTER TABLE  [dbo].[EnrollCourseResource] ALTER COLUMN [Type]  INT NOT NULL;
ALTER TABLE  [dbo].[EnrollCourseResource] ALTER COLUMN [Link]  nvarchar(500) NOT NULL;



ALTER TABLE  [dbo].[EnrollCourseExam] ALTER COLUMN [PublishDate]  datetime  NULL;
ALTER TABLE  [dbo].[EnrollCourseExam] ALTER COLUMN [EnrollTeacherCourseId]  INT NOT NULL;


ALTER TABLE  [dbo].[EnrollCourseExamQuestion] ALTER COLUMN [QuestionId]  INT NOT NULL;


IF COL_LENGTH('EnrollTeacherCourseTranlation','CreatedOn') IS NOT NULL
BEGIN
ALTER table [dbo].[EnrollTeacherCourseTranlation] drop  COLUMN CreatedOn
ALTER table [dbo].[EnrollTeacherCourseTranlation] drop  COLUMN CreatedBy
ALTER table [dbo].[EnrollTeacherCourseTranlation] drop  COLUMN [Status]
ALTER table [dbo].[EnrollTeacherCourseTranlation] drop  COLUMN DeletedOn
END




ALTER TABLE [EnrollAssignment]
DROP CONSTRAINT FK_EnrollAssignment_EnrollCourseExam

IF NOT EXISTS(
    SELECT 1 FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[EnrollTeacherCourse]') 
        AND name = 'FK_EnrollTeacherCourse_Trainer'
)
BEGIN 
   
ALTER TABLE [dbo].[EnrollTeacherCourse]  WITH CHECK ADD  CONSTRAINT [FK_EnrollTeacherCourse_Trainer] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Trainer] ([Id])
END 


IF NOT EXISTS(
    SELECT 1 FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[EnrollLecture]') 
        AND name = 'FK_EnrollLecture_EnrollTeacherCourse'
)
BEGIN 
   
ALTER TABLE [dbo].[EnrollLecture]   WITH CHECK ADD  CONSTRAINT [FK_EnrollLecture_EnrollTeacherCourse] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
END 


IF NOT EXISTS(
    SELECT 1 FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[EnrollAssignment]') 
        AND name = 'FK_EnrollAssignment_EnrollCourseExam'
)
BEGIN 
   
ALTER TABLE [dbo].[EnrollAssignment]   WITH CHECK ADD  CONSTRAINT [FK_EnrollAssignment_EnrollCourseExam] FOREIGN KEY([EnrollCourseId])
REFERENCES [dbo].[EnrollTeacherCourse] ([Id])
END 


