
IF COL_LENGTH('dbo.CmsPageTranslation','ShortDescription') IS NULL
begin
ALTER TABLE dbo.CmsPageTranslation 
ADD ShortDescription nVARCHAR(max) NULL
end

ALTER TABLE  dbo.CmsPage DROP COLUMN SortOrder;
ALTER TABLE dbo.CmsPage ADD SortOrder int NULL;