  alter table SenangPay add ProjectId int 


  ALTER TABLE [dbo].[SenangPay]  WITH CHECK ADD  CONSTRAINT [FK_SenangPay_CmsProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[CmsProject] ([Id])

ALTER TABLE [dbo].[SenangPay] CHECK CONSTRAINT [FK_SenangPay_CmsProject]