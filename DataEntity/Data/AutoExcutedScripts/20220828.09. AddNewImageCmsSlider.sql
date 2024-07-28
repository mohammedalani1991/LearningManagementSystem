IF COL_LENGTH('[dbo].[CmsSlider]','Image2Url') IS NULL
BEGIN
Alter table  [dbo].[CmsSlider] Add Image2Url nvarchar(max) null
END
