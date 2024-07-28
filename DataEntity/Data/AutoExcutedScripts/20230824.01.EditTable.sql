IF COL_LENGTH('Permission','SuperAdminId') IS NULL
BEGIN
ALTER TABLE  [dbo].[Permission] add  [SuperAdminId] [int] NULL;
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_SuperAdmin] FOREIGN KEY([SuperAdminId])
REFERENCES [dbo].[SuperAdmin] ([Id])
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_SuperAdmin]
END

IF COL_LENGTH('SystemSetting','SuperAdminId') IS NULL
BEGIN
ALTER TABLE  [dbo].[SystemSetting] add  [SuperAdminId] [int] NULL;
ALTER TABLE [dbo].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_SuperAdmin] FOREIGN KEY([SuperAdminId])
REFERENCES [dbo].[SuperAdmin] ([Id])
ALTER TABLE [dbo].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_SuperAdmin]
END
