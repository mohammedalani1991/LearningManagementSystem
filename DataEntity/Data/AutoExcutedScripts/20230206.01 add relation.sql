ALTER TABLE [dbo].[CmsProjectDonor]  WITH CHECK ADD  CONSTRAINT [FK_CmsProjectDonor_CmsProjectCost] FOREIGN KEY([ProjectCostId])
REFERENCES [dbo].[CmsProjectCost] ([Id])

ALTER TABLE [dbo].[CmsProjectDonor] CHECK CONSTRAINT [FK_CmsProjectDonor_CmsProjectCost]