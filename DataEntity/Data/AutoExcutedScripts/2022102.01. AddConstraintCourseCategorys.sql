IF NOT EXISTS(
    SELECT 1 FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[CourseCategory]') 
        AND name = 'FK_CourseCategory_CourseCategory'
)
BEGIN 
   
ALTER TABLE [dbo].[CourseCategory]  WITH CHECK ADD  CONSTRAINT [FK_CourseCategory_CourseCategory] FOREIGN KEY([ParentId])
REFERENCES [dbo].[CourseCategory] ([Id])
END 