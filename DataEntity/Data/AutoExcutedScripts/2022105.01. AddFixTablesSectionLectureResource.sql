
delete from [dbo].[CourseResourceTranslation];
ALTER TABLE  [dbo].[CourseResourceTranslation] ALTER COLUMN [Name]  NVARCHAR(500) NOT NULL;
ALTER TABLE  [dbo].[CourseResourceTranslation] ALTER COLUMN [CourseResourceId]  INT NOT NULL;
ALTER TABLE  [dbo].[CourseResourceTranslation] ALTER COLUMN [LanguageId]  INT NOT NULL;


delete from [dbo].[CourseResource];
ALTER TABLE  [dbo].[CourseResource] ALTER COLUMN [Name]  NVARCHAR(500) NOT NULL;
ALTER TABLE  [dbo].[CourseResource] ALTER COLUMN [LectureId]  INT NOT NULL;
ALTER TABLE  [dbo].[CourseResource] ALTER COLUMN [CourseId]  INT NOT NULL;
ALTER TABLE  [dbo].[CourseResource] ALTER COLUMN [Type]  INT NOT NULL;
ALTER TABLE  [dbo].[CourseResource] ALTER COLUMN [Link]  NVARCHAR(500) NOT NULL;



delete from [dbo].[LectureTranslation];
ALTER TABLE  [dbo].[LectureTranslation] ALTER COLUMN [LectureName]  NVARCHAR(500) NOT NULL;
ALTER TABLE  [dbo].[LectureTranslation] ALTER COLUMN [LectureId]  INT NOT NULL;

delete from [dbo].[Lecture];
ALTER TABLE  [dbo].[Lecture] ALTER COLUMN [LectureName]  NVARCHAR(500) NOT NULL;
ALTER TABLE  [dbo].[Lecture] ALTER COLUMN [SectionId]  INT NOT NULL;




delete from [dbo].[SectionOfCourseTranslation];
ALTER TABLE  [dbo].[SectionOfCourseTranslation] ALTER COLUMN [SectionName]  NVARCHAR(500) NOT NULL;
ALTER TABLE  [dbo].[SectionOfCourseTranslation] ALTER COLUMN [SectionId]  int NOT NULL;

delete from [dbo].[SectionOfCourse];
ALTER TABLE  [dbo].[SectionOfCourse] ALTER COLUMN [SectionName]  NVARCHAR(500) NOT NULL;
ALTER TABLE  [dbo].[SectionOfCourse] ALTER COLUMN [CourseId]  INT NOT NULL;










  
   
