alter table SenangPay add ProjectCostId int 


alter table SenangPay add PhoneNumber nvarchar(100) 

ALTER TABLE [dbo].[SenangPay]  WITH CHECK ADD  CONSTRAINT [FK_SenangPay_CmsProjectCost] FOREIGN KEY([ProjectCostId])
REFERENCES [dbo].[CmsProjectCost] ([Id])

ALTER TABLE [dbo].[SenangPay] CHECK CONSTRAINT [FK_SenangPay_CmsProjectCost]


