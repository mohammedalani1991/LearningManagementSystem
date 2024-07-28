

IF COL_LENGTH('Question','TeacherId') IS NULL
BEGIN
ALTER TABLE  [dbo].[Question] ADD  [TeacherId]  int  NULL;
END



   
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Trainer] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Trainer] ([Id])
